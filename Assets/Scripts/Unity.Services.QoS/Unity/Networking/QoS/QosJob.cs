namespace Unity.Networking.QoS
{
	internal struct QosJob : global::Unity.Services.Qos.Runner.IQosJob, global::Unity.Jobs.IJob
	{
		private struct InternalQosServer
		{
			public readonly global::Unity.Networking.QoS.NetworkEndPoint RemoteEndpoint;

			public readonly global::System.DateTime BackoffUntilUtc;

			public readonly int Idx;

			private int m_FirstIdx;

			private ushort m_RequestIdentifier;

			public int FirstIdx
			{
				get
				{
					return m_FirstIdx;
				}
				set
				{
					m_FirstIdx = value;
				}
			}

			public ushort RequestIdentifier
			{
				get
				{
					return m_RequestIdentifier;
				}
				set
				{
					m_RequestIdentifier = value;
				}
			}

			public bool Duplicate => m_FirstIdx != Idx;

			public string Address => RemoteEndpoint.Address;

			public InternalQosServer(global::Unity.Networking.QoS.NetworkEndPoint remote, global::System.DateTime backoffUntilUtc, int idx)
			{
				RemoteEndpoint = remote;
				BackoffUntilUtc = backoffUntilUtc;
				Idx = idx;
				m_FirstIdx = idx;
				m_RequestIdentifier = 0;
			}
		}

		private uint RequestsPerEndpoint;

		private ulong TimeoutMs;

		private ulong MaxWaitMs;

		private uint RequestsBetweenPause;

		private uint RequestPauseMs;

		private uint ReceiveWaitMs;

		private global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.InternalQosResult> _qosResults;

		[global::Unity.Collections.DeallocateOnJobCompletion]
		private global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.QosJob.InternalQosServer> m_QosServers;

		[global::Unity.Collections.DeallocateOnJobCompletion]
		private global::Unity.Collections.NativeArray<byte> m_TitleBytesUtf8;

		private global::Unity.Collections.NativeHashMap<global::Unity.Collections.FixedString64Bytes, int> m_AddressIndexes;

		private global::System.DateTime m_JobExpireTimeUtc;

		private int m_Requests;

		private int m_Responses;

		public global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.InternalQosResult> QosResults => _qosResults;

		public global::Unity.Jobs.JobHandle Schedule<T>(global::Unity.Jobs.JobHandle dependsOn = default(global::Unity.Jobs.JobHandle)) where T : struct, global::Unity.Jobs.IJob
		{
			return global::Unity.Jobs.IJobExtensions.Schedule(this, dependsOn);
		}

		internal QosJob(global::System.Collections.Generic.IList<global::Unity.Networking.QoS.UcgQosServer> qosServers, string title, uint requestsPerEndpoint = 5u, ulong timeoutMs = 10000uL, ulong maxWaitMs = 500uL, uint requestsBetweenPause = 10u, uint requestPauseMs = 1u, uint receiveWaitMs = 10u)
		{
			this = default(global::Unity.Networking.QoS.QosJob);
			RequestsPerEndpoint = requestsPerEndpoint;
			TimeoutMs = timeoutMs;
			MaxWaitMs = maxWaitMs;
			RequestsBetweenPause = requestsBetweenPause;
			RequestPauseMs = requestPauseMs;
			ReceiveWaitMs = receiveWaitMs;
			m_AddressIndexes = new global::Unity.Collections.NativeHashMap<global::Unity.Collections.FixedString64Bytes, int>(qosServers?.Count ?? 0, global::Unity.Collections.Allocator.Persistent);
			m_QosServers = new global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.QosJob.InternalQosServer>(qosServers?.Count ?? 0, global::Unity.Collections.Allocator.Persistent);
			if (qosServers != null)
			{
				int num = 0;
				foreach (global::Unity.Networking.QoS.UcgQosServer qosServer in qosServers)
				{
					if (!global::Unity.Networking.QoS.NetworkEndPoint.TryParse(qosServer.ipv4, qosServer.port, out var endpoint))
					{
						global::UnityEngine.Debug.LogError("QosJob: Invalid IP address " + qosServer.ipv4 + " in QoS Servers list");
						continue;
					}
					global::Unity.Networking.QoS.QosJob.InternalQosServer server = new global::Unity.Networking.QoS.QosJob.InternalQosServer(endpoint, qosServer.BackoffUntilUtc, num);
					if (m_AddressIndexes.ContainsKey(server.Address))
					{
						server.FirstIdx = m_AddressIndexes[server.Address];
					}
					else
					{
						m_AddressIndexes.Add(server.Address, num);
					}
					StoreServer(server);
					num++;
				}
				if (num < m_QosServers.Length)
				{
					global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.QosJob.InternalQosServer> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.QosJob.InternalQosServer>(num, global::Unity.Collections.Allocator.Persistent);
					m_QosServers.GetSubArray(0, nativeArray.Length).CopyTo(nativeArray);
					m_QosServers.Dispose();
					m_QosServers = nativeArray;
				}
			}
			_qosResults = new global::Unity.Collections.NativeArray<global::Unity.Networking.QoS.InternalQosResult>(m_QosServers.Length, global::Unity.Collections.Allocator.Persistent);
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(title);
			m_TitleBytesUtf8 = new global::Unity.Collections.NativeArray<byte>(bytes.Length, global::Unity.Collections.Allocator.Persistent);
			m_TitleBytesUtf8.CopyFrom(bytes);
		}

		public void Dispose()
		{
			if (m_AddressIndexes.IsCreated)
			{
				m_AddressIndexes.Dispose();
			}
		}

		public void Execute()
		{
			if (m_QosServers.Length != 0)
			{
				m_Requests = 0;
				m_Responses = 0;
				m_JobExpireTimeUtc = global::System.DateTime.UtcNow.AddMilliseconds(TimeoutMs);
				var (baselib_Socket_Handle, baselib_ErrorCode) = CreateAndBindSocket();
				if (baselib_ErrorCode != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					global::UnityEngine.Debug.LogError($"QosJob: failed to create and bind the local socket (errorcode {baselib_ErrorCode})");
					return;
				}
				ProcessServers(baselib_Socket_Handle);
				global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Close(baselib_Socket_Handle);
			}
		}

		private void ProcessServers(global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socketHandle)
		{
			global::Unity.Networking.QoS.NetworkEndPoint addr = default(global::Unity.Networking.QoS.NetworkEndPoint);
			foreach (global::Unity.Networking.QoS.QosJob.InternalQosServer qosServer in m_QosServers)
			{
				if (!qosServer.Duplicate)
				{
					ProcessServer(qosServer, socketHandle);
					RecvQosResponsesTimed(addr, m_JobExpireTimeUtc, socketHandle, wait: false);
				}
			}
			global::System.DateTime dateTime = global::System.DateTime.UtcNow.AddMilliseconds(MaxWaitMs);
			if (m_JobExpireTimeUtc < dateTime)
			{
				dateTime = m_JobExpireTimeUtc;
			}
			string text = EnableReceiveWait();
			if (text != "")
			{
				global::UnityEngine.Debug.LogError(text);
				return;
			}
			RecvQosResponsesTimed(addr, dateTime, socketHandle, wait: true);
			foreach (global::Unity.Networking.QoS.QosJob.InternalQosServer qosServer2 in m_QosServers)
			{
				global::Unity.Networking.QoS.InternalQosResult result = (qosServer2.Duplicate ? _qosResults[qosServer2.FirstIdx] : _qosResults[qosServer2.Idx]);
				StoreResult(qosServer2.Idx, result);
			}
		}

		private void ProcessServer(global::Unity.Networking.QoS.QosJob.InternalQosServer server, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socketHandle)
		{
			if (global::Unity.Networking.QoS.QosHelper.ExpiredUtc(m_JobExpireTimeUtc))
			{
				global::UnityEngine.Debug.LogWarning("QosJob: not enough time to process " + server.Address + ".");
				return;
			}
			if (global::System.DateTime.UtcNow < server.BackoffUntilUtc)
			{
				global::UnityEngine.Debug.LogWarning("QosJob: skipping " + server.Address + " due to backoff restrictions");
				return;
			}
			global::Unity.Networking.QoS.InternalQosResult result = _qosResults[server.Idx];
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode baselib_ErrorCode = SendQosRequests(server, socketHandle, ref result);
			if (baselib_ErrorCode != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				global::UnityEngine.Debug.LogError($"QosJob: failed to send to {server.Address} (errorcode {baselib_ErrorCode})");
			}
			StoreResult(server.Idx, result);
		}

		private global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode SendQosRequests(global::Unity.Networking.QoS.QosJob.InternalQosServer server, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socketHandle, ref global::Unity.Networking.QoS.InternalQosResult result)
		{
			global::Unity.Networking.QoS.QosRequest qosRequest = new global::Unity.Networking.QoS.QosRequest
			{
				Title = m_TitleBytesUtf8.ToArray(),
				Identifier = (ushort)new global::System.Random().Next(0, 65535)
			};
			server.RequestIdentifier = qosRequest.Identifier;
			StoreServer(server);
			result.RequestsSent = 0u;
			do
			{
				if (global::Unity.Networking.QoS.QosHelper.ExpiredUtc(m_JobExpireTimeUtc))
				{
					global::UnityEngine.Debug.LogWarning($"QosJob: not enough time to complete {RequestsPerEndpoint - result.RequestsSent} sends to {server.Address} ");
					return global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Timeout;
				}
				qosRequest.Timestamp = (ulong)(global::System.DateTime.UtcNow.Ticks / 10000);
				qosRequest.Sequence = (byte)result.RequestsSent;
				var (num, num2) = qosRequest.Send(socketHandle.handle, server.RemoteEndpoint, m_JobExpireTimeUtc);
				if (num2 != 0)
				{
					global::UnityEngine.Debug.LogError($"QosJob: send returned error code {(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode)num2}, can't continue");
					return (global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode)num2;
				}
				if (num != qosRequest.Length)
				{
					global::UnityEngine.Debug.LogWarning($"QosJob: sent {num} of {qosRequest.Length} bytes, ignoring this request");
					result.InvalidRequests++;
					continue;
				}
				m_Requests++;
				result.RequestsSent++;
				if (RequestsBetweenPause != 0 && RequestPauseMs != 0 && m_Requests % RequestsBetweenPause == 0L)
				{
					global::System.Threading.Thread.Sleep((int)RequestPauseMs);
				}
			}
			while (result.RequestsSent < RequestsPerEndpoint);
			return global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success;
		}

		private void StoreServer(global::Unity.Networking.QoS.QosJob.InternalQosServer server)
		{
			m_QosServers[server.Idx] = server;
		}

		private void StoreResult(int idx, global::Unity.Networking.QoS.InternalQosResult result)
		{
			_qosResults[idx] = result;
		}

		private void RecvQosResponsesTimed(global::Unity.Networking.QoS.NetworkEndPoint addr, global::System.DateTime deadline, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socketHandle, bool wait)
		{
			RecvQosResponses(addr, deadline, socketHandle, wait);
		}

		private void RecvQosResponses(global::Unity.Networking.QoS.NetworkEndPoint addr, global::System.DateTime deadline, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle socketHandle, bool wait)
		{
			if (m_Requests == m_Responses)
			{
				return;
			}
			global::Unity.Networking.QoS.QosResponse qosResponse = new global::Unity.Networking.QoS.QosResponse();
			global::Unity.Networking.QoS.InternalQosResult result = _qosResults[0];
			while (m_Requests > m_Responses && !global::Unity.Networking.QoS.QosHelper.ExpiredUtc(deadline))
			{
				switch (qosResponse.Recv(socketHandle.handle, wait, deadline, ref addr).received)
				{
				case 0:
					if (!wait)
					{
						return;
					}
					continue;
				case -1:
					continue;
				}
				int num = LookupResult(addr, qosResponse, ref result);
				if (num < 0)
				{
					continue;
				}
				string error = "";
				if (!qosResponse.Verify(result.RequestsSent, ref error))
				{
					global::UnityEngine.Debug.LogWarning("QosJob: ignoring response from " + m_QosServers[num].Address + " verify failed with " + error);
					result.InvalidResponses++;
				}
				else
				{
					m_Responses++;
					result.ResponsesReceived++;
					result.AddAggregateLatency((uint)qosResponse.LatencyMs);
					(global::Unity.Networking.QoS.FcType, byte) tuple = qosResponse.ParseFlowControl();
					if (tuple.Item1 != global::Unity.Networking.QoS.FcType.None && tuple.Item2 > result.FcUnits)
					{
						(result.FcType, result.FcUnits) = tuple;
					}
				}
				StoreResult(num, result);
			}
		}

		private string EnableReceiveWait()
		{
			return "";
		}

		private int LookupResult(global::Unity.Networking.QoS.NetworkEndPoint endPoint, global::Unity.Networking.QoS.QosResponse response, ref global::Unity.Networking.QoS.InternalQosResult result)
		{
			if (m_AddressIndexes.TryGetValue(endPoint.Address, out var item))
			{
				result = _qosResults[item];
				global::Unity.Networking.QoS.QosJob.InternalQosServer internalQosServer = m_QosServers[item];
				if (response.Identifier != internalQosServer.RequestIdentifier)
				{
					global::UnityEngine.Debug.LogWarning($"QosJob: invalid identifier from {internalQosServer.Address} 0x{response.Identifier:X4} != 0x{internalQosServer.RequestIdentifier:X4} ignoring");
					result.InvalidResponses++;
					return -1;
				}
				return item;
			}
			global::UnityEngine.Debug.LogWarning("QosJob: ignoring unexpected response from " + endPoint.Address);
			return -1;
		}

		private unsafe static (global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle, global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode) CreateAndBindSocket()
		{
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Handle item = global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Create(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv4, global::Unity.Baselib.LowLevel.Binding.Baselib_Socket_Protocol.UDP, &baselib_ErrorState);
			if (baselib_ErrorState.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				global::UnityEngine.Debug.LogError($"QosJob: Unable to create socket {baselib_ErrorState.code}");
			}
			return (item, baselib_ErrorState.code);
		}
	}
}

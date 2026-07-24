namespace Unity.Networking.Transport
{
	internal struct ConnectionList : global::System.IDisposable
	{
		internal struct HostnameLookupTask
		{
			private unsafe global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_HostnameLookupHandle* m_LookupHandle;

			private global::Unity.Collections.FixedString512Bytes m_Address;

			private int RetryCount;

			public unsafe bool IsCreated => m_LookupHandle != null;

			public static bool Create(global::Unity.Collections.FixedString512Bytes address, out global::Unity.Networking.Transport.ConnectionList.HostnameLookupTask task, out global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress result, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error)
			{
				task = default(global::Unity.Networking.Transport.ConnectionList.HostnameLookupTask);
				return task.Initialize(address, out result, out error);
			}

			private unsafe bool Initialize(global::Unity.Collections.FixedString512Bytes address, out global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress result, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error)
			{
				byte* unsafePtr = address.GetUnsafePtr();
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselib_NetworkAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_HostnameLookupHandle* ptr;
				do
				{
					if (RetryCount > 10)
					{
						baselib_ErrorState.code = global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.NoSupportedAddressFound;
						error = baselib_ErrorState;
						result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
						return true;
					}
					ptr = global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_HostnameLookup(unsafePtr, &baselib_NetworkAddress, &baselib_ErrorState);
					RetryCount++;
				}
				while (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.TryAgain);
				error = baselib_ErrorState;
				if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					result = baselib_NetworkAddress;
					m_LookupHandle = ptr;
					m_Address = address;
					return true;
				}
				if (ptr != null)
				{
					result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
					return true;
				}
				result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
				return false;
			}

			public unsafe bool CheckStatus(out global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress result, out global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState error)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselib_NetworkAddress = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
				if (global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_HostnameLookupCheckStatus(m_LookupHandle, &baselib_NetworkAddress, &baselib_ErrorState))
				{
					if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.TryAgain)
					{
						if (RetryCount > 10)
						{
							baselib_ErrorState.code = global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.NoSupportedAddressFound;
							error = baselib_ErrorState;
							result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
							return true;
						}
						return Initialize(m_Address, out result, out error);
					}
					error = baselib_ErrorState;
					if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
					{
						result = baselib_NetworkAddress;
					}
					else
					{
						result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
					}
					m_LookupHandle = null;
					return true;
				}
				result = default(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress);
				error = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
				return false;
			}
		}

		private struct HostnameLookupData
		{
			public global::Unity.Networking.Transport.ConnectionList.HostnameLookupTask Task;

			public ushort Port;
		}

		private struct ConnectionData
		{
			public global::Unity.Networking.Transport.NetworkEndpoint Endpoint;

			public global::Unity.Networking.Transport.NetworkConnection.State State;

			public int PathMtu;
		}

		internal struct IncomingDisconnection
		{
			public global::Unity.Networking.Transport.ConnectionId Connection;

			public global::Unity.Networking.Transport.Error.DisconnectReason Reason;
		}

		private global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.ConnectionList.ConnectionData> m_Connections;

		private global::Unity.Collections.NativeHashMap<int, global::Unity.Networking.Transport.ConnectionList.HostnameLookupData> m_HostnameLookupTasks;

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> m_IncomingDisconnections;

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId> m_FinishedConnections;

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId> m_IncomingConnections;

		private global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId> m_FreeList;

		public int Count => m_Connections.Length;

		public bool IsCreated => m_Connections.IsCreated;

		internal global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId> FreeList => m_FreeList;

		internal global::Unity.Networking.Transport.ConnectionId ConnectionAt(int index)
		{
			return m_Connections.ConnectionAt(index);
		}

		internal global::Unity.Networking.Transport.NetworkEndpoint GetConnectionEndpoint(global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			return m_Connections[connectionId].Endpoint;
		}

		internal global::Unity.Networking.Transport.NetworkConnection.State GetConnectionState(global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			return m_Connections[connectionId].State;
		}

		internal int GetConnectionPathMtu(global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			return m_Connections[connectionId].PathMtu;
		}

		internal void SetConnectionPathMtu(global::Unity.Networking.Transport.ConnectionId connectionId, int pathMtu)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connectionId];
			value.PathMtu = pathMtu;
			m_Connections[connectionId] = value;
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionId> QueryFinishedConnections(global::Unity.Collections.Allocator allocator)
		{
			return m_FinishedConnections.ToArray(allocator);
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionId> QueryIncomingConnections(global::Unity.Collections.Allocator allocator)
		{
			return m_IncomingConnections.ToArray(allocator);
		}

		internal global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection> QueryIncomingDisconnections(global::Unity.Collections.Allocator allocator)
		{
			return m_IncomingDisconnections.ToArray(allocator);
		}

		public static global::Unity.Networking.Transport.ConnectionList Create()
		{
			return new global::Unity.Networking.Transport.ConnectionList(global::Unity.Collections.Allocator.Persistent);
		}

		private ConnectionList(global::Unity.Collections.Allocator allocator)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData defaultDataValue = new global::Unity.Networking.Transport.ConnectionList.ConnectionData
			{
				State = global::Unity.Networking.Transport.NetworkConnection.State.Disconnected,
				PathMtu = 1024
			};
			m_Connections = new global::Unity.Networking.Transport.ConnectionDataMap<global::Unity.Networking.Transport.ConnectionList.ConnectionData>(1, defaultDataValue, allocator);
			m_HostnameLookupTasks = new global::Unity.Collections.NativeHashMap<int, global::Unity.Networking.Transport.ConnectionList.HostnameLookupData>(1, allocator);
			m_IncomingDisconnections = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection>(allocator);
			m_FinishedConnections = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId>(allocator);
			m_FreeList = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId>(allocator);
			m_IncomingConnections = new global::Unity.Collections.NativeQueue<global::Unity.Networking.Transport.ConnectionId>(allocator);
		}

		public void Dispose()
		{
			m_Connections.Dispose();
			m_HostnameLookupTasks.Dispose();
			m_IncomingDisconnections.Dispose();
			m_FinishedConnections.Dispose();
			m_IncomingConnections.Dispose();
			m_FreeList.Dispose();
		}

		private global::Unity.Networking.Transport.ConnectionId GetNewConnection()
		{
			if (m_FreeList.TryDequeue(out var item))
			{
				return item;
			}
			return new global::Unity.Networking.Transport.ConnectionId
			{
				Id = m_Connections.Length,
				Version = 1
			};
		}

		internal global::Unity.Networking.Transport.ConnectionId StartConnecting(ref global::Unity.Networking.Transport.NetworkEndpoint address)
		{
			global::Unity.Networking.Transport.ConnectionId newConnection = GetNewConnection();
			m_Connections[newConnection] = new global::Unity.Networking.Transport.ConnectionList.ConnectionData
			{
				Endpoint = address,
				State = global::Unity.Networking.Transport.NetworkConnection.State.Connecting,
				PathMtu = 1024
			};
			return newConnection;
		}

		internal global::Unity.Networking.Transport.ConnectionId StartConnecting(global::Unity.Collections.FixedString512Bytes address, ushort port, out bool hostnameLookupFinished, out global::Unity.Networking.Transport.NetworkEndpoint resolvedEndpoint, out global::Unity.Networking.Transport.Error.DisconnectReason disconnectReason)
		{
			global::Unity.Networking.Transport.ConnectionId newConnection = GetNewConnection();
			global::Unity.Networking.Transport.ConnectionList.HostnameLookupTask.Create(address, out var task, out var result, out var _);
			if (task.IsCreated)
			{
				resolvedEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				m_Connections[newConnection] = new global::Unity.Networking.Transport.ConnectionList.ConnectionData
				{
					State = global::Unity.Networking.Transport.NetworkConnection.State.Connecting,
					PathMtu = 1024
				};
				m_HostnameLookupTasks[newConnection.Id] = new global::Unity.Networking.Transport.ConnectionList.HostnameLookupData
				{
					Task = task,
					Port = port
				};
				hostnameLookupFinished = false;
				disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.HostNotFound;
			}
			else
			{
				resolvedEndpoint = new global::Unity.Networking.Transport.NetworkEndpoint(result);
				resolvedEndpoint.Port = port;
				m_Connections[newConnection] = new global::Unity.Networking.Transport.ConnectionList.ConnectionData
				{
					Endpoint = resolvedEndpoint,
					State = global::Unity.Networking.Transport.NetworkConnection.State.Connecting,
					PathMtu = 1024
				};
				hostnameLookupFinished = true;
				disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.Default;
			}
			return newConnection;
		}

		internal bool CheckHostnameLookupStatus(ref global::Unity.Networking.Transport.ConnectionId connectionId, out global::Unity.Networking.Transport.NetworkEndpoint resolvedEndpoint, out global::Unity.Networking.Transport.Error.DisconnectReason disconnectReason)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connectionId];
			global::Unity.Networking.Transport.ConnectionList.HostnameLookupData item;
			bool flag = m_HostnameLookupTasks.TryGetValue(connectionId.Id, out item);
			if (value.State != global::Unity.Networking.Transport.NetworkConnection.State.Connecting || !flag || !item.Task.IsCreated)
			{
				resolvedEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.Default;
				return true;
			}
			if (item.Task.CheckStatus(out var result, out var error))
			{
				if (error.code != global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
				{
					disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.HostNotFound;
					resolvedEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
					return true;
				}
				value.Endpoint = new global::Unity.Networking.Transport.NetworkEndpoint(result);
				value.Endpoint.Port = item.Port;
				m_Connections[connectionId] = value;
				m_HostnameLookupTasks.Remove(connectionId.Id);
				resolvedEndpoint = value.Endpoint;
				disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.Default;
				return true;
			}
			disconnectReason = global::Unity.Networking.Transport.Error.DisconnectReason.Default;
			resolvedEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
			return false;
		}

		internal void FinishConnectingFromLocal(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			CompleteConnecting(ref connectionId);
			m_FinishedConnections.Enqueue(connectionId);
		}

		internal void FinishConnectingFromRemote(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			CompleteConnecting(ref connectionId);
			m_IncomingConnections.Enqueue(connectionId);
		}

		private void CompleteConnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connectionId];
			if (value.State == global::Unity.Networking.Transport.NetworkConnection.State.Connecting)
			{
				value.State = global::Unity.Networking.Transport.NetworkConnection.State.Connected;
				m_Connections[connectionId] = value;
			}
		}

		internal global::Unity.Networking.Transport.ConnectionId AcceptConnection()
		{
			if (!m_IncomingConnections.TryDequeue(out var item))
			{
				return default(global::Unity.Networking.Transport.ConnectionId);
			}
			global::Unity.Networking.Transport.NetworkConnection.State connectionState = GetConnectionState(item);
			if (connectionState != global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				global::UnityEngine.Debug.LogWarning($"Attempting to accept a connection ({item}) with state '{connectionState}'");
				return default(global::Unity.Networking.Transport.ConnectionId);
			}
			return item;
		}

		internal bool IsConnectionAccepted(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			if (m_IncomingConnections.Count == 0)
			{
				return true;
			}
			if (global::Unity.Collections.NativeArrayExtensions.Contains(QueryIncomingConnections(global::Unity.Collections.Allocator.Temp), connectionId))
			{
				return false;
			}
			return true;
		}

		internal void StartDisconnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId, global::Unity.Networking.Transport.Error.DisconnectReason reason = global::Unity.Networking.Transport.Error.DisconnectReason.Default)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connectionId];
			if (value.State == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected || value.State == global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
			{
				global::UnityEngine.Debug.LogWarning("Attempting to disconnect an already disconnected connection");
				return;
			}
			m_IncomingDisconnections.Enqueue(new global::Unity.Networking.Transport.ConnectionList.IncomingDisconnection
			{
				Connection = connectionId,
				Reason = reason
			});
			value.State = global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting;
			m_Connections[connectionId] = value;
		}

		internal void FinishDisconnecting(ref global::Unity.Networking.Transport.ConnectionId connectionId)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connectionId];
			if (value.State != global::Unity.Networking.Transport.NetworkConnection.State.Disconnecting)
			{
				global::UnityEngine.Debug.LogWarning($"Attempting to complete a disconnection with state different to Disconnecting ({value.State})");
				return;
			}
			value.State = global::Unity.Networking.Transport.NetworkConnection.State.Disconnected;
			m_Connections[connectionId] = value;
		}

		internal void Cleanup()
		{
			m_FinishedConnections.Clear();
			m_IncomingDisconnections.Clear();
			if (m_FreeList.Count != 0)
			{
				return;
			}
			for (int i = 0; i < Count; i++)
			{
				global::Unity.Networking.Transport.ConnectionId connection = ConnectionAt(i);
				if (m_Connections[connection].State == global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
				{
					connection.Version++;
					m_Connections.ClearData(ref connection);
					m_FreeList.Enqueue(connection);
				}
			}
		}

		internal void UpdateConnectionAddress(ref global::Unity.Networking.Transport.ConnectionId connection, ref global::Unity.Networking.Transport.NetworkEndpoint address)
		{
			global::Unity.Networking.Transport.ConnectionList.ConnectionData value = m_Connections[connection];
			if (value.Endpoint != address)
			{
				value.Endpoint = address;
				m_Connections[connection] = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Networking.Transport.ConnectionList connectionList)
			{
				return this == connectionList;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_Connections.GetHashCode();
		}

		public static bool operator ==(global::Unity.Networking.Transport.ConnectionList a, global::Unity.Networking.Transport.ConnectionList b)
		{
			return a.m_Connections == b.m_Connections;
		}

		public static bool operator !=(global::Unity.Networking.Transport.ConnectionList a, global::Unity.Networking.Transport.ConnectionList b)
		{
			return !(a == b);
		}
	}
}

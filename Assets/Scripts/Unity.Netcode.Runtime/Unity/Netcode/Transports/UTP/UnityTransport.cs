namespace Unity.Netcode.Transports.UTP
{
	[global::UnityEngine.AddComponentMenu("Netcode/Unity Transport")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/api/Unity.Netcode.Transports.UTP.UnityTransport.html")]
	public class UnityTransport : global::Unity.Netcode.NetworkTransport, global::Unity.Netcode.Transports.UTP.INetworkStreamDriverConstructor
	{
		public enum ProtocolType
		{
			UnityTransport = 0,
			RelayUnityTransport = 1
		}

		[global::System.Serializable]
		public struct ConnectionAddressData
		{
			[global::UnityEngine.Tooltip("IP address of the server (address to which clients will connect to).")]
			[global::UnityEngine.SerializeField]
			public string Address;

			[global::UnityEngine.Tooltip("UDP port of the server.")]
			[global::UnityEngine.SerializeField]
			public ushort Port;

			[global::UnityEngine.Tooltip("IP address the server will listen on. If not provided, will use localhost.")]
			[global::UnityEngine.SerializeField]
			public string ServerListenAddress;

			[global::UnityEngine.SerializeField]
			public ushort ClientBindPort;

			[global::System.Obsolete("Use NetworkEndpoint.Parse on the Address field instead.")]
			public global::Unity.Networking.Transport.NetworkEndpoint ServerEndPoint => ParseNetworkEndpoint(Address, Port);

			public global::Unity.Networking.Transport.NetworkEndpoint ListenEndPoint
			{
				get
				{
					global::Unity.Networking.Transport.NetworkEndpoint networkEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
					if (string.IsNullOrEmpty(ServerListenAddress))
					{
						networkEndpoint = (IsIpv6 ? global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv6 : global::Unity.Networking.Transport.NetworkEndpoint.LoopbackIpv4).WithPort(Port);
					}
					else
					{
						networkEndpoint = ParseNetworkEndpoint(ServerListenAddress, Port);
						if (networkEndpoint == default(global::Unity.Networking.Transport.NetworkEndpoint))
						{
							global::UnityEngine.Debug.LogError($"Invalid listen endpoint: {ServerListenAddress}:{Port}. Note that the listen endpoint MUST be an IP address (not a hostname).");
						}
					}
					return networkEndpoint;
				}
			}

			public bool IsIpv6
			{
				get
				{
					global::Unity.Networking.Transport.NetworkEndpoint endpoint;
					if (!string.IsNullOrEmpty(Address))
					{
						return !global::Unity.Networking.Transport.NetworkEndpoint.TryParse(Address, Port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv4);
					}
					return false;
				}
			}

			internal static global::Unity.Networking.Transport.NetworkEndpoint ParseNetworkEndpoint(string ip, ushort port)
			{
				global::Unity.Networking.Transport.NetworkEndpoint endpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				if (!global::Unity.Networking.Transport.NetworkEndpoint.TryParse(ip, port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv4))
				{
					global::Unity.Networking.Transport.NetworkEndpoint.TryParse(ip, port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv6);
				}
				return endpoint;
			}
		}

		[global::System.Serializable]
		public struct SimulatorParameters
		{
			[global::UnityEngine.Tooltip("Delay to add to every send and received packet (in milliseconds). Only applies in the editor and in development builds. The value is ignored in production builds.")]
			[global::UnityEngine.SerializeField]
			public int PacketDelayMS;

			[global::UnityEngine.Tooltip("Jitter (random variation) to add/substract to the packet delay (in milliseconds). Only applies in the editor and in development builds. The value is ignored in production builds.")]
			[global::UnityEngine.SerializeField]
			public int PacketJitterMS;

			[global::UnityEngine.Tooltip("Percentage of sent and received packets to drop. Only applies in the editor and in the editor and in developments builds.")]
			[global::UnityEngine.SerializeField]
			public int PacketDropRate;
		}

		private struct PacketLossCache
		{
			public int PacketsReceived;

			public int PacketsDropped;

			public float PacketLoss;
		}

		[global::Unity.Burst.BurstCompile]
		private struct SendBatchedMessagesJob : global::Unity.Jobs.IJob
		{
			public global::Unity.Networking.Transport.NetworkDriver.Concurrent Driver;

			public global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget Target;

			public global::Unity.Netcode.Transports.UTP.BatchedSendQueue Queue;

			public global::Unity.Networking.Transport.NetworkPipeline ReliablePipeline;

			public int MTU;

			public void Execute()
			{
				ulong clientId = Target.ClientId;
				global::Unity.Networking.Transport.NetworkConnection connection = ParseClientId(clientId);
				global::Unity.Networking.Transport.NetworkPipeline networkPipeline = Target.NetworkPipeline;
				while (!Queue.IsEmpty)
				{
					int num = Driver.BeginSend(networkPipeline, connection, out var writer);
					if (num != 0)
					{
						global::UnityEngine.Debug.LogError($"Send error on connection {clientId}: {(global::Unity.Netcode.Transports.UTP.ErrorUtilities.ErrorToFixedString(num))}");
						break;
					}
					int num2 = ((networkPipeline == ReliablePipeline) ? Queue.FillWriterWithBytes(ref writer, MTU) : Queue.FillWriterWithMessages(ref writer, MTU));
					num = Driver.EndSend(writer);
					if (num == num2)
					{
						Queue.Consume(num2);
						continue;
					}
					if (num != -5)
					{
						global::UnityEngine.Debug.LogError($"Send error on connection {clientId}: {(global::Unity.Netcode.Transports.UTP.ErrorUtilities.ErrorToFixedString(num))}");
						Queue.Consume(num2);
					}
					break;
				}
			}
		}

		private struct SendTarget : global::System.IEquatable<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget>
		{
			public readonly ulong ClientId;

			public readonly global::Unity.Networking.Transport.NetworkPipeline NetworkPipeline;

			public SendTarget(ulong clientId, global::Unity.Networking.Transport.NetworkPipeline networkPipeline)
			{
				ClientId = clientId;
				NetworkPipeline = networkPipeline;
			}

			public bool Equals(global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget other)
			{
				if (ClientId == other.ClientId)
				{
					return NetworkPipeline.Equals(other.NetworkPipeline);
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj is global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget other)
				{
					return Equals(other);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (ClientId.GetHashCode() * 397) ^ NetworkPipeline.GetHashCode();
			}
		}

		public const int InitialMaxPacketQueueSize = 128;

		public const int InitialMaxPayloadSize = 6144;

		[global::System.Obsolete("MaxSendQueueSize is now determined dynamically (can still be set programmatically using the MaxSendQueueSize property). This initial value is not used anymore.", false)]
		public const int InitialMaxSendQueueSize = 98304;

		private const int k_MaxReliableThroughput = 5376;

		private static global::Unity.Netcode.Transports.UTP.UnityTransport.ConnectionAddressData s_DefaultConnectionAddressData = new global::Unity.Netcode.Transports.UTP.UnityTransport.ConnectionAddressData
		{
			Address = "127.0.0.1",
			Port = 7777,
			ServerListenAddress = string.Empty
		};

		public static global::Unity.Netcode.Transports.UTP.INetworkStreamDriverConstructor s_DriverConstructor;

		[global::UnityEngine.Tooltip("Which protocol should be selected (Relay/Non-Relay).")]
		[global::UnityEngine.SerializeField]
		private global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType m_ProtocolType;

		[global::UnityEngine.Tooltip("Per default the client/server will communicate over UDP. Set to true to communicate with WebSocket.")]
		[global::UnityEngine.SerializeField]
		private bool m_UseWebSockets;

		[global::UnityEngine.Tooltip("Per default the client/server communication will not be encrypted. Select true to enable DTLS for UDP and TLS for Websocket.")]
		[global::UnityEngine.SerializeField]
		private bool m_UseEncryption;

		[global::UnityEngine.Tooltip("The maximum amount of packets that can be in the internal send/receive queues. Basically this is how many packets can be sent/received in a single update/frame.")]
		[global::UnityEngine.SerializeField]
		private int m_MaxPacketQueueSize = 128;

		[global::UnityEngine.Tooltip("The maximum size of an unreliable payload that can be handled by the transport.")]
		[global::UnityEngine.SerializeField]
		private int m_MaxPayloadSize = 6144;

		private int m_MaxSendQueueSize;

		[global::UnityEngine.Tooltip("Timeout in milliseconds after which a heartbeat is sent if there is no activity.")]
		[global::UnityEngine.SerializeField]
		private int m_HeartbeatTimeoutMS = 500;

		[global::UnityEngine.Tooltip("Timeout in milliseconds indicating how long we will wait until we send a new connection attempt.")]
		[global::UnityEngine.SerializeField]
		private int m_ConnectTimeoutMS = 1000;

		[global::UnityEngine.Tooltip("The maximum amount of connection attempts we will try before disconnecting.")]
		[global::UnityEngine.SerializeField]
		private int m_MaxConnectAttempts = 60;

		[global::UnityEngine.Tooltip("Inactivity timeout after which a connection will be disconnected. The connection needs to receive data from the connected endpoint within this timeout. Note that with heartbeats enabled, simply not sending any data will not be enough to trigger this timeout (since heartbeats count as connection events).")]
		[global::UnityEngine.SerializeField]
		private int m_DisconnectTimeoutMS = 30000;

		public global::Unity.Netcode.Transports.UTP.UnityTransport.ConnectionAddressData ConnectionData = s_DefaultConnectionAddressData;

		[global::System.Obsolete("DebugSimulator is no longer supported and has no effect. Use Network Simulator from the Multiplayer Tools package.", false)]
		[global::UnityEngine.HideInInspector]
		public global::Unity.Netcode.Transports.UTP.UnityTransport.SimulatorParameters DebugSimulator = new global::Unity.Netcode.Transports.UTP.UnityTransport.SimulatorParameters
		{
			PacketDelayMS = 0,
			PacketJitterMS = 0,
			PacketDropRate = 0
		};

		protected global::Unity.Networking.Transport.NetworkDriver m_Driver;

		private global::Unity.Netcode.Transports.UTP.UnityTransport.PacketLossCache m_PacketLossCache;

		private ulong m_ServerClientId;

		private global::Unity.Networking.Transport.NetworkPipeline m_UnreliableFragmentedPipeline;

		private global::Unity.Networking.Transport.NetworkPipeline m_UnreliableSequencedFragmentedPipeline;

		private global::Unity.Networking.Transport.NetworkPipeline m_ReliableSequencedPipeline;

		private global::Unity.Networking.Transport.Relay.RelayServerData m_RelayServerData;

		protected global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::Unity.Netcode.IRealTimeProvider m_RealTimeProvider;

		private readonly global::System.Collections.Generic.Dictionary<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue> m_SendQueue = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue>();

		private readonly global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.Transports.UTP.BatchedReceiveQueue> m_ReliableReceiveQueues = new global::System.Collections.Generic.Dictionary<ulong, global::Unity.Netcode.Transports.UTP.BatchedReceiveQueue>();

		private const string k_OverridePortArg = "-port";

		private const string k_OverrideIpAddressArg = "-ip";

		private global::Unity.Netcode.Transports.UTP.UnityTransportNotificationHandler m_UnityTransportNotificationHandler;

		private bool m_HasForcedConnectionData;

		private string m_ServerPrivateKey;

		private string m_ServerCertificate;

		private string m_ServerCommonName;

		private string m_ClientCaCertificate;

		public global::Unity.Netcode.Transports.UTP.INetworkStreamDriverConstructor DriverConstructor => s_DriverConstructor ?? this;

		public bool UseWebSockets
		{
			get
			{
				return m_UseWebSockets;
			}
			set
			{
				m_UseWebSockets = value;
			}
		}

		public bool UseEncryption
		{
			get
			{
				return m_UseEncryption;
			}
			set
			{
				m_UseEncryption = value;
			}
		}

		public int MaxPacketQueueSize
		{
			get
			{
				return m_MaxPacketQueueSize;
			}
			set
			{
				m_MaxPacketQueueSize = value;
			}
		}

		public int MaxPayloadSize
		{
			get
			{
				return m_MaxPayloadSize;
			}
			set
			{
				m_MaxPayloadSize = value;
			}
		}

		public int MaxSendQueueSize
		{
			get
			{
				return m_MaxSendQueueSize;
			}
			set
			{
				m_MaxSendQueueSize = value;
			}
		}

		public int HeartbeatTimeoutMS
		{
			get
			{
				return m_HeartbeatTimeoutMS;
			}
			set
			{
				m_HeartbeatTimeoutMS = value;
			}
		}

		public int ConnectTimeoutMS
		{
			get
			{
				return m_ConnectTimeoutMS;
			}
			set
			{
				m_ConnectTimeoutMS = value;
			}
		}

		public int MaxConnectAttempts
		{
			get
			{
				return m_MaxConnectAttempts;
			}
			set
			{
				m_MaxConnectAttempts = value;
			}
		}

		public int DisconnectTimeoutMS
		{
			get
			{
				return m_DisconnectTimeoutMS;
			}
			set
			{
				m_DisconnectTimeoutMS = value;
			}
		}

		internal uint? DebugSimulatorRandomSeed { get; set; }

		public override ulong ServerClientId => m_ServerClientId;

		public global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType Protocol => m_ProtocolType;

		internal static event global::System.Action<global::UnityEngine.EntityId, global::Unity.Networking.Transport.NetworkDriver> OnDriverInitialized;

		internal static event global::System.Action<global::UnityEngine.EntityId> OnDisposingDriver;

		internal static event global::System.Action<int, global::Unity.Networking.Transport.NetworkDriver> TransportInitialized;

		internal static event global::System.Action<int> TransportDisposed;

		public ref global::Unity.Networking.Transport.NetworkDriver GetNetworkDriver()
		{
			return ref m_Driver;
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetLocalEndpoint()
		{
			if (m_Driver.IsCreated)
			{
				return m_Driver.GetLocalEndpoint();
			}
			return default(global::Unity.Networking.Transport.NetworkEndpoint);
		}

		private void InitDriver()
		{
			DriverConstructor.CreateDriver(this, out m_Driver, out m_UnreliableFragmentedPipeline, out m_UnreliableSequencedFragmentedPipeline, out m_ReliableSequencedPipeline);
			global::UnityEngine.EntityId entityId = GetEntityId();
			global::Unity.Netcode.Transports.UTP.UnityTransport.OnDriverInitialized?.Invoke(entityId, m_Driver);
			global::Unity.Netcode.Transports.UTP.UnityTransport.TransportInitialized?.Invoke(entityId.GetHashCode(), m_Driver);
		}

		private void DisposeInternals()
		{
			if (m_Driver.IsCreated)
			{
				m_Driver.Dispose();
			}
			foreach (global::Unity.Netcode.Transports.UTP.BatchedSendQueue value in m_SendQueue.Values)
			{
				value.Dispose();
			}
			m_SendQueue.Clear();
			global::UnityEngine.EntityId entityId = GetEntityId();
			global::Unity.Netcode.Transports.UTP.UnityTransport.OnDisposingDriver?.Invoke(entityId);
			global::Unity.Netcode.Transports.UTP.UnityTransport.TransportDisposed?.Invoke(entityId.GetHashCode());
		}

		public global::Unity.Networking.Transport.NetworkSettings GetDefaultNetworkSettings()
		{
			global::Unity.Networking.Transport.NetworkSettings settings = default(global::Unity.Networking.Transport.NetworkSettings);
			int maxConnectAttempts = m_MaxConnectAttempts;
			global::Unity.Networking.Transport.CommonNetworkParametersExtensions.WithNetworkConfigParameters(connectTimeoutMS: m_ConnectTimeoutMS, maxConnectAttempts: maxConnectAttempts, disconnectTimeoutMS: m_DisconnectTimeoutMS, sendQueueCapacity: m_MaxPacketQueueSize, receiveQueueCapacity: m_MaxPacketQueueSize, settings: ref settings, heartbeatTimeoutMS: m_HeartbeatTimeoutMS);
			if (m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport)
			{
				if (m_RelayServerData.Equals(default(global::Unity.Networking.Transport.Relay.RelayServerData)))
				{
					throw new global::System.Exception("You must call SetRelayServerData() before calling StartClient() or StartServer().");
				}
				global::Unity.Networking.Transport.Relay.RelayParameterExtensions.WithRelayParameters(ref settings, ref m_RelayServerData, m_HeartbeatTimeoutMS);
			}
			int payloadCapacity = m_MaxPayloadSize + 4;
			global::Unity.Networking.Transport.Utilities.FragmentationStageParameterExtensions.WithFragmentationStageParameters(ref settings, payloadCapacity);
			int maximumResendTime = ((m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport) ? 750 : 500);
			global::Unity.Networking.Transport.Utilities.ReliableStageParameterExtensions.WithReliableStageParameters(ref settings, 64, 64, maximumResendTime);
			if (m_UseEncryption && m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.UnityTransport)
			{
				if (m_NetworkManager.IsServer)
				{
					if (string.IsNullOrEmpty(m_ServerCertificate) || string.IsNullOrEmpty(m_ServerPrivateKey))
					{
						throw new global::System.Exception("In order to use encryption, you must call SetServerSecrets() before calling StartServer().");
					}
					global::Unity.Networking.Transport.TLS.SecureParameterExtensions.WithSecureServerParameters(ref settings, m_ServerCertificate, m_ServerPrivateKey);
				}
				else
				{
					if (string.IsNullOrEmpty(m_ServerCommonName))
					{
						throw new global::System.Exception("In order to use encryption, you must call SetClientSecrets() before calling StartClient().");
					}
					if (string.IsNullOrEmpty(m_ClientCaCertificate))
					{
						global::Unity.Networking.Transport.TLS.SecureParameterExtensions.WithSecureClientParameters(ref settings, m_ServerCommonName);
					}
					else
					{
						global::Unity.Networking.Transport.TLS.SecureParameterExtensions.WithSecureClientParameters(ref settings, m_ClientCaCertificate, m_ServerCommonName);
					}
				}
			}
			return settings;
		}

		public void GetDefaultPipelineConfigurations(out global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> unreliableFragmentedPipelineStages, out global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> unreliableSequencedFragmentedPipelineStages, out global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId> reliableSequencedPipelineStages)
		{
			global::Unity.Networking.Transport.NetworkPipelineStageId[] array = new global::Unity.Networking.Transport.NetworkPipelineStageId[2]
			{
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.FragmentationPipelineStage>(),
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Netcode.Transports.UTP.NetworkMetricsPipelineStage>()
			};
			global::Unity.Networking.Transport.NetworkPipelineStageId[] array2 = new global::Unity.Networking.Transport.NetworkPipelineStageId[3]
			{
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.FragmentationPipelineStage>(),
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.UnreliableSequencedPipelineStage>(),
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Netcode.Transports.UTP.NetworkMetricsPipelineStage>()
			};
			global::Unity.Networking.Transport.NetworkPipelineStageId[] array3 = new global::Unity.Networking.Transport.NetworkPipelineStageId[2]
			{
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>(),
				global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Netcode.Transports.UTP.NetworkMetricsPipelineStage>()
			};
			unreliableFragmentedPipelineStages = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId>(array, global::Unity.Collections.Allocator.Temp);
			unreliableSequencedFragmentedPipelineStages = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId>(array2, global::Unity.Collections.Allocator.Temp);
			reliableSequencedPipelineStages = new global::Unity.Collections.NativeArray<global::Unity.Networking.Transport.NetworkPipelineStageId>(array3, global::Unity.Collections.Allocator.Temp);
		}

		private global::Unity.Networking.Transport.NetworkPipeline SelectSendPipeline(global::Unity.Netcode.NetworkDelivery delivery)
		{
			switch (delivery)
			{
			case global::Unity.Netcode.NetworkDelivery.Unreliable:
				return m_UnreliableFragmentedPipeline;
			case global::Unity.Netcode.NetworkDelivery.UnreliableSequenced:
				return m_UnreliableSequencedFragmentedPipeline;
			case global::Unity.Netcode.NetworkDelivery.Reliable:
			case global::Unity.Netcode.NetworkDelivery.ReliableSequenced:
			case global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced:
				return m_ReliableSequencedPipeline;
			default:
				global::UnityEngine.Debug.LogError(string.Format("Unknown {0} value: {1}", "NetworkDelivery", delivery));
				return global::Unity.Networking.Transport.NetworkPipeline.Null;
			}
		}

		private bool ClientBindAndConnect()
		{
			global::Unity.Networking.Transport.NetworkEndpoint networkEndpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
			if (m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport)
			{
				networkEndpoint = m_RelayServerData.Endpoint;
			}
			else
			{
				networkEndpoint = global::Unity.Netcode.Transports.UTP.UnityTransport.ConnectionAddressData.ParseNetworkEndpoint(ConnectionData.Address, ConnectionData.Port);
				if (networkEndpoint.Family == global::Unity.Networking.Transport.NetworkFamily.Invalid && global::System.Uri.CheckHostName(ConnectionData.Address) != global::System.UriHostNameType.Dns)
				{
					global::UnityEngine.Debug.LogError("Provided connection address \"" + ConnectionData.Address + "\" is not a valid hostname.");
					return false;
				}
			}
			InitDriver();
			if (networkEndpoint.Family != global::Unity.Networking.Transport.NetworkFamily.Invalid && ConnectionData.ClientBindPort != 0)
			{
				global::Unity.Networking.Transport.NetworkEndpoint endpoint = ((networkEndpoint.Family == global::Unity.Networking.Transport.NetworkFamily.Ipv6) ? global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv6.WithPort(ConnectionData.ClientBindPort) : global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv4.WithPort(ConnectionData.ClientBindPort));
				if (m_Driver.Bind(endpoint) != 0)
				{
					global::UnityEngine.Debug.LogError($"Couldn't create socket. Possibly another process is using port {ConnectionData.ClientBindPort}.");
					return false;
				}
			}
			Connect(networkEndpoint);
			return true;
		}

		protected virtual global::Unity.Networking.Transport.NetworkConnection Connect(global::Unity.Networking.Transport.NetworkEndpoint serverEndpoint)
		{
			if (serverEndpoint.Family == global::Unity.Networking.Transport.NetworkFamily.Invalid)
			{
				return m_Driver.Connect(ConnectionData.Address, ConnectionData.Port);
			}
			return m_Driver.Connect(serverEndpoint);
		}

		private bool ServerBindAndListen(global::Unity.Networking.Transport.NetworkEndpoint endPoint)
		{
			if (endPoint.Family == global::Unity.Networking.Transport.NetworkFamily.Invalid)
			{
				return false;
			}
			InitDriver();
			if (m_Driver.Bind(endPoint) != 0)
			{
				global::UnityEngine.Debug.LogError("Server failed to bind. This is usually caused by another process being bound to the same port.");
				return false;
			}
			if (m_Driver.Listen() != 0)
			{
				global::UnityEngine.Debug.LogError("Server failed to listen.");
				return false;
			}
			return true;
		}

		private void SetProtocol(global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType inProtocol)
		{
			m_ProtocolType = inProtocol;
		}

		public void SetRelayServerData(string ipv4Address, ushort port, byte[] allocationIdBytes, byte[] keyBytes, byte[] connectionDataBytes, byte[] hostConnectionDataBytes = null, bool isSecure = false)
		{
			byte[] hostConnectionData = hostConnectionDataBytes ?? connectionDataBytes;
			m_RelayServerData = new global::Unity.Networking.Transport.Relay.RelayServerData(ipv4Address, port, allocationIdBytes, connectionDataBytes, hostConnectionData, keyBytes, isSecure);
			SetProtocol(global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport);
		}

		public void SetRelayServerData(global::Unity.Networking.Transport.Relay.RelayServerData serverData)
		{
			m_RelayServerData = serverData;
			SetProtocol(global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport);
		}

		public void SetHostRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData, bool isSecure = false)
		{
			SetRelayServerData(ipAddress, port, allocationId, key, connectionData, null, isSecure);
		}

		public void SetClientRelayData(string ipAddress, ushort port, byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, bool isSecure = false)
		{
			SetRelayServerData(ipAddress, port, allocationId, key, connectionData, hostConnectionData, isSecure);
		}

		private bool ParseCommandLineOptionsPort(out ushort port)
		{
			string arg = global::Unity.Netcode.CommandLineOptions.Instance.GetArg("-port");
			if (arg != null)
			{
				port = (ushort)global::System.Convert.ChangeType(arg, typeof(ushort));
				return true;
			}
			port = 0;
			return false;
		}

		private bool ParseCommandLineOptionsAddress(out string ipValue)
		{
			string arg = global::Unity.Netcode.CommandLineOptions.Instance.GetArg("-ip");
			if (arg != null)
			{
				ipValue = arg;
				return true;
			}
			ipValue = null;
			return false;
		}

		public void SetConnectionData(string ipv4Address, ushort port, string listenAddress = null)
		{
			SetConnectionData(forceOverrideCommandLineArgs: false, ipv4Address, port, listenAddress);
		}

		public void SetConnectionData(bool forceOverrideCommandLineArgs, string ipv4Address, ushort port, string listenAddress = null)
		{
			m_HasForcedConnectionData = forceOverrideCommandLineArgs;
			if (!forceOverrideCommandLineArgs && ParseCommandLineOptionsPort(out var port2))
			{
				port = port2;
			}
			if (!forceOverrideCommandLineArgs && ParseCommandLineOptionsAddress(out var ipValue))
			{
				ipv4Address = ipValue;
			}
			ConnectionData = new global::Unity.Netcode.Transports.UTP.UnityTransport.ConnectionAddressData
			{
				Address = ipv4Address,
				Port = port,
				ServerListenAddress = (listenAddress ?? ipv4Address),
				ClientBindPort = ConnectionData.ClientBindPort
			};
			SetProtocol(global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.UnityTransport);
		}

		public void SetConnectionData(global::Unity.Networking.Transport.NetworkEndpoint endPoint, global::Unity.Networking.Transport.NetworkEndpoint listenEndPoint = default(global::Unity.Networking.Transport.NetworkEndpoint))
		{
			string ipv4Address = endPoint.Address.Split(':')[0];
			string listenAddress = string.Empty;
			if (listenEndPoint != default(global::Unity.Networking.Transport.NetworkEndpoint))
			{
				listenAddress = listenEndPoint.Address.Split(':')[0];
				if (endPoint.Port != listenEndPoint.Port)
				{
					global::UnityEngine.Debug.LogError($"Port mismatch between server and listen endpoints ({endPoint.Port} vs {listenEndPoint.Port}).");
				}
			}
			SetConnectionData(ipv4Address, endPoint.Port, listenAddress);
		}

		[global::System.Obsolete("SetDebugSimulatorParameters is no longer supported and has no effect. Use Network Simulator from the Multiplayer Tools package.", false)]
		public void SetDebugSimulatorParameters(int packetDelay, int packetJitter, int dropRate)
		{
			if (m_Driver.IsCreated)
			{
				global::UnityEngine.Debug.LogError("SetDebugSimulatorParameters() must be called before StartClient() or StartServer().");
				return;
			}
			DebugSimulator = new global::Unity.Netcode.Transports.UTP.UnityTransport.SimulatorParameters
			{
				PacketDelayMS = packetDelay,
				PacketJitterMS = packetJitter,
				PacketDropRate = dropRate
			};
		}

		private void SendBatchedMessages(global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget sendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue queue)
		{
			if (!m_Driver.IsCreated)
			{
				return;
			}
			int mTU = 0;
			if ((bool)m_NetworkManager)
			{
				(ulong, bool) tuple = m_NetworkManager.ConnectionManager.TransportIdToClientId(sendTarget.ClientId);
				var (clientId, _) = tuple;
				if (!tuple.Item2)
				{
					return;
				}
				mTU = m_NetworkManager.GetPeerMTU(clientId);
			}
			global::Unity.Jobs.IJobExtensions.Run(new global::Unity.Netcode.Transports.UTP.UnityTransport.SendBatchedMessagesJob
			{
				Driver = m_Driver.ToConcurrent(),
				Target = sendTarget,
				Queue = queue,
				ReliablePipeline = m_ReliableSequencedPipeline,
				MTU = mTU
			});
		}

		private bool AcceptConnection()
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = m_Driver.Accept();
			if (networkConnection == default(global::Unity.Networking.Transport.NetworkConnection))
			{
				return false;
			}
			InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Connect, ParseClientId(networkConnection), default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
			return true;
		}

		private void ReceiveMessages(ulong clientId, global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Collections.DataStreamReader dataReader)
		{
			global::Unity.Netcode.Transports.UTP.BatchedReceiveQueue value;
			if (pipeline == m_ReliableSequencedPipeline)
			{
				if (m_ReliableReceiveQueues.TryGetValue(clientId, out value))
				{
					value.PushReader(dataReader);
				}
				else
				{
					value = new global::Unity.Netcode.Transports.UTP.BatchedReceiveQueue(dataReader);
					m_ReliableReceiveQueues[clientId] = value;
				}
			}
			else
			{
				value = new global::Unity.Netcode.Transports.UTP.BatchedReceiveQueue(dataReader);
			}
			while (!value.IsEmpty)
			{
				global::System.ArraySegment<byte> arraySegment = value.PopMessage();
				if (!(arraySegment == default(global::System.ArraySegment<byte>)))
				{
					InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Data, clientId, arraySegment, m_RealTimeProvider.RealTimeSinceStartup);
					continue;
				}
				break;
			}
		}

		private bool ProcessEvent()
		{
			global::Unity.Networking.Transport.NetworkConnection connection;
			global::Unity.Collections.DataStreamReader reader;
			global::Unity.Networking.Transport.NetworkPipeline pipe;
			global::Unity.Networking.Transport.NetworkEvent.Type type = m_Driver.PopEvent(out connection, out reader, out pipe);
			ulong num = ParseClientId(connection);
			switch (type)
			{
			case global::Unity.Networking.Transport.NetworkEvent.Type.Connect:
				InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Connect, num, default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
				m_ServerClientId = num;
				return true;
			case global::Unity.Networking.Transport.NetworkEvent.Type.Disconnect:
			{
				if (!m_Driver.Listening && m_ServerClientId == 0L)
				{
					global::UnityEngine.Debug.LogError("Failed to connect to server.");
				}
				global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent = m_UnityTransportNotificationHandler.GetDisconnectEvent(reader.ReadByte());
				SetDisconnectEvent(disconnectEvent);
				m_ServerClientId = 0uL;
				m_ReliableReceiveQueues.Remove(num);
				ClearSendQueuesForClientId(num);
				InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Disconnect, num, default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
				return true;
			}
			case global::Unity.Networking.Transport.NetworkEvent.Type.Data:
				ReceiveMessages(num, pipe, reader);
				return true;
			default:
				return false;
			}
		}

		protected override void OnEarlyUpdate()
		{
			if (m_Driver.IsCreated)
			{
				if (m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport && global::Unity.Networking.Transport.Relay.NetworkDriverRelayExtensions.GetRelayConnectionStatus(m_Driver) == global::Unity.Networking.Transport.Relay.RelayConnectionStatus.AllocationInvalid)
				{
					global::UnityEngine.Debug.LogError("Transport failure! Relay allocation needs to be recreated, and NetworkManager restarted. Use NetworkManager.OnTransportFailure to be notified of such events programmatically.");
					InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.TransportFailure, 0uL, default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
					return;
				}
				m_Driver.ScheduleUpdate().Complete();
				while (AcceptConnection() && m_Driver.IsCreated)
				{
				}
				while (ProcessEvent() && m_Driver.IsCreated)
				{
				}
			}
			base.OnEarlyUpdate();
		}

		protected override void OnPostLateUpdate()
		{
			if (m_Driver.IsCreated)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue> item in m_SendQueue)
				{
					SendBatchedMessages(item.Key, item.Value);
				}
				m_Driver.ScheduleFlushSend().Complete();
				if ((bool)m_NetworkManager)
				{
					ExtractNetworkMetrics();
				}
			}
			base.OnPostLateUpdate();
		}

		private void OnDestroy()
		{
			DisposeInternals();
		}

		private void ExtractNetworkMetrics()
		{
			if (m_NetworkManager.IsServer)
			{
				for (int i = 0; i < m_NetworkManager.ConnectedClientsIds.Count; i++)
				{
					ulong num = m_NetworkManager.ConnectedClientsIds[i];
					if (num != 0L || !m_NetworkManager.IsHost)
					{
						(ulong, bool) tuple = m_NetworkManager.ConnectionManager.ClientIdToTransportId(num);
						if (tuple.Item2)
						{
							ExtractNetworkMetricsForClient(tuple.Item1);
						}
					}
				}
			}
			else if (m_ServerClientId != 0L)
			{
				ExtractNetworkMetricsForClient(m_ServerClientId);
			}
		}

		private void ExtractNetworkMetricsForClient(ulong transportClientId)
		{
			global::Unity.Networking.Transport.NetworkConnection networkConnection = ParseClientId(transportClientId);
			ExtractNetworkMetricsFromPipeline(m_UnreliableFragmentedPipeline, networkConnection);
			ExtractNetworkMetricsFromPipeline(m_UnreliableSequencedFragmentedPipeline, networkConnection);
			ExtractNetworkMetricsFromPipeline(m_ReliableSequencedPipeline, networkConnection);
			int rtt = ((!m_NetworkManager.IsServer) ? ExtractRtt(networkConnection) : 0);
			NetworkMetrics.UpdateRttToServer(rtt);
			float packetLoss = (m_NetworkManager.IsServer ? 0f : ExtractPacketLoss(networkConnection));
			NetworkMetrics.UpdatePacketLoss(packetLoss);
		}

		private unsafe void ExtractNetworkMetricsFromPipeline(global::Unity.Networking.Transport.NetworkPipeline pipeline, global::Unity.Networking.Transport.NetworkConnection networkConnection)
		{
			if (m_Driver.GetConnectionState(networkConnection) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				m_Driver.GetPipelineBuffers(pipeline, global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Netcode.Transports.UTP.NetworkMetricsPipelineStage>(), networkConnection, out var _, out var _, out var sharedBuffer);
				global::Unity.Netcode.Transports.UTP.NetworkMetricsContext* unsafePtr = (global::Unity.Netcode.Transports.UTP.NetworkMetricsContext*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedBuffer);
				NetworkMetrics.TrackPacketSent(unsafePtr->PacketSentCount);
				NetworkMetrics.TrackPacketReceived(unsafePtr->PacketReceivedCount);
				unsafePtr->PacketSentCount = 0u;
				unsafePtr->PacketReceivedCount = 0u;
			}
		}

		private unsafe int ExtractRtt(global::Unity.Networking.Transport.NetworkConnection networkConnection)
		{
			if (m_Driver.GetConnectionState(networkConnection) != global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				return 0;
			}
			m_Driver.GetPipelineBuffers(m_ReliableSequencedPipeline, global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>(), networkConnection, out var _, out var _, out var sharedBuffer);
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* unsafePtr = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedBuffer);
			return unsafePtr->RttInfo.LastRtt;
		}

		private unsafe float ExtractPacketLoss(global::Unity.Networking.Transport.NetworkConnection networkConnection)
		{
			if (m_Driver.GetConnectionState(networkConnection) != global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				return 0f;
			}
			m_Driver.GetPipelineBuffers(m_ReliableSequencedPipeline, global::Unity.Networking.Transport.NetworkPipelineStageId.Get<global::Unity.Networking.Transport.ReliableSequencedPipelineStage>(), networkConnection, out var _, out var _, out var sharedBuffer);
			global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext* unsafePtr = (global::Unity.Networking.Transport.Utilities.ReliableUtility.SharedContext*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(sharedBuffer);
			float num = unsafePtr->stats.PacketsReceived - m_PacketLossCache.PacketsReceived;
			float num2 = unsafePtr->stats.PacketsDropped - m_PacketLossCache.PacketsDropped;
			if (num2 == 0f && num == 0f)
			{
				return m_PacketLossCache.PacketLoss;
			}
			m_PacketLossCache.PacketsReceived = unsafePtr->stats.PacketsReceived;
			m_PacketLossCache.PacketsDropped = unsafePtr->stats.PacketsDropped;
			m_PacketLossCache.PacketLoss = ((num > 0f) ? (num2 / num) : 0f);
			return m_PacketLossCache.PacketLoss;
		}

		private unsafe static ulong ParseClientId(global::Unity.Networking.Transport.NetworkConnection utpConnectionId)
		{
			return *(ulong*)(&utpConnectionId);
		}

		private unsafe static global::Unity.Networking.Transport.NetworkConnection ParseClientId(ulong netcodeConnectionId)
		{
			return *(global::Unity.Networking.Transport.NetworkConnection*)(&netcodeConnectionId);
		}

		private void ClearSendQueuesForClientId(ulong clientId)
		{
			using global::Unity.Collections.NativeList<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget> nativeList = new global::Unity.Collections.NativeList<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget>(16, global::Unity.Collections.Allocator.Temp);
			foreach (global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget key in m_SendQueue.Keys)
			{
				global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget value = key;
				if (value.ClientId == clientId)
				{
					nativeList.Add(in value);
				}
			}
			foreach (global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget item in nativeList)
			{
				m_SendQueue[item].Dispose();
				m_SendQueue.Remove(item);
			}
		}

		private void FlushSendQueuesForClientId(ulong clientId)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue> item in m_SendQueue)
			{
				if (item.Key.ClientId == clientId)
				{
					SendBatchedMessages(item.Key, item.Value);
				}
			}
		}

		public override void DisconnectLocalClient()
		{
			if (m_ServerClientId != 0L)
			{
				FlushSendQueuesForClientId(m_ServerClientId);
				if (m_Driver.Disconnect(ParseClientId(m_ServerClientId)) == 0)
				{
					m_ServerClientId = 0uL;
					m_ReliableReceiveQueues.Remove(m_ServerClientId);
					ClearSendQueuesForClientId(m_ServerClientId);
					InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Disconnect, m_ServerClientId, default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
				}
			}
		}

		public override void DisconnectRemoteClient(ulong clientId)
		{
			if ((!m_NetworkManager || m_NetworkManager.IsServer) && m_Driver.IsCreated)
			{
				FlushSendQueuesForClientId(clientId);
				m_ReliableReceiveQueues.Remove(clientId);
				ClearSendQueuesForClientId(clientId);
				global::Unity.Networking.Transport.NetworkConnection connection = ParseClientId(clientId);
				if (m_Driver.GetConnectionState(connection) != global::Unity.Networking.Transport.NetworkConnection.State.Disconnected)
				{
					m_Driver.Disconnect(connection);
				}
			}
		}

		public override ulong GetCurrentRtt(ulong clientId)
		{
			if (m_NetworkManager != null)
			{
				ulong item = m_NetworkManager.ConnectionManager.ClientIdToTransportId(clientId).Item1;
				int num = ExtractRtt(ParseClientId(item));
				if (num > 0)
				{
					return (ulong)num;
				}
			}
			return (ulong)ExtractRtt(ParseClientId(clientId));
		}

		public global::Unity.Networking.Transport.NetworkEndpoint GetEndpoint(ulong clientId)
		{
			if (m_Driver.IsCreated && m_NetworkManager != null && m_NetworkManager.IsListening)
			{
				(ulong, bool) tuple = m_NetworkManager.ConnectionManager.ClientIdToTransportId(clientId);
				ulong item = tuple.Item1;
				bool item2 = tuple.Item2;
				global::Unity.Networking.Transport.NetworkConnection connection = ParseClientId(item);
				if (item2 && m_Driver.GetConnectionState(connection) == global::Unity.Networking.Transport.NetworkConnection.State.Connected)
				{
					return m_Driver.GetRemoteEndpoint(connection);
				}
			}
			return default(global::Unity.Networking.Transport.NetworkEndpoint);
		}

		public override global::Unity.Netcode.NetworkEvent PollEvent(out ulong clientId, out global::System.ArraySegment<byte> payload, out float receiveTime)
		{
			clientId = 0uL;
			payload = default(global::System.ArraySegment<byte>);
			receiveTime = 0f;
			return global::Unity.Netcode.NetworkEvent.Nothing;
		}

		public override void Send(ulong clientId, global::System.ArraySegment<byte> payload, global::Unity.Netcode.NetworkDelivery networkDelivery)
		{
			global::Unity.Networking.Transport.NetworkConnection connection = ParseClientId(clientId);
			if (!m_Driver.IsCreated || m_Driver.GetConnectionState(connection) != global::Unity.Networking.Transport.NetworkConnection.State.Connected)
			{
				return;
			}
			global::Unity.Networking.Transport.NetworkPipeline networkPipeline = SelectSendPipeline(networkDelivery);
			if (networkPipeline != m_ReliableSequencedPipeline && payload.Count > m_MaxPayloadSize)
			{
				global::UnityEngine.Debug.LogError($"Unreliable payload of size {payload.Count} larger than configured 'Max Payload Size' ({m_MaxPayloadSize}).");
				return;
			}
			global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget sendTarget = new global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget(clientId, networkPipeline);
			if (!m_SendQueue.TryGetValue(sendTarget, out var value))
			{
				int num = m_MaxSendQueueSize;
				if (num <= 0)
				{
					num = (int)((m_DisconnectTimeoutMS != 0) ? global::System.Math.Min(global::System.Math.BigMul(m_DisconnectTimeoutMS, 5376), 2147483646L) : 2147483646);
				}
				value = new global::Unity.Netcode.Transports.UTP.BatchedSendQueue(global::System.Math.Max(num, m_MaxPayloadSize));
				m_SendQueue.Add(sendTarget, value);
			}
			if (value.PushMessage(payload))
			{
				return;
			}
			if (networkPipeline == m_ReliableSequencedPipeline)
			{
				if (m_NetworkManager != null)
				{
					(ulong, bool) tuple = m_NetworkManager.ConnectionManager.TransportIdToClientId(clientId);
					var (num2, _) = tuple;
					if (tuple.Item2)
					{
						clientId = num2;
					}
				}
				global::UnityEngine.Debug.LogError($"Couldn't add payload of size {payload.Count} to reliable send queue. " + $"Closing connection {clientId} as reliability guarantees can't be maintained.");
				if (clientId == m_ServerClientId)
				{
					DisconnectLocalClient();
					return;
				}
				DisconnectRemoteClient(clientId);
				InvokeOnTransportEvent(global::Unity.Netcode.NetworkEvent.Disconnect, clientId, default(global::System.ArraySegment<byte>), m_RealTimeProvider.RealTimeSinceStartup);
			}
			else
			{
				m_Driver.ScheduleFlushSend().Complete();
				SendBatchedMessages(sendTarget, value);
				value.PushMessage(payload);
			}
		}

		public override bool StartClient()
		{
			if (m_Driver.IsCreated)
			{
				return false;
			}
			bool num = ClientBindAndConnect();
			if (!num && m_Driver.IsCreated)
			{
				m_Driver.Dispose();
			}
			return num;
		}

		public override bool StartServer()
		{
			if (m_Driver.IsCreated)
			{
				return false;
			}
			global::Unity.Networking.Transport.NetworkEndpoint endPoint = ((m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.UnityTransport) ? ConnectionData.ListenEndPoint : global::Unity.Networking.Transport.NetworkEndpoint.AnyIpv4);
			bool num = ServerBindAndListen(endPoint);
			if (!num && m_Driver.IsCreated)
			{
				m_Driver.Dispose();
			}
			return num;
		}

		protected override string GetDisconnectEventMessage(global::Unity.Netcode.NetworkTransport.DisconnectEvents disconnectEvent)
		{
			return m_UnityTransportNotificationHandler.GetDisconnectEventMessage(disconnectEvent);
		}

		public override void Initialize(global::Unity.Netcode.NetworkManager networkManager = null)
		{
			m_NetworkManager = networkManager;
			if (!m_HasForcedConnectionData && ParseCommandLineOptionsAddress(out var ipValue))
			{
				global::Unity.Netcode.NetworkManager networkManager2 = m_NetworkManager;
				if ((object)networkManager2 != null && networkManager2.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::UnityEngine.Debug.Log("The port is set by a command line option. Using following connection data: " + ConnectionData.Address + ":" + ipValue);
				}
				if (ushort.TryParse(ipValue, out var result))
				{
					ConnectionData.Port = result;
				}
				else
				{
					global::UnityEngine.Debug.LogError("The port (" + ipValue + ") is not a valid unsigned short value!");
				}
			}
			global::Unity.Netcode.IRealTimeProvider realTimeProvider2;
			if (!m_NetworkManager)
			{
				global::Unity.Netcode.IRealTimeProvider realTimeProvider = new global::Unity.Netcode.RealTimeProvider();
				realTimeProvider2 = realTimeProvider;
			}
			else
			{
				realTimeProvider2 = m_NetworkManager.RealTimeProvider;
			}
			m_RealTimeProvider = realTimeProvider2;
			m_UnityTransportNotificationHandler = new global::Unity.Netcode.Transports.UTP.UnityTransportNotificationHandler();
		}

		public override void Shutdown()
		{
			if ((bool)m_NetworkManager && !m_NetworkManager.ShutdownInProgress)
			{
				global::UnityEngine.Debug.LogWarning("Directly calling `UnityTransport.Shutdown()` results in unexpected shutdown behaviour. All pending events will be lost. Use `NetworkManager.Shutdown()` instead.");
			}
			if (m_Driver.IsCreated)
			{
				while (ProcessEvent() && m_Driver.IsCreated)
				{
				}
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.Transports.UTP.UnityTransport.SendTarget, global::Unity.Netcode.Transports.UTP.BatchedSendQueue> item in m_SendQueue)
				{
					SendBatchedMessages(item.Key, item.Value);
				}
				m_Driver.ScheduleUpdate().Complete();
			}
			DisposeInternals();
			m_ReliableReceiveQueues.Clear();
			m_ServerClientId = 0uL;
			m_UnityTransportNotificationHandler = null;
		}

		protected override global::Unity.Netcode.NetworkTopologyTypes OnCurrentTopology()
		{
			if (!(m_NetworkManager != null))
			{
				return global::Unity.Netcode.NetworkTopologyTypes.ClientServer;
			}
			return m_NetworkManager.NetworkConfig.NetworkTopology;
		}

		public void SetServerSecrets(string serverCertificate, string serverPrivateKey)
		{
			m_ServerPrivateKey = serverPrivateKey;
			m_ServerCertificate = serverCertificate;
		}

		public void SetClientSecrets(string serverCommonName, string caCertificate = null)
		{
			m_ServerCommonName = serverCommonName;
			m_ClientCaCertificate = caCertificate;
		}

		public void CreateDriver(global::Unity.Netcode.Transports.UTP.UnityTransport transport, out global::Unity.Networking.Transport.NetworkDriver driver, out global::Unity.Networking.Transport.NetworkPipeline unreliableFragmentedPipeline, out global::Unity.Networking.Transport.NetworkPipeline unreliableSequencedFragmentedPipeline, out global::Unity.Networking.Transport.NetworkPipeline reliableSequencedPipeline)
		{
			if (m_ProtocolType == global::Unity.Netcode.Transports.UTP.UnityTransport.ProtocolType.RelayUnityTransport)
			{
				if (m_UseWebSockets && m_RelayServerData.IsWebSocket == 0)
				{
					global::UnityEngine.Debug.LogError("Transport is configured to use WebSockets, but Relay server data isn't. Be sure to use \"wss\" as the connection type when creating the server data (instead of \"dtls\" or \"udp\").");
				}
				if (!m_UseWebSockets && m_RelayServerData.IsWebSocket != 0)
				{
					global::UnityEngine.Debug.LogError("Relay server data indicates usage of WebSockets, but \"Use WebSockets\" checkbox isn't checked under \"Unity Transport\" component.");
				}
			}
			if (m_UseWebSockets)
			{
				driver = global::Unity.Networking.Transport.NetworkDriver.Create(default(global::Unity.Networking.Transport.WebSocketNetworkInterface), GetDefaultNetworkSettings());
			}
			else
			{
				driver = global::Unity.Networking.Transport.NetworkDriver.Create(default(global::Unity.Networking.Transport.UDPNetworkInterface), GetDefaultNetworkSettings());
			}
			driver.RegisterPipelineStage(default(global::Unity.Netcode.Transports.UTP.NetworkMetricsPipelineStage));
			GetDefaultPipelineConfigurations(out var unreliableFragmentedPipelineStages, out var unreliableSequencedFragmentedPipelineStages, out var reliableSequencedPipelineStages);
			unreliableFragmentedPipeline = driver.CreatePipeline(unreliableFragmentedPipelineStages);
			unreliableSequencedFragmentedPipeline = driver.CreatePipeline(unreliableSequencedFragmentedPipelineStages);
			reliableSequencedPipeline = driver.CreatePipeline(reliableSequencedPipelineStages);
		}
	}
}

namespace Unity.Services.Relay.Models
{
	public static class AllocationUtils
	{
		private const string UdpProtocol = "udp";

		private const string DtlsProtocol = "dtls";

		private const string SecureWebSocketProtocol = "wss";

		private static readonly global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.RelayProtocol> k_StringToEnumMap = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.RelayProtocol>
		{
			["udp"] = global::Unity.Services.Multiplayer.RelayProtocol.UDP,
			["dtls"] = global::Unity.Services.Multiplayer.RelayProtocol.DTLS,
			["wss"] = global::Unity.Services.Multiplayer.RelayProtocol.WSS
		};

		private static readonly global::System.Collections.Generic.IReadOnlyDictionary<global::Unity.Services.Multiplayer.RelayProtocol, string> k_EnumToStringMap = global::System.Linq.Enumerable.ToDictionary(k_StringToEnumMap, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.RelayProtocol> kvp) => kvp.Value, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.RelayProtocol> kvp) => kvp.Key);

		public static global::Unity.Networking.Transport.Relay.RelayServerData ToRelayServerData(this global::Unity.Services.Relay.Models.Allocation allocation, string connectionType)
		{
			return allocation.ToRelayServerData(ToRelayProtocol(connectionType));
		}

		public static global::Unity.Networking.Transport.Relay.RelayServerData ToRelayServerData(this global::Unity.Services.Relay.Models.JoinAllocation allocation, string connectionType)
		{
			return allocation.ToRelayServerData(ToRelayProtocol(connectionType));
		}

		public static global::Unity.Networking.Transport.Relay.RelayServerData ToRelayServerData(this global::Unity.Services.Relay.Models.Allocation allocation, global::Unity.Services.Multiplayer.RelayProtocol connectionType)
		{
			if (allocation == null)
			{
				throw new global::System.ArgumentException("Invalid allocation.");
			}
			ValidateRelayConnectionType(connectionType);
			bool isWebSocket = connectionType == global::Unity.Services.Multiplayer.RelayProtocol.WSS;
			global::Unity.Services.Relay.Models.RelayServerEndpoint endpoint = GetEndpoint(allocation.ServerEndpoints, connectionType);
			return new global::Unity.Networking.Transport.Relay.RelayServerData(endpoint.Host, (ushort)endpoint.Port, allocation.AllocationIdBytes, allocation.ConnectionData, allocation.ConnectionData, allocation.Key, endpoint.Secure, isWebSocket);
		}

		public static global::Unity.Networking.Transport.Relay.RelayServerData ToRelayServerData(this global::Unity.Services.Relay.Models.JoinAllocation allocation, global::Unity.Services.Multiplayer.RelayProtocol connectionType)
		{
			if (allocation == null)
			{
				throw new global::System.ArgumentException("Invalid allocation.");
			}
			ValidateRelayConnectionType(connectionType);
			bool isWebSocket = connectionType == global::Unity.Services.Multiplayer.RelayProtocol.WSS;
			global::Unity.Services.Relay.Models.RelayServerEndpoint endpoint = GetEndpoint(allocation.ServerEndpoints, connectionType);
			return new global::Unity.Networking.Transport.Relay.RelayServerData(endpoint.Host, (ushort)endpoint.Port, allocation.AllocationIdBytes, allocation.ConnectionData, allocation.HostConnectionData, allocation.Key, endpoint.Secure, isWebSocket);
		}

		internal static bool IsValidProtocol(global::Unity.Services.Multiplayer.RelayProtocol protocol)
		{
			return global::System.Linq.Enumerable.Contains(GetValidProtocols(), protocol);
		}

		private static global::Unity.Services.Relay.Models.RelayServerEndpoint GetEndpoint(global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.RelayServerEndpoint> endpoints, global::Unity.Services.Multiplayer.RelayProtocol connectionType)
		{
			if (endpoints != null)
			{
				foreach (global::Unity.Services.Relay.Models.RelayServerEndpoint endpoint in endpoints)
				{
					if (string.Equals(endpoint.ConnectionType, connectionType.ToString("G"), global::System.StringComparison.OrdinalIgnoreCase))
					{
						return endpoint;
					}
				}
			}
			throw new global::System.ArgumentException($"No endpoint for connection type \"{connectionType}\" in allocation.");
		}

		private static global::Unity.Services.Multiplayer.RelayProtocol ToRelayProtocol(string connectionType)
		{
			connectionType = connectionType.ToLower();
			if (k_StringToEnumMap.TryGetValue(connectionType, out var value))
			{
				return value;
			}
			throw UnsupportedProtocolError(connectionType);
		}

		private static void ValidateRelayConnectionType(global::Unity.Services.Multiplayer.RelayProtocol connectionType)
		{
			if (!global::System.Linq.Enumerable.Contains(GetValidProtocols(), connectionType))
			{
				throw UnsupportedProtocolError(connectionType.ToString("G"));
			}
		}

		private static global::Unity.Services.Multiplayer.RelayProtocol[] GetValidProtocols()
		{
			return new global::Unity.Services.Multiplayer.RelayProtocol[3]
			{
				global::Unity.Services.Multiplayer.RelayProtocol.UDP,
				global::Unity.Services.Multiplayer.RelayProtocol.DTLS,
				global::Unity.Services.Multiplayer.RelayProtocol.WSS
			};
		}

		private static global::System.ArgumentException UnsupportedProtocolError(string connectionType)
		{
			string[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(GetValidProtocols(), (global::Unity.Services.Multiplayer.RelayProtocol p) => "\"" + k_EnumToStringMap[p] + "\""));
			return new global::System.ArgumentException("Invalid connection type: \"" + connectionType + "\". Connection type must be one of: " + EnumerateValidProtocols((global::System.ReadOnlySpan<string>)array) + ".");
		}

		private static string EnumerateValidProtocols(in global::System.ReadOnlySpan<string> names)
		{
			global::System.ReadOnlySpan<string> readOnlySpan = names;
			string text = string.Join(", ", readOnlySpan.Slice(0, readOnlySpan.Length - 1).ToArray());
			readOnlySpan = names;
			return text + " or " + readOnlySpan[readOnlySpan.Length - 1];
		}
	}
}

namespace Unity.Networking.Transport.Relay
{
	public struct RelayServerData
	{
		public global::Unity.Networking.Transport.NetworkEndpoint Endpoint;

		public ushort Nonce;

		public global::Unity.Networking.Transport.Relay.RelayConnectionData ConnectionData;

		public global::Unity.Networking.Transport.Relay.RelayConnectionData HostConnectionData;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId AllocationId;

		public global::Unity.Networking.Transport.Relay.RelayHMACKey HMACKey;

		public readonly byte IsSecure;

		public readonly byte IsWebSocket;

		internal unsafe fixed byte HMAC[32];

		internal global::Unity.Collections.FixedString512Bytes HostString;

		private unsafe RelayServerData(byte[] allocationId, byte[] connectionData, byte[] hostConnectionData, byte[] key)
		{
			Nonce = 0;
			AllocationId = global::Unity.Networking.Transport.Relay.RelayAllocationId.FromByteArray(allocationId);
			ConnectionData = global::Unity.Networking.Transport.Relay.RelayConnectionData.FromByteArray(connectionData);
			HostConnectionData = global::Unity.Networking.Transport.Relay.RelayConnectionData.FromByteArray(hostConnectionData);
			HMACKey = global::Unity.Networking.Transport.Relay.RelayHMACKey.FromByteArray(key);
			Endpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
			IsSecure = 0;
			IsWebSocket = 0;
			HostString = default(global::Unity.Collections.FixedString512Bytes);
			fixed (byte* hMAC = HMAC)
			{
				ComputeBindHMAC(hMAC, Nonce, ref ConnectionData, ref HMACKey);
			}
		}

		public RelayServerData(string host, ushort port, byte[] allocationId, byte[] connectionData, byte[] hostConnectionData, byte[] key, bool isSecure, bool isWebSocket)
			: this(allocationId, connectionData, hostConnectionData, key)
		{
			Endpoint = HostToEndpoint(host, port);
			IsSecure = (byte)(isSecure ? 1 : 0);
			IsWebSocket = (byte)(isWebSocket ? 1 : 0);
			HostString = host;
		}

		public RelayServerData(string host, ushort port, byte[] allocationId, byte[] connectionData, byte[] hostConnectionData, byte[] key, bool isSecure)
			: this(host, port, allocationId, connectionData, hostConnectionData, key, isSecure, isWebSocket: false)
		{
		}

		public unsafe RelayServerData(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, ushort nonce, ref global::Unity.Networking.Transport.Relay.RelayAllocationId allocationId, ref global::Unity.Networking.Transport.Relay.RelayConnectionData connectionData, ref global::Unity.Networking.Transport.Relay.RelayConnectionData hostConnectionData, ref global::Unity.Networking.Transport.Relay.RelayHMACKey key, bool isSecure, bool isWebSocket)
		{
			Endpoint = endpoint;
			Nonce = nonce;
			AllocationId = allocationId;
			ConnectionData = connectionData;
			HostConnectionData = hostConnectionData;
			HMACKey = key;
			IsSecure = (byte)(isSecure ? 1 : 0);
			IsWebSocket = (byte)(isWebSocket ? 1 : 0);
			fixed (byte* hMAC = HMAC)
			{
				ComputeBindHMAC(hMAC, Nonce, ref connectionData, ref key);
			}
			HostString = endpoint.ToFixedString512Bytes();
		}

		public RelayServerData(ref global::Unity.Networking.Transport.NetworkEndpoint endpoint, ushort nonce, ref global::Unity.Networking.Transport.Relay.RelayAllocationId allocationId, ref global::Unity.Networking.Transport.Relay.RelayConnectionData connectionData, ref global::Unity.Networking.Transport.Relay.RelayConnectionData hostConnectionData, ref global::Unity.Networking.Transport.Relay.RelayHMACKey key, bool isSecure)
			: this(ref endpoint, nonce, ref allocationId, ref connectionData, ref hostConnectionData, ref key, isSecure, isWebSocket: false)
		{
		}

		public unsafe void IncrementNonce()
		{
			Nonce++;
			fixed (byte* hMAC = HMAC)
			{
				ComputeBindHMAC(hMAC, Nonce, ref ConnectionData, ref HMACKey);
			}
		}

		private unsafe static void ComputeBindHMAC(byte* result, ushort nonce, ref global::Unity.Networking.Transport.Relay.RelayConnectionData connectionData, ref global::Unity.Networking.Transport.Relay.RelayHMACKey key)
		{
			byte* destination = stackalloc byte[64];
			fixed (byte* value = key.Value)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, value, 64L);
				byte* ptr = stackalloc byte[263];
				*ptr = 218;
				ptr[1] = 114;
				ptr[5] = (byte)nonce;
				ptr[6] = (byte)(nonce >> 8);
				ptr[7] = byte.MaxValue;
				fixed (byte* value2 = connectionData.Value)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(ptr + 8, value2, 255L);
				}
				global::Unity.Networking.Transport.HMACSHA256.ComputeHash(value, 64, ptr, 263, result);
			}
		}

		private static global::Unity.Networking.Transport.NetworkEndpoint HostToEndpoint(string host, ushort port)
		{
			if (global::Unity.Networking.Transport.NetworkEndpoint.TryParse(host, port, out var endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv4))
			{
				return endpoint;
			}
			if (global::Unity.Networking.Transport.NetworkEndpoint.TryParse(host, port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv6))
			{
				return endpoint;
			}
			global::System.Net.IPAddress[] addressList = global::System.Net.Dns.GetHostEntry(host).AddressList;
			if (addressList.Length != 0)
			{
				string address = addressList[0].ToString();
				global::System.Net.Sockets.AddressFamily addressFamily = addressList[0].AddressFamily;
				return global::Unity.Networking.Transport.NetworkEndpoint.Parse(address, port, (global::Unity.Networking.Transport.NetworkFamily)addressFamily);
			}
			global::UnityEngine.Debug.LogError("Couldn't map hostname " + host + " to an IP address.");
			return default(global::Unity.Networking.Transport.NetworkEndpoint);
		}
	}
}

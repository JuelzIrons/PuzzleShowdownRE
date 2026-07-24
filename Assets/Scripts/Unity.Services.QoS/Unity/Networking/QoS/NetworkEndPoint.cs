namespace Unity.Networking.QoS
{
	internal struct NetworkEndPoint
	{
		internal global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress rawNetworkAddress;

		private ushort Port => (ushort)(rawNetworkAddress.port1 | (rawNetworkAddress.port0 << 8));

		private global::Unity.Networking.QoS.NetworkFamily Family => FromBaselibFamily((global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family)rawNetworkAddress.family);

		internal string Address => AddressAsString();

		private bool IsValid => Family != global::Unity.Networking.QoS.NetworkFamily.Invalid;

		internal unsafe static bool TryParse(string address, ushort port, out global::Unity.Networking.QoS.NetworkEndPoint endpoint, global::Unity.Networking.QoS.NetworkFamily family = global::Unity.Networking.QoS.NetworkFamily.Ipv4)
		{
			endpoint = default(global::Unity.Networking.QoS.NetworkEndPoint);
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			fixed (byte* bytes = global::System.Text.Encoding.UTF8.GetBytes(address + "\0"))
			{
				fixed (global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress* dstAddress = &endpoint.rawNetworkAddress)
				{
					global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Encode(dstAddress, ToBaselibFamily(family), bytes, port, &baselib_ErrorState);
				}
			}
			if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				return endpoint.IsValid;
			}
			return false;
		}

		private string AddressAsString()
		{
			return Family switch
			{
				global::Unity.Networking.QoS.NetworkFamily.Ipv4 => $"{rawNetworkAddress.data0}.{rawNetworkAddress.data1}.{rawNetworkAddress.data2}.{rawNetworkAddress.data3}:{Port}", 
				global::Unity.Networking.QoS.NetworkFamily.Ipv6 => $"[{rawNetworkAddress.data1 | (rawNetworkAddress.data0 << 8):x}:{rawNetworkAddress.data3 | (rawNetworkAddress.data2 << 8):x}:{rawNetworkAddress.data5 | (rawNetworkAddress.data4 << 8):x}:{rawNetworkAddress.data7 | (rawNetworkAddress.data6 << 8):x}:{rawNetworkAddress.data9 | (rawNetworkAddress.data8 << 8):x}:{rawNetworkAddress.data11 | (rawNetworkAddress.data10 << 8):x}:{rawNetworkAddress.data13 | (rawNetworkAddress.data12 << 8):x}:{rawNetworkAddress.data15 | (rawNetworkAddress.data14 << 8):x}]:{Port}", 
				_ => string.Empty, 
			};
		}

		private static global::Unity.Networking.QoS.NetworkFamily FromBaselibFamily(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family family)
		{
			return family switch
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv4 => global::Unity.Networking.QoS.NetworkFamily.Ipv4, 
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv6 => global::Unity.Networking.QoS.NetworkFamily.Ipv6, 
				_ => global::Unity.Networking.QoS.NetworkFamily.Invalid, 
			};
		}

		private static global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family ToBaselibFamily(global::Unity.Networking.QoS.NetworkFamily family)
		{
			return family switch
			{
				global::Unity.Networking.QoS.NetworkFamily.Ipv4 => global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv4, 
				global::Unity.Networking.QoS.NetworkFamily.Ipv6 => global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv6, 
				_ => global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.Invalid, 
			};
		}
	}
}

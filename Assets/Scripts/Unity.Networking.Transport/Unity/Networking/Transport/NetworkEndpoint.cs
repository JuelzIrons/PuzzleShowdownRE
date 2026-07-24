namespace Unity.Networking.Transport
{
	public struct NetworkEndpoint : global::System.IEquatable<global::Unity.Networking.Transport.NetworkEndpoint>
	{
		public struct TransferrableData
		{
			internal global::Unity.Collections.FixedList64Bytes<byte> m_RawAddressContainer;
		}

		private const int k_Ipv4Length = 4;

		private const int k_Ipv6Length = 16;

		private const int k_CustomLength = 60;

		private const int k_FamilyOffset = 60;

		public global::Unity.Networking.Transport.NetworkEndpoint.TransferrableData Transferrable;

		internal unsafe byte* RawAddressPtr
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return (byte*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref Transferrable.m_RawAddressContainer);
			}
		}

		internal unsafe global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress* BaselibAddressPtr
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return (global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress*)RawAddressPtr;
			}
		}

		public unsafe global::Unity.Networking.Transport.NetworkFamily Family
		{
			get
			{
				return (global::Unity.Networking.Transport.NetworkFamily)RawAddressPtr[60];
			}
			set
			{
				RawAddressPtr[60] = (byte)value;
				if (value == global::Unity.Networking.Transport.NetworkFamily.Ipv4)
				{
					BaselibAddressPtr->family = 1;
				}
				if (value == global::Unity.Networking.Transport.NetworkFamily.Ipv6)
				{
					BaselibAddressPtr->family = 2;
				}
			}
		}

		public bool IsValid => Family != global::Unity.Networking.Transport.NetworkFamily.Invalid;

		public int Length => Family switch
		{
			global::Unity.Networking.Transport.NetworkFamily.Ipv4 => 4, 
			global::Unity.Networking.Transport.NetworkFamily.Ipv6 => 16, 
			global::Unity.Networking.Transport.NetworkFamily.Custom => 60, 
			_ => 0, 
		};

		public unsafe ushort Port
		{
			get
			{
				return (ushort)(BaselibAddressPtr->port1 | (BaselibAddressPtr->port0 << 8));
			}
			set
			{
				BaselibAddressPtr->port0 = (byte)((value >> 8) & 0xFF);
				BaselibAddressPtr->port1 = (byte)(value & 0xFF);
			}
		}

		[global::System.Obsolete("Use Port instead, and use standard C# APIs to convert to/from network byte order.")]
		public unsafe ushort RawPort
		{
			get
			{
				return (ushort)((BaselibAddressPtr->port1 << 8) | BaselibAddressPtr->port0);
			}
			set
			{
				BaselibAddressPtr->port0 = (byte)(value & 0xFF);
				BaselibAddressPtr->port1 = (byte)((value >> 8) & 0xFF);
			}
		}

		public static global::Unity.Networking.Transport.NetworkEndpoint AnyIpv4 => new global::Unity.Networking.Transport.NetworkEndpoint
		{
			Family = global::Unity.Networking.Transport.NetworkFamily.Ipv4
		};

		public static global::Unity.Networking.Transport.NetworkEndpoint AnyIpv6 => new global::Unity.Networking.Transport.NetworkEndpoint
		{
			Family = global::Unity.Networking.Transport.NetworkFamily.Ipv6
		};

		public unsafe static global::Unity.Networking.Transport.NetworkEndpoint LoopbackIpv4
		{
			get
			{
				global::Unity.Networking.Transport.NetworkEndpoint result = new global::Unity.Networking.Transport.NetworkEndpoint
				{
					Family = global::Unity.Networking.Transport.NetworkFamily.Ipv4
				};
				result.BaselibAddressPtr->data0 = 127;
				result.BaselibAddressPtr->data3 = 1;
				return result;
			}
		}

		public unsafe static global::Unity.Networking.Transport.NetworkEndpoint LoopbackIpv6
		{
			get
			{
				global::Unity.Networking.Transport.NetworkEndpoint result = new global::Unity.Networking.Transport.NetworkEndpoint
				{
					Family = global::Unity.Networking.Transport.NetworkFamily.Ipv6
				};
				result.BaselibAddressPtr->data15 = 1;
				return result;
			}
		}

		public bool IsAny
		{
			get
			{
				if (Family == global::Unity.Networking.Transport.NetworkFamily.Ipv4 || Family == global::Unity.Networking.Transport.NetworkFamily.Ipv6)
				{
					if (!(this == AnyIpv4.WithPort(Port)))
					{
						return this == AnyIpv6.WithPort(Port);
					}
					return true;
				}
				return false;
			}
		}

		public bool IsLoopback
		{
			get
			{
				if (Family == global::Unity.Networking.Transport.NetworkFamily.Ipv4 || Family == global::Unity.Networking.Transport.NetworkFamily.Ipv6)
				{
					if (!(this == LoopbackIpv4.WithPort(Port)))
					{
						return this == LoopbackIpv6.WithPort(Port);
					}
					return true;
				}
				return false;
			}
		}

		public string Address => ToString();

		internal unsafe NetworkEndpoint(global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress baselibAddress)
		{
			Transferrable = default(global::Unity.Networking.Transport.NetworkEndpoint.TransferrableData);
			if (baselibAddress.family == 1)
			{
				Family = global::Unity.Networking.Transport.NetworkFamily.Ipv4;
			}
			if (baselibAddress.family == 2)
			{
				Family = global::Unity.Networking.Transport.NetworkFamily.Ipv6;
			}
			*BaselibAddressPtr = baselibAddress;
		}

		public global::Unity.Networking.Transport.NetworkEndpoint WithPort(ushort port)
		{
			global::Unity.Networking.Transport.NetworkEndpoint result = this;
			result.Port = port;
			return result;
		}

		public unsafe global::Unity.Collections.NativeArray<byte> GetRawAddressBytes()
		{
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), RawAddressPtr, Length);
			return nativeArray;
		}

		public unsafe void SetRawAddressBytes(global::Unity.Collections.NativeArray<byte> bytes, global::Unity.Networking.Transport.NetworkFamily family = global::Unity.Networking.Transport.NetworkFamily.Ipv4)
		{
			int num = global::Unity.Mathematics.math.min(bytes.Length, 60);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(RawAddressPtr, global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(bytes), num);
			Family = family;
		}

		public unsafe static bool TryParse(string address, ushort port, out global::Unity.Networking.Transport.NetworkEndpoint endpoint, global::Unity.Networking.Transport.NetworkFamily family = global::Unity.Networking.Transport.NetworkFamily.Invalid)
		{
			if (family == global::Unity.Networking.Transport.NetworkFamily.Invalid)
			{
				endpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
				if (TryParse(address, port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv4))
				{
					return true;
				}
				return TryParse(address, port, out endpoint, global::Unity.Networking.Transport.NetworkFamily.Ipv6);
			}
			endpoint = default(global::Unity.Networking.Transport.NetworkEndpoint);
			if (family != global::Unity.Networking.Transport.NetworkFamily.Ipv4 && family != global::Unity.Networking.Transport.NetworkFamily.Ipv6)
			{
				global::UnityEngine.Debug.LogError("Can only parse addresses that are IPv4 or IPv6.");
				return false;
			}
			endpoint.Family = family;
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(address + "\0");
			global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family family2 = ((family == global::Unity.Networking.Transport.NetworkFamily.Ipv4) ? global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv4 : global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Family.IPv6);
			global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState baselib_ErrorState = default(global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorState);
			fixed (byte* ip = bytes)
			{
				global::Unity.Baselib.LowLevel.Binding.Baselib_NetworkAddress_Encode(endpoint.BaselibAddressPtr, family2, ip, port, &baselib_ErrorState);
			}
			if (baselib_ErrorState.code == global::Unity.Baselib.LowLevel.Binding.Baselib_ErrorCode.Success)
			{
				return true;
			}
			endpoint.Family = global::Unity.Networking.Transport.NetworkFamily.Invalid;
			return false;
		}

		public static global::Unity.Networking.Transport.NetworkEndpoint Parse(string address, ushort port, global::Unity.Networking.Transport.NetworkFamily family = global::Unity.Networking.Transport.NetworkFamily.Invalid)
		{
			if (!TryParse(address, port, out var endpoint, family))
			{
				return default(global::Unity.Networking.Transport.NetworkEndpoint);
			}
			return endpoint;
		}

		public global::Unity.Collections.FixedString128Bytes ToFixedString()
		{
			global::Unity.Collections.FixedString512Bytes other = ToFixedString512Bytes();
			if (other.Length > global::Unity.Collections.FixedString128Bytes.UTF8MaxLengthInBytes)
			{
				throw new global::System.Exception("Endpoint URL is too long. Use ToFixedString512Bytes instead.");
			}
			return new global::Unity.Collections.FixedString128Bytes(in other);
		}

		public unsafe global::Unity.Collections.FixedString512Bytes ToFixedString512Bytes()
		{
			global::Unity.Collections.FixedString128Bytes fs = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fixedString32Bytes = default(global::Unity.Collections.FixedString32Bytes);
			byte* rawAddressPtr = RawAddressPtr;
			switch (Family)
			{
			case global::Unity.Networking.Transport.NetworkFamily.Ipv4:
				fs = $"{*rawAddressPtr}.{rawAddressPtr[1]}.{rawAddressPtr[2]}.{rawAddressPtr[3]}";
				break;
			case global::Unity.Networking.Transport.NetworkFamily.Ipv6:
			{
				global::Unity.Collections.FixedStringMethods.Append(ref fs, '[');
				for (int j = 0; j < 16; j += 2)
				{
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString32Bytes>(ref fs, (global::Unity.Collections.FixedString32Bytes)$"{rawAddressPtr[j]:x2}{rawAddressPtr[j + 1]:x2}:");
				}
				fs.Length--;
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ']');
				break;
			}
			case global::Unity.Networking.Transport.NetworkFamily.Custom:
			{
				fs = "custom:0x";
				for (int i = 0; i < 60; i++)
				{
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString32Bytes>(ref fs, (global::Unity.Collections.FixedString32Bytes)$"{rawAddressPtr[i]:x2}");
				}
				break;
			}
			default:
				return "invalid";
			}
			if (Family == global::Unity.Networking.Transport.NetworkFamily.Ipv4 || Family == global::Unity.Networking.Transport.NetworkFamily.Ipv6)
			{
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ':');
				global::Unity.Collections.FixedStringMethods.Append(ref fs, Port);
			}
			return fs;
		}

		public unsafe global::Unity.Collections.FixedString512Bytes ToFixedStringNoPort()
		{
			global::Unity.Collections.FixedString128Bytes fs = default(global::Unity.Collections.FixedString128Bytes);
			global::Unity.Collections.FixedString32Bytes fixedString32Bytes = default(global::Unity.Collections.FixedString32Bytes);
			byte* rawAddressPtr = RawAddressPtr;
			switch (Family)
			{
			case global::Unity.Networking.Transport.NetworkFamily.Ipv4:
				fs = $"{*rawAddressPtr}.{rawAddressPtr[1]}.{rawAddressPtr[2]}.{rawAddressPtr[3]}";
				break;
			case global::Unity.Networking.Transport.NetworkFamily.Ipv6:
			{
				global::Unity.Collections.FixedStringMethods.Append(ref fs, '[');
				for (int j = 0; j < 16; j += 2)
				{
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString32Bytes>(ref fs, (global::Unity.Collections.FixedString32Bytes)$"{rawAddressPtr[j]:x2}{rawAddressPtr[j + 1]:x2}:");
				}
				fs.Length--;
				global::Unity.Collections.FixedStringMethods.Append(ref fs, ']');
				break;
			}
			case global::Unity.Networking.Transport.NetworkFamily.Custom:
			{
				fs = "custom:0x";
				for (int i = 0; i < 60; i++)
				{
					global::Unity.Collections.FixedStringMethods.Append<global::Unity.Collections.FixedString128Bytes, global::Unity.Collections.FixedString32Bytes>(ref fs, (global::Unity.Collections.FixedString32Bytes)$"{rawAddressPtr[i]:x2}");
				}
				break;
			}
			default:
				return "invalid";
			}
			return fs;
		}

		public override string ToString()
		{
			return ToFixedString512Bytes().ToString();
		}

		public unsafe override int GetHashCode()
		{
			int bytes = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Collections.FixedList64Bytes<byte>>();
			return (int)global::Unity.Collections.CollectionHelper.Hash(RawAddressPtr, bytes);
		}

		public unsafe bool Equals(global::Unity.Networking.Transport.NetworkEndpoint other)
		{
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Collections.FixedList64Bytes<byte>>();
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCmp(RawAddressPtr, other.RawAddressPtr, num) == 0;
		}

		public override bool Equals(object other)
		{
			return Equals((global::Unity.Networking.Transport.NetworkEndpoint)other);
		}

		public static bool operator ==(global::Unity.Networking.Transport.NetworkEndpoint lhs, global::Unity.Networking.Transport.NetworkEndpoint rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(global::Unity.Networking.Transport.NetworkEndpoint lhs, global::Unity.Networking.Transport.NetworkEndpoint rhs)
		{
			return !lhs.Equals(rhs);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckFamilyIsIPv4OrIPv6()
		{
			if (Family != global::Unity.Networking.Transport.NetworkFamily.Ipv4 && Family != global::Unity.Networking.Transport.NetworkFamily.Ipv6)
			{
				throw new global::System.InvalidOperationException($"Trying to access endpoint as IPv4 or IPv6, but family is {Family}.");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckRawAddressLength(int length, global::Unity.Networking.Transport.NetworkFamily family)
		{
			if (family == global::Unity.Networking.Transport.NetworkFamily.Ipv4 && length != 4)
			{
				throw new global::System.ArgumentException($"Raw IPv4 addresses must be {4} bytes long (got {length}).");
			}
			if (family == global::Unity.Networking.Transport.NetworkFamily.Ipv6 && length != 16)
			{
				throw new global::System.ArgumentException($"Raw IPv6 addresses must be {4} bytes long (got {length}).");
			}
			if (family == global::Unity.Networking.Transport.NetworkFamily.Custom && length > 60)
			{
				throw new global::System.ArgumentException($"Raw custom addresses must be no greater than {60} bytes long (got {length}).");
			}
			if (family == global::Unity.Networking.Transport.NetworkFamily.Invalid)
			{
				throw new global::System.ArgumentException("Can't set raw address if family is invalid.");
			}
		}
	}
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	[global::System.Obsolete("NetworkEndPoint has been renamed to NetworkEndpoint. (UnityUpgradable) -> NetworkEndpoint", true)]
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	public struct NetworkEndPoint
	{
	}
}

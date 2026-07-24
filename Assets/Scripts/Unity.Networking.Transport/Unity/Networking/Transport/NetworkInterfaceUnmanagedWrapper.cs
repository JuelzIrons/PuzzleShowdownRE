namespace Unity.Networking.Transport
{
	public struct NetworkInterfaceUnmanagedWrapper<T> : global::Unity.Networking.Transport.INetworkInterface, global::System.IDisposable where T : global::Unity.Networking.Transport.INetworkInterface
	{
		private struct LocalEndpoint_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public global::Unity.Networking.Transport.NetworkEndpoint Return;
		}

		private struct Bind_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public global::Unity.Networking.Transport.NetworkEndpoint Endpoint;

			public int Return;
		}

		private struct Dispose_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;
		}

		private struct Initialize_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public global::Unity.Networking.Transport.NetworkSettings NetworkSettings;

			public int PacketPadding;

			public int Return;
		}

		private struct Listen_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public int Return;
		}

		private struct ScheduleReceive_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public global::Unity.Networking.Transport.ReceiveJobArguments Arguments;

			public global::Unity.Jobs.JobHandle Dependency;

			public global::Unity.Jobs.JobHandle Return;
		}

		private struct ScheduleSend_Arguments
		{
			public global::Unity.Networking.Transport.Utilities.ManagedReference<T> InterfaceReference;

			public global::Unity.Networking.Transport.SendJobArguments Arguments;

			public global::Unity.Jobs.JobHandle Dependency;

			public global::Unity.Jobs.JobHandle Return;
		}

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_LocalEndpoint_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_Bind_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_Dispose_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_Initialize_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_Listen_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_ScheduleReceive_FPtr;

		private static global::Unity.Networking.Transport.ManagedCallWrapper s_ScheduleSend_FPtr;

		private global::Unity.Networking.Transport.Utilities.ManagedReference<T> m_NetworkInterfaceReference;

		internal global::Unity.Networking.Transport.Utilities.ManagedReference<T> NetworkInterfaceReference => m_NetworkInterfaceReference;

		public global::Unity.Networking.Transport.NetworkEndpoint LocalEndpoint
		{
			get
			{
				global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.LocalEndpoint_Arguments arguments = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.LocalEndpoint_Arguments
				{
					InterfaceReference = m_NetworkInterfaceReference
				};
				s_LocalEndpoint_FPtr.Invoke(ref arguments);
				return arguments.Return;
			}
		}

		private unsafe static void InitializeFunctionPointers()
		{
			if (!s_LocalEndpoint_FPtr.IsCreated)
			{
				s_LocalEndpoint_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&LocalEndpointWrapper));
				s_Bind_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&BindWrapper));
				s_Dispose_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&DisposeWrapper));
				s_Initialize_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&InitializeWrapper));
				s_Listen_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ListenWrapper));
				s_ScheduleReceive_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ScheduleReceiveWrapper));
				s_ScheduleSend_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ScheduleSendWrapper));
			}
		}

		internal NetworkInterfaceUnmanagedWrapper(ref T networkInterface)
		{
			InitializeFunctionPointers();
			m_NetworkInterfaceReference = new global::Unity.Networking.Transport.Utilities.ManagedReference<T>(ref networkInterface);
		}

		private unsafe static void LocalEndpointWrapper(void* argumentsPtr, int argumentsSize)
		{
			ref global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.LocalEndpoint_Arguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.LocalEndpoint_Arguments>(argumentsPtr, argumentsSize);
			reference.Return = reference.InterfaceReference.Element.LocalEndpoint;
		}

		private unsafe static void BindWrapper(void* argumentsPtr, int argumentsSize)
		{
			ref global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Bind_Arguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Bind_Arguments>(argumentsPtr, argumentsSize);
			ref T element = ref reference.InterfaceReference.Element;
			global::Unity.Networking.Transport.NetworkEndpoint endpoint = reference.Endpoint;
			reference.Return = element.Bind(endpoint);
		}

		public int Bind(global::Unity.Networking.Transport.NetworkEndpoint endpoint)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Bind_Arguments arguments = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Bind_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference,
				Endpoint = endpoint
			};
			s_Bind_FPtr.Invoke(ref arguments);
			return arguments.Return;
		}

		private unsafe static void DisposeWrapper(void* argumentsPtr, int argumentsSize)
		{
			global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Dispose_Arguments>(argumentsPtr, argumentsSize).InterfaceReference.Element.Dispose();
		}

		public void Dispose()
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Dispose_Arguments arguments = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Dispose_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference
			};
			s_Dispose_FPtr.Invoke(ref arguments);
			m_NetworkInterfaceReference.Dispose();
		}

		private unsafe static void InitializeWrapper(void* argumentsPtr, int argumentsSize)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments>(argumentsPtr, 0);
			ref T element = ref value.InterfaceReference.Element;
			ref global::Unity.Networking.Transport.NetworkSettings networkSettings = ref value.NetworkSettings;
			ref int packetPadding = ref value.PacketPadding;
			value.Return = element.Initialize(ref networkSettings, ref packetPadding);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(argumentsPtr, 0, value);
		}

		public unsafe int Initialize(ref global::Unity.Networking.Transport.NetworkSettings settings, ref int packetPadding)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments value = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference,
				NetworkSettings = settings,
				PacketPadding = packetPadding
			};
			byte* ptr = stackalloc byte[(int)(uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments>()];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr, 0, value);
			s_Initialize_FPtr.Invoke(ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments>());
			value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Initialize_Arguments>(ptr, 0);
			settings = value.NetworkSettings;
			packetPadding = value.PacketPadding;
			return value.Return;
		}

		private unsafe static void ListenWrapper(void* argumentsPtr, int argumentsSize)
		{
			ref global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Listen_Arguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Listen_Arguments>(argumentsPtr, argumentsSize);
			reference.Return = reference.InterfaceReference.Element.Listen();
		}

		public int Listen()
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Listen_Arguments arguments = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.Listen_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference
			};
			s_Listen_FPtr.Invoke(ref arguments);
			return arguments.Return;
		}

		private unsafe static void ScheduleReceiveWrapper(void* argumentsPtr, int argumentsSize)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments>(argumentsPtr, 0);
			ref T element = ref value.InterfaceReference.Element;
			ref global::Unity.Networking.Transport.ReceiveJobArguments arguments = ref value.Arguments;
			global::Unity.Jobs.JobHandle dependency = value.Dependency;
			value.Return = element.ScheduleReceive(ref arguments, dependency);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(argumentsPtr, 0, value);
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments receiveJobArguments, global::Unity.Jobs.JobHandle dep)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments value = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference,
				Arguments = receiveJobArguments,
				Dependency = dep
			};
			byte* ptr = stackalloc byte[(int)(uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments>()];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr, 0, value);
			s_ScheduleReceive_FPtr.Invoke(ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments>());
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleReceive_Arguments>(ptr, 0).Return;
		}

		private unsafe static void ScheduleSendWrapper(void* argumentsPtr, int argumentsSize)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments value = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments>(argumentsPtr, 0);
			ref T element = ref value.InterfaceReference.Element;
			ref global::Unity.Networking.Transport.SendJobArguments arguments = ref value.Arguments;
			global::Unity.Jobs.JobHandle dependency = value.Dependency;
			value.Return = element.ScheduleSend(ref arguments, dependency);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(argumentsPtr, 0, value);
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments sendJobArguments, global::Unity.Jobs.JobHandle dep)
		{
			global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments value = new global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments
			{
				InterfaceReference = m_NetworkInterfaceReference,
				Arguments = sendJobArguments,
				Dependency = dep
			};
			byte* ptr = stackalloc byte[(int)(uint)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments>()];
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.WriteArrayElement(ptr, 0, value);
			s_ScheduleSend_FPtr.Invoke(ptr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments>());
			return global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ReadArrayElement<global::Unity.Networking.Transport.NetworkInterfaceUnmanagedWrapper<T>.ScheduleSend_Arguments>(ptr, 0).Return;
		}
	}
}

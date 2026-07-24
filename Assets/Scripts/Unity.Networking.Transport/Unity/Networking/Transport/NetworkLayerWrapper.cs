namespace Unity.Networking.Transport
{
	internal struct NetworkLayerWrapper : global::System.IDisposable
	{
		private struct DisposeArguments
		{
			public unsafe void* LayerPtr;
		}

		private struct ScheduleReceiveArguments
		{
			public unsafe void* LayerPtr;

			public global::Unity.Networking.Transport.ReceiveJobArguments JobArguments;

			public global::Unity.Jobs.JobHandle Dependency;

			public global::Unity.Jobs.JobHandle Return;
		}

		private struct ScheduleSendArguments
		{
			public unsafe void* LayerPtr;

			public global::Unity.Networking.Transport.SendJobArguments JobArguments;

			public global::Unity.Jobs.JobHandle Dependency;

			public global::Unity.Jobs.JobHandle Return;
		}

		private unsafe void* m_RawLayerData;

		private long m_TypeHash;

		private global::Unity.Networking.Transport.ManagedCallWrapper m_Dispose_FPtr;

		private global::Unity.Networking.Transport.ManagedCallWrapper m_ScheduleReceive_FPtr;

		private global::Unity.Networking.Transport.ManagedCallWrapper m_ScheduleSend_FPtr;

		public unsafe static global::Unity.Networking.Transport.NetworkLayerWrapper Create<T>(ref T layer) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			global::Unity.Networking.Transport.NetworkLayerWrapper result = new global::Unity.Networking.Transport.NetworkLayerWrapper
			{
				m_RawLayerData = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Malloc(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>(), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AlignOf<T>(), global::Unity.Collections.Allocator.Persistent),
				m_TypeHash = global::Unity.Burst.BurstRuntime.GetHashCode64<T>(),
				m_Dispose_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&DisposeWrapper<T>)),
				m_ScheduleReceive_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ScheduleReceiveWrapper<T>)),
				m_ScheduleSend_FPtr = new global::Unity.Networking.Transport.ManagedCallWrapper((delegate*<void*, int, void>)(&ScheduleSendWrapper<T>))
			};
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.CopyStructureToPtr(ref layer, result.m_RawLayerData);
			return result;
		}

		public bool IsType<T>() where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			return m_TypeHash == global::Unity.Burst.BurstRuntime.GetHashCode64<T>();
		}

		public unsafe ref T CastRef<T>() where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			return ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(m_RawLayerData);
		}

		public unsafe void Dispose()
		{
			global::Unity.Networking.Transport.NetworkLayerWrapper.DisposeArguments arguments = new global::Unity.Networking.Transport.NetworkLayerWrapper.DisposeArguments
			{
				LayerPtr = m_RawLayerData
			};
			m_Dispose_FPtr.Invoke(ref arguments);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.Free(m_RawLayerData, global::Unity.Collections.Allocator.Persistent);
		}

		private unsafe static void DisposeWrapper<T>(void* argumentsPtr, int size) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkLayerWrapper.DisposeArguments>(argumentsPtr, size).LayerPtr).Dispose();
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleReceive(ref global::Unity.Networking.Transport.ReceiveJobArguments jobArguments, global::Unity.Jobs.JobHandle dependency)
		{
			global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleReceiveArguments arguments = new global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleReceiveArguments
			{
				LayerPtr = m_RawLayerData,
				JobArguments = jobArguments,
				Dependency = dependency
			};
			m_ScheduleReceive_FPtr.Invoke(ref arguments);
			return arguments.Return;
		}

		private unsafe static void ScheduleReceiveWrapper<T>(void* argumentsPtr, int size) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			ref global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleReceiveArguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleReceiveArguments>(argumentsPtr, size);
			reference.Return = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(reference.LayerPtr).ScheduleReceive(ref reference.JobArguments, reference.Dependency);
		}

		public unsafe global::Unity.Jobs.JobHandle ScheduleSend(ref global::Unity.Networking.Transport.SendJobArguments jobArguments, global::Unity.Jobs.JobHandle dependency)
		{
			global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleSendArguments arguments = new global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleSendArguments
			{
				LayerPtr = m_RawLayerData,
				JobArguments = jobArguments,
				Dependency = dependency
			};
			m_ScheduleSend_FPtr.Invoke(ref arguments);
			return arguments.Return;
		}

		private unsafe static void ScheduleSendWrapper<T>(void* argumentsPtr, int size) where T : unmanaged, global::Unity.Networking.Transport.INetworkLayer
		{
			ref global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleSendArguments reference = ref global::Unity.Networking.Transport.ManagedCallWrapper.ArgumentsFromPtr<global::Unity.Networking.Transport.NetworkLayerWrapper.ScheduleSendArguments>(argumentsPtr, size);
			reference.Return = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(reference.LayerPtr).ScheduleSend(ref reference.JobArguments, reference.Dependency);
		}
	}
}

namespace Unity.Networking.Transport
{
	public struct NetworkSettings : global::System.IDisposable
	{
		private struct ParameterSlice
		{
			public int Offset;

			public int Size;
		}

		private const int k_MapInitialCapacity = 8;

		private global::Unity.Collections.NativeParallelHashMap<long, global::Unity.Networking.Transport.NetworkSettings.ParameterSlice> m_ParameterOffsets;

		private global::Unity.Collections.NativeList<byte> m_Parameters;

		private byte m_Initialized;

		private byte m_ReadOnly;

		public bool IsCreated
		{
			get
			{
				if (m_Initialized != 0)
				{
					return m_Parameters.IsCreated;
				}
				return true;
			}
		}

		private bool EnsureInitializedOrError()
		{
			if (m_Initialized == 0)
			{
				m_Initialized = 1;
				m_Parameters = new global::Unity.Collections.NativeList<byte>(global::Unity.Collections.Allocator.Temp);
				m_ParameterOffsets = new global::Unity.Collections.NativeParallelHashMap<long, global::Unity.Networking.Transport.NetworkSettings.ParameterSlice>(8, global::Unity.Collections.Allocator.Temp);
			}
			if (!m_Parameters.IsCreated)
			{
				global::UnityEngine.Debug.LogError("The NetworkSettings has been deallocated, it is not allowed to access it.");
				return false;
			}
			return true;
		}

		public NetworkSettings(global::Unity.Collections.Allocator allocator)
		{
			m_Initialized = 1;
			m_ReadOnly = 0;
			m_Parameters = new global::Unity.Collections.NativeList<byte>(allocator);
			m_ParameterOffsets = new global::Unity.Collections.NativeParallelHashMap<long, global::Unity.Networking.Transport.NetworkSettings.ParameterSlice>(8, allocator);
		}

		internal NetworkSettings(global::Unity.Networking.Transport.NetworkSettings from, global::Unity.Collections.Allocator allocator)
		{
			m_Initialized = 1;
			m_ReadOnly = 0;
			if (from.m_Initialized == 0)
			{
				m_Parameters = new global::Unity.Collections.NativeList<byte>(allocator);
				m_ParameterOffsets = new global::Unity.Collections.NativeParallelHashMap<long, global::Unity.Networking.Transport.NetworkSettings.ParameterSlice>(8, allocator);
				return;
			}
			m_Parameters = new global::Unity.Collections.NativeList<byte>(from.m_Parameters.Length, allocator);
			m_Parameters.AddRangeNoResize(from.m_Parameters);
			global::Unity.Collections.NativeArray<long> keyArray = from.m_ParameterOffsets.GetKeyArray(global::Unity.Collections.Allocator.Temp);
			m_ParameterOffsets = new global::Unity.Collections.NativeParallelHashMap<long, global::Unity.Networking.Transport.NetworkSettings.ParameterSlice>(keyArray.Length, allocator);
			for (int i = 0; i < keyArray.Length; i++)
			{
				m_ParameterOffsets.Add(keyArray[i], from.m_ParameterOffsets[keyArray[i]]);
			}
		}

		public void Dispose()
		{
			m_Initialized = 1;
			if (m_Parameters.IsCreated)
			{
				m_Parameters.Dispose();
				m_ParameterOffsets.Dispose();
			}
		}

		public global::Unity.Networking.Transport.NetworkSettings AsReadOnly()
		{
			global::Unity.Networking.Transport.NetworkSettings result = this;
			result.m_ReadOnly = 1;
			return result;
		}

		public unsafe void AddRawParameterStruct<T>(ref T parameter) where T : unmanaged, global::Unity.Networking.Transport.INetworkParameter
		{
			if (!EnsureInitializedOrError())
			{
				return;
			}
			if (m_ReadOnly != 0)
			{
				global::UnityEngine.Debug.LogError("NetworkSettings structure is read-only, modifications are not allowed.");
				return;
			}
			ValidateParameterOrError(ref parameter);
			long hashCode = global::Unity.Burst.BurstRuntime.GetHashCode64<T>();
			global::Unity.Networking.Transport.NetworkSettings.ParameterSlice item = new global::Unity.Networking.Transport.NetworkSettings.ParameterSlice
			{
				Offset = m_Parameters.Length,
				Size = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>()
			};
			if (m_ParameterOffsets.TryAdd(hashCode, item))
			{
				m_Parameters.Resize(m_Parameters.Length + item.Size, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				item = m_ParameterOffsets[hashCode];
			}
			T* ptr = (T*)(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafePtr(m_Parameters) + item.Offset);
			*ptr = parameter;
		}

		public unsafe bool TryGet<T>(out T parameter) where T : unmanaged, global::Unity.Networking.Transport.INetworkParameter
		{
			parameter = default(T);
			if (!EnsureInitializedOrError())
			{
				return false;
			}
			long hashCode = global::Unity.Burst.BurstRuntime.GetHashCode64<T>();
			if (m_ParameterOffsets.TryGetValue(hashCode, out var item))
			{
				parameter = *(T*)(global::Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility.GetUnsafeReadOnlyPtr(m_Parameters) + item.Offset);
				return true;
			}
			return false;
		}

		internal static void ValidateParameterOrError<T>(ref T parameter) where T : global::Unity.Networking.Transport.INetworkParameter
		{
			if (!parameter.Validate())
			{
				global::UnityEngine.Debug.LogError("The provided network parameter (" + parameter.GetType().Name + ") is not valid");
			}
		}
	}
}

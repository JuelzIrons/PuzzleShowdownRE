namespace Unity.Networking.Transport
{
	internal struct ConnectionDataMap<T> : global::System.IDisposable where T : unmanaged
	{
		private struct ConnectionSlot
		{
			public int Version;

			public T Value;
		}

		private global::Unity.Collections.NativeList<global::Unity.Networking.Transport.ConnectionDataMap<T>.ConnectionSlot> m_List;

		private global::Unity.Collections.NativeReference<T> m_DefaultData;

		public bool IsCreated => m_List.IsCreated;

		public int Length => m_List.Length;

		internal T this[global::Unity.Networking.Transport.ConnectionId connection]
		{
			get
			{
				if (connection.Id >= m_List.Length || connection.Id < 0)
				{
					return m_DefaultData.Value;
				}
				global::Unity.Networking.Transport.ConnectionDataMap<T>.ConnectionSlot connectionSlot = m_List[connection.Id];
				if (connectionSlot.Version != connection.Version)
				{
					return m_DefaultData.Value;
				}
				return connectionSlot.Value;
			}
			set
			{
				if (connection.Id >= m_List.Length)
				{
					m_List.Resize(connection.Id + 1, global::Unity.Collections.NativeArrayOptions.ClearMemory);
				}
				ref global::Unity.Networking.Transport.ConnectionDataMap<T>.ConnectionSlot reference = ref m_List.ElementAt(connection.Id);
				if (reference.Version > connection.Version)
				{
					global::UnityEngine.Debug.LogError("The provided connection is not valid");
					return;
				}
				reference.Version = connection.Version;
				reference.Value = value;
			}
		}

		internal ConnectionDataMap(int initialCapacity, T defaultDataValue, global::Unity.Collections.Allocator allocator)
		{
			m_List = new global::Unity.Collections.NativeList<global::Unity.Networking.Transport.ConnectionDataMap<T>.ConnectionSlot>(initialCapacity, allocator);
			m_DefaultData = new global::Unity.Collections.NativeReference<T>(defaultDataValue, allocator);
		}

		public void Dispose()
		{
			m_List.Dispose();
			m_DefaultData.Dispose();
		}

		internal void ClearData(ref global::Unity.Networking.Transport.ConnectionId connection)
		{
			this[connection] = m_DefaultData.Value;
		}

		internal global::Unity.Networking.Transport.ConnectionId ConnectionAt(int index)
		{
			if (index < 0 || index >= m_List.Length)
			{
				return default(global::Unity.Networking.Transport.ConnectionId);
			}
			return new global::Unity.Networking.Transport.ConnectionId
			{
				Id = index,
				Version = m_List[index].Version
			};
		}

		internal T DataAt(int index)
		{
			if (index < 0 || index >= m_List.Length)
			{
				return m_DefaultData.Value;
			}
			return m_List[index].Value;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::Unity.Networking.Transport.ConnectionDataMap<T> connectionDataMap)
			{
				return this == connectionDataMap;
			}
			return false;
		}

		public unsafe override int GetHashCode()
		{
			return ((int)m_List.GetUnsafeList()).GetHashCode();
		}

		public unsafe static bool operator ==(global::Unity.Networking.Transport.ConnectionDataMap<T> a, global::Unity.Networking.Transport.ConnectionDataMap<T> b)
		{
			return a.m_List.GetUnsafeList() == b.m_List.GetUnsafeList();
		}

		public static bool operator !=(global::Unity.Networking.Transport.ConnectionDataMap<T> a, global::Unity.Networking.Transport.ConnectionDataMap<T> b)
		{
			return !(a == b);
		}
	}
}

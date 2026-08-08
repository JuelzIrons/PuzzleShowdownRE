namespace Steamworks
{
	public class MMKVPMarshaller
	{
		private global::System.IntPtr m_pNativeArray;

		private global::System.IntPtr m_pArrayEntries;

		public MMKVPMarshaller(global::Steamworks.MatchMakingKeyValuePair_t[] filters)
		{
			if (filters != null)
			{
				int num = global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.MatchMakingKeyValuePair_t));
				m_pNativeArray = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)) * filters.Length);
				m_pArrayEntries = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(num * filters.Length);
				for (int i = 0; i < filters.Length; i++)
				{
					global::System.Runtime.InteropServices.Marshal.StructureToPtr(filters[i], new global::System.IntPtr(m_pArrayEntries.ToInt64() + i * num), fDeleteOld: false);
				}
				global::System.Runtime.InteropServices.Marshal.WriteIntPtr(m_pNativeArray, m_pArrayEntries);
			}
		}

		~MMKVPMarshaller()
		{
			if (m_pArrayEntries != global::System.IntPtr.Zero)
			{
				global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_pArrayEntries);
			}
			if (m_pNativeArray != global::System.IntPtr.Zero)
			{
				global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_pNativeArray);
			}
		}

		public static implicit operator global::System.IntPtr(global::Steamworks.MMKVPMarshaller that)
		{
			return that.m_pNativeArray;
		}
	}
}

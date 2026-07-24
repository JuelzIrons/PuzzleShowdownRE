namespace Steamworks
{
	public class ISteamMatchmakingPlayersResponse
	{
		public delegate void AddPlayerToList(string pchName, int nScore, float flTimePlayed);

		public delegate void PlayersFailedToRespond();

		public delegate void PlayersRefreshComplete();

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalAddPlayerToList(global::System.IntPtr thisptr, global::System.IntPtr pchName, int nScore, float flTimePlayed);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalPlayersFailedToRespond(global::System.IntPtr thisptr);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalPlayersRefreshComplete(global::System.IntPtr thisptr);

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential)]
		private class VTable
		{
			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingPlayersResponse.InternalAddPlayerToList m_VTAddPlayerToList;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond m_VTPlayersFailedToRespond;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete m_VTPlayersRefreshComplete;
		}

		private global::Steamworks.ISteamMatchmakingPlayersResponse.VTable m_VTable;

		private global::System.IntPtr m_pVTable;

		private global::System.Runtime.InteropServices.GCHandle m_pGCHandle;

		private global::System.IntPtr m_pInstance;

		private global::Steamworks.ISteamMatchmakingPlayersResponse.AddPlayerToList m_AddPlayerToList;

		private global::Steamworks.ISteamMatchmakingPlayersResponse.PlayersFailedToRespond m_PlayersFailedToRespond;

		private global::Steamworks.ISteamMatchmakingPlayersResponse.PlayersRefreshComplete m_PlayersRefreshComplete;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingPlayersResponse> m_Instances = new global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingPlayersResponse>();

		public ISteamMatchmakingPlayersResponse(global::Steamworks.ISteamMatchmakingPlayersResponse.AddPlayerToList onAddPlayerToList, global::Steamworks.ISteamMatchmakingPlayersResponse.PlayersFailedToRespond onPlayersFailedToRespond, global::Steamworks.ISteamMatchmakingPlayersResponse.PlayersRefreshComplete onPlayersRefreshComplete)
		{
			if (onAddPlayerToList == null || onPlayersFailedToRespond == null || onPlayersRefreshComplete == null)
			{
				throw new global::System.ArgumentNullException();
			}
			m_AddPlayerToList = onAddPlayerToList;
			m_PlayersFailedToRespond = onPlayersFailedToRespond;
			m_PlayersRefreshComplete = onPlayersRefreshComplete;
			m_VTable = new global::Steamworks.ISteamMatchmakingPlayersResponse.VTable
			{
				m_VTAddPlayerToList = InternalOnAddPlayerToList,
				m_VTPlayersFailedToRespond = InternalOnPlayersFailedToRespond,
				m_VTPlayersRefreshComplete = InternalOnPlayersRefreshComplete
			};
			m_pVTable = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.ISteamMatchmakingPlayersResponse.VTable)));
			global::System.Runtime.InteropServices.Marshal.StructureToPtr(m_VTable, m_pVTable, fDeleteOld: false);
			m_pGCHandle = global::System.Runtime.InteropServices.GCHandle.Alloc(m_pVTable, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			m_pInstance = m_pGCHandle.AddrOfPinnedObject();
			lock (m_Instances)
			{
				m_Instances[m_pInstance] = this;
			}
		}

		~ISteamMatchmakingPlayersResponse()
		{
			lock (m_Instances)
			{
				m_Instances.Remove(m_pVTable);
			}
			if (m_pVTable != global::System.IntPtr.Zero)
			{
				global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_pVTable);
			}
			if (m_pGCHandle.IsAllocated)
			{
				m_pGCHandle.Free();
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingPlayersResponse.InternalAddPlayerToList))]
		private static void InternalOnAddPlayerToList(global::System.IntPtr thisptr, global::System.IntPtr pchName, int nScore, float flTimePlayed)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_AddPlayerToList(global::Steamworks.InteropHelp.PtrToStringUTF8(pchName), nScore, flTimePlayed);
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingPlayersResponse.InternalPlayersFailedToRespond))]
		private static void InternalOnPlayersFailedToRespond(global::System.IntPtr thisptr)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_PlayersFailedToRespond();
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingPlayersResponse.InternalPlayersRefreshComplete))]
		private static void InternalOnPlayersRefreshComplete(global::System.IntPtr thisptr)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_PlayersRefreshComplete();
			}
		}

		public static explicit operator global::System.IntPtr(global::Steamworks.ISteamMatchmakingPlayersResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}
	}
}

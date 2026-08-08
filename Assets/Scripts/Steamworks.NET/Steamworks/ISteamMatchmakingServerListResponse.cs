namespace Steamworks
{
	public class ISteamMatchmakingServerListResponse
	{
		public delegate void ServerResponded(global::Steamworks.HServerListRequest hRequest, int iServer);

		public delegate void ServerFailedToRespond(global::Steamworks.HServerListRequest hRequest, int iServer);

		public delegate void RefreshComplete(global::Steamworks.HServerListRequest hRequest, global::Steamworks.EMatchMakingServerResponse response);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, int iServer);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, int iServer);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		private delegate void InternalRefreshComplete(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, global::Steamworks.EMatchMakingServerResponse response);

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential)]
		private class VTable
		{
			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingServerListResponse.InternalServerResponded m_VTServerResponded;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingServerListResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingServerListResponse.InternalRefreshComplete m_VTRefreshComplete;
		}

		private global::Steamworks.ISteamMatchmakingServerListResponse.VTable m_VTable;

		private global::System.IntPtr m_pVTable;

		private global::System.Runtime.InteropServices.GCHandle m_pGCHandle;

		private global::System.IntPtr m_pInstance;

		private global::Steamworks.ISteamMatchmakingServerListResponse.ServerResponded m_ServerResponded;

		private global::Steamworks.ISteamMatchmakingServerListResponse.ServerFailedToRespond m_ServerFailedToRespond;

		private global::Steamworks.ISteamMatchmakingServerListResponse.RefreshComplete m_RefreshComplete;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingServerListResponse> m_Instances = new global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingServerListResponse>();

		public ISteamMatchmakingServerListResponse(global::Steamworks.ISteamMatchmakingServerListResponse.ServerResponded onServerResponded, global::Steamworks.ISteamMatchmakingServerListResponse.ServerFailedToRespond onServerFailedToRespond, global::Steamworks.ISteamMatchmakingServerListResponse.RefreshComplete onRefreshComplete)
		{
			if (onServerResponded == null || onServerFailedToRespond == null || onRefreshComplete == null)
			{
				throw new global::System.ArgumentNullException();
			}
			m_ServerResponded = onServerResponded;
			m_ServerFailedToRespond = onServerFailedToRespond;
			m_RefreshComplete = onRefreshComplete;
			m_VTable = new global::Steamworks.ISteamMatchmakingServerListResponse.VTable
			{
				m_VTServerResponded = InternalOnServerResponded,
				m_VTServerFailedToRespond = InternalOnServerFailedToRespond,
				m_VTRefreshComplete = InternalOnRefreshComplete
			};
			m_pVTable = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.ISteamMatchmakingServerListResponse.VTable)));
			global::System.Runtime.InteropServices.Marshal.StructureToPtr(m_VTable, m_pVTable, fDeleteOld: false);
			m_pGCHandle = global::System.Runtime.InteropServices.GCHandle.Alloc(m_pVTable, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			m_pInstance = m_pGCHandle.AddrOfPinnedObject();
			lock (m_Instances)
			{
				m_Instances[m_pInstance] = this;
			}
		}

		~ISteamMatchmakingServerListResponse()
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

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingServerListResponse.InternalServerResponded))]
		private static void InternalOnServerResponded(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, int iServer)
		{
			try
			{
				if (m_Instances.TryGetValue(thisptr, out var value))
				{
					value.m_ServerResponded(hRequest, iServer);
				}
			}
			catch (global::System.Exception e)
			{
				global::Steamworks.CallbackDispatcher.ExceptionHandler(e);
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingServerListResponse.InternalServerFailedToRespond))]
		private static void InternalOnServerFailedToRespond(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, int iServer)
		{
			try
			{
				if (m_Instances.TryGetValue(thisptr, out var value))
				{
					value.m_ServerFailedToRespond(hRequest, iServer);
				}
			}
			catch (global::System.Exception e)
			{
				global::Steamworks.CallbackDispatcher.ExceptionHandler(e);
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingServerListResponse.InternalRefreshComplete))]
		private static void InternalOnRefreshComplete(global::System.IntPtr thisptr, global::Steamworks.HServerListRequest hRequest, global::Steamworks.EMatchMakingServerResponse response)
		{
			try
			{
				if (m_Instances.TryGetValue(thisptr, out var value))
				{
					value.m_RefreshComplete(hRequest, response);
				}
			}
			catch (global::System.Exception e)
			{
				global::Steamworks.CallbackDispatcher.ExceptionHandler(e);
			}
		}

		public static explicit operator global::System.IntPtr(global::Steamworks.ISteamMatchmakingServerListResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}
	}
}

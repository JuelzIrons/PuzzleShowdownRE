namespace Steamworks
{
	public class ISteamMatchmakingPingResponse
	{
		public delegate void ServerResponded(global::Steamworks.gameserveritem_t server);

		public delegate void ServerFailedToRespond();

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		private delegate void InternalServerResponded(global::System.IntPtr thisptr, global::Steamworks.gameserveritem_t server);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		private delegate void InternalServerFailedToRespond(global::System.IntPtr thisptr);

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential)]
		private class VTable
		{
			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingPingResponse.InternalServerResponded m_VTServerResponded;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingPingResponse.InternalServerFailedToRespond m_VTServerFailedToRespond;
		}

		private global::Steamworks.ISteamMatchmakingPingResponse.VTable m_VTable;

		private global::System.IntPtr m_pVTable;

		private global::System.Runtime.InteropServices.GCHandle m_pGCHandle;

		private global::System.IntPtr m_pInstance;

		private global::Steamworks.ISteamMatchmakingPingResponse.ServerResponded m_ServerResponded;

		private global::Steamworks.ISteamMatchmakingPingResponse.ServerFailedToRespond m_ServerFailedToRespond;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingPingResponse> m_Instances = new global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingPingResponse>();

		public ISteamMatchmakingPingResponse(global::Steamworks.ISteamMatchmakingPingResponse.ServerResponded onServerResponded, global::Steamworks.ISteamMatchmakingPingResponse.ServerFailedToRespond onServerFailedToRespond)
		{
			if (onServerResponded == null || onServerFailedToRespond == null)
			{
				throw new global::System.ArgumentNullException();
			}
			m_ServerResponded = onServerResponded;
			m_ServerFailedToRespond = onServerFailedToRespond;
			m_VTable = new global::Steamworks.ISteamMatchmakingPingResponse.VTable
			{
				m_VTServerResponded = InternalOnServerResponded,
				m_VTServerFailedToRespond = InternalOnServerFailedToRespond
			};
			m_pVTable = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.ISteamMatchmakingPingResponse.VTable)));
			global::System.Runtime.InteropServices.Marshal.StructureToPtr(m_VTable, m_pVTable, fDeleteOld: false);
			m_pGCHandle = global::System.Runtime.InteropServices.GCHandle.Alloc(m_pVTable, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			m_pInstance = m_pGCHandle.AddrOfPinnedObject();
			lock (m_Instances)
			{
				m_Instances[m_pInstance] = this;
			}
		}

		~ISteamMatchmakingPingResponse()
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

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingPingResponse.InternalServerResponded))]
		private static void InternalOnServerResponded(global::System.IntPtr thisptr, global::Steamworks.gameserveritem_t server)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_ServerResponded(server);
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingPingResponse.InternalServerFailedToRespond))]
		private static void InternalOnServerFailedToRespond(global::System.IntPtr thisptr)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_ServerFailedToRespond();
			}
		}

		public static explicit operator global::System.IntPtr(global::Steamworks.ISteamMatchmakingPingResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}
	}
}

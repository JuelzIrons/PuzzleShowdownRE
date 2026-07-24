namespace Steamworks
{
	public class ISteamMatchmakingRulesResponse
	{
		public delegate void RulesResponded(string pchRule, string pchValue);

		public delegate void RulesFailedToRespond();

		public delegate void RulesRefreshComplete();

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalRulesResponded(global::System.IntPtr thisptr, global::System.IntPtr pchRule, global::System.IntPtr pchValue);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalRulesFailedToRespond(global::System.IntPtr thisptr);

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.ThisCall)]
		public delegate void InternalRulesRefreshComplete(global::System.IntPtr thisptr);

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential)]
		private class VTable
		{
			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesResponded m_VTRulesResponded;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond m_VTRulesFailedToRespond;

			[global::System.NonSerialized]
			[global::System.Runtime.InteropServices.MarshalAs(global::System.Runtime.InteropServices.UnmanagedType.FunctionPtr)]
			public global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete m_VTRulesRefreshComplete;
		}

		private global::Steamworks.ISteamMatchmakingRulesResponse.VTable m_VTable;

		private global::System.IntPtr m_pVTable;

		private global::System.Runtime.InteropServices.GCHandle m_pGCHandle;

		private global::System.IntPtr m_pInstance;

		private global::Steamworks.ISteamMatchmakingRulesResponse.RulesResponded m_RulesResponded;

		private global::Steamworks.ISteamMatchmakingRulesResponse.RulesFailedToRespond m_RulesFailedToRespond;

		private global::Steamworks.ISteamMatchmakingRulesResponse.RulesRefreshComplete m_RulesRefreshComplete;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingRulesResponse> m_Instances = new global::System.Collections.Generic.Dictionary<global::System.IntPtr, global::Steamworks.ISteamMatchmakingRulesResponse>();

		public ISteamMatchmakingRulesResponse(global::Steamworks.ISteamMatchmakingRulesResponse.RulesResponded onRulesResponded, global::Steamworks.ISteamMatchmakingRulesResponse.RulesFailedToRespond onRulesFailedToRespond, global::Steamworks.ISteamMatchmakingRulesResponse.RulesRefreshComplete onRulesRefreshComplete)
		{
			if (onRulesResponded == null || onRulesFailedToRespond == null || onRulesRefreshComplete == null)
			{
				throw new global::System.ArgumentNullException();
			}
			m_RulesResponded = onRulesResponded;
			m_RulesFailedToRespond = onRulesFailedToRespond;
			m_RulesRefreshComplete = onRulesRefreshComplete;
			m_VTable = new global::Steamworks.ISteamMatchmakingRulesResponse.VTable
			{
				m_VTRulesResponded = InternalOnRulesResponded,
				m_VTRulesFailedToRespond = InternalOnRulesFailedToRespond,
				m_VTRulesRefreshComplete = InternalOnRulesRefreshComplete
			};
			m_pVTable = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.ISteamMatchmakingRulesResponse.VTable)));
			global::System.Runtime.InteropServices.Marshal.StructureToPtr(m_VTable, m_pVTable, fDeleteOld: false);
			m_pGCHandle = global::System.Runtime.InteropServices.GCHandle.Alloc(m_pVTable, global::System.Runtime.InteropServices.GCHandleType.Pinned);
			m_pInstance = m_pGCHandle.AddrOfPinnedObject();
			lock (m_Instances)
			{
				m_Instances[m_pInstance] = this;
			}
		}

		~ISteamMatchmakingRulesResponse()
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

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesResponded))]
		private static void InternalOnRulesResponded(global::System.IntPtr thisptr, global::System.IntPtr pchRule, global::System.IntPtr pchValue)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_RulesResponded(global::Steamworks.InteropHelp.PtrToStringUTF8(pchRule), global::Steamworks.InteropHelp.PtrToStringUTF8(pchValue));
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesFailedToRespond))]
		private static void InternalOnRulesFailedToRespond(global::System.IntPtr thisptr)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_RulesFailedToRespond();
			}
		}

		[global::Steamworks.MonoPInvokeCallback(typeof(global::Steamworks.ISteamMatchmakingRulesResponse.InternalRulesRefreshComplete))]
		private static void InternalOnRulesRefreshComplete(global::System.IntPtr thisptr)
		{
			if (m_Instances.TryGetValue(thisptr, out var value))
			{
				value.m_RulesRefreshComplete();
			}
		}

		public static explicit operator global::System.IntPtr(global::Steamworks.ISteamMatchmakingRulesResponse that)
		{
			return that.m_pGCHandle.AddrOfPinnedObject();
		}
	}
}

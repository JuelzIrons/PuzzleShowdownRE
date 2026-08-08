namespace Steamworks
{
	public static class CallbackDispatcher
	{
		public delegate void SteamworksExceptionHandler(global::System.Exception e);

		public static global::Steamworks.CallbackDispatcher.SteamworksExceptionHandler ExceptionHandler = DefaultExceptionHandler;

		private static global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> m_registeredCallbacks = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>>();

		private static global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> m_registeredGameServerCallbacks = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>>();

		private static global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Steamworks.CallResult>> m_registeredCallResults = new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Steamworks.CallResult>>();

		private static object m_sync = new object();

		private static global::System.IntPtr m_pCallbackMsg;

		private static int m_initCount;

		public static bool IsInitialized => m_initCount > 0;

		private static void DefaultExceptionHandler(global::System.Exception e)
		{
			global::UnityEngine.Debug.LogException(e);
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitOnPlayMode()
		{
			m_initCount = 0;
		}

		internal static void Initialize()
		{
			lock (m_sync)
			{
				if (m_initCount == 0)
				{
					global::Steamworks.NativeMethods.SteamAPI_ManualDispatch_Init();
					m_pCallbackMsg = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.CallbackMsg_t)));
				}
				m_initCount++;
			}
		}

		internal static void Shutdown()
		{
			lock (m_sync)
			{
				m_initCount--;
				if (m_initCount == 0)
				{
					UnregisterAll();
					global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_pCallbackMsg);
					m_pCallbackMsg = global::System.IntPtr.Zero;
				}
			}
		}

		internal static void Register(global::Steamworks.Callback cb)
		{
			int callbackIdentity = global::Steamworks.CallbackIdentities.GetCallbackIdentity(cb.GetCallbackType());
			global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> dictionary = (cb.IsGameServer ? m_registeredGameServerCallbacks : m_registeredCallbacks);
			lock (m_sync)
			{
				if (!dictionary.TryGetValue(callbackIdentity, out var value))
				{
					value = new global::System.Collections.Generic.List<global::Steamworks.Callback>();
					dictionary.Add(callbackIdentity, value);
				}
				value.Add(cb);
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public static void Register(global::Steamworks.SteamAPICall_t asyncCall, global::Steamworks.CallResult cr)
		{
			lock (m_sync)
			{
				if (!m_registeredCallResults.TryGetValue((ulong)asyncCall, out var value))
				{
					value = new global::System.Collections.Generic.List<global::Steamworks.CallResult>();
					m_registeredCallResults.Add((ulong)asyncCall, value);
				}
				value.Add(cr);
			}
		}

		internal static void Unregister(global::Steamworks.Callback cb)
		{
			int callbackIdentity = global::Steamworks.CallbackIdentities.GetCallbackIdentity(cb.GetCallbackType());
			global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> dictionary = (cb.IsGameServer ? m_registeredGameServerCallbacks : m_registeredCallbacks);
			lock (m_sync)
			{
				if (dictionary.TryGetValue(callbackIdentity, out var value))
				{
					value.Remove(cb);
					if (value.Count == 0)
					{
						dictionary.Remove(callbackIdentity);
					}
				}
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public static void Unregister(global::Steamworks.SteamAPICall_t asyncCall, global::Steamworks.CallResult cr)
		{
			lock (m_sync)
			{
				if (m_registeredCallResults.TryGetValue((ulong)asyncCall, out var value))
				{
					value.Remove(cr);
					if (value.Count == 0)
					{
						m_registeredCallResults.Remove((ulong)asyncCall);
					}
				}
			}
		}

		private static void UnregisterAll()
		{
			global::System.Collections.Generic.List<global::Steamworks.Callback> list = new global::System.Collections.Generic.List<global::Steamworks.Callback>();
			global::System.Collections.Generic.List<global::Steamworks.CallResult> list2 = new global::System.Collections.Generic.List<global::Steamworks.CallResult>();
			lock (m_sync)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> registeredCallback in m_registeredCallbacks)
				{
					list.AddRange(registeredCallback.Value);
				}
				m_registeredCallbacks.Clear();
				foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> registeredGameServerCallback in m_registeredGameServerCallbacks)
				{
					list.AddRange(registeredGameServerCallback.Value);
				}
				m_registeredGameServerCallbacks.Clear();
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<global::Steamworks.CallResult>> registeredCallResult in m_registeredCallResults)
				{
					list2.AddRange(registeredCallResult.Value);
				}
				m_registeredCallResults.Clear();
				foreach (global::Steamworks.Callback item in list)
				{
					item.SetUnregistered();
				}
				foreach (global::Steamworks.CallResult item2 in list2)
				{
					item2.SetUnregistered();
				}
			}
		}

		internal static void RunFrame(bool isGameServer)
		{
			if (!IsInitialized)
			{
				throw new global::System.InvalidOperationException("Callback dispatcher is not initialized.");
			}
			global::Steamworks.HSteamPipe hSteamPipe = (global::Steamworks.HSteamPipe)(isGameServer ? global::Steamworks.NativeMethods.SteamGameServer_GetHSteamPipe() : global::Steamworks.NativeMethods.SteamAPI_GetHSteamPipe());
			global::Steamworks.NativeMethods.SteamAPI_ManualDispatch_RunFrame(hSteamPipe);
			global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Steamworks.Callback>> dictionary = (isGameServer ? m_registeredGameServerCallbacks : m_registeredCallbacks);
			while (global::Steamworks.NativeMethods.SteamAPI_ManualDispatch_GetNextCallback(hSteamPipe, m_pCallbackMsg))
			{
				global::Steamworks.CallbackMsg_t callbackMsg_t = (global::Steamworks.CallbackMsg_t)global::System.Runtime.InteropServices.Marshal.PtrToStructure(m_pCallbackMsg, typeof(global::Steamworks.CallbackMsg_t));
				try
				{
					if (callbackMsg_t.m_iCallback == 703)
					{
						global::Steamworks.SteamAPICallCompleted_t steamAPICallCompleted_t = (global::Steamworks.SteamAPICallCompleted_t)global::System.Runtime.InteropServices.Marshal.PtrToStructure(callbackMsg_t.m_pubParam, typeof(global::Steamworks.SteamAPICallCompleted_t));
						global::System.IntPtr intPtr = global::System.Runtime.InteropServices.Marshal.AllocHGlobal((int)steamAPICallCompleted_t.m_cubParam);
						if (global::Steamworks.NativeMethods.SteamAPI_ManualDispatch_GetAPICallResult(hSteamPipe, steamAPICallCompleted_t.m_hAsyncCall, intPtr, (int)steamAPICallCompleted_t.m_cubParam, steamAPICallCompleted_t.m_iCallback, out var pbFailed))
						{
							lock (m_sync)
							{
								if (m_registeredCallResults.TryGetValue((ulong)steamAPICallCompleted_t.m_hAsyncCall, out var value))
								{
									m_registeredCallResults.Remove((ulong)steamAPICallCompleted_t.m_hAsyncCall);
									foreach (global::Steamworks.CallResult item in value)
									{
										item.OnRunCallResult(intPtr, pbFailed, (ulong)steamAPICallCompleted_t.m_hAsyncCall);
										item.SetUnregistered();
									}
								}
							}
						}
						global::System.Runtime.InteropServices.Marshal.FreeHGlobal(intPtr);
						continue;
					}
					global::System.Collections.Generic.List<global::Steamworks.Callback> list = null;
					lock (m_sync)
					{
						global::System.Collections.Generic.List<global::Steamworks.Callback> value2 = null;
						if (dictionary.TryGetValue(callbackMsg_t.m_iCallback, out value2))
						{
							list = new global::System.Collections.Generic.List<global::Steamworks.Callback>(value2);
						}
					}
					if (list == null)
					{
						continue;
					}
					foreach (global::Steamworks.Callback item2 in list)
					{
						item2.OnRunCallback(callbackMsg_t.m_pubParam);
					}
				}
				catch (global::System.Exception e)
				{
					ExceptionHandler(e);
				}
				finally
				{
					global::Steamworks.NativeMethods.SteamAPI_ManualDispatch_FreeLastCallback(hSteamPipe);
				}
			}
		}
	}
}

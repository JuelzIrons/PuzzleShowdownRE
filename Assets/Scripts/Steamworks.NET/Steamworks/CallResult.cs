namespace Steamworks
{
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	public abstract class CallResult
	{
		protected internal abstract global::System.Type GetCallbackType();

		protected internal abstract void OnRunCallResult(global::System.IntPtr pvParam, bool bFailed, ulong hSteamAPICall);

		protected internal abstract void SetUnregistered();
	}
	public sealed class CallResult<T> : global::Steamworks.CallResult, global::System.IDisposable
	{
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);

		private global::Steamworks.SteamAPICall_t m_hAPICall = global::Steamworks.SteamAPICall_t.Invalid;

		private bool m_bDisposed;

		public global::Steamworks.SteamAPICall_t Handle => m_hAPICall;

		private event global::Steamworks.CallResult<T>.APIDispatchDelegate m_Func;

		public static global::Steamworks.CallResult<T> Create(global::Steamworks.CallResult<T>.APIDispatchDelegate func = null)
		{
			return new global::Steamworks.CallResult<T>(func);
		}

		public CallResult(global::Steamworks.CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
		}

		~CallResult()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!m_bDisposed)
			{
				global::System.GC.SuppressFinalize(this);
				Cancel();
				m_bDisposed = true;
			}
		}

		public void Set(global::Steamworks.SteamAPICall_t hAPICall, global::Steamworks.CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new global::System.Exception("CallResult function was null, you must either set it in the CallResult Constructor or via Set()");
			}
			if (m_hAPICall != global::Steamworks.SteamAPICall_t.Invalid)
			{
				global::Steamworks.CallbackDispatcher.Unregister(m_hAPICall, this);
			}
			m_hAPICall = hAPICall;
			if (hAPICall != global::Steamworks.SteamAPICall_t.Invalid)
			{
				global::Steamworks.CallbackDispatcher.Register(hAPICall, this);
			}
		}

		public bool IsActive()
		{
			return m_hAPICall != global::Steamworks.SteamAPICall_t.Invalid;
		}

		public void Cancel()
		{
			if (IsActive())
			{
				global::Steamworks.CallbackDispatcher.Unregister(m_hAPICall, this);
			}
		}

		protected internal override global::System.Type GetCallbackType()
		{
			return typeof(T);
		}

		protected internal override void OnRunCallResult(global::System.IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			if ((global::Steamworks.SteamAPICall_t)hSteamAPICall_ == m_hAPICall)
			{
				try
				{
					this.m_Func((T)global::System.Runtime.InteropServices.Marshal.PtrToStructure(pvParam, typeof(T)), bFailed);
				}
				catch (global::System.Exception e)
				{
					global::Steamworks.CallbackDispatcher.ExceptionHandler(e);
				}
			}
		}

		protected internal override void SetUnregistered()
		{
			m_hAPICall = global::Steamworks.SteamAPICall_t.Invalid;
		}
	}
}

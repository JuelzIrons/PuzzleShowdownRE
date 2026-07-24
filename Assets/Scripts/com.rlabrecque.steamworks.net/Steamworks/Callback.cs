namespace Steamworks
{
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	public abstract class Callback
	{
		public abstract bool IsGameServer { get; }

		internal abstract global::System.Type GetCallbackType();

		internal abstract void OnRunCallback(global::System.IntPtr pvParam);

		internal abstract void SetUnregistered();
	}
	public sealed class Callback<T> : global::Steamworks.Callback, global::System.IDisposable
	{
		public delegate void DispatchDelegate(T param);

		private bool m_bGameServer;

		private bool m_bIsRegistered;

		private bool m_bDisposed;

		public override bool IsGameServer => m_bGameServer;

		private event global::Steamworks.Callback<T>.DispatchDelegate m_Func;

		public static global::Steamworks.Callback<T> Create(global::Steamworks.Callback<T>.DispatchDelegate func)
		{
			return new global::Steamworks.Callback<T>(func);
		}

		public static global::Steamworks.Callback<T> CreateGameServer(global::Steamworks.Callback<T>.DispatchDelegate func)
		{
			return new global::Steamworks.Callback<T>(func, bGameServer: true);
		}

		public Callback(global::Steamworks.Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			m_bGameServer = bGameServer;
			Register(func);
		}

		~Callback()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!m_bDisposed)
			{
				global::System.GC.SuppressFinalize(this);
				if (m_bIsRegistered)
				{
					Unregister();
				}
				m_bDisposed = true;
			}
		}

		public void Register(global::Steamworks.Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new global::System.Exception("Callback function must not be null.");
			}
			if (m_bIsRegistered)
			{
				Unregister();
			}
			this.m_Func = func;
			global::Steamworks.CallbackDispatcher.Register(this);
			m_bIsRegistered = true;
		}

		public void Unregister()
		{
			global::Steamworks.CallbackDispatcher.Unregister(this);
			m_bIsRegistered = false;
		}

		internal override global::System.Type GetCallbackType()
		{
			return typeof(T);
		}

		internal override void OnRunCallback(global::System.IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)global::System.Runtime.InteropServices.Marshal.PtrToStructure(pvParam, typeof(T)));
			}
			catch (global::System.Exception e)
			{
				global::Steamworks.CallbackDispatcher.ExceptionHandler(e);
			}
		}

		internal override void SetUnregistered()
		{
			m_bIsRegistered = false;
		}
	}
}

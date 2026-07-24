namespace Unity.Services.Core
{
	public class ServiceObserver<T> : global::System.IDisposable
	{
		private global::Unity.Services.Core.IUnityServices m_Registry;

		public T Service { get; private set; }

		public event global::System.Action<T> Initialized;

		public ServiceObserver()
			: this(global::Unity.Services.Core.UnityServices.Instance)
		{
			if (global::UnityEngine.Application.isPlaying)
			{
				m_Registry = global::Unity.Services.Core.UnityServices.Instance;
				Init();
			}
		}

		public ServiceObserver(global::Unity.Services.Core.IUnityServices registry)
		{
			m_Registry = registry ?? throw new global::System.ArgumentNullException("registry");
			Init();
		}

		private void Init()
		{
			if (m_Registry.State == global::Unity.Services.Core.ServicesInitializationState.Initialized)
			{
				AssignService();
				return;
			}
			m_Registry.Initialized -= AssignService;
			m_Registry.Initialized += AssignService;
		}

		private void AssignService()
		{
			Service = m_Registry.GetService<T>();
			if (Service != null)
			{
				this.Initialized?.Invoke(Service);
			}
		}

		public void Dispose()
		{
			if (m_Registry != null)
			{
				m_Registry.Initialized -= AssignService;
				m_Registry = null;
			}
		}
	}
}

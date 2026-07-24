namespace UnityEngine.AdaptivePerformance
{
	public sealed class AdaptivePerformanceManagerSettings : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.HideInInspector]
		private bool m_InitializationComplete = false;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Determines if the Adaptive Performance Manager instance is responsible for creating and destroying the appropriate loader instance.")]
		private bool m_AutomaticLoading = false;

		[global::UnityEngine.Tooltip("Determines if the Adaptive Performance Manager instance is responsible for starting and stopping subsystems for the active loader instance.")]
		[global::UnityEngine.SerializeField]
		private bool m_AutomaticRunning = false;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("List of Adaptive Performance Loader instances arranged in desired load order.")]
		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader> m_Loaders = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader>();

		[global::UnityEngine.HideInInspector]
		private static global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader s_ActiveLoader;

		public bool automaticLoading
		{
			get
			{
				return m_AutomaticLoading;
			}
			set
			{
				m_AutomaticLoading = value;
			}
		}

		public bool automaticRunning
		{
			get
			{
				return m_AutomaticRunning;
			}
			set
			{
				m_AutomaticRunning = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader> loaders
		{
			get
			{
				return m_Loaders;
			}
			set
			{
				m_Loaders = value;
			}
		}

		public bool isInitializationComplete => m_InitializationComplete;

		[global::UnityEngine.HideInInspector]
		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader activeLoader
		{
			get
			{
				return s_ActiveLoader;
			}
			private set
			{
				s_ActiveLoader = value;
			}
		}

		public T ActiveLoaderAs<T>() where T : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader
		{
			return activeLoader as T;
		}

		internal void InitializeLoaderSync()
		{
			if (isInitializationComplete && activeLoader != null)
			{
				global::UnityEngine.Debug.LogWarning("Adaptive Performance Management has already initialized an active loader in this scene.Please make sure to stop all subsystems and deinitialize the active loader before initializing a new one.");
				return;
			}
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader loader in loaders)
			{
				if (loader != null && loader.Initialize())
				{
					activeLoader = loader;
					m_InitializationComplete = true;
					return;
				}
			}
			activeLoader = null;
		}

		internal global::System.Collections.IEnumerator InitializeLoader()
		{
			if (isInitializationComplete && activeLoader != null)
			{
				global::UnityEngine.Debug.LogWarning("Adaptive Performance Management has already initialized an active loader in this scene.Please make sure to stop all subsystems and deinitialize the active loader before initializing a new one.");
				yield break;
			}
			foreach (global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader loader in loaders)
			{
				if (loader != null && loader.Initialize())
				{
					activeLoader = loader;
					m_InitializationComplete = true;
					yield break;
				}
				yield return null;
			}
			activeLoader = null;
		}

		internal void StartSubsystems()
		{
			if (!m_InitializationComplete)
			{
				global::UnityEngine.Debug.LogWarning("Call to StartSubsystems without an initialized manager.Please make sure to wait for initialization to complete before calling this API.");
			}
			else if (!(activeLoader == null))
			{
				activeLoader.Start();
			}
		}

		internal void StopSubsystems()
		{
			if (!m_InitializationComplete)
			{
				global::UnityEngine.Debug.LogWarning("Call to StopSubsystems without an initialized manager.Please make sure to wait for initialization to complete before calling this API.");
			}
			else if (!(activeLoader == null))
			{
				activeLoader.Stop();
			}
		}

		internal void DeinitializeLoader()
		{
			if (!m_InitializationComplete)
			{
				global::UnityEngine.Debug.LogWarning("Call to DeinitializeLoader without an initialized manager.Please make sure to wait for initialization to complete before calling this API.");
				return;
			}
			StopSubsystems();
			if (activeLoader != null)
			{
				activeLoader.Deinitialize();
				activeLoader = null;
			}
			m_InitializationComplete = false;
		}

		private void OnDisable()
		{
			if (automaticLoading && automaticRunning)
			{
				StopSubsystems();
			}
		}

		private void OnDestroy()
		{
			if (automaticLoading)
			{
				DeinitializeLoader();
			}
		}
	}
}

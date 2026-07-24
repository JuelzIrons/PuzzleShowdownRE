namespace UnityEngine.AdaptivePerformance
{
	public abstract class AdaptivePerformanceLoaderHelper : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceLoader
	{
		protected global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.ISubsystem> m_SubsystemInstanceMap = new global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.ISubsystem>();

		public override T GetLoadedSubsystem<T>()
		{
			global::System.Type typeFromHandle = typeof(T);
			m_SubsystemInstanceMap.TryGetValue(typeFromHandle, out var value);
			return value as T;
		}

		protected void StartSubsystem<T>() where T : class, global::UnityEngine.ISubsystem
		{
			GetLoadedSubsystem<T>()?.Start();
		}

		protected void StopSubsystem<T>() where T : class, global::UnityEngine.ISubsystem
		{
			GetLoadedSubsystem<T>()?.Stop();
		}

		protected void DestroySubsystem<T>() where T : class, global::UnityEngine.ISubsystem
		{
			T loadedSubsystem = GetLoadedSubsystem<T>();
			if (loadedSubsystem != null)
			{
				if (loadedSubsystem.running)
				{
					loadedSubsystem.Stop();
				}
				global::System.Type typeFromHandle = typeof(T);
				if (m_SubsystemInstanceMap.ContainsKey(typeFromHandle))
				{
					m_SubsystemInstanceMap.Remove(typeFromHandle);
				}
				loadedSubsystem.Destroy();
			}
		}

		protected void CreateSubsystem<TDescriptor, TSubsystem>(global::System.Collections.Generic.List<TDescriptor> descriptors, string id) where TDescriptor : global::UnityEngine.ISubsystemDescriptor where TSubsystem : global::UnityEngine.ISubsystem
		{
			if (descriptors == null)
			{
				throw new global::System.ArgumentNullException("descriptors");
			}
			global::UnityEngine.SubsystemManager.GetSubsystemDescriptors(descriptors);
			if (descriptors.Count <= 0)
			{
				return;
			}
			foreach (TDescriptor descriptor in descriptors)
			{
				global::UnityEngine.ISubsystem subsystem = null;
				if (string.Compare(descriptor.id, id, ignoreCase: true) == 0)
				{
					subsystem = descriptor.Create();
				}
				if (subsystem != null)
				{
					m_SubsystemInstanceMap[typeof(TSubsystem)] = subsystem;
					break;
				}
			}
		}

		public override bool Deinitialize()
		{
			m_SubsystemInstanceMap.Clear();
			return base.Deinitialize();
		}
	}
}

namespace UnityEngine.AdaptivePerformance
{
	public abstract class AdaptivePerformanceLoader : global::UnityEngine.ScriptableObject
	{
		public abstract bool Initialized { get; }

		public abstract bool Running { get; }

		public virtual bool Initialize()
		{
			return true;
		}

		public virtual bool Start()
		{
			return true;
		}

		public virtual bool Stop()
		{
			return true;
		}

		public virtual bool Deinitialize()
		{
			return true;
		}

		public abstract T GetLoadedSubsystem<T>() where T : class, global::UnityEngine.ISubsystem;

		public abstract global::UnityEngine.ISubsystem GetDefaultSubsystem();

		public abstract global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings GetSettings();
	}
}

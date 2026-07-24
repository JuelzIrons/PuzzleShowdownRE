namespace UnityEngine.Rendering
{
	public abstract class DebugDisplaySettings<T> : global::UnityEngine.Rendering.IDebugDisplaySettings where T : global::UnityEngine.Rendering.IDebugDisplaySettings, new()
	{
		private class IDebugDisplaySettingsDataComparer : global::System.Collections.Generic.IEqualityComparer<global::UnityEngine.Rendering.IDebugDisplaySettingsData>
		{
			public bool Equals(global::UnityEngine.Rendering.IDebugDisplaySettingsData x, global::UnityEngine.Rendering.IDebugDisplaySettingsData y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null || y == null)
				{
					return false;
				}
				return x.GetType() == y.GetType();
			}

			public int GetHashCode(global::UnityEngine.Rendering.IDebugDisplaySettingsData obj)
			{
				return 17 * 23 + obj.GetType().GetHashCode();
			}
		}

		protected readonly global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.IDebugDisplaySettingsData> m_Settings = new global::System.Collections.Generic.HashSet<global::UnityEngine.Rendering.IDebugDisplaySettingsData>(new global::UnityEngine.Rendering.DebugDisplaySettings<T>.IDebugDisplaySettingsDataComparer());

		private static readonly global::System.Lazy<T> s_Instance = new global::System.Lazy<T>(delegate
		{
			T result = new T();
			result.Reset();
			return result;
		});

		public static T Instance => s_Instance.Value;

		public virtual bool AreAnySettingsActive
		{
			get
			{
				foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
				{
					if (setting.AreAnySettingsActive)
					{
						return true;
					}
				}
				return false;
			}
		}

		public virtual bool IsPostProcessingAllowed
		{
			get
			{
				bool flag = true;
				foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
				{
					flag &= setting.IsPostProcessingAllowed;
				}
				return flag;
			}
		}

		public virtual bool IsLightingActive
		{
			get
			{
				bool flag = true;
				foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
				{
					flag &= setting.IsLightingActive;
				}
				return flag;
			}
		}

		protected TData Add<TData>(TData newData) where TData : global::UnityEngine.Rendering.IDebugDisplaySettingsData
		{
			m_Settings.Add(newData);
			return newData;
		}

		global::UnityEngine.Rendering.IDebugDisplaySettingsData global::UnityEngine.Rendering.IDebugDisplaySettings.Add(global::UnityEngine.Rendering.IDebugDisplaySettingsData newData)
		{
			m_Settings.Add(newData);
			return newData;
		}

		public void ForEach(global::System.Action<global::UnityEngine.Rendering.IDebugDisplaySettingsData> onExecute)
		{
			foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
			{
				onExecute(setting);
			}
		}

		public virtual void Reset()
		{
			foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
			{
				setting.Reset();
			}
			m_Settings.Clear();
		}

		public virtual bool TryGetScreenClearColor(ref global::UnityEngine.Color color)
		{
			foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsData setting in m_Settings)
			{
				if (setting.TryGetScreenClearColor(ref color))
				{
					return true;
				}
			}
			return false;
		}
	}
}

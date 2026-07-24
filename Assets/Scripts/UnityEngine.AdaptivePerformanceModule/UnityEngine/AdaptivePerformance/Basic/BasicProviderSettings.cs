namespace UnityEngine.AdaptivePerformance.Basic
{
	[global::System.Serializable]
	[global::UnityEngine.AdaptivePerformance.AdaptivePerformanceConfigurationData("Basic", "com.unity.adaptivePerformance.basic.provider_settings")]
	public class BasicProviderSettings : global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings
	{
		private static global::UnityEngine.AdaptivePerformance.Basic.BasicProviderSettings m_Instance;

		private void Awake()
		{
			m_Instance = this;
		}

		internal static global::UnityEngine.AdaptivePerformance.Basic.BasicProviderSettings GetSettings()
		{
			return m_Instance;
		}
	}
}

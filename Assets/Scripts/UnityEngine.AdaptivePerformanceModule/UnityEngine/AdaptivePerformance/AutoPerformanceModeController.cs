namespace UnityEngine.AdaptivePerformance
{
	internal class AutoPerformanceModeController
	{
		private string m_FeatureName = "Auto Performance Mode Control";

		public AutoPerformanceModeController(global::UnityEngine.AdaptivePerformance.IPerformanceModeStatus perfModeStat)
		{
			perfModeStat.PerformanceModeEvent += delegate(global::UnityEngine.AdaptivePerformance.PerformanceMode mode)
			{
				OnPerformanceModeChange(mode);
			};
		}

		private void OnPerformanceModeChange(global::UnityEngine.AdaptivePerformance.PerformanceMode performanceMode)
		{
			switch (performanceMode)
			{
			case global::UnityEngine.AdaptivePerformance.PerformanceMode.Battery:
				global::UnityEngine.Application.targetFrameRate = 30;
				break;
			case global::UnityEngine.AdaptivePerformance.PerformanceMode.Optimize:
				global::UnityEngine.Application.targetFrameRate = (int)global::UnityEngine.Screen.currentResolution.refreshRateRatio.value;
				break;
			default:
				global::UnityEngine.Application.targetFrameRate = -1;
				break;
			}
			global::UnityEngine.AdaptivePerformance.APLog.Debug($"[AutoPerformanceModeController] Performance Mode: {performanceMode}, fps: {(global::UnityEngine.Application.targetFrameRate)}");
		}
	}
}

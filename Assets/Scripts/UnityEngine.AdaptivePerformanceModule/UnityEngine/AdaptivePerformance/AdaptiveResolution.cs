namespace UnityEngine.AdaptivePerformance
{
	public class AdaptiveResolution : global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScaler
	{
		private static int instanceCount;

		protected override void Awake()
		{
			base.Awake();
			if (!(m_Settings == null))
			{
				ApplyDefaultSetting(m_Settings.scalerSettings.AdaptiveResolution);
			}
		}

		protected override void OnDisabled()
		{
			OnDestroy();
		}

		protected override void OnEnabled()
		{
		}

		private void OnValidate()
		{
			if (MaxLevel < 1)
			{
				MaxLevel = 1;
			}
			MaxBound = global::UnityEngine.Mathf.Clamp(MaxBound, 0.25f, 1f);
			MinBound = global::UnityEngine.Mathf.Clamp(MinBound, 0.25f, MaxBound);
		}

		private bool IsDynamicResolutionSupported()
		{
			return true;
		}

		private void Start()
		{
			instanceCount++;
			if (instanceCount > 1)
			{
				global::UnityEngine.Debug.LogWarning("Multiple Adaptive Resolution scalers created. They will interfere with each other.");
			}
			if (!IsDynamicResolutionSupported())
			{
				global::UnityEngine.Debug.Log($"Dynamic resolution is not supported. Will be using fallback to Render Scale Multiplier.");
			}
		}

		private void OnDestroy()
		{
			instanceCount--;
			if (Scale != 1f)
			{
				global::UnityEngine.AdaptivePerformance.APLog.Debug("Restoring dynamic resolution scale factor to 1.0");
				if (IsDynamicResolutionSupported())
				{
					global::UnityEngine.ScalableBufferManager.ResizeBuffers(1f, 1f);
				}
				else
				{
					global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.RenderScaleMultiplier = 1f;
				}
			}
		}

		protected override void OnLevel()
		{
			bool flag = ScaleChanged();
			if (IsDynamicResolutionSupported())
			{
				if (flag)
				{
					global::UnityEngine.ScalableBufferManager.ResizeBuffers(Scale, Scale);
				}
				int num = (int)global::UnityEngine.Mathf.Ceil(global::UnityEngine.ScalableBufferManager.widthScaleFactor * (float)global::UnityEngine.Screen.currentResolution.width);
				int num2 = (int)global::UnityEngine.Mathf.Ceil(global::UnityEngine.ScalableBufferManager.heightScaleFactor * (float)global::UnityEngine.Screen.currentResolution.height);
				global::UnityEngine.AdaptivePerformance.APLog.Debug($"Adaptive Resolution Scale: {Scale:F3} Resolution: {num}x{num2} ScaleFactor: {(global::UnityEngine.ScalableBufferManager.widthScaleFactor):F3}x{(global::UnityEngine.ScalableBufferManager.heightScaleFactor):F3} Level:{base.CurrentLevel}/{MaxLevel}");
			}
			else
			{
				global::UnityEngine.AdaptivePerformance.AdaptivePerformanceRenderSettings.RenderScaleMultiplier = Scale;
				global::UnityEngine.AdaptivePerformance.APLog.Debug($"Dynamic resolution is not supported. Using fallback to Render Scale Multiplier : {Scale:F3}");
			}
		}
	}
}

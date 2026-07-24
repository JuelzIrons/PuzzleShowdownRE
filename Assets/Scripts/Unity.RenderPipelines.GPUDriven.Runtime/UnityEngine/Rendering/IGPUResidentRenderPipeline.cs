namespace UnityEngine.Rendering
{
	public interface IGPUResidentRenderPipeline
	{
		global::UnityEngine.Rendering.GPUResidentDrawerSettings gpuResidentDrawerSettings { get; }

		global::UnityEngine.Rendering.GPUResidentDrawerMode gpuResidentDrawerMode { get; set; }

		static void ReinitializeGPUResidentDrawer()
		{
			global::UnityEngine.Rendering.GPUResidentDrawer.Reinitialize();
		}

		bool IsGPUResidentDrawerSupportedBySRP(bool logReason = false)
		{
			string message;
			global::UnityEngine.LogType severity;
			bool flag = IsGPUResidentDrawerSupportedBySRP(out message, out severity);
			if (logReason && !flag)
			{
				global::UnityEngine.Rendering.GPUResidentDrawer.LogMessage(message, severity);
			}
			return flag;
		}

		bool IsGPUResidentDrawerSupportedBySRP(out string message, out global::UnityEngine.LogType severity)
		{
			message = string.Empty;
			severity = global::UnityEngine.LogType.Log;
			return true;
		}

		static bool IsGPUResidentDrawerSupportedByProjectConfiguration(bool logReason = false)
		{
			string message;
			global::UnityEngine.LogType severity;
			bool result = global::UnityEngine.Rendering.GPUResidentDrawer.IsProjectSupported(out message, out severity);
			if (logReason && !string.IsNullOrEmpty(message))
			{
				global::UnityEngine.Debug.LogWarning(message);
			}
			return result;
		}

		static bool IsGPUResidentDrawerEnabled()
		{
			return global::UnityEngine.Rendering.GPUResidentDrawer.IsEnabled();
		}
	}
}

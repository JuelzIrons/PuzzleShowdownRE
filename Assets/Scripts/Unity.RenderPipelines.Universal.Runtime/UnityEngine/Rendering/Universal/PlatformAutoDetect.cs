namespace UnityEngine.Rendering.Universal
{
	internal static class PlatformAutoDetect
	{
		internal static bool isRunningOnPowerVRGPU = global::UnityEngine.SystemInfo.graphicsDeviceName.Contains("PowerVR");

		internal static bool isXRMobile { get; private set; } = false;

		internal static bool isShaderAPIMobileDefined { get; private set; } = false;

		internal static bool isSwitch { get; private set; } = false;

		internal static bool isSwitch2 { get; private set; } = false;

		internal static void Initialize()
		{
			isXRMobile = false;
			isShaderAPIMobileDefined = global::UnityEngine.Rendering.GraphicsSettings.HasShaderDefine(global::UnityEngine.Rendering.BuiltinShaderDefine.SHADER_API_MOBILE);
			isSwitch = global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Switch;
			isSwitch2 = global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Switch2;
		}

		internal static global::UnityEngine.Rendering.Universal.ShEvalMode ShAutoDetect(global::UnityEngine.Rendering.Universal.ShEvalMode mode)
		{
			if (mode == global::UnityEngine.Rendering.Universal.ShEvalMode.Auto)
			{
				if (isXRMobile || isShaderAPIMobileDefined || isSwitch || isSwitch2)
				{
					return global::UnityEngine.Rendering.Universal.ShEvalMode.PerVertex;
				}
				return global::UnityEngine.Rendering.Universal.ShEvalMode.PerPixel;
			}
			return mode;
		}
	}
}

namespace UnityEngine.Rendering
{
	public static class FSRUtils
	{
		private static class ShaderConstants
		{
			public static readonly int _FsrEasuConstants0 = global::UnityEngine.Shader.PropertyToID("_FsrEasuConstants0");

			public static readonly int _FsrEasuConstants1 = global::UnityEngine.Shader.PropertyToID("_FsrEasuConstants1");

			public static readonly int _FsrEasuConstants2 = global::UnityEngine.Shader.PropertyToID("_FsrEasuConstants2");

			public static readonly int _FsrEasuConstants3 = global::UnityEngine.Shader.PropertyToID("_FsrEasuConstants3");

			public static readonly int _FsrRcasConstants = global::UnityEngine.Shader.PropertyToID("_FsrRcasConstants");
		}

		internal const float kMaxSharpnessStops = 2.5f;

		public const float kDefaultSharpnessStops = 0.2f;

		public const float kDefaultSharpnessLinear = 0.92f;

		public static void SetEasuConstants(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Vector2 inputViewportSizeInPixels, global::UnityEngine.Vector2 inputImageSizeInPixels, global::UnityEngine.Vector2 outputImageSizeInPixels)
		{
			global::UnityEngine.Vector4 value = default(global::UnityEngine.Vector4);
			value.x = inputViewportSizeInPixels.x / outputImageSizeInPixels.x;
			value.y = inputViewportSizeInPixels.y / outputImageSizeInPixels.y;
			value.z = 0.5f * inputViewportSizeInPixels.x / outputImageSizeInPixels.x - 0.5f;
			value.w = 0.5f * inputViewportSizeInPixels.y / outputImageSizeInPixels.y - 0.5f;
			global::UnityEngine.Vector4 value2 = default(global::UnityEngine.Vector4);
			value2.x = 1f / inputImageSizeInPixels.x;
			value2.y = 1f / inputImageSizeInPixels.y;
			value2.z = 1f / inputImageSizeInPixels.x;
			value2.w = -1f / inputImageSizeInPixels.y;
			global::UnityEngine.Vector4 value3 = default(global::UnityEngine.Vector4);
			value3.x = -1f / inputImageSizeInPixels.x;
			value3.y = 2f / inputImageSizeInPixels.y;
			value3.z = 1f / inputImageSizeInPixels.x;
			value3.w = 2f / inputImageSizeInPixels.y;
			global::UnityEngine.Vector4 value4 = default(global::UnityEngine.Vector4);
			value4.x = 0f / inputImageSizeInPixels.x;
			value4.y = 4f / inputImageSizeInPixels.y;
			value4.z = 0f;
			value4.w = 0f;
			cmd.SetGlobalVector(global::UnityEngine.Rendering.FSRUtils.ShaderConstants._FsrEasuConstants0, value);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.FSRUtils.ShaderConstants._FsrEasuConstants1, value2);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.FSRUtils.ShaderConstants._FsrEasuConstants2, value3);
			cmd.SetGlobalVector(global::UnityEngine.Rendering.FSRUtils.ShaderConstants._FsrEasuConstants3, value4);
		}

		public static void SetEasuConstants(global::UnityEngine.Rendering.BaseCommandBuffer cmd, global::UnityEngine.Vector2 inputViewportSizeInPixels, global::UnityEngine.Vector2 inputImageSizeInPixels, global::UnityEngine.Vector2 outputImageSizeInPixels)
		{
			SetEasuConstants(cmd.m_WrappedCommandBuffer, inputViewportSizeInPixels, inputImageSizeInPixels, outputImageSizeInPixels);
		}

		public static void SetRcasConstants(global::UnityEngine.Rendering.CommandBuffer cmd, float sharpnessStops = 0.2f)
		{
			float num = global::UnityEngine.Mathf.Pow(2f, 0f - sharpnessStops);
			ushort num2 = global::UnityEngine.Mathf.FloatToHalf(num);
			float y = global::System.BitConverter.Int32BitsToSingle(num2 | (num2 << 16));
			global::UnityEngine.Vector4 value = default(global::UnityEngine.Vector4);
			value.x = num;
			value.y = y;
			value.z = 0f;
			value.w = 0f;
			cmd.SetGlobalVector(global::UnityEngine.Rendering.FSRUtils.ShaderConstants._FsrRcasConstants, value);
		}

		public static void SetRcasConstants(global::UnityEngine.Rendering.BaseCommandBuffer cmd, float sharpnessStops = 0.2f)
		{
			SetRcasConstants(cmd.m_WrappedCommandBuffer, sharpnessStops);
		}

		public static void SetRcasConstantsLinear(global::UnityEngine.Rendering.CommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			float sharpnessStops = (1f - sharpnessLinear) * 2.5f;
			SetRcasConstants(cmd, sharpnessStops);
		}

		public static void SetRcasConstantsLinear(global::UnityEngine.Rendering.RasterCommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			SetRcasConstantsLinear(cmd.m_WrappedCommandBuffer, sharpnessLinear);
		}

		public static bool IsSupported()
		{
			return global::UnityEngine.SystemInfo.graphicsShaderLevel >= 45;
		}
	}
}

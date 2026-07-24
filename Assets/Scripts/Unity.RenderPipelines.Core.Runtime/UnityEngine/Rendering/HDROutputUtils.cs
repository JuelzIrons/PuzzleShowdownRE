namespace UnityEngine.Rendering
{
	public static class HDROutputUtils
	{
		[global::System.Flags]
		public enum Operation
		{
			None = 0,
			ColorConversion = 1,
			ColorEncoding = 2
		}

		public struct HDRDisplayInformation
		{
			public int maxFullFrameToneMapLuminance;

			public int maxToneMapLuminance;

			public int minToneMapLuminance;

			public float paperWhiteNits;

			public HDRDisplayInformation(int maxFullFrameToneMapLuminance, int maxToneMapLuminance, int minToneMapLuminance, float hdrPaperWhiteNits)
			{
				this.maxFullFrameToneMapLuminance = maxFullFrameToneMapLuminance;
				this.maxToneMapLuminance = maxToneMapLuminance;
				this.minToneMapLuminance = minToneMapLuminance;
				paperWhiteNits = hdrPaperWhiteNits;
			}
		}

		public static class ShaderKeywords
		{
			public const string HDR_COLORSPACE_CONVERSION = "HDR_COLORSPACE_CONVERSION";

			public const string HDR_ENCODING = "HDR_ENCODING";

			public const string HDR_COLORSPACE_CONVERSION_AND_ENCODING = "HDR_COLORSPACE_CONVERSION_AND_ENCODING";

			public const string HDR_INPUT = "HDR_INPUT";

			internal static readonly global::UnityEngine.Rendering.ShaderKeyword HDRColorSpaceConversion = new global::UnityEngine.Rendering.ShaderKeyword("HDR_COLORSPACE_CONVERSION");

			internal static readonly global::UnityEngine.Rendering.ShaderKeyword HDREncoding = new global::UnityEngine.Rendering.ShaderKeyword("HDR_ENCODING");

			internal static readonly global::UnityEngine.Rendering.ShaderKeyword HDRColorSpaceConversionAndEncoding = new global::UnityEngine.Rendering.ShaderKeyword("HDR_COLORSPACE_CONVERSION_AND_ENCODING");

			internal static readonly global::UnityEngine.Rendering.ShaderKeyword HDRInput = new global::UnityEngine.Rendering.ShaderKeyword("HDR_INPUT");
		}

		private static class ShaderPropertyId
		{
			public static readonly int hdrColorSpace = global::UnityEngine.Shader.PropertyToID("_HDRColorspace");

			public static readonly int hdrEncoding = global::UnityEngine.Shader.PropertyToID("_HDREncoding");
		}

		public static bool GetColorSpaceForGamut(global::UnityEngine.ColorGamut gamut, out int colorspace)
		{
			if (global::UnityEngine.ColorGamutUtility.GetWhitePoint(gamut) != global::UnityEngine.WhitePoint.D65)
			{
				global::UnityEngine.Debug.LogWarningFormat("{0} white point is currently unsupported for outputting to HDR.", gamut.ToString());
				colorspace = -1;
				return false;
			}
			switch (global::UnityEngine.ColorGamutUtility.GetColorPrimaries(gamut))
			{
			case global::UnityEngine.ColorPrimaries.Rec709:
				colorspace = 0;
				return true;
			case global::UnityEngine.ColorPrimaries.Rec2020:
				colorspace = 1;
				return true;
			case global::UnityEngine.ColorPrimaries.P3:
				colorspace = 2;
				return true;
			default:
				global::UnityEngine.Debug.LogWarningFormat("{0} color space is currently unsupported for outputting to HDR.", gamut.ToString());
				colorspace = -1;
				return false;
			}
		}

		public static bool GetColorEncodingForGamut(global::UnityEngine.ColorGamut gamut, out int encoding)
		{
			switch (global::UnityEngine.ColorGamutUtility.GetTransferFunction(gamut))
			{
			case global::UnityEngine.TransferFunction.Linear:
				encoding = 3;
				return true;
			case global::UnityEngine.TransferFunction.PQ:
				encoding = 2;
				return true;
			case global::UnityEngine.TransferFunction.Gamma22:
				encoding = 4;
				return true;
			case global::UnityEngine.TransferFunction.sRGB:
				encoding = 0;
				return true;
			default:
				global::UnityEngine.Debug.LogWarningFormat("{0} color encoding is currently unsupported for outputting to HDR.", gamut.ToString());
				encoding = -1;
				return false;
			}
		}

		public static void ConfigureHDROutput(global::UnityEngine.Material material, global::UnityEngine.ColorGamut gamut, global::UnityEngine.Rendering.HDROutputUtils.Operation operations)
		{
			if (GetColorSpaceForGamut(gamut, out var colorspace) && GetColorEncodingForGamut(gamut, out var encoding))
			{
				material.SetInteger(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorspace);
				material.SetInteger(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRInput.name, operations == global::UnityEngine.Rendering.HDROutputUtils.Operation.None);
			}
		}

		public static void ConfigureHDROutput(global::UnityEngine.MaterialPropertyBlock properties, global::UnityEngine.ColorGamut gamut)
		{
			if (GetColorSpaceForGamut(gamut, out var colorspace) && GetColorEncodingForGamut(gamut, out var encoding))
			{
				properties.SetInteger(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorspace);
				properties.SetInteger(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
			}
		}

		public static void ConfigureHDROutput(global::UnityEngine.Material material, global::UnityEngine.Rendering.HDROutputUtils.Operation operations)
		{
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion));
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
			global::UnityEngine.Rendering.CoreUtils.SetKeyword(material, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRInput.name, operations == global::UnityEngine.Rendering.HDROutputUtils.Operation.None);
		}

		public static void ConfigureHDROutput(global::UnityEngine.ComputeShader computeShader, global::UnityEngine.ColorGamut gamut, global::UnityEngine.Rendering.HDROutputUtils.Operation operations)
		{
			if (GetColorSpaceForGamut(gamut, out var colorspace) && GetColorEncodingForGamut(gamut, out var encoding))
			{
				computeShader.SetInt(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrColorSpace, colorspace);
				computeShader.SetInt(global::UnityEngine.Rendering.HDROutputUtils.ShaderPropertyId.hdrEncoding, encoding);
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(computeShader, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(computeShader, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDREncoding.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(computeShader, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion.name, operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorConversion) && !operations.HasFlag(global::UnityEngine.Rendering.HDROutputUtils.Operation.ColorEncoding));
				global::UnityEngine.Rendering.CoreUtils.SetKeyword(computeShader, global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRInput.name, operations == global::UnityEngine.Rendering.HDROutputUtils.Operation.None);
			}
		}

		public static bool IsShaderVariantValid(global::UnityEngine.Rendering.ShaderKeywordSet shaderKeywordSet, bool isHDREnabled)
		{
			bool flag = shaderKeywordSet.IsEnabled(global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDREncoding) || shaderKeywordSet.IsEnabled(global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversion) || shaderKeywordSet.IsEnabled(global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRColorSpaceConversionAndEncoding) || shaderKeywordSet.IsEnabled(global::UnityEngine.Rendering.HDROutputUtils.ShaderKeywords.HDRInput);
			if (!isHDREnabled && flag)
			{
				return false;
			}
			return true;
		}
	}
}

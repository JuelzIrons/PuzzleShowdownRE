namespace UnityEngine.Rendering
{
	public static class ColorUtils
	{
		public static float s_LightMeterCalibrationConstant = 12.5f;

		public static float s_LensAttenuation = 0.65f;

		public static float lensImperfectionExposureScale => 78f / (100f * s_LensAttenuation);

		public static float StandardIlluminantY(float x)
		{
			return 2.87f * x - 3f * x * x - 0.27509508f;
		}

		public static global::UnityEngine.Vector3 CIExyToLMS(float x, float y)
		{
			float num = 1f;
			float num2 = num * x / y;
			float num3 = num * (1f - x - y) / y;
			float x2 = 0.7328f * num2 + 0.4296f * num - 0.1624f * num3;
			float y2 = -0.7036f * num2 + 1.6975f * num + 0.0061f * num3;
			float z = 0.003f * num2 + 0.0136f * num + 0.9834f * num3;
			return new global::UnityEngine.Vector3(x2, y2, z);
		}

		public static global::UnityEngine.Vector3 ColorBalanceToLMSCoeffs(float temperature, float tint)
		{
			float num = temperature / 65f;
			float num2 = tint / 65f;
			float x = 0.31271f - num * ((num < 0f) ? 0.1f : 0.05f);
			float y = StandardIlluminantY(x) + num2 * 0.05f;
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(0.949237f, 1.03542f, 1.08728f);
			global::UnityEngine.Vector3 vector2 = CIExyToLMS(x, y);
			return new global::UnityEngine.Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		}

		public static (global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::UnityEngine.Vector4) PrepareShadowsMidtonesHighlights(in global::UnityEngine.Vector4 inShadows, in global::UnityEngine.Vector4 inMidtones, in global::UnityEngine.Vector4 inHighlights)
		{
			global::UnityEngine.Vector4 item = inShadows;
			item.x = global::UnityEngine.Mathf.GammaToLinearSpace(item.x);
			item.y = global::UnityEngine.Mathf.GammaToLinearSpace(item.y);
			item.z = global::UnityEngine.Mathf.GammaToLinearSpace(item.z);
			float num = item.w * ((global::UnityEngine.Mathf.Sign(item.w) < 0f) ? 1f : 4f);
			item.x = global::UnityEngine.Mathf.Max(item.x + num, 0f);
			item.y = global::UnityEngine.Mathf.Max(item.y + num, 0f);
			item.z = global::UnityEngine.Mathf.Max(item.z + num, 0f);
			item.w = 0f;
			global::UnityEngine.Vector4 item2 = inMidtones;
			item2.x = global::UnityEngine.Mathf.GammaToLinearSpace(item2.x);
			item2.y = global::UnityEngine.Mathf.GammaToLinearSpace(item2.y);
			item2.z = global::UnityEngine.Mathf.GammaToLinearSpace(item2.z);
			num = item2.w * ((global::UnityEngine.Mathf.Sign(item2.w) < 0f) ? 1f : 4f);
			item2.x = global::UnityEngine.Mathf.Max(item2.x + num, 0f);
			item2.y = global::UnityEngine.Mathf.Max(item2.y + num, 0f);
			item2.z = global::UnityEngine.Mathf.Max(item2.z + num, 0f);
			item2.w = 0f;
			global::UnityEngine.Vector4 item3 = inHighlights;
			item3.x = global::UnityEngine.Mathf.GammaToLinearSpace(item3.x);
			item3.y = global::UnityEngine.Mathf.GammaToLinearSpace(item3.y);
			item3.z = global::UnityEngine.Mathf.GammaToLinearSpace(item3.z);
			num = item3.w * ((global::UnityEngine.Mathf.Sign(item3.w) < 0f) ? 1f : 4f);
			item3.x = global::UnityEngine.Mathf.Max(item3.x + num, 0f);
			item3.y = global::UnityEngine.Mathf.Max(item3.y + num, 0f);
			item3.z = global::UnityEngine.Mathf.Max(item3.z + num, 0f);
			item3.w = 0f;
			return (item, item2, item3);
		}

		public static (global::UnityEngine.Vector4, global::UnityEngine.Vector4, global::UnityEngine.Vector4) PrepareLiftGammaGain(in global::UnityEngine.Vector4 inLift, in global::UnityEngine.Vector4 inGamma, in global::UnityEngine.Vector4 inGain)
		{
			global::UnityEngine.Vector4 vector = inLift;
			vector.x = global::UnityEngine.Mathf.GammaToLinearSpace(vector.x) * 0.15f;
			vector.y = global::UnityEngine.Mathf.GammaToLinearSpace(vector.y) * 0.15f;
			vector.z = global::UnityEngine.Mathf.GammaToLinearSpace(vector.z) * 0.15f;
			float num = Luminance((global::UnityEngine.Color)vector);
			vector.x = vector.x - num + vector.w;
			vector.y = vector.y - num + vector.w;
			vector.z = vector.z - num + vector.w;
			vector.w = 0f;
			global::UnityEngine.Vector4 vector2 = inGamma;
			vector2.x = global::UnityEngine.Mathf.GammaToLinearSpace(vector2.x) * 0.8f;
			vector2.y = global::UnityEngine.Mathf.GammaToLinearSpace(vector2.y) * 0.8f;
			vector2.z = global::UnityEngine.Mathf.GammaToLinearSpace(vector2.z) * 0.8f;
			float num2 = Luminance((global::UnityEngine.Color)vector2);
			vector2.w += 1f;
			vector2.x = 1f / global::UnityEngine.Mathf.Max(vector2.x - num2 + vector2.w, 0.001f);
			vector2.y = 1f / global::UnityEngine.Mathf.Max(vector2.y - num2 + vector2.w, 0.001f);
			vector2.z = 1f / global::UnityEngine.Mathf.Max(vector2.z - num2 + vector2.w, 0.001f);
			vector2.w = 0f;
			global::UnityEngine.Vector4 vector3 = inGain;
			vector3.x = global::UnityEngine.Mathf.GammaToLinearSpace(vector3.x) * 0.8f;
			vector3.y = global::UnityEngine.Mathf.GammaToLinearSpace(vector3.y) * 0.8f;
			vector3.z = global::UnityEngine.Mathf.GammaToLinearSpace(vector3.z) * 0.8f;
			float num3 = Luminance((global::UnityEngine.Color)vector3);
			vector3.w += 1f;
			vector3.x = vector3.x - num3 + vector3.w;
			vector3.y = vector3.y - num3 + vector3.w;
			vector3.z = vector3.z - num3 + vector3.w;
			vector3.w = 0f;
			return (vector, vector2, vector3);
		}

		public static (global::UnityEngine.Vector4, global::UnityEngine.Vector4) PrepareSplitToning(in global::UnityEngine.Vector4 inShadows, in global::UnityEngine.Vector4 inHighlights, float balance)
		{
			global::UnityEngine.Vector4 item = inShadows;
			global::UnityEngine.Vector4 item2 = inHighlights;
			item.w = balance / 100f;
			item2.w = 0f;
			return (item, item2);
		}

		public static float Luminance(in global::UnityEngine.Color color)
		{
			return color.r * 0.2126729f + color.g * 0.7151522f + color.b * 0.072175f;
		}

		public static float ComputeEV100(float aperture, float shutterSpeed, float ISO)
		{
			return global::UnityEngine.Mathf.Log(aperture * aperture / shutterSpeed * 100f / ISO, 2f);
		}

		public static float ConvertEV100ToExposure(float EV100)
		{
			float num = lensImperfectionExposureScale * global::UnityEngine.Mathf.Pow(2f, EV100);
			return 1f / num;
		}

		public static float ConvertExposureToEV100(float exposure)
		{
			return global::UnityEngine.Mathf.Log(1f / (lensImperfectionExposureScale * exposure), 2f);
		}

		public static float ComputeEV100FromAvgLuminance(float avgLuminance)
		{
			float num = s_LightMeterCalibrationConstant;
			return global::UnityEngine.Mathf.Log(avgLuminance * 100f / num, 2f);
		}

		public static float ComputeISO(float aperture, float shutterSpeed, float targetEV100)
		{
			return aperture * aperture * 100f / (shutterSpeed * global::UnityEngine.Mathf.Pow(2f, targetEV100));
		}

		public static uint ToHex(global::UnityEngine.Color c)
		{
			return ((uint)(c.a * 255f) << 24) | ((uint)(c.r * 255f) << 16) | ((uint)(c.g * 255f) << 8) | (uint)(c.b * 255f);
		}

		public static global::UnityEngine.Color ToRGBA(uint hex)
		{
			return new global::UnityEngine.Color((float)((hex >> 16) & 0xFF) / 255f, (float)((hex >> 8) & 0xFF) / 255f, (float)(hex & 0xFF) / 255f, (float)((hex >> 24) & 0xFF) / 255f);
		}
	}
}

namespace UnityEngine.Rendering
{
	public static class LightUnitUtils
	{
		public const float SphereSolidAngle = global::System.MathF.PI * 4f;

		private static float k_LuminanceToEvFactor => global::UnityEngine.Mathf.Log(100f / global::UnityEngine.Rendering.ColorUtils.s_LightMeterCalibrationConstant, 2f);

		private static float k_EvToLuminanceFactor => 0f - k_LuminanceToEvFactor;

		public static global::UnityEngine.Rendering.LightUnit GetNativeLightUnit(global::UnityEngine.LightType lightType)
		{
			switch (lightType)
			{
			case global::UnityEngine.LightType.Spot:
			case global::UnityEngine.LightType.Point:
			case global::UnityEngine.LightType.Pyramid:
				return global::UnityEngine.Rendering.LightUnit.Candela;
			case global::UnityEngine.LightType.Directional:
			case global::UnityEngine.LightType.Box:
				return global::UnityEngine.Rendering.LightUnit.Lux;
			case global::UnityEngine.LightType.Area:
			case global::UnityEngine.LightType.Disc:
			case global::UnityEngine.LightType.Tube:
				return global::UnityEngine.Rendering.LightUnit.Nits;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			}
		}

		public static bool IsLightUnitSupported(global::UnityEngine.LightType lightType, global::UnityEngine.Rendering.LightUnit lightUnit)
		{
			int num = 1 << (int)lightUnit;
			switch (lightType)
			{
			case global::UnityEngine.LightType.Spot:
			case global::UnityEngine.LightType.Point:
			case global::UnityEngine.LightType.Pyramid:
				return (num & 0x17) > 0;
			case global::UnityEngine.LightType.Directional:
			case global::UnityEngine.LightType.Box:
				return (num & 4) > 0;
			case global::UnityEngine.LightType.Area:
			case global::UnityEngine.LightType.Disc:
			case global::UnityEngine.LightType.Tube:
				return (num & 0x19) > 0;
			default:
				return false;
			}
		}

		public static float GetSolidAngleFromPointLight()
		{
			return global::System.MathF.PI * 4f;
		}

		public static float GetSolidAngleFromSpotLight(float spotAngle)
		{
			double num = global::System.Math.PI * (double)spotAngle / 180.0;
			return (float)(global::System.Math.PI * 2.0 * (1.0 - global::System.Math.Cos(num * 0.5)));
		}

		public static float GetSolidAngleFromPyramidLight(float spotAngle, float aspectRatio)
		{
			if (aspectRatio < 1f)
			{
				aspectRatio = (float)(1.0 / (double)aspectRatio);
			}
			double num = global::System.Math.PI * (double)spotAngle / 180.0;
			double num2 = global::System.Math.Atan(global::System.Math.Tan(0.5 * num) * (double)aspectRatio) * 2.0;
			return (float)(4.0 * global::System.Math.Asin(global::System.Math.Sin(num * 0.5) * global::System.Math.Sin(num2 * 0.5)));
		}

		internal static float GetSolidAngle(global::UnityEngine.LightType lightType, bool spotReflector, float spotAngle, float aspectRatio)
		{
			return lightType switch
			{
				global::UnityEngine.LightType.Spot => spotReflector ? GetSolidAngleFromSpotLight(spotAngle) : (global::System.MathF.PI * 4f), 
				global::UnityEngine.LightType.Pyramid => spotReflector ? GetSolidAngleFromPyramidLight(spotAngle, aspectRatio) : (global::System.MathF.PI * 4f), 
				global::UnityEngine.LightType.Point => GetSolidAngleFromPointLight(), 
				_ => throw new global::System.ArgumentException("Solid angle is undefined for lights of type " + lightType), 
			};
		}

		public static float GetAreaFromRectangleLight(float rectSizeX, float rectSizeY)
		{
			return global::UnityEngine.Mathf.Abs(rectSizeX * rectSizeY) * global::System.MathF.PI;
		}

		public static float GetAreaFromRectangleLight(global::UnityEngine.Vector2 rectSize)
		{
			return GetAreaFromRectangleLight(rectSize.x, rectSize.y);
		}

		public static float GetAreaFromDiscLight(float discRadius)
		{
			return discRadius * discRadius * global::System.MathF.PI * global::System.MathF.PI;
		}

		public static float GetAreaFromTubeLight(float tubeLength)
		{
			return global::UnityEngine.Mathf.Abs(tubeLength) * 4f * global::System.MathF.PI;
		}

		public static float LumenToCandela(float lumen, float solidAngle)
		{
			return lumen / solidAngle;
		}

		public static float CandelaToLumen(float candela, float solidAngle)
		{
			return candela * solidAngle;
		}

		public static float LumenToNits(float lumen, float area)
		{
			return lumen / area;
		}

		public static float NitsToLumen(float nits, float area)
		{
			return nits * area;
		}

		public static float LuxToCandela(float lux, float distance)
		{
			return lux * (distance * distance);
		}

		public static float CandelaToLux(float candela, float distance)
		{
			return candela / (distance * distance);
		}

		public static float Ev100ToNits(float ev100)
		{
			return global::UnityEngine.Mathf.Pow(2f, ev100 + k_EvToLuminanceFactor);
		}

		public static float NitsToEv100(float nits)
		{
			return global::UnityEngine.Mathf.Log(nits, 2f) + k_LuminanceToEvFactor;
		}

		public static float Ev100ToCandela(float ev100)
		{
			return Ev100ToNits(ev100);
		}

		public static float CandelaToEv100(float candela)
		{
			return NitsToEv100(candela);
		}

		internal static float ConvertIntensityInternal(float intensity, global::UnityEngine.Rendering.LightUnit fromUnit, global::UnityEngine.Rendering.LightUnit toUnit, global::UnityEngine.LightType lightType, float area, float luxAtDistance, float solidAngle)
		{
			if (!IsLightUnitSupported(lightType, fromUnit) || !IsLightUnitSupported(lightType, toUnit))
			{
				throw new global::System.ArgumentException("Converting " + fromUnit.ToString() + " to " + toUnit.ToString() + " is undefined for lights of type " + lightType);
			}
			if (fromUnit == toUnit)
			{
				return intensity;
			}
			switch (fromUnit)
			{
			case global::UnityEngine.Rendering.LightUnit.Lumen:
				switch (toUnit)
				{
				case global::UnityEngine.Rendering.LightUnit.Candela:
					return LumenToCandela(intensity, solidAngle);
				case global::UnityEngine.Rendering.LightUnit.Lux:
					return CandelaToLux(LumenToCandela(intensity, solidAngle), luxAtDistance);
				case global::UnityEngine.Rendering.LightUnit.Nits:
					return LumenToNits(intensity, area);
				case global::UnityEngine.Rendering.LightUnit.Ev100:
				{
					float nits;
					switch (lightType)
					{
					case global::UnityEngine.LightType.Spot:
					case global::UnityEngine.LightType.Point:
					case global::UnityEngine.LightType.Pyramid:
						nits = LumenToCandela(intensity, solidAngle);
						break;
					case global::UnityEngine.LightType.Area:
					case global::UnityEngine.LightType.Disc:
					case global::UnityEngine.LightType.Tube:
						nits = LumenToNits(intensity, area);
						break;
					default:
						throw new global::System.ArgumentException("Converting from Lumen to Ev100 is undefined for light type " + lightType);
					}
					return NitsToEv100(nits);
				}
				default:
					throw new global::System.ArgumentOutOfRangeException("toUnit", toUnit, null);
				}
			case global::UnityEngine.Rendering.LightUnit.Candela:
				return toUnit switch
				{
					global::UnityEngine.Rendering.LightUnit.Lumen => CandelaToLumen(intensity, solidAngle), 
					global::UnityEngine.Rendering.LightUnit.Lux => CandelaToLux(intensity, luxAtDistance), 
					global::UnityEngine.Rendering.LightUnit.Ev100 => NitsToEv100(intensity), 
					_ => throw new global::System.ArgumentOutOfRangeException("toUnit", toUnit, null), 
				};
			case global::UnityEngine.Rendering.LightUnit.Lux:
				return toUnit switch
				{
					global::UnityEngine.Rendering.LightUnit.Lumen => CandelaToLumen(LuxToCandela(intensity, luxAtDistance), solidAngle), 
					global::UnityEngine.Rendering.LightUnit.Candela => LuxToCandela(intensity, luxAtDistance), 
					global::UnityEngine.Rendering.LightUnit.Ev100 => NitsToEv100(LuxToCandela(intensity, luxAtDistance)), 
					_ => throw new global::System.ArgumentOutOfRangeException("toUnit", toUnit, null), 
				};
			case global::UnityEngine.Rendering.LightUnit.Nits:
				return toUnit switch
				{
					global::UnityEngine.Rendering.LightUnit.Lumen => NitsToLumen(intensity, area), 
					global::UnityEngine.Rendering.LightUnit.Ev100 => NitsToEv100(intensity), 
					_ => throw new global::System.ArgumentOutOfRangeException("toUnit", toUnit, null), 
				};
			case global::UnityEngine.Rendering.LightUnit.Ev100:
				switch (toUnit)
				{
				case global::UnityEngine.Rendering.LightUnit.Lumen:
				{
					float num = Ev100ToNits(intensity);
					switch (lightType)
					{
					case global::UnityEngine.LightType.Spot:
					case global::UnityEngine.LightType.Point:
					case global::UnityEngine.LightType.Pyramid:
						return CandelaToLumen(num, solidAngle);
					case global::UnityEngine.LightType.Area:
					case global::UnityEngine.LightType.Disc:
					case global::UnityEngine.LightType.Tube:
						return NitsToLumen(num, area);
					default:
						throw new global::System.ArgumentException("Converting from Lumen to Ev100 is undefined for light type " + lightType);
					}
				}
				case global::UnityEngine.Rendering.LightUnit.Candela:
				case global::UnityEngine.Rendering.LightUnit.Nits:
					return Ev100ToNits(intensity);
				case global::UnityEngine.Rendering.LightUnit.Lux:
					return CandelaToLux(Ev100ToNits(intensity), luxAtDistance);
				default:
					throw new global::System.ArgumentOutOfRangeException("toUnit", toUnit, null);
				}
			default:
				throw new global::System.ArgumentOutOfRangeException("fromUnit", fromUnit, null);
			}
		}

		public static float ConvertIntensity(global::UnityEngine.Light light, float intensity, global::UnityEngine.Rendering.LightUnit fromUnit, global::UnityEngine.Rendering.LightUnit toUnit)
		{
			global::UnityEngine.LightType type = light.type;
			float area = type switch
			{
				global::UnityEngine.LightType.Area => GetAreaFromRectangleLight(light.areaSize), 
				global::UnityEngine.LightType.Disc => GetAreaFromDiscLight(light.areaSize.x), 
				global::UnityEngine.LightType.Tube => GetAreaFromTubeLight(light.areaSize.x), 
				_ => 0f, 
			};
			float luxAtDistance = light.luxAtDistance;
			float num = ((type != global::UnityEngine.LightType.Spot && type != global::UnityEngine.LightType.Point && type != global::UnityEngine.LightType.Pyramid) ? 0f : GetSolidAngle(type, light.enableSpotReflector, light.spotAngle, light.areaSize.x));
			float solidAngle = num;
			return ConvertIntensityInternal(intensity, fromUnit, toUnit, type, area, luxAtDistance, solidAngle);
		}
	}
}

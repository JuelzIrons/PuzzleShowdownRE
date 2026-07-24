namespace UnityEngine.Rendering
{
	public sealed class LensFlareCommonSRP
	{
		internal class LensFlareCompInfo
		{
			internal int index;

			internal global::UnityEngine.Rendering.LensFlareComponentSRP comp;

			internal LensFlareCompInfo(int idx, global::UnityEngine.Rendering.LensFlareComponentSRP cmp)
			{
				index = idx;
				comp = cmp;
			}
		}

		private static global::UnityEngine.Rendering.LensFlareCommonSRP m_Instance = null;

		private static readonly object m_Padlock = new object();

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo> m_Data = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo>();

		private static global::System.Collections.Generic.List<int> m_AvailableIndicies = new global::System.Collections.Generic.List<int>();

		public static int maxLensFlareWithOcclusion = 128;

		public static int maxLensFlareWithOcclusionTemporalSample = 8;

		public static int mergeNeeded = 1;

		public static global::UnityEngine.Rendering.RTHandle occlusionRT = null;

		private static int frameIdx = 0;

		internal static readonly int _FlareOcclusionPermutation = global::UnityEngine.Shader.PropertyToID("_FlareOcclusionPermutation");

		internal static readonly int _FlareOcclusionRemapTex = global::UnityEngine.Shader.PropertyToID("_FlareOcclusionRemapTex");

		internal static readonly int _FlareOcclusionTex = global::UnityEngine.Shader.PropertyToID("_FlareOcclusionTex");

		internal static readonly int _FlareOcclusionIndex = global::UnityEngine.Shader.PropertyToID("_FlareOcclusionIndex");

		internal static readonly int _FlareCloudOpacity = global::UnityEngine.Shader.PropertyToID("_FlareCloudOpacity");

		internal static readonly int _FlareSunOcclusionTex = global::UnityEngine.Shader.PropertyToID("_FlareSunOcclusionTex");

		internal static readonly int _FlareTex = global::UnityEngine.Shader.PropertyToID("_FlareTex");

		internal static readonly int _FlareColorValue = global::UnityEngine.Shader.PropertyToID("_FlareColorValue");

		internal static readonly int _FlareData0 = global::UnityEngine.Shader.PropertyToID("_FlareData0");

		internal static readonly int _FlareData1 = global::UnityEngine.Shader.PropertyToID("_FlareData1");

		internal static readonly int _FlareData2 = global::UnityEngine.Shader.PropertyToID("_FlareData2");

		internal static readonly int _FlareData3 = global::UnityEngine.Shader.PropertyToID("_FlareData3");

		internal static readonly int _FlareData4 = global::UnityEngine.Shader.PropertyToID("_FlareData4");

		internal static readonly int _FlareData5 = global::UnityEngine.Shader.PropertyToID("_FlareData5");

		internal static readonly int _FlareData6 = global::UnityEngine.Shader.PropertyToID("_FlareData6");

		internal static readonly int _FlareRadialTint = global::UnityEngine.Shader.PropertyToID("_FlareRadialTint");

		internal static readonly int _ViewId = global::UnityEngine.Shader.PropertyToID("_ViewId");

		internal static readonly int _LensFlareScreenSpaceBloomMipTexture = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceBloomMipTexture");

		internal static readonly int _LensFlareScreenSpaceResultTexture = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceResultTexture");

		internal static readonly int _LensFlareScreenSpaceSpectralLut = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceSpectralLut");

		internal static readonly int _LensFlareScreenSpaceStreakTex = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceStreakTex");

		internal static readonly int _LensFlareScreenSpaceMipLevel = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceMipLevel");

		internal static readonly int _LensFlareScreenSpaceTintColor = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceTintColor");

		internal static readonly int _LensFlareScreenSpaceParams1 = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceParams1");

		internal static readonly int _LensFlareScreenSpaceParams2 = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceParams2");

		internal static readonly int _LensFlareScreenSpaceParams3 = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceParams3");

		internal static readonly int _LensFlareScreenSpaceParams4 = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceParams4");

		internal static readonly int _LensFlareScreenSpaceParams5 = global::UnityEngine.Shader.PropertyToID("_LensFlareScreenSpaceParams5");

		private static readonly bool s_SupportsLensFlare16bitsFormat = global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render);

		private static readonly bool s_SupportsLensFlare32bitsFormat = global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.Render);

		private static readonly bool s_SupportsLensFlare16bitsFormatWithLoadStore = global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.LoadStore);

		private static readonly bool s_SupportsLensFlare32bitsFormatWithLoadStore = global::UnityEngine.SystemInfo.IsFormatSupported(global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat, global::UnityEngine.Experimental.Rendering.GraphicsFormatUsage.LoadStore);

		private static bool requireOcclusionRTRandomWrite => mergeNeeded > 0;

		public static global::UnityEngine.Rendering.LensFlareCommonSRP Instance
		{
			get
			{
				if (m_Instance == null)
				{
					lock (m_Padlock)
					{
						if (m_Instance == null)
						{
							m_Instance = new global::UnityEngine.Rendering.LensFlareCommonSRP();
						}
					}
				}
				return m_Instance;
			}
		}

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo> Data => m_Data;

		private LensFlareCommonSRP()
		{
		}

		private static bool CheckOcclusionBasedOnDeviceType()
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Null && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLCore)
			{
				return global::UnityEngine.SystemInfo.graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.WebGPU;
			}
			return false;
		}

		public static bool IsOcclusionRTCompatible()
		{
			if (requireOcclusionRTRandomWrite)
			{
				if (CheckOcclusionBasedOnDeviceType())
				{
					if (!s_SupportsLensFlare16bitsFormatWithLoadStore)
					{
						return s_SupportsLensFlare32bitsFormatWithLoadStore;
					}
					return true;
				}
				return false;
			}
			if (CheckOcclusionBasedOnDeviceType())
			{
				if (!s_SupportsLensFlare16bitsFormat)
				{
					return s_SupportsLensFlare32bitsFormat;
				}
				return true;
			}
			return false;
		}

		private static global::UnityEngine.Experimental.Rendering.GraphicsFormat GetOcclusionRTFormat()
		{
			if (requireOcclusionRTRandomWrite ? s_SupportsLensFlare16bitsFormatWithLoadStore : s_SupportsLensFlare16bitsFormat)
			{
				return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat;
			}
			return global::UnityEngine.Experimental.Rendering.GraphicsFormat.R32_SFloat;
		}

		public static void Initialize()
		{
			frameIdx = 0;
			if (IsOcclusionRTCompatible() && occlusionRT == null)
			{
				occlusionRT = global::UnityEngine.Rendering.RTHandles.Alloc(maxLensFlareWithOcclusion, global::UnityEngine.Mathf.Max(mergeNeeded * (maxLensFlareWithOcclusionTemporalSample + 1), 1), GetOcclusionRTFormat(), global::UnityEngine.Rendering.TextureXR.slices, global::UnityEngine.FilterMode.Point, global::UnityEngine.TextureWrapMode.Repeat, global::UnityEngine.Rendering.TextureDimension.Tex2DArray, requireOcclusionRTRandomWrite);
			}
		}

		public static void Dispose()
		{
			if (IsOcclusionRTCompatible() && occlusionRT != null)
			{
				global::UnityEngine.Rendering.RTHandles.Release(occlusionRT);
				occlusionRT = null;
			}
		}

		public bool IsEmpty()
		{
			return Data.Count == 0;
		}

		private int GetNextAvailableIndex()
		{
			if (m_AvailableIndicies.Count == 0)
			{
				return m_Data.Count;
			}
			int result = m_AvailableIndicies[m_AvailableIndicies.Count - 1];
			m_AvailableIndicies.RemoveAt(m_AvailableIndicies.Count - 1);
			return result;
		}

		public void AddData(global::UnityEngine.Rendering.LensFlareComponentSRP newData)
		{
			if (!m_Data.Exists((global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo x) => x.comp == newData))
			{
				m_Data.Add(new global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo(GetNextAvailableIndex(), newData));
			}
		}

		public void RemoveData(global::UnityEngine.Rendering.LensFlareComponentSRP data)
		{
			global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo lensFlareCompInfo = m_Data.Find((global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo x) => x.comp == data);
			if (lensFlareCompInfo != null)
			{
				int index = lensFlareCompInfo.index;
				m_Data.Remove(lensFlareCompInfo);
				m_AvailableIndicies.Add(index);
				if (m_Data.Count == 0)
				{
					m_AvailableIndicies.Clear();
				}
			}
		}

		public static float ShapeAttenuationPointLight()
		{
			return 1f;
		}

		public static float ShapeAttenuationDirLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Vector3.Dot(-forward, wo), 0f);
		}

		public static float ShapeAttenuationSpotConeLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo, float spotAngle, float innerSpotPercent01)
		{
			float num = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Cos(0.5f * spotAngle * (global::System.MathF.PI / 180f)), 0f);
			float num2 = global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Cos(0.5f * spotAngle * (global::System.MathF.PI / 180f) * innerSpotPercent01), 0f);
			return global::UnityEngine.Mathf.Clamp01((global::UnityEngine.Mathf.Max(global::UnityEngine.Vector3.Dot(forward, wo), 0f) - num) / (num2 - num));
		}

		public static float ShapeAttenuationSpotBoxLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Sign(global::UnityEngine.Vector3.Dot(forward, wo)), 0f);
		}

		public static float ShapeAttenuationSpotPyramidLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return ShapeAttenuationSpotBoxLight(forward, wo);
		}

		public static float ShapeAttenuationAreaTubeLight(global::UnityEngine.Vector3 lightPositionWS, global::UnityEngine.Vector3 lightSide, float lightWidth, global::UnityEngine.Camera cam)
		{
			global::UnityEngine.Transform transform = cam.transform;
			global::UnityEngine.Vector3 position = lightPositionWS + 0.5f * lightWidth * lightSide;
			global::UnityEngine.Vector3 position2 = lightPositionWS - 0.5f * lightWidth * lightSide;
			global::UnityEngine.Vector3 position3 = lightPositionWS + 0.5f * lightWidth * transform.right;
			global::UnityEngine.Vector3 position4 = lightPositionWS - 0.5f * lightWidth * transform.right;
			global::UnityEngine.Vector3 p = transform.InverseTransformPoint(position);
			global::UnityEngine.Vector3 p2 = transform.InverseTransformPoint(position2);
			global::UnityEngine.Vector3 p3 = transform.InverseTransformPoint(position3);
			global::UnityEngine.Vector3 p4 = transform.InverseTransformPoint(position4);
			float num = DiffLineIntegral(p3, p4);
			float num2 = DiffLineIntegral(p, p2);
			if (!(num > 0f))
			{
				return 1f;
			}
			return num2 / num;
			static float DiffLineIntegral(global::UnityEngine.Vector3 vector2, global::UnityEngine.Vector3 vector)
			{
				global::UnityEngine.Vector3 normalized = (vector - vector2).normalized;
				if ((double)vector2.z <= 0.0 && (double)vector.z <= 0.0)
				{
					return 0f;
				}
				if ((double)vector2.z < 0.0)
				{
					vector2 = (vector2 * vector.z - vector * vector2.z) / (vector.z - vector2.z);
				}
				if ((double)vector.z < 0.0)
				{
					vector = (-vector2 * vector.z + vector * vector2.z) / (0f - vector.z + vector2.z);
				}
				float num3 = global::UnityEngine.Vector3.Dot(vector2, normalized);
				float l = global::UnityEngine.Vector3.Dot(vector, normalized);
				global::UnityEngine.Vector3 vector3 = vector2 - num3 * normalized;
				float magnitude = vector3.magnitude;
				return ((Fpo(magnitude, l) - Fpo(magnitude, num3)) * vector3.z + (Fwt(magnitude, l) - Fwt(magnitude, num3)) * normalized.z) / global::System.MathF.PI;
			}
			static float Fpo(float d, float l)
			{
				return l / (d * (d * d + l * l)) + global::UnityEngine.Mathf.Atan(l / d) / (d * d);
			}
			static float Fwt(float d, float l)
			{
				return l * l / (d * (d * d + l * l));
			}
		}

		private static float ShapeAttenuateForwardLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return global::UnityEngine.Mathf.Max(global::UnityEngine.Vector3.Dot(forward, wo), 0f);
		}

		public static float ShapeAttenuationAreaRectangleLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return ShapeAttenuateForwardLight(forward, wo);
		}

		public static float ShapeAttenuationAreaDiscLight(global::UnityEngine.Vector3 forward, global::UnityEngine.Vector3 wo)
		{
			return ShapeAttenuateForwardLight(forward, wo);
		}

		private static bool IsLensFlareSRPHidden(global::UnityEngine.Camera cam, global::UnityEngine.Rendering.LensFlareComponentSRP comp, global::UnityEngine.Rendering.LensFlareDataSRP data)
		{
			if (!comp.enabled || !comp.gameObject.activeSelf || !comp.gameObject.activeInHierarchy || data == null || data.elements == null || data.elements.Length == 0 || comp.intensity <= 0f || (cam.cullingMask & (1 << comp.gameObject.layer)) == 0)
			{
				return true;
			}
			return false;
		}

		private static global::UnityEngine.Vector4 InternalGetFlareData0(global::UnityEngine.Vector2 screenPos, global::UnityEngine.Vector2 translationScale, global::UnityEngine.Vector2 rayOff0, global::UnityEngine.Vector2 vLocalScreenRatio, float angleDeg, float position, float angularOffset, global::UnityEngine.Vector2 positionOffset, bool autoRotate)
		{
			if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop)
			{
				angleDeg *= -1f;
				positionOffset.y *= -1f;
			}
			float num = global::UnityEngine.Mathf.Cos((0f - angularOffset) * (global::System.MathF.PI / 180f));
			float num2 = global::UnityEngine.Mathf.Sin((0f - angularOffset) * (global::System.MathF.PI / 180f));
			global::UnityEngine.Vector2 vector = -translationScale * (screenPos + screenPos * (position - 1f));
			vector = new global::UnityEngine.Vector2(num * vector.x - num2 * vector.y, num2 * vector.x + num * vector.y);
			float num3 = angleDeg;
			num3 += 180f;
			if (autoRotate)
			{
				global::UnityEngine.Vector2 vector2 = vector.normalized * vLocalScreenRatio * translationScale;
				num3 += -57.29578f * global::UnityEngine.Mathf.Atan2(vector2.y, vector2.x);
			}
			num3 *= global::System.MathF.PI / 180f;
			float x = global::UnityEngine.Mathf.Cos(0f - num3);
			float y = global::UnityEngine.Mathf.Sin(0f - num3);
			return new global::UnityEngine.Vector4(x, y, positionOffset.x + rayOff0.x * translationScale.x, 0f - positionOffset.y + rayOff0.y * translationScale.y);
		}

		[global::System.Obsolete("This is now deprecated as a public API. Call ComputeOcclusion() or DoLensFlareDataDrivenCommon() instead. #from(6000.3)")]
		public static global::UnityEngine.Vector4 GetFlareData0(global::UnityEngine.Vector2 screenPos, global::UnityEngine.Vector2 translationScale, global::UnityEngine.Vector2 rayOff0, global::UnityEngine.Vector2 vLocalScreenRatio, float angleDeg, float position, float angularOffset, global::UnityEngine.Vector2 positionOffset, bool autoRotate)
		{
			return InternalGetFlareData0(screenPos, translationScale, rayOff0, vLocalScreenRatio, angleDeg, position, angularOffset, positionOffset, autoRotate);
		}

		private static global::UnityEngine.Vector2 GetLensFlareRayOffset(global::UnityEngine.Vector2 screenPos, float position, float globalCos0, float globalSin0)
		{
			global::UnityEngine.Vector2 vector = -(screenPos + screenPos * (position - 1f));
			return new global::UnityEngine.Vector2(globalCos0 * vector.x - globalSin0 * vector.y, globalSin0 * vector.x + globalCos0 * vector.y);
		}

		private static global::UnityEngine.Vector3 WorldToViewport(global::UnityEngine.Camera camera, bool isLocalLight, bool isCameraRelative, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Vector3 positionWS)
		{
			if (isLocalLight)
			{
				return WorldToViewportLocal(isCameraRelative, viewProjMatrix, camera.transform.position, positionWS);
			}
			return WorldToViewportDistance(camera, positionWS);
		}

		private static global::UnityEngine.Vector3 WorldToViewportLocal(bool isCameraRelative, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Vector3 cameraPosWS, global::UnityEngine.Vector3 positionWS)
		{
			global::UnityEngine.Vector3 vector = positionWS;
			if (isCameraRelative)
			{
				vector -= cameraPosWS;
			}
			global::UnityEngine.Vector4 vector2 = viewProjMatrix * vector;
			global::UnityEngine.Vector3 result = new global::UnityEngine.Vector3(vector2.x, vector2.y, 0f);
			result /= vector2.w;
			result.x = result.x * 0.5f + 0.5f;
			result.y = result.y * 0.5f + 0.5f;
			result.y = 1f - result.y;
			result.z = vector2.w;
			return result;
		}

		private static global::UnityEngine.Vector3 WorldToViewportDistance(global::UnityEngine.Camera cam, global::UnityEngine.Vector3 positionWS)
		{
			global::UnityEngine.Vector4 vector = cam.worldToCameraMatrix * positionWS;
			global::UnityEngine.Vector4 vector2 = cam.projectionMatrix * vector;
			global::UnityEngine.Vector3 result = new global::UnityEngine.Vector3(vector2.x, vector2.y, 0f);
			result /= vector2.w;
			result.x = result.x * 0.5f + 0.5f;
			result.y = result.y * 0.5f + 0.5f;
			result.z = vector2.w;
			return result;
		}

		public static bool IsCloudLayerOpacityNeeded(global::UnityEngine.Camera cam)
		{
			if (Instance.IsEmpty())
			{
				return false;
			}
			foreach (global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo datum in Instance.Data)
			{
				if (datum != null && !(datum.comp == null))
				{
					global::UnityEngine.Rendering.LensFlareComponentSRP comp = datum.comp;
					global::UnityEngine.Rendering.LensFlareDataSRP lensFlareData = comp.lensFlareData;
					if (!IsLensFlareSRPHidden(cam, comp, lensFlareData) && comp.useOcclusion && (!comp.useOcclusion || comp.sampleCount != 0) && comp.environmentOcclusion)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static void ComputeOcclusion(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, global::UnityEngine.Experimental.Rendering.XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, global::UnityEngine.Vector3 cameraPositionWS, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, global::UnityEngine.Texture cloudOpacityTexture, global::UnityEngine.Texture sunOcclusionTexture)
		{
			ComputeOcclusion(lensFlareShader, cam, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd.m_WrappedCommandBuffer, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture);
		}

		private static bool ForceSingleElement(global::UnityEngine.Rendering.LensFlareDataElementSRP element)
		{
			if (element.allowMultipleElement && element.count != 1)
			{
				return element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Ring;
			}
			return true;
		}

		private static bool PreDrawSetup(bool occlusionOnly, bool clearRenderTarget, global::UnityEngine.Rendering.RenderTargetIdentifier rt, global::UnityEngine.Camera cam, global::UnityEngine.Experimental.Rendering.XRPass xr, int xrIndex, global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			xr.StopSinglePass(cmd);
			if (Instance.IsEmpty())
			{
				return false;
			}
			int value = (occlusionOnly ? (-1) : 0);
			if (xr.enabled && xr.singlePassEnabled)
			{
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, rt, global::UnityEngine.Rendering.ClearFlag.None, 0, global::UnityEngine.CubemapFace.Unknown, xrIndex);
				cmd.SetGlobalInt(_ViewId, xrIndex);
			}
			else
			{
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, rt);
				if (xr.enabled)
				{
					cmd.SetGlobalInt(_ViewId, xr.multipassId);
				}
				else
				{
					cmd.SetGlobalInt(_ViewId, value);
				}
			}
			if (clearRenderTarget)
			{
				cmd.ClearRenderTarget(clearDepth: false, clearColor: true, global::UnityEngine.Color.black);
			}
			return true;
		}

		private static bool DoComponent(bool occlusionOnly, global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo info, global::UnityEngine.Camera cam, global::UnityEngine.Vector3 cameraPositionWS, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Rendering.CommandBuffer cmd, out global::UnityEngine.Vector3 flarePosWS, out global::UnityEngine.Vector3 flarePosViewport, out global::UnityEngine.Vector2 flarePosScreen, out global::UnityEngine.Vector3 camToFlare, out global::UnityEngine.Light light, out bool isDirLight, out float flareIntensity, out float distanceAttenuation)
		{
			flarePosWS = global::UnityEngine.Vector3.zero;
			flarePosViewport = global::UnityEngine.Vector3.zero;
			flarePosScreen = global::UnityEngine.Vector2.zero;
			camToFlare = global::UnityEngine.Vector3.zero;
			isDirLight = false;
			light = null;
			flareIntensity = 0f;
			distanceAttenuation = 1f;
			if (info == null || info.comp == null)
			{
				return false;
			}
			global::UnityEngine.Rendering.LensFlareComponentSRP comp = info.comp;
			global::UnityEngine.Rendering.LensFlareDataSRP lensFlareData = comp.lensFlareData;
			if (IsLensFlareSRPHidden(cam, comp, lensFlareData))
			{
				return false;
			}
			if (occlusionOnly && !comp.useOcclusion)
			{
				return false;
			}
			if (!comp.TryGetComponent<global::UnityEngine.Light>(out light))
			{
				light = null;
			}
			if (light != null && light.type == global::UnityEngine.LightType.Directional)
			{
				flarePosWS = -light.transform.forward * cam.farClipPlane;
				isDirLight = true;
			}
			else
			{
				flarePosWS = comp.transform.position;
			}
			if (!occlusionOnly && comp.lightOverride != null)
			{
				light = comp.lightOverride;
			}
			flarePosViewport = WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, flarePosWS);
			if (usePanini && cam == global::UnityEngine.Camera.main)
			{
				flarePosViewport = DoPaniniProjection(flarePosViewport, actualWidth, actualHeight, cam.fieldOfView, paniniCropToFit, paniniDistance);
			}
			if (flarePosViewport.z < 0f)
			{
				return false;
			}
			if (!comp.allowOffScreen && (flarePosViewport.x < 0f || flarePosViewport.x > 1f || flarePosViewport.y < 0f || flarePosViewport.y > 1f))
			{
				return false;
			}
			camToFlare = flarePosWS - cameraPositionWS;
			if (global::UnityEngine.Vector3.Dot(cam.transform.forward, camToFlare) < 0f)
			{
				return false;
			}
			float time = camToFlare.magnitude / comp.maxAttenuationDistance;
			distanceAttenuation = ((!isDirLight && comp.distanceAttenuationCurve.length > 0) ? comp.distanceAttenuationCurve.Evaluate(time) : 1f);
			flarePosScreen = new global::UnityEngine.Vector2(2f * flarePosViewport.x - 1f, 0f - (2f * flarePosViewport.y - 1f));
			if (!global::UnityEngine.SystemInfo.graphicsUVStartsAtTop & isDirLight)
			{
				flarePosScreen.y = 0f - flarePosScreen.y;
			}
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Abs(flarePosScreen.x), global::UnityEngine.Mathf.Abs(flarePosScreen.y));
			float time2 = global::UnityEngine.Mathf.Max(vector.x, vector.y);
			float num = ((comp.radialScreenAttenuationCurve.length > 0) ? comp.radialScreenAttenuationCurve.Evaluate(time2) : 1f);
			flareIntensity = comp.intensity * num * distanceAttenuation;
			if (flareIntensity <= 0f)
			{
				return false;
			}
			float num2 = (isDirLight ? comp.celestialProjectedOcclusionRadius(cam) : comp.occlusionRadius);
			global::UnityEngine.Vector2 vector2 = flarePosViewport;
			float magnitude = ((global::UnityEngine.Vector2)WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, flarePosWS + cam.transform.up * num2) - vector2).magnitude;
			global::UnityEngine.Vector3 normalized = (cam.transform.position - comp.transform.position).normalized;
			global::UnityEngine.Vector3 vector3 = WorldToViewport(cam, !isDirLight, isCameraRelative, viewProjMatrix, flarePosWS + normalized * comp.occlusionOffset);
			cmd.SetGlobalVector(_FlareData1, new global::UnityEngine.Vector4(magnitude, comp.sampleCount, vector3.z, actualHeight / actualWidth));
			return true;
		}

		public static void ComputeOcclusion(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, global::UnityEngine.Experimental.Rendering.XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, global::UnityEngine.Vector3 cameraPositionWS, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Rendering.CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, global::UnityEngine.Texture cloudOpacityTexture, global::UnityEngine.Texture sunOcclusionTexture)
		{
			if (!IsOcclusionRTCompatible())
			{
				return;
			}
			bool clearRenderTarget = !taaEnabled;
			if (!PreDrawSetup(occlusionOnly: true, clearRenderTarget, occlusionRT, cam, xr, xrIndex, cmd))
			{
				return;
			}
			float x = actualWidth / actualHeight;
			foreach (global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo datum in m_Data)
			{
				if (DoComponent(occlusionOnly: true, datum, cam, cameraPositionWS, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, viewProjMatrix, cmd, out var _, out var _, out var flarePosScreen, out var _, out var _, out var _, out var _, out var _))
				{
					global::UnityEngine.Rendering.LensFlareComponentSRP comp = datum.comp;
					cmd.EnableShaderKeyword("FLARE_COMPUTE_OCCLUSION");
					uint num = 1u;
					if (comp.environmentOcclusion && sunOcclusionTexture != null)
					{
						num |= 4;
						cmd.SetGlobalTexture(_FlareSunOcclusionTex, sunOcclusionTexture);
					}
					int value = (int)num;
					cmd.SetGlobalInt(_FlareOcclusionPermutation, value);
					float globalCos = global::UnityEngine.Mathf.Cos(0f);
					float globalSin = global::UnityEngine.Mathf.Sin(0f);
					float position = 0f;
					float y = global::UnityEngine.Mathf.Clamp01(0.999999f);
					cmd.SetGlobalVector(_FlareData3, new global::UnityEngine.Vector4(comp.allowOffScreen ? 1f : (-1f), y, global::UnityEngine.Mathf.Exp(global::UnityEngine.Mathf.Lerp(0f, 4f, 1f)), 1f / 3f));
					global::UnityEngine.Vector2 lensFlareRayOffset = GetLensFlareRayOffset(flarePosScreen, position, globalCos, globalSin);
					global::UnityEngine.Vector4 value2 = InternalGetFlareData0(vLocalScreenRatio: new global::UnityEngine.Vector2(x, 1f), screenPos: flarePosScreen, translationScale: global::UnityEngine.Vector2.one, rayOff0: lensFlareRayOffset, angleDeg: 0f, position: position, angularOffset: 0f, positionOffset: global::UnityEngine.Vector2.zero, autoRotate: false);
					cmd.SetGlobalVector(_FlareData0, value2);
					cmd.SetGlobalVector(_FlareData2, new global::UnityEngine.Vector4(flarePosScreen.x, flarePosScreen.y, 0f, 0f));
					global::UnityEngine.Rect viewport = ((!taaEnabled) ? new global::UnityEngine.Rect
					{
						x = datum.index,
						y = 0f,
						width = 1f,
						height = 1f
					} : new global::UnityEngine.Rect
					{
						x = datum.index,
						y = frameIdx + mergeNeeded,
						width = 1f,
						height = 1f
					});
					cmd.SetViewport(viewport);
					global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, lensFlareShader.FindPass("LensFlareOcclusion"));
				}
			}
			if (taaEnabled)
			{
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, occlusionRT, global::UnityEngine.Rendering.ClearFlag.None, 0, global::UnityEngine.CubemapFace.Unknown, xrIndex);
				cmd.SetViewport(new global::UnityEngine.Rect
				{
					x = m_Data.Count,
					y = 0f,
					width = maxLensFlareWithOcclusion - m_Data.Count,
					height = maxLensFlareWithOcclusionTemporalSample + mergeNeeded
				});
				cmd.ClearRenderTarget(clearDepth: false, clearColor: true, global::UnityEngine.Color.black);
			}
			frameIdx++;
			frameIdx %= maxLensFlareWithOcclusionTemporalSample;
			xr.StartSinglePass(cmd);
		}

		public static void ProcessLensFlareSRPElementsSingle(global::UnityEngine.Rendering.LensFlareDataElementSRP element, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Color globalColorModulation, global::UnityEngine.Light light, float compIntensity, float scale, global::UnityEngine.Material lensFlareShader, global::UnityEngine.Vector2 screenPos, bool compAllowOffScreen, global::UnityEngine.Vector2 vScreenRatio, global::UnityEngine.Vector3 flareData1, bool preview, int depth)
		{
			if (element == null || !element.visible || (element.lensFlareTexture == null && element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Image) || element.localIntensity <= 0f || element.count <= 0 || (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.LensFlareDataSRP && element.lensFlareDataSRP == null))
			{
				return;
			}
			if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.LensFlareDataSRP && element.lensFlareDataSRP != null)
			{
				ProcessLensFlareSRPElements(ref element.lensFlareDataSRP.elements, cmd, globalColorModulation, light, compIntensity, scale, lensFlareShader, screenPos, compAllowOffScreen, vScreenRatio.x, default(global::UnityEngine.Vector3), preview, depth + 1);
				return;
			}
			global::UnityEngine.Color color = globalColorModulation;
			if (light != null && element.modulateByLightColor)
			{
				if (light.useColorTemperature)
				{
					color *= light.color * global::UnityEngine.Mathf.CorrelatedColorTemperatureToRGB(light.colorTemperature);
				}
				else
				{
					color *= light.color;
				}
			}
			global::UnityEngine.Color color2 = color;
			float num = element.localIntensity * compIntensity;
			if (num <= 0f)
			{
				return;
			}
			global::UnityEngine.Texture lensFlareTexture = element.lensFlareTexture;
			float usedAspectRatio;
			if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Image)
			{
				usedAspectRatio = (element.preserveAspectRatio ? ((float)lensFlareTexture.height / (float)lensFlareTexture.width) : 1f);
			}
			else
			{
				usedAspectRatio = 1f;
			}
			float rotation = element.rotation;
			global::UnityEngine.Vector2 vector = ((!element.preserveAspectRatio) ? new global::UnityEngine.Vector2(element.sizeXY.x, element.sizeXY.y) : ((!(usedAspectRatio >= 1f)) ? new global::UnityEngine.Vector2(element.sizeXY.x, element.sizeXY.y * usedAspectRatio) : new global::UnityEngine.Vector2(element.sizeXY.x / usedAspectRatio, element.sizeXY.y)));
			float num2 = 0.1f;
			global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(vector.x, vector.y);
			float combinedScale = num2 * element.uniformScale * scale;
			vector2 *= combinedScale;
			color2 *= element.tint;
			float num3 = (global::UnityEngine.SystemInfo.graphicsUVStartsAtTop ? element.angularOffset : (0f - element.angularOffset));
			float globalCos0 = global::UnityEngine.Mathf.Cos((0f - num3) * (global::System.MathF.PI / 180f));
			float globalSin0 = global::UnityEngine.Mathf.Sin((0f - num3) * (global::System.MathF.PI / 180f));
			float position = 2f * element.position;
			int shaderPass = element.blendMode switch
			{
				global::UnityEngine.Rendering.SRPLensFlareBlendMode.Additive => lensFlareShader.FindPass("LensFlareAdditive"), 
				global::UnityEngine.Rendering.SRPLensFlareBlendMode.Screen => lensFlareShader.FindPass("LensFlareScreen"), 
				global::UnityEngine.Rendering.SRPLensFlareBlendMode.Premultiply => lensFlareShader.FindPass("LensFlarePremultiply"), 
				global::UnityEngine.Rendering.SRPLensFlareBlendMode.Lerp => lensFlareShader.FindPass("LensFlareLerp"), 
				_ => lensFlareShader.FindPass("LensFlareOcclusion"), 
			};
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4((float)element.flareType, 0f, 0f, 0f);
			if (ForceSingleElement(element))
			{
				cmd.SetGlobalVector(_FlareData6, value);
			}
			if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Circle || element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Polygon || element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Ring)
			{
				if (element.inverseSDF)
				{
					cmd.EnableShaderKeyword("FLARE_INVERSE_SDF");
				}
				else
				{
					cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
				}
			}
			else
			{
				cmd.DisableShaderKeyword("FLARE_INVERSE_SDF");
			}
			if (element.lensFlareTexture != null)
			{
				cmd.SetGlobalTexture(_FlareTex, element.lensFlareTexture);
			}
			if (element.tintColorType != global::UnityEngine.Rendering.SRPLensFlareColorType.Constant)
			{
				cmd.SetGlobalTexture(_FlareRadialTint, element.tintGradient.GetTexture());
			}
			float num4 = global::UnityEngine.Mathf.Clamp01(1f - element.edgeOffset - 1E-06f);
			if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Polygon)
			{
				num4 = global::UnityEngine.Mathf.Pow(num4 + 1f, 5f);
			}
			float sdfRoundness = element.sdfRoundness;
			global::UnityEngine.Vector4 value2 = new global::UnityEngine.Vector4(compAllowOffScreen ? 1f : (-1f), num4, global::UnityEngine.Mathf.Exp(global::UnityEngine.Mathf.Lerp(0f, 4f, global::UnityEngine.Mathf.Clamp01(1f - element.fallOff))), (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Ring) ? element.ringThickness : (1f / (float)element.sideCount));
			cmd.SetGlobalVector(_FlareData3, value2);
			if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Polygon)
			{
				float num5 = 1f / (float)element.sideCount;
				float num6 = global::UnityEngine.Mathf.Cos(global::System.MathF.PI * num5);
				float num7 = num6 * sdfRoundness;
				float num8 = num6 - num7;
				float num9 = global::System.MathF.PI * 2f * num5;
				float w = num8 * global::UnityEngine.Mathf.Tan(0.5f * num9);
				cmd.SetGlobalVector(_FlareData4, new global::UnityEngine.Vector4(sdfRoundness, num8, num9, w));
			}
			else if (element.flareType == global::UnityEngine.Rendering.SRPLensFlareType.Ring)
			{
				cmd.SetGlobalVector(_FlareData4, new global::UnityEngine.Vector4(element.noiseAmplitude, element.noiseFrequency, element.noiseSpeed, 0f));
			}
			else
			{
				cmd.SetGlobalVector(_FlareData4, new global::UnityEngine.Vector4(sdfRoundness, 0f, 0f, 0f));
			}
			cmd.SetGlobalVector(_FlareData5, new global::UnityEngine.Vector4((float)element.tintColorType, num, element.shapeCutOffSpeed, element.shapeCutOffRadius));
			if (ForceSingleElement(element))
			{
				global::UnityEngine.Vector2 curSize = vector2;
				global::UnityEngine.Vector2 lensFlareRayOffset = GetLensFlareRayOffset(screenPos, position, globalCos0, globalSin0);
				if (element.enableRadialDistortion)
				{
					global::UnityEngine.Vector2 lensFlareRayOffset2 = GetLensFlareRayOffset(screenPos, 0f, globalCos0, globalSin0);
					curSize = ComputeLocalSize(lensFlareRayOffset, lensFlareRayOffset2, curSize, element.distortionCurve);
				}
				global::UnityEngine.Vector4 value3 = InternalGetFlareData0(screenPos, element.translationScale, lensFlareRayOffset, vScreenRatio, rotation, position, num3, element.positionOffset, element.autoRotate);
				cmd.SetGlobalVector(_FlareData0, value3);
				cmd.SetGlobalVector(_FlareData2, new global::UnityEngine.Vector4(screenPos.x, screenPos.y, curSize.x, curSize.y));
				cmd.SetGlobalVector(_FlareColorValue, color2);
				global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
				return;
			}
			float num10 = 2f * element.lengthSpread / (float)(element.count - 1);
			if (element.distribution == global::UnityEngine.Rendering.SRPLensFlareDistribution.Uniform)
			{
				float num11 = 0f;
				for (int i = 0; i < element.count; i++)
				{
					global::UnityEngine.Vector2 curSize2 = vector2;
					global::UnityEngine.Vector2 lensFlareRayOffset3 = GetLensFlareRayOffset(screenPos, position, globalCos0, globalSin0);
					if (element.enableRadialDistortion)
					{
						global::UnityEngine.Vector2 lensFlareRayOffset4 = GetLensFlareRayOffset(screenPos, 0f, globalCos0, globalSin0);
						curSize2 = ComputeLocalSize(lensFlareRayOffset3, lensFlareRayOffset4, curSize2, element.distortionCurve);
					}
					float time = ((element.count >= 2) ? ((float)i / (float)(element.count - 1)) : 0.5f);
					global::UnityEngine.Color color3 = element.colorGradient.Evaluate(time);
					global::UnityEngine.Vector4 value4 = InternalGetFlareData0(screenPos, element.translationScale, lensFlareRayOffset3, vScreenRatio, rotation + num11, position, num3, element.positionOffset, element.autoRotate);
					cmd.SetGlobalVector(_FlareData0, value4);
					value.y = i;
					cmd.SetGlobalVector(_FlareData6, value);
					cmd.SetGlobalVector(_FlareData2, new global::UnityEngine.Vector4(screenPos.x, screenPos.y, curSize2.x, curSize2.y));
					cmd.SetGlobalVector(_FlareColorValue, color2 * color3);
					global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
					position += num10;
					num11 += element.uniformAngle;
				}
			}
			else if (element.distribution == global::UnityEngine.Rendering.SRPLensFlareDistribution.Random)
			{
				global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
				global::UnityEngine.Random.InitState(element.seed);
				global::UnityEngine.Vector2 vector3 = new global::UnityEngine.Vector2(globalSin0, globalCos0);
				vector3 *= element.positionVariation.y;
				for (int j = 0; j < element.count; j++)
				{
					float num12 = RandomRange(-1f, 1f) * element.intensityVariation + 1f;
					global::UnityEngine.Vector2 lensFlareRayOffset5 = GetLensFlareRayOffset(screenPos, position, globalCos0, globalSin0);
					global::UnityEngine.Vector2 vector4 = vector2;
					if (element.enableRadialDistortion)
					{
						global::UnityEngine.Vector2 lensFlareRayOffset6 = GetLensFlareRayOffset(screenPos, 0f, globalCos0, globalSin0);
						vector4 = ComputeLocalSize(lensFlareRayOffset5, lensFlareRayOffset6, vector4, element.distortionCurve);
					}
					vector4 += vector4 * (element.scaleVariation * RandomRange(-1f, 1f));
					global::UnityEngine.Color color4 = element.colorGradient.Evaluate(RandomRange(0f, 1f));
					global::UnityEngine.Vector2 positionOffset = element.positionOffset + RandomRange(-1f, 1f) * vector3;
					float angleDeg = rotation + RandomRange(-global::System.MathF.PI, global::System.MathF.PI) * element.rotationVariation;
					if (num12 > 0f)
					{
						global::UnityEngine.Vector4 value5 = InternalGetFlareData0(screenPos, element.translationScale, lensFlareRayOffset5, vScreenRatio, angleDeg, position, num3, positionOffset, element.autoRotate);
						cmd.SetGlobalVector(_FlareData0, value5);
						value.y = j;
						cmd.SetGlobalVector(_FlareData6, value);
						cmd.SetGlobalVector(_FlareData2, new global::UnityEngine.Vector4(screenPos.x, screenPos.y, vector4.x, vector4.y));
						cmd.SetGlobalVector(_FlareColorValue, color2 * color4 * num12);
						global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
					}
					position += num10;
					position += 0.5f * num10 * RandomRange(-1f, 1f) * element.positionVariation.x;
				}
				global::UnityEngine.Random.state = state;
			}
			else
			{
				if (element.distribution != global::UnityEngine.Rendering.SRPLensFlareDistribution.Curve)
				{
					return;
				}
				for (int k = 0; k < element.count; k++)
				{
					float time2 = ((element.count >= 2) ? ((float)k / (float)(element.count - 1)) : 0.5f);
					global::UnityEngine.Color color5 = element.colorGradient.Evaluate(time2);
					float num13 = ((element.positionCurve.length > 0) ? element.positionCurve.Evaluate(time2) : 1f);
					float position2 = position + 2f * element.lengthSpread * num13;
					global::UnityEngine.Vector2 lensFlareRayOffset7 = GetLensFlareRayOffset(screenPos, position2, globalCos0, globalSin0);
					global::UnityEngine.Vector2 curSize3 = vector2;
					if (element.enableRadialDistortion)
					{
						global::UnityEngine.Vector2 lensFlareRayOffset8 = GetLensFlareRayOffset(screenPos, 0f, globalCos0, globalSin0);
						curSize3 = ComputeLocalSize(lensFlareRayOffset7, lensFlareRayOffset8, curSize3, element.distortionCurve);
					}
					float num14 = ((element.scaleCurve.length > 0) ? element.scaleCurve.Evaluate(time2) : 1f);
					curSize3 *= num14;
					float num15 = element.uniformAngleCurve.Evaluate(time2) * (180f - 180f / (float)element.count);
					global::UnityEngine.Vector4 value6 = InternalGetFlareData0(screenPos, element.translationScale, lensFlareRayOffset7, vScreenRatio, rotation + num15, position2, num3, element.positionOffset, element.autoRotate);
					cmd.SetGlobalVector(_FlareData0, value6);
					value.y = k;
					cmd.SetGlobalVector(_FlareData6, value);
					cmd.SetGlobalVector(_FlareData2, new global::UnityEngine.Vector4(screenPos.x, screenPos.y, curSize3.x, curSize3.y));
					cmd.SetGlobalVector(_FlareColorValue, color2 * color5);
					global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
				}
			}
			global::UnityEngine.Vector2 ComputeLocalSize(global::UnityEngine.Vector2 rayOff, global::UnityEngine.Vector2 rayOff0, global::UnityEngine.Vector2 vector6, global::UnityEngine.AnimationCurve distortionCurve)
			{
				GetLensFlareRayOffset(screenPos, position, globalCos0, globalSin0);
				float time3;
				if (!element.distortionRelativeToCenter)
				{
					global::UnityEngine.Vector2 vector5 = (rayOff - rayOff0) * 0.5f;
					time3 = global::UnityEngine.Mathf.Clamp01(global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(vector5.x), global::UnityEngine.Mathf.Abs(vector5.y)));
				}
				else
				{
					time3 = global::UnityEngine.Mathf.Clamp01((screenPos + (rayOff + new global::UnityEngine.Vector2(element.positionOffset.x, 0f - element.positionOffset.y)) * element.translationScale).magnitude);
				}
				float t = global::UnityEngine.Mathf.Clamp01(distortionCurve.Evaluate(time3));
				return new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Lerp(vector6.x, element.targetSizeDistortion.x * combinedScale / usedAspectRatio, t), global::UnityEngine.Mathf.Lerp(vector6.y, element.targetSizeDistortion.y * combinedScale, t));
			}
			static float RandomRange(float min, float max)
			{
				return global::UnityEngine.Random.Range(min, max);
			}
		}

		private static void ProcessLensFlareSRPElements(ref global::UnityEngine.Rendering.LensFlareDataElementSRP[] elements, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Color globalColorModulation, global::UnityEngine.Light light, float compIntensity, float scale, global::UnityEngine.Material lensFlareShader, global::UnityEngine.Vector2 screenPos, bool compAllowOffScreen, float aspect, global::UnityEngine.Vector4 flareData6, bool preview, int depth)
		{
			if (depth > 16)
			{
				global::UnityEngine.Debug.LogWarning("LensFlareSRPAsset contains too deep recursive asset (> 16). Be careful to not have recursive aggregation, A contains B, B contains A, ... which will produce an infinite loop.");
				return;
			}
			global::UnityEngine.Rendering.LensFlareDataElementSRP[] array = elements;
			for (int i = 0; i < array.Length; i++)
			{
				ProcessLensFlareSRPElementsSingle(array[i], cmd, globalColorModulation, light, compIntensity, scale, lensFlareShader, screenPos, compAllowOffScreen, new global::UnityEngine.Vector2(aspect, 1f), default(global::UnityEngine.Vector3), preview, depth);
			}
		}

		public static void DoLensFlareDataDrivenCommon(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, global::UnityEngine.Rect viewport, global::UnityEngine.Experimental.Rendering.XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, global::UnityEngine.Vector3 cameraPositionWS, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, global::UnityEngine.Texture cloudOpacityTexture, global::UnityEngine.Texture sunOcclusionTexture, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::System.Func<global::UnityEngine.Light, global::UnityEngine.Camera, global::UnityEngine.Vector3, float> GetLensFlareLightAttenuation, bool debugView)
		{
			DoLensFlareDataDrivenCommon(lensFlareShader, cam, viewport, xr, xrIndex, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, cameraPositionWS, viewProjMatrix, cmd.m_WrappedCommandBuffer, taaEnabled, hasCloudLayer, cloudOpacityTexture, sunOcclusionTexture, colorBuffer, GetLensFlareLightAttenuation, debugView);
		}

		public static void DoLensFlareDataDrivenCommon(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, global::UnityEngine.Rect viewport, global::UnityEngine.Experimental.Rendering.XRPass xr, int xrIndex, float actualWidth, float actualHeight, bool usePanini, float paniniDistance, float paniniCropToFit, bool isCameraRelative, global::UnityEngine.Vector3 cameraPositionWS, global::UnityEngine.Matrix4x4 viewProjMatrix, global::UnityEngine.Rendering.CommandBuffer cmd, bool taaEnabled, bool hasCloudLayer, global::UnityEngine.Texture cloudOpacityTexture, global::UnityEngine.Texture sunOcclusionTexture, global::UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, global::System.Func<global::UnityEngine.Light, global::UnityEngine.Camera, global::UnityEngine.Vector3, float> GetLensFlareLightAttenuation, bool debugView)
		{
			bool clearRenderTarget = debugView;
			if (!PreDrawSetup(occlusionOnly: false, clearRenderTarget, colorBuffer, cam, xr, xrIndex, cmd))
			{
				return;
			}
			cmd.SetViewport(viewport);
			float aspect = actualWidth / actualHeight;
			foreach (global::UnityEngine.Rendering.LensFlareCommonSRP.LensFlareCompInfo datum in m_Data)
			{
				if (DoComponent(occlusionOnly: false, datum, cam, cameraPositionWS, actualWidth, actualHeight, usePanini, paniniDistance, paniniCropToFit, isCameraRelative, viewProjMatrix, cmd, out var _, out var _, out var flarePosScreen, out var camToFlare, out var light, out var isDirLight, out var flareIntensity, out var distanceAttenuation))
				{
					global::UnityEngine.Rendering.LensFlareComponentSRP comp = datum.comp;
					if (comp.useOcclusion && IsOcclusionRTCompatible())
					{
						cmd.SetGlobalTexture(_FlareOcclusionTex, occlusionRT);
						cmd.EnableShaderKeyword("FLARE_HAS_OCCLUSION");
					}
					else if (comp.useOcclusion && !IsOcclusionRTCompatible())
					{
						cmd.EnableShaderKeyword("FLARE_HAS_OCCLUSION");
					}
					else
					{
						cmd.DisableShaderKeyword("FLARE_HAS_OCCLUSION");
					}
					if (IsOcclusionRTCompatible())
					{
						cmd.DisableShaderKeyword("FLARE_OPENGL3_OR_OPENGLCORE");
					}
					else
					{
						cmd.EnableShaderKeyword("FLARE_OPENGL3_OR_OPENGLCORE");
					}
					cmd.SetGlobalVector(_FlareOcclusionIndex, new global::UnityEngine.Vector4(datum.index, 0f, 0f, 0f));
					cmd.SetGlobalTexture(_FlareOcclusionRemapTex, comp.occlusionRemapCurve.GetTexture());
					global::UnityEngine.Vector4 flareData = default(global::UnityEngine.Vector4);
					float time = camToFlare.magnitude / comp.maxAttenuationScale;
					float num = ((!isDirLight && comp.scaleByDistanceCurve.length >= 1) ? comp.scaleByDistanceCurve.Evaluate(time) : 1f);
					global::UnityEngine.Color white = global::UnityEngine.Color.white;
					if (light != null && comp.attenuationByLightShape)
					{
						white *= GetLensFlareLightAttenuation(light, cam, -camToFlare.normalized);
					}
					white *= distanceAttenuation;
					ProcessLensFlareSRPElements(ref comp.lensFlareData.elements, cmd, white, light, flareIntensity, num * comp.scale, lensFlareShader, flarePosScreen, comp.allowOffScreen, aspect, flareData, preview: false, 0);
				}
			}
			xr.StartSinglePass(cmd);
		}

		public static void DoLensFlareScreenSpaceCommon(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, float actualWidth, float actualHeight, global::UnityEngine.Color tintColor, global::UnityEngine.Texture originalBloomTexture, global::UnityEngine.Texture bloomMipTexture, global::UnityEngine.Texture spectralLut, global::UnityEngine.Texture streakTextureTmp, global::UnityEngine.Texture streakTextureTmp2, global::UnityEngine.Vector4 parameters1, global::UnityEngine.Vector4 parameters2, global::UnityEngine.Vector4 parameters3, global::UnityEngine.Vector4 parameters4, global::UnityEngine.Vector4 parameters5, global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.Rendering.RTHandle result, bool debugView)
		{
			DoLensFlareScreenSpaceCommon(lensFlareShader, cam, actualWidth, actualHeight, tintColor, originalBloomTexture, bloomMipTexture, spectralLut, streakTextureTmp, streakTextureTmp2, parameters1, parameters2, parameters3, parameters4, parameters5, cmd.m_WrappedCommandBuffer, result, debugView);
		}

		public static void DoLensFlareScreenSpaceCommon(global::UnityEngine.Material lensFlareShader, global::UnityEngine.Camera cam, float actualWidth, float actualHeight, global::UnityEngine.Color tintColor, global::UnityEngine.Texture originalBloomTexture, global::UnityEngine.Texture bloomMipTexture, global::UnityEngine.Texture spectralLut, global::UnityEngine.Texture streakTextureTmp, global::UnityEngine.Texture streakTextureTmp2, global::UnityEngine.Vector4 parameters1, global::UnityEngine.Vector4 parameters2, global::UnityEngine.Vector4 parameters3, global::UnityEngine.Vector4 parameters4, global::UnityEngine.Vector4 parameters5, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RTHandle result, bool debugView)
		{
			parameters2.x = global::UnityEngine.Mathf.Pow(parameters2.x, 0.25f);
			parameters3.z /= 20f;
			parameters4.y *= 10f;
			parameters4.z /= 90f;
			parameters5.y = 1f / parameters5.y;
			parameters5.z = 1f / parameters5.z;
			cmd.SetViewport(new global::UnityEngine.Rect
			{
				width = actualWidth,
				height = actualHeight
			});
			if (debugView)
			{
				cmd.ClearRenderTarget(clearDepth: false, clearColor: true, global::UnityEngine.Color.black);
			}
			float y = parameters5.y;
			y *= actualWidth / actualHeight;
			parameters5.y = y;
			float y2 = parameters4.y;
			y2 *= actualWidth * 0.0005f;
			parameters4.y = y2;
			int shaderPass = lensFlareShader.FindPass("LensFlareScreenSpac Prefilter");
			int shaderPass2 = lensFlareShader.FindPass("LensFlareScreenSpace Downsample");
			int shaderPass3 = lensFlareShader.FindPass("LensFlareScreenSpace Upsample");
			int shaderPass4 = lensFlareShader.FindPass("LensFlareScreenSpace Composition");
			int shaderPass5 = lensFlareShader.FindPass("LensFlareScreenSpace Write to BloomTexture");
			cmd.SetGlobalTexture(_LensFlareScreenSpaceBloomMipTexture, bloomMipTexture);
			cmd.SetGlobalTexture(_LensFlareScreenSpaceSpectralLut, spectralLut);
			cmd.SetGlobalVector(_LensFlareScreenSpaceParams1, parameters1);
			cmd.SetGlobalVector(_LensFlareScreenSpaceParams2, parameters2);
			cmd.SetGlobalVector(_LensFlareScreenSpaceParams3, parameters3);
			cmd.SetGlobalVector(_LensFlareScreenSpaceParams4, parameters4);
			cmd.SetGlobalVector(_LensFlareScreenSpaceParams5, parameters5);
			cmd.SetGlobalColor(_LensFlareScreenSpaceTintColor, tintColor);
			if (parameters4.x > 0f)
			{
				global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, streakTextureTmp);
				global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass);
				int b = global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Log(global::UnityEngine.Mathf.Max(actualHeight, actualWidth), 2f));
				int num = global::UnityEngine.Mathf.Max(1, b);
				int num2 = 2;
				int num3 = 0;
				bool flag = false;
				for (int i = 0; i < num; i++)
				{
					flag = i % 2 == 0;
					cmd.SetGlobalInt(_LensFlareScreenSpaceMipLevel, i);
					cmd.SetGlobalTexture(_LensFlareScreenSpaceStreakTex, flag ? streakTextureTmp : streakTextureTmp2);
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, flag ? streakTextureTmp2 : streakTextureTmp);
					global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass2);
				}
				if (flag)
				{
					num3 = 1;
				}
				for (int j = num3; j < num3 + num2; j++)
				{
					flag = j % 2 == 0;
					cmd.SetGlobalInt(_LensFlareScreenSpaceMipLevel, j - num3);
					cmd.SetGlobalTexture(_LensFlareScreenSpaceStreakTex, flag ? streakTextureTmp : streakTextureTmp2);
					global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, flag ? streakTextureTmp2 : streakTextureTmp);
					global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass3);
				}
				cmd.SetGlobalTexture(_LensFlareScreenSpaceStreakTex, flag ? streakTextureTmp2 : streakTextureTmp);
			}
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, result);
			global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass4);
			cmd.SetGlobalTexture(_LensFlareScreenSpaceResultTexture, result);
			global::UnityEngine.Rendering.CoreUtils.SetRenderTarget(cmd, originalBloomTexture);
			global::UnityEngine.Rendering.Blitter.DrawQuad(cmd, lensFlareShader, shaderPass5);
		}

		private static global::UnityEngine.Vector2 DoPaniniProjection(global::UnityEngine.Vector2 screenPos, float actualWidth, float actualHeight, float fieldOfView, float paniniProjectionCropToFit, float paniniProjectionDistance)
		{
			global::UnityEngine.Vector2 vector = CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			global::UnityEngine.Vector2 vector2 = CalcCropExtents(actualWidth, actualHeight, fieldOfView, paniniProjectionDistance);
			float a = vector2.x / vector.x;
			float b = vector2.y / vector.y;
			float value = global::UnityEngine.Mathf.Min(a, b);
			float num = global::UnityEngine.Mathf.Lerp(1f, global::UnityEngine.Mathf.Clamp01(value), paniniProjectionCropToFit);
			global::UnityEngine.Vector2 vector3 = Panini_Generic_Inv(new global::UnityEngine.Vector2(2f * screenPos.x - 1f, 2f * screenPos.y - 1f) * vector, paniniProjectionDistance) / (vector * num);
			return new global::UnityEngine.Vector2(0.5f * vector3.x + 0.5f, 0.5f * vector3.y + 0.5f);
		}

		private static global::UnityEngine.Vector2 CalcViewExtents(float actualWidth, float actualHeight, float fieldOfView)
		{
			float num = fieldOfView * (global::System.MathF.PI / 180f);
			float num2 = actualWidth / actualHeight;
			float num3 = global::UnityEngine.Mathf.Tan(0.5f * num);
			return new global::UnityEngine.Vector2(num2 * num3, num3);
		}

		private static global::UnityEngine.Vector2 CalcCropExtents(float actualWidth, float actualHeight, float fieldOfView, float d)
		{
			float num = 1f + d;
			global::UnityEngine.Vector2 vector = CalcViewExtents(actualWidth, actualHeight, fieldOfView);
			float num2 = global::UnityEngine.Mathf.Sqrt(vector.x * vector.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return vector * num3 * (num / num4);
		}

		private static global::UnityEngine.Vector2 Panini_Generic_Inv(global::UnityEngine.Vector2 projPos, float d)
		{
			float num = 1f + d;
			float num2 = global::UnityEngine.Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float num3 = 1f / num2;
			float num4 = num3 + d;
			return projPos * num3 * (num / num4);
		}
	}
}

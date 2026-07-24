namespace UnityEngine.Rendering
{
	public class DynamicResolutionHandler
	{
		private struct ScalerContainer
		{
			public global::UnityEngine.Rendering.DynamicResScalePolicyType type;

			public global::UnityEngine.Rendering.PerformDynamicRes method;
		}

		public enum UpsamplerScheduleType
		{
			BeforePost = 0,
			AfterDepthOfField = 1,
			AfterPost = 2
		}

		private bool m_Enabled;

		private bool m_UseMipBias;

		private float m_MinScreenFraction;

		private float m_MaxScreenFraction;

		private float m_CurrentFraction;

		private bool m_ForcingRes;

		private bool m_CurrentCameraRequest;

		private float m_PrevFraction;

		private bool m_ForceSoftwareFallback;

		private bool m_RunUpscalerFilterOnFullResolution;

		private float m_PrevHWScaleWidth;

		private float m_PrevHWScaleHeight;

		private global::UnityEngine.Vector2Int m_LastScaledSize;

		private static global::UnityEngine.Rendering.DynamicResScalerSlot s_ActiveScalerSlot = global::UnityEngine.Rendering.DynamicResScalerSlot.User;

		private static global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer[] s_ScalerContainers = new global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer[2]
		{
			new global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer
			{
				type = global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = DefaultDynamicResMethod
			},
			new global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer
			{
				type = global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsMinMaxLerpFactor,
				method = DefaultDynamicResMethod
			}
		};

		private global::UnityEngine.Vector2Int cachedOriginalSize;

		private static global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.DynamicResUpscaleFilter> s_CameraUpscaleFilters = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.DynamicResUpscaleFilter>();

		private global::UnityEngine.Rendering.DynamicResolutionType type;

		private global::UnityEngine.Rendering.GlobalDynamicResolutionSettings m_CachedSettings = global::UnityEngine.Rendering.GlobalDynamicResolutionSettings.NewDefault();

		private const int CameraDictionaryMaxcCapacity = 32;

		private global::System.WeakReference m_OwnerCameraWeakRef;

		private static global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.DynamicResolutionHandler> s_CameraInstances = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.DynamicResolutionHandler>(32);

		private static global::UnityEngine.Rendering.DynamicResolutionHandler s_DefaultInstance = new global::UnityEngine.Rendering.DynamicResolutionHandler();

		private static int s_ActiveCameraId = 0;

		private static global::UnityEngine.Rendering.DynamicResolutionHandler s_ActiveInstance = s_DefaultInstance;

		private static bool s_ActiveInstanceDirty = true;

		private static float s_GlobalHwFraction = 1f;

		private static bool s_GlobalHwUpresActive = false;

		private global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType m_UpsamplerSchedule = global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType.AfterPost;

		public global::UnityEngine.Rendering.DynamicResUpscaleFilter filter { get; private set; }

		public global::UnityEngine.Vector2Int finalViewport { get; set; }

		public bool runUpscalerFilterOnFullResolution
		{
			get
			{
				if (!m_RunUpscalerFilterOnFullResolution)
				{
					return filter == global::UnityEngine.Rendering.DynamicResUpscaleFilter.EdgeAdaptiveScalingUpres;
				}
				return true;
			}
			set
			{
				m_RunUpscalerFilterOnFullResolution = value;
			}
		}

		public bool forcingResolution => m_ForcingRes;

		public global::UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType upsamplerSchedule
		{
			get
			{
				return m_UpsamplerSchedule;
			}
			set
			{
				m_UpsamplerSchedule = value;
			}
		}

		public static global::UnityEngine.Rendering.DynamicResolutionHandler instance => s_ActiveInstance;

		private void Reset()
		{
			m_Enabled = false;
			m_UseMipBias = false;
			m_MinScreenFraction = 1f;
			m_MaxScreenFraction = 1f;
			m_CurrentFraction = 1f;
			m_ForcingRes = false;
			m_CurrentCameraRequest = true;
			m_PrevFraction = -1f;
			m_ForceSoftwareFallback = false;
			m_RunUpscalerFilterOnFullResolution = false;
			m_PrevHWScaleWidth = 1f;
			m_PrevHWScaleHeight = 1f;
			m_LastScaledSize = new global::UnityEngine.Vector2Int(0, 0);
			filter = global::UnityEngine.Rendering.DynamicResUpscaleFilter.CatmullRom;
		}

		private bool FlushScalableBufferManagerState()
		{
			if (s_GlobalHwUpresActive == HardwareDynamicResIsEnabled() && s_GlobalHwFraction == m_CurrentFraction)
			{
				return false;
			}
			s_GlobalHwUpresActive = HardwareDynamicResIsEnabled();
			s_GlobalHwFraction = m_CurrentFraction;
			float num = (s_GlobalHwUpresActive ? s_GlobalHwFraction : 1f);
			global::UnityEngine.ScalableBufferManager.ResizeBuffers(num, num);
			return true;
		}

		private static global::UnityEngine.Rendering.DynamicResolutionHandler GetOrCreateDrsInstanceHandler(global::UnityEngine.Camera camera)
		{
			if (camera == null)
			{
				return null;
			}
			global::UnityEngine.Rendering.DynamicResolutionHandler value = null;
			int instanceID = camera.GetInstanceID();
			if (!s_CameraInstances.TryGetValue(instanceID, out value))
			{
				if (s_CameraInstances.Count >= 32)
				{
					int key = 0;
					global::UnityEngine.Rendering.DynamicResolutionHandler dynamicResolutionHandler = null;
					foreach (global::System.Collections.Generic.KeyValuePair<int, global::UnityEngine.Rendering.DynamicResolutionHandler> s_CameraInstance in s_CameraInstances)
					{
						if (s_CameraInstance.Value.m_OwnerCameraWeakRef == null || !s_CameraInstance.Value.m_OwnerCameraWeakRef.IsAlive)
						{
							dynamicResolutionHandler = s_CameraInstance.Value;
							key = s_CameraInstance.Key;
							break;
						}
					}
					if (dynamicResolutionHandler != null)
					{
						value = dynamicResolutionHandler;
						s_CameraInstances.Remove(key);
						s_CameraUpscaleFilters.Remove(key);
					}
				}
				if (value == null)
				{
					value = new global::UnityEngine.Rendering.DynamicResolutionHandler();
					value.m_OwnerCameraWeakRef = new global::System.WeakReference(camera);
				}
				else
				{
					value.Reset();
					value.m_OwnerCameraWeakRef.Target = camera;
				}
				s_CameraInstances.Add(instanceID, value);
			}
			return value;
		}

		private DynamicResolutionHandler()
		{
			Reset();
		}

		private static float DefaultDynamicResMethod()
		{
			return 1f;
		}

		private void ProcessSettings(global::UnityEngine.Rendering.GlobalDynamicResolutionSettings settings)
		{
			m_Enabled = settings.enabled && (global::UnityEngine.Application.isPlaying || settings.forceResolution);
			if (!m_Enabled)
			{
				m_CurrentFraction = 1f;
			}
			else
			{
				type = settings.dynResType;
				m_UseMipBias = settings.useMipBias;
				float minScreenFraction = global::UnityEngine.Mathf.Clamp(settings.minPercentage / 100f, 0.1f, 1f);
				m_MinScreenFraction = minScreenFraction;
				float maxScreenFraction = global::UnityEngine.Mathf.Clamp(settings.maxPercentage / 100f, m_MinScreenFraction, 3f);
				m_MaxScreenFraction = maxScreenFraction;
				global::UnityEngine.Rendering.DynamicResUpscaleFilter value;
				bool flag = s_CameraUpscaleFilters.TryGetValue(s_ActiveCameraId, out value);
				filter = (flag ? value : settings.upsampleFilter);
				m_ForcingRes = settings.forceResolution;
				if (m_ForcingRes)
				{
					float currentFraction = global::UnityEngine.Mathf.Clamp(settings.forcedPercentage / 100f, 0.1f, 1.5f);
					m_CurrentFraction = currentFraction;
				}
			}
			m_CachedSettings = settings;
		}

		public global::UnityEngine.Vector2 GetResolvedScale()
		{
			if (!m_Enabled || !m_CurrentCameraRequest)
			{
				return new global::UnityEngine.Vector2(1f, 1f);
			}
			float x = m_CurrentFraction;
			float y = m_CurrentFraction;
			if (!m_ForceSoftwareFallback && type == global::UnityEngine.Rendering.DynamicResolutionType.Hardware)
			{
				x = global::UnityEngine.ScalableBufferManager.widthScaleFactor;
				y = global::UnityEngine.ScalableBufferManager.heightScaleFactor;
			}
			return new global::UnityEngine.Vector2(x, y);
		}

		public float CalculateMipBias(global::UnityEngine.Vector2Int inputResolution, global::UnityEngine.Vector2Int outputResolution, bool forceApply = false)
		{
			if (!m_UseMipBias && !forceApply)
			{
				return 0f;
			}
			return (float)global::System.Math.Log((double)inputResolution.x / (double)outputResolution.x, 2.0);
		}

		public static void SetDynamicResScaler(global::UnityEngine.Rendering.PerformDynamicRes scaler, global::UnityEngine.Rendering.DynamicResScalePolicyType scalerType = global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			s_ScalerContainers[0] = new global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		public static void SetSystemDynamicResScaler(global::UnityEngine.Rendering.PerformDynamicRes scaler, global::UnityEngine.Rendering.DynamicResScalePolicyType scalerType = global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
		{
			s_ScalerContainers[1] = new global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer
			{
				type = scalerType,
				method = scaler
			};
		}

		public static void SetActiveDynamicScalerSlot(global::UnityEngine.Rendering.DynamicResScalerSlot slot)
		{
			s_ActiveScalerSlot = slot;
		}

		public static void ClearSelectedCamera()
		{
			s_ActiveInstance = s_DefaultInstance;
			s_ActiveCameraId = 0;
			s_ActiveInstanceDirty = true;
		}

		public static void SetUpscaleFilter(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.DynamicResUpscaleFilter filter)
		{
			int instanceID = camera.GetInstanceID();
			if (s_CameraUpscaleFilters.ContainsKey(instanceID))
			{
				s_CameraUpscaleFilters[instanceID] = filter;
			}
			else
			{
				s_CameraUpscaleFilters.Add(instanceID, filter);
			}
		}

		public void SetCurrentCameraRequest(bool cameraRequest)
		{
			m_CurrentCameraRequest = cameraRequest;
		}

		public static void UpdateAndUseCamera(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.GlobalDynamicResolutionSettings? settings = null, global::System.Action OnResolutionChange = null)
		{
			int num;
			if (camera == null)
			{
				s_ActiveInstance = s_DefaultInstance;
				num = 0;
			}
			else
			{
				s_ActiveInstance = GetOrCreateDrsInstanceHandler(camera);
				num = camera.GetInstanceID();
			}
			s_ActiveInstanceDirty = num != s_ActiveCameraId;
			s_ActiveCameraId = num;
			s_ActiveInstance.Update(settings.HasValue ? settings.Value : s_ActiveInstance.m_CachedSettings, OnResolutionChange);
		}

		public void Update(global::UnityEngine.Rendering.GlobalDynamicResolutionSettings settings, global::System.Action OnResolutionChange = null)
		{
			ProcessSettings(settings);
			if (!m_Enabled || !s_ActiveInstanceDirty)
			{
				FlushScalableBufferManagerState();
				s_ActiveInstanceDirty = false;
				return;
			}
			if (!m_ForcingRes)
			{
				ref global::UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer reference = ref s_ScalerContainers[(int)s_ActiveScalerSlot];
				if (reference.type == global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsMinMaxLerpFactor)
				{
					float t = global::UnityEngine.Mathf.Clamp(reference.method(), 0f, 1f);
					m_CurrentFraction = global::UnityEngine.Mathf.Lerp(m_MinScreenFraction, m_MaxScreenFraction, t);
				}
				else if (reference.type == global::UnityEngine.Rendering.DynamicResScalePolicyType.ReturnsPercentage)
				{
					float num = global::UnityEngine.Mathf.Max(reference.method(), 5f);
					m_CurrentFraction = global::UnityEngine.Mathf.Clamp(num / 100f, m_MinScreenFraction, m_MaxScreenFraction);
				}
			}
			bool flag = false;
			bool num2 = m_CurrentFraction != m_PrevFraction;
			m_PrevFraction = m_CurrentFraction;
			if (!m_ForceSoftwareFallback && type == global::UnityEngine.Rendering.DynamicResolutionType.Hardware)
			{
				flag = FlushScalableBufferManagerState();
				if (global::UnityEngine.ScalableBufferManager.widthScaleFactor != m_PrevHWScaleWidth || global::UnityEngine.ScalableBufferManager.heightScaleFactor != m_PrevHWScaleHeight)
				{
					flag = true;
				}
			}
			if (num2 || flag)
			{
				OnResolutionChange?.Invoke();
			}
			s_ActiveInstanceDirty = false;
			m_PrevHWScaleWidth = global::UnityEngine.ScalableBufferManager.widthScaleFactor;
			m_PrevHWScaleHeight = global::UnityEngine.ScalableBufferManager.heightScaleFactor;
		}

		public bool SoftwareDynamicResIsEnabled()
		{
			if (m_CurrentCameraRequest && m_Enabled && (m_CurrentFraction != 1f || runUpscalerFilterOnFullResolution))
			{
				if (!m_ForceSoftwareFallback)
				{
					return type == global::UnityEngine.Rendering.DynamicResolutionType.Software;
				}
				return true;
			}
			return false;
		}

		public bool HardwareDynamicResIsEnabled()
		{
			if (!m_ForceSoftwareFallback && m_CurrentCameraRequest && m_Enabled)
			{
				return type == global::UnityEngine.Rendering.DynamicResolutionType.Hardware;
			}
			return false;
		}

		public bool RequestsHardwareDynamicResolution()
		{
			if (m_ForceSoftwareFallback)
			{
				return false;
			}
			return type == global::UnityEngine.Rendering.DynamicResolutionType.Hardware;
		}

		public bool DynamicResolutionEnabled()
		{
			if (m_CurrentCameraRequest && m_Enabled)
			{
				if (m_CurrentFraction == 1f)
				{
					return runUpscalerFilterOnFullResolution;
				}
				return true;
			}
			return false;
		}

		public void ForceSoftwareFallback()
		{
			m_ForceSoftwareFallback = true;
		}

		public global::UnityEngine.Vector2Int GetScaledSize(global::UnityEngine.Vector2Int size)
		{
			cachedOriginalSize = size;
			if (!m_Enabled || !m_CurrentCameraRequest)
			{
				return size;
			}
			return m_LastScaledSize = ApplyScalesOnSize(size);
		}

		public global::UnityEngine.Vector2Int ApplyScalesOnSize(global::UnityEngine.Vector2Int size)
		{
			return ApplyScalesOnSize(size, GetResolvedScale());
		}

		internal global::UnityEngine.Vector2Int ApplyScalesOnSize(global::UnityEngine.Vector2Int size, global::UnityEngine.Vector2 scales)
		{
			global::UnityEngine.Vector2Int result = new global::UnityEngine.Vector2Int(global::UnityEngine.Mathf.CeilToInt((float)size.x * scales.x), global::UnityEngine.Mathf.CeilToInt((float)size.y * scales.y));
			if (m_ForceSoftwareFallback || type != global::UnityEngine.Rendering.DynamicResolutionType.Hardware)
			{
				result.x += 1 & result.x;
				result.y += 1 & result.y;
			}
			result.x = global::System.Math.Min(result.x, size.x);
			result.y = global::System.Math.Min(result.y, size.y);
			return result;
		}

		public float GetCurrentScale()
		{
			if (!m_Enabled || !m_CurrentCameraRequest)
			{
				return 1f;
			}
			return m_CurrentFraction;
		}

		public global::UnityEngine.Vector2Int GetLastScaledSize()
		{
			return m_LastScaledSize;
		}

		public float GetLowResMultiplier(float targetLowRes)
		{
			return GetLowResMultiplier(targetLowRes, m_CachedSettings.lowResTransparencyMinimumThreshold);
		}

		public float GetLowResMultiplier(float targetLowRes, float minimumThreshold)
		{
			if (!m_Enabled)
			{
				return targetLowRes;
			}
			float num = global::System.Math.Min(minimumThreshold / 100f, targetLowRes);
			if (targetLowRes * m_CurrentFraction >= num)
			{
				return targetLowRes;
			}
			return global::UnityEngine.Mathf.Clamp(num / m_CurrentFraction, 0f, 1f);
		}
	}
}

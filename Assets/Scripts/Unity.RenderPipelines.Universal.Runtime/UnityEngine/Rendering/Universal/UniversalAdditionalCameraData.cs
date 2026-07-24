namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
	[global::UnityEngine.ExecuteAlways]
	public class UniversalAdditionalCameraData : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.Rendering.IAdditionalData
	{
		private enum Version
		{
			Initial = 0,
			DepthAndOpaqueTextureOptions = 2,
			Count = 3
		}

		private const string k_GizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/";

		private const string k_BaseCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		private const string k_OverlayCameraGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";

		private const string k_PostProcessingGizmoPath = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png";

		[global::UnityEngine.Serialization.FormerlySerializedAs("renderShadows")]
		[global::UnityEngine.SerializeField]
		private bool m_RenderShadows = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.CameraOverrideOption m_RequiresDepthTextureOption = global::UnityEngine.Rendering.Universal.CameraOverrideOption.UsePipelineSettings;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.CameraOverrideOption m_RequiresOpaqueTextureOption = global::UnityEngine.Rendering.Universal.CameraOverrideOption.UsePipelineSettings;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.CameraRenderType m_CameraType;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Camera> m_Cameras = new global::System.Collections.Generic.List<global::UnityEngine.Camera>();

		[global::UnityEngine.SerializeField]
		private int m_RendererIndex = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.LayerMask m_VolumeLayerMask = 1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Transform m_VolumeTrigger;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode m_VolumeFrameworkUpdateModeOption = global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode.UsePipelineSettings;

		[global::UnityEngine.SerializeField]
		private bool m_RenderPostProcessing;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.AntialiasingMode m_Antialiasing;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.AntialiasingQuality m_AntialiasingQuality = global::UnityEngine.Rendering.Universal.AntialiasingQuality.High;

		[global::UnityEngine.SerializeField]
		private bool m_StopNaN;

		[global::UnityEngine.SerializeField]
		private bool m_Dithering;

		[global::UnityEngine.SerializeField]
		private bool m_ClearDepth = true;

		[global::UnityEngine.SerializeField]
		private bool m_AllowXRRendering = true;

		[global::UnityEngine.SerializeField]
		private bool m_AllowHDROutput = true;

		[global::UnityEngine.SerializeField]
		private bool m_UseScreenCoordOverride;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector4 m_ScreenSizeOverride;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector4 m_ScreenCoordScaleBias;

		[global::System.NonSerialized]
		private global::UnityEngine.Camera m_Camera;

		[global::UnityEngine.Serialization.FormerlySerializedAs("requiresDepthTexture")]
		[global::UnityEngine.SerializeField]
		private bool m_RequiresDepthTexture;

		[global::UnityEngine.Serialization.FormerlySerializedAs("requiresColorTexture")]
		[global::UnityEngine.SerializeField]
		private bool m_RequiresColorTexture;

		[global::System.NonSerialized]
		private global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData m_MotionVectorsPersistentData = new global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData();

		[global::System.NonSerialized]
		internal global::UnityEngine.Rendering.Universal.UniversalCameraHistory m_History = new global::UnityEngine.Rendering.Universal.UniversalCameraHistory();

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.Universal.TemporalAA.Settings m_TaaSettings = global::UnityEngine.Rendering.Universal.TemporalAA.Settings.Create();

		private static global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData s_DefaultAdditionalCameraData;

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeStack> s_CachedVolumeStacks;

		private global::UnityEngine.Rendering.VolumeStack m_VolumeStack;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.Count;

		internal static global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData defaultAdditionalCameraData
		{
			get
			{
				if (s_DefaultAdditionalCameraData == null)
				{
					s_DefaultAdditionalCameraData = new global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData();
				}
				return s_DefaultAdditionalCameraData;
			}
		}

		internal global::UnityEngine.Camera camera
		{
			get
			{
				if (!m_Camera)
				{
					base.gameObject.TryGetComponent<global::UnityEngine.Camera>(out m_Camera);
				}
				return m_Camera;
			}
		}

		public bool renderShadows
		{
			get
			{
				return m_RenderShadows;
			}
			set
			{
				m_RenderShadows = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.CameraOverrideOption requiresDepthOption
		{
			get
			{
				return m_RequiresDepthTextureOption;
			}
			set
			{
				m_RequiresDepthTextureOption = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.CameraOverrideOption requiresColorOption
		{
			get
			{
				return m_RequiresOpaqueTextureOption;
			}
			set
			{
				m_RequiresOpaqueTextureOption = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.CameraRenderType renderType
		{
			get
			{
				return m_CameraType;
			}
			set
			{
				m_CameraType = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.Camera> cameraStack
		{
			get
			{
				if (renderType != global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
				{
					global::UnityEngine.Camera component = base.gameObject.GetComponent<global::UnityEngine.Camera>();
					global::UnityEngine.Debug.LogWarning($"{component.name}: This camera is of {renderType} type. Only Base cameras can have a camera stack.");
					return null;
				}
				if (!scriptableRenderer.SupportsCameraStackingType(global::UnityEngine.Rendering.Universal.CameraRenderType.Base))
				{
					global::UnityEngine.Camera component2 = base.gameObject.GetComponent<global::UnityEngine.Camera>();
					global::UnityEngine.Debug.LogWarning($"{component2.name}: This camera has a ScriptableRenderer that doesn't support camera stacking. Camera stack is null.");
					return null;
				}
				return m_Cameras;
			}
		}

		public bool clearDepth => m_ClearDepth;

		public bool requiresDepthTexture
		{
			get
			{
				if (m_RequiresDepthTextureOption == global::UnityEngine.Rendering.Universal.CameraOverrideOption.UsePipelineSettings)
				{
					return global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.supportsCameraDepthTexture;
				}
				return m_RequiresDepthTextureOption == global::UnityEngine.Rendering.Universal.CameraOverrideOption.On;
			}
			set
			{
				m_RequiresDepthTextureOption = (value ? global::UnityEngine.Rendering.Universal.CameraOverrideOption.On : global::UnityEngine.Rendering.Universal.CameraOverrideOption.Off);
			}
		}

		public bool requiresColorTexture
		{
			get
			{
				if (m_RequiresOpaqueTextureOption == global::UnityEngine.Rendering.Universal.CameraOverrideOption.UsePipelineSettings)
				{
					return global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.supportsCameraOpaqueTexture;
				}
				return m_RequiresOpaqueTextureOption == global::UnityEngine.Rendering.Universal.CameraOverrideOption.On;
			}
			set
			{
				m_RequiresOpaqueTextureOption = (value ? global::UnityEngine.Rendering.Universal.CameraOverrideOption.On : global::UnityEngine.Rendering.Universal.CameraOverrideOption.Off);
			}
		}

		public global::UnityEngine.Rendering.Universal.ScriptableRenderer scriptableRenderer
		{
			get
			{
				if ((object)global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset == null)
				{
					return null;
				}
				if (!global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.ValidateRendererData(m_RendererIndex))
				{
					int defaultRendererIndex = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.m_DefaultRendererIndex;
					global::UnityEngine.Rendering.Universal.ScriptableRendererData scriptableRendererData = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.m_RendererDataList[defaultRendererIndex];
					global::UnityEngine.Debug.LogWarning("Renderer at <b>index " + m_RendererIndex + "</b> is missing for camera <b>" + camera.name + "</b>, falling back to Default Renderer. <b>" + scriptableRendererData?.name + "</b>", global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset);
					return global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.GetRenderer(defaultRendererIndex);
				}
				return global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.GetRenderer(m_RendererIndex);
			}
		}

		public global::UnityEngine.LayerMask volumeLayerMask
		{
			get
			{
				return m_VolumeLayerMask;
			}
			set
			{
				m_VolumeLayerMask = value;
			}
		}

		public global::UnityEngine.Transform volumeTrigger
		{
			get
			{
				return m_VolumeTrigger;
			}
			set
			{
				m_VolumeTrigger = value;
			}
		}

		internal global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode volumeFrameworkUpdateMode
		{
			get
			{
				return m_VolumeFrameworkUpdateModeOption;
			}
			set
			{
				m_VolumeFrameworkUpdateModeOption = value;
			}
		}

		public bool requiresVolumeFrameworkUpdate
		{
			get
			{
				if (m_VolumeFrameworkUpdateModeOption == global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode.UsePipelineSettings)
				{
					return global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.volumeFrameworkUpdateMode != global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode.ViaScripting;
				}
				return m_VolumeFrameworkUpdateModeOption == global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode.EveryFrame;
			}
		}

		public global::UnityEngine.Rendering.VolumeStack volumeStack
		{
			get
			{
				return m_VolumeStack;
			}
			set
			{
				if (value == null && m_VolumeStack != null && m_VolumeStack.isValid)
				{
					if (s_CachedVolumeStacks == null)
					{
						s_CachedVolumeStacks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeStack>(4);
					}
					s_CachedVolumeStacks.Add(m_VolumeStack);
				}
				m_VolumeStack = value;
			}
		}

		public bool renderPostProcessing
		{
			get
			{
				return m_RenderPostProcessing;
			}
			set
			{
				m_RenderPostProcessing = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.AntialiasingMode antialiasing
		{
			get
			{
				return m_Antialiasing;
			}
			set
			{
				m_Antialiasing = value;
			}
		}

		public global::UnityEngine.Rendering.Universal.AntialiasingQuality antialiasingQuality
		{
			get
			{
				return m_AntialiasingQuality;
			}
			set
			{
				m_AntialiasingQuality = value;
			}
		}

		public ref global::UnityEngine.Rendering.Universal.TemporalAA.Settings taaSettings => ref m_TaaSettings;

		public global::UnityEngine.Rendering.ICameraHistoryReadAccess history => m_History;

		internal global::UnityEngine.Rendering.Universal.UniversalCameraHistory historyManager => m_History;

		internal global::UnityEngine.Rendering.Universal.MotionVectorsPersistentData motionVectorsPersistentData => m_MotionVectorsPersistentData;

		public bool resetHistory
		{
			get
			{
				return m_TaaSettings.resetHistoryFrames != 0;
			}
			set
			{
				m_TaaSettings.resetHistoryFrames += (value ? 1 : 0);
				m_MotionVectorsPersistentData.Reset();
				m_TaaSettings.jitterFrameCountOffset = -global::UnityEngine.Time.frameCount;
			}
		}

		public bool stopNaN
		{
			get
			{
				return m_StopNaN;
			}
			set
			{
				m_StopNaN = value;
			}
		}

		public bool dithering
		{
			get
			{
				return m_Dithering;
			}
			set
			{
				m_Dithering = value;
			}
		}

		public bool allowXRRendering
		{
			get
			{
				return m_AllowXRRendering;
			}
			set
			{
				m_AllowXRRendering = value;
			}
		}

		public bool useScreenCoordOverride
		{
			get
			{
				return m_UseScreenCoordOverride;
			}
			set
			{
				m_UseScreenCoordOverride = value;
			}
		}

		public global::UnityEngine.Vector4 screenSizeOverride
		{
			get
			{
				return m_ScreenSizeOverride;
			}
			set
			{
				m_ScreenSizeOverride = value;
			}
		}

		public global::UnityEngine.Vector4 screenCoordScaleBias
		{
			get
			{
				return m_ScreenCoordScaleBias;
			}
			set
			{
				m_ScreenCoordScaleBias = value;
			}
		}

		public bool allowHDROutput
		{
			get
			{
				return m_AllowHDROutput;
			}
			set
			{
				m_AllowHDROutput = value;
			}
		}

		[global::System.Obsolete("This field has been deprecated. #from(6000.2)")]
		public float version => (float)m_Version;

		private void Start()
		{
			if (m_CameraType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
			{
				camera.clearFlags = global::UnityEngine.CameraClearFlags.Nothing;
			}
		}

		internal void UpdateCameraStack()
		{
			int count = m_Cameras.Count;
			m_Cameras.RemoveAll((global::UnityEngine.Camera cam) => cam == null);
			int count2 = m_Cameras.Count;
			int num = count - count2;
			if (num != 0)
			{
				global::UnityEngine.Debug.LogWarning(base.name + ": " + num + " camera overlay" + ((num > 1) ? "s" : "") + " no longer exists and will be removed from the camera stack.");
			}
		}

		public void SetRenderer(int index)
		{
			m_RendererIndex = index;
		}

		internal void GetOrCreateVolumeStack()
		{
			if (s_CachedVolumeStacks != null && s_CachedVolumeStacks.Count > 0)
			{
				int index = s_CachedVolumeStacks.Count - 1;
				global::UnityEngine.Rendering.VolumeStack volumeStack = s_CachedVolumeStacks[index];
				s_CachedVolumeStacks.RemoveAt(index);
				if (volumeStack.isValid)
				{
					this.volumeStack = volumeStack;
				}
			}
			if (this.volumeStack == null)
			{
				this.volumeStack = global::UnityEngine.Rendering.VolumeManager.instance.CreateStack();
			}
		}

		public void OnValidate()
		{
			if (m_CameraType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay && m_Camera != null)
			{
				m_Camera.clearFlags = global::UnityEngine.CameraClearFlags.Nothing;
			}
		}

		public void OnDrawGizmos()
		{
			string value = "";
			global::UnityEngine.Color white = global::UnityEngine.Color.white;
			if (m_CameraType == global::UnityEngine.Rendering.Universal.CameraRenderType.Base)
			{
				value = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			else if (m_CameraType == global::UnityEngine.Rendering.Universal.CameraRenderType.Overlay)
			{
				value = "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_Base.png";
			}
			if (!string.IsNullOrEmpty(value))
			{
				global::UnityEngine.Gizmos.DrawIcon(base.transform.position, value, allowScaling: true, white);
			}
			if (renderPostProcessing)
			{
				global::UnityEngine.Gizmos.DrawIcon(base.transform.position, "Packages/com.unity.render-pipelines.universal/Editor/Gizmos/Camera_PostProcessing.png", allowScaling: true, white);
			}
		}

		public void OnDestroy()
		{
			m_Camera.DestroyVolumeStack(this);
			if (camera.cameraType != global::UnityEngine.CameraType.SceneView)
			{
				GetRawRenderer()?.ReleaseRenderTargets();
			}
			m_History?.Dispose();
			m_History = null;
		}

		private global::UnityEngine.Rendering.Universal.ScriptableRenderer GetRawRenderer()
		{
			if ((object)global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset == null)
			{
				return null;
			}
			global::System.ReadOnlySpan<global::UnityEngine.Rendering.Universal.ScriptableRenderer> renderers = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.renderers;
			if (renderers == null || renderers.IsEmpty)
			{
				return null;
			}
			if (m_RendererIndex >= renderers.Length || m_RendererIndex < 0)
			{
				return null;
			}
			return renderers[m_RendererIndex];
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.Count)
			{
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.DepthAndOpaqueTextureOptions;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_Version == global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.Count)
			{
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.Initial;
			}
			if (m_Version < global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.DepthAndOpaqueTextureOptions)
			{
				m_RequiresDepthTextureOption = (m_RequiresDepthTexture ? global::UnityEngine.Rendering.Universal.CameraOverrideOption.On : global::UnityEngine.Rendering.Universal.CameraOverrideOption.Off);
				m_RequiresOpaqueTextureOption = (m_RequiresColorTexture ? global::UnityEngine.Rendering.Universal.CameraOverrideOption.On : global::UnityEngine.Rendering.Universal.CameraOverrideOption.Off);
				m_Version = global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData.Version.DepthAndOpaqueTextureOptions;
			}
		}
	}
}

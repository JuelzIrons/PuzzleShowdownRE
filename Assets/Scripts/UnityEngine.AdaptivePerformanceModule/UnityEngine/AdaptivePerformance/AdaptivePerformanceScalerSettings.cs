namespace UnityEngine.AdaptivePerformance
{
	[global::System.Serializable]
	public class AdaptivePerformanceScalerSettings
	{
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the application update rate using Application.TargetFramerate")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveFramerate = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Framerate",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.FillRate),
			minBound = 15f,
			maxBound = 60f,
			maxLevel = 45
		};

		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the resolution of all render targets that allow dynamic resolution.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveResolution = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Resolution",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.FillRate),
			maxLevel = 9,
			minBound = 0.5f,
			maxBound = 1f
		};

		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to control if dynamic batching is enabled.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveBatching = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Batching",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU,
			maxLevel = 1,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer for adjusting at what distance LODs are switched.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveLOD = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive LOD",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 3,
			minBound = 0.4f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the size of the palette used for color grading in URP.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveLut = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Lut",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU),
			maxLevel = 1,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the level of antialiasing.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveMSAA = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive MSAA",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.FillRate),
			maxLevel = 2,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the number of shadow cascades to be used.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveShadowCascade = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Shadow Cascade",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU),
			maxLevel = 2,
			minBound = 0f,
			maxBound = 1f
		};

		private const string obsoleteMsg = "AdaptiveShadowCascades has been renamed. Please use AdaptiveShadowCascade. (UnityUpgradable) -> AdaptiveShadowCascade";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change the distance at which shadows are rendered.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveShadowDistance = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Shadow Distance",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 3,
			minBound = 0.15f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the resolution of shadow maps.")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveShadowmapResolution = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Shadowmap Resolution",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 3,
			minBound = 0.15f,
			maxBound = 1f
		};

		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to adjust the quality of shadows.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveShadowQuality = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Shadow Quality",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High,
			target = (global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU | global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU),
			maxLevel = 3,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change if objects in the scene are sorted by depth before rendering to reduce overdraw.")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveSorting = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Sorting",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU,
			maxLevel = 1,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to disable transparent objects rendering")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveTransparency = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Transparency",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 1,
			minBound = 0f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change the view distance")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveViewDistance = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive View Distance",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 40,
			minBound = 50f,
			maxBound = 1000f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change physics properties")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptivePhysics = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Physics",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU,
			maxLevel = 5,
			minBound = 0.5f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change decal properties")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveDecals = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Decals",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU,
			maxLevel = 20,
			minBound = 0.01f,
			maxBound = 1f
		};

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Settings for a scaler used by the Indexer to change the layer culling distance")]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_AdaptiveLayerCulling = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase
		{
			name = "Adaptive Layer Culling",
			enabled = false,
			scale = 1f,
			visualImpact = global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium,
			target = global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU,
			maxLevel = 40,
			minBound = 0.01f,
			maxBound = 1f
		};

		private global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase> m_DefaultScalerSettings = null;

		internal static readonly global::System.Collections.Generic.List<global::System.Type> k_DefaultScalerNames = new global::System.Collections.Generic.List<global::System.Type>
		{
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveFramerate),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveBatching),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveLOD),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveLut),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveMSAA),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveResolution),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveShadowCascade),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveShadowDistance),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveShadowmapResolution),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveShadowQuality),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveTransparency),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveSorting),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveViewDistance),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptivePhysics),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveLayerCulling),
			typeof(global::UnityEngine.AdaptivePerformance.AdaptiveDecals)
		};

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveFramerate
		{
			get
			{
				return m_AdaptiveFramerate;
			}
			set
			{
				m_AdaptiveFramerate = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveResolution
		{
			get
			{
				return m_AdaptiveResolution;
			}
			set
			{
				m_AdaptiveResolution = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveBatching
		{
			get
			{
				return m_AdaptiveBatching;
			}
			set
			{
				m_AdaptiveBatching = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveLOD
		{
			get
			{
				return m_AdaptiveLOD;
			}
			set
			{
				m_AdaptiveLOD = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveLut
		{
			get
			{
				return m_AdaptiveLut;
			}
			set
			{
				m_AdaptiveLut = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveMSAA
		{
			get
			{
				return m_AdaptiveMSAA;
			}
			set
			{
				m_AdaptiveMSAA = value;
			}
		}

		[global::System.Obsolete("AdaptiveShadowCascades has been renamed. Please use AdaptiveShadowCascade. (UnityUpgradable) -> AdaptiveShadowCascade", false)]
		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveShadowCascades => AdaptiveShadowCascade;

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveShadowCascade
		{
			get
			{
				return m_AdaptiveShadowCascade;
			}
			set
			{
				m_AdaptiveShadowCascade = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveShadowDistance
		{
			get
			{
				return m_AdaptiveShadowDistance;
			}
			set
			{
				m_AdaptiveShadowDistance = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveShadowmapResolution
		{
			get
			{
				return m_AdaptiveShadowmapResolution;
			}
			set
			{
				m_AdaptiveShadowmapResolution = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveShadowQuality
		{
			get
			{
				return m_AdaptiveShadowQuality;
			}
			set
			{
				m_AdaptiveShadowQuality = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveSorting
		{
			get
			{
				return m_AdaptiveSorting;
			}
			set
			{
				m_AdaptiveSorting = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveTransparency
		{
			get
			{
				return m_AdaptiveTransparency;
			}
			set
			{
				m_AdaptiveTransparency = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveViewDistance
		{
			get
			{
				return m_AdaptiveViewDistance;
			}
			set
			{
				m_AdaptiveViewDistance = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptivePhysics
		{
			get
			{
				return m_AdaptivePhysics;
			}
			set
			{
				m_AdaptivePhysics = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveDecals
		{
			get
			{
				return m_AdaptiveDecals;
			}
			set
			{
				m_AdaptiveDecals = value;
			}
		}

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase AdaptiveLayerCulling
		{
			get
			{
				return m_AdaptiveLayerCulling;
			}
			set
			{
				m_AdaptiveLayerCulling = value;
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase> DefaultScalerSettings
		{
			get
			{
				if (m_DefaultScalerSettings == null || m_DefaultScalerSettings.Count == 0)
				{
					m_DefaultScalerSettings = new global::System.Collections.Generic.List<global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase>
					{
						AdaptiveFramerate, AdaptiveBatching, AdaptiveLOD, AdaptiveLut, AdaptiveMSAA, AdaptiveResolution, AdaptiveShadowCascade, AdaptiveShadowDistance, AdaptiveShadowmapResolution, AdaptiveShadowQuality,
						AdaptiveTransparency, AdaptiveSorting, AdaptiveViewDistance, AdaptivePhysics, AdaptiveLayerCulling, AdaptiveDecals
					};
				}
				return m_DefaultScalerSettings;
			}
		}

		public void ApplySettings(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettings settings)
		{
			if (settings != null)
			{
				ApplySettingsBase(AdaptiveFramerate, settings.AdaptiveFramerate);
				ApplySettingsBase(AdaptiveBatching, settings.AdaptiveBatching);
				ApplySettingsBase(AdaptiveLOD, settings.AdaptiveLOD);
				ApplySettingsBase(AdaptiveLut, settings.AdaptiveLut);
				ApplySettingsBase(AdaptiveMSAA, settings.AdaptiveMSAA);
				ApplySettingsBase(AdaptiveResolution, settings.AdaptiveResolution);
				ApplySettingsBase(AdaptiveShadowCascade, settings.AdaptiveShadowCascade);
				ApplySettingsBase(AdaptiveShadowDistance, settings.AdaptiveShadowDistance);
				ApplySettingsBase(AdaptiveShadowmapResolution, settings.AdaptiveShadowmapResolution);
				ApplySettingsBase(AdaptiveShadowQuality, settings.AdaptiveShadowQuality);
				ApplySettingsBase(AdaptiveTransparency, settings.AdaptiveTransparency);
				ApplySettingsBase(AdaptiveSorting, settings.AdaptiveSorting);
				ApplySettingsBase(AdaptiveViewDistance, settings.AdaptiveViewDistance);
				ApplySettingsBase(AdaptivePhysics, settings.AdaptivePhysics);
				ApplySettingsBase(AdaptiveLayerCulling, settings.AdaptiveLayerCulling);
				ApplySettingsBase(AdaptiveDecals, settings.AdaptiveDecals);
			}
		}

		private void ApplySettingsBase(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase destination, global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase sources)
		{
			destination.enabled = sources.enabled;
			destination.scale = sources.scale;
			destination.visualImpact = sources.visualImpact;
			destination.target = sources.target;
			destination.minBound = sources.minBound;
			destination.maxBound = sources.maxBound;
			destination.maxLevel = sources.maxLevel;
		}
	}
}

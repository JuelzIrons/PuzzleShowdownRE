namespace UnityEngine.AdaptivePerformance
{
	[global::UnityEngine.Scripting.RequireDerived]
	public abstract class AdaptivePerformanceScaler : global::UnityEngine.ScriptableObject
	{
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceIndexer m_Indexer;

		private int m_OverrideLevel = -1;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase m_defaultSetting = new global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase();

		protected global::UnityEngine.AdaptivePerformance.IAdaptivePerformanceSettings m_Settings;

		public virtual string Name
		{
			get
			{
				return m_defaultSetting.name;
			}
			set
			{
				if (!(m_defaultSetting.name == value))
				{
					m_defaultSetting.name = value;
				}
			}
		}

		public virtual bool Enabled
		{
			get
			{
				return m_defaultSetting.enabled;
			}
			set
			{
				if (m_defaultSetting.enabled != value)
				{
					m_defaultSetting.enabled = value;
				}
			}
		}

		public virtual float Scale
		{
			get
			{
				return m_defaultSetting.scale;
			}
			set
			{
				if (m_defaultSetting.scale != value)
				{
					m_defaultSetting.scale = value;
				}
			}
		}

		public virtual global::UnityEngine.AdaptivePerformance.ScalerVisualImpact VisualImpact
		{
			get
			{
				return m_defaultSetting.visualImpact;
			}
			set
			{
				if (m_defaultSetting.visualImpact != value)
				{
					m_defaultSetting.visualImpact = value;
				}
			}
		}

		public virtual global::UnityEngine.AdaptivePerformance.ScalerTarget Target
		{
			get
			{
				return m_defaultSetting.target;
			}
			set
			{
				if (m_defaultSetting.target != value)
				{
					m_defaultSetting.target = value;
				}
			}
		}

		public virtual int MaxLevel
		{
			get
			{
				return m_defaultSetting.maxLevel;
			}
			set
			{
				if (m_defaultSetting.maxLevel != value)
				{
					m_defaultSetting.maxLevel = value;
				}
			}
		}

		public virtual float MinBound
		{
			get
			{
				return m_defaultSetting.minBound;
			}
			set
			{
				if (m_defaultSetting.minBound != value)
				{
					m_defaultSetting.minBound = value;
				}
			}
		}

		public virtual float MaxBound
		{
			get
			{
				return m_defaultSetting.maxBound;
			}
			set
			{
				if (m_defaultSetting.maxBound != value)
				{
					m_defaultSetting.maxBound = value;
				}
			}
		}

		public int CurrentLevel { get; private set; }

		public bool IsMaxLevel => CurrentLevel == MaxLevel;

		public bool NotLeveled => CurrentLevel == 0;

		public int GpuImpact { get; internal set; }

		public int CpuImpact { get; internal set; }

		public global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase DefaultSetting
		{
			get
			{
				return m_defaultSetting;
			}
			set
			{
				m_defaultSetting = value;
			}
		}

		public int OverrideLevel
		{
			get
			{
				return m_OverrideLevel;
			}
			set
			{
				m_OverrideLevel = value;
				m_Indexer.UpdateOverrideLevel(this);
			}
		}

		public int CalculateCost()
		{
			global::UnityEngine.AdaptivePerformance.PerformanceBottleneck performanceBottleneck = global::UnityEngine.AdaptivePerformance.Holder.Instance.PerformanceStatus.PerformanceMetrics.PerformanceBottleneck;
			int num = 0;
			switch (VisualImpact)
			{
			case global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Low:
				num += CurrentLevel;
				break;
			case global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.Medium:
				num += CurrentLevel * 2;
				break;
			case global::UnityEngine.AdaptivePerformance.ScalerVisualImpact.High:
				num += CurrentLevel * 3;
				break;
			}
			if (performanceBottleneck == global::UnityEngine.AdaptivePerformance.PerformanceBottleneck.CPU && (Target & global::UnityEngine.AdaptivePerformance.ScalerTarget.CPU) == 0)
			{
				num = 6;
			}
			if (performanceBottleneck == global::UnityEngine.AdaptivePerformance.PerformanceBottleneck.GPU && (Target & global::UnityEngine.AdaptivePerformance.ScalerTarget.GPU) == 0)
			{
				num = 6;
			}
			if (performanceBottleneck == global::UnityEngine.AdaptivePerformance.PerformanceBottleneck.TargetFrameRate && (Target & global::UnityEngine.AdaptivePerformance.ScalerTarget.FillRate) == 0)
			{
				num = 6;
			}
			return num;
		}

		protected virtual void Awake()
		{
			if (global::UnityEngine.AdaptivePerformance.Holder.Instance != null)
			{
				m_Settings = global::UnityEngine.AdaptivePerformance.Holder.Instance.Settings;
				m_Indexer = global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer;
			}
		}

		internal void InitializeScaler()
		{
			if (global::UnityEngine.AdaptivePerformance.Holder.Instance != null)
			{
				m_Settings = global::UnityEngine.AdaptivePerformance.Holder.Instance.Settings;
				m_Indexer = global::UnityEngine.AdaptivePerformance.Holder.Instance.Indexer;
				EnableScaler();
			}
		}

		private void OnEnable()
		{
			EnableScaler();
		}

		internal void EnableScaler()
		{
			if (m_Indexer != null)
			{
				m_Indexer.AddScaler(this);
				OnEnabled();
			}
		}

		internal void RemoveScaler()
		{
			if (m_Indexer != null && m_Indexer.RemoveScaler(this))
			{
				OnDisabled();
			}
		}

		private void OnDisable()
		{
			RemoveScaler();
		}

		internal void IncreaseLevel()
		{
			if (IsMaxLevel)
			{
				global::UnityEngine.Debug.LogError("Cannot increase scaler level as it is already max.");
				return;
			}
			CurrentLevel++;
			OnLevelIncrease();
			OnLevel();
		}

		internal void DecreaseLevel()
		{
			if (NotLeveled)
			{
				global::UnityEngine.Debug.LogError("Cannot decrease scaler level as it is already 0.");
				return;
			}
			CurrentLevel--;
			OnLevelDecrease();
			OnLevel();
		}

		internal void Activate()
		{
			OnEnabled();
		}

		internal void Deactivate()
		{
			OnDisabled();
		}

		public void ApplyDefaultSetting(global::UnityEngine.AdaptivePerformance.AdaptivePerformanceScalerSettingsBase defaultSetting)
		{
			m_defaultSetting = defaultSetting;
		}

		protected bool ScaleChanged()
		{
			float scale = Scale;
			float num = (MaxBound - MinBound) / (float)MaxLevel;
			Scale = num * (float)(MaxLevel - CurrentLevel) + MinBound;
			return Scale != scale;
		}

		protected virtual void OnLevelIncrease()
		{
		}

		protected virtual void OnLevelDecrease()
		{
		}

		protected virtual void OnLevel()
		{
		}

		protected virtual void OnEnabled()
		{
		}

		protected virtual void OnDisabled()
		{
		}
	}
}

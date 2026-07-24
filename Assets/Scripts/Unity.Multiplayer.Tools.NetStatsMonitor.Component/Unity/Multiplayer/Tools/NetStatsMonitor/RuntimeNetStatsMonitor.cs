namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::UnityEngine.AddComponentMenu("Netcode/Runtime Network Stats Monitor")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.multiplayer.tools@latest/?subfolder=/manual/runtime-stats-monitor.html")]
	public class RuntimeNetStatsMonitor : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Visibility toggle to hide or show the on-screen display.")]
		private bool m_Visible = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Min(1f)]
		[global::UnityEngine.Tooltip("The maximum rate at which the Runtime Net Stats Monitor's on-screen display is updated (per second). The on-screen display will never be updated faster than the overall refresh rate.")]
		private double m_MaxRefreshRate = 30.0;

		public bool Visible
		{
			get
			{
				return m_Visible;
			}
			set
			{
				m_Visible = value;
				UpdateUiVisibility();
			}
		}

		public double MaxRefreshRate
		{
			get
			{
				return m_MaxRefreshRate;
			}
			set
			{
				m_MaxRefreshRate = global::System.Math.Max(value, 1.0);
			}
		}

		[field: global::UnityEngine.SerializeField]
		public global::UnityEngine.UIElements.StyleSheet CustomStyleSheet { get; set; }

		[field: global::UnityEngine.Tooltip("Optional panel settings that can be used to override the default. These panel settings can be used to control a number of things, including how the on-screen display of the Runtime Net Stats Monitor scales on different devices and displays. ")]
		[field: global::UnityEngine.SerializeField]
		public global::UnityEngine.UIElements.PanelSettings PanelSettingsOverride { get; set; }

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration Position { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration();

		[global::JetBrains.Annotations.CanBeNull]
		[field: global::UnityEngine.SerializeField]
		[field: global::UnityEngine.Tooltip("The configuration asset used to configure the information displayed in this Runtime Net Stats Monitor. The NetStatsMonitorConfiguration can created from the Create menu, or from C# using ScriptableObject.CreateInstance.")]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration Configuration { get; set; }

		[global::JetBrains.Annotations.CanBeNull]
		internal global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.RnsmComponentImplementation Implementation { get; private set; }

		private void Start()
		{
			Setup();
		}

		private void OnEnable()
		{
			Setup();
		}

		private void OnDisable()
		{
			Teardown();
		}

		private void OnDestroy()
		{
			Teardown();
		}

		private void OnValidate()
		{
			if (base.enabled)
			{
				ApplyConfiguration();
			}
			else
			{
				Teardown();
			}
		}

		internal void Setup()
		{
			SetupImplementation();
			UpdateUiVisibility();
		}

		internal void Teardown()
		{
			PerformRemainingImplementationTeardownSteps();
		}

		public void ApplyConfiguration()
		{
			if (Configuration != null)
			{
				Configuration.RecomputeConfigurationHash();
			}
			ConfigureImplementation();
			UpdateUiVisibility();
		}

		public void AddCustomValue(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, float value)
		{
			Implementation?.AddCustomValue(metricId, value);
		}

		private void UpdateUiVisibility()
		{
			Implementation?.UpdateUiVisibility(base.enabled, m_Visible);
		}

		private void SetupImplementation()
		{
			if (Implementation == null)
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.RnsmComponentImplementation rnsmComponentImplementation = (Implementation = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.RnsmComponentImplementation());
			}
			Implementation?.SetupAndConfigure(Configuration, Position, CustomStyleSheet, PanelSettingsOverride, MaxRefreshRate);
		}

		private void ConfigureImplementation()
		{
			Implementation?.Configure(Configuration, Position, CustomStyleSheet, PanelSettingsOverride);
		}

		private void PerformRemainingImplementationTeardownSteps()
		{
			Implementation?.Teardown();
			Implementation = null;
		}

		internal void Update()
		{
			if (Visible)
			{
				Implementation?.Update(Configuration, MaxRefreshRate);
			}
		}
	}
}

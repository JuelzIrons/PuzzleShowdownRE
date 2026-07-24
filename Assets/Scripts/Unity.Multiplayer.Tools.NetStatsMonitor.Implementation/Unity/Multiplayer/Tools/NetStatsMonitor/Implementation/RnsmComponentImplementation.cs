namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class RnsmComponentImplementation
	{
		private const double k_NoDataReceivedMessageDelaySeconds = 1.0;

		private const double k_MinCollectionInterval_PerFrame = 0.005;

		private const double k_MinCollectionInterval_PerSecond = 1.0;

		private static readonly global::UnityEngine.UIElements.PanelSettings k_DefaultPanelSettings;

		private static readonly global::UnityEngine.UIElements.StyleSheet k_DefaultStyleSheet;

		private double m_LastDisplayUpdateTime = double.MinValue;

		[global::JetBrains.Annotations.NotNull]
		private readonly global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator> m_Accumulators = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator>
		{
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame,
				new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator()
			},
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond,
				new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator()
			}
		};

		private readonly global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, bool> m_NewDataAvailable = new global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, bool>(value: false);

		[global::JetBrains.Annotations.NotNull]
		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory m_MultiStatHistory = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory();

		[global::JetBrains.Annotations.CanBeNull]
		private global::UnityEngine.UIElements.StyleSheet m_CustomStyleSheet;

		private int? m_PreviousConfigurationHash;

		private int? m_PreviousHistoryRequirementsHash;

		private global::Unity.Multiplayer.Tools.Adapters.UnsubscribeFromAllAdapters m_UnsubscribeFromAllAdapters;

		[global::JetBrains.Annotations.NotNull]
		internal global::System.Func<double> GetCurrentTime { get; set; } = () => global::UnityEngine.Time.timeAsDouble;

		internal global::UnityEngine.UIElements.UIDocument UiDoc { get; private set; }

		[global::JetBrains.Annotations.NotNull]
		internal global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.RnsmVisualElement RnsmVisualElement { get; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.RnsmVisualElement();

		internal event global::System.Action OnDisplayUpdate;

		private static double MinCollectionInterval(global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate)
		{
			return rate switch
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame => 0.005, 
				global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond => 1.0, 
				_ => throw new global::System.ArgumentOutOfRangeException(string.Format("Unhandled {0} {1}", "SampleRate", rate)), 
			};
		}

		static RnsmComponentImplementation()
		{
			k_DefaultPanelSettings = global::UnityEngine.Resources.Load<global::UnityEngine.UIElements.PanelSettings>("UnityMpToolsRnsmDefaultPanelSettings");
			k_DefaultStyleSheet = global::UnityEngine.Resources.Load<global::UnityEngine.UIElements.StyleSheet>("UnityMpToolsRnsmDefaultStyleSheet");
		}

		internal void UpdateUiVisibility(bool enabled, bool visible)
		{
			RnsmVisualElement.visible = enabled && visible;
		}

		internal RnsmComponentImplementation()
		{
			SubscribeToAllAdapters();
		}

		private void SubscribeToAllAdapters()
		{
			m_UnsubscribeFromAllAdapters = global::Unity.Multiplayer.Tools.Adapters.NetworkAdapters.SubscribeToAll(SubscribeToAdapter, UnsubscribeFromAdapter);
		}

		private void SubscribeToAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent>();
			if (component != null)
			{
				component.MetricCollectionEvent += OnMetricsReceived;
			}
		}

		private void UnsubscribeFromAdapter(global::Unity.Multiplayer.Tools.Adapters.INetworkAdapter adapter)
		{
			global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent component = adapter.GetComponent<global::Unity.Multiplayer.Tools.Adapters.IMetricCollectionEvent>();
			if (component != null)
			{
				component.MetricCollectionEvent -= OnMetricsReceived;
			}
		}

		private void SetupUiDoc()
		{
			if (UiDoc == null)
			{
				global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject();
				gameObject.name = "__NetStatsMonitorUiDocObject";
				gameObject.hideFlags |= global::UnityEngine.HideFlags.DontSave | global::UnityEngine.HideFlags.HideInHierarchy | global::UnityEngine.HideFlags.HideInInspector;
				UiDoc = gameObject.AddComponent<global::UnityEngine.UIElements.UIDocument>();
			}
			global::UnityEngine.UIElements.VisualElement rootVisualElement = UiDoc.rootVisualElement;
			if (RnsmVisualElement.parent != rootVisualElement)
			{
				rootVisualElement?.Add(RnsmVisualElement);
			}
			RnsmVisualElement.styleSheets.Add(k_DefaultStyleSheet);
		}

		internal void SetupAndConfigure(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration, global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration position, global::UnityEngine.UIElements.StyleSheet styleSheet, global::UnityEngine.UIElements.PanelSettings panelSettingsOverride, double maxRefreshRate)
		{
			SetupUiDoc();
			if (configuration != null)
			{
				configuration.RecomputeConfigurationHash();
			}
			Configure(configuration, position, styleSheet, panelSettingsOverride);
			double num = (m_LastDisplayUpdateTime = GetCurrentTime() - 1.0 / maxRefreshRate - double.Epsilon);
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				m_Accumulators[sampleRate].LastAccumulationTime = num;
				m_Accumulators[sampleRate].LastCollectionTime = num;
			}
		}

		internal void Teardown()
		{
			m_UnsubscribeFromAllAdapters?.Invoke();
			m_MultiStatHistory.Clear();
			if (UiDoc != null && UiDoc.gameObject != null)
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					global::UnityEngine.Object.Destroy(UiDoc.gameObject);
				}
				else
				{
					global::UnityEngine.Object.DestroyImmediate(UiDoc.gameObject);
				}
			}
		}

		internal void Configure(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration, global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration positionConfiguration, global::UnityEngine.UIElements.StyleSheet customStyleSheet, global::UnityEngine.UIElements.PanelSettings panelSettingsOverride)
		{
			Analytic(customStyleSheet, panelSettingsOverride, positionConfiguration);
			if (customStyleSheet != m_CustomStyleSheet)
			{
				if (m_CustomStyleSheet != null)
				{
					RnsmVisualElement.styleSheets.Remove(m_CustomStyleSheet);
				}
				if (customStyleSheet != null)
				{
					RnsmVisualElement.styleSheets.Add(customStyleSheet);
				}
				m_CustomStyleSheet = customStyleSheet;
			}
			UiDoc.panelSettings = ((panelSettingsOverride != null) ? panelSettingsOverride : k_DefaultPanelSettings);
			RnsmVisualElement.ApplyPosition(positionConfiguration);
			ApplyConfigurationChangesIfHashHasChanged(configuration);
		}

		private void Analytic(global::UnityEngine.UIElements.StyleSheet customStyleSheet, global::UnityEngine.UIElements.PanelSettings panelSettingsOverride, global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration positionConfiguration)
		{
		}

		private void ApplyConfigurationChangesIfHashHasChanged(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration)
		{
			int? num = ((configuration != null) ? configuration.ConfigurationHash : ((int?)null));
			if (num == m_PreviousConfigurationHash)
			{
				return;
			}
			RnsmVisualElement.UpdateConfiguration(configuration);
			int? num2 = ((configuration != null) ? new int?(configuration.GetHistoryRequirementsHash()) : ((int?)null));
			if (num2 != m_PreviousHistoryRequirementsHash)
			{
				m_PreviousHistoryRequirementsHash = num2;
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements requirements = global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistoryRequirements.FromConfiguration(configuration);
				for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
				{
					m_Accumulators[sampleRate].UpdateRequirements(requirements, sampleRate);
				}
				m_MultiStatHistory.UpdateRequirements(requirements);
			}
			m_PreviousConfigurationHash = num;
		}

		private void CollectStatsIfEnoughTimeHasElapsed(global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate rate, double time)
		{
			global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator statsAccumulator = m_Accumulators[rate];
			double num = time - statsAccumulator.LastCollectionTime;
			double num2 = MinCollectionInterval(rate);
			if (num > num2)
			{
				m_MultiStatHistory.Collect(rate, statsAccumulator, time);
			}
		}

		private void OnMetricsReceived(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
			double time = GetCurrentTime();
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				CollectStatsIfEnoughTimeHasElapsed(sampleRate, time);
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAggregator.UpdateAccumulatorWithStatsFromMetrics(metricCollection, m_Accumulators[sampleRate], time);
			}
		}

		internal void Update(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration, double maxRefreshRate)
		{
			ApplyConfigurationChangesIfHashHasChanged(configuration);
			double num = GetCurrentTime();
			double num2 = double.MinValue;
			bool flag = false;
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator statsAccumulator = m_Accumulators[sampleRate];
				if (statsAccumulator.HasAccumulatedStats)
				{
					CollectStatsIfEnoughTimeHasElapsed(sampleRate, num);
				}
				num2 = global::System.Math.Max(num2, statsAccumulator.LastAccumulationTime);
				bool flag2 = statsAccumulator.LastAccumulationTime > m_LastDisplayUpdateTime;
				m_NewDataAvailable[sampleRate] = flag2;
				flag = flag || flag2;
			}
			double num3 = num - m_LastDisplayUpdateTime;
			if (!(maxRefreshRate * num3 >= 1.0))
			{
				return;
			}
			if (!flag)
			{
				double num4 = num - num2;
				if (num4 > 1.0)
				{
					RnsmVisualElement.DisplayDataNotReceivedMessage(num4);
				}
			}
			else
			{
				RnsmVisualElement.UpdateDisplayData(m_MultiStatHistory, m_NewDataAvailable, num);
				this.OnDisplayUpdate?.Invoke();
				m_LastDisplayUpdateTime = num;
			}
		}

		public void AddCustomValue(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, float value)
		{
			double num = GetCurrentTime();
			for (global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate sampleRate = global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerFrame; sampleRate <= global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate.PerSecond; sampleRate = sampleRate.Next())
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.StatsAccumulator statsAccumulator = m_Accumulators[sampleRate];
				if (statsAccumulator.Contains(metricId))
				{
					CollectStatsIfEnoughTimeHasElapsed(sampleRate, num);
					statsAccumulator.Accumulate(metricId, value);
					statsAccumulator.LastAccumulationTime = num;
				}
			}
		}
	}
}

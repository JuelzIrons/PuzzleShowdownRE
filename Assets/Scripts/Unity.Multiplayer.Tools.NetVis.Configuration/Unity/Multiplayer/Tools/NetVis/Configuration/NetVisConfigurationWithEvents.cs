namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal class NetVisConfigurationWithEvents
	{
		private global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisConfiguration m_Configuration;

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisConfiguration Configuration
		{
			get
			{
				return m_Configuration;
			}
			set
			{
				if (value != m_Configuration)
				{
					global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisConfiguration configuration = m_Configuration;
					m_Configuration = value;
					if (m_Configuration.Metric != configuration.Metric)
					{
						this.MetricChanged?.Invoke(m_Configuration.Metric);
					}
					if (m_Configuration.Settings != configuration.Settings)
					{
						this.SettingsChanged?.Invoke(m_Configuration.Settings);
					}
					this.ConfigurationChanged?.Invoke(m_Configuration);
				}
			}
		}

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric Metric
		{
			get
			{
				return Configuration.Metric;
			}
			set
			{
				if (value != Configuration.Metric)
				{
					Configuration.Metric = value;
					this.MetricChanged?.Invoke(m_Configuration.Metric);
					this.ConfigurationChanged?.Invoke(m_Configuration);
				}
			}
		}

		public event global::System.Action<global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisConfiguration> ConfigurationChanged;

		public event global::System.Action<global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisMetric> MetricChanged;

		public event global::System.Action<global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisSettings> SettingsChanged;

		public NetVisConfigurationWithEvents(global::Unity.Multiplayer.Tools.NetVis.Configuration.NetVisConfiguration configuration)
		{
			m_Configuration = configuration;
		}

		public void NotifySettingsChanged()
		{
			this.SettingsChanged?.Invoke(m_Configuration.Settings);
			this.ConfigurationChanged?.Invoke(m_Configuration);
		}
	}
}

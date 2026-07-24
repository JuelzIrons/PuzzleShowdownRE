namespace UnityEngine.Rendering
{
	public class DebugDisplaySettingsStats<TProfileId> : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery where TProfileId : global::System.Enum
	{
		[global::UnityEngine.Rendering.DisplayInfo(name = "Display Stats", order = int.MinValue)]
		private class StatsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel
		{
			private readonly global::UnityEngine.Rendering.DebugDisplaySettingsStats<TProfileId> m_Data;

			public override global::UnityEngine.Rendering.DebugUI.Flags Flags => global::UnityEngine.Rendering.DebugUI.Flags.RuntimeOnly;

			public StatsPanel(global::UnityEngine.Rendering.DebugDisplaySettingsStats<TProfileId> displaySettingsStats)
			{
				m_Data = displaySettingsStats;
				m_Data.debugDisplayStats.EnableProfilingRecorders();
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget>();
				m_Data.debugDisplayStats.RegisterDebugUI(list);
				foreach (global::UnityEngine.Rendering.DebugUI.Widget item in list)
				{
					AddWidget(item);
				}
			}

			public override void Dispose()
			{
				m_Data.debugDisplayStats.DisableProfilingRecorders();
				base.Dispose();
			}
		}

		public global::UnityEngine.Rendering.DebugDisplayStats<TProfileId> debugDisplayStats { get; }

		public bool AreAnySettingsActive => false;

		public DebugDisplaySettingsStats(global::UnityEngine.Rendering.DebugDisplayStats<TProfileId> debugDisplayStats)
		{
			this.debugDisplayStats = debugDisplayStats;
		}

		public global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new global::UnityEngine.Rendering.DebugDisplaySettingsStats<TProfileId>.StatsPanel(this);
		}
	}
}

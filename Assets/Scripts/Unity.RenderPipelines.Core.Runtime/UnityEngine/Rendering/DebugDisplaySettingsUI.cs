namespace UnityEngine.Rendering
{
	public class DebugDisplaySettingsUI : global::UnityEngine.Rendering.IDebugData
	{
		private global::System.Collections.Generic.IEnumerable<global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable> m_DisposablePanels;

		private global::UnityEngine.Rendering.IDebugDisplaySettings m_Settings;

		private void Reset()
		{
			if (m_Settings != null)
			{
				m_Settings.Reset();
				UnregisterDebug();
				RegisterDebug(m_Settings);
				global::UnityEngine.Rendering.DebugManager.instance.RefreshEditor();
			}
		}

		public void RegisterDebug(global::UnityEngine.Rendering.IDebugDisplaySettings settings)
		{
			global::UnityEngine.Rendering.DebugManager debugManager = global::UnityEngine.Rendering.DebugManager.instance;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable> panels = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable>();
			debugManager.RegisterData(this);
			m_Settings = settings;
			m_DisposablePanels = panels;
			m_Settings.Add(new global::UnityEngine.Rendering.DebugDisplaySettingsRenderGraph());
			global::System.Action<global::UnityEngine.Rendering.IDebugDisplaySettingsData> onExecute = delegate(global::UnityEngine.Rendering.IDebugDisplaySettingsData data)
			{
				global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable debugDisplaySettingsPanelDisposable = data.CreatePanel();
				global::UnityEngine.Rendering.DebugUI.Widget[] widgets = debugDisplaySettingsPanelDisposable.Widgets;
				global::UnityEngine.Rendering.DebugUI.Panel panel = debugManager.GetPanel(debugDisplaySettingsPanelDisposable.PanelName, createIfNull: true, (debugDisplaySettingsPanelDisposable is global::UnityEngine.Rendering.DebugDisplaySettingsPanel debugDisplaySettingsPanel) ? debugDisplaySettingsPanel.Order : 0);
				global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children = panel.children;
				panel.flags = debugDisplaySettingsPanelDisposable.Flags;
				panels.Add(debugDisplaySettingsPanelDisposable);
				children.Add(widgets);
			};
			m_Settings.ForEach(onExecute);
		}

		public void UnregisterDebug()
		{
			global::UnityEngine.Rendering.DebugManager instance = global::UnityEngine.Rendering.DebugManager.instance;
			if (m_DisposablePanels != null)
			{
				foreach (global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable disposablePanel in m_DisposablePanels)
				{
					global::UnityEngine.Rendering.DebugUI.Widget[] widgets = disposablePanel.Widgets;
					string panelName = disposablePanel.PanelName;
					global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children = instance.GetPanel(panelName, createIfNull: true).children;
					disposablePanel.Dispose();
					children.Remove(widgets);
				}
				m_DisposablePanels = null;
			}
			instance.UnregisterData(this);
		}

		public global::System.Action GetReset()
		{
			return Reset;
		}
	}
}

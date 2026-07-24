namespace UnityEngine.Rendering
{
	public interface IDebugDisplaySettingsPanel
	{
		string PanelName { get; }

		global::UnityEngine.Rendering.DebugUI.Widget[] Widgets { get; }

		global::UnityEngine.Rendering.DebugUI.Flags Flags { get; }
	}
}

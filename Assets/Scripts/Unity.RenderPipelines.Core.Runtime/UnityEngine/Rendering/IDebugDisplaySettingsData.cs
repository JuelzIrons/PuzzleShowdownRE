namespace UnityEngine.Rendering
{
	public interface IDebugDisplaySettingsData : global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable CreatePanel();

		void Reset()
		{
		}
	}
}

namespace UnityEngine.Rendering
{
	public interface IDebugDisplaySettings
	{
		void Reset();

		void ForEach(global::System.Action<global::UnityEngine.Rendering.IDebugDisplaySettingsData> onExecute);

		global::UnityEngine.Rendering.IDebugDisplaySettingsData Add(global::UnityEngine.Rendering.IDebugDisplaySettingsData newData)
		{
			return null;
		}
	}
}

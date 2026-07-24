namespace UnityEngine.Rendering
{
	public interface IDefaultVolumeProfileSettings : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		global::UnityEngine.Rendering.VolumeProfile volumeProfile { get; set; }
	}
}

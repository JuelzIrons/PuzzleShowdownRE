namespace UnityEngine.Rendering
{
	[global::System.Obsolete("This is not longer supported Please use DebugDisplaySettingsVolume. #from(6000.2)")]
	public interface IVolumeDebugSettings2 : global::UnityEngine.Rendering.IVolumeDebugSettings
	{
		[global::System.Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(2023.2)")]
		global::System.Type targetRenderPipeline { get; }

		[global::System.Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(2023.2)")]
		global::System.Collections.Generic.List<(string, global::System.Type)> volumeComponentsPathAndType { get; }
	}
}

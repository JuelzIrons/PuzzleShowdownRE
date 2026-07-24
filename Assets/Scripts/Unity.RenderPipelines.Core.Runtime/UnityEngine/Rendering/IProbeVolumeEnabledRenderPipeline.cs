namespace UnityEngine.Rendering
{
	public interface IProbeVolumeEnabledRenderPipeline
	{
		bool supportProbeVolume { get; }

		global::UnityEngine.Rendering.ProbeVolumeSHBands maxSHBands { get; }

		[global::System.Obsolete("This field is no longer necessary. #from(2023.3)")]
		global::UnityEngine.Rendering.ProbeVolumeSceneData probeVolumeSceneData { get; }
	}
}

namespace UnityEngine.Rendering
{
	[global::System.Obsolete("This is not longer supported Please use DebugDisplaySettingsVolume. #from(6000.2)")]
	public interface IVolumeDebugSettings
	{
		int selectedComponent { get; set; }

		global::UnityEngine.Camera selectedCamera { get; }

		global::System.Collections.Generic.IEnumerable<global::UnityEngine.Camera> cameras { get; }

		int selectedCameraIndex { get; set; }

		global::UnityEngine.Rendering.VolumeStack selectedCameraVolumeStack { get; }

		global::UnityEngine.LayerMask selectedCameraLayerMask { get; }

		global::UnityEngine.Vector3 selectedCameraPosition { get; }

		global::System.Type selectedComponentType { get; set; }

		global::UnityEngine.Rendering.Volume[] GetVolumes();

		bool VolumeHasInfluence(global::UnityEngine.Rendering.Volume volume);

		bool RefreshVolumes(global::UnityEngine.Rendering.Volume[] newVolumes);

		float GetVolumeWeight(global::UnityEngine.Rendering.Volume volume);
	}
}

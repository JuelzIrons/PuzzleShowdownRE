namespace UnityEngine.Rendering.Universal
{
	[global::System.Obsolete("This is not longer supported Please use DebugDisplaySettingsVolume. #from(6000.2)")]
	public class UniversalRenderPipelineVolumeDebugSettings : global::UnityEngine.Rendering.VolumeDebugSettings<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>
	{
		public override global::UnityEngine.Rendering.VolumeStack selectedCameraVolumeStack
		{
			get
			{
				if (base.selectedCamera == null)
				{
					return null;
				}
				global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData component = base.selectedCamera.GetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
				if (component == null)
				{
					return null;
				}
				global::UnityEngine.Rendering.VolumeStack volumeStack = component.volumeStack;
				if (volumeStack != null)
				{
					return volumeStack;
				}
				return global::UnityEngine.Rendering.VolumeManager.instance.stack;
			}
		}

		public override global::UnityEngine.LayerMask selectedCameraLayerMask
		{
			get
			{
				if (base.selectedCamera != null && base.selectedCamera.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
				{
					return component.volumeLayerMask;
				}
				return 1;
			}
		}

		public override global::UnityEngine.Vector3 selectedCameraPosition
		{
			get
			{
				if (!(base.selectedCamera != null))
				{
					return global::UnityEngine.Vector3.zero;
				}
				return base.selectedCamera.transform.position;
			}
		}

		[global::System.Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(2023.2)")]
		public override global::System.Type targetRenderPipeline => typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipeline);
	}
}

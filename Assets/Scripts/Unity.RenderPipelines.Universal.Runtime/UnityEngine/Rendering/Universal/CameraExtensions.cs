namespace UnityEngine.Rendering.Universal
{
	public static class CameraExtensions
	{
		public static global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData GetUniversalAdditionalCameraData(this global::UnityEngine.Camera camera)
		{
			global::UnityEngine.GameObject gameObject = camera.gameObject;
			if (!gameObject.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var component))
			{
				return gameObject.AddComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
			}
			return component;
		}

		public static global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode GetVolumeFrameworkUpdateMode(this global::UnityEngine.Camera camera)
		{
			return camera.GetUniversalAdditionalCameraData().volumeFrameworkUpdateMode;
		}

		public static void SetVolumeFrameworkUpdateMode(this global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.VolumeFrameworkUpdateMode mode)
		{
			global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			if (universalAdditionalCameraData.volumeFrameworkUpdateMode != mode)
			{
				bool requiresVolumeFrameworkUpdate = universalAdditionalCameraData.requiresVolumeFrameworkUpdate;
				universalAdditionalCameraData.volumeFrameworkUpdateMode = mode;
				if (requiresVolumeFrameworkUpdate && !universalAdditionalCameraData.requiresVolumeFrameworkUpdate)
				{
					camera.UpdateVolumeStack(universalAdditionalCameraData);
				}
			}
		}

		public static void UpdateVolumeStack(this global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			camera.UpdateVolumeStack(universalAdditionalCameraData);
		}

		public static void UpdateVolumeStack(this global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cameraData)
		{
			if (!global::UnityEngine.Rendering.VolumeManager.instance.isInitialized)
			{
				global::UnityEngine.Debug.LogError("UpdateVolumeStack must not be called before VolumeManager.instance.Initialize. If you tries calling this from Awake or Start, try instead to use the RenderPipelineManager.activeRenderPipelineCreated callback to be sure your render pipeline is fully initialized before calling this.");
			}
			else if (!cameraData.requiresVolumeFrameworkUpdate)
			{
				if (cameraData.volumeStack == null)
				{
					cameraData.GetOrCreateVolumeStack();
				}
				camera.GetVolumeLayerMaskAndTrigger(cameraData, out var layerMask, out var trigger);
				global::UnityEngine.Rendering.VolumeManager.instance.Update(cameraData.volumeStack, trigger, layerMask);
			}
		}

		public static void DestroyVolumeStack(this global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
			camera.DestroyVolumeStack(universalAdditionalCameraData);
		}

		public static void DestroyVolumeStack(this global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cameraData)
		{
			if (!(cameraData == null) && cameraData.volumeStack != null)
			{
				cameraData.volumeStack = null;
			}
		}

		internal static void GetVolumeLayerMaskAndTrigger(this global::UnityEngine.Camera camera, global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cameraData, out global::UnityEngine.LayerMask layerMask, out global::UnityEngine.Transform trigger)
		{
			layerMask = 1;
			trigger = camera.transform;
			if (cameraData != null)
			{
				layerMask = cameraData.volumeLayerMask;
				trigger = ((cameraData.volumeTrigger != null) ? cameraData.volumeTrigger : trigger);
			}
			else if (camera.cameraType == global::UnityEngine.CameraType.SceneView)
			{
				global::UnityEngine.Camera main = global::UnityEngine.Camera.main;
				global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData component = null;
				if (main != null && main.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out component))
				{
					layerMask = component.volumeLayerMask;
				}
				trigger = ((component != null && component.volumeTrigger != null) ? component.volumeTrigger : trigger);
			}
		}
	}
}

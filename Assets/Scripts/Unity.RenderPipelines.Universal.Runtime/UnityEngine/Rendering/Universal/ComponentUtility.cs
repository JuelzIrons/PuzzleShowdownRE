namespace UnityEngine.Rendering.Universal
{
	public static class ComponentUtility
	{
		public static bool IsUniversalCamera(global::UnityEngine.Camera camera)
		{
			return camera.GetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>() != null;
		}

		public static bool IsUniversalLight(global::UnityEngine.Light light)
		{
			return light.GetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>() != null;
		}
	}
}

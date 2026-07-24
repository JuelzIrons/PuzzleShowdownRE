namespace UnityEngine.Rendering.Universal
{
	public static class LightExtensions
	{
		public static global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData GetUniversalAdditionalLightData(this global::UnityEngine.Light light)
		{
			global::UnityEngine.GameObject gameObject = light.gameObject;
			if (!gameObject.TryGetComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>(out var component))
			{
				return gameObject.AddComponent<global::UnityEngine.Rendering.Universal.UniversalAdditionalLightData>();
			}
			return component;
		}
	}
}

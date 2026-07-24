namespace UnityEngine.Rendering.Universal
{
	internal static class Light2DManager
	{
		private static global::UnityEngine.SortingLayer[] s_SortingLayers;

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D> lights { get; } = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.Light2D>();

		internal static void Initialize()
		{
		}

		internal static void Dispose()
		{
		}

		public static void RegisterLight(global::UnityEngine.Rendering.Universal.Light2D light)
		{
			lights.Add(light);
			ErrorIfDuplicateGlobalLight(light);
		}

		public static void DeregisterLight(global::UnityEngine.Rendering.Universal.Light2D light)
		{
			lights.Remove(light);
		}

		public static void ErrorIfDuplicateGlobalLight(global::UnityEngine.Rendering.Universal.Light2D light)
		{
			if (light.lightType != global::UnityEngine.Rendering.Universal.Light2D.LightType.Global)
			{
				return;
			}
			int[] targetSortingLayers = light.targetSortingLayers;
			foreach (int num in targetSortingLayers)
			{
				if (ContainsDuplicateGlobalLight(num, light.blendStyleIndex))
				{
					global::UnityEngine.Debug.LogError("More than one global light on layer " + global::UnityEngine.SortingLayer.IDToName(num) + " for light blend style index " + light.blendStyleIndex);
				}
			}
		}

		public static bool GetGlobalColor(int sortingLayerIndex, int blendStyleIndex, out global::UnityEngine.Color color)
		{
			bool flag = false;
			color = global::UnityEngine.Color.black;
			foreach (global::UnityEngine.Rendering.Universal.Light2D light in lights)
			{
				if (light.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && light.IsLitLayer(sortingLayerIndex))
				{
					if (true)
					{
						color = light.color * light.intensity;
						return true;
					}
					if (!flag)
					{
						color = light.color * light.intensity;
						flag = true;
					}
				}
			}
			return flag;
		}

		private static bool ContainsDuplicateGlobalLight(int sortingLayerIndex, int blendStyleIndex)
		{
			int num = 0;
			foreach (global::UnityEngine.Rendering.Universal.Light2D light in lights)
			{
				if (light.lightType == global::UnityEngine.Rendering.Universal.Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && light.IsLitLayer(sortingLayerIndex))
				{
					if (num > 0)
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		public static global::UnityEngine.SortingLayer[] GetCachedSortingLayer()
		{
			if (s_SortingLayers == null)
			{
				s_SortingLayers = global::UnityEngine.SortingLayer.layers;
			}
			return s_SortingLayers;
		}
	}
}

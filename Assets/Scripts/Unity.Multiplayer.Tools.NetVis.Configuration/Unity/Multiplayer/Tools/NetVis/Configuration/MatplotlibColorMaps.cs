namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal static class MatplotlibColorMaps
	{
		public static global::UnityEngine.Color GetViridis(float percentage)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetViridisColor(percentage, out var rgb);
			return new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
		}

		public static global::UnityEngine.Color GetPlasma(float percentage)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetPlasmaColor(percentage, out var rgb);
			return new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
		}

		public static global::UnityEngine.Color GetMagma(float percentage)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetMagmaColor(percentage, out var rgb);
			return new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
		}

		public static global::UnityEngine.Color GetInferno(float percentage)
		{
			global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetInfernoColor(percentage, out var rgb);
			return new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
		}

		public static global::UnityEngine.Color[] GenerateViridis(uint resolution)
		{
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[resolution];
			for (int i = 0; i < resolution; i++)
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetViridisColor((float)i / (float)resolution, out var rgb);
				array[i] = new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
			}
			return array;
		}

		public static global::UnityEngine.Color[] GeneratePlasma(uint resolution)
		{
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[resolution];
			for (int i = 0; i < resolution; i++)
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetPlasmaColor((float)i / (float)resolution, out var rgb);
				array[i] = new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
			}
			return array;
		}

		public static global::UnityEngine.Color[] GenerateMagma(uint resolution)
		{
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[resolution];
			for (int i = 0; i < resolution; i++)
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetMagmaColor((float)i / (float)resolution, out var rgb);
				array[i] = new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
			}
			return array;
		}

		public static global::UnityEngine.Color[] GenerateInferno(uint resolution)
		{
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[resolution];
			for (int i = 0; i < resolution; i++)
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibHelper.GetInfernoColor((float)i / (float)resolution, out var rgb);
				array[i] = new global::UnityEngine.Color(rgb.x, rgb.y, rgb.z);
			}
			return array;
		}
	}
}

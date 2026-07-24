namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal static class MeshShadingGradientExtensions
	{
		public static global::UnityEngine.Color Evaluate(this global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradient meshShadingFill, float percentage)
		{
			percentage = global::UnityEngine.Mathf.Clamp01(percentage);
			if (meshShadingFill.UseGradient)
			{
				return meshShadingFill.Gradient.Evaluate(percentage);
			}
			return meshShadingFill.Preset switch
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Viridis => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GetViridis(percentage), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Plasma => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GetPlasma(percentage), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Magma => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GetMagma(percentage), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Inferno => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GetInferno(percentage), 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
		}

		public static global::UnityEngine.Gradient ToGradient(this global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradient meshShadingFill)
		{
			if (!meshShadingFill.UseGradient)
			{
				return meshShadingFill.Preset.ToGradient();
			}
			return meshShadingFill.Gradient;
		}

		public static global::UnityEngine.Gradient ToGradient(this global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset preset)
		{
			global::UnityEngine.Color[] source = preset switch
			{
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Viridis => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GenerateViridis(8u), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Plasma => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GeneratePlasma(8u), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Magma => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GenerateMagma(8u), 
				global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.Inferno => global::Unity.Multiplayer.Tools.NetVis.Configuration.MatplotlibColorMaps.GenerateInferno(8u), 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
			return new global::UnityEngine.Gradient
			{
				colorKeys = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(source, (global::UnityEngine.Color color, int index) => new global::UnityEngine.GradientColorKey(color, (float)index / 8f)))
			};
		}
	}
}

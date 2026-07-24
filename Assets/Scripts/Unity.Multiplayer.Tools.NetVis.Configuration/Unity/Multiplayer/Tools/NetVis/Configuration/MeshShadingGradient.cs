namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	[global::System.Serializable]
	internal class MeshShadingGradient
	{
		[global::System.Diagnostics.CodeAnalysis.AllowNull]
		public global::UnityEngine.Gradient Gradient = new global::UnityEngine.Gradient();

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset Preset;

		public bool UseGradient => Preset == global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset.None;
	}
}

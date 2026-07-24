namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal class OwnershipSettings
	{
		public bool MeshShadingEnabled { get; set; } = true;

		public bool TextOverlayEnabled { get; set; } = true;

		public global::UnityEngine.Color ServerHostColor => global::Unity.Multiplayer.Tools.Common.Visualization.CategoricalColorPalette.GetColor(0);

		public event global::System.Action ColorsChanged;

		internal global::UnityEngine.Color GetClientColor(global::Unity.Multiplayer.Tools.Adapters.ClientId clientId)
		{
			return global::Unity.Multiplayer.Tools.Common.Visualization.CategoricalColorPalette.GetColor((int)clientId);
		}

		internal void SetCustomColor(global::Unity.Multiplayer.Tools.Adapters.ClientId clientId, global::UnityEngine.Color color)
		{
		}

		internal void ResetCustomColor(global::Unity.Multiplayer.Tools.Adapters.ClientId clientId)
		{
		}

		internal void ResetCustomColors()
		{
		}
	}
}

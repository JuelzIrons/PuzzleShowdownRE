namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	[global::System.Serializable]
	internal class NetVisCommonSettings
	{
		public const float k_SceneSaturationMin = 0f;

		public const float k_SceneSaturationMax = 1f;

		[global::UnityEngine.Range(0f, 1f)]
		public float SceneSaturation;

		public bool Outline { get; set; } = true;
	}
}

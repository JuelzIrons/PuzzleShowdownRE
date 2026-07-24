namespace Unity.Multiplayer.Tools.Common.Visualization
{
	internal static class CategoricalColorPalette
	{
		private static readonly global::UnityEngine.Color[] k_ColorPalette = new global::UnityEngine.Color[48]
		{
			new global::UnityEngine.Color(0f, 0.7058824f, 0.03137255f),
			new global::UnityEngine.Color(0.5568628f, 2f / 85f, 41f / 51f),
			new global::UnityEngine.Color(0f, 47f / 51f, 0.75686276f),
			new global::UnityEngine.Color(0.5254902f, 0.03137255f, 0.10980392f),
			new global::UnityEngine.Color(0f, 0.7607843f, 83f / 85f),
			new global::UnityEngine.Color(72f / 85f, 0.050980393f, 41f / 85f),
			new global::UnityEngine.Color(35f / 51f, 1f, 14f / 85f),
			new global::UnityEngine.Color(14f / 51f, 0.043137256f, 0.4392157f),
			new global::UnityEngine.Color(1f, 0.18039216f, 0.58431375f),
			new global::UnityEngine.Color(0f, 7f / 15f, 0.007843138f),
			new global::UnityEngine.Color(1f, 0.6392157f, 84f / 85f),
			new global::UnityEngine.Color(0f, 29f / 85f, 23f / 85f),
			new global::UnityEngine.Color(1f, 0.5294118f, 0.20784314f),
			new global::UnityEngine.Color(0f, 0.4745098f, 50f / 51f),
			new global::UnityEngine.Color(1f, 43f / 51f, 0.88235295f),
			new global::UnityEngine.Color(0f, 0.2509804f, 0.007843138f),
			new global::UnityEngine.Color(0f, 53f / 85f, 50f / 51f),
			new global::UnityEngine.Color(57f / 85f, 0.050980393f, 0.38039216f),
			new global::UnityEngine.Color(0f, 0.95686275f, 0.02745098f),
			new global::UnityEngine.Color(0.41960785f, 2f / 85f, 53f / 85f),
			new global::UnityEngine.Color(0f, 0.79607844f, 0.654902f),
			new global::UnityEngine.Color(74f / 85f, 0.050980393f, 0.18039216f),
			new global::UnityEngine.Color(0.4862745f, 1f, 50f / 51f),
			new global::UnityEngine.Color(0.3529412f, 2f / 51f, 0.2f),
			new global::UnityEngine.Color(1f, 22f / 85f, 0.20784314f),
			new global::UnityEngine.Color(0f, 19f / 51f, 0.8f),
			new global::UnityEngine.Color(1f, 0.6745098f, 66f / 85f),
			new global::UnityEngine.Color(0f, 0.3529412f, 0.003921569f),
			new global::UnityEngine.Color(1f, 0.4f, 0.99215686f),
			new global::UnityEngine.Color(0f, 29f / 51f, 39f / 85f),
			new global::UnityEngine.Color(1f, 0.8862745f, 19f / 85f),
			new global::UnityEngine.Color(0f, 16f / 85f, 37f / 85f),
			new global::UnityEngine.Color(0f, 35f / 51f, 0.5568628f),
			new global::UnityEngine.Color(0.69803923f, 0.02745098f, 0.14509805f),
			new global::UnityEngine.Color(0f, 0.8980392f, 0.972549f),
			new global::UnityEngine.Color(43f / 85f, 0.050980393f, 0.28627452f),
			new global::UnityEngine.Color(0f, 0.827451f, 0.007843138f),
			new global::UnityEngine.Color(0.7058824f, 2f / 51f, 84f / 85f),
			new global::UnityEngine.Color(0.5254902f, 1f, 74f / 85f),
			new global::UnityEngine.Color(19f / 51f, 3f / 85f, 4f / 51f),
			new global::UnityEngine.Color(79f / 85f, 0.050980393f, 0.99215686f),
			new global::UnityEngine.Color(0f, 23f / 51f, 0.36078432f),
			new global::UnityEngine.Color(1f, 37f / 51f, 0.20784314f),
			new global::UnityEngine.Color(0f, 24f / 85f, 0.61960787f),
			new global::UnityEngine.Color(1f, 0.47058824f, 0.6784314f),
			new global::UnityEngine.Color(0f, 0.58431375f, 1f / 85f),
			new global::UnityEngine.Color(1f, 71f / 85f, 0.99215686f),
			new global::UnityEngine.Color(0f, 0.23921569f, 16f / 85f)
		};

		public static int ColorCount => k_ColorPalette.Length;

		public static global::UnityEngine.Color GetColor(int id)
		{
			return k_ColorPalette[(id % ColorCount + ColorCount) % ColorCount];
		}
	}
}

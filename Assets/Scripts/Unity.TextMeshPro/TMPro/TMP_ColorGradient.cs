namespace TMPro
{
	[global::System.Serializable]
	[global::UnityEngine.ExcludeFromPreset]
	public class TMP_ColorGradient : global::UnityEngine.ScriptableObject
	{
		public global::TMPro.ColorMode colorMode = global::TMPro.ColorMode.FourCornersGradient;

		public global::UnityEngine.Color topLeft;

		public global::UnityEngine.Color topRight;

		public global::UnityEngine.Color bottomLeft;

		public global::UnityEngine.Color bottomRight;

		private const global::TMPro.ColorMode k_DefaultColorMode = global::TMPro.ColorMode.FourCornersGradient;

		private static readonly global::UnityEngine.Color k_DefaultColor = global::UnityEngine.Color.white;

		public TMP_ColorGradient()
		{
			colorMode = global::TMPro.ColorMode.FourCornersGradient;
			topLeft = k_DefaultColor;
			topRight = k_DefaultColor;
			bottomLeft = k_DefaultColor;
			bottomRight = k_DefaultColor;
		}

		public TMP_ColorGradient(global::UnityEngine.Color color)
		{
			colorMode = global::TMPro.ColorMode.FourCornersGradient;
			topLeft = color;
			topRight = color;
			bottomLeft = color;
			bottomRight = color;
		}

		public TMP_ColorGradient(global::UnityEngine.Color color0, global::UnityEngine.Color color1, global::UnityEngine.Color color2, global::UnityEngine.Color color3)
		{
			colorMode = global::TMPro.ColorMode.FourCornersGradient;
			topLeft = color0;
			topRight = color1;
			bottomLeft = color2;
			bottomRight = color3;
		}
	}
}

namespace Unity.VisualScripting
{
	public class StickyNote : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.IGraph>
	{
		public enum ColorEnum
		{
			Classic = 0,
			Black = 1,
			Dark = 2,
			Orange = 3,
			Green = 4,
			Blue = 5,
			Red = 6,
			Purple = 7,
			Teal = 8
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public static readonly global::UnityEngine.Color defaultColor = new global::UnityEngine.Color(0.969f, 0.91f, 0.624f);

		[global::Unity.VisualScripting.Serialize]
		public global::UnityEngine.Rect position { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public string title { get; set; } = "Sticky Note";

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorTextArea(minLines = 1f)]
		public string body { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public global::Unity.VisualScripting.StickyNote.ColorEnum colorTheme { get; set; }

		public static global::UnityEngine.Color GetStickyColor(global::Unity.VisualScripting.StickyNote.ColorEnum enumValue)
		{
			return enumValue switch
			{
				global::Unity.VisualScripting.StickyNote.ColorEnum.Black => new global::UnityEngine.Color(0.122f, 0.114f, 0.09f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Dark => new global::UnityEngine.Color(0.184f, 0.145f, 0.024f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Orange => new global::UnityEngine.Color(0.988f, 0.663f, 0.275f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Green => new global::UnityEngine.Color(0.376f, 0.886f, 0.655f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Blue => new global::UnityEngine.Color(0.518f, 0.725f, 0.855f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Red => new global::UnityEngine.Color(1f, 0.502f, 0.502f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Purple => new global::UnityEngine.Color(0.98f, 0.769f, 0.949f), 
				global::Unity.VisualScripting.StickyNote.ColorEnum.Teal => new global::UnityEngine.Color(0.475f, 0.878f, 0.89f), 
				_ => new global::UnityEngine.Color(0.969f, 0.91f, 0.624f), 
			};
		}

		public static global::UnityEngine.Color GetFontColor(global::Unity.VisualScripting.StickyNote.ColorEnum enumValue)
		{
			if ((uint)(enumValue - 1) <= 1u)
			{
				return global::UnityEngine.Color.white;
			}
			return global::UnityEngine.Color.black;
		}
	}
}

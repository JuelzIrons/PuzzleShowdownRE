namespace TMPro
{
	public static class TMP_Compatibility
	{
		public enum AnchorPositions
		{
			TopLeft = 0,
			Top = 1,
			TopRight = 2,
			Left = 3,
			Center = 4,
			Right = 5,
			BottomLeft = 6,
			Bottom = 7,
			BottomRight = 8,
			BaseLine = 9,
			None = 10
		}

		public static global::TMPro.TextAlignmentOptions ConvertTextAlignmentEnumValues(global::TMPro.TextAlignmentOptions oldValue)
		{
			return (int)oldValue switch
			{
				0 => global::TMPro.TextAlignmentOptions.TopLeft, 
				1 => global::TMPro.TextAlignmentOptions.Top, 
				2 => global::TMPro.TextAlignmentOptions.TopRight, 
				3 => global::TMPro.TextAlignmentOptions.TopJustified, 
				4 => global::TMPro.TextAlignmentOptions.Left, 
				5 => global::TMPro.TextAlignmentOptions.Center, 
				6 => global::TMPro.TextAlignmentOptions.Right, 
				7 => global::TMPro.TextAlignmentOptions.Justified, 
				8 => global::TMPro.TextAlignmentOptions.BottomLeft, 
				9 => global::TMPro.TextAlignmentOptions.Bottom, 
				10 => global::TMPro.TextAlignmentOptions.BottomRight, 
				11 => global::TMPro.TextAlignmentOptions.BottomJustified, 
				12 => global::TMPro.TextAlignmentOptions.BaselineLeft, 
				13 => global::TMPro.TextAlignmentOptions.Baseline, 
				14 => global::TMPro.TextAlignmentOptions.BaselineRight, 
				15 => global::TMPro.TextAlignmentOptions.BaselineJustified, 
				16 => global::TMPro.TextAlignmentOptions.MidlineLeft, 
				17 => global::TMPro.TextAlignmentOptions.Midline, 
				18 => global::TMPro.TextAlignmentOptions.MidlineRight, 
				19 => global::TMPro.TextAlignmentOptions.MidlineJustified, 
				20 => global::TMPro.TextAlignmentOptions.CaplineLeft, 
				21 => global::TMPro.TextAlignmentOptions.Capline, 
				22 => global::TMPro.TextAlignmentOptions.CaplineRight, 
				23 => global::TMPro.TextAlignmentOptions.CaplineJustified, 
				_ => global::TMPro.TextAlignmentOptions.TopLeft, 
			};
		}
	}
}

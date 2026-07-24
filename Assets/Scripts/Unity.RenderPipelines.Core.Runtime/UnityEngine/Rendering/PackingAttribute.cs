namespace UnityEngine.Rendering
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field, AllowMultiple = true)]
	public class PackingAttribute : global::System.Attribute
	{
		public string[] displayNames;

		public float[] range;

		public global::UnityEngine.Rendering.FieldPacking packingScheme;

		public int offsetInSource;

		public int sizeInBits;

		public bool isDirection;

		public bool sRGBDisplay;

		public bool checkIsNormalized;

		public string preprocessor;

		public PackingAttribute(string[] displayNames, global::UnityEngine.Rendering.FieldPacking packingScheme = global::UnityEngine.Rendering.FieldPacking.NoPacking, int bitSize = 32, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			this.displayNames = displayNames;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			sizeInBits = bitSize;
			range = new float[2] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}

		public PackingAttribute(string displayName = "", global::UnityEngine.Rendering.FieldPacking packingScheme = global::UnityEngine.Rendering.FieldPacking.NoPacking, int bitSize = 0, int offsetInSource = 0, float minValue = 0f, float maxValue = 1f, bool isDirection = false, bool sRGBDisplay = false, bool checkIsNormalized = false, string preprocessor = "")
		{
			displayNames = new string[1];
			displayNames[0] = displayName;
			this.packingScheme = packingScheme;
			this.offsetInSource = offsetInSource;
			this.isDirection = isDirection;
			this.sRGBDisplay = sRGBDisplay;
			this.checkIsNormalized = checkIsNormalized;
			sizeInBits = bitSize;
			range = new float[2] { minValue, maxValue };
			this.preprocessor = preprocessor;
		}
	}
}

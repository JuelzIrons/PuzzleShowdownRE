namespace UnityEngine.Rendering
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Field)]
	public class HLSLArray : global::System.Attribute
	{
		public int arraySize;

		public global::System.Type elementType;

		public HLSLArray(int arraySize, global::System.Type elementType)
		{
			this.arraySize = arraySize;
			this.elementType = elementType;
		}
	}
}

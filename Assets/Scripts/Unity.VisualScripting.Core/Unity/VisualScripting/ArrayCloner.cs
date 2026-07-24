namespace Unity.VisualScripting
{
	public sealed class ArrayCloner : global::Unity.VisualScripting.Cloner<global::System.Array>
	{
		public override bool Handles(global::System.Type type)
		{
			return type.IsArray;
		}

		public override global::System.Array ConstructClone(global::System.Type type, global::System.Array original)
		{
			return global::System.Array.CreateInstance(type.GetElementType(), 0);
		}

		public override void FillClone(global::System.Type type, ref global::System.Array clone, global::System.Array original, global::Unity.VisualScripting.CloningContext context)
		{
			int length = original.GetLength(0);
			clone = global::System.Array.CreateInstance(type.GetElementType(), length);
			for (int i = 0; i < length; i++)
			{
				clone.SetValue(global::Unity.VisualScripting.Cloning.Clone(context, original.GetValue(i)), i);
			}
		}
	}
}

namespace Unity.VisualScripting
{
	public sealed class ListCloner : global::Unity.VisualScripting.Cloner<global::System.Collections.IList>
	{
		public override bool Handles(global::System.Type type)
		{
			return typeof(global::System.Collections.IList).IsAssignableFrom(type);
		}

		public override void FillClone(global::System.Type type, ref global::System.Collections.IList clone, global::System.Collections.IList original, global::Unity.VisualScripting.CloningContext context)
		{
			if (context.tryPreserveInstances)
			{
				for (int i = 0; i < original.Count; i++)
				{
					object original2 = original[i];
					if (i < clone.Count)
					{
						object clone2 = clone[i];
						global::Unity.VisualScripting.Cloning.CloneInto(context, ref clone2, original2);
						clone[i] = clone2;
					}
					else
					{
						clone.Add(global::Unity.VisualScripting.Cloning.Clone(context, original2));
					}
				}
			}
			else
			{
				for (int j = 0; j < original.Count; j++)
				{
					object original3 = original[j];
					clone.Add(global::Unity.VisualScripting.Cloning.Clone(context, original3));
				}
			}
		}
	}
}

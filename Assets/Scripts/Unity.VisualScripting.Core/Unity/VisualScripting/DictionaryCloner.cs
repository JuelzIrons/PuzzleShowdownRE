namespace Unity.VisualScripting
{
	public sealed class DictionaryCloner : global::Unity.VisualScripting.Cloner<global::System.Collections.IDictionary>
	{
		public override bool Handles(global::System.Type type)
		{
			return typeof(global::System.Collections.IDictionary).IsAssignableFrom(type);
		}

		public override void FillClone(global::System.Type type, ref global::System.Collections.IDictionary clone, global::System.Collections.IDictionary original, global::Unity.VisualScripting.CloningContext context)
		{
			global::System.Collections.IDictionaryEnumerator enumerator = original.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object key = enumerator.Key;
				object value = enumerator.Value;
				object key2 = global::Unity.VisualScripting.Cloning.Clone(context, key);
				object value2 = global::Unity.VisualScripting.Cloning.Clone(context, value);
				clone.Add(key2, value2);
			}
		}
	}
}

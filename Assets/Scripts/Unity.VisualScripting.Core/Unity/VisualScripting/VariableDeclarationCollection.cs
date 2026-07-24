namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class VariableDeclarationCollection : global::System.Collections.ObjectModel.KeyedCollection<string, global::Unity.VisualScripting.VariableDeclaration>, global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.VariableDeclaration>, global::System.Collections.Generic.ICollection<global::Unity.VisualScripting.VariableDeclaration>, global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.VariableDeclaration>, global::System.Collections.IEnumerable
	{
		global::Unity.VisualScripting.VariableDeclaration global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.VariableDeclaration>.this[string key] => base[key];

		protected override string GetKeyForItem(global::Unity.VisualScripting.VariableDeclaration item)
		{
			return item.name;
		}

		public void EditorRename(global::Unity.VisualScripting.VariableDeclaration item, string newName)
		{
			ChangeItemKey(item, newName);
		}

		public new bool TryGetValue(string key, out global::Unity.VisualScripting.VariableDeclaration value)
		{
			if (base.Dictionary == null)
			{
				value = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.VariableDeclaration>.Contains(string key)
		{
			return Contains(key);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.VariableDeclaration>.Remove(string key)
		{
			return Remove(key);
		}
	}
}

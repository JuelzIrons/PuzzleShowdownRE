namespace Unity.VisualScripting
{
	public class GuidCollection<T> : global::System.Collections.ObjectModel.KeyedCollection<global::System.Guid, T>, global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable where T : global::Unity.VisualScripting.IIdentifiable
	{
		T global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, T>.this[global::System.Guid key] => base[key];

		protected override global::System.Guid GetKeyForItem(T item)
		{
			return item.guid;
		}

		protected override void InsertItem(int index, T item)
		{
			global::Unity.VisualScripting.Ensure.That("item").IsNotNull(item);
			base.InsertItem(index, item);
		}

		protected override void SetItem(int index, T item)
		{
			global::Unity.VisualScripting.Ensure.That("item").IsNotNull(item);
			base.SetItem(index, item);
		}

		public new bool TryGetValue(global::System.Guid key, out T value)
		{
			if (base.Dictionary == null)
			{
				value = default(T);
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, T>.Contains(global::System.Guid key)
		{
			return Contains(key);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, T>.Remove(global::System.Guid key)
		{
			return Remove(key);
		}
	}
}

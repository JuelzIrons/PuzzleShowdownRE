namespace Unity.VisualScripting
{
	public class WatchedList<T> : global::System.Collections.ObjectModel.Collection<T>, global::Unity.VisualScripting.INotifyCollectionChanged<T>
	{
		public event global::System.Action<T> ItemAdded;

		public event global::System.Action<T> ItemRemoved;

		public event global::System.Action CollectionChanged;

		protected override void InsertItem(int index, T item)
		{
			base.InsertItem(index, item);
			this.ItemAdded?.Invoke(item);
			this.CollectionChanged?.Invoke();
		}

		protected override void RemoveItem(int index)
		{
			if (index < base.Count)
			{
				T obj = base[index];
				base.RemoveItem(index);
				this.ItemRemoved?.Invoke(obj);
				this.CollectionChanged?.Invoke();
			}
		}

		protected override void ClearItems()
		{
			while (base.Count > 0)
			{
				RemoveItem(0);
			}
		}
	}
}

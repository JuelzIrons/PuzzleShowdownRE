namespace Unity.VisualScripting
{
	public abstract class NonNullableCollection<T> : global::System.Collections.ObjectModel.Collection<T>
	{
		protected override void InsertItem(int index, T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		protected override void SetItem(int index, T item)
		{
			if (item == null)
			{
				throw new global::System.ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		public void AddRange(global::System.Collections.Generic.IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				Add(item);
			}
		}
	}
}

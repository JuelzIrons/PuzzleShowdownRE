namespace Unity.VisualScripting
{
	public sealed class MergedGraphElementCollection : global::Unity.VisualScripting.MergedKeyedCollection<global::System.Guid, global::Unity.VisualScripting.IGraphElement>, global::Unity.VisualScripting.INotifyCollectionChanged<global::Unity.VisualScripting.IGraphElement>
	{
		public event global::System.Action<global::Unity.VisualScripting.IGraphElement> ItemAdded;

		public event global::System.Action<global::Unity.VisualScripting.IGraphElement> ItemRemoved;

		public event global::System.Action CollectionChanged;

		public override void Include<TSubItem>(global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TSubItem> collection)
		{
			base.Include(collection);
			if (collection is global::Unity.VisualScripting.IGraphElementCollection<TSubItem> graphElementCollection)
			{
				graphElementCollection.ItemAdded += delegate(TSubItem element)
				{
					this.ItemAdded?.Invoke(element);
				};
				graphElementCollection.ItemRemoved += delegate(TSubItem element)
				{
					this.ItemRemoved?.Invoke(element);
				};
				graphElementCollection.CollectionChanged += delegate
				{
					this.CollectionChanged?.Invoke();
				};
			}
		}
	}
}

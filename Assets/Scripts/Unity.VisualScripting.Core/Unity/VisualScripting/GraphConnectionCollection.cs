namespace Unity.VisualScripting
{
	public class GraphConnectionCollection<TConnection, TSource, TDestination> : global::Unity.VisualScripting.ConnectionCollectionBase<TConnection, TSource, TDestination, global::Unity.VisualScripting.GraphElementCollection<TConnection>>, global::Unity.VisualScripting.IGraphElementCollection<TConnection>, global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TConnection>, global::System.Collections.Generic.ICollection<TConnection>, global::System.Collections.Generic.IEnumerable<TConnection>, global::System.Collections.IEnumerable, global::Unity.VisualScripting.INotifyCollectionChanged<TConnection> where TConnection : global::Unity.VisualScripting.IConnection<TSource, TDestination>, global::Unity.VisualScripting.IGraphElement
	{
		TConnection global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TConnection>.this[global::System.Guid key] => collection[key];

		TConnection global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TConnection>.this[int index] => collection[index];

		public event global::System.Action<TConnection> ItemAdded
		{
			add
			{
				collection.ItemAdded += value;
			}
			remove
			{
				collection.ItemAdded -= value;
			}
		}

		public event global::System.Action<TConnection> ItemRemoved
		{
			add
			{
				collection.ItemRemoved += value;
			}
			remove
			{
				collection.ItemRemoved -= value;
			}
		}

		public event global::System.Action CollectionChanged
		{
			add
			{
				collection.CollectionChanged += value;
			}
			remove
			{
				collection.CollectionChanged -= value;
			}
		}

		public GraphConnectionCollection(global::Unity.VisualScripting.IGraph graph)
			: base(new global::Unity.VisualScripting.GraphElementCollection<TConnection>(graph))
		{
			collection.ProxyCollectionChange = true;
		}

		public bool TryGetValue(global::System.Guid key, out TConnection value)
		{
			return collection.TryGetValue(key, out value);
		}

		public bool Contains(global::System.Guid key)
		{
			return collection.Contains(key);
		}

		public bool Remove(global::System.Guid key)
		{
			if (Contains(key))
			{
				return Remove(collection[key]);
			}
			return false;
		}

		protected override void BeforeAdd(TConnection item)
		{
			collection.BeforeAdd(item);
		}

		protected override void AfterAdd(TConnection item)
		{
			collection.AfterAdd(item);
		}

		protected override void BeforeRemove(TConnection item)
		{
			collection.BeforeRemove(item);
		}

		protected override void AfterRemove(TConnection item)
		{
			collection.AfterRemove(item);
		}
	}
}

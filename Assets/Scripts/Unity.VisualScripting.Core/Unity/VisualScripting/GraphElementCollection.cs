namespace Unity.VisualScripting
{
	public sealed class GraphElementCollection<TElement> : global::Unity.VisualScripting.GuidCollection<TElement>, global::Unity.VisualScripting.IGraphElementCollection<TElement>, global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TElement>, global::System.Collections.Generic.ICollection<TElement>, global::System.Collections.Generic.IEnumerable<TElement>, global::System.Collections.IEnumerable, global::Unity.VisualScripting.INotifyCollectionChanged<TElement>, global::Unity.VisualScripting.IProxyableNotifyCollectionChanged<TElement> where TElement : global::Unity.VisualScripting.IGraphElement
	{
		public global::Unity.VisualScripting.IGraph graph { get; }

		public bool ProxyCollectionChange { get; set; }

		TElement global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TElement>.this[global::System.Guid key] => base[key];

		public event global::System.Action<TElement> ItemAdded;

		public event global::System.Action<TElement> ItemRemoved;

		public event global::System.Action CollectionChanged;

		public GraphElementCollection(global::Unity.VisualScripting.IGraph graph)
		{
			global::Unity.VisualScripting.Ensure.That("graph").IsNotNull(graph);
			this.graph = graph;
		}

		public void BeforeAdd(TElement element)
		{
			if (element.graph != null)
			{
				if (element.graph == graph)
				{
					throw new global::System.InvalidOperationException("Graph elements cannot be added multiple time into the same graph.");
				}
				throw new global::System.InvalidOperationException("Graph elements cannot be shared across graphs.");
			}
			global::Unity.VisualScripting.IGraph obj = graph;
			element.graph = obj;
			element.BeforeAdd();
		}

		public void AfterAdd(TElement element)
		{
			element.AfterAdd();
			this.ItemAdded?.Invoke(element);
			this.CollectionChanged?.Invoke();
		}

		public void BeforeRemove(TElement element)
		{
			element.BeforeRemove();
		}

		public void AfterRemove(TElement element)
		{
			element.graph = null;
			element.AfterRemove();
			this.ItemRemoved?.Invoke(element);
			this.CollectionChanged?.Invoke();
		}

		protected override void InsertItem(int index, TElement element)
		{
			global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
			if (!ProxyCollectionChange)
			{
				BeforeAdd(element);
			}
			base.InsertItem(index, element);
			if (!ProxyCollectionChange)
			{
				AfterAdd(element);
			}
		}

		protected override void RemoveItem(int index)
		{
			TElement val = base[index];
			if (!Contains(val))
			{
				throw new global::System.ArgumentOutOfRangeException("element");
			}
			if (!ProxyCollectionChange)
			{
				BeforeRemove(val);
			}
			base.RemoveItem(index);
			if (!ProxyCollectionChange)
			{
				AfterRemove(val);
			}
		}

		protected override void ClearItems()
		{
			global::System.Collections.Generic.List<TElement> list = global::Unity.VisualScripting.ListPool<TElement>.New();
			using (global::Unity.VisualScripting.NoAllocEnumerator<TElement> noAllocEnumerator = GetEnumerator())
			{
				while (noAllocEnumerator.MoveNext())
				{
					TElement current = noAllocEnumerator.Current;
					list.Add(current);
				}
			}
			list.Sort((TElement a, TElement b) => b.dependencyOrder.CompareTo(a.dependencyOrder));
			foreach (TElement item in list)
			{
				Remove(item);
			}
			global::Unity.VisualScripting.ListPool<TElement>.Free(list);
		}

		protected override void SetItem(int index, TElement item)
		{
			throw new global::System.NotSupportedException();
		}

		public new global::Unity.VisualScripting.NoAllocEnumerator<TElement> GetEnumerator()
		{
			return new global::Unity.VisualScripting.NoAllocEnumerator<TElement>(this);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TElement>.Contains(global::System.Guid key)
		{
			return Contains(key);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, TElement>.Remove(global::System.Guid key)
		{
			return Remove(key);
		}
	}
}

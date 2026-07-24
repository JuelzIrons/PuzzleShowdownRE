namespace Unity.VisualScripting
{
	public class MergedCollection<T> : global::Unity.VisualScripting.IMergedCollection<T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.ICollection<T>> collections;

		public int Count
		{
			get
			{
				int num = 0;
				foreach (global::System.Collections.Generic.ICollection<T> value in collections.Values)
				{
					num += value.Count;
				}
				return num;
			}
		}

		public bool IsReadOnly => false;

		public MergedCollection()
		{
			collections = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.ICollection<T>>();
		}

		public void Include<TI>(global::System.Collections.Generic.ICollection<TI> collection) where TI : T
		{
			collections.Add(typeof(TI), new global::Unity.VisualScripting.VariantCollection<T, TI>(collection));
		}

		public bool Includes<TI>() where TI : T
		{
			return Includes(typeof(TI));
		}

		public bool Includes(global::System.Type implementationType)
		{
			return GetCollectionForType(implementationType, throwOnFail: false) != null;
		}

		public global::System.Collections.Generic.ICollection<TI> ForType<TI>() where TI : T
		{
			return ((global::Unity.VisualScripting.VariantCollection<T, TI>)GetCollectionForType(typeof(TI))).implementation;
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public global::System.Collections.Generic.IEnumerator<T> GetEnumerator()
		{
			foreach (global::System.Collections.Generic.ICollection<T> value in collections.Values)
			{
				foreach (T item in value)
				{
					yield return item;
				}
			}
		}

		private global::System.Collections.Generic.ICollection<T> GetCollectionForItem(T item)
		{
			global::Unity.VisualScripting.Ensure.That("item").IsNotNull(item);
			return GetCollectionForType(item.GetType());
		}

		private global::System.Collections.Generic.ICollection<T> GetCollectionForType(global::System.Type type, bool throwOnFail = true)
		{
			if (collections.ContainsKey(type))
			{
				return collections[type];
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Collections.Generic.ICollection<T>> collection in collections)
			{
				if (collection.Key.IsAssignableFrom(type))
				{
					return collection.Value;
				}
			}
			if (throwOnFail)
			{
				throw new global::System.InvalidOperationException($"No sub-collection available for type '{type}'.");
			}
			return null;
		}

		public bool Contains(T item)
		{
			return GetCollectionForItem(item).Contains(item);
		}

		public virtual void Add(T item)
		{
			GetCollectionForItem(item).Add(item);
		}

		public virtual void Clear()
		{
			foreach (global::System.Collections.Generic.ICollection<T> value in collections.Values)
			{
				value.Clear();
			}
		}

		public virtual bool Remove(T item)
		{
			return GetCollectionForItem(item).Remove(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < Count)
			{
				throw new global::System.ArgumentException();
			}
			int num = 0;
			foreach (global::System.Collections.Generic.ICollection<T> value in collections.Values)
			{
				value.CopyTo(array, arrayIndex + num);
				num += value.Count;
			}
		}
	}
}

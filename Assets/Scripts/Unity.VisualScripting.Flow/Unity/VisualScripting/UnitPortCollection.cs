namespace Unity.VisualScripting
{
	public sealed class UnitPortCollection<TPort> : global::System.Collections.ObjectModel.KeyedCollection<string, TPort>, global::Unity.VisualScripting.IUnitPortCollection<TPort>, global::Unity.VisualScripting.IKeyedCollection<string, TPort>, global::System.Collections.Generic.ICollection<TPort>, global::System.Collections.Generic.IEnumerable<TPort>, global::System.Collections.IEnumerable where TPort : global::Unity.VisualScripting.IUnitPort
	{
		public global::Unity.VisualScripting.IUnit unit { get; }

		TPort global::Unity.VisualScripting.IKeyedCollection<string, TPort>.this[string key] => base[key];

		public UnitPortCollection(global::Unity.VisualScripting.IUnit unit)
		{
			this.unit = unit;
		}

		private void BeforeAdd(TPort port)
		{
			if (port.unit != null)
			{
				if (port.unit == unit)
				{
					throw new global::System.InvalidOperationException("Node ports cannot be added multiple time to the same unit.");
				}
				throw new global::System.InvalidOperationException("Node ports cannot be shared across nodes.");
			}
			global::Unity.VisualScripting.IUnit obj = unit;
			port.unit = obj;
		}

		private void AfterAdd(TPort port)
		{
			unit.PortsChanged();
		}

		private void BeforeRemove(TPort port)
		{
		}

		private void AfterRemove(TPort port)
		{
			port.unit = null;
			unit.PortsChanged();
		}

		public TPort Single()
		{
			if (base.Count != 0)
			{
				throw new global::System.InvalidOperationException("Port collection does not have a single port.");
			}
			return base[0];
		}

		protected override string GetKeyForItem(TPort item)
		{
			return item.key;
		}

		public new bool TryGetValue(string key, out TPort value)
		{
			if (base.Dictionary == null)
			{
				value = default(TPort);
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		protected override void InsertItem(int index, TPort item)
		{
			BeforeAdd(item);
			base.InsertItem(index, item);
			AfterAdd(item);
		}

		protected override void RemoveItem(int index)
		{
			TPort port = base[index];
			BeforeRemove(port);
			base.RemoveItem(index);
			AfterRemove(port);
		}

		protected override void SetItem(int index, TPort item)
		{
			throw new global::System.NotSupportedException();
		}

		protected override void ClearItems()
		{
			while (base.Count > 0)
			{
				RemoveItem(0);
			}
		}

		bool global::Unity.VisualScripting.IKeyedCollection<string, TPort>.Contains(string key)
		{
			return Contains(key);
		}

		bool global::Unity.VisualScripting.IKeyedCollection<string, TPort>.Remove(string key)
		{
			return Remove(key);
		}
	}
}

namespace Unity.VisualScripting
{
	public sealed class Namespace
	{
		private class Collection : global::System.Collections.ObjectModel.KeyedCollection<string, global::Unity.VisualScripting.Namespace>, global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.Namespace>, global::System.Collections.Generic.ICollection<global::Unity.VisualScripting.Namespace>, global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.Namespace>, global::System.Collections.IEnumerable
		{
			global::Unity.VisualScripting.Namespace global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.Namespace>.this[string key] => base[key];

			protected override string GetKeyForItem(global::Unity.VisualScripting.Namespace item)
			{
				return item.FullName;
			}

			public new bool TryGetValue(string key, out global::Unity.VisualScripting.Namespace value)
			{
				if (base.Dictionary == null)
				{
					value = null;
					return false;
				}
				return base.Dictionary.TryGetValue(key, out value);
			}

			bool global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.Namespace>.Contains(string key)
			{
				return Contains(key);
			}

			bool global::Unity.VisualScripting.IKeyedCollection<string, global::Unity.VisualScripting.Namespace>.Remove(string key)
			{
				return Remove(key);
			}
		}

		private static readonly global::Unity.VisualScripting.Namespace.Collection collection;

		public global::Unity.VisualScripting.Namespace Root { get; }

		public global::Unity.VisualScripting.Namespace Parent { get; }

		public string FullName { get; }

		public string Name { get; }

		public bool IsRoot { get; }

		public bool IsGlobal { get; }

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.Namespace> Ancestors
		{
			get
			{
				global::Unity.VisualScripting.Namespace ancestor = Parent;
				while (ancestor != null)
				{
					yield return ancestor;
					ancestor = ancestor.Parent;
				}
			}
		}

		public static global::Unity.VisualScripting.Namespace Global { get; }

		private Namespace(string fullName)
		{
			FullName = fullName;
			if (fullName != null)
			{
				string[] array = fullName.Split('.');
				Name = array[^1];
				if (array.Length > 1)
				{
					Root = array[0];
					Parent = fullName.Substring(0, fullName.LastIndexOf('.'));
				}
				else
				{
					Root = this;
					IsRoot = true;
					Parent = Global;
				}
			}
			else
			{
				Root = this;
				IsRoot = true;
				IsGlobal = true;
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.Namespace> AndAncestors()
		{
			yield return this;
			foreach (global::Unity.VisualScripting.Namespace ancestor in Ancestors)
			{
				yield return ancestor;
			}
		}

		public override int GetHashCode()
		{
			if (FullName == null)
			{
				return 0;
			}
			return FullName.GetHashCode();
		}

		public override string ToString()
		{
			return FullName;
		}

		static Namespace()
		{
			Global = new global::Unity.VisualScripting.Namespace(null);
			collection = new global::Unity.VisualScripting.Namespace.Collection();
		}

		public static global::Unity.VisualScripting.Namespace FromFullName(string fullName)
		{
			if (fullName == null)
			{
				return Global;
			}
			if (!collection.TryGetValue(fullName, out var value))
			{
				value = new global::Unity.VisualScripting.Namespace(fullName);
				collection.Add(value);
			}
			return value;
		}

		public override bool Equals(object obj)
		{
			global::Unity.VisualScripting.Namespace obj2 = obj as global::Unity.VisualScripting.Namespace;
			if (obj2 == null)
			{
				return false;
			}
			return FullName == obj2.FullName;
		}

		public static implicit operator global::Unity.VisualScripting.Namespace(string fullName)
		{
			return FromFullName(fullName);
		}

		public static implicit operator string(global::Unity.VisualScripting.Namespace @namespace)
		{
			return @namespace.FullName;
		}

		public static bool operator ==(global::Unity.VisualScripting.Namespace a, global::Unity.VisualScripting.Namespace b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.Namespace a, global::Unity.VisualScripting.Namespace b)
		{
			return !(a == b);
		}
	}
}

namespace UnityEngine.InputSystem.Utilities
{
	internal struct TypeTable
	{
		public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> table;

		private global::UnityEngine.InputSystem.InputManager m_Manager;

		public global::System.Collections.Generic.IEnumerable<string> names => global::System.Linq.Enumerable.Select(table.Keys, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString());

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.InternedString> internedNames => table.Keys;

		public void Initialize(global::UnityEngine.InputSystem.InputManager manager)
		{
			table = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type>();
			m_Manager = manager;
		}

		public global::UnityEngine.InputSystem.Utilities.InternedString FindNameForType(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> item in table)
			{
				if (item.Value == type)
				{
					return item.Key;
				}
			}
			return default(global::UnityEngine.InputSystem.Utilities.InternedString);
		}

		public void AddTypeRegistration(string name, global::System.Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentException("Name cannot be null or empty", "name");
			}
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			global::UnityEngine.InputSystem.Utilities.InternedString key = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			table[key] = type;
		}

		public global::System.Type LookupTypeRegistration(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (table == null)
			{
				throw new global::System.InvalidOperationException("Input System not yet initialized");
			}
			return TryLookupTypeRegistration(new global::UnityEngine.InputSystem.Utilities.InternedString(name));
		}

		private global::System.Type TryLookupTypeRegistration(global::UnityEngine.InputSystem.Utilities.InternedString internedName)
		{
			if (!table.TryGetValue(internedName, out var value) && m_Manager != null && m_Manager.RegisterCustomTypes())
			{
				table.TryGetValue(internedName, out value);
			}
			return value;
		}
	}
}

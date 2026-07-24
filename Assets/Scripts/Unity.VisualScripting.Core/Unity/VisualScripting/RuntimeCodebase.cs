namespace Unity.VisualScripting
{
	public static class RuntimeCodebase
	{
		private static readonly object @lock;

		private static readonly global::System.Collections.Generic.List<global::System.Type> _types;

		private static readonly global::System.Collections.Generic.List<global::System.Reflection.Assembly> _assemblies;

		public static global::System.Collections.Generic.HashSet<string> disallowedAssemblies;

		private static readonly global::System.Collections.Generic.Dictionary<string, global::System.Type> typeSerializations;

		private static global::System.Collections.Generic.Dictionary<string, global::System.Type> _renamedTypes;

		private static global::System.Collections.Generic.Dictionary<string, string> _renamedNamespaces;

		private static global::System.Collections.Generic.Dictionary<string, string> _renamedAssemblies;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<string, string>> _renamedMembers;

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> types => _types;

		public static global::System.Collections.Generic.IEnumerable<global::System.Reflection.Assembly> assemblies => _assemblies;

		public static global::System.Collections.Generic.Dictionary<string, string> renamedNamespaces
		{
			get
			{
				if (_renamedNamespaces == null)
				{
					_renamedNamespaces = FetchRenamedNamespaces();
				}
				return _renamedNamespaces;
			}
		}

		public static global::System.Collections.Generic.Dictionary<string, string> renamedAssemblies
		{
			get
			{
				if (_renamedAssemblies == null)
				{
					_renamedAssemblies = FetchRenamedAssemblies();
				}
				return _renamedAssemblies;
			}
		}

		public static global::System.Collections.Generic.Dictionary<string, global::System.Type> renamedTypes
		{
			get
			{
				if (_renamedTypes == null)
				{
					_renamedTypes = FetchRenamedTypes();
				}
				return _renamedTypes;
			}
		}

		static RuntimeCodebase()
		{
			@lock = new object();
			_types = new global::System.Collections.Generic.List<global::System.Type>();
			_assemblies = new global::System.Collections.Generic.List<global::System.Reflection.Assembly>();
			disallowedAssemblies = new global::System.Collections.Generic.HashSet<string>();
			typeSerializations = new global::System.Collections.Generic.Dictionary<string, global::System.Type>();
			_renamedTypes = null;
			_renamedNamespaces = null;
			_renamedAssemblies = null;
			_renamedMembers = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.Dictionary<string, string>>();
			lock (@lock)
			{
				global::System.Reflection.Assembly[] array = global::System.AppDomain.CurrentDomain.GetAssemblies();
				foreach (global::System.Reflection.Assembly assembly in array)
				{
					_assemblies.Add(assembly);
					foreach (global::System.Type item in assembly.GetTypesSafely())
					{
						_types.Add(item);
					}
				}
			}
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAssemblyAttributes(global::System.Type attributeType)
		{
			return GetAssemblyAttributes(attributeType, assemblies);
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAssemblyAttributes(global::System.Type attributeType, global::System.Collections.Generic.IEnumerable<global::System.Reflection.Assembly> assemblies)
		{
			global::Unity.VisualScripting.Ensure.That("attributeType").IsNotNull(attributeType);
			global::Unity.VisualScripting.Ensure.That("assemblies").IsNotNull(assemblies);
			foreach (global::System.Reflection.Assembly assembly in assemblies)
			{
				foreach (global::System.Attribute customAttribute in global::System.Reflection.CustomAttributeExtensions.GetCustomAttributes(assembly, attributeType))
				{
					if (attributeType.IsInstanceOfType(customAttribute))
					{
						yield return customAttribute;
					}
				}
			}
		}

		public static global::System.Collections.Generic.IEnumerable<TAttribute> GetAssemblyAttributes<TAttribute>(global::System.Collections.Generic.IEnumerable<global::System.Reflection.Assembly> assemblies) where TAttribute : global::System.Attribute
		{
			return global::System.Linq.Enumerable.Cast<TAttribute>(GetAssemblyAttributes(typeof(TAttribute), assemblies));
		}

		public static global::System.Collections.Generic.IEnumerable<TAttribute> GetAssemblyAttributes<TAttribute>() where TAttribute : global::System.Attribute
		{
			return global::System.Linq.Enumerable.Cast<TAttribute>(GetAssemblyAttributes(typeof(TAttribute)));
		}

		public static void PrewarmTypeDeserialization(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			string key = SerializeType(type);
			if (!typeSerializations.ContainsKey(key))
			{
				typeSerializations.Add(key, type);
			}
		}

		public static string SerializeType(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			return type?.FullName;
		}

		public static bool TryDeserializeType(string typeName, out global::System.Type type)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				type = null;
				return false;
			}
			lock (@lock)
			{
				if (!TryCachedTypeLookup(typeName, out type))
				{
					if (!TrySystemTypeLookup(typeName, out type) && !TryRenamedTypeLookup(typeName, out type))
					{
						return false;
					}
					typeSerializations.Add(typeName, type);
				}
				return true;
			}
		}

		public static global::System.Type DeserializeType(string typeName)
		{
			if (!TryDeserializeType(typeName, out var type))
			{
				throw new global::System.Runtime.Serialization.SerializationException("Unable to find type: '" + (typeName ?? "(null)") + "'.");
			}
			return type;
		}

		public static void ClearCachedTypes()
		{
			typeSerializations.Clear();
		}

		private static bool TryCachedTypeLookup(string typeName, out global::System.Type type)
		{
			return typeSerializations.TryGetValue(typeName, out type);
		}

		private static bool TrySystemTypeLookup(string typeName, out global::System.Type type)
		{
			foreach (global::System.Reflection.Assembly assembly in _assemblies)
			{
				if (disallowedAssemblies.Contains(assembly.GetName().Name))
				{
					continue;
				}
				type = assembly.GetType(typeName);
				if (!(type != null))
				{
					continue;
				}
				foreach (string disallowedAssembly in disallowedAssemblies)
				{
					if (type.FullName.Contains(disallowedAssembly))
					{
						return false;
					}
				}
				return true;
			}
			type = null;
			return false;
		}

		private static bool TrySystemTypeLookup(global::Unity.VisualScripting.TypeName typeName, out global::System.Type type)
		{
			if (disallowedAssemblies.Contains(typeName.AssemblyName))
			{
				type = null;
				return false;
			}
			if (typeName.IsArray)
			{
				foreach (global::System.Reflection.Assembly item in global::System.Linq.Enumerable.Where(_assemblies, (global::System.Reflection.Assembly a) => typeName.AssemblyName == a.GetName().Name))
				{
					type = item.GetType(typeName.Name);
					if (type != null)
					{
						return true;
					}
				}
				type = null;
				return false;
			}
			return TrySystemTypeLookup(typeName.ToLooseString(), out type);
		}

		private static bool TryRenamedTypeLookup(string previousTypeName, out global::System.Type type)
		{
			if (renamedTypes.TryGetValue(previousTypeName, out var value))
			{
				type = value;
				return true;
			}
			global::Unity.VisualScripting.TypeName typeName = global::Unity.VisualScripting.TypeName.Parse(previousTypeName);
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Type> renamedType in renamedTypes)
			{
				typeName.ReplaceName(renamedType.Key, renamedType.Value);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> renamedNamespace in renamedNamespaces)
			{
				typeName.ReplaceNamespace(renamedNamespace.Key, renamedNamespace.Value);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> renamedAssembly in renamedAssemblies)
			{
				typeName.ReplaceAssembly(renamedAssembly.Key, renamedAssembly.Value);
			}
			if (TrySystemTypeLookup(typeName, out type))
			{
				return true;
			}
			type = null;
			return false;
		}

		public static global::System.Collections.Generic.Dictionary<string, string> RenamedMembers(global::System.Type type)
		{
			if (!_renamedMembers.TryGetValue(type, out var value))
			{
				value = FetchRenamedMembers(type);
				_renamedMembers.Add(type, value);
			}
			return value;
		}

		private static global::System.Collections.Generic.Dictionary<string, string> FetchRenamedMembers(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			global::System.Reflection.MemberInfo[] extendedMembers = type.GetExtendedMembers(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.FlattenHierarchy);
			foreach (global::System.Reflection.MemberInfo memberInfo in extendedMembers)
			{
				global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.RenamedFromAttribute> enumerable;
				try
				{
					enumerable = global::System.Linq.Enumerable.Cast<global::Unity.VisualScripting.RenamedFromAttribute>(global::System.Attribute.GetCustomAttributes(memberInfo, typeof(global::Unity.VisualScripting.RenamedFromAttribute), inherit: false));
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch RenamedFrom attributes for member '{memberInfo}':\n{arg}");
					continue;
				}
				string name = memberInfo.Name;
				foreach (global::Unity.VisualScripting.RenamedFromAttribute item in enumerable)
				{
					string previousName = item.previousName;
					if (dictionary.ContainsKey(previousName))
					{
						global::UnityEngine.Debug.LogWarning($"Multiple members on '{type}' indicate having been renamed from '{previousName}'.\nIgnoring renamed attributes for '{memberInfo}'.");
					}
					else
					{
						dictionary.Add(previousName, name);
					}
				}
			}
			return dictionary;
		}

		private static global::System.Collections.Generic.Dictionary<string, string> FetchRenamedNamespaces()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			foreach (global::Unity.VisualScripting.RenamedNamespaceAttribute assemblyAttribute in GetAssemblyAttributes<global::Unity.VisualScripting.RenamedNamespaceAttribute>())
			{
				string previousName = assemblyAttribute.previousName;
				string newName = assemblyAttribute.newName;
				if (dictionary.ContainsKey(previousName))
				{
					global::UnityEngine.Debug.LogWarning("Multiple new names have been provided for namespace '" + previousName + "'.\nIgnoring new name '" + newName + "'.");
				}
				else
				{
					dictionary.Add(previousName, newName);
				}
			}
			return dictionary;
		}

		private static global::System.Collections.Generic.Dictionary<string, string> FetchRenamedAssemblies()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			foreach (global::Unity.VisualScripting.RenamedAssemblyAttribute assemblyAttribute in GetAssemblyAttributes<global::Unity.VisualScripting.RenamedAssemblyAttribute>())
			{
				string previousName = assemblyAttribute.previousName;
				string newName = assemblyAttribute.newName;
				if (dictionary.ContainsKey(previousName))
				{
					global::UnityEngine.Debug.LogWarning("Multiple new names have been provided for assembly '" + previousName + "'.\nIgnoring new name '" + newName + "'.");
				}
				else
				{
					dictionary.Add(previousName, newName);
				}
			}
			return dictionary;
		}

		private static global::System.Collections.Generic.Dictionary<string, global::System.Type> FetchRenamedTypes()
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> dictionary = new global::System.Collections.Generic.Dictionary<string, global::System.Type>();
			foreach (global::System.Reflection.Assembly assembly in assemblies)
			{
				foreach (global::System.Type item in assembly.GetTypesSafely())
				{
					global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.RenamedFromAttribute> enumerable;
					try
					{
						enumerable = global::System.Linq.Enumerable.Cast<global::Unity.VisualScripting.RenamedFromAttribute>(global::System.Attribute.GetCustomAttributes(item, typeof(global::Unity.VisualScripting.RenamedFromAttribute), inherit: false));
					}
					catch (global::System.Exception arg)
					{
						global::UnityEngine.Debug.LogWarning($"Failed to fetch RenamedFrom attributes for type '{item}':\n{arg}");
						continue;
					}
					_ = item.FullName;
					foreach (global::Unity.VisualScripting.RenamedFromAttribute item2 in enumerable)
					{
						string previousName = item2.previousName;
						if (dictionary.ContainsKey(previousName))
						{
							global::UnityEngine.Debug.LogWarning($"Multiple types indicate having been renamed from '{previousName}'.\nIgnoring renamed attributes for '{item}'.");
						}
						else
						{
							dictionary.Add(previousName, item);
						}
					}
				}
			}
			return dictionary;
		}
	}
}

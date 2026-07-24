namespace Unity.VisualScripting.FullSerializer.Internal
{
	public static class fsVersionManager
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>> _cache = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>>();

		public static global::Unity.VisualScripting.FullSerializer.fsResult GetVersionImportPath(string currentVersion, global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType targetVersion, out global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> path)
		{
			path = new global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>();
			if (!GetVersionImportPathRecursive(path, currentVersion, targetVersion))
			{
				return global::Unity.VisualScripting.FullSerializer.fsResult.Fail("There is no migration path from \"" + currentVersion + "\" to \"" + targetVersion.VersionString + "\"");
			}
			path.Add(targetVersion);
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success;
		}

		private static bool GetVersionImportPathRecursive(global::System.Collections.Generic.List<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> path, string currentVersion, global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType current)
		{
			for (int i = 0; i < current.Ancestors.Length; i++)
			{
				global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType fsVersionedType2 = current.Ancestors[i];
				if (fsVersionedType2.VersionString == currentVersion || GetVersionImportPathRecursive(path, currentVersion, fsVersionedType2))
				{
					path.Add(fsVersionedType2);
					return true;
				}
			}
			return false;
		}

		public static global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> GetVersionedType(global::System.Type type)
		{
			if (!_cache.TryGetValue(type, out var value))
			{
				global::Unity.VisualScripting.FullSerializer.fsObjectAttribute attribute = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetAttribute<global::Unity.VisualScripting.FullSerializer.fsObjectAttribute>(type);
				if (attribute != null && (!string.IsNullOrEmpty(attribute.VersionString) || attribute.PreviousModels != null))
				{
					if (attribute.PreviousModels != null && string.IsNullOrEmpty(attribute.VersionString))
					{
						throw new global::System.Exception("fsObject attribute on " + type?.ToString() + " contains a PreviousModels specifier - it must also include a VersionString modifier");
					}
					global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[] array = new global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[(attribute.PreviousModels != null) ? attribute.PreviousModels.Length : 0];
					for (int i = 0; i < array.Length; i++)
					{
						global::Unity.VisualScripting.FullSerializer.Internal.fsOption<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> versionedType = GetVersionedType(attribute.PreviousModels[i]);
						if (versionedType.IsEmpty)
						{
							throw new global::System.Exception("Unable to create versioned type for ancestor " + versionedType.ToString() + "; please add an [fsObject(VersionString=\"...\")] attribute");
						}
						array[i] = versionedType.Value;
					}
					global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType obj = new global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType
					{
						Ancestors = array,
						VersionString = attribute.VersionString,
						ModelType = type
					};
					VerifyUniqueVersionStrings(obj);
					VerifyConstructors(obj);
					value = global::Unity.VisualScripting.FullSerializer.Internal.fsOption.Just(obj);
				}
				_cache[type] = value;
			}
			return value;
		}

		private static void VerifyConstructors(global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type)
		{
			global::System.Reflection.ConstructorInfo[] declaredConstructors = type.ModelType.GetDeclaredConstructors();
			for (int i = 0; i < type.Ancestors.Length; i++)
			{
				global::System.Type modelType = type.Ancestors[i].ModelType;
				bool flag = false;
				for (int j = 0; j < declaredConstructors.Length; j++)
				{
					global::System.Reflection.ParameterInfo[] parameters = declaredConstructors[j].GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == modelType)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new global::Unity.VisualScripting.FullSerializer.fsMissingVersionConstructorException(type.ModelType, modelType);
				}
			}
		}

		private static void VerifyUniqueVersionStrings(global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type)
		{
			global::System.Collections.Generic.Dictionary<string, global::System.Type> dictionary = new global::System.Collections.Generic.Dictionary<string, global::System.Type>();
			global::System.Collections.Generic.Queue<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> queue = new global::System.Collections.Generic.Queue<global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>();
			queue.Enqueue(type);
			while (queue.Count > 0)
			{
				global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType fsVersionedType2 = queue.Dequeue();
				if (dictionary.ContainsKey(fsVersionedType2.VersionString) && dictionary[fsVersionedType2.VersionString] != fsVersionedType2.ModelType)
				{
					throw new global::Unity.VisualScripting.FullSerializer.fsDuplicateVersionNameException(dictionary[fsVersionedType2.VersionString], fsVersionedType2.ModelType, fsVersionedType2.VersionString);
				}
				dictionary[fsVersionedType2.VersionString] = fsVersionedType2.ModelType;
				global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[] ancestors = fsVersionedType2.Ancestors;
				foreach (global::Unity.VisualScripting.FullSerializer.Internal.fsVersionedType item in ancestors)
				{
					queue.Enqueue(item);
				}
			}
		}
	}
}

namespace UnityEngine.InputSystem
{
	public class InputActionAsset : global::UnityEngine.ScriptableObject, global::UnityEngine.InputSystem.IInputActionCollection2, global::UnityEngine.InputSystem.IInputActionCollection, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputAction>, global::System.Collections.IEnumerable
	{
		private static class JsonVersion
		{
			public const int Version0 = 0;

			public const int Version1 = 1;

			public const int Current = 1;
		}

		[global::System.Serializable]
		internal struct WriteFileJson
		{
			public int version;

			public string name;

			public global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[] maps;

			public global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] controlSchemes;
		}

		[global::System.Serializable]
		internal struct WriteFileJsonNoName
		{
			public global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[] maps;

			public global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] controlSchemes;
		}

		[global::System.Serializable]
		internal struct ReadFileJson
		{
			public int version;

			public string name;

			public global::UnityEngine.InputSystem.InputActionMap.ReadMapJson[] maps;

			public global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] controlSchemes;

			public void ToAsset(global::UnityEngine.InputSystem.InputActionAsset asset)
			{
				asset.name = name;
				global::UnityEngine.InputSystem.InputActionMap.ReadFileJson readFileJson = new global::UnityEngine.InputSystem.InputActionMap.ReadFileJson
				{
					maps = maps
				};
				asset.m_ActionMaps = readFileJson.ToMaps();
				asset.m_ControlSchemes = global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.ToSchemes(controlSchemes);
				if (asset.m_ActionMaps != null)
				{
					global::UnityEngine.InputSystem.InputActionMap[] actionMaps = asset.m_ActionMaps;
					for (int i = 0; i < actionMaps.Length; i++)
					{
						actionMaps[i].m_Asset = asset;
					}
				}
			}
		}

		public const string Extension = "inputactions";

		internal const string kDefaultAssetLayoutJson = "{}";

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionMap[] m_ActionMaps;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputControlScheme[] m_ControlSchemes;

		[global::UnityEngine.SerializeField]
		internal bool m_IsProjectWide;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionState m_SharedStateForAllMaps;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputBinding? m_BindingMask;

		[global::System.NonSerialized]
		internal int m_ParameterOverridesCount;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[] m_ParameterOverrides;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionMap.DeviceArray m_Devices;

		public bool enabled
		{
			get
			{
				foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in actionMaps)
				{
					if (actionMap.enabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputActionMap> actionMaps => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputActionMap>(m_ActionMaps);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme> controlSchemes => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme>(m_ControlSchemes);

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> bindings
		{
			get
			{
				int numActionMaps = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps);
				if (numActionMaps == 0)
				{
					yield break;
				}
				int i = 0;
				while (i < numActionMaps)
				{
					global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionMaps[i];
					global::UnityEngine.InputSystem.InputBinding[] bindings = inputActionMap.m_Bindings;
					int numBindings = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(bindings);
					int num;
					for (int n = 0; n < numBindings; n = num)
					{
						yield return bindings[n];
						num = n + 1;
					}
					num = i + 1;
					i = num;
				}
			}
		}

		public global::UnityEngine.InputSystem.InputBinding? bindingMask
		{
			get
			{
				return m_BindingMask;
			}
			set
			{
				if (!(m_BindingMask == value))
				{
					m_BindingMask = value;
					ReResolveIfNecessary(fullResolve: true);
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices
		{
			get
			{
				return m_Devices.Get();
			}
			set
			{
				if (m_Devices.Set(value))
				{
					ReResolveIfNecessary(fullResolve: false);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputAction this[string actionNameOrId] => FindAction(actionNameOrId) ?? throw new global::System.Collections.Generic.KeyNotFoundException($"Cannot find action '{actionNameOrId}' in '{this}'");

		public string ToJson()
		{
			bool flag = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps) > 0 || global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ControlSchemes) > 0;
			return global::UnityEngine.JsonUtility.ToJson(new global::UnityEngine.InputSystem.InputActionAsset.WriteFileJson
			{
				version = (flag ? 1 : 0),
				name = base.name,
				maps = global::UnityEngine.InputSystem.InputActionMap.WriteFileJson.FromMaps(m_ActionMaps).maps,
				controlSchemes = global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.ToJson(m_ControlSchemes)
			}, prettyPrint: true);
		}

		public void LoadFromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new global::System.ArgumentNullException("json");
			}
			global::UnityEngine.InputSystem.InputActionAsset.ReadFileJson parsedJson = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.InputActionAsset.ReadFileJson>(json);
			MigrateJson(ref parsedJson);
			parsedJson.ToAsset(this);
		}

		public static global::UnityEngine.InputSystem.InputActionAsset FromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new global::System.ArgumentNullException("json");
			}
			global::UnityEngine.InputSystem.InputActionAsset inputActionAsset = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.InputSystem.InputActionAsset>();
			inputActionAsset.LoadFromJson(json);
			return inputActionAsset;
		}

		public global::UnityEngine.InputSystem.InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			if (actionNameOrId == null)
			{
				throw new global::System.ArgumentNullException("actionNameOrId");
			}
			if (m_ActionMaps != null)
			{
				int num = actionNameOrId.IndexOf('/');
				if (num >= 0)
				{
					global::UnityEngine.InputSystem.Utilities.Substring right = new global::UnityEngine.InputSystem.Utilities.Substring(actionNameOrId, 0, num);
					global::UnityEngine.InputSystem.Utilities.Substring right2 = new global::UnityEngine.InputSystem.Utilities.Substring(actionNameOrId, num + 1);
					if (right.isEmpty || right2.isEmpty)
					{
						throw new global::System.ArgumentException("Malformed action path: " + actionNameOrId, "actionNameOrId");
					}
					for (int i = 0; i < m_ActionMaps.Length; i++)
					{
						global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionMaps[i];
						if (global::UnityEngine.InputSystem.Utilities.Substring.Compare(inputActionMap.name, right, global::System.StringComparison.InvariantCultureIgnoreCase) != 0)
						{
							continue;
						}
						global::UnityEngine.InputSystem.InputAction[] actions = inputActionMap.m_Actions;
						if (actions == null)
						{
							break;
						}
						foreach (global::UnityEngine.InputSystem.InputAction inputAction in actions)
						{
							if (global::UnityEngine.InputSystem.Utilities.Substring.Compare(inputAction.name, right2, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
							{
								return inputAction;
							}
						}
						break;
					}
				}
				global::UnityEngine.InputSystem.InputAction inputAction2 = null;
				for (int k = 0; k < m_ActionMaps.Length; k++)
				{
					global::UnityEngine.InputSystem.InputAction inputAction3 = m_ActionMaps[k].FindAction(actionNameOrId);
					if (inputAction3 != null)
					{
						if (inputAction3.enabled || inputAction3.m_Id == actionNameOrId)
						{
							return inputAction3;
						}
						if (inputAction2 == null)
						{
							inputAction2 = inputAction3;
						}
					}
				}
				if (inputAction2 != null)
				{
					return inputAction2;
				}
			}
			if (throwIfNotFound)
			{
				throw new global::System.ArgumentException($"No action '{actionNameOrId}' in '{this}'");
			}
			return null;
		}

		public int FindBinding(global::UnityEngine.InputSystem.InputBinding mask, out global::UnityEngine.InputSystem.InputAction action)
		{
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps);
			for (int i = 0; i < num; i++)
			{
				int num2 = m_ActionMaps[i].FindBinding(mask, out action);
				if (num2 >= 0)
				{
					return num2;
				}
			}
			action = null;
			return -1;
		}

		public global::UnityEngine.InputSystem.InputActionMap FindActionMap(string nameOrId, bool throwIfNotFound = false)
		{
			if (nameOrId == null)
			{
				throw new global::System.ArgumentNullException("nameOrId");
			}
			if (m_ActionMaps == null)
			{
				return null;
			}
			if (nameOrId.Contains('-') && global::System.Guid.TryParse(nameOrId, out var result))
			{
				for (int i = 0; i < m_ActionMaps.Length; i++)
				{
					global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionMaps[i];
					if (inputActionMap.idDontGenerate == result)
					{
						return inputActionMap;
					}
				}
			}
			for (int j = 0; j < m_ActionMaps.Length; j++)
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap2 = m_ActionMaps[j];
				if (string.Compare(nameOrId, inputActionMap2.name, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return inputActionMap2;
				}
			}
			if (throwIfNotFound)
			{
				throw new global::System.ArgumentException($"Cannot find action map '{nameOrId}' in '{this}'");
			}
			return null;
		}

		public global::UnityEngine.InputSystem.InputActionMap FindActionMap(global::System.Guid id)
		{
			if (m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < m_ActionMaps.Length; i++)
			{
				global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionMaps[i];
				if (inputActionMap.idDontGenerate == id)
				{
					return inputActionMap;
				}
			}
			return null;
		}

		public global::UnityEngine.InputSystem.InputAction FindAction(global::System.Guid guid)
		{
			if (m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < m_ActionMaps.Length; i++)
			{
				global::UnityEngine.InputSystem.InputAction inputAction = m_ActionMaps[i].FindAction(guid);
				if (inputAction != null)
				{
					return inputAction;
				}
			}
			return null;
		}

		public int FindControlSchemeIndex(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (m_ControlSchemes == null)
			{
				return -1;
			}
			for (int i = 0; i < m_ControlSchemes.Length; i++)
			{
				if (string.Compare(name, m_ControlSchemes[i].name, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		public global::UnityEngine.InputSystem.InputControlScheme? FindControlScheme(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			int num = FindControlSchemeIndex(name);
			if (num == -1)
			{
				return null;
			}
			return m_ControlSchemes[num];
		}

		public bool IsUsableWithDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ControlSchemes);
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					if (m_ControlSchemes[i].SupportsDevice(device))
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps);
				for (int j = 0; j < num2; j++)
				{
					if (m_ActionMaps[j].IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void Enable()
		{
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in actionMaps)
			{
				actionMap.Enable();
			}
		}

		public void Disable()
		{
			foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in actionMaps)
			{
				actionMap.Disable();
			}
		}

		public bool Contains(global::UnityEngine.InputSystem.InputAction action)
		{
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = action?.actionMap;
			if (inputActionMap == null)
			{
				return false;
			}
			return inputActionMap.asset == this;
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputAction> GetEnumerator()
		{
			if (m_ActionMaps == null)
			{
				yield break;
			}
			int i = 0;
			while (i < m_ActionMaps.Length)
			{
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputAction> actions = m_ActionMaps[i].actions;
				int actionCount = actions.Count;
				int num;
				for (int n = 0; n < actionCount; n = num)
				{
					yield return actions[n];
					num = n + 1;
				}
				num = i + 1;
				i = num;
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void MarkAsDirty()
		{
		}

		internal bool IsEmpty()
		{
			if (actionMaps.Count == 0)
			{
				return controlSchemes.Count == 0;
			}
			return false;
		}

		internal void OnWantToChangeSetup()
		{
			if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps) > 0)
			{
				m_ActionMaps[0].OnWantToChangeSetup();
			}
		}

		internal void OnSetupChanged()
		{
			MarkAsDirty();
			if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps) > 0)
			{
				m_ActionMaps[0].OnSetupChanged();
			}
			else
			{
				m_SharedStateForAllMaps = null;
			}
		}

		private void ReResolveIfNecessary(bool fullResolve)
		{
			if (m_SharedStateForAllMaps != null)
			{
				m_ActionMaps[0].LazyResolveBindings(fullResolve);
			}
		}

		internal void ResolveBindingsIfNecessary()
		{
			if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMaps) > 0)
			{
				global::UnityEngine.InputSystem.InputActionMap[] array = m_ActionMaps;
				for (int i = 0; i < array.Length && !array[i].ResolveBindingsIfNecessary(); i++)
				{
				}
			}
		}

		private void OnDestroy()
		{
			Disable();
			if (m_SharedStateForAllMaps != null)
			{
				m_SharedStateForAllMaps.Dispose();
				m_SharedStateForAllMaps = null;
			}
		}

		internal void MigrateJson(ref global::UnityEngine.InputSystem.InputActionAsset.ReadFileJson parsedJson)
		{
			if (parsedJson.version >= 1)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputActionMap.ReadMapJson[] maps = parsedJson.maps;
			if (((maps != null && maps.Length != 0) ? 1 : 0) > (false ? 1 : 0) && parsedJson.version < 1)
			{
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters> list = null;
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters> list2 = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters>(8);
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NamedValue> list3 = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NamedValue>(4);
				global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Array> dictionary = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Array>(8);
				for (int i = 0; i < parsedJson.maps.Length; i++)
				{
					global::UnityEngine.InputSystem.InputActionMap.ReadMapJson readMapJson = parsedJson.maps[i];
					for (int j = 0; j < readMapJson.actions.Length; j++)
					{
						global::UnityEngine.InputSystem.InputActionMap.ReadActionJson readActionJson = readMapJson.actions[j];
						string processors = readActionJson.processors;
						if (string.IsNullOrEmpty(processors) || !global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(processors, ref list))
						{
							continue;
						}
						list2.Clear();
						for (int k = 0; k < list.Count; k++)
						{
							global::UnityEngine.InputSystem.Utilities.NameAndParameters item = list[k];
							global::System.Type type = global::UnityEngine.InputSystem.InputSystem.TryGetProcessor(item.name);
							if (item.parameters.Count == 0 || type == null)
							{
								list2.Add(item);
								continue;
							}
							list3.Clear();
							for (int l = 0; l < item.parameters.Count; l++)
							{
								global::UnityEngine.InputSystem.Utilities.NamedValue namedValue = item.parameters[l];
								global::UnityEngine.InputSystem.Utilities.NamedValue item2 = namedValue;
								global::System.Reflection.FieldInfo field = type.GetField(namedValue.name, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
								if (field != null && field.FieldType.IsEnum)
								{
									int num = namedValue.value.ToInt32();
									if (num >= 0)
									{
										if (!dictionary.TryGetValue(field.FieldType, out var value))
										{
											value = global::System.Enum.GetValues(field.FieldType);
											dictionary[field.FieldType] = value;
										}
										if (num < value.Length)
										{
											int value2 = global::System.Convert.ToInt32(value.GetValue(num));
											item2 = global::UnityEngine.InputSystem.Utilities.NamedValue.From(namedValue.name, value2);
										}
									}
								}
								list3.Add(item2);
							}
							list2.Add(global::UnityEngine.InputSystem.Utilities.NameAndParameters.Create(item.name, list3));
						}
						readActionJson.processors = global::UnityEngine.InputSystem.Utilities.NameAndParameters.ToSerializableString(list2);
						readMapJson.actions[j] = readActionJson;
					}
					parsedJson.maps[i] = readMapJson;
				}
			}
			parsedJson.version = 1;
		}
	}
}

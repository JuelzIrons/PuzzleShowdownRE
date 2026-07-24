namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	public sealed class InputActionMap : global::System.ICloneable, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.InputSystem.IInputActionCollection2, global::UnityEngine.InputSystem.IInputActionCollection, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputAction>, global::System.Collections.IEnumerable, global::System.IDisposable
	{
		[global::System.Flags]
		private enum Flags
		{
			NeedToResolveBindings = 1,
			BindingResolutionNeedsFullReResolve = 2,
			ControlsForEachActionInitialized = 4,
			BindingsForEachActionInitialized = 8
		}

		internal struct DeviceArray
		{
			private bool m_HaveValue;

			private int m_DeviceCount;

			private global::UnityEngine.InputSystem.InputDevice[] m_DeviceArray;

			public int IndexOf(global::UnityEngine.InputSystem.InputDevice device)
			{
				return global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(m_DeviceArray, device, m_DeviceCount);
			}

			public bool Remove(global::UnityEngine.InputSystem.InputDevice device)
			{
				int num = IndexOf(device);
				if (num < 0)
				{
					return false;
				}
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_DeviceArray, ref m_DeviceCount, num);
				return true;
			}

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? Get()
			{
				if (!m_HaveValue)
				{
					return null;
				}
				return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>(m_DeviceArray, 0, m_DeviceCount);
			}

			public bool Set(global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices)
			{
				if (!devices.HasValue)
				{
					if (!m_HaveValue)
					{
						return false;
					}
					if (m_DeviceCount > 0)
					{
						global::System.Array.Clear(m_DeviceArray, 0, m_DeviceCount);
					}
					m_DeviceCount = 0;
					m_HaveValue = false;
				}
				else
				{
					global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> value = devices.Value;
					if (m_HaveValue && value.Count == m_DeviceCount && global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.HaveEqualReferences(value, m_DeviceArray, m_DeviceCount))
					{
						return false;
					}
					if (m_DeviceCount > 0)
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Clear(m_DeviceArray, ref m_DeviceCount);
					}
					m_HaveValue = true;
					m_DeviceCount = 0;
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendListWithCapacity(ref m_DeviceArray, ref m_DeviceCount, value);
				}
				return true;
			}
		}

		[global::System.Serializable]
		internal struct BindingOverrideListJson
		{
			public global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson> bindings;
		}

		[global::System.Serializable]
		internal struct BindingOverrideJson
		{
			public string action;

			public string id;

			public string path;

			public string interactions;

			public string processors;

			public static global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson FromBinding(global::UnityEngine.InputSystem.InputBinding binding, string actionName)
			{
				return new global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson
				{
					action = actionName,
					id = binding.id.ToString(),
					path = (binding.overridePath ?? "null"),
					interactions = (binding.overrideInteractions ?? "null"),
					processors = (binding.overrideProcessors ?? "null")
				};
			}

			public static global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson FromBinding(global::UnityEngine.InputSystem.InputBinding binding)
			{
				return FromBinding(binding, binding.action);
			}

			public static global::UnityEngine.InputSystem.InputBinding ToBinding(global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson bindingOverride)
			{
				return new global::UnityEngine.InputSystem.InputBinding
				{
					overridePath = ((bindingOverride.path != "null") ? bindingOverride.path : null),
					overrideInteractions = ((bindingOverride.interactions != "null") ? bindingOverride.interactions : null),
					overrideProcessors = ((bindingOverride.processors != "null") ? bindingOverride.processors : null)
				};
			}
		}

		[global::System.Serializable]
		internal struct BindingJson
		{
			public string name;

			public string id;

			public string path;

			public string interactions;

			public string processors;

			public string groups;

			public string action;

			public bool isComposite;

			public bool isPartOfComposite;

			public global::UnityEngine.InputSystem.InputBinding ToBinding()
			{
				return new global::UnityEngine.InputSystem.InputBinding
				{
					name = (string.IsNullOrEmpty(name) ? null : name),
					m_Id = (string.IsNullOrEmpty(id) ? null : id),
					path = path,
					action = (string.IsNullOrEmpty(action) ? null : action),
					interactions = (string.IsNullOrEmpty(interactions) ? null : interactions),
					processors = (string.IsNullOrEmpty(processors) ? null : processors),
					groups = (string.IsNullOrEmpty(groups) ? null : groups),
					isComposite = isComposite,
					isPartOfComposite = isPartOfComposite
				};
			}

			public static global::UnityEngine.InputSystem.InputActionMap.BindingJson FromBinding(ref global::UnityEngine.InputSystem.InputBinding binding)
			{
				return new global::UnityEngine.InputSystem.InputActionMap.BindingJson
				{
					name = binding.name,
					id = binding.m_Id,
					path = binding.path,
					action = binding.action,
					interactions = binding.interactions,
					processors = binding.processors,
					groups = binding.groups,
					isComposite = binding.isComposite,
					isPartOfComposite = binding.isPartOfComposite
				};
			}
		}

		[global::System.Serializable]
		internal struct ReadActionJson
		{
			public string name;

			public string type;

			public string id;

			public string expectedControlType;

			public string expectedControlLayout;

			public string processors;

			public string interactions;

			public bool passThrough;

			public bool initialStateCheck;

			public global::UnityEngine.InputSystem.InputActionMap.BindingJson[] bindings;

			public global::UnityEngine.InputSystem.InputAction ToAction(string actionName = null)
			{
				if (!string.IsNullOrEmpty(expectedControlLayout))
				{
					expectedControlType = expectedControlLayout;
				}
				global::UnityEngine.InputSystem.InputActionType inputActionType = global::UnityEngine.InputSystem.InputActionType.Value;
				if (!string.IsNullOrEmpty(type))
				{
					inputActionType = (global::UnityEngine.InputSystem.InputActionType)global::System.Enum.Parse(typeof(global::UnityEngine.InputSystem.InputActionType), type, ignoreCase: true);
				}
				else if (passThrough)
				{
					inputActionType = global::UnityEngine.InputSystem.InputActionType.PassThrough;
				}
				else if (initialStateCheck)
				{
					inputActionType = global::UnityEngine.InputSystem.InputActionType.Value;
				}
				else if (!string.IsNullOrEmpty(expectedControlType) && (expectedControlType == "Button" || expectedControlType == "Key"))
				{
					inputActionType = global::UnityEngine.InputSystem.InputActionType.Button;
				}
				return new global::UnityEngine.InputSystem.InputAction(actionName ?? name, inputActionType)
				{
					m_Id = (string.IsNullOrEmpty(id) ? null : id),
					m_ExpectedControlType = ((!string.IsNullOrEmpty(expectedControlType)) ? expectedControlType : null),
					m_Processors = processors,
					m_Interactions = interactions,
					wantsInitialStateCheck = initialStateCheck
				};
			}
		}

		[global::System.Serializable]
		internal struct WriteActionJson
		{
			public string name;

			public string type;

			public string id;

			public string expectedControlType;

			public string processors;

			public string interactions;

			public bool initialStateCheck;

			public static global::UnityEngine.InputSystem.InputActionMap.WriteActionJson FromAction(global::UnityEngine.InputSystem.InputAction action)
			{
				return new global::UnityEngine.InputSystem.InputActionMap.WriteActionJson
				{
					name = action.m_Name,
					type = action.m_Type.ToString(),
					id = action.m_Id,
					expectedControlType = action.m_ExpectedControlType,
					processors = action.processors,
					interactions = action.interactions,
					initialStateCheck = action.wantsInitialStateCheck
				};
			}
		}

		[global::System.Serializable]
		internal struct ReadMapJson
		{
			public string name;

			public string id;

			public global::UnityEngine.InputSystem.InputActionMap.ReadActionJson[] actions;

			public global::UnityEngine.InputSystem.InputActionMap.BindingJson[] bindings;
		}

		[global::System.Serializable]
		internal struct WriteMapJson
		{
			public string name;

			public string id;

			public global::UnityEngine.InputSystem.InputActionMap.WriteActionJson[] actions;

			public global::UnityEngine.InputSystem.InputActionMap.BindingJson[] bindings;

			public static global::UnityEngine.InputSystem.InputActionMap.WriteMapJson FromMap(global::UnityEngine.InputSystem.InputActionMap map)
			{
				global::UnityEngine.InputSystem.InputActionMap.WriteActionJson[] array = null;
				global::UnityEngine.InputSystem.InputActionMap.BindingJson[] array2 = null;
				global::UnityEngine.InputSystem.InputAction[] array3 = map.m_Actions;
				if (array3 != null)
				{
					int num = array3.Length;
					array = new global::UnityEngine.InputSystem.InputActionMap.WriteActionJson[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = global::UnityEngine.InputSystem.InputActionMap.WriteActionJson.FromAction(array3[i]);
					}
				}
				global::UnityEngine.InputSystem.InputBinding[] array4 = map.m_Bindings;
				if (array4 != null)
				{
					int num2 = array4.Length;
					array2 = new global::UnityEngine.InputSystem.InputActionMap.BindingJson[num2];
					for (int j = 0; j < num2; j++)
					{
						array2[j] = global::UnityEngine.InputSystem.InputActionMap.BindingJson.FromBinding(ref array4[j]);
					}
				}
				return new global::UnityEngine.InputSystem.InputActionMap.WriteMapJson
				{
					name = map.name,
					id = map.id.ToString(),
					actions = array,
					bindings = array2
				};
			}
		}

		[global::System.Serializable]
		internal struct WriteFileJson
		{
			public global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[] maps;

			public static global::UnityEngine.InputSystem.InputActionMap.WriteFileJson FromMap(global::UnityEngine.InputSystem.InputActionMap map)
			{
				return new global::UnityEngine.InputSystem.InputActionMap.WriteFileJson
				{
					maps = new global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[1] { global::UnityEngine.InputSystem.InputActionMap.WriteMapJson.FromMap(map) }
				};
			}

			public static global::UnityEngine.InputSystem.InputActionMap.WriteFileJson FromMaps(global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputActionMap> maps)
			{
				int num = global::System.Linq.Enumerable.Count(maps);
				if (num == 0)
				{
					return default(global::UnityEngine.InputSystem.InputActionMap.WriteFileJson);
				}
				global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[] array = new global::UnityEngine.InputSystem.InputActionMap.WriteMapJson[num];
				int num2 = 0;
				foreach (global::UnityEngine.InputSystem.InputActionMap map in maps)
				{
					array[num2++] = global::UnityEngine.InputSystem.InputActionMap.WriteMapJson.FromMap(map);
				}
				return new global::UnityEngine.InputSystem.InputActionMap.WriteFileJson
				{
					maps = array
				};
			}
		}

		[global::System.Serializable]
		internal struct ReadFileJson
		{
			public global::UnityEngine.InputSystem.InputActionMap.ReadActionJson[] actions;

			public global::UnityEngine.InputSystem.InputActionMap.ReadMapJson[] maps;

			public global::UnityEngine.InputSystem.InputActionMap[] ToMaps()
			{
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap>();
				global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction>> list2 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction>>();
				global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding>> list3 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding>>();
				global::UnityEngine.InputSystem.InputActionMap.ReadActionJson[] array = actions;
				int num = ((array != null) ? array.Length : 0);
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.InputActionMap.ReadActionJson readActionJson = actions[i];
					if (string.IsNullOrEmpty(readActionJson.name))
					{
						throw new global::System.InvalidOperationException($"Action number {i + 1} has no name");
					}
					string text = null;
					string text2 = readActionJson.name;
					int num2 = text2.IndexOf('/');
					if (num2 != -1)
					{
						text = text2.Substring(0, num2);
						text2 = text2.Substring(num2 + 1);
						if (string.IsNullOrEmpty(text2))
						{
							throw new global::System.InvalidOperationException("Invalid action name '" + readActionJson.name + "' (missing action name after '/')");
						}
					}
					global::UnityEngine.InputSystem.InputActionMap inputActionMap = null;
					int j;
					for (j = 0; j < list.Count; j++)
					{
						if (string.Compare(list[j].name, text, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							inputActionMap = list[j];
							break;
						}
					}
					if (inputActionMap == null)
					{
						inputActionMap = new global::UnityEngine.InputSystem.InputActionMap(text);
						j = list.Count;
						list.Add(inputActionMap);
						list2.Add(new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction>());
						list3.Add(new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding>());
					}
					global::UnityEngine.InputSystem.InputAction inputAction = readActionJson.ToAction(text2);
					list2[j].Add(inputAction);
					if (readActionJson.bindings != null)
					{
						global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding> list4 = list3[j];
						for (int k = 0; k < readActionJson.bindings.Length; k++)
						{
							global::UnityEngine.InputSystem.InputActionMap.BindingJson bindingJson = readActionJson.bindings[k];
							global::UnityEngine.InputSystem.InputBinding item = bindingJson.ToBinding();
							item.action = inputAction.m_Name;
							list4.Add(item);
						}
					}
				}
				global::UnityEngine.InputSystem.InputActionMap.ReadMapJson[] array2 = maps;
				int num3 = ((array2 != null) ? array2.Length : 0);
				for (int l = 0; l < num3; l++)
				{
					global::UnityEngine.InputSystem.InputActionMap.ReadMapJson readMapJson = maps[l];
					string name = readMapJson.name;
					if (string.IsNullOrEmpty(name))
					{
						throw new global::System.InvalidOperationException($"Map number {l + 1} has no name");
					}
					global::UnityEngine.InputSystem.InputActionMap inputActionMap2 = null;
					int m;
					for (m = 0; m < list.Count; m++)
					{
						if (string.Compare(list[m].name, name, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							inputActionMap2 = list[m];
							break;
						}
					}
					if (inputActionMap2 == null)
					{
						inputActionMap2 = new global::UnityEngine.InputSystem.InputActionMap(name)
						{
							m_Id = (string.IsNullOrEmpty(readMapJson.id) ? null : readMapJson.id)
						};
						m = list.Count;
						list.Add(inputActionMap2);
						list2.Add(new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputAction>());
						list3.Add(new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding>());
					}
					global::UnityEngine.InputSystem.InputActionMap.ReadActionJson[] array3 = readMapJson.actions;
					int num4 = ((array3 != null) ? array3.Length : 0);
					for (int n = 0; n < num4; n++)
					{
						global::UnityEngine.InputSystem.InputActionMap.ReadActionJson readActionJson2 = readMapJson.actions[n];
						if (string.IsNullOrEmpty(readActionJson2.name))
						{
							throw new global::System.InvalidOperationException($"Action number {l + 1} in map '{name}' has no name");
						}
						global::UnityEngine.InputSystem.InputAction inputAction2 = readActionJson2.ToAction();
						list2[m].Add(inputAction2);
						if (readActionJson2.bindings != null)
						{
							global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding> list5 = list3[m];
							for (int num5 = 0; num5 < readActionJson2.bindings.Length; num5++)
							{
								global::UnityEngine.InputSystem.InputActionMap.BindingJson bindingJson2 = readActionJson2.bindings[num5];
								global::UnityEngine.InputSystem.InputBinding item2 = bindingJson2.ToBinding();
								item2.action = inputAction2.m_Name;
								list5.Add(item2);
							}
						}
					}
					global::UnityEngine.InputSystem.InputActionMap.BindingJson[] bindings = readMapJson.bindings;
					int num6 = ((bindings != null) ? bindings.Length : 0);
					global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputBinding> list6 = list3[m];
					for (int num7 = 0; num7 < num6; num7++)
					{
						global::UnityEngine.InputSystem.InputActionMap.BindingJson bindingJson3 = readMapJson.bindings[num7];
						global::UnityEngine.InputSystem.InputBinding item3 = bindingJson3.ToBinding();
						list6.Add(item3);
					}
				}
				for (int num8 = 0; num8 < list.Count; num8++)
				{
					global::UnityEngine.InputSystem.InputActionMap inputActionMap3 = list[num8];
					global::UnityEngine.InputSystem.InputAction[] array4 = list2[num8].ToArray();
					global::UnityEngine.InputSystem.InputBinding[] bindings2 = list3[num8].ToArray();
					inputActionMap3.m_Actions = array4;
					inputActionMap3.m_Bindings = bindings2;
					for (int num9 = 0; num9 < array4.Length; num9++)
					{
						array4[num9].m_ActionMap = inputActionMap3;
					}
				}
				return list.ToArray();
			}
		}

		private static readonly global::Unity.Profiling.ProfilerMarker k_ResolveBindingsProfilerMarker = new global::Unity.Profiling.ProfilerMarker("InputActionMap.ResolveBindings");

		[global::UnityEngine.SerializeField]
		internal string m_Name;

		[global::UnityEngine.SerializeField]
		internal string m_Id;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionAsset m_Asset;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputAction[] m_Actions;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputBinding[] m_Bindings;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.InputBinding[] m_BindingsForEachAction;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.InputControl[] m_ControlsForEachAction;

		[global::System.NonSerialized]
		internal int m_EnabledActionsCount;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputAction m_SingletonAction;

		[global::System.NonSerialized]
		internal int m_MapIndexInState = -1;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionState m_State;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputBinding? m_BindingMask;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.InputActionMap.Flags m_Flags;

		[global::System.NonSerialized]
		internal int m_ParameterOverridesCount;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[] m_ParameterOverrides;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionMap.DeviceArray m_Devices;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> m_ActionCallbacks;

		[global::System.NonSerialized]
		internal global::System.Collections.Generic.Dictionary<string, int> m_ActionIndexByNameOrId;

		internal static int s_DeferBindingResolution;

		internal static bool s_NeedToResolveBindings;

		public string name => m_Name;

		public global::UnityEngine.InputSystem.InputActionAsset asset => m_Asset;

		public global::System.Guid id
		{
			get
			{
				if (string.IsNullOrEmpty(m_Id))
				{
					GenerateId();
				}
				return new global::System.Guid(m_Id);
			}
		}

		internal global::System.Guid idDontGenerate
		{
			get
			{
				if (string.IsNullOrEmpty(m_Id))
				{
					return default(global::System.Guid);
				}
				return new global::System.Guid(m_Id);
			}
		}

		public bool enabled => m_EnabledActionsCount > 0;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputAction> actions => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputAction>(m_Actions);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding>(m_Bindings);

		global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> global::UnityEngine.InputSystem.IInputActionCollection2.bindings => bindings;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme> controlSchemes
		{
			get
			{
				if (m_Asset == null)
				{
					return default(global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme>);
				}
				return m_Asset.controlSchemes;
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
					LazyResolveBindings(fullResolve: true);
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices
		{
			get
			{
				return m_Devices.Get() ?? m_Asset?.devices;
			}
			set
			{
				if (m_Devices.Set(value))
				{
					LazyResolveBindings(fullResolve: false);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputAction this[string actionNameOrId]
		{
			get
			{
				if (actionNameOrId == null)
				{
					throw new global::System.ArgumentNullException("actionNameOrId");
				}
				return FindAction(actionNameOrId) ?? throw new global::System.Collections.Generic.KeyNotFoundException("Cannot find action '" + actionNameOrId + "'");
			}
		}

		private bool needToResolveBindings
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputActionMap.Flags.NeedToResolveBindings) != 0;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputActionMap.Flags.NeedToResolveBindings;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionMap.Flags.NeedToResolveBindings;
				}
			}
		}

		private bool bindingResolutionNeedsFullReResolve
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputActionMap.Flags.BindingResolutionNeedsFullReResolve) != 0;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputActionMap.Flags.BindingResolutionNeedsFullReResolve;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionMap.Flags.BindingResolutionNeedsFullReResolve;
				}
			}
		}

		private bool controlsForEachActionInitialized
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputActionMap.Flags.ControlsForEachActionInitialized) != 0;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputActionMap.Flags.ControlsForEachActionInitialized;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionMap.Flags.ControlsForEachActionInitialized;
				}
			}
		}

		private bool bindingsForEachActionInitialized
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputActionMap.Flags.BindingsForEachActionInitialized) != 0;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputActionMap.Flags.BindingsForEachActionInitialized;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionMap.Flags.BindingsForEachActionInitialized;
				}
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> actionTriggered
		{
			add
			{
				m_ActionCallbacks.AddCallback(value);
			}
			remove
			{
				m_ActionCallbacks.RemoveCallback(value);
			}
		}

		public InputActionMap()
		{
			s_NeedToResolveBindings = true;
		}

		public InputActionMap(string name)
			: this()
		{
			m_Name = name;
		}

		public void Dispose()
		{
			m_State?.Dispose();
		}

		internal int FindActionIndex(string nameOrId)
		{
			if (string.IsNullOrEmpty(nameOrId))
			{
				return -1;
			}
			if (m_Actions == null)
			{
				return -1;
			}
			SetUpActionLookupTable();
			int num = m_Actions.Length;
			if (nameOrId.StartsWith("{") && nameOrId.EndsWith("}"))
			{
				int length = nameOrId.Length - 2;
				for (int i = 0; i < num; i++)
				{
					if (string.Compare(m_Actions[i].m_Id, 0, nameOrId, 1, length) == 0)
					{
						return i;
					}
				}
			}
			if (m_ActionIndexByNameOrId.TryGetValue(nameOrId, out var value))
			{
				return value;
			}
			for (int j = 0; j < num; j++)
			{
				if (m_Actions[j].m_Id == nameOrId || string.Compare(m_Actions[j].m_Name, nameOrId, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return j;
				}
			}
			return -1;
		}

		private void SetUpActionLookupTable()
		{
			if (m_ActionIndexByNameOrId == null && m_Actions != null)
			{
				m_ActionIndexByNameOrId = new global::System.Collections.Generic.Dictionary<string, int>();
				int num = m_Actions.Length;
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.InputAction inputAction = m_Actions[i];
					inputAction.MakeSureIdIsInPlace();
					m_ActionIndexByNameOrId[inputAction.name] = i;
					m_ActionIndexByNameOrId[inputAction.m_Id] = i;
				}
			}
		}

		internal void ClearActionLookupTable()
		{
			m_ActionIndexByNameOrId?.Clear();
		}

		private int FindActionIndex(global::System.Guid id)
		{
			if (m_Actions == null)
			{
				return -1;
			}
			int num = m_Actions.Length;
			for (int i = 0; i < num; i++)
			{
				if (m_Actions[i].idDontGenerate == id)
				{
					return i;
				}
			}
			return -1;
		}

		public global::UnityEngine.InputSystem.InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			if (actionNameOrId == null)
			{
				throw new global::System.ArgumentNullException("actionNameOrId");
			}
			int num = FindActionIndex(actionNameOrId);
			if (num == -1)
			{
				if (throwIfNotFound)
				{
					throw new global::System.ArgumentException($"No action '{actionNameOrId}' in '{this}'", "actionNameOrId");
				}
				return null;
			}
			return m_Actions[num];
		}

		public global::UnityEngine.InputSystem.InputAction FindAction(global::System.Guid id)
		{
			int num = FindActionIndex(id);
			if (num == -1)
			{
				return null;
			}
			return m_Actions[num];
		}

		public bool IsUsableWithDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			if (m_Bindings == null)
			{
				return false;
			}
			global::UnityEngine.InputSystem.InputBinding[] array = m_Bindings;
			foreach (global::UnityEngine.InputSystem.InputBinding inputBinding in array)
			{
				string effectivePath = inputBinding.effectivePath;
				if (!string.IsNullOrEmpty(effectivePath) && global::UnityEngine.InputSystem.InputControlPath.Matches(effectivePath, device))
				{
					return true;
				}
			}
			return false;
		}

		public void Enable()
		{
			if (m_Actions != null && m_EnabledActionsCount != m_Actions.Length)
			{
				ResolveBindingsIfNecessary();
				m_State.EnableAllActions(this);
			}
		}

		public void Disable()
		{
			if (enabled)
			{
				m_State.DisableAllActions(this);
			}
		}

		public global::UnityEngine.InputSystem.InputActionMap Clone()
		{
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = new global::UnityEngine.InputSystem.InputActionMap
			{
				m_Name = m_Name
			};
			if (m_Actions != null)
			{
				int num = m_Actions.Length;
				global::UnityEngine.InputSystem.InputAction[] array = new global::UnityEngine.InputSystem.InputAction[num];
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.InputAction inputAction = m_Actions[i];
					array[i] = new global::UnityEngine.InputSystem.InputAction
					{
						m_Name = inputAction.m_Name,
						m_ActionMap = inputActionMap,
						m_Type = inputAction.m_Type,
						m_Interactions = inputAction.m_Interactions,
						m_Processors = inputAction.m_Processors,
						m_ExpectedControlType = inputAction.m_ExpectedControlType,
						m_Flags = inputAction.m_Flags
					};
				}
				inputActionMap.m_Actions = array;
			}
			if (m_Bindings != null)
			{
				int num2 = m_Bindings.Length;
				global::UnityEngine.InputSystem.InputBinding[] array2 = new global::UnityEngine.InputSystem.InputBinding[num2];
				global::System.Array.Copy(m_Bindings, 0, array2, 0, num2);
				for (int j = 0; j < num2; j++)
				{
					array2[j].m_Id = null;
				}
				inputActionMap.m_Bindings = array2;
			}
			return inputActionMap;
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}

		public bool Contains(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				return false;
			}
			return action.actionMap == this;
		}

		public override string ToString()
		{
			if (m_Asset != null)
			{
				return $"{m_Asset}:{m_Name}";
			}
			if (!string.IsNullOrEmpty(m_Name))
			{
				return m_Name;
			}
			return "<Unnamed Action Map>";
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputAction> GetEnumerator()
		{
			return actions.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> GetBindingsForSingleAction(global::UnityEngine.InputSystem.InputAction action)
		{
			if (!bindingsForEachActionInitialized)
			{
				SetUpPerActionControlAndBindingArrays();
			}
			return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding>(m_BindingsForEachAction, action.m_BindingsStartIndex, action.m_BindingsCount);
		}

		internal global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> GetControlsForSingleAction(global::UnityEngine.InputSystem.InputAction action)
		{
			if (!controlsForEachActionInitialized)
			{
				SetUpPerActionControlAndBindingArrays();
			}
			return new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl>(m_ControlsForEachAction, action.m_ControlStartIndex, action.m_ControlCount);
		}

		private unsafe void SetUpPerActionControlAndBindingArrays()
		{
			if (m_Bindings == null)
			{
				m_ControlsForEachAction = null;
				m_BindingsForEachAction = null;
				controlsForEachActionInitialized = true;
				bindingsForEachActionInitialized = true;
				return;
			}
			if (m_SingletonAction != null)
			{
				m_BindingsForEachAction = m_Bindings;
				m_ControlsForEachAction = m_State?.controls;
				m_SingletonAction.m_BindingsStartIndex = 0;
				m_SingletonAction.m_BindingsCount = m_Bindings.Length;
				m_SingletonAction.m_ControlStartIndex = 0;
				m_SingletonAction.m_ControlCount = m_State?.totalControlCount ?? 0;
				if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.HaveDuplicateReferences(m_ControlsForEachAction, 0, m_SingletonAction.m_ControlCount))
				{
					int num = 0;
					global::UnityEngine.InputSystem.InputControl[] array = new global::UnityEngine.InputSystem.InputControl[m_SingletonAction.m_ControlCount];
					for (int i = 0; i < m_SingletonAction.m_ControlCount; i++)
					{
						if (!global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(array, m_ControlsForEachAction[i]))
						{
							array[num] = m_ControlsForEachAction[i];
							num++;
						}
					}
					m_ControlsForEachAction = array;
					m_SingletonAction.m_ControlCount = num;
				}
			}
			else
			{
				global::UnityEngine.InputSystem.InputActionState.ActionMapIndices actionMapIndices = m_State?.FetchMapIndices(this) ?? default(global::UnityEngine.InputSystem.InputActionState.ActionMapIndices);
				for (int j = 0; j < m_Actions.Length; j++)
				{
					global::UnityEngine.InputSystem.InputAction obj = m_Actions[j];
					obj.m_BindingsCount = 0;
					obj.m_BindingsStartIndex = -1;
					obj.m_ControlCount = 0;
					obj.m_ControlStartIndex = -1;
				}
				int num2 = m_Bindings.Length;
				for (int k = 0; k < num2; k++)
				{
					global::UnityEngine.InputSystem.InputAction inputAction = FindAction(m_Bindings[k].action);
					if (inputAction != null)
					{
						inputAction.m_BindingsCount++;
					}
				}
				int num3 = 0;
				if (m_State != null && (m_ControlsForEachAction == null || m_ControlsForEachAction.Length != actionMapIndices.controlCount))
				{
					if (actionMapIndices.controlCount == 0)
					{
						m_ControlsForEachAction = null;
					}
					else
					{
						m_ControlsForEachAction = new global::UnityEngine.InputSystem.InputControl[actionMapIndices.controlCount];
					}
				}
				global::UnityEngine.InputSystem.InputBinding[] array2 = null;
				int num4 = 0;
				int num5 = 0;
				while (num5 < m_Bindings.Length)
				{
					global::UnityEngine.InputSystem.InputAction inputAction2 = FindAction(m_Bindings[num5].action);
					if (inputAction2 == null || inputAction2.m_BindingsStartIndex != -1)
					{
						num5++;
						continue;
					}
					inputAction2.m_BindingsStartIndex = ((array2 != null) ? num3 : num5);
					inputAction2.m_ControlStartIndex = num4;
					int bindingsCount = inputAction2.m_BindingsCount;
					int num6 = num5;
					for (int l = 0; l < bindingsCount; l++)
					{
						if (FindAction(m_Bindings[num6].action) != inputAction2)
						{
							if (array2 == null)
							{
								array2 = new global::UnityEngine.InputSystem.InputBinding[m_Bindings.Length];
								num3 = num6;
								global::System.Array.Copy(m_Bindings, 0, array2, 0, num6);
							}
							do
							{
								num6++;
							}
							while (FindAction(m_Bindings[num6].action) != inputAction2);
						}
						else if (num5 == num6)
						{
							num5++;
						}
						if (array2 != null)
						{
							array2[num3++] = m_Bindings[num6];
						}
						if (m_State != null && !m_Bindings[num6].isComposite)
						{
							ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref m_State.bindingStates[actionMapIndices.bindingStartIndex + num6];
							int controlCount = reference.controlCount;
							if (controlCount > 0)
							{
								int controlStartIndex = reference.controlStartIndex;
								for (int m = 0; m < controlCount; m++)
								{
									global::UnityEngine.InputSystem.InputControl inputControl = m_State.controls[controlStartIndex + m];
									if (!global::UnityEngine.InputSystem.Utilities.ArrayHelpers.ContainsReference(m_ControlsForEachAction, inputAction2.m_ControlStartIndex, inputAction2.m_ControlCount, inputControl))
									{
										m_ControlsForEachAction[num4] = inputControl;
										num4++;
										inputAction2.m_ControlCount++;
									}
								}
							}
						}
						num6++;
					}
				}
				if (array2 == null)
				{
					m_BindingsForEachAction = m_Bindings;
				}
				else
				{
					m_BindingsForEachAction = array2;
				}
			}
			controlsForEachActionInitialized = true;
			bindingsForEachActionInitialized = true;
		}

		internal void OnWantToChangeSetup()
		{
			if (asset != null)
			{
				foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in asset.actionMaps)
				{
					if (actionMap.enabled)
					{
						throw new global::System.InvalidOperationException($"Cannot add, remove, or change elements of InputActionAsset {asset} while one or more of its actions are enabled");
					}
				}
				return;
			}
			if (enabled)
			{
				throw new global::System.InvalidOperationException($"Cannot add, remove, or change elements of InputActionMap {this} while one or more of its actions are enabled");
			}
		}

		internal void OnSetupChanged()
		{
			if (m_Asset != null)
			{
				m_Asset.MarkAsDirty();
				foreach (global::UnityEngine.InputSystem.InputActionMap actionMap in m_Asset.actionMaps)
				{
					actionMap.m_State = null;
				}
			}
			else
			{
				m_State = null;
			}
			ClearCachedActionData();
			LazyResolveBindings(fullResolve: true);
		}

		internal void OnBindingModified()
		{
			ClearCachedActionData();
			LazyResolveBindings(fullResolve: true);
		}

		internal void ClearCachedActionData(bool onlyControls = false)
		{
			if (!onlyControls)
			{
				bindingsForEachActionInitialized = false;
				m_BindingsForEachAction = null;
				m_ActionIndexByNameOrId = null;
			}
			controlsForEachActionInitialized = false;
			m_ControlsForEachAction = null;
		}

		internal void GenerateId()
		{
			m_Id = global::System.Guid.NewGuid().ToString();
		}

		internal bool LazyResolveBindings(bool fullResolve)
		{
			m_ControlsForEachAction = null;
			controlsForEachActionInitialized = false;
			s_NeedToResolveBindings = true;
			if (m_State == null)
			{
				return false;
			}
			needToResolveBindings = true;
			bindingResolutionNeedsFullReResolve |= fullResolve;
			if (s_DeferBindingResolution > 0)
			{
				return false;
			}
			ResolveBindings();
			return true;
		}

		internal bool ResolveBindingsIfNecessary()
		{
			if (m_State == null || needToResolveBindings)
			{
				if (m_State != null && m_State.isProcessingControlStateChange)
				{
					return false;
				}
				ResolveBindings();
				return true;
			}
			return false;
		}

		internal void ResolveBindings()
		{
			using (k_ResolveBindingsProfilerMarker.Auto())
			{
				using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
				{
					global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory oldMemory = default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
					try
					{
						global::UnityEngine.InputSystem.InputBindingResolver resolver = default(global::UnityEngine.InputSystem.InputBindingResolver);
						bool flag = m_State == null;
						global::UnityEngine.InputSystem.Utilities.OneOrMore<global::UnityEngine.InputSystem.InputActionMap, global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputActionMap>> oneOrMore;
						if (m_Asset != null)
						{
							oneOrMore = m_Asset.actionMaps;
							resolver.bindingMask = m_Asset.m_BindingMask;
							foreach (global::UnityEngine.InputSystem.InputActionMap item in oneOrMore)
							{
								flag |= item.bindingResolutionNeedsFullReResolve;
								item.needToResolveBindings = false;
								item.bindingResolutionNeedsFullReResolve = false;
								item.controlsForEachActionInitialized = false;
							}
						}
						else
						{
							oneOrMore = this;
							flag |= bindingResolutionNeedsFullReResolve;
							needToResolveBindings = false;
							bindingResolutionNeedsFullReResolve = false;
							controlsForEachActionInitialized = false;
						}
						bool hasEnabledActions = false;
						global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> activeControls = default(global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl>);
						if (m_State != null)
						{
							oldMemory = m_State.memory.Clone();
							m_State.PrepareForBindingReResolution(flag, ref activeControls, ref hasEnabledActions);
							resolver.StartWithPreviousResolve(m_State, flag);
							m_State.memory.Dispose();
						}
						foreach (global::UnityEngine.InputSystem.InputActionMap item2 in oneOrMore)
						{
							resolver.AddActionMap(item2);
						}
						if (m_State == null)
						{
							m_State = new global::UnityEngine.InputSystem.InputActionState();
							m_State.Initialize(resolver);
						}
						else
						{
							m_State.ClaimDataFrom(resolver);
						}
						if (m_Asset != null)
						{
							foreach (global::UnityEngine.InputSystem.InputActionMap item3 in oneOrMore)
							{
								item3.m_State = m_State;
							}
							m_Asset.m_SharedStateForAllMaps = m_State;
						}
						m_State.FinishBindingResolution(hasEnabledActions, oldMemory, activeControls, flag);
					}
					finally
					{
						oldMemory.Dispose();
					}
				}
			}
		}

		public int FindBinding(global::UnityEngine.InputSystem.InputBinding mask, out global::UnityEngine.InputSystem.InputAction action)
		{
			int num = FindBindingRelativeToMap(mask);
			if (num == -1)
			{
				action = null;
				return -1;
			}
			action = m_SingletonAction ?? FindAction(bindings[num].action);
			return action.BindingIndexOnMapToBindingIndexOnAction(num);
		}

		internal int FindBindingRelativeToMap(global::UnityEngine.InputSystem.InputBinding mask)
		{
			global::UnityEngine.InputSystem.InputBinding[] array = m_Bindings;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(array);
			for (int i = 0; i < num; i++)
			{
				if (mask.Matches(ref array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public static global::UnityEngine.InputSystem.InputActionMap[] FromJson(string json)
		{
			if (json == null)
			{
				throw new global::System.ArgumentNullException("json");
			}
			return global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.InputActionMap.ReadFileJson>(json).ToMaps();
		}

		public static string ToJson(global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputActionMap> maps)
		{
			if (maps == null)
			{
				throw new global::System.ArgumentNullException("maps");
			}
			return global::UnityEngine.JsonUtility.ToJson(global::UnityEngine.InputSystem.InputActionMap.WriteFileJson.FromMaps(maps), prettyPrint: true);
		}

		public string ToJson()
		{
			return global::UnityEngine.JsonUtility.ToJson(global::UnityEngine.InputSystem.InputActionMap.WriteFileJson.FromMap(this), prettyPrint: true);
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			s_NeedToResolveBindings = true;
			m_State = null;
			m_MapIndexInState = -1;
			m_EnabledActionsCount = 0;
			if (m_Actions != null)
			{
				int num = m_Actions.Length;
				for (int i = 0; i < num; i++)
				{
					m_Actions[i].m_ActionMap = this;
				}
			}
			ClearCachedActionData();
			ClearActionLookupTable();
		}
	}
}

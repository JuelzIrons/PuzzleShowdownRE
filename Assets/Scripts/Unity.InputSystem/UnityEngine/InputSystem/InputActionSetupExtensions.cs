namespace UnityEngine.InputSystem
{
	public static class InputActionSetupExtensions
	{
		public struct BindingSyntax
		{
			private readonly global::UnityEngine.InputSystem.InputActionMap m_ActionMap;

			private readonly global::UnityEngine.InputSystem.InputAction m_Action;

			internal readonly int m_BindingIndexInMap;

			public bool valid
			{
				get
				{
					if (m_ActionMap != null && m_BindingIndexInMap >= 0)
					{
						return m_BindingIndexInMap < global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMap.m_Bindings);
					}
					return false;
				}
			}

			public int bindingIndex
			{
				get
				{
					if (!valid)
					{
						return -1;
					}
					if (m_Action != null)
					{
						return m_Action.BindingIndexOnMapToBindingIndexOnAction(m_BindingIndexInMap);
					}
					return m_BindingIndexInMap;
				}
			}

			public global::UnityEngine.InputSystem.InputBinding binding
			{
				get
				{
					if (!valid)
					{
						throw new global::System.InvalidOperationException("BindingSyntax accessor is not valid");
					}
					return m_ActionMap.m_Bindings[m_BindingIndexInMap];
				}
			}

			internal BindingSyntax(global::UnityEngine.InputSystem.InputActionMap map, int bindingIndexInMap, global::UnityEngine.InputSystem.InputAction action = null)
			{
				m_ActionMap = map;
				m_BindingIndexInMap = bindingIndexInMap;
				m_Action = action;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithName(string name)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].name = name;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithPath(string path)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].path = path;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithGroup(string group)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(group))
				{
					throw new global::System.ArgumentException("Group name cannot be null or empty", "group");
				}
				if (group.IndexOf(';') != -1)
				{
					throw new global::System.ArgumentException($"Group name cannot contain separator character '{';'}'", "group");
				}
				return WithGroups(group);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithGroups(string groups)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(groups))
				{
					return this;
				}
				string groups2 = m_ActionMap.m_Bindings[m_BindingIndexInMap].groups;
				if (!string.IsNullOrEmpty(groups2))
				{
					groups = string.Join(";", groups2, groups);
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].groups = groups;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteraction(string interaction)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interaction))
				{
					throw new global::System.ArgumentException("Interaction cannot be null or empty", "interaction");
				}
				if (interaction.IndexOf(';') != -1)
				{
					throw new global::System.ArgumentException($"Interaction string cannot contain separator character '{';'}'", "interaction");
				}
				return WithInteractions(interaction);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteractions(string interactions)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interactions))
				{
					return this;
				}
				string interactions2 = m_ActionMap.m_Bindings[m_BindingIndexInMap].interactions;
				if (!string.IsNullOrEmpty(interactions2))
				{
					interactions = string.Join(";", interactions2, interactions);
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].interactions = interactions;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithInteraction<TInteraction>() where TInteraction : global::UnityEngine.InputSystem.IInputInteraction
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = global::UnityEngine.InputSystem.InputInteraction.s_Interactions.FindNameForType(typeof(TInteraction));
				if (internedString.IsEmpty())
				{
					throw new global::System.NotSupportedException($"Type '{typeof(TInteraction)}' has not been registered as a interaction");
				}
				return WithInteraction(internedString);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessor(string processor)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processor))
				{
					throw new global::System.ArgumentException("Processor cannot be null or empty", "processor");
				}
				if (processor.IndexOf(';') != -1)
				{
					throw new global::System.ArgumentException($"Processor string cannot contain separator character '{';'}'", "processor");
				}
				return WithProcessors(processor);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessors(string processors)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processors))
				{
					return this;
				}
				string processors2 = m_ActionMap.m_Bindings[m_BindingIndexInMap].processors;
				if (!string.IsNullOrEmpty(processors2))
				{
					processors = string.Join(";", processors2, processors);
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].processors = processors;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax WithProcessor<TProcessor>()
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = global::UnityEngine.InputSystem.InputProcessor.s_Processors.FindNameForType(typeof(TProcessor));
				if (internedString.IsEmpty())
				{
					throw new global::System.NotSupportedException($"Type '{typeof(TProcessor)}' has not been registered as a processor");
				}
				return WithProcessor(internedString);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax Triggering(global::UnityEngine.InputSystem.InputAction action)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				if (action == null)
				{
					throw new global::System.ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new global::System.ArgumentException($"Cannot change the action a binding triggers on singleton action '{action}'", "action");
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap].action = action.name;
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax To(global::UnityEngine.InputSystem.InputBinding binding)
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Accessor is not valid");
				}
				m_ActionMap.m_Bindings[m_BindingIndexInMap] = binding;
				if (m_ActionMap.m_SingletonAction != null)
				{
					m_ActionMap.m_Bindings[m_BindingIndexInMap].action = m_ActionMap.m_SingletonAction.name;
				}
				m_ActionMap.OnBindingModified();
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextBinding()
			{
				return Iterate(next: true);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousBinding()
			{
				return Iterate(next: false);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new global::System.ArgumentNullException("partName");
				}
				return IteratePartBinding(next: true, partName);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new global::System.ArgumentNullException("partName");
				}
				return IteratePartBinding(next: false, partName);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax NextCompositeBinding(string compositeName = null)
			{
				return IterateCompositeBinding(next: true, compositeName);
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax PreviousCompositeBinding(string compositeName = null)
			{
				return IterateCompositeBinding(next: false, compositeName);
			}

			private global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax Iterate(bool next)
			{
				if (m_ActionMap == null)
				{
					return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
				}
				global::UnityEngine.InputSystem.InputBinding[] bindings = m_ActionMap.m_Bindings;
				if (bindings == null)
				{
					return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
				}
				int num = m_BindingIndexInMap;
				do
				{
					num += (next ? 1 : (-1));
					if (num < 0 || num >= bindings.Length)
					{
						return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
					}
				}
				while (m_Action != null && !bindings[num].TriggersAction(m_Action));
				return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(m_ActionMap, num, m_Action);
			}

			private global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax IterateCompositeBinding(bool next, string compositeName)
			{
				global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax result = Iterate(next);
				while (result.valid)
				{
					if (result.binding.isComposite)
					{
						if (compositeName == null)
						{
							return result;
						}
						if (compositeName.Equals(result.binding.name, global::System.StringComparison.InvariantCultureIgnoreCase))
						{
							return result;
						}
						string value = global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseName(result.binding.path);
						if (compositeName.Equals(value, global::System.StringComparison.InvariantCultureIgnoreCase))
						{
							return result;
						}
					}
					result = result.Iterate(next);
				}
				return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
			}

			private global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax IteratePartBinding(bool next, string partName)
			{
				if (!valid)
				{
					return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
				}
				if (binding.isComposite)
				{
					if (!next)
					{
						return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
					}
				}
				else if (!binding.isPartOfComposite)
				{
					return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
				}
				global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax result = Iterate(next);
				while (result.valid)
				{
					if (!result.binding.isPartOfComposite)
					{
						return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
					}
					if (partName.Equals(result.binding.name, global::System.StringComparison.InvariantCultureIgnoreCase))
					{
						return result;
					}
					result = result.Iterate(next);
				}
				return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
			}

			public void Erase()
			{
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Instance not valid");
				}
				bool isComposite = m_ActionMap.m_Bindings[m_BindingIndexInMap].isComposite;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref m_ActionMap.m_Bindings, m_BindingIndexInMap);
				if (isComposite)
				{
					while (m_BindingIndexInMap < global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMap.m_Bindings) && m_ActionMap.m_Bindings[m_BindingIndexInMap].isPartOfComposite)
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref m_ActionMap.m_Bindings, m_BindingIndexInMap);
					}
				}
				m_Action.m_BindingsCount = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_ActionMap.m_Bindings);
				m_ActionMap.OnBindingModified();
				if (m_ActionMap.m_SingletonAction != null)
				{
					m_ActionMap.m_SingletonAction.m_SingletonActionBindings = m_ActionMap.m_Bindings;
				}
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax InsertPartBinding(string partName, string path)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new global::System.ArgumentNullException("partName");
				}
				if (!valid)
				{
					throw new global::System.InvalidOperationException("Binding accessor is not valid");
				}
				global::UnityEngine.InputSystem.InputBinding inputBinding = binding;
				if (!inputBinding.isPartOfComposite && !inputBinding.isComposite)
				{
					throw new global::System.InvalidOperationException("Binding accessor must point to composite or part binding");
				}
				AddBindingInternal(m_ActionMap, new global::UnityEngine.InputSystem.InputBinding
				{
					path = path,
					isPartOfComposite = true,
					name = partName,
					action = m_Action?.name
				}, m_BindingIndexInMap + 1);
				return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(m_ActionMap, m_BindingIndexInMap + 1, m_Action);
			}
		}

		public struct CompositeSyntax
		{
			private readonly global::UnityEngine.InputSystem.InputAction m_Action;

			private readonly global::UnityEngine.InputSystem.InputActionMap m_ActionMap;

			private int m_BindingIndexInMap;

			public int bindingIndex
			{
				get
				{
					if (m_ActionMap == null)
					{
						return -1;
					}
					if (m_Action != null)
					{
						return m_Action.BindingIndexOnMapToBindingIndexOnAction(m_BindingIndexInMap);
					}
					return m_BindingIndexInMap;
				}
			}

			internal CompositeSyntax(global::UnityEngine.InputSystem.InputActionMap map, global::UnityEngine.InputSystem.InputAction action, int compositeIndex)
			{
				m_Action = action;
				m_ActionMap = map;
				m_BindingIndexInMap = compositeIndex;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.CompositeSyntax With(string name, string binding, string groups = null, string processors = null)
			{
				using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolution())
				{
					int bindingIndexInMap;
					if (m_Action != null)
					{
						bindingIndexInMap = m_Action.AddBinding(binding, null, processors, groups).m_BindingIndexInMap;
					}
					else
					{
						bindingIndexInMap = m_ActionMap.AddBinding(binding, null, groups, null, processors).m_BindingIndexInMap;
					}
					m_ActionMap.m_Bindings[bindingIndexInMap].name = name;
					m_ActionMap.m_Bindings[bindingIndexInMap].isPartOfComposite = true;
				}
				return this;
			}
		}

		public struct ControlSchemeSyntax
		{
			private readonly global::UnityEngine.InputSystem.InputActionAsset m_Asset;

			private readonly int m_ControlSchemeIndex;

			private global::UnityEngine.InputSystem.InputControlScheme m_ControlScheme;

			internal ControlSchemeSyntax(global::UnityEngine.InputSystem.InputActionAsset asset, int index)
			{
				m_Asset = asset;
				m_ControlSchemeIndex = index;
				m_ControlScheme = default(global::UnityEngine.InputSystem.InputControlScheme);
			}

			internal ControlSchemeSyntax(global::UnityEngine.InputSystem.InputControlScheme controlScheme)
			{
				m_Asset = null;
				m_ControlSchemeIndex = -1;
				m_ControlScheme = controlScheme;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithBindingGroup(string bindingGroup)
			{
				if (string.IsNullOrEmpty(bindingGroup))
				{
					throw new global::System.ArgumentNullException("bindingGroup");
				}
				if (m_Asset == null)
				{
					m_ControlScheme.m_BindingGroup = bindingGroup;
				}
				else
				{
					m_Asset.m_ControlSchemes[m_ControlSchemeIndex].bindingGroup = bindingGroup;
				}
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
			{
				return WithRequiredDevice(DeviceTypeToControlPath<TDevice>());
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
			{
				return WithOptionalDevice(DeviceTypeToControlPath<TDevice>());
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
			{
				return OrWithRequiredDevice(DeviceTypeToControlPath<TDevice>());
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
			{
				return OrWithOptionalDevice(DeviceTypeToControlPath<TDevice>());
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice(string controlPath)
			{
				AddDeviceEntry(controlPath, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.None);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice(string controlPath)
			{
				AddDeviceEntry(controlPath, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Optional);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice(string controlPath)
			{
				AddDeviceEntry(controlPath, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice(string controlPath)
			{
				AddDeviceEntry(controlPath, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Optional | global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			private string DeviceTypeToControlPath<TDevice>() where TDevice : global::UnityEngine.InputSystem.InputDevice
			{
				string text = global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice)).ToString();
				if (string.IsNullOrEmpty(text))
				{
					text = typeof(TDevice).Name;
				}
				return "<" + text + ">";
			}

			public global::UnityEngine.InputSystem.InputControlScheme Done()
			{
				if (m_Asset != null)
				{
					return m_Asset.m_ControlSchemes[m_ControlSchemeIndex];
				}
				return m_ControlScheme;
			}

			private void AddDeviceEntry(string controlPath, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags flags)
			{
				if (string.IsNullOrEmpty(controlPath))
				{
					throw new global::System.ArgumentNullException("controlPath");
				}
				global::UnityEngine.InputSystem.InputControlScheme inputControlScheme = ((m_Asset != null) ? m_Asset.m_ControlSchemes[m_ControlSchemeIndex] : m_ControlScheme);
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref inputControlScheme.m_DeviceRequirements, new global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement
				{
					m_ControlPath = controlPath,
					m_Flags = flags
				});
				if (m_Asset == null)
				{
					m_ControlScheme = inputControlScheme;
				}
				else
				{
					m_Asset.m_ControlSchemes[m_ControlSchemeIndex] = inputControlScheme;
				}
			}
		}

		public static global::UnityEngine.InputSystem.InputActionMap AddActionMap(this global::UnityEngine.InputSystem.InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (asset.FindActionMap(name) != null)
			{
				throw new global::System.InvalidOperationException("An action map called '" + name + "' already exists in the asset");
			}
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = new global::UnityEngine.InputSystem.InputActionMap(name);
			inputActionMap.GenerateId();
			asset.AddActionMap(inputActionMap);
			return inputActionMap;
		}

		public static void AddActionMap(this global::UnityEngine.InputSystem.InputActionAsset asset, global::UnityEngine.InputSystem.InputActionMap map)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new global::System.ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(map.name))
			{
				throw new global::System.InvalidOperationException("Maps added to an input action asset must be named");
			}
			if (map.asset != null)
			{
				throw new global::System.InvalidOperationException($"Cannot add map '{map}' to asset '{asset}' as it has already been added to asset '{map.asset}'");
			}
			if (asset.FindActionMap(map.name) != null)
			{
				throw new global::System.InvalidOperationException("An action map called '" + map.name + "' already exists in the asset");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref asset.m_ActionMaps, map);
			map.m_Asset = asset;
			asset.OnSetupChanged();
		}

		public static void RemoveActionMap(this global::UnityEngine.InputSystem.InputActionAsset asset, global::UnityEngine.InputSystem.InputActionMap map)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new global::System.ArgumentNullException("map");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			if (!(map.m_Asset != asset))
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Erase(ref asset.m_ActionMaps, map);
				map.m_Asset = null;
				asset.OnSetupChanged();
			}
		}

		public static void RemoveActionMap(this global::UnityEngine.InputSystem.InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new global::System.ArgumentNullException("nameOrId");
			}
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = asset.FindActionMap(nameOrId);
			if (inputActionMap != null)
			{
				asset.RemoveActionMap(inputActionMap);
			}
		}

		public static global::UnityEngine.InputSystem.InputAction AddAction(this global::UnityEngine.InputSystem.InputActionMap map, string name, global::UnityEngine.InputSystem.InputActionType type = global::UnityEngine.InputSystem.InputActionType.Value, string binding = null, string interactions = null, string processors = null, string groups = null, string expectedControlLayout = null)
		{
			if (map == null)
			{
				throw new global::System.ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentException("Action must have name", "name");
			}
			map.OnWantToChangeSetup();
			if (map.FindAction(name) != null)
			{
				throw new global::System.InvalidOperationException("Cannot add action with duplicate name '" + name + "' to set '" + map.name + "'");
			}
			global::UnityEngine.InputSystem.InputAction inputAction = new global::UnityEngine.InputSystem.InputAction(name, type)
			{
				expectedControlType = expectedControlLayout
			};
			inputAction.GenerateId();
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref map.m_Actions, inputAction);
			inputAction.m_ActionMap = map;
			if (!string.IsNullOrEmpty(binding))
			{
				inputAction.AddBinding(binding, interactions, processors, groups);
			}
			else
			{
				if (!string.IsNullOrEmpty(groups))
				{
					throw new global::System.ArgumentException($"No binding path was specified for action '{inputAction}' but groups was specified ('{groups}'); cannot apply groups without binding", "groups");
				}
				inputAction.m_Interactions = interactions;
				inputAction.m_Processors = processors;
				map.OnSetupChanged();
			}
			return inputAction;
		}

		public static void RemoveAction(this global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::UnityEngine.InputSystem.InputActionMap actionMap = action.actionMap;
			if (actionMap == null)
			{
				throw new global::System.ArgumentException($"Action '{action}' does not belong to an action map; nowhere to remove from", "action");
			}
			actionMap.OnWantToChangeSetup();
			global::UnityEngine.InputSystem.InputBinding[] array = action.bindings.ToArray();
			int index = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(actionMap.m_Actions, action);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref actionMap.m_Actions, index);
			action.m_ActionMap = null;
			action.m_SingletonActionBindings = array;
			int num = ((actionMap.m_Bindings != null) ? (actionMap.m_Bindings.Length - array.Length) : 0);
			if (num == 0)
			{
				actionMap.m_Bindings = null;
			}
			else
			{
				global::UnityEngine.InputSystem.InputBinding[] array2 = new global::UnityEngine.InputSystem.InputBinding[num];
				global::UnityEngine.InputSystem.InputBinding[] bindings = actionMap.m_Bindings;
				int num2 = 0;
				foreach (global::UnityEngine.InputSystem.InputBinding binding in bindings)
				{
					if (global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOf(array, (global::UnityEngine.InputSystem.InputBinding b) => b == binding) == -1)
					{
						array2[num2++] = binding;
					}
				}
				actionMap.m_Bindings = array2;
			}
			actionMap.OnSetupChanged();
		}

		public static void RemoveAction(this global::UnityEngine.InputSystem.InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new global::System.ArgumentNullException("nameOrId");
			}
			asset.FindAction(nameOrId)?.RemoveAction();
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputAction action, string path, string interactions = null, string processors = null, string groups = null)
		{
			return action.AddBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				path = path,
				interactions = interactions,
				processors = processors,
				groups = groups
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			return action.AddBinding(control.path);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding binding = default(global::UnityEngine.InputSystem.InputBinding))
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			binding.action = action.name;
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			int bindingIndexInMap = AddBindingInternal(orCreateActionMap, binding);
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(orCreateActionMap, bindingIndexInMap);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputActionMap actionMap, string path, string interactions = null, string groups = null, string action = null, string processors = null)
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path", "Binding path cannot be null");
			}
			return actionMap.AddBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				path = path,
				interactions = interactions,
				groups = groups,
				action = action,
				processors = processors
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputActionMap actionMap, string path, global::UnityEngine.InputSystem.InputAction action, string interactions = null, string groups = null)
		{
			if (action != null && action.actionMap != actionMap)
			{
				throw new global::System.ArgumentException($"Action '{action}' is not part of action map '{actionMap}'", "action");
			}
			if (action == null)
			{
				return actionMap.AddBinding(path, interactions, groups);
			}
			return actionMap.AddBinding(path, action.id, interactions, groups);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputActionMap actionMap, string path, global::System.Guid action, string interactions = null, string groups = null)
		{
			if (action == global::System.Guid.Empty)
			{
				return actionMap.AddBinding(path, interactions, groups);
			}
			return actionMap.AddBinding(path, interactions, groups, action.ToString());
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax AddBinding(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputBinding binding)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (binding.path == null)
			{
				throw new global::System.ArgumentException("Binding path cannot be null", "binding");
			}
			int bindingIndexInMap = AddBindingInternal(actionMap, binding);
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(actionMap, bindingIndexInMap);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.CompositeSyntax AddCompositeBinding(this global::UnityEngine.InputSystem.InputAction action, string composite, string interactions = null, string processors = null)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(composite))
			{
				throw new global::System.ArgumentException("Composite name cannot be null or empty", "composite");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			global::UnityEngine.InputSystem.InputBinding binding = new global::UnityEngine.InputSystem.InputBinding
			{
				name = global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseName(composite),
				path = composite,
				interactions = interactions,
				processors = processors,
				isComposite = true,
				action = action.name
			};
			int compositeIndex = AddBindingInternal(orCreateActionMap, binding);
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.CompositeSyntax(orCreateActionMap, action, compositeIndex);
		}

		private static int AddBindingInternal(global::UnityEngine.InputSystem.InputActionMap map, global::UnityEngine.InputSystem.InputBinding binding, int bindingIndex = -1)
		{
			if (string.IsNullOrEmpty(binding.m_Id))
			{
				binding.GenerateId();
			}
			if (bindingIndex < 0)
			{
				bindingIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref map.m_Bindings, binding);
			}
			else
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.InsertAt(ref map.m_Bindings, bindingIndex, binding);
			}
			if (map.asset != null)
			{
				map.asset.MarkAsDirty();
			}
			if (map.m_SingletonAction != null)
			{
				map.m_SingletonAction.m_SingletonActionBindings = map.m_Bindings;
			}
			map.OnBindingModified();
			return bindingIndex;
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(this global::UnityEngine.InputSystem.InputAction action, int index)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			int bindingIndexInMap = action.BindingIndexOnActionToBindingIndexOnMap(index);
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(action.GetOrCreateActionMap(), bindingIndexInMap, action);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(this global::UnityEngine.InputSystem.InputAction action, string name)
		{
			return action.ChangeBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				name = name
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(this global::UnityEngine.InputSystem.InputActionMap actionMap, int index)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (index < 0 || index >= global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(actionMap.m_Bindings))
			{
				throw new global::System.ArgumentOutOfRangeException("index");
			}
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(actionMap, index);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this global::UnityEngine.InputSystem.InputAction action, string id)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return action.ChangeBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				m_Id = id
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this global::UnityEngine.InputSystem.InputAction action, global::System.Guid id)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return action.ChangeBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				id = id
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithGroup(this global::UnityEngine.InputSystem.InputAction action, string group)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return action.ChangeBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				groups = group
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBindingWithPath(this global::UnityEngine.InputSystem.InputAction action, string path)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return action.ChangeBinding(new global::UnityEngine.InputSystem.InputBinding
			{
				path = path
			});
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeBinding(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding match)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			int num = -1;
			_ = action.idDontGenerate;
			match.action = action.id.ToString();
			num = orCreateActionMap.FindBindingRelativeToMap(match);
			if (num == -1)
			{
				match.action = action.name;
				num = orCreateActionMap.FindBindingRelativeToMap(match);
			}
			if (num == -1)
			{
				return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
			}
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(orCreateActionMap, num, action);
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax ChangeCompositeBinding(this global::UnityEngine.InputSystem.InputAction action, string compositeName)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(compositeName))
			{
				throw new global::System.ArgumentNullException("compositeName");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			global::UnityEngine.InputSystem.InputBinding[] bindings = orCreateActionMap.m_Bindings;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(bindings);
			for (int i = 0; i < num; i++)
			{
				ref global::UnityEngine.InputSystem.InputBinding reference = ref bindings[i];
				if (reference.isComposite && reference.TriggersAction(action) && (compositeName.Equals(reference.name, global::System.StringComparison.InvariantCultureIgnoreCase) || compositeName.Equals(global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseName(reference.path), global::System.StringComparison.InvariantCultureIgnoreCase)))
				{
					return new global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax(orCreateActionMap, i, action);
				}
			}
			return default(global::UnityEngine.InputSystem.InputActionSetupExtensions.BindingSyntax);
		}

		public static void Rename(this global::UnityEngine.InputSystem.InputAction action, string newName)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new global::System.ArgumentNullException("newName");
			}
			if (action.name == newName)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputActionMap actionMap = action.actionMap;
			if (actionMap?.FindAction(newName) != null)
			{
				throw new global::System.InvalidOperationException($"Cannot rename '{action}' to '{newName}' in map '{actionMap}' as the map already contains an action with that name");
			}
			string name = action.m_Name;
			action.m_Name = newName;
			actionMap?.ClearActionLookupTable();
			if (actionMap?.asset != null)
			{
				actionMap?.asset.MarkAsDirty();
			}
			global::UnityEngine.InputSystem.InputBinding[] bindings = action.GetOrCreateActionMap().m_Bindings;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(bindings);
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].action = newName;
				}
			}
		}

		public static void AddControlScheme(this global::UnityEngine.InputSystem.InputActionAsset asset, global::UnityEngine.InputSystem.InputControlScheme controlScheme)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(controlScheme.name))
			{
				throw new global::System.ArgumentException("Cannot add control scheme without name to asset " + asset.name, "controlScheme");
			}
			if (asset.FindControlScheme(controlScheme.name).HasValue)
			{
				throw new global::System.InvalidOperationException("Asset '" + asset.name + "' already contains a control scheme called '" + controlScheme.name + "'");
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref asset.m_ControlSchemes, controlScheme);
			asset.MarkAsDirty();
		}

		public static global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax AddControlScheme(this global::UnityEngine.InputSystem.InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			int count = asset.controlSchemes.Count;
			asset.AddControlScheme(new global::UnityEngine.InputSystem.InputControlScheme(name));
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(asset, count);
		}

		public static void RemoveControlScheme(this global::UnityEngine.InputSystem.InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			int num = asset.FindControlSchemeIndex(name);
			if (num != -1)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref asset.m_ControlSchemes, num);
			}
			asset.MarkAsDirty();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme WithBindingGroup(this global::UnityEngine.InputSystem.InputControlScheme scheme, string bindingGroup)
		{
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithBindingGroup(bindingGroup).Done();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme WithDevice(this global::UnityEngine.InputSystem.InputControlScheme scheme, string controlPath, bool required)
		{
			if (required)
			{
				return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
			}
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme WithRequiredDevice(this global::UnityEngine.InputSystem.InputControlScheme scheme, string controlPath)
		{
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme WithOptionalDevice(this global::UnityEngine.InputSystem.InputControlScheme scheme, string controlPath)
		{
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme OrWithRequiredDevice(this global::UnityEngine.InputSystem.InputControlScheme scheme, string controlPath)
		{
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithRequiredDevice(controlPath).Done();
		}

		public static global::UnityEngine.InputSystem.InputControlScheme OrWithOptionalDevice(this global::UnityEngine.InputSystem.InputControlScheme scheme, string controlPath)
		{
			return new global::UnityEngine.InputSystem.InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithOptionalDevice(controlPath).Done();
		}
	}
}

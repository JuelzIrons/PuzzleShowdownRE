namespace UnityEngine.InputSystem
{
	internal struct InputBindingResolver : global::System.IDisposable
	{
		public int totalProcessorCount;

		public int totalCompositeCount;

		public int totalInteractionCount;

		public global::UnityEngine.InputSystem.InputActionMap[] maps;

		public global::UnityEngine.InputSystem.InputControl[] controls;

		public global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory memory;

		public global::UnityEngine.InputSystem.IInputInteraction[] interactions;

		public global::UnityEngine.InputSystem.InputProcessor[] processors;

		public global::UnityEngine.InputSystem.InputBindingComposite[] composites;

		public global::UnityEngine.InputSystem.InputBinding? bindingMask;

		private bool m_IsControlOnlyResolve;

		private global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters> m_Parameters;

		public int totalMapCount => memory.mapCount;

		public int totalActionCount => memory.actionCount;

		public int totalBindingCount => memory.bindingCount;

		public int totalControlCount => memory.controlCount;

		public void Dispose()
		{
			memory.Dispose();
		}

		public void StartWithPreviousResolve(global::UnityEngine.InputSystem.InputActionState state, bool isFullResolve)
		{
			m_IsControlOnlyResolve = !isFullResolve;
			maps = state.maps;
			interactions = state.interactions;
			processors = state.processors;
			composites = state.composites;
			controls = state.controls;
			if (isFullResolve)
			{
				if (maps != null)
				{
					global::System.Array.Clear(maps, 0, state.totalMapCount);
				}
				if (interactions != null)
				{
					global::System.Array.Clear(interactions, 0, state.totalInteractionCount);
				}
				if (processors != null)
				{
					global::System.Array.Clear(processors, 0, state.totalProcessorCount);
				}
				if (composites != null)
				{
					global::System.Array.Clear(composites, 0, state.totalCompositeCount);
				}
			}
			if (controls != null)
			{
				global::System.Array.Clear(controls, 0, state.totalControlCount);
			}
			state.maps = null;
			state.interactions = null;
			state.processors = null;
			state.composites = null;
			state.controls = null;
		}

		public unsafe void AddActionMap(global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			global::UnityEngine.InputSystem.InputSystem.EnsureInitialized();
			global::UnityEngine.InputSystem.InputAction[] actions = actionMap.m_Actions;
			global::UnityEngine.InputSystem.InputBinding[] bindings = actionMap.m_Bindings;
			int num = ((bindings != null) ? bindings.Length : 0);
			int num2 = ((actions != null) ? actions.Length : 0);
			int num3 = totalMapCount;
			int num4 = totalActionCount;
			int num5 = totalBindingCount;
			int controlStartIndex = totalControlCount;
			int num6 = totalInteractionCount;
			int num7 = totalProcessorCount;
			int num8 = totalCompositeCount;
			global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory unmanagedMemory = default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
			unmanagedMemory.Allocate(totalMapCount + 1, totalActionCount + num2, totalBindingCount + num, interactionCount: totalInteractionCount, compositeCount: totalCompositeCount, controlCount: totalControlCount);
			if (memory.isAllocated)
			{
				unmanagedMemory.CopyDataFrom(memory);
			}
			int num9 = -1;
			int num10 = -1;
			int currentCompositePartCount = 0;
			int num11 = -1;
			global::UnityEngine.InputSystem.InputAction inputAction = null;
			global::UnityEngine.InputSystem.InputBinding? inputBinding = actionMap.m_BindingMask;
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices = actionMap.devices;
			bool flag = actionMap.m_SingletonAction != null;
			global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> matches = new global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl>(global::Unity.Collections.Allocator.Temp);
			try
			{
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStates = unmanagedMemory.bindingStates;
					ref global::UnityEngine.InputSystem.InputBinding reference = ref bindings[i];
					int num12 = num5 + i;
					bool isComposite = reference.isComposite;
					bool flag2 = !isComposite && reference.isPartOfComposite;
					global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = bindingStates + num12;
					try
					{
						int controlStartIndex2 = 0;
						int num13 = -1;
						int num14 = -1;
						int actionIndex = -1;
						int partIndex = -1;
						int num15 = 0;
						int num16 = 0;
						int num17 = 0;
						if (flag2 && num9 == -1)
						{
							throw new global::System.InvalidOperationException($"Binding '{reference}' is marked as being part of a composite but the preceding binding is not a composite");
						}
						int num18 = -1;
						string action = reference.action;
						global::UnityEngine.InputSystem.InputAction inputAction2 = null;
						if (!flag2)
						{
							if (flag)
							{
								num18 = 0;
							}
							else if (!string.IsNullOrEmpty(action))
							{
								num18 = actionMap.FindActionIndex(action);
							}
							if (num18 != -1)
							{
								inputAction2 = actions[num18];
							}
						}
						else
						{
							num18 = num11;
							inputAction2 = inputAction;
						}
						if (isComposite)
						{
							num9 = num12;
							inputAction = inputAction2;
							num11 = num18;
						}
						string effectivePath = reference.effectivePath;
						bool flag3 = string.IsNullOrEmpty(effectivePath) || inputAction2 == null || (!isComposite && bindingMask.HasValue && !bindingMask.Value.Matches(ref reference, global::UnityEngine.InputSystem.InputBinding.MatchOptions.EmptyGroupMatchesAny)) || (!isComposite && inputBinding.HasValue && !inputBinding.Value.Matches(ref reference, global::UnityEngine.InputSystem.InputBinding.MatchOptions.EmptyGroupMatchesAny)) || (!isComposite && inputAction2 != null && inputAction2.m_BindingMask.HasValue && !inputAction2.m_BindingMask.Value.Matches(ref reference, global::UnityEngine.InputSystem.InputBinding.MatchOptions.EmptyGroupMatchesAny));
						if (!flag3 && !isComposite)
						{
							controlStartIndex2 = memory.controlCount + matches.Count;
							if (devices.HasValue)
							{
								global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice> value = devices.Value;
								for (int j = 0; j < value.Count; j++)
								{
									global::UnityEngine.InputSystem.InputDevice inputDevice = value[j];
									if (inputDevice.added)
									{
										num15 += global::UnityEngine.InputSystem.InputControlPath.TryFindControls(inputDevice, effectivePath, 0, ref matches);
									}
								}
							}
							else
							{
								num15 = global::UnityEngine.InputSystem.InputSystem.FindControls(effectivePath, ref matches);
							}
						}
						if (!flag3)
						{
							string effectiveProcessors = reference.effectiveProcessors;
							if (!string.IsNullOrEmpty(effectiveProcessors))
							{
								num14 = InstantiateWithParameters(global::UnityEngine.InputSystem.InputProcessor.s_Processors, effectiveProcessors, ref processors, ref totalProcessorCount, actionMap, ref reference);
								if (num14 != -1)
								{
									num17 = totalProcessorCount - num14;
								}
							}
							if (!string.IsNullOrEmpty(inputAction2.m_Processors))
							{
								int num19 = InstantiateWithParameters(global::UnityEngine.InputSystem.InputProcessor.s_Processors, inputAction2.m_Processors, ref processors, ref totalProcessorCount, actionMap, ref reference);
								if (num19 != -1)
								{
									if (num14 == -1)
									{
										num14 = num19;
									}
									num17 += totalProcessorCount - num19;
								}
							}
							if (flag2)
							{
								if (num9 != -1)
								{
									num13 = bindingStates[num9].interactionStartIndex;
									num16 = bindingStates[num9].interactionCount;
								}
							}
							else
							{
								string effectiveInteractions = reference.effectiveInteractions;
								if (!string.IsNullOrEmpty(effectiveInteractions))
								{
									num13 = InstantiateWithParameters(global::UnityEngine.InputSystem.InputInteraction.s_Interactions, effectiveInteractions, ref interactions, ref totalInteractionCount, actionMap, ref reference);
									if (num13 != -1)
									{
										num16 = totalInteractionCount - num13;
									}
								}
								if (!string.IsNullOrEmpty(inputAction2.m_Interactions))
								{
									int num20 = InstantiateWithParameters(global::UnityEngine.InputSystem.InputInteraction.s_Interactions, inputAction2.m_Interactions, ref interactions, ref totalInteractionCount, actionMap, ref reference);
									if (num20 != -1)
									{
										if (num13 == -1)
										{
											num13 = num20;
										}
										num16 += totalInteractionCount - num20;
									}
								}
							}
							if (isComposite)
							{
								global::UnityEngine.InputSystem.InputBindingComposite value2 = InstantiateBindingComposite(ref reference, actionMap);
								num10 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref composites, ref totalCompositeCount, value2);
								controlStartIndex2 = memory.controlCount + matches.Count;
							}
							else if (!flag2 && num9 != -1)
							{
								currentCompositePartCount = 0;
								num9 = -1;
								num10 = -1;
								inputAction = null;
								num11 = -1;
							}
						}
						if (flag2 && num9 != -1 && num15 > 0)
						{
							if (string.IsNullOrEmpty(reference.name))
							{
								throw new global::System.InvalidOperationException($"Binding '{reference}' that is part of composite '{composites[num10]}' is missing a name");
							}
							partIndex = AssignCompositePartIndex(composites[num10], reference.name, ref currentCompositePartCount);
							bindingStates[num9].controlCount += num15;
							actionIndex = bindingStates[num9].actionIndex;
						}
						else if (num18 != -1)
						{
							actionIndex = num4 + num18;
						}
						*ptr = new global::UnityEngine.InputSystem.InputActionState.BindingState
						{
							controlStartIndex = controlStartIndex2,
							controlCount = num15,
							interactionStartIndex = num13,
							interactionCount = num16,
							processorStartIndex = num14,
							processorCount = num17,
							isComposite = isComposite,
							isPartOfComposite = reference.isPartOfComposite,
							partIndex = partIndex,
							actionIndex = actionIndex,
							compositeOrCompositeBindingIndex = (isComposite ? num10 : num9),
							mapIndex = totalMapCount,
							wantsInitialStateCheck = (inputAction2?.wantsInitialStateCheck ?? false)
						};
					}
					catch (global::System.Exception ex)
					{
						global::UnityEngine.Debug.LogError($"{ex.GetType().Name} while resolving binding '{reference}' in action map '{actionMap}'");
						global::UnityEngine.Debug.LogException(ex);
						if (global::UnityEngine.InputSystem.Utilities.ExceptionHelpers.IsExceptionIndicatingBugInCode(ex))
						{
							throw;
						}
					}
				}
				int count = matches.Count;
				int num21 = memory.controlCount + count;
				if (unmanagedMemory.interactionCount != totalInteractionCount || unmanagedMemory.compositeCount != totalCompositeCount || unmanagedMemory.controlCount != num21)
				{
					global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory unmanagedMemory2 = default(global::UnityEngine.InputSystem.InputActionState.UnmanagedMemory);
					unmanagedMemory2.Allocate(unmanagedMemory.mapCount, unmanagedMemory.actionCount, unmanagedMemory.bindingCount, num21, totalInteractionCount, totalCompositeCount);
					unmanagedMemory2.CopyDataFrom(unmanagedMemory);
					unmanagedMemory.Dispose();
					unmanagedMemory = unmanagedMemory2;
				}
				int length = memory.controlCount;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendListWithCapacity(ref controls, ref length, matches);
				for (int k = 0; k < num; k++)
				{
					global::UnityEngine.InputSystem.InputActionState.BindingState* num22 = unmanagedMemory.bindingStates + (num5 + k);
					int controlCount = num22->controlCount;
					int controlStartIndex3 = num22->controlStartIndex;
					for (int l = 0; l < controlCount; l++)
					{
						unmanagedMemory.controlIndexToBindingIndex[controlStartIndex3 + l] = num5 + k;
					}
				}
				for (int m = memory.interactionCount; m < unmanagedMemory.interactionCount; m++)
				{
					global::UnityEngine.InputSystem.InputActionState.InteractionState* num23 = unmanagedMemory.interactionStates + m;
					num23->phase = global::UnityEngine.InputSystem.InputActionPhase.Waiting;
					num23->triggerControlIndex = -1;
				}
				int num24 = memory.bindingCount;
				for (int n = 0; n < num2; n++)
				{
					global::UnityEngine.InputSystem.InputAction inputAction3 = actions[n];
					int num25 = (inputAction3.m_ActionIndexInState = num4 + n);
					unmanagedMemory.actionBindingIndicesAndCounts[num25 * 2] = (ushort)num24;
					int num26 = -1;
					int num27 = 0;
					int num28 = 0;
					for (int num29 = 0; num29 < num; num29++)
					{
						int num30 = num5 + num29;
						global::UnityEngine.InputSystem.InputActionState.BindingState* ptr2 = unmanagedMemory.bindingStates + num30;
						if (ptr2->actionIndex != num25 || ptr2->isPartOfComposite)
						{
							continue;
						}
						unmanagedMemory.actionBindingIndices[num24] = (ushort)num30;
						num24++;
						num27++;
						if (num26 == -1)
						{
							num26 = num30;
						}
						if (ptr2->isComposite)
						{
							if (ptr2->controlCount > 0)
							{
								num28++;
							}
						}
						else
						{
							num28 += ptr2->controlCount;
						}
					}
					if (num26 == -1)
					{
						num26 = 0;
					}
					unmanagedMemory.actionBindingIndicesAndCounts[num25 * 2 + 1] = (ushort)num27;
					bool flag4 = inputAction3.type == global::UnityEngine.InputSystem.InputActionType.PassThrough;
					bool isButton = inputAction3.type == global::UnityEngine.InputSystem.InputActionType.Button;
					bool mayNeedConflictResolution = !flag4 && num28 > 1;
					unmanagedMemory.actionStates[num25] = new global::UnityEngine.InputSystem.InputActionState.TriggerState
					{
						phase = global::UnityEngine.InputSystem.InputActionPhase.Disabled,
						mapIndex = num3,
						controlIndex = -1,
						interactionIndex = -1,
						isPassThrough = flag4,
						isButton = isButton,
						mayNeedConflictResolution = mayNeedConflictResolution,
						bindingIndex = num26
					};
				}
				unmanagedMemory.mapIndices[num3] = new global::UnityEngine.InputSystem.InputActionState.ActionMapIndices
				{
					actionStartIndex = num4,
					actionCount = num2,
					controlStartIndex = controlStartIndex,
					controlCount = count,
					bindingStartIndex = num5,
					bindingCount = num,
					interactionStartIndex = num6,
					interactionCount = totalInteractionCount - num6,
					processorStartIndex = num7,
					processorCount = totalProcessorCount - num7,
					compositeStartIndex = num8,
					compositeCount = totalCompositeCount - num8
				};
				actionMap.m_MapIndexInState = num3;
				int count2 = memory.mapCount;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref maps, ref count2, actionMap, 4);
				memory.Dispose();
				memory = unmanagedMemory;
			}
			catch (global::System.Exception)
			{
				unmanagedMemory.Dispose();
				throw;
			}
			finally
			{
				matches.Dispose();
			}
		}

		private int InstantiateWithParameters<TType>(global::UnityEngine.InputSystem.Utilities.TypeTable registrations, string namesAndParameters, ref TType[] array, ref int count, global::UnityEngine.InputSystem.InputActionMap actionMap, ref global::UnityEngine.InputSystem.InputBinding binding)
		{
			if (!global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(namesAndParameters, ref m_Parameters))
			{
				return -1;
			}
			int result = count;
			for (int i = 0; i < m_Parameters.Count; i++)
			{
				string name = m_Parameters[i].name;
				global::System.Type type = registrations.LookupTypeRegistration(name);
				if (type == null)
				{
					global::UnityEngine.Debug.LogError("No " + typeof(TType).Name + " with name '" + name + "' (mentioned in '" + namesAndParameters + "') has been registered");
				}
				else if (!m_IsControlOnlyResolve)
				{
					if (!(global::System.Activator.CreateInstance(type) is TType val))
					{
						global::UnityEngine.Debug.LogError("Type '" + type.Name + "' registered as '" + name + "' (mentioned in '" + namesAndParameters + "') is not an " + typeof(TType).Name);
					}
					else
					{
						ApplyParameters(m_Parameters[i].parameters, val, actionMap, ref binding, name, namesAndParameters);
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref array, ref count, val);
					}
				}
				else
				{
					count++;
				}
			}
			return result;
		}

		private static global::UnityEngine.InputSystem.InputBindingComposite InstantiateBindingComposite(ref global::UnityEngine.InputSystem.InputBinding binding, global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			global::UnityEngine.InputSystem.Utilities.NameAndParameters nameAndParameters = global::UnityEngine.InputSystem.Utilities.NameAndParameters.Parse(binding.effectivePath);
			global::System.Type type = global::UnityEngine.InputSystem.InputBindingComposite.s_Composites.LookupTypeRegistration(nameAndParameters.name);
			if (type == null)
			{
				throw new global::System.InvalidOperationException("No binding composite with name '" + nameAndParameters.name + "' has been registered");
			}
			if (!(global::System.Activator.CreateInstance(type) is global::UnityEngine.InputSystem.InputBindingComposite inputBindingComposite))
			{
				throw new global::System.InvalidOperationException("Registered type '" + type.Name + "' used for '" + nameAndParameters.name + "' is not an InputBindingComposite");
			}
			ApplyParameters(nameAndParameters.parameters, inputBindingComposite, actionMap, ref binding, nameAndParameters.name, binding.effectivePath);
			return inputBindingComposite;
		}

		private static void ApplyParameters(global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue> parameters, object instance, global::UnityEngine.InputSystem.InputActionMap actionMap, ref global::UnityEngine.InputSystem.InputBinding binding, string objectRegistrationName, string namesAndParameters)
		{
			foreach (global::UnityEngine.InputSystem.Utilities.NamedValue item in parameters)
			{
				global::System.Reflection.FieldInfo field = instance.GetType().GetField(item.name, global::System.Reflection.BindingFlags.IgnoreCase | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
				if (field == null)
				{
					global::UnityEngine.Debug.LogError("Type '" + instance.GetType().Name + "' registered as '" + objectRegistrationName + "' (mentioned in '" + namesAndParameters + "') has no public field called '" + item.name + "'");
				}
				else
				{
					global::System.TypeCode typeCode = global::System.Type.GetTypeCode(field.FieldType);
					global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? parameterOverride = global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride.Find(actionMap, ref binding, item.name, objectRegistrationName);
					field.SetValue(instance, (parameterOverride.HasValue ? parameterOverride.Value.value : item.value).ConvertTo(typeCode).ToObject());
				}
			}
		}

		private static int AssignCompositePartIndex(object composite, string name, ref int currentCompositePartCount)
		{
			global::System.Type type = composite.GetType();
			global::System.Reflection.FieldInfo field = type.GetField(name, global::System.Reflection.BindingFlags.IgnoreCase | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			if (field == null)
			{
				throw new global::System.InvalidOperationException($"Cannot find public field '{name}' used as parameter of binding composite '{composite}' of type '{type}'");
			}
			if (field.FieldType != typeof(int))
			{
				throw new global::System.InvalidOperationException($"Field '{name}' used as a parameter of binding composite '{composite}' must be of type 'int' but is of type '{type.Name}' instead");
			}
			int num = (int)field.GetValue(composite);
			if (num == 0)
			{
				num = ++currentCompositePartCount;
				field.SetValue(composite, num);
			}
			return num;
		}
	}
}

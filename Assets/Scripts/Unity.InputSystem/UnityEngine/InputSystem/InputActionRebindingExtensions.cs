namespace UnityEngine.InputSystem
{
	public static class InputActionRebindingExtensions
	{
		internal struct Parameter
		{
			public object instance;

			public global::System.Reflection.FieldInfo field;

			public int bindingIndex;
		}

		private struct ParameterEnumerable : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter>, global::System.Collections.IEnumerable
		{
			private global::UnityEngine.InputSystem.InputActionState m_State;

			private global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride m_Parameter;

			private int m_MapIndex;

			public ParameterEnumerable(global::UnityEngine.InputSystem.InputActionState state, global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				m_State = state;
				m_Parameter = parameter;
				m_MapIndex = mapIndex;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerator GetEnumerator()
			{
				return new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerator(m_State, m_Parameter, m_MapIndex);
			}

			global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter> global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter>.GetEnumerator()
			{
				return GetEnumerator();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private struct ParameterEnumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::UnityEngine.InputSystem.InputActionState m_State;

			private int m_MapIndex;

			private int m_BindingCurrentIndex;

			private int m_BindingEndIndex;

			private int m_InteractionCurrentIndex;

			private int m_InteractionEndIndex;

			private int m_ProcessorCurrentIndex;

			private int m_ProcessorEndIndex;

			private global::UnityEngine.InputSystem.InputBinding m_BindingMask;

			private global::System.Type m_ObjectType;

			private string m_ParameterName;

			private bool m_MayBeInteraction;

			private bool m_MayBeProcessor;

			private bool m_MayBeComposite;

			private bool m_CurrentBindingIsComposite;

			private object m_CurrentObject;

			private global::System.Reflection.FieldInfo m_CurrentParameter;

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter Current => new global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter
			{
				instance = m_CurrentObject,
				field = m_CurrentParameter,
				bindingIndex = m_BindingCurrentIndex
			};

			object global::System.Collections.IEnumerator.Current => Current;

			public ParameterEnumerator(global::UnityEngine.InputSystem.InputActionState state, global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this = default(global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerator);
				m_State = state;
				m_ParameterName = parameter.parameter;
				m_MapIndex = mapIndex;
				m_ObjectType = parameter.objectType;
				m_MayBeComposite = m_ObjectType == null || typeof(global::UnityEngine.InputSystem.InputBindingComposite).IsAssignableFrom(m_ObjectType);
				m_MayBeProcessor = m_ObjectType == null || typeof(global::UnityEngine.InputSystem.InputProcessor).IsAssignableFrom(m_ObjectType);
				m_MayBeInteraction = m_ObjectType == null || typeof(global::UnityEngine.InputSystem.IInputInteraction).IsAssignableFrom(m_ObjectType);
				m_BindingMask = parameter.bindingMask;
				Reset();
			}

			private bool MoveToNextBinding()
			{
				ref global::UnityEngine.InputSystem.InputBinding binding;
				ref global::UnityEngine.InputSystem.InputActionState.BindingState bindingState;
				do
				{
					m_BindingCurrentIndex++;
					if (m_BindingCurrentIndex >= m_BindingEndIndex)
					{
						return false;
					}
					binding = ref m_State.GetBinding(m_BindingCurrentIndex);
					bindingState = ref m_State.GetBindingState(m_BindingCurrentIndex);
				}
				while ((bindingState.processorCount == 0 && bindingState.interactionCount == 0 && !binding.isComposite) || (m_MayBeComposite && !m_MayBeProcessor && !m_MayBeInteraction && !binding.isComposite) || (m_MayBeProcessor && !m_MayBeComposite && !m_MayBeInteraction && bindingState.processorCount == 0) || (m_MayBeInteraction && !m_MayBeComposite && !m_MayBeProcessor && bindingState.interactionCount == 0) || !m_BindingMask.Matches(ref binding));
				if (m_MayBeComposite)
				{
					m_CurrentBindingIsComposite = binding.isComposite;
				}
				m_ProcessorCurrentIndex = bindingState.processorStartIndex - 1;
				m_ProcessorEndIndex = bindingState.processorStartIndex + bindingState.processorCount;
				m_InteractionCurrentIndex = bindingState.interactionStartIndex - 1;
				m_InteractionEndIndex = bindingState.interactionStartIndex + bindingState.interactionCount;
				return true;
			}

			private bool MoveToNextInteraction()
			{
				while (m_InteractionCurrentIndex < m_InteractionEndIndex)
				{
					m_InteractionCurrentIndex++;
					if (m_InteractionCurrentIndex == m_InteractionEndIndex)
					{
						break;
					}
					global::UnityEngine.InputSystem.IInputInteraction instance = m_State.interactions[m_InteractionCurrentIndex];
					if (FindParameter(instance))
					{
						return true;
					}
				}
				return false;
			}

			private bool MoveToNextProcessor()
			{
				while (m_ProcessorCurrentIndex < m_ProcessorEndIndex)
				{
					m_ProcessorCurrentIndex++;
					if (m_ProcessorCurrentIndex == m_ProcessorEndIndex)
					{
						break;
					}
					global::UnityEngine.InputSystem.InputProcessor instance = m_State.processors[m_ProcessorCurrentIndex];
					if (FindParameter(instance))
					{
						return true;
					}
				}
				return false;
			}

			private bool FindParameter(object instance)
			{
				if (m_ObjectType != null && !m_ObjectType.IsInstanceOfType(instance))
				{
					return false;
				}
				global::System.Reflection.FieldInfo field = instance.GetType().GetField(m_ParameterName, global::System.Reflection.BindingFlags.IgnoreCase | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
				if (field == null)
				{
					return false;
				}
				m_CurrentParameter = field;
				m_CurrentObject = instance;
				return true;
			}

			public bool MoveNext()
			{
				while (true)
				{
					if (m_MayBeInteraction && MoveToNextInteraction())
					{
						return true;
					}
					if (m_MayBeProcessor && MoveToNextProcessor())
					{
						return true;
					}
					if (!MoveToNextBinding())
					{
						return false;
					}
					if (m_MayBeComposite && m_CurrentBindingIsComposite)
					{
						int compositeOrCompositeBindingIndex = m_State.GetBindingState(m_BindingCurrentIndex).compositeOrCompositeBindingIndex;
						global::UnityEngine.InputSystem.InputBindingComposite instance = m_State.composites[compositeOrCompositeBindingIndex];
						if (FindParameter(instance))
						{
							break;
						}
					}
				}
				return true;
			}

			public unsafe void Reset()
			{
				m_CurrentObject = null;
				m_CurrentParameter = null;
				m_InteractionCurrentIndex = 0;
				m_InteractionEndIndex = 0;
				m_ProcessorCurrentIndex = 0;
				m_ProcessorEndIndex = 0;
				m_CurrentBindingIsComposite = false;
				if (m_MapIndex < 0)
				{
					m_BindingCurrentIndex = -1;
					m_BindingEndIndex = m_State.totalBindingCount;
				}
				else
				{
					m_BindingCurrentIndex = m_State.mapIndices[m_MapIndex].bindingStartIndex - 1;
					m_BindingEndIndex = m_State.mapIndices[m_MapIndex].bindingStartIndex + m_State.mapIndices[m_MapIndex].bindingCount;
				}
			}

			public void Dispose()
			{
			}
		}

		internal struct ParameterOverride
		{
			public string objectRegistrationName;

			public string parameter;

			public global::UnityEngine.InputSystem.InputBinding bindingMask;

			public global::UnityEngine.InputSystem.Utilities.PrimitiveValue value;

			public global::System.Type objectType => global::UnityEngine.InputSystem.InputProcessor.s_Processors.LookupTypeRegistration(objectRegistrationName) ?? global::UnityEngine.InputSystem.InputInteraction.s_Interactions.LookupTypeRegistration(objectRegistrationName) ?? global::UnityEngine.InputSystem.InputBindingComposite.s_Composites.LookupTypeRegistration(objectRegistrationName);

			public ParameterOverride(string parameterName, global::UnityEngine.InputSystem.InputBinding bindingMask, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue))
			{
				int num = parameterName.IndexOf(':');
				if (num < 0)
				{
					objectRegistrationName = null;
					parameter = parameterName;
				}
				else
				{
					objectRegistrationName = parameterName.Substring(0, num);
					parameter = parameterName.Substring(num + 1);
				}
				this.bindingMask = bindingMask;
				this.value = value;
			}

			public ParameterOverride(string objectRegistrationName, string parameterName, global::UnityEngine.InputSystem.InputBinding bindingMask, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue))
			{
				this.objectRegistrationName = objectRegistrationName;
				parameter = parameterName;
				this.bindingMask = bindingMask;
				this.value = value;
			}

			public static global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? Find(global::UnityEngine.InputSystem.InputActionMap actionMap, ref global::UnityEngine.InputSystem.InputBinding binding, string parameterName, string objectRegistrationName)
			{
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? first = Find(actionMap.m_ParameterOverrides, actionMap.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName);
				global::UnityEngine.InputSystem.InputActionAsset asset = actionMap.asset;
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? second = ((asset != null) ? Find(asset.m_ParameterOverrides, asset.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName) : ((global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride?)null));
				return PickMoreSpecificOne(first, second);
			}

			private static global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? Find(global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[] overrides, int overrideCount, ref global::UnityEngine.InputSystem.InputBinding binding, string parameterName, string objectRegistrationName)
			{
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? parameterOverride = null;
				for (int i = 0; i < overrideCount; i++)
				{
					ref global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride reference = ref overrides[i];
					if (string.Equals(parameterName, reference.parameter, global::System.StringComparison.OrdinalIgnoreCase) && reference.bindingMask.Matches(binding) && (reference.objectRegistrationName == null || string.Equals(reference.objectRegistrationName, objectRegistrationName, global::System.StringComparison.OrdinalIgnoreCase)))
					{
						parameterOverride = (parameterOverride.HasValue ? PickMoreSpecificOne(parameterOverride, reference) : new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride?(reference));
					}
				}
				return parameterOverride;
			}

			private static global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? PickMoreSpecificOne(global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? first, global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? second)
			{
				if (!first.HasValue)
				{
					return second;
				}
				if (!second.HasValue)
				{
					return first;
				}
				if (first.Value.objectRegistrationName != null && second.Value.objectRegistrationName == null)
				{
					return first;
				}
				if (second.Value.objectRegistrationName != null && first.Value.objectRegistrationName == null)
				{
					return second;
				}
				if (first.Value.bindingMask.effectivePath != null && second.Value.bindingMask.effectivePath == null)
				{
					return first;
				}
				if (second.Value.bindingMask.effectivePath != null && first.Value.bindingMask.effectivePath == null)
				{
					return second;
				}
				if (first.Value.bindingMask.action != null && second.Value.bindingMask.action == null)
				{
					return first;
				}
				if (second.Value.bindingMask.action != null && first.Value.bindingMask.action == null)
				{
					return second;
				}
				return first;
			}
		}

		public sealed class RebindingOperation : global::System.IDisposable
		{
			[global::System.Flags]
			private enum Flags
			{
				Started = 1,
				Completed = 2,
				Canceled = 4,
				OnEventHooked = 8,
				OnAfterUpdateHooked = 0x10,
				DontIgnoreNoisyControls = 0x40,
				DontGeneralizePathOfSelectedControl = 0x80,
				AddNewBinding = 0x100,
				SuppressMatchingEvents = 0x200
			}

			public const float kDefaultMagnitudeThreshold = 0.2f;

			private global::UnityEngine.InputSystem.InputAction m_ActionToRebind;

			private global::UnityEngine.InputSystem.InputBinding? m_BindingMask;

			private global::System.Type m_ControlType;

			private global::UnityEngine.InputSystem.Utilities.InternedString m_ExpectedLayout;

			private int m_IncludePathCount;

			private string[] m_IncludePaths;

			private int m_ExcludePathCount;

			private string[] m_ExcludePaths;

			private int m_TargetBindingIndex = -1;

			private string m_BindingGroupForNewBinding;

			private string m_CancelBinding;

			private float m_MagnitudeThreshold = 0.2f;

			private float[] m_Scores;

			private float[] m_Magnitudes;

			private double m_LastMatchTime;

			private double m_StartTime;

			private float m_Timeout;

			private float m_WaitSecondsAfterMatch;

			private global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy m_SavedInputEventHandledPolicy;

			private global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy m_TargetInputEventHandledPolicy;

			private global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> m_Candidates;

			private global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> m_OnComplete;

			private global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> m_OnCancel;

			private global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> m_OnPotentialMatch;

			private global::System.Func<global::UnityEngine.InputSystem.InputControl, string> m_OnGeneratePath;

			private global::System.Func<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, float> m_OnComputeScore;

			private global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation, string> m_OnApplyBinding;

			private global::System.Action<global::UnityEngine.InputSystem.LowLevel.InputEventPtr, global::UnityEngine.InputSystem.InputDevice> m_OnEventDelegate;

			private global::System.Action m_OnAfterUpdateDelegate;

			private global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache m_LayoutCache;

			private global::System.Text.StringBuilder m_PathBuilder;

			private global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags m_Flags;

			private global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.InputControl, float> m_StartingActuations = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.InputControl, float>();

			public global::UnityEngine.InputSystem.InputAction action => m_ActionToRebind;

			public global::UnityEngine.InputSystem.InputBinding? bindingMask => m_BindingMask;

			public global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> candidates => m_Candidates;

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<float> scores => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<float>(m_Scores, 0, m_Candidates.Count);

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<float> magnitudes => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<float>(m_Magnitudes, 0, m_Candidates.Count);

			public global::UnityEngine.InputSystem.InputControl selectedControl
			{
				get
				{
					if (m_Candidates.Count == 0)
					{
						return null;
					}
					return m_Candidates[0];
				}
			}

			public bool started => (m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Started) != 0;

			public bool completed => (m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Completed) != 0;

			public bool canceled => (m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Canceled) != 0;

			public double startTime => m_StartTime;

			public float timeout => m_Timeout;

			public string expectedControlType => m_ExpectedLayout;

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithAction(global::UnityEngine.InputSystem.InputAction action)
			{
				ThrowIfRebindInProgress();
				if (action == null)
				{
					throw new global::System.ArgumentNullException("action");
				}
				if (action.enabled)
				{
					throw new global::System.InvalidOperationException($"Cannot rebind action '{action}' while it is enabled");
				}
				m_ActionToRebind = action;
				if (!string.IsNullOrEmpty(action.expectedControlType))
				{
					WithExpectedControlType(action.expectedControlType);
				}
				else if (action.type == global::UnityEngine.InputSystem.InputActionType.Button)
				{
					WithExpectedControlType("Button");
				}
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithMatchingEventsBeingSuppressed(bool value = true)
			{
				ThrowIfRebindInProgress();
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(string binding)
			{
				ThrowIfRebindInProgress();
				m_CancelBinding = binding;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(global::UnityEngine.InputSystem.InputControl control)
			{
				ThrowIfRebindInProgress();
				if (control == null)
				{
					throw new global::System.ArgumentNullException("control");
				}
				return WithCancelingThrough(control.path);
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(string layoutName)
			{
				ThrowIfRebindInProgress();
				m_ExpectedLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(layoutName);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(global::System.Type type)
			{
				ThrowIfRebindInProgress();
				if (type != null && !typeof(global::UnityEngine.InputSystem.InputControl).IsAssignableFrom(type))
				{
					throw new global::System.ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
				}
				m_ControlType = type;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithExpectedControlType<TControl>() where TControl : global::UnityEngine.InputSystem.InputControl
			{
				ThrowIfRebindInProgress();
				return WithExpectedControlType(typeof(TControl));
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithTargetBinding(int bindingIndex)
			{
				if (bindingIndex < 0)
				{
					throw new global::System.ArgumentOutOfRangeException("bindingIndex");
				}
				m_TargetBindingIndex = bindingIndex;
				if (m_ActionToRebind != null && bindingIndex < m_ActionToRebind.bindings.Count)
				{
					global::UnityEngine.InputSystem.InputBinding inputBinding = m_ActionToRebind.bindings[bindingIndex];
					if (inputBinding.isPartOfComposite)
					{
						string nameOfComposite = m_ActionToRebind.ChangeBinding(bindingIndex).PreviousCompositeBinding().binding.GetNameOfComposite();
						string name = inputBinding.name;
						string expectedControlLayoutName = global::UnityEngine.InputSystem.InputBindingComposite.GetExpectedControlLayoutName(nameOfComposite, name);
						if (!string.IsNullOrEmpty(expectedControlLayoutName))
						{
							WithExpectedControlType(expectedControlLayoutName);
						}
					}
					global::UnityEngine.InputSystem.InputActionAsset inputActionAsset = action.actionMap?.asset;
					if (inputActionAsset != null && !string.IsNullOrEmpty(inputBinding.groups))
					{
						string[] array = inputBinding.groups.Split(';');
						foreach (string group in array)
						{
							int num = inputActionAsset.controlSchemes.IndexOf((global::UnityEngine.InputSystem.InputControlScheme x) => group.Equals(x.bindingGroup, global::System.StringComparison.InvariantCultureIgnoreCase));
							if (num == -1)
							{
								continue;
							}
							foreach (global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement deviceRequirement in inputActionAsset.controlSchemes[num].deviceRequirements)
							{
								WithControlsHavingToMatchPath(deviceRequirement.controlPath);
							}
						}
					}
				}
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithBindingMask(global::UnityEngine.InputSystem.InputBinding? bindingMask)
			{
				m_BindingMask = bindingMask;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithBindingGroup(string group)
			{
				return WithBindingMask(new global::UnityEngine.InputSystem.InputBinding
				{
					groups = group
				});
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithoutGeneralizingPathOfSelectedControl()
			{
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithRebindAddingNewBinding(string group = null)
			{
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding;
				m_BindingGroupForNewBinding = group;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithMagnitudeHavingToBeGreaterThan(float magnitude)
			{
				ThrowIfRebindInProgress();
				if (magnitude < 0f)
				{
					throw new global::System.ArgumentException($"Magnitude has to be positive but was {magnitude}", "magnitude");
				}
				m_MagnitudeThreshold = magnitude;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithoutIgnoringNoisyControls()
			{
				ThrowIfRebindInProgress();
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithControlsHavingToMatchPath(string path)
			{
				ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new global::System.ArgumentNullException("path");
				}
				for (int i = 0; i < m_IncludePathCount; i++)
				{
					if (string.Compare(m_IncludePaths[i], path, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_IncludePaths, ref m_IncludePathCount, path);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithControlsExcluding(string path)
			{
				ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new global::System.ArgumentNullException("path");
				}
				for (int i = 0; i < m_ExcludePathCount; i++)
				{
					if (string.Compare(m_ExcludePaths[i], path, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_ExcludePaths, ref m_ExcludePathCount, path);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithTimeout(float timeInSeconds)
			{
				ThrowIfRebindInProgress();
				m_Timeout = timeInSeconds;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnComplete(global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
			{
				m_OnComplete = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnCancel(global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
			{
				m_OnCancel = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnPotentialMatch(global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation> callback)
			{
				m_OnPotentialMatch = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnGeneratePath(global::System.Func<global::UnityEngine.InputSystem.InputControl, string> callback)
			{
				m_OnGeneratePath = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnComputeScore(global::System.Func<global::UnityEngine.InputSystem.InputControl, global::UnityEngine.InputSystem.LowLevel.InputEventPtr, float> callback)
			{
				m_OnComputeScore = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnApplyBinding(global::System.Action<global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation, string> callback)
			{
				m_OnApplyBinding = callback;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation OnMatchWaitForAnother(float seconds)
			{
				m_WaitSecondsAfterMatch = seconds;
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation WithActionEventNotificationsBeingSuppressed(bool value = true)
			{
				ThrowIfRebindInProgress();
				m_TargetInputEventHandledPolicy = (value ? global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy.SuppressActionEventNotifications : global::UnityEngine.InputSystem.LowLevel.InputEventHandledPolicy.SuppressStateUpdates);
				return this;
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation Start()
			{
				if (started)
				{
					return this;
				}
				if (m_ActionToRebind != null && m_ActionToRebind.bindings.Count == 0 && (m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) == 0)
				{
					throw new global::System.InvalidOperationException($"Action '{action}' must have at least one existing binding or must be used with WithRebindingAddNewBinding()");
				}
				if (m_ActionToRebind == null && m_OnApplyBinding == null)
				{
					throw new global::System.InvalidOperationException("Must either have an action (call WithAction()) to apply binding to or have a custom callback to apply the binding (call OnApplyBinding())");
				}
				m_StartTime = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
				m_SavedInputEventHandledPolicy = global::UnityEngine.InputSystem.InputSystem.s_Manager.inputEventHandledPolicy;
				global::UnityEngine.InputSystem.InputSystem.s_Manager.inputEventHandledPolicy = m_TargetInputEventHandledPolicy;
				if (m_WaitSecondsAfterMatch > 0f || m_Timeout > 0f)
				{
					HookOnAfterUpdate();
					m_LastMatchTime = -1.0;
				}
				HookOnEvent();
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				return this;
			}

			public void Cancel()
			{
				if (started)
				{
					OnCancel();
				}
			}

			public void Complete()
			{
				if (started)
				{
					OnComplete();
				}
			}

			public void AddCandidate(global::UnityEngine.InputSystem.InputControl control, float score, float magnitude = -1f)
			{
				if (control == null)
				{
					throw new global::System.ArgumentNullException("control");
				}
				int num = m_Candidates.IndexOf(control);
				if (num != -1)
				{
					m_Scores[num] = score;
				}
				else
				{
					int count = m_Candidates.Count;
					int count2 = m_Candidates.Count;
					m_Candidates.Add(control);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Scores, ref count, score);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Magnitudes, ref count2, magnitude);
				}
				SortCandidatesByScore();
			}

			public void RemoveCandidate(global::UnityEngine.InputSystem.InputControl control)
			{
				if (control == null)
				{
					throw new global::System.ArgumentNullException("control");
				}
				int num = m_Candidates.IndexOf(control);
				if (num != -1)
				{
					int count = m_Candidates.Count;
					m_Candidates.RemoveAt(num);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(m_Scores, ref count, num);
				}
			}

			public void Dispose()
			{
				UnhookOnEvent();
				UnhookOnAfterUpdate();
				m_Candidates.Dispose();
				m_LayoutCache.Clear();
			}

			~RebindingOperation()
			{
				Dispose();
			}

			public global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation Reset()
			{
				Cancel();
				m_ActionToRebind = null;
				m_BindingMask = null;
				m_ControlType = null;
				m_ExpectedLayout = default(global::UnityEngine.InputSystem.Utilities.InternedString);
				m_IncludePathCount = 0;
				m_ExcludePathCount = 0;
				m_TargetBindingIndex = -1;
				m_BindingGroupForNewBinding = null;
				m_CancelBinding = null;
				m_MagnitudeThreshold = 0.2f;
				m_Timeout = 0f;
				m_WaitSecondsAfterMatch = 0f;
				m_Flags = (global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags)0;
				m_StartingActuations?.Clear();
				return this;
			}

			private void HookOnEvent()
			{
				if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) == 0)
				{
					if (m_OnEventDelegate == null)
					{
						m_OnEventDelegate = OnEvent;
					}
					global::UnityEngine.InputSystem.InputSystem.onEvent += m_OnEventDelegate;
					m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
				}
			}

			private void UnhookOnEvent()
			{
				if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) != 0)
				{
					global::UnityEngine.InputSystem.InputSystem.onEvent -= m_OnEventDelegate;
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
				}
			}

			private unsafe void OnEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
			{
				global::UnityEngine.InputSystem.Utilities.FourCC type = eventPtr.type;
				if (type != 1398030676 && type != 1145852993)
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				global::UnityEngine.InputSystem.InputControlExtensions.Enumerate enumerate = global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeSyntheticControls | global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeNonLeafControls;
				if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls) != 0)
				{
					enumerate |= global::UnityEngine.InputSystem.InputControlExtensions.Enumerate.IncludeNoisyControls;
				}
				foreach (global::UnityEngine.InputSystem.InputControl item in eventPtr.EnumerateControls(enumerate, device))
				{
					void* statePtrFromStateEventUnchecked = item.GetStatePtrFromStateEventUnchecked(eventPtr, type);
					if (!string.IsNullOrEmpty(m_CancelBinding) && global::UnityEngine.InputSystem.InputControlPath.Matches(m_CancelBinding, item) && item.HasValueChangeInState(statePtrFromStateEventUnchecked))
					{
						eventPtr.handled = true;
						OnCancel();
						break;
					}
					if ((m_ExcludePathCount > 0 && HavePathMatch(item, m_ExcludePaths, m_ExcludePathCount)) || (m_IncludePathCount > 0 && !HavePathMatch(item, m_IncludePaths, m_IncludePathCount)) || (m_ControlType != null && !m_ControlType.IsInstanceOfType(item)) || (!m_ExpectedLayout.IsEmpty() && m_ExpectedLayout != item.m_Layout && !global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.IsBasedOn(m_ExpectedLayout, item.m_Layout)))
					{
						continue;
					}
					if (item.CheckStateIsAtDefault(statePtrFromStateEventUnchecked, null))
					{
						if (!m_StartingActuations.ContainsKey(item))
						{
							m_StartingActuations.Add(item, 0f);
						}
						m_StartingActuations[item] = 0f;
						continue;
					}
					flag2 = true;
					float num = item.EvaluateMagnitude(statePtrFromStateEventUnchecked);
					if (num >= 0f)
					{
						if (!m_StartingActuations.TryGetValue(item, out var value))
						{
							value = item.magnitude;
							m_StartingActuations.Add(item, value);
						}
						if (global::UnityEngine.Mathf.Abs(value - num) < m_MagnitudeThreshold)
						{
							continue;
						}
					}
					float num2;
					if (m_OnComputeScore != null)
					{
						num2 = m_OnComputeScore(item, eventPtr);
					}
					else
					{
						num2 = num;
						if (!item.synthetic)
						{
							num2 += 1f;
						}
					}
					int num3 = m_Candidates.IndexOf(item);
					if (num3 != -1)
					{
						if (m_Scores[num3] < num2)
						{
							flag = true;
							m_Scores[num3] = num2;
							if (m_WaitSecondsAfterMatch > 0f)
							{
								m_LastMatchTime = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
							}
						}
						continue;
					}
					int count = m_Candidates.Count;
					int count2 = m_Candidates.Count;
					m_Candidates.Add(item);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Scores, ref count, num2);
					global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Magnitudes, ref count2, num);
					flag = true;
					if (m_WaitSecondsAfterMatch > 0f)
					{
						m_LastMatchTime = global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
					}
				}
				if (flag2 && (m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents) != 0)
				{
					eventPtr.handled = true;
				}
				if (flag && !canceled)
				{
					if (m_OnPotentialMatch != null)
					{
						SortCandidatesByScore();
						m_OnPotentialMatch(this);
					}
					else if (m_WaitSecondsAfterMatch <= 0f)
					{
						OnComplete();
					}
					else
					{
						SortCandidatesByScore();
					}
				}
			}

			private void SortCandidatesByScore()
			{
				int count = m_Candidates.Count;
				if (count <= 1)
				{
					return;
				}
				for (int i = 1; i < count; i++)
				{
					int num = i;
					while (num > 0 && m_Scores[num - 1] < m_Scores[num])
					{
						int index = num - 1;
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.SwapElements(m_Scores, num, index);
						m_Candidates.SwapElements(num, index);
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.SwapElements(m_Magnitudes, num, index);
						num--;
					}
				}
			}

			private static bool HavePathMatch(global::UnityEngine.InputSystem.InputControl control, string[] paths, int pathCount)
			{
				for (int i = 0; i < pathCount; i++)
				{
					if (global::UnityEngine.InputSystem.InputControlPath.MatchesPrefix(paths[i], control))
					{
						return true;
					}
				}
				return false;
			}

			private void HookOnAfterUpdate()
			{
				if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) == 0)
				{
					if (m_OnAfterUpdateDelegate == null)
					{
						m_OnAfterUpdateDelegate = OnAfterUpdate;
					}
					global::UnityEngine.InputSystem.InputSystem.onAfterUpdate += m_OnAfterUpdateDelegate;
					m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
				}
			}

			private void UnhookOnAfterUpdate()
			{
				if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) != 0)
				{
					global::UnityEngine.InputSystem.InputSystem.onAfterUpdate -= m_OnAfterUpdateDelegate;
					m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
				}
			}

			private void OnAfterUpdate()
			{
				if (m_LastMatchTime < 0.0 && m_Timeout > 0f && global::UnityEngine.InputSystem.LowLevel.InputState.currentTime - m_StartTime > (double)m_Timeout)
				{
					Cancel();
				}
				else if (!(m_WaitSecondsAfterMatch <= 0f) && !(m_LastMatchTime < 0.0) && global::UnityEngine.InputSystem.LowLevel.InputState.currentTime >= m_LastMatchTime + (double)m_WaitSecondsAfterMatch)
				{
					Complete();
				}
			}

			private void OnComplete()
			{
				SortCandidatesByScore();
				if (m_Candidates.Count > 0)
				{
					global::UnityEngine.InputSystem.InputControl inputControl = m_Candidates[0];
					string text = inputControl.path;
					if (m_OnGeneratePath != null)
					{
						string text2 = m_OnGeneratePath(inputControl);
						if (!string.IsNullOrEmpty(text2))
						{
							text = text2;
						}
						else if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == 0)
						{
							text = GeneratePathForControl(inputControl);
						}
					}
					else if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == 0)
					{
						text = GeneratePathForControl(inputControl);
					}
					if (m_OnApplyBinding != null)
					{
						m_OnApplyBinding(this, text);
					}
					else if ((m_Flags & global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) != 0)
					{
						m_ActionToRebind.AddBinding(text, null, null, m_BindingGroupForNewBinding);
					}
					else if (m_TargetBindingIndex >= 0)
					{
						if (m_TargetBindingIndex >= m_ActionToRebind.bindings.Count)
						{
							throw new global::System.InvalidOperationException($"Target binding index {m_TargetBindingIndex} out of range for action '{m_ActionToRebind}' with {m_ActionToRebind.bindings.Count} bindings");
						}
						m_ActionToRebind.ApplyBindingOverride(m_TargetBindingIndex, text);
					}
					else if (m_BindingMask.HasValue)
					{
						global::UnityEngine.InputSystem.InputBinding value = m_BindingMask.Value;
						value.overridePath = text;
						m_ActionToRebind.ApplyBindingOverride(value);
					}
					else
					{
						m_ActionToRebind.ApplyBindingOverride(text);
					}
				}
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				m_OnComplete?.Invoke(this);
				ResetAfterMatchCompleted();
			}

			private void OnCancel()
			{
				m_Flags |= global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				m_OnCancel?.Invoke(this);
				ResetAfterMatchCompleted();
			}

			private void ResetAfterMatchCompleted()
			{
				m_Flags &= ~global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				m_Candidates.Clear();
				m_Candidates.Capacity = 0;
				m_StartTime = -1.0;
				m_StartingActuations.Clear();
				UnhookOnEvent();
				UnhookOnAfterUpdate();
				global::UnityEngine.InputSystem.InputSystem.s_Manager.inputEventHandledPolicy = m_SavedInputEventHandledPolicy;
			}

			private void ThrowIfRebindInProgress()
			{
				if (started)
				{
					throw new global::System.InvalidOperationException("Cannot reconfigure rebinding while operation is in progress");
				}
			}

			private string GeneratePathForControl(global::UnityEngine.InputSystem.InputControl control)
			{
				_ = control.device;
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.FindLayoutThatIntroducesControl(control, m_LayoutCache);
				if (m_PathBuilder == null)
				{
					m_PathBuilder = new global::System.Text.StringBuilder();
				}
				else
				{
					m_PathBuilder.Length = 0;
				}
				control.BuildPath(internedString, m_PathBuilder);
				return m_PathBuilder.ToString();
			}
		}

		internal class DeferBindingResolutionWrapper : global::System.IDisposable
		{
			public void Acquire()
			{
				global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution++;
			}

			public void Dispose()
			{
				if (global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution > 0)
				{
					global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution--;
				}
				if (global::UnityEngine.InputSystem.InputActionMap.s_DeferBindingResolution == 0)
				{
					global::UnityEngine.InputSystem.InputActionState.DeferredResolutionOfBindings();
				}
			}
		}

		private static global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolutionWrapper s_DeferBindingResolutionWrapper;

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue? GetParameterValue(this global::UnityEngine.InputSystem.InputAction action, string name, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding))
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			return action.GetParameterValue(new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride(name, bindingMask));
		}

		private static global::UnityEngine.InputSystem.Utilities.PrimitiveValue? GetParameterValue(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			parameterOverride.bindingMask.action = action.name;
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			using (global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerator parameterEnumerator = new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerable(orCreateActionMap.m_State, parameterOverride, orCreateActionMap.m_MapIndexInState).GetEnumerator())
			{
				if (parameterEnumerator.MoveNext())
				{
					global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter current = parameterEnumerator.Current;
					return global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(current.field.GetValue(current.instance));
				}
			}
			return null;
		}

		public static global::UnityEngine.InputSystem.Utilities.PrimitiveValue? GetParameterValue(this global::UnityEngine.InputSystem.InputAction action, string name, int bindingIndex)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("bindingIndex");
			}
			int index = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			global::UnityEngine.InputSystem.InputBinding bindingMask = new global::UnityEngine.InputSystem.InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[index].id
			};
			return action.GetParameterValue(name, bindingMask);
		}

		public unsafe static TValue? GetParameterValue<TObject, TValue>(this global::UnityEngine.InputSystem.InputAction action, global::System.Linq.Expressions.Expression<global::System.Func<TObject, TValue>> expr, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new global::System.ArgumentNullException("expr");
			}
			global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride = ExtractParameterOverride(expr, bindingMask);
			global::UnityEngine.InputSystem.Utilities.PrimitiveValue? parameterValue = action.GetParameterValue(parameterOverride);
			if (!parameterValue.HasValue)
			{
				return null;
			}
			if (global::System.Type.GetTypeCode(typeof(TValue)) == parameterValue.Value.type)
			{
				global::UnityEngine.InputSystem.Utilities.PrimitiveValue value = parameterValue.Value;
				TValue output = default(TValue);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), value.valuePtr, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>());
				return output;
			}
			return (TValue)global::System.Convert.ChangeType(parameterValue.Value.ToObject(), typeof(TValue));
		}

		public static void ApplyParameterOverride<TObject, TValue>(this global::UnityEngine.InputSystem.InputAction action, global::System.Linq.Expressions.Expression<global::System.Func<TObject, TValue>> expr, TValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new global::System.ArgumentNullException("expr");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride = ExtractParameterOverride(expr, bindingMask, global::UnityEngine.InputSystem.Utilities.PrimitiveValue.From(value));
			ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, parameterOverride);
		}

		public static void ApplyParameterOverride<TObject, TValue>(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::System.Linq.Expressions.Expression<global::System.Func<TObject, TValue>> expr, TValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding)) where TValue : struct
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (expr == null)
			{
				throw new global::System.ArgumentNullException("expr");
			}
			actionMap.ResolveBindingsIfNecessary();
			global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride = ExtractParameterOverride(expr, bindingMask, global::UnityEngine.InputSystem.Utilities.PrimitiveValue.From(value));
			ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, parameterOverride);
		}

		public static void ApplyParameterOverride<TObject, TValue>(this global::UnityEngine.InputSystem.InputActionAsset asset, global::System.Linq.Expressions.Expression<global::System.Func<TObject, TValue>> expr, TValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding)) where TValue : struct
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (expr == null)
			{
				throw new global::System.ArgumentNullException("expr");
			}
			asset.ResolveBindingsIfNecessary();
			global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride = ExtractParameterOverride(expr, bindingMask, global::UnityEngine.InputSystem.Utilities.PrimitiveValue.From(value));
			ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, parameterOverride);
		}

		private static global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride ExtractParameterOverride<TObject, TValue>(global::System.Linq.Expressions.Expression<global::System.Func<TObject, TValue>> expr, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding), global::UnityEngine.InputSystem.Utilities.PrimitiveValue value = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue))
		{
			if (expr == null)
			{
				throw new global::System.ArgumentException("Expression must be a LambdaExpression but was a " + expr.GetType().Name + " instead", "expr");
			}
			global::System.Linq.Expressions.MemberExpression memberExpression = expr.Body as global::System.Linq.Expressions.MemberExpression;
			if (memberExpression == null)
			{
				if (!(expr.Body is global::System.Linq.Expressions.UnaryExpression { NodeType: global::System.Linq.Expressions.ExpressionType.Convert, Operand: global::System.Linq.Expressions.MemberExpression operand }))
				{
					throw new global::System.ArgumentException("Body in LambdaExpression must be a MemberExpression (x.name) but was a " + expr.GetType().Name + " instead", "expr");
				}
				memberExpression = operand;
			}
			string objectRegistrationName;
			if (typeof(global::UnityEngine.InputSystem.InputProcessor).IsAssignableFrom(typeof(TObject)))
			{
				objectRegistrationName = global::UnityEngine.InputSystem.InputProcessor.s_Processors.FindNameForType(typeof(TObject));
			}
			else if (typeof(global::UnityEngine.InputSystem.IInputInteraction).IsAssignableFrom(typeof(TObject)))
			{
				objectRegistrationName = global::UnityEngine.InputSystem.InputInteraction.s_Interactions.FindNameForType(typeof(TObject));
			}
			else
			{
				if (!typeof(global::UnityEngine.InputSystem.InputBindingComposite).IsAssignableFrom(typeof(TObject)))
				{
					throw new global::System.ArgumentException("Given type must be an InputProcessor, IInputInteraction, or InputBindingComposite (was " + typeof(TObject).Name + ")", "TObject");
				}
				objectRegistrationName = global::UnityEngine.InputSystem.InputBindingComposite.s_Composites.FindNameForType(typeof(TObject));
			}
			return new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride(objectRegistrationName, memberExpression.Member.Name, bindingMask, value);
		}

		public static void ApplyParameterOverride(this global::UnityEngine.InputSystem.InputActionMap actionMap, string name, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding))
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			actionMap.ResolveBindingsIfNecessary();
			ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		public static void ApplyParameterOverride(this global::UnityEngine.InputSystem.InputActionAsset asset, string name, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding))
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			asset.ResolveBindingsIfNecessary();
			ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		public static void ApplyParameterOverride(this global::UnityEngine.InputSystem.InputAction action, string name, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value, global::UnityEngine.InputSystem.InputBinding bindingMask = default(global::UnityEngine.InputSystem.InputBinding))
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (name == null)
			{
				throw new global::System.ArgumentNullException("name");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		public static void ApplyParameterOverride(this global::UnityEngine.InputSystem.InputAction action, string name, global::UnityEngine.InputSystem.Utilities.PrimitiveValue value, int bindingIndex)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("bindingIndex");
			}
			int index = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			global::UnityEngine.InputSystem.InputBinding bindingMask = new global::UnityEngine.InputSystem.InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[index].id
			};
			action.ApplyParameterOverride(name, value, bindingMask);
		}

		private static void ApplyParameterOverride(global::UnityEngine.InputSystem.InputActionState state, int mapIndex, ref global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride[] parameterOverrides, ref int parameterOverridesCount, global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			bool flag = false;
			if (parameterOverrides != null)
			{
				for (int i = 0; i < parameterOverridesCount; i++)
				{
					ref global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride reference = ref parameterOverrides[i];
					if (string.Equals(reference.objectRegistrationName, parameterOverride.objectRegistrationName, global::System.StringComparison.OrdinalIgnoreCase) && string.Equals(reference.parameter, parameterOverride.parameter, global::System.StringComparison.OrdinalIgnoreCase) && reference.bindingMask == parameterOverride.bindingMask)
					{
						flag = true;
						reference = parameterOverride;
						break;
					}
				}
			}
			if (!flag)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref parameterOverrides, ref parameterOverridesCount, parameterOverride);
			}
			foreach (global::UnityEngine.InputSystem.InputActionRebindingExtensions.Parameter item in new global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterEnumerable(state, parameterOverride, mapIndex))
			{
				global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride? parameterOverride2 = global::UnityEngine.InputSystem.InputActionRebindingExtensions.ParameterOverride.Find(state.GetActionMap(item.bindingIndex), ref state.GetBinding(item.bindingIndex), parameterOverride.parameter, parameterOverride.objectRegistrationName);
				if (parameterOverride2.HasValue)
				{
					global::System.TypeCode typeCode = global::System.Type.GetTypeCode(item.field.FieldType);
					item.field.SetValue(item.instance, parameterOverride2.Value.value.ConvertTo(typeCode).ToObject());
				}
			}
		}

		public static int GetBindingIndex(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public static int GetBindingIndex(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = actionMap.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		public static int GetBindingIndex(this global::UnityEngine.InputSystem.InputAction action, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			return action.GetBindingIndex(new global::UnityEngine.InputSystem.InputBinding(path, null, group));
		}

		public static global::UnityEngine.InputSystem.InputBinding? GetBindingForControl(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputControl control)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			int bindingIndexForControl = action.GetBindingIndexForControl(control);
			if (bindingIndexForControl == -1)
			{
				return null;
			}
			return action.bindings[bindingIndexForControl];
		}

		public unsafe static int GetBindingIndexForControl(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputControl control)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			global::UnityEngine.InputSystem.InputActionState state = orCreateActionMap.m_State;
			global::UnityEngine.InputSystem.InputControl[] controls = state.controls;
			int totalControlCount = state.totalControlCount;
			global::UnityEngine.InputSystem.InputActionState.BindingState* bindingStates = state.bindingStates;
			int* controlIndexToBindingIndex = state.controlIndexToBindingIndex;
			int actionIndexInState = action.m_ActionIndexInState;
			for (int i = 0; i < totalControlCount; i++)
			{
				if (controls[i] == control)
				{
					int num = controlIndexToBindingIndex[i];
					if (bindingStates[num].actionIndex == actionIndexInState)
					{
						int bindingIndexInMap = state.GetBindingIndexInMap(num);
						return action.BindingIndexOnMapToBindingIndexOnAction(bindingIndexInMap);
					}
				}
			}
			return -1;
		}

		public static string GetBindingDisplayString(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0, string group = null)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::UnityEngine.InputSystem.InputBinding bindingMask;
			if (!string.IsNullOrEmpty(group))
			{
				bindingMask = global::UnityEngine.InputSystem.InputBinding.MaskByGroup(group);
			}
			else
			{
				global::UnityEngine.InputSystem.InputBinding? inputBinding = action.FindEffectiveBindingMask();
				bindingMask = ((!inputBinding.HasValue) ? default(global::UnityEngine.InputSystem.InputBinding) : inputBinding.Value);
			}
			return action.GetBindingDisplayString(bindingMask, options);
		}

		public static string GetBindingDisplayString(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding bindingMask, global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			string text = string.Empty;
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (!bindings[i].isPartOfComposite && bindingMask.Matches(bindings[i]))
				{
					string bindingDisplayString = action.GetBindingDisplayString(i, options);
					text = ((!(text != "")) ? bindingDisplayString : (text + " | " + bindingDisplayString));
				}
			}
			return text;
		}

		public static string GetBindingDisplayString(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex, global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			string deviceLayoutName;
			string controlPath;
			return action.GetBindingDisplayString(bindingIndex, out deviceLayoutName, out controlPath, options);
		}

		public unsafe static string GetBindingDisplayString(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex, out string deviceLayoutName, out string controlPath, global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions options = (global::UnityEngine.InputSystem.InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			deviceLayoutName = null;
			controlPath = null;
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			if (bindingIndex < 0 || bindingIndex >= count)
			{
				throw new global::System.ArgumentOutOfRangeException($"Binding index {bindingIndex} is out of range on action '{action}' with {bindings.Count} bindings", "bindingIndex");
			}
			if (bindings[bindingIndex].isComposite)
			{
				string name = global::UnityEngine.InputSystem.Utilities.NameAndParameters.Parse(bindings[bindingIndex].effectivePath).name;
				int firstPartIndex = bindingIndex + 1;
				int i;
				for (i = firstPartIndex; i < count && bindings[i].isPartOfComposite; i++)
				{
				}
				int partCount = i - firstPartIndex;
				string[] partStrings = new string[partCount];
				for (int j = 0; j < partCount; j++)
				{
					string text = action.GetBindingDisplayString(firstPartIndex + j, options);
					if (string.IsNullOrEmpty(text))
					{
						text = " ";
					}
					partStrings[j] = text;
				}
				string displayFormatString = global::UnityEngine.InputSystem.InputBindingComposite.GetDisplayFormatString(name);
				if (string.IsNullOrEmpty(displayFormatString))
				{
					return global::UnityEngine.InputSystem.Utilities.StringHelpers.Join("/", partStrings);
				}
				return global::UnityEngine.InputSystem.Utilities.StringHelpers.ExpandTemplateString(displayFormatString, delegate(string fragment)
				{
					string text2 = string.Empty;
					for (int k = 0; k < partCount; k++)
					{
						if (string.Equals(bindings[firstPartIndex + k].name, fragment, global::System.StringComparison.InvariantCultureIgnoreCase))
						{
							text2 = (string.IsNullOrEmpty(text2) ? partStrings[k] : (text2 + "|" + partStrings[k]));
						}
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = " ";
					}
					return text2;
				});
			}
			global::UnityEngine.InputSystem.InputControl control = null;
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			global::UnityEngine.InputSystem.InputActionState state = orCreateActionMap.m_State;
			int bindingIndexInMap = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			int bindingIndexInState = state.GetBindingIndexInState(orCreateActionMap.m_MapIndexInState, bindingIndexInMap);
			global::UnityEngine.InputSystem.InputActionState.BindingState* ptr = state.bindingStates + bindingIndexInState;
			if (ptr->controlCount > 0)
			{
				control = state.controls[ptr->controlStartIndex];
			}
			global::UnityEngine.InputSystem.InputBinding inputBinding = bindings[bindingIndex];
			if (string.IsNullOrEmpty(inputBinding.effectiveInteractions))
			{
				inputBinding.overrideInteractions = action.interactions;
			}
			else if (!string.IsNullOrEmpty(action.interactions))
			{
				inputBinding.overrideInteractions = inputBinding.effectiveInteractions + ";action.interactions";
			}
			return inputBinding.ToDisplayString(out deviceLayoutName, out controlPath, options, control);
		}

		public static void ApplyBindingOverride(this global::UnityEngine.InputSystem.InputAction action, string newPath, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			action.ApplyBindingOverride(new global::UnityEngine.InputSystem.InputBinding
			{
				overridePath = newPath,
				groups = group,
				path = path
			});
		}

		public static void ApplyBindingOverride(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			bool enabled = action.enabled;
			if (enabled)
			{
				action.Disable();
			}
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(bindingOverride);
			if (enabled)
			{
				action.Enable();
				action.RequestInitialStateCheckOnEnabledAction();
			}
		}

		public static void ApplyBindingOverride(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex, global::UnityEngine.InputSystem.InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			int bindingIndex2 = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(bindingIndex2, bindingOverride);
		}

		public static void ApplyBindingOverride(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex, string path)
		{
			if (path == null)
			{
				throw new global::System.ArgumentException("Binding path cannot be null", "path");
			}
			action.ApplyBindingOverride(bindingIndex, new global::UnityEngine.InputSystem.InputBinding
			{
				overridePath = path
			});
		}

		public static int ApplyBindingOverride(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			global::UnityEngine.InputSystem.InputBinding[] bindings = actionMap.m_Bindings;
			if (bindings == null)
			{
				return 0;
			}
			int num = bindings.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (bindingOverride.Matches(ref bindings[i]))
				{
					bindings[i].overridePath = bindingOverride.overridePath;
					bindings[i].overrideInteractions = bindingOverride.overrideInteractions;
					bindings[i].overrideProcessors = bindingOverride.overrideProcessors;
					num2++;
				}
			}
			if (num2 > 0)
			{
				actionMap.OnBindingModified();
			}
			return num2;
		}

		public static void ApplyBindingOverride(this global::UnityEngine.InputSystem.InputActionMap actionMap, int bindingIndex, global::UnityEngine.InputSystem.InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			global::UnityEngine.InputSystem.InputBinding[] bindings = actionMap.m_Bindings;
			int num = ((bindings != null) ? bindings.Length : 0);
			if (bindingIndex < 0 || bindingIndex >= num)
			{
				throw new global::System.ArgumentOutOfRangeException("bindingIndex", $"Cannot apply override to binding at index {bindingIndex} in map '{actionMap}' with only {num} bindings");
			}
			actionMap.m_Bindings[bindingIndex].overridePath = bindingOverride.overridePath;
			actionMap.m_Bindings[bindingIndex].overrideInteractions = bindingOverride.overrideInteractions;
			actionMap.m_Bindings[bindingIndex].overrideProcessors = bindingOverride.overrideProcessors;
			actionMap.OnBindingModified();
		}

		public static void RemoveBindingOverride(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			action.ApplyBindingOverride(bindingIndex, default(global::UnityEngine.InputSystem.InputBinding));
		}

		public static void RemoveBindingOverride(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			action.ApplyBindingOverride(bindingMask);
		}

		private static void RemoveBindingOverride(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			actionMap.ApplyBindingOverride(bindingMask);
		}

		public static void RemoveAllBindingOverrides(this global::UnityEngine.InputSystem.IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new global::System.ArgumentNullException("actions");
			}
			using (DeferBindingResolution())
			{
				foreach (global::UnityEngine.InputSystem.InputAction action in actions)
				{
					global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
					global::UnityEngine.InputSystem.InputBinding[] bindings = orCreateActionMap.m_Bindings;
					int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(bindings);
					for (int i = 0; i < num; i++)
					{
						ref global::UnityEngine.InputSystem.InputBinding reference = ref bindings[i];
						if (reference.TriggersAction(action))
						{
							reference.RemoveOverrides();
						}
					}
					orCreateActionMap.OnBindingModified();
				}
			}
		}

		public static void RemoveAllBindingOverrides(this global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			string name = action.name;
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			global::UnityEngine.InputSystem.InputBinding[] bindings = orCreateActionMap.m_Bindings;
			if (bindings == null)
			{
				return;
			}
			int num = bindings.Length;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].overridePath = null;
					bindings[i].overrideInteractions = null;
					bindings[i].overrideProcessors = null;
				}
			}
			orCreateActionMap.OnBindingModified();
		}

		public static void ApplyBindingOverrides(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			foreach (global::UnityEngine.InputSystem.InputBinding @override in overrides)
			{
				actionMap.ApplyBindingOverride(@override);
			}
		}

		public static void RemoveBindingOverrides(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			foreach (global::UnityEngine.InputSystem.InputBinding @override in overrides)
			{
				actionMap.RemoveBindingOverride(@override);
			}
		}

		public static int ApplyBindingOverridesOnMatchingControls(this global::UnityEngine.InputSystem.InputAction action, global::UnityEngine.InputSystem.InputControl control)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = global::UnityEngine.InputSystem.InputControlPath.TryFindControl(control, bindings[i].path);
				if (inputControl != null)
				{
					action.ApplyBindingOverride(i, inputControl.path);
					num++;
				}
			}
			return num;
		}

		public static int ApplyBindingOverridesOnMatchingControls(this global::UnityEngine.InputSystem.InputActionMap actionMap, global::UnityEngine.InputSystem.InputControl control)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (control == null)
			{
				throw new global::System.ArgumentNullException("control");
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputAction> actions = actionMap.actions;
			int count = actions.Count;
			int result = 0;
			for (int i = 0; i < count; i++)
			{
				result = actions[i].ApplyBindingOverridesOnMatchingControls(control);
			}
			return result;
		}

		public static string SaveBindingOverridesAsJson(this global::UnityEngine.InputSystem.IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new global::System.ArgumentNullException("actions");
			}
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson>();
			foreach (global::UnityEngine.InputSystem.InputBinding binding in actions.bindings)
			{
				actions.AddBindingOverrideJsonTo(binding, list);
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return global::UnityEngine.JsonUtility.ToJson(new global::UnityEngine.InputSystem.InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		public static string SaveBindingOverridesAsJson(this global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			bool isSingletonAction = action.isSingletonAction;
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson>();
			foreach (global::UnityEngine.InputSystem.InputBinding binding in action.bindings)
			{
				if (isSingletonAction || binding.TriggersAction(action))
				{
					orCreateActionMap.AddBindingOverrideJsonTo(binding, list, isSingletonAction ? action : null);
				}
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return global::UnityEngine.JsonUtility.ToJson(new global::UnityEngine.InputSystem.InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		private static void AddBindingOverrideJsonTo(this global::UnityEngine.InputSystem.IInputActionCollection2 actions, global::UnityEngine.InputSystem.InputBinding binding, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson> list, global::UnityEngine.InputSystem.InputAction action = null)
		{
			if (binding.hasOverrides)
			{
				if (action == null)
				{
					action = actions.FindAction(binding.action);
				}
				string actionName = ((action != null && !action.isSingletonAction) ? (action.actionMap.name + "/" + action.name) : "");
				global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson item = global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson.FromBinding(binding, actionName);
				list.Add(item);
			}
		}

		public static void LoadBindingOverridesFromJson(this global::UnityEngine.InputSystem.IInputActionCollection2 actions, string json, bool removeExisting = true)
		{
			if (actions == null)
			{
				throw new global::System.ArgumentNullException("actions");
			}
			using (DeferBindingResolution())
			{
				if (removeExisting)
				{
					actions.RemoveAllBindingOverrides();
				}
				actions.LoadBindingOverridesFromJsonInternal(json);
			}
		}

		public static void LoadBindingOverridesFromJson(this global::UnityEngine.InputSystem.InputAction action, string json, bool removeExisting = true)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			using (DeferBindingResolution())
			{
				if (removeExisting)
				{
					action.RemoveAllBindingOverrides();
				}
				action.GetOrCreateActionMap().LoadBindingOverridesFromJsonInternal(json);
			}
		}

		private static void LoadBindingOverridesFromJsonInternal(this global::UnityEngine.InputSystem.IInputActionCollection2 actions, string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			foreach (global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson binding in global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.InputActionMap.BindingOverrideListJson>(json).bindings)
			{
				if (!string.IsNullOrEmpty(binding.id))
				{
					global::UnityEngine.InputSystem.InputAction action;
					int num = actions.FindBinding(new global::UnityEngine.InputSystem.InputBinding
					{
						m_Id = binding.id
					}, out action);
					if (num != -1)
					{
						action.ApplyBindingOverride(num, global::UnityEngine.InputSystem.InputActionMap.BindingOverrideJson.ToBinding(binding));
						continue;
					}
				}
				global::UnityEngine.Debug.LogWarning("Could not override binding as no existing binding was found with the id: " + binding.id);
			}
		}

		public static global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation PerformInteractiveRebinding(this global::UnityEngine.InputSystem.InputAction action, int bindingIndex = -1)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation rebindingOperation = new global::UnityEngine.InputSystem.InputActionRebindingExtensions.RebindingOperation().WithAction(action).OnMatchWaitForAnother(0.05f).WithControlsExcluding("<Pointer>/delta")
				.WithControlsExcluding("<Pointer>/position")
				.WithControlsExcluding("<Touchscreen>/touch*/position")
				.WithControlsExcluding("<Touchscreen>/touch*/delta")
				.WithControlsExcluding("<Mouse>/clickCount")
				.WithMatchingEventsBeingSuppressed();
			if (rebindingOperation.expectedControlType != "Button")
			{
				rebindingOperation.WithCancelingThrough("<Keyboard>/escape");
			}
			if (bindingIndex >= 0)
			{
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings = action.bindings;
				if (bindingIndex >= bindings.Count)
				{
					throw new global::System.ArgumentOutOfRangeException($"Binding index {bindingIndex} is out of range for action '{action}' with {bindings.Count} bindings", "bindings");
				}
				if (bindings[bindingIndex].isComposite)
				{
					throw new global::System.InvalidOperationException($"Cannot perform rebinding on composite binding '{bindings[bindingIndex]}' of '{action}'");
				}
				rebindingOperation.WithTargetBinding(bindingIndex);
			}
			return rebindingOperation;
		}

		internal static global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolutionWrapper DeferBindingResolution()
		{
			if (s_DeferBindingResolutionWrapper == null)
			{
				s_DeferBindingResolutionWrapper = new global::UnityEngine.InputSystem.InputActionRebindingExtensions.DeferBindingResolutionWrapper();
			}
			s_DeferBindingResolutionWrapper.Acquire();
			return s_DeferBindingResolutionWrapper;
		}
	}
}

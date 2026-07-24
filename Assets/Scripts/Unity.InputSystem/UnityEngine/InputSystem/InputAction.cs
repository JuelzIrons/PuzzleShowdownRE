namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	public sealed class InputAction : global::System.ICloneable, global::System.IDisposable
	{
		[global::System.Flags]
		internal enum ActionFlags
		{
			WantsInitialStateCheck = 1
		}

		public struct CallbackContext
		{
			internal global::UnityEngine.InputSystem.InputActionState m_State;

			internal int m_ActionIndex;

			private int actionIndex => m_ActionIndex;

			private unsafe int bindingIndex => m_State.actionStates[actionIndex].bindingIndex;

			private unsafe int controlIndex => m_State.actionStates[actionIndex].controlIndex;

			private unsafe int interactionIndex => m_State.actionStates[actionIndex].interactionIndex;

			public unsafe global::UnityEngine.InputSystem.InputActionPhase phase
			{
				get
				{
					if (m_State == null)
					{
						return global::UnityEngine.InputSystem.InputActionPhase.Disabled;
					}
					return m_State.actionStates[actionIndex].phase;
				}
			}

			public bool started => phase == global::UnityEngine.InputSystem.InputActionPhase.Started;

			public bool performed => phase == global::UnityEngine.InputSystem.InputActionPhase.Performed;

			public bool canceled => phase == global::UnityEngine.InputSystem.InputActionPhase.Canceled;

			public global::UnityEngine.InputSystem.InputAction action => m_State?.GetActionOrNull(bindingIndex);

			public global::UnityEngine.InputSystem.InputControl control
			{
				get
				{
					global::UnityEngine.InputSystem.InputActionState state = m_State;
					if (state == null)
					{
						return null;
					}
					return state.controls[controlIndex];
				}
			}

			public global::UnityEngine.InputSystem.IInputInteraction interaction
			{
				get
				{
					if (m_State == null)
					{
						return null;
					}
					int num = interactionIndex;
					if (num == -1)
					{
						return null;
					}
					return m_State.interactions[num];
				}
			}

			public unsafe double time
			{
				get
				{
					if (m_State == null)
					{
						return 0.0;
					}
					return m_State.actionStates[actionIndex].time;
				}
			}

			public unsafe double startTime
			{
				get
				{
					if (m_State == null)
					{
						return 0.0;
					}
					return m_State.actionStates[actionIndex].startTime;
				}
			}

			public double duration => time - startTime;

			public global::System.Type valueType => m_State?.GetValueType(bindingIndex, controlIndex);

			public int valueSizeInBytes
			{
				get
				{
					if (m_State == null)
					{
						return 0;
					}
					return m_State.GetValueSizeInBytes(bindingIndex, controlIndex);
				}
			}

			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				if (buffer == null)
				{
					throw new global::System.ArgumentNullException("buffer");
				}
				if (m_State != null && phase.IsInProgress())
				{
					m_State.ReadValue(bindingIndex, controlIndex, buffer, bufferSize);
					return;
				}
				int num = valueSizeInBytes;
				if (bufferSize < num)
				{
					throw new global::System.ArgumentException($"Expected buffer of at least {num} bytes but got buffer of only {bufferSize} bytes", "bufferSize");
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(buffer, valueSizeInBytes);
			}

			public TValue ReadValue<TValue>() where TValue : struct
			{
				TValue val = default(TValue);
				if (m_State != null)
				{
					return phase.IsInProgress() ? m_State.ReadValue<TValue>(bindingIndex, controlIndex) : m_State.ApplyProcessors(bindingIndex, val);
				}
				return val;
			}

			public bool ReadValueAsButton()
			{
				bool result = false;
				if (m_State != null && phase.IsInProgress())
				{
					result = m_State.ReadValueAsButton(bindingIndex, controlIndex);
				}
				return result;
			}

			public object ReadValueAsObject()
			{
				if (m_State != null && phase.IsInProgress())
				{
					return m_State.ReadValueAsObject(bindingIndex, controlIndex);
				}
				return null;
			}

			public override string ToString()
			{
				return $"{{ action={action} phase={phase} time={time} control={control} value={ReadValueAsObject()} interaction={interaction} }}";
			}
		}

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputActionEnableProfilerMarker = new global::Unity.Profiling.ProfilerMarker("InputAction.Enable");

		private static readonly global::Unity.Profiling.ProfilerMarker k_InputActionDisableProfilerMarker = new global::Unity.Profiling.ProfilerMarker("InputAction.Disable");

		[global::UnityEngine.Tooltip("Human readable name of the action. Must be unique within its action map (case is ignored). Can be changed without breaking references to the action.")]
		[global::UnityEngine.SerializeField]
		internal string m_Name;

		[global::UnityEngine.Tooltip("Determines how the action triggers.\n\nA Value action will start and perform when a control moves from its default value and then perform on every value change. It will cancel when controls go back to default value. Also, when enabled, a Value action will respond right away to a control's current value.\n\nA Button action will start when a button is pressed and perform when the press threshold (see 'Default Button Press Point' in settings) is reached. It will cancel when the button is going below the release threshold (see 'Button Release Threshold' in settings). Also, if a button is already pressed when the action is enabled, the button has to be released first.\n\nA Pass-Through action will not explicitly start and will never cancel. Instead, for every value change on any bound control, the action will perform.")]
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionType m_Type;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_ExpectedControlLayout")]
		[global::UnityEngine.Tooltip("The type of control expected by the action (e.g. \"Digital\" for buttons, \"Vector2\" for sticks). This will limit the controls shown when setting up bindings in the UI and will also limit which controls can be bound interactively to the action.")]
		[global::UnityEngine.SerializeField]
		internal string m_ExpectedControlType;

		[global::UnityEngine.Tooltip("Unique ID of the action (GUID). Used to reference the action from bindings such that actions can be renamed without breaking references.")]
		[global::UnityEngine.SerializeField]
		internal string m_Id;

		[global::UnityEngine.SerializeField]
		internal string m_Processors;

		[global::UnityEngine.SerializeField]
		internal string m_Interactions;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputBinding[] m_SingletonActionBindings;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputAction.ActionFlags m_Flags;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputBinding? m_BindingMask;

		[global::System.NonSerialized]
		internal int m_BindingsStartIndex;

		[global::System.NonSerialized]
		internal int m_BindingsCount;

		[global::System.NonSerialized]
		internal int m_ControlStartIndex;

		[global::System.NonSerialized]
		internal int m_ControlCount;

		[global::System.NonSerialized]
		internal int m_ActionIndexInState = -1;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.InputActionMap m_ActionMap;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> m_OnStarted;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> m_OnCanceled;

		[global::System.NonSerialized]
		internal global::UnityEngine.InputSystem.Utilities.CallbackArray<global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>> m_OnPerformed;

		public string name => m_Name;

		public global::UnityEngine.InputSystem.InputActionType type => m_Type;

		public global::System.Guid id
		{
			get
			{
				MakeSureIdIsInPlace();
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

		public string expectedControlType
		{
			get
			{
				return m_ExpectedControlType;
			}
			set
			{
				m_ExpectedControlType = value;
			}
		}

		public string processors => m_Processors;

		public string interactions => m_Interactions;

		public global::UnityEngine.InputSystem.InputActionMap actionMap
		{
			get
			{
				if (!isSingletonAction)
				{
					return m_ActionMap;
				}
				return null;
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
				if (!(value == m_BindingMask))
				{
					if (value.HasValue)
					{
						global::UnityEngine.InputSystem.InputBinding value2 = value.Value;
						value2.action = name;
						value = value2;
					}
					m_BindingMask = value;
					global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = GetOrCreateActionMap();
					if (orCreateActionMap.m_State != null)
					{
						orCreateActionMap.LazyResolveBindings(fullResolve: true);
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputBinding> bindings => GetOrCreateActionMap().GetBindingsForSingleAction(this);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> controls
		{
			get
			{
				global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = GetOrCreateActionMap();
				orCreateActionMap.ResolveBindingsIfNecessary();
				return orCreateActionMap.GetControlsForSingleAction(this);
			}
		}

		public global::UnityEngine.InputSystem.InputActionPhase phase => currentState.phase;

		public bool inProgress => phase.IsInProgress();

		public bool enabled => phase != global::UnityEngine.InputSystem.InputActionPhase.Disabled;

		public bool triggered => WasPerformedThisFrame();

		public unsafe global::UnityEngine.InputSystem.InputControl activeControl
		{
			get
			{
				global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
				if (state != null)
				{
					int controlIndex = state.actionStates[m_ActionIndexInState].controlIndex;
					if (controlIndex != -1)
					{
						return state.controls[controlIndex];
					}
				}
				return null;
			}
		}

		public unsafe global::System.Type activeValueType
		{
			get
			{
				global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
				if (state != null)
				{
					global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = state.actionStates + m_ActionIndexInState;
					int controlIndex = ptr->controlIndex;
					if (controlIndex != -1)
					{
						return state.GetValueType(ptr->bindingIndex, controlIndex);
					}
				}
				return null;
			}
		}

		public bool wantsInitialStateCheck
		{
			get
			{
				if (type != global::UnityEngine.InputSystem.InputActionType.Value)
				{
					return (m_Flags & global::UnityEngine.InputSystem.InputAction.ActionFlags.WantsInitialStateCheck) != 0;
				}
				return true;
			}
			set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputAction.ActionFlags.WantsInitialStateCheck;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputAction.ActionFlags.WantsInitialStateCheck;
				}
			}
		}

		internal bool isSingletonAction
		{
			get
			{
				if (m_ActionMap != null)
				{
					return m_ActionMap.m_SingletonAction == this;
				}
				return true;
			}
		}

		private global::UnityEngine.InputSystem.InputActionState.TriggerState currentState
		{
			get
			{
				if (m_ActionIndexInState == -1)
				{
					return default(global::UnityEngine.InputSystem.InputActionState.TriggerState);
				}
				return m_ActionMap.m_State.FetchActionState(this);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> started
		{
			add
			{
				m_OnStarted.AddCallback(value);
			}
			remove
			{
				m_OnStarted.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> canceled
		{
			add
			{
				m_OnCanceled.AddCallback(value);
			}
			remove
			{
				m_OnCanceled.RemoveCallback(value);
			}
		}

		public event global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> performed
		{
			add
			{
				m_OnPerformed.AddCallback(value);
			}
			remove
			{
				m_OnPerformed.RemoveCallback(value);
			}
		}

		public InputAction()
		{
			m_Id = global::System.Guid.NewGuid().ToString();
		}

		public InputAction(string name = null, global::UnityEngine.InputSystem.InputActionType type = global::UnityEngine.InputSystem.InputActionType.Value, string binding = null, string interactions = null, string processors = null, string expectedControlType = null)
		{
			m_Name = name;
			m_Type = type;
			if (!string.IsNullOrEmpty(binding))
			{
				m_SingletonActionBindings = new global::UnityEngine.InputSystem.InputBinding[1]
				{
					new global::UnityEngine.InputSystem.InputBinding
					{
						path = binding,
						interactions = interactions,
						processors = processors,
						action = m_Name,
						id = global::System.Guid.NewGuid()
					}
				};
				m_BindingsStartIndex = 0;
				m_BindingsCount = 1;
			}
			else
			{
				m_Interactions = interactions;
				m_Processors = processors;
			}
			m_ExpectedControlType = expectedControlType;
			m_Id = global::System.Guid.NewGuid().ToString();
		}

		public void Dispose()
		{
			m_ActionMap?.m_State?.Dispose();
		}

		public override string ToString()
		{
			string text = ((m_Name == null) ? "<Unnamed>" : ((m_ActionMap == null || isSingletonAction || string.IsNullOrEmpty(m_ActionMap.name)) ? m_Name : (m_ActionMap.name + "/" + m_Name)));
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> readOnlyArray = controls;
			if (readOnlyArray.Count > 0)
			{
				text += "[";
				bool flag = true;
				foreach (global::UnityEngine.InputSystem.InputControl item in readOnlyArray)
				{
					if (!flag)
					{
						text += ",";
					}
					text += item.path;
					flag = false;
				}
				text += "]";
			}
			return text;
		}

		public void Enable()
		{
			using (k_InputActionEnableProfilerMarker.Auto())
			{
				if (!enabled)
				{
					global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = GetOrCreateActionMap();
					orCreateActionMap.ResolveBindingsIfNecessary();
					orCreateActionMap.m_State.EnableSingleAction(this);
				}
			}
		}

		public void Disable()
		{
			using (k_InputActionDisableProfilerMarker.Auto())
			{
				if (enabled)
				{
					m_ActionMap.m_State.DisableSingleAction(this);
				}
			}
		}

		public global::UnityEngine.InputSystem.InputAction Clone()
		{
			return new global::UnityEngine.InputSystem.InputAction(m_Name, m_Type)
			{
				m_SingletonActionBindings = bindings.ToArray(),
				m_BindingsCount = m_BindingsCount,
				m_ExpectedControlType = m_ExpectedControlType,
				m_Interactions = m_Interactions,
				m_Processors = m_Processors,
				m_Flags = m_Flags
			};
		}

		object global::System.ICloneable.Clone()
		{
			return Clone();
		}

		public unsafe TValue ReadValue<TValue>() where TValue : struct
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return default(TValue);
			}
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = state.actionStates + m_ActionIndexInState;
			if (!ptr->phase.IsInProgress())
			{
				return state.ApplyProcessors(ptr->bindingIndex, default(TValue));
			}
			return state.ReadValue<TValue>(ptr->bindingIndex, ptr->controlIndex);
		}

		public unsafe object ReadValueAsObject()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return null;
			}
			global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = state.actionStates + m_ActionIndexInState;
			if (ptr->phase.IsInProgress())
			{
				int controlIndex = ptr->controlIndex;
				if (controlIndex != -1)
				{
					return state.ReadValueAsObject(ptr->bindingIndex, controlIndex);
				}
			}
			return null;
		}

		public unsafe float GetControlMagnitude()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				global::UnityEngine.InputSystem.InputActionState.TriggerState* ptr = state.actionStates + m_ActionIndexInState;
				if (ptr->haveMagnitude)
				{
					return ptr->magnitude;
				}
			}
			return 0f;
		}

		public void Reset()
		{
			GetOrCreateActionMap().m_State?.ResetActionState(m_ActionIndexInState, enabled ? global::UnityEngine.InputSystem.InputActionPhase.Waiting : global::UnityEngine.InputSystem.InputActionPhase.Disabled, hardReset: true);
		}

		public unsafe bool IsPressed()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].isPressed;
			}
			return false;
		}

		public unsafe bool IsInProgress()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].phase.IsInProgress();
			}
			return false;
		}

		private int ExpectedFrame()
		{
			int num = ((global::UnityEngine.InputSystem.InputSystem.settings.updateMode == global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsManually) ? 1 : 0);
			return global::UnityEngine.Time.frameCount - num;
		}

		public unsafe bool WasPressedThisFrame()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null && !state.IsSuppressed)
			{
				global::UnityEngine.InputSystem.InputActionState.TriggerState* num = state.actionStates + m_ActionIndexInState;
				uint s_UpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				if (num->pressedInUpdate == s_UpdateStepCount)
				{
					return s_UpdateStepCount != 0;
				}
				return false;
			}
			return false;
		}

		public unsafe bool WasPressedThisDynamicUpdate()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].framePressed == ExpectedFrame();
			}
			return false;
		}

		public unsafe bool WasReleasedThisFrame()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				global::UnityEngine.InputSystem.InputActionState.TriggerState* num = state.actionStates + m_ActionIndexInState;
				uint s_UpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				if (num->releasedInUpdate == s_UpdateStepCount)
				{
					return s_UpdateStepCount != 0;
				}
				return false;
			}
			return false;
		}

		public unsafe bool WasReleasedThisDynamicUpdate()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].frameReleased == ExpectedFrame();
			}
			return false;
		}

		public unsafe bool WasPerformedThisFrame()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null && !state.IsSuppressed)
			{
				global::UnityEngine.InputSystem.InputActionState.TriggerState* num = state.actionStates + m_ActionIndexInState;
				uint s_UpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				if (num->lastPerformedInUpdate == s_UpdateStepCount)
				{
					return s_UpdateStepCount != 0;
				}
				return false;
			}
			return false;
		}

		public unsafe bool WasPerformedThisDynamicUpdate()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].framePerformed == ExpectedFrame();
			}
			return false;
		}

		public unsafe bool WasCompletedThisFrame()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				global::UnityEngine.InputSystem.InputActionState.TriggerState* num = state.actionStates + m_ActionIndexInState;
				uint s_UpdateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
				if (num->lastCompletedInUpdate == s_UpdateStepCount)
				{
					return s_UpdateStepCount != 0;
				}
				return false;
			}
			return false;
		}

		public unsafe bool WasCompletedThisDynamicUpdate()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state != null)
			{
				return state.actionStates[m_ActionIndexInState].frameCompleted == ExpectedFrame();
			}
			return false;
		}

		public unsafe float GetTimeoutCompletionPercentage()
		{
			global::UnityEngine.InputSystem.InputActionState state = GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return 0f;
			}
			ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference = ref state.actionStates[m_ActionIndexInState];
			int interactionIndex = reference.interactionIndex;
			if (interactionIndex == -1)
			{
				return (reference.phase == global::UnityEngine.InputSystem.InputActionPhase.Performed) ? 1 : 0;
			}
			ref global::UnityEngine.InputSystem.InputActionState.InteractionState reference2 = ref state.interactionStates[interactionIndex];
			switch (reference2.phase)
			{
			case global::UnityEngine.InputSystem.InputActionPhase.Started:
			{
				float num = 0f;
				if (reference2.isTimerRunning)
				{
					float timerDuration = reference2.timerDuration;
					double num2 = reference2.timerStartTime + (double)timerDuration - global::UnityEngine.InputSystem.LowLevel.InputState.currentTime;
					num = ((!(num2 <= 0.0)) ? ((float)(((double)timerDuration - num2) / (double)timerDuration)) : 1f);
				}
				if (reference2.totalTimeoutCompletionTimeRemaining > 0f)
				{
					return (reference2.totalTimeoutCompletionDone + num * reference2.timerDuration) / (reference2.totalTimeoutCompletionDone + reference2.totalTimeoutCompletionTimeRemaining);
				}
				return num;
			}
			case global::UnityEngine.InputSystem.InputActionPhase.Performed:
				return 1f;
			default:
				return 0f;
			}
		}

		internal string MakeSureIdIsInPlace()
		{
			if (string.IsNullOrEmpty(m_Id))
			{
				GenerateId();
			}
			return m_Id;
		}

		internal void GenerateId()
		{
			m_Id = global::System.Guid.NewGuid().ToString();
		}

		internal global::UnityEngine.InputSystem.InputActionMap GetOrCreateActionMap()
		{
			if (m_ActionMap == null)
			{
				CreateInternalActionMapForSingletonAction();
			}
			return m_ActionMap;
		}

		private void CreateInternalActionMapForSingletonAction()
		{
			m_ActionMap = new global::UnityEngine.InputSystem.InputActionMap
			{
				m_Actions = new global::UnityEngine.InputSystem.InputAction[1] { this },
				m_SingletonAction = this,
				m_Bindings = m_SingletonActionBindings
			};
		}

		internal void RequestInitialStateCheckOnEnabledAction()
		{
			GetOrCreateActionMap().m_State.SetInitialStateCheckPending(m_ActionIndexInState);
		}

		internal bool ActiveControlIsValid(global::UnityEngine.InputSystem.InputControl control)
		{
			if (control == null)
			{
				return false;
			}
			global::UnityEngine.InputSystem.InputDevice device = control.device;
			if (!device.added)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>? devices = GetOrCreateActionMap().devices;
			if (devices.HasValue && !global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.ContainsReference(devices.Value, device))
			{
				return false;
			}
			return true;
		}

		internal global::UnityEngine.InputSystem.InputBinding? FindEffectiveBindingMask()
		{
			if (m_BindingMask.HasValue)
			{
				return m_BindingMask;
			}
			global::UnityEngine.InputSystem.InputActionMap inputActionMap = m_ActionMap;
			if (inputActionMap != null && inputActionMap.m_BindingMask.HasValue)
			{
				return m_ActionMap.m_BindingMask;
			}
			return m_ActionMap?.m_Asset?.m_BindingMask;
		}

		internal int BindingIndexOnActionToBindingIndexOnMap(int indexOfBindingOnAction)
		{
			global::UnityEngine.InputSystem.InputBinding[] array = GetOrCreateActionMap().m_Bindings;
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(array);
			_ = name;
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (array[i].TriggersAction(this))
				{
					num2++;
					if (num2 == indexOfBindingOnAction)
					{
						return i;
					}
				}
			}
			throw new global::System.ArgumentOutOfRangeException("indexOfBindingOnAction", $"Binding index {indexOfBindingOnAction} is out of range for action '{this}' with {num2 + 1} bindings");
		}

		internal int BindingIndexOnMapToBindingIndexOnAction(int indexOfBindingOnMap)
		{
			global::UnityEngine.InputSystem.InputBinding[] array = GetOrCreateActionMap().m_Bindings;
			string strB = name;
			int num = 0;
			for (int num2 = indexOfBindingOnMap - 1; num2 >= 0; num2--)
			{
				ref global::UnityEngine.InputSystem.InputBinding reference = ref array[num2];
				if (string.Compare(reference.action, strB, global::System.StringComparison.InvariantCultureIgnoreCase) == 0 || reference.action == m_Id)
				{
					num++;
				}
			}
			return num;
		}
	}
}

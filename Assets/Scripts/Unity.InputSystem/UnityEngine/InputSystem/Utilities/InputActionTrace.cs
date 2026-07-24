namespace UnityEngine.InputSystem.Utilities
{
	public sealed class InputActionTrace : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr>, global::System.Collections.IEnumerable, global::System.IDisposable
	{
		public struct ActionEventPtr
		{
			internal global::UnityEngine.InputSystem.InputActionState m_State;

			internal unsafe global::UnityEngine.InputSystem.LowLevel.ActionEvent* m_Ptr;

			public unsafe global::UnityEngine.InputSystem.InputAction action => m_State.GetActionOrNull(m_Ptr->bindingIndex);

			public unsafe global::UnityEngine.InputSystem.InputActionPhase phase => m_Ptr->phase;

			public unsafe global::UnityEngine.InputSystem.InputControl control => m_State.controls[m_Ptr->controlIndex];

			public unsafe global::UnityEngine.InputSystem.IInputInteraction interaction
			{
				get
				{
					int interactionIndex = m_Ptr->interactionIndex;
					if (interactionIndex == -1)
					{
						return null;
					}
					return m_State.interactions[interactionIndex];
				}
			}

			public unsafe double time => m_Ptr->baseEvent.time;

			public unsafe double startTime => m_Ptr->startTime;

			public double duration => time - startTime;

			public unsafe int valueSizeInBytes => m_Ptr->valueSizeInBytes;

			public unsafe object ReadValueAsObject()
			{
				if (m_Ptr == null)
				{
					throw new global::System.InvalidOperationException("ActionEventPtr is invalid");
				}
				byte* valueData = m_Ptr->valueData;
				int bindingIndex = m_Ptr->bindingIndex;
				if (m_State.bindingStates[bindingIndex].isPartOfComposite)
				{
					int compositeOrCompositeBindingIndex = m_State.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
					int compositeOrCompositeBindingIndex2 = m_State.bindingStates[compositeOrCompositeBindingIndex].compositeOrCompositeBindingIndex;
					global::UnityEngine.InputSystem.InputBindingComposite inputBindingComposite = m_State.composites[compositeOrCompositeBindingIndex2];
					global::System.Type valueType = inputBindingComposite.valueType;
					if (valueType == null)
					{
						throw new global::System.InvalidOperationException($"Cannot read value from Composite '{inputBindingComposite}' which does not have a valueType set");
					}
					return global::System.Runtime.InteropServices.Marshal.PtrToStructure(new global::System.IntPtr(valueData), valueType);
				}
				int bufferSize = m_Ptr->valueSizeInBytes;
				return control.ReadValueFromBufferAsObject(valueData, bufferSize);
			}

			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				int num = m_Ptr->valueSizeInBytes;
				if (bufferSize < num)
				{
					throw new global::System.ArgumentException($"Expected buffer of at least {num} bytes but got buffer of just {bufferSize} bytes instead", "bufferSize");
				}
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, m_Ptr->valueData, num);
			}

			public unsafe TValue ReadValue<TValue>() where TValue : struct
			{
				int num = m_Ptr->valueSizeInBytes;
				if (global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>() != num)
				{
					throw new global::System.InvalidOperationException($"Cannot read a value of type '{typeof(TValue).Name}' with size {(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>())} from event on action '{action}' with value size {num}");
				}
				TValue output = new TValue();
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), m_Ptr->valueData, num);
				return output;
			}

			public unsafe override string ToString()
			{
				if (m_Ptr == null)
				{
					return "<null>";
				}
				string text = ((action.actionMap != null) ? (action.actionMap.name + "/" + action.name) : action.name);
				return $"{{ action={text} phase={phase} time={time} control={control} value={ReadValueAsObject()} interaction={interaction} duration={duration} }}";
			}
		}

		private struct Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private readonly global::UnityEngine.InputSystem.Utilities.InputActionTrace m_Trace;

			private unsafe readonly global::UnityEngine.InputSystem.LowLevel.ActionEvent* m_Buffer;

			private readonly int m_EventCount;

			private unsafe global::UnityEngine.InputSystem.LowLevel.ActionEvent* m_CurrentEvent;

			private int m_CurrentIndex;

			public unsafe global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr Current
			{
				get
				{
					global::UnityEngine.InputSystem.InputActionState state = m_Trace.m_ActionMapStates[m_CurrentEvent->stateIndex];
					return new global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr
					{
						m_State = state,
						m_Ptr = m_CurrentEvent
					};
				}
			}

			object global::System.Collections.IEnumerator.Current => Current;

			public unsafe Enumerator(global::UnityEngine.InputSystem.Utilities.InputActionTrace trace)
			{
				m_Trace = trace;
				m_Buffer = (global::UnityEngine.InputSystem.LowLevel.ActionEvent*)trace.m_EventBuffer.bufferPtr.data;
				m_EventCount = trace.m_EventBuffer.eventCount;
				m_CurrentEvent = null;
				m_CurrentIndex = 0;
			}

			public unsafe bool MoveNext()
			{
				if (m_CurrentIndex == m_EventCount)
				{
					return false;
				}
				if (m_CurrentEvent == null)
				{
					m_CurrentEvent = m_Buffer;
					return m_CurrentEvent != null;
				}
				m_CurrentIndex++;
				if (m_CurrentIndex == m_EventCount)
				{
					return false;
				}
				m_CurrentEvent = (global::UnityEngine.InputSystem.LowLevel.ActionEvent*)global::UnityEngine.InputSystem.LowLevel.InputEvent.GetNextInMemory((global::UnityEngine.InputSystem.LowLevel.InputEvent*)m_CurrentEvent);
				return true;
			}

			public unsafe void Reset()
			{
				m_CurrentEvent = null;
				m_CurrentIndex = 0;
			}

			public void Dispose()
			{
			}
		}

		private bool m_SubscribedToAll;

		private bool m_OnActionChangeHooked;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputAction> m_SubscribedActions;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputActionMap> m_SubscribedActionMaps;

		private global::UnityEngine.InputSystem.LowLevel.InputEventBuffer m_EventBuffer;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputActionState> m_ActionMapStates;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.InputActionState> m_ActionMapStateClones;

		private global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext> m_CallbackDelegate;

		private global::System.Action<object, global::UnityEngine.InputSystem.InputActionChange> m_ActionChangeDelegate;

		public global::UnityEngine.InputSystem.LowLevel.InputEventBuffer buffer => m_EventBuffer;

		public int count => m_EventBuffer.eventCount;

		public InputActionTrace()
		{
		}

		public InputActionTrace(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			SubscribeTo(action);
		}

		public InputActionTrace(global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			SubscribeTo(actionMap);
		}

		public void SubscribeToAll()
		{
			if (!m_SubscribedToAll)
			{
				HookOnActionChange();
				m_SubscribedToAll = true;
				while (m_SubscribedActions.length > 0)
				{
					UnsubscribeFrom(m_SubscribedActions[m_SubscribedActions.length - 1]);
				}
				while (m_SubscribedActionMaps.length > 0)
				{
					UnsubscribeFrom(m_SubscribedActionMaps[m_SubscribedActionMaps.length - 1]);
				}
			}
		}

		public void UnsubscribeFromAll()
		{
			if (count == 0)
			{
				UnhookOnActionChange();
			}
			m_SubscribedToAll = false;
			while (m_SubscribedActions.length > 0)
			{
				UnsubscribeFrom(m_SubscribedActions[m_SubscribedActions.length - 1]);
			}
			while (m_SubscribedActionMaps.length > 0)
			{
				UnsubscribeFrom(m_SubscribedActionMaps[m_SubscribedActionMaps.length - 1]);
			}
		}

		public void SubscribeTo(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (m_CallbackDelegate == null)
			{
				m_CallbackDelegate = RecordAction;
			}
			action.performed += m_CallbackDelegate;
			action.started += m_CallbackDelegate;
			action.canceled += m_CallbackDelegate;
			m_SubscribedActions.AppendWithCapacity(action);
		}

		public void SubscribeTo(global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (m_CallbackDelegate == null)
			{
				m_CallbackDelegate = RecordAction;
			}
			actionMap.actionTriggered += m_CallbackDelegate;
			m_SubscribedActionMaps.AppendWithCapacity(actionMap);
		}

		public void UnsubscribeFrom(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			if (m_CallbackDelegate != null)
			{
				action.performed -= m_CallbackDelegate;
				action.started -= m_CallbackDelegate;
				action.canceled -= m_CallbackDelegate;
				int num = m_SubscribedActions.IndexOfReference(action);
				if (num != -1)
				{
					m_SubscribedActions.RemoveAtWithCapacity(num);
				}
			}
		}

		public void UnsubscribeFrom(global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new global::System.ArgumentNullException("actionMap");
			}
			if (m_CallbackDelegate != null)
			{
				actionMap.actionTriggered -= m_CallbackDelegate;
				int num = m_SubscribedActionMaps.IndexOfReference(actionMap);
				if (num != -1)
				{
					m_SubscribedActionMaps.RemoveAtWithCapacity(num);
				}
			}
		}

		public unsafe void RecordAction(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			int num = m_ActionMapStates.IndexOfReference(context.m_State);
			if (num == -1)
			{
				num = m_ActionMapStates.AppendWithCapacity(context.m_State);
			}
			HookOnActionChange();
			int valueSizeInBytes = context.valueSizeInBytes;
			global::UnityEngine.InputSystem.LowLevel.ActionEvent* ptr = (global::UnityEngine.InputSystem.LowLevel.ActionEvent*)m_EventBuffer.AllocateEvent(global::UnityEngine.InputSystem.LowLevel.ActionEvent.GetEventSizeWithValueSize(valueSizeInBytes));
			ref global::UnityEngine.InputSystem.InputActionState.TriggerState reference = ref context.m_State.actionStates[context.m_ActionIndex];
			ptr->baseEvent.type = global::UnityEngine.InputSystem.LowLevel.ActionEvent.Type;
			ptr->baseEvent.time = reference.time;
			ptr->stateIndex = num;
			ptr->controlIndex = reference.controlIndex;
			ptr->bindingIndex = reference.bindingIndex;
			ptr->interactionIndex = reference.interactionIndex;
			ptr->startTime = reference.startTime;
			ptr->phase = reference.phase;
			byte* valueData = ptr->valueData;
			context.ReadValue(valueData, valueSizeInBytes);
		}

		public void Clear()
		{
			m_EventBuffer.Reset();
			m_ActionMapStates.ClearWithCapacity();
		}

		~InputActionTrace()
		{
			DisposeInternal();
		}

		public override string ToString()
		{
			if (count == 0)
			{
				return "[]";
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append('[');
			bool flag = true;
			using (global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr current = enumerator.Current;
					if (!flag)
					{
						stringBuilder.Append(",\n");
					}
					stringBuilder.Append(current.ToString());
					flag = false;
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public void Dispose()
		{
			UnsubscribeFromAll();
			DisposeInternal();
		}

		private void DisposeInternal()
		{
			for (int i = 0; i < m_ActionMapStateClones.length; i++)
			{
				m_ActionMapStateClones[i].Dispose();
			}
			m_EventBuffer.Dispose();
			m_ActionMapStates.Clear();
			m_ActionMapStateClones.Clear();
			if (m_ActionChangeDelegate != null)
			{
				global::UnityEngine.InputSystem.InputSystem.onActionChange -= m_ActionChangeDelegate;
				m_ActionChangeDelegate = null;
			}
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.Utilities.InputActionTrace.ActionEventPtr> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.Utilities.InputActionTrace.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void HookOnActionChange()
		{
			if (!m_OnActionChangeHooked)
			{
				if (m_ActionChangeDelegate == null)
				{
					m_ActionChangeDelegate = OnActionChange;
				}
				global::UnityEngine.InputSystem.InputSystem.onActionChange += m_ActionChangeDelegate;
				m_OnActionChangeHooked = true;
			}
		}

		private void UnhookOnActionChange()
		{
			if (m_OnActionChangeHooked)
			{
				global::UnityEngine.InputSystem.InputSystem.onActionChange -= m_ActionChangeDelegate;
				m_OnActionChangeHooked = false;
			}
		}

		private void OnActionChange(object actionOrMapOrAsset, global::UnityEngine.InputSystem.InputActionChange change)
		{
			if (m_SubscribedToAll && (uint)(change - 4) <= 2u)
			{
				global::UnityEngine.InputSystem.InputAction obj = (global::UnityEngine.InputSystem.InputAction)actionOrMapOrAsset;
				int actionIndexInState = obj.m_ActionIndexInState;
				global::UnityEngine.InputSystem.InputActionState state = obj.m_ActionMap.m_State;
				global::UnityEngine.InputSystem.InputAction.CallbackContext context = new global::UnityEngine.InputSystem.InputAction.CallbackContext
				{
					m_State = state,
					m_ActionIndex = actionIndexInState
				};
				RecordAction(context);
			}
			else
			{
				if (change != global::UnityEngine.InputSystem.InputActionChange.BoundControlsAboutToChange)
				{
					return;
				}
				if (actionOrMapOrAsset is global::UnityEngine.InputSystem.InputAction inputAction)
				{
					CloneActionStateBeforeBindingsChange(inputAction.m_ActionMap);
				}
				else if (actionOrMapOrAsset is global::UnityEngine.InputSystem.InputActionMap actionMap)
				{
					CloneActionStateBeforeBindingsChange(actionMap);
				}
				else
				{
					if (!(actionOrMapOrAsset is global::UnityEngine.InputSystem.InputActionAsset { actionMaps: var actionMaps }))
					{
						return;
					}
					foreach (global::UnityEngine.InputSystem.InputActionMap item in actionMaps)
					{
						CloneActionStateBeforeBindingsChange(item);
					}
				}
			}
		}

		private void CloneActionStateBeforeBindingsChange(global::UnityEngine.InputSystem.InputActionMap actionMap)
		{
			global::UnityEngine.InputSystem.InputActionState state = actionMap.m_State;
			if (state != null)
			{
				int num = m_ActionMapStates.IndexOfReference(state);
				if (num != -1)
				{
					global::UnityEngine.InputSystem.InputActionState value = state.Clone();
					m_ActionMapStateClones.Append(value);
					m_ActionMapStates[num] = value;
				}
			}
		}
	}
}

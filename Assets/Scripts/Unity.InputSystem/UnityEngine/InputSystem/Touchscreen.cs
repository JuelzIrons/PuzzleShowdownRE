namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.TouchscreenState), isGenericTypeOfDevice = true)]
	public class Touchscreen : global::UnityEngine.InputSystem.Pointer, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver, global::UnityEngine.InputSystem.LowLevel.IEventMerger, global::UnityEngine.InputSystem.LowLevel.ICustomDeviceReset
	{
		private static readonly global::Unity.Profiling.ProfilerMarker k_TouchscreenUpdateMarker = new global::Unity.Profiling.ProfilerMarker("Touchscreen.OnNextUpdate");

		private static readonly global::Unity.Profiling.ProfilerMarker k_TouchAllocateMarker = new global::Unity.Profiling.ProfilerMarker("TouchAllocate");

		internal static float s_TapTime;

		internal static float s_TapDelayTime;

		internal static float s_TapRadiusSquared;

		public global::UnityEngine.InputSystem.Controls.TouchControl primaryTouch { get; protected set; }

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.TouchControl> touches { get; protected set; }

		protected global::UnityEngine.InputSystem.Controls.TouchControl[] touchControlArray
		{
			get
			{
				return touches.m_Array;
			}
			set
			{
				touches = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.TouchControl>(value);
			}
		}

		public new static global::UnityEngine.InputSystem.Touchscreen current { get; internal set; }

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
		}

		protected override void FinishSetup()
		{
			base.FinishSetup();
			primaryTouch = GetChildControl<global::UnityEngine.InputSystem.Controls.TouchControl>("primaryTouch");
			int num = 0;
			foreach (global::UnityEngine.InputSystem.InputControl child in base.children)
			{
				if (child is global::UnityEngine.InputSystem.Controls.TouchControl)
				{
					num++;
				}
			}
			if (num >= 1)
			{
				num--;
			}
			global::UnityEngine.InputSystem.Controls.TouchControl[] array = new global::UnityEngine.InputSystem.Controls.TouchControl[num];
			int num2 = 0;
			foreach (global::UnityEngine.InputSystem.InputControl child2 in base.children)
			{
				if (child2 != primaryTouch && child2 is global::UnityEngine.InputSystem.Controls.TouchControl touchControl)
				{
					array[num2++] = touchControl;
				}
			}
			touches = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Controls.TouchControl>(array);
		}

		protected new unsafe void OnNextUpdate()
		{
			void* ptr = base.currentStatePtr;
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)ptr + base.stateBlock.byteOffset + 56);
			int num = 0;
			while (num < touches.Count)
			{
				if (ptr2->delta != default(global::UnityEngine.Vector2))
				{
					global::UnityEngine.InputSystem.LowLevel.InputState.Change(touches[num].delta, global::UnityEngine.Vector2.zero);
				}
				if (ptr2->tapCount > 0 && global::UnityEngine.InputSystem.LowLevel.InputState.currentTime >= ptr2->startTime + (double)s_TapTime + (double)s_TapDelayTime)
				{
					global::UnityEngine.InputSystem.LowLevel.InputState.Change((global::UnityEngine.InputSystem.InputControl)touches[num].tapCount, (byte)0, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr));
				}
				num++;
				ptr2++;
			}
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr3 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)ptr + base.stateBlock.byteOffset);
			if (ptr3->delta != default(global::UnityEngine.Vector2))
			{
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch.delta, global::UnityEngine.Vector2.zero);
			}
			if (ptr3->tapCount > 0 && global::UnityEngine.InputSystem.LowLevel.InputState.currentTime >= ptr3->startTime + (double)s_TapTime + (double)s_TapDelayTime)
			{
				global::UnityEngine.InputSystem.LowLevel.InputState.Change((global::UnityEngine.InputSystem.InputControl)primaryTouch.tapCount, (byte)0, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr));
			}
		}

		protected new unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.LowLevel.TouchState.Format)
			{
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(this, eventPtr);
				return;
			}
			void* num = base.currentStatePtr;
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)num + touches[0].stateBlock.byteOffset);
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr3 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)num + primaryTouch.stateBlock.byteOffset);
			int count = touches.Count;
			global::UnityEngine.InputSystem.LowLevel.TouchState output;
			if (ptr->stateSizeInBytes == 56)
			{
				output = *(global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr->state;
			}
			else
			{
				output = default(global::UnityEngine.InputSystem.LowLevel.TouchState);
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), ptr->state, ptr->stateSizeInBytes);
			}
			output.tapCount = 0;
			output.isTapPress = false;
			output.isTapRelease = false;
			output.updateStepCount = global::UnityEngine.InputSystem.LowLevel.InputUpdate.s_UpdateStepCount;
			if (output.phase != global::UnityEngine.InputSystem.TouchPhase.Began)
			{
				int touchId = output.touchId;
				for (int i = 0; i < count; i++)
				{
					if (ptr2[i].touchId != touchId)
					{
						continue;
					}
					bool flag = (output.isPrimaryTouch = ptr2[i].isPrimaryTouch);
					if (output.delta == default(global::UnityEngine.Vector2))
					{
						output.delta = output.position - ptr2[i].position;
					}
					output.delta += ptr2[i].delta;
					output.startTime = ptr2[i].startTime;
					output.startPosition = ptr2[i].startPosition;
					bool flag2 = output.isNoneEndedOrCanceled && eventPtr.time - output.startTime <= (double)s_TapTime && (output.position - output.startPosition).sqrMagnitude <= s_TapRadiusSquared;
					if (flag2)
					{
						output.tapCount = (byte)(ptr2[i].tapCount + 1);
					}
					else
					{
						output.tapCount = ptr2[i].tapCount;
					}
					if (flag)
					{
						if (output.isNoneEndedOrCanceled)
						{
							output.isPrimaryTouch = false;
							bool flag3 = false;
							for (int j = 0; j < count; j++)
							{
								if (j != i && ptr2[j].isInProgress)
								{
									flag3 = true;
									break;
								}
							}
							if (!flag3)
							{
								if (flag2)
								{
									TriggerTap(primaryTouch, ref output, eventPtr);
								}
								else
								{
									global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch, ref output, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
								}
							}
							else
							{
								global::UnityEngine.InputSystem.LowLevel.TouchState state = output;
								state.phase = global::UnityEngine.InputSystem.TouchPhase.Moved;
								state.isOrphanedPrimaryTouch = true;
								global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch, ref state, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
							}
						}
						else
						{
							global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch, ref output, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
						}
					}
					else if (output.isNoneEndedOrCanceled && ptr3->isOrphanedPrimaryTouch)
					{
						bool flag4 = false;
						for (int k = 0; k < count; k++)
						{
							if (k != i && ptr2[k].isInProgress)
							{
								flag4 = true;
								break;
							}
						}
						if (!flag4)
						{
							ptr3->isOrphanedPrimaryTouch = false;
							global::UnityEngine.InputSystem.LowLevel.InputState.Change((global::UnityEngine.InputSystem.InputControl)primaryTouch.phase, (byte)3, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, default(global::UnityEngine.InputSystem.LowLevel.InputEventPtr));
						}
					}
					if (flag2)
					{
						TriggerTap(touches[i], ref output, eventPtr);
					}
					else
					{
						global::UnityEngine.InputSystem.LowLevel.InputState.Change(touches[i], ref output, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
					}
					break;
				}
				return;
			}
			int num2 = 0;
			while (num2 < count)
			{
				if (ptr2->isNoneEndedOrCanceled)
				{
					output.delta = global::UnityEngine.Vector2.zero;
					output.startTime = eventPtr.time;
					output.startPosition = output.position;
					output.isPrimaryTouch = false;
					output.isOrphanedPrimaryTouch = false;
					output.isTap = false;
					output.tapCount = ptr2->tapCount;
					if (ptr3->isNoneEndedOrCanceled)
					{
						output.isPrimaryTouch = true;
						global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch, ref output, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
					}
					global::UnityEngine.InputSystem.LowLevel.InputState.Change(touches[num2], ref output, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
					break;
				}
				num2++;
				ptr2++;
			}
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
		{
			OnNextUpdate();
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			OnStateEvent(eventPtr);
		}

		unsafe bool global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.GetStateOffsetForEvent(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, ref uint offset)
		{
			if (!eventPtr.IsA<global::UnityEngine.InputSystem.LowLevel.StateEvent>())
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.LowLevel.TouchState.Format)
			{
				return false;
			}
			if (control == null)
			{
				global::UnityEngine.InputSystem.LowLevel.TouchState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)base.currentStatePtr + touches[0].stateBlock.byteOffset);
				global::UnityEngine.InputSystem.LowLevel.TouchState* state = (global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr->state;
				int touchId = state->touchId;
				global::UnityEngine.InputSystem.TouchPhase phase = state->phase;
				int count = touches.Count;
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.InputSystem.LowLevel.TouchState* ptr3 = ptr2 + i;
					if (ptr3->touchId == touchId || (!ptr3->isInProgress && phase.IsActive()))
					{
						offset = primaryTouch.m_StateBlock.byteOffset + primaryTouch.m_StateBlock.alignedSizeInBytes - m_StateBlock.byteOffset + (uint)(i * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.TouchState>());
						return true;
					}
				}
				return false;
			}
			global::UnityEngine.InputSystem.Controls.TouchControl touchControl = control.FindInParentChain<global::UnityEngine.InputSystem.Controls.TouchControl>();
			if (touchControl == null || touchControl.parent != this)
			{
				return false;
			}
			if (touchControl != primaryTouch)
			{
				return false;
			}
			offset = touchControl.stateBlock.byteOffset - m_StateBlock.byteOffset;
			return true;
		}

		unsafe void global::UnityEngine.InputSystem.LowLevel.ICustomDeviceReset.Reset()
		{
			void* ptr = base.currentStatePtr;
			using global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(global::UnityEngine.InputSystem.LowLevel.StateEvent.GetEventSizeWithPayload<global::UnityEngine.InputSystem.LowLevel.TouchState>(), global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.InputSystem.LowLevel.StateEvent* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.StateEvent*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			unsafePtr->baseEvent = new global::UnityEngine.InputSystem.LowLevel.InputEvent(1398030676, nativeArray.Length, base.deviceId);
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)ptr + primaryTouch.stateBlock.byteOffset);
			if (ptr2->phase.IsActive())
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr->state, ptr2, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.TouchState>());
				((global::UnityEngine.InputSystem.LowLevel.TouchState*)unsafePtr->state)->phase = global::UnityEngine.InputSystem.TouchPhase.Canceled;
				global::UnityEngine.InputSystem.LowLevel.InputState.Change(primaryTouch.phase, global::UnityEngine.InputSystem.TouchPhase.Canceled, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)unsafePtr));
			}
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr3 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)ptr + touches[0].stateBlock.byteOffset);
			int count = touches.Count;
			for (int i = 0; i < count; i++)
			{
				if (ptr3[i].phase.IsActive())
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(unsafePtr->state, ptr3 + i, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.TouchState>());
					((global::UnityEngine.InputSystem.LowLevel.TouchState*)unsafePtr->state)->phase = global::UnityEngine.InputSystem.TouchPhase.Canceled;
					global::UnityEngine.InputSystem.LowLevel.InputState.Change(touches[i].phase, global::UnityEngine.InputSystem.TouchPhase.Canceled, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)unsafePtr));
				}
			}
		}

		internal unsafe static bool MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(currentEventPtr);
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr2 = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.LowLevel.TouchState.Format || ptr2->stateFormat != global::UnityEngine.InputSystem.LowLevel.TouchState.Format)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.TouchState* state = (global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr->state;
			global::UnityEngine.InputSystem.LowLevel.TouchState* state2 = (global::UnityEngine.InputSystem.LowLevel.TouchState*)ptr2->state;
			if (state->touchId != state2->touchId || state->phaseId != state2->phaseId || state->flags != state2->flags)
			{
				return false;
			}
			state2->delta += state->delta;
			return true;
		}

		bool global::UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
		{
			return MergeForward(currentEventPtr, nextEventPtr);
		}

		private static void TriggerTap(global::UnityEngine.InputSystem.Controls.TouchControl control, ref global::UnityEngine.InputSystem.LowLevel.TouchState state, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			state.isTapPress = true;
			state.isTapRelease = false;
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(control, ref state, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
			state.isTapPress = false;
			state.isTapRelease = true;
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(control, ref state, global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None, eventPtr);
			state.isTapRelease = false;
		}
	}
}

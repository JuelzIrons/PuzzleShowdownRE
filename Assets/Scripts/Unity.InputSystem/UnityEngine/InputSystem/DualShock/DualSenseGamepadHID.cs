namespace UnityEngine.InputSystem.DualShock
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport), displayName = "DualSense HID")]
	public class DualSenseGamepadHID : global::UnityEngine.InputSystem.DualShock.DualShockGamepad, global::UnityEngine.InputSystem.LowLevel.IEventMerger, global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct DualSenseHIDGenericInputReport
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D');
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct DualSenseHIDUSBInputReport
		{
			public const int ExpectedReportId = 1;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte leftStickX;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte leftStickY;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte rightStickX;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte rightStickY;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte leftTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte rightTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(9)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(10)]
			public byte buttons2;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
			{
				return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY,
					leftTrigger = leftTrigger,
					rightTrigger = rightTrigger,
					buttons0 = buttons0,
					buttons1 = buttons1,
					buttons2 = (byte)(buttons2 & 7)
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct DualSenseHIDBluetoothInputReport
		{
			public const int ExpectedReportId = 49;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte leftStickX;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte leftStickY;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte rightStickX;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte rightStickY;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte leftTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(7)]
			public byte rightTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(9)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(10)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(11)]
			public byte buttons2;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
			{
				return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY,
					leftTrigger = leftTrigger,
					rightTrigger = rightTrigger,
					buttons0 = buttons0,
					buttons1 = buttons1,
					buttons2 = (byte)(buttons2 & 7)
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct DualSenseHIDMinimalInputReport
		{
			public static int ExpectedSize1 = 10;

			public static int ExpectedSize2 = 78;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte leftStickX;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte leftStickY;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte rightStickX;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte rightStickY;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(7)]
			public byte buttons2;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public byte leftTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(9)]
			public byte rightTrigger;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport ToHIDInputReport()
			{
				return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY,
					leftTrigger = leftTrigger,
					rightTrigger = rightTrigger,
					buttons0 = buttons0,
					buttons1 = buttons1,
					buttons2 = (byte)(buttons2 & 3)
				};
			}
		}

		private float? m_LowFrequencyMotorSpeed;

		private float? m_HighFrequenceyMotorSpeed;

		protected global::UnityEngine.Color? m_LightBarColor;

		private byte outputSequenceId;

		internal const byte JitterMaskLow = 120;

		internal const byte JitterMaskHigh = 135;

		public global::UnityEngine.InputSystem.Controls.ButtonControl leftTriggerButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightTriggerButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl playStationButton { get; protected set; }

		protected override void FinishSetup()
		{
			leftTriggerButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftTriggerButton");
			rightTriggerButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightTriggerButton");
			playStationButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("systemButton");
			base.FinishSetup();
		}

		public override void PauseHaptics()
		{
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue)
			{
				SetMotorSpeedsAndLightBarColor(0f, 0f, m_LightBarColor);
			}
		}

		public override void ResetHaptics()
		{
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue)
			{
				m_HighFrequenceyMotorSpeed = null;
				m_LowFrequencyMotorSpeed = null;
				SetMotorSpeedsAndLightBarColor(m_LowFrequencyMotorSpeed, m_HighFrequenceyMotorSpeed, m_LightBarColor);
			}
		}

		public override void ResumeHaptics()
		{
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue)
			{
				SetMotorSpeedsAndLightBarColor(m_LowFrequencyMotorSpeed, m_HighFrequenceyMotorSpeed, m_LightBarColor);
			}
		}

		public override void SetLightBarColor(global::UnityEngine.Color color)
		{
			m_LightBarColor = color;
			SetMotorSpeedsAndLightBarColor(m_LowFrequencyMotorSpeed, m_HighFrequenceyMotorSpeed, m_LightBarColor);
		}

		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			m_LowFrequencyMotorSpeed = lowFrequency;
			m_HighFrequenceyMotorSpeed = highFrequency;
			SetMotorSpeedsAndLightBarColor(m_LowFrequencyMotorSpeed, m_HighFrequenceyMotorSpeed, m_LightBarColor);
		}

		public bool SetMotorSpeedsAndLightBarColor(float? lowFrequency, float? highFrequency, global::UnityEngine.Color? color)
		{
			float value = (lowFrequency.HasValue ? lowFrequency.Value : 0f);
			float value2 = (highFrequency.HasValue ? highFrequency.Value : 0f);
			global::UnityEngine.Color color2 = (color.HasValue ? color.Value : global::UnityEngine.Color.black);
			global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDUSBOutputReport.Create(new global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDOutputReportPayload
			{
				enableFlags1 = 3,
				enableFlags2 = 4,
				lowFrequencyMotorSpeed = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value, 0u, 255u),
				highFrequencyMotorSpeed = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(value2, 0u, 255u),
				redColor = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(color2.r, 0u, 255u),
				greenColor = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(color2.g, 0u, 255u),
				blueColor = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.NormalizedFloatToUInt(color2.b, 0u, 255u)
			}, base.hidDescriptor.outputReportSize);
			return ExecuteCommand(ref command) >= 0;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState, global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState)
		{
			if (currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1)
			{
				return currentState->buttons2 == nextState->buttons2;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState, global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState)
		{
			if (currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1)
			{
				return currentState->buttons2 == nextState->buttons2;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState, global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState)
		{
			if (currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1)
			{
				return currentState->buttons2 == nextState->buttons2;
			}
			return false;
		}

		unsafe bool global::UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(currentEventPtr);
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr2 = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || ptr2->stateFormat != global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format)
			{
				return false;
			}
			if (ptr->stateSizeInBytes != ptr2->stateSizeInBytes)
			{
				return false;
			}
			global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport* state2 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr2->state;
			if (state->reportId != state2->reportId)
			{
				return false;
			}
			if (state->reportId == 1)
			{
				if (ptr->stateSizeInBytes == global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1 || ptr->stateSizeInBytes == global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2)
				{
					global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state3 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state;
					global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport* state4 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr2->state;
					return MergeForward(state3, state4);
				}
				global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* state5 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state;
				global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport* state6 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr2->state;
				return MergeForward(state5, state6);
			}
			if (state->reportId == 49)
			{
				global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state7 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state;
				global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* state8 = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr2->state;
				return MergeForward(state7, state8);
			}
			return false;
		}

		unsafe bool global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || stateSizeInBytes < sizeof(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport))
			{
				return false;
			}
			global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport* state = (global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDGenericInputReport*)ptr->state;
			if (state->reportId == 1)
			{
				if (ptr->stateSizeInBytes == global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1 || ptr->stateSizeInBytes == global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2)
				{
					global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport dualSenseHIDInputReport = ((global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)ptr->state)->ToHIDInputReport();
					*(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport;
				}
				else
				{
					global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport dualSenseHIDInputReport2 = ((global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDUSBInputReport*)ptr->state)->ToHIDInputReport();
					*(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport2;
				}
				ptr->stateFormat = global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport.Format;
				return true;
			}
			if (state->reportId == 49)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport dualSenseHIDInputReport3 = ((global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)ptr->state)->ToHIDInputReport();
				*(global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport*)ptr->state = dualSenseHIDInputReport3;
				ptr->stateFormat = global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport.Format;
				return true;
			}
			return false;
		}

		public void OnNextUpdate()
		{
		}

		public unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport.Format)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport* ptr = (global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport*)((byte*)base.currentStatePtr + m_StateBlock.byteOffset);
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport* state = (global::UnityEngine.InputSystem.DualShock.LowLevel.DualSenseHIDInputReport*)global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons0 == ptr->buttons0 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
				{
					global::UnityEngine.InputSystem.InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(this, eventPtr);
		}

		public bool GetStateOffsetForEvent(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}
	}
}

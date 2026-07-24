namespace UnityEngine.InputSystem.DualShock
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport), hideInUI = true, isNoisy = true)]
	public class DualShock4GamepadHID : global::UnityEngine.InputSystem.DualShock.DualShockGamepad, global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		internal struct DualShock4HIDGenericInputReport
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte leftStickX;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte leftStickY;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte rightStickX;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte rightStickY;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte buttons2;

			[global::System.Runtime.InteropServices.FieldOffset(7)]
			public byte leftTrigger;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public byte rightTrigger;

			public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D');

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport ToHIDInputReport()
			{
				return new global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY,
					leftTrigger = leftTrigger,
					rightTrigger = rightTrigger,
					buttons1 = buttons0,
					buttons2 = buttons1,
					buttons3 = buttons2
				};
			}
		}

		private float? m_LowFrequencyMotorSpeed;

		private float? m_HighFrequenceyMotorSpeed;

		private global::UnityEngine.Color? m_LightBarColor;

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
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue || m_LightBarColor.HasValue)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
				command.SetMotorSpeeds(0f, 0f);
				if (m_LightBarColor.HasValue)
				{
					command.SetColor(global::UnityEngine.Color.black);
				}
				ExecuteCommand(ref command);
			}
		}

		public override void ResetHaptics()
		{
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue || m_LightBarColor.HasValue)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
				command.SetMotorSpeeds(0f, 0f);
				if (m_LightBarColor.HasValue)
				{
					command.SetColor(global::UnityEngine.Color.black);
				}
				ExecuteCommand(ref command);
				m_HighFrequenceyMotorSpeed = null;
				m_LowFrequencyMotorSpeed = null;
				m_LightBarColor = null;
			}
		}

		public override void ResumeHaptics()
		{
			if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue || m_LightBarColor.HasValue)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
				if (m_LowFrequencyMotorSpeed.HasValue || m_HighFrequenceyMotorSpeed.HasValue)
				{
					command.SetMotorSpeeds(m_LowFrequencyMotorSpeed.Value, m_HighFrequenceyMotorSpeed.Value);
				}
				if (m_LightBarColor.HasValue)
				{
					command.SetColor(m_LightBarColor.Value);
				}
				ExecuteCommand(ref command);
			}
		}

		public override void SetLightBarColor(global::UnityEngine.Color color)
		{
			global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetColor(color);
			ExecuteCommand(ref command);
			m_LightBarColor = color;
		}

		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(lowFrequency, highFrequency);
			ExecuteCommand(ref command);
			m_LowFrequencyMotorSpeed = lowFrequency;
			m_HighFrequenceyMotorSpeed = highFrequency;
		}

		public bool SetMotorSpeedsAndLightBarColor(float lowFrequency, float highFrequency, global::UnityEngine.Color color)
		{
			global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport command = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(lowFrequency, highFrequency);
			command.SetColor(color);
			long num = ExecuteCommand(ref command);
			m_LowFrequencyMotorSpeed = lowFrequency;
			m_HighFrequenceyMotorSpeed = highFrequency;
			m_LightBarColor = color;
			return num >= 0;
		}

		unsafe bool global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor.PreProcessEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat == global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport.Format)
			{
				return true;
			}
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat != global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport.Format || stateSizeInBytes < sizeof(global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport))
			{
				return false;
			}
			byte* state = (byte*)ptr->state;
			switch (*state)
			{
			case 1:
			{
				if (stateSizeInBytes < sizeof(global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 1)
				{
					return false;
				}
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport dualShock4HIDInputReport2 = ((global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 1))->ToHIDInputReport();
				*(global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport2;
				ptr->stateFormat = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport.Format;
				return true;
			}
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
				if ((state[1] & 0x80) != 0)
				{
					if (stateSizeInBytes < sizeof(global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 3)
					{
						return false;
					}
					global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport dualShock4HIDInputReport = ((global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(state + 3))->ToHIDInputReport();
					*(global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport*)ptr->state = dualShock4HIDInputReport;
					ptr->stateFormat = global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport.Format;
					return true;
				}
				return false;
			default:
				return false;
			}
		}

		public void OnNextUpdate()
		{
		}

		public unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport.Format)
			{
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport* ptr = (global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport*)((byte*)base.currentStatePtr + m_StateBlock.byteOffset);
				global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport* state = (global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock4HIDInputReport*)global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->leftTrigger == ptr->leftTrigger && state->rightTrigger == ptr->rightTrigger && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2 && state->buttons3 == ptr->buttons3)
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

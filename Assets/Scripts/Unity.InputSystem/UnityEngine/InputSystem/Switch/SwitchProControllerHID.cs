namespace UnityEngine.InputSystem.Switch
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState), displayName = "Switch Pro Controller")]
	public class SwitchProControllerHID : global::UnityEngine.InputSystem.Gamepad, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver, global::UnityEngine.InputSystem.LowLevel.IEventPreProcessor
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 7)]
		private struct SwitchInputOnlyReport
		{
			public const int kSize = 7;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte hat;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte leftX;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte leftY;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte rightX;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte rightY;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
			{
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState result = new global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState
				{
					leftStickX = leftX,
					leftStickY = leftY,
					rightStickX = rightX,
					rightStickY = rightY
				};
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.West, (buttons0 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.South, (buttons0 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.East, (buttons0 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.North, (buttons0 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.L, (buttons0 & 0x10) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.R, (buttons0 & 0x20) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZL, (buttons0 & 0x40) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZR, (buttons0 & 0x80) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Minus, (buttons1 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Plus, (buttons1 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickL, (buttons1 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickR, (buttons1 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Home, (buttons1 & 0x10) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Capture, (buttons1 & 0x20) != 0);
				bool state = false;
				bool state2 = false;
				bool state3 = false;
				bool state4 = false;
				switch (hat)
				{
				case 0:
					state2 = true;
					break;
				case 1:
					state2 = true;
					state3 = true;
					break;
				case 2:
					state3 = true;
					break;
				case 3:
					state4 = true;
					state3 = true;
					break;
				case 4:
					state4 = true;
					break;
				case 5:
					state4 = true;
					state = true;
					break;
				case 6:
					state = true;
					break;
				case 7:
					state2 = true;
					state = true;
					break;
				}
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Left, state);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Up, state2);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Right, state3);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Down, state4);
				return result;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 12)]
		private struct SwitchSimpleInputReport
		{
			public const int kSize = 12;

			public const byte ExpectedReportId = 63;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(2)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte hat;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public ushort leftX;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public ushort leftY;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public ushort rightX;

			[global::System.Runtime.InteropServices.FieldOffset(10)]
			public ushort rightY;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
			{
				byte leftStickX = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(leftX, 16u, 8u);
				byte leftStickY = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(leftY, 16u, 8u);
				byte rightStickX = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(rightX, 16u, 8u);
				byte rightStickY = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(rightY, 16u, 8u);
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState result = new global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY
				};
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.South, (buttons0 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.East, (buttons0 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.West, (buttons0 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.North, (buttons0 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.L, (buttons0 & 0x10) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.R, (buttons0 & 0x20) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZL, (buttons0 & 0x40) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZR, (buttons0 & 0x80) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Minus, (buttons1 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Plus, (buttons1 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickL, (buttons1 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickR, (buttons1 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Home, (buttons1 & 0x10) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Capture, (buttons1 & 0x20) != 0);
				bool state = false;
				bool state2 = false;
				bool state3 = false;
				bool state4 = false;
				switch (hat)
				{
				case 0:
					state2 = true;
					break;
				case 1:
					state2 = true;
					state3 = true;
					break;
				case 2:
					state3 = true;
					break;
				case 3:
					state4 = true;
					state3 = true;
					break;
				case 4:
					state4 = true;
					break;
				case 5:
					state4 = true;
					state = true;
					break;
				case 6:
					state = true;
					break;
				case 7:
					state2 = true;
					state = true;
					break;
				}
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Left, state);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Up, state2);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Right, state3);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Down, state4);
				return result;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 25)]
		private struct SwitchFullInputReport
		{
			public const int kSize = 25;

			public const byte ExpectedReportId = 48;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			[global::System.Runtime.InteropServices.FieldOffset(3)]
			public byte buttons0;

			[global::System.Runtime.InteropServices.FieldOffset(4)]
			public byte buttons1;

			[global::System.Runtime.InteropServices.FieldOffset(5)]
			public byte buttons2;

			[global::System.Runtime.InteropServices.FieldOffset(6)]
			public byte left0;

			[global::System.Runtime.InteropServices.FieldOffset(7)]
			public byte left1;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public byte left2;

			[global::System.Runtime.InteropServices.FieldOffset(9)]
			public byte right0;

			[global::System.Runtime.InteropServices.FieldOffset(10)]
			public byte right1;

			[global::System.Runtime.InteropServices.FieldOffset(11)]
			public byte right2;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState ToHIDInputReport()
			{
				uint value = (uint)(left0 | ((left1 & 0xF) << 8));
				uint value2 = (uint)(((left1 & 0xF0) >> 4) | (left2 << 4));
				int value3 = right0 | ((right1 & 0xF) << 8);
				uint value4 = (uint)(((right1 & 0xF0) >> 4) | (right2 << 4));
				byte leftStickX = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(value, 12u, 8u);
				byte leftStickY = (byte)(255 - (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(value2, 12u, 8u));
				byte rightStickX = (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)value3, 12u, 8u);
				byte rightStickY = (byte)(255 - (byte)global::UnityEngine.InputSystem.Utilities.NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(value4, 12u, 8u));
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState result = new global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState
				{
					leftStickX = leftStickX,
					leftStickY = leftStickY,
					rightStickX = rightStickX,
					rightStickY = rightStickY
				};
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.West, (buttons0 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.North, (buttons0 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.South, (buttons0 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.East, (buttons0 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.R, (buttons0 & 0x40) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZR, (buttons0 & 0x80) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Minus, (buttons1 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Plus, (buttons1 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickR, (buttons1 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.StickL, (buttons1 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Home, (buttons1 & 0x10) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Capture, (buttons1 & 0x20) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Down, (buttons2 & 1) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Up, (buttons2 & 2) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Right, (buttons2 & 4) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.Left, (buttons2 & 8) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.L, (buttons2 & 0x40) != 0);
				result.Set(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Button.ZL, (buttons2 & 0x80) != 0);
				return result;
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		private struct SwitchHIDGenericInputReport
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportId;

			public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D');
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 49)]
		internal struct SwitchMagicOutputReport
		{
			internal enum ReportType
			{
				Magic = 0x80
			}

			public enum CommandIdType
			{
				Status = 1,
				Handshake = 2,
				Highspeed = 3,
				ForceUSB = 4
			}

			public const int kSize = 49;

			public const byte ExpectedReplyInputReportId = 129;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public byte reportType;

			[global::System.Runtime.InteropServices.FieldOffset(1)]
			public byte commandId;
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 57)]
		internal struct SwitchMagicOutputHIDBluetooth : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
		{
			public const int kSize = 57;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport report;

			public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'O');

			public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

			public static global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDBluetooth Create(global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDBluetooth
				{
					baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 57),
					report = new global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 72)]
		internal struct SwitchMagicOutputHIDUSB : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
		{
			public const int kSize = 72;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

			[global::System.Runtime.InteropServices.FieldOffset(8)]
			public global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport report;

			public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'O');

			public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

			public static global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDUSB Create(global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDUSB
				{
					baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type, 72),
					report = new global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}
		}

		private static readonly global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[] s_HandshakeSequence = new global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[5]
		{
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Status,
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Highspeed,
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.ForceUSB
		};

		private int m_HandshakeStepIndex;

		private double m_HandshakeTimer;

		internal const byte JitterMaskLow = 120;

		internal const byte JitterMaskHigh = 135;

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "capture", displayName = "Capture")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl captureButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "home", displayName = "Home")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl homeButton { get; protected set; }

		protected override void OnAdded()
		{
			base.OnAdded();
			captureButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("capture");
			homeButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("home");
			HandshakeRestart();
		}

		private void HandshakeRestart()
		{
			m_HandshakeStepIndex = -1;
			m_HandshakeTimer = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
		}

		private void HandshakeTick()
		{
			double currentTime = global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.currentTime;
			if (currentTime >= m_LastUpdateTimeInternal + 2.0 && currentTime >= m_HandshakeTimer + 2.0)
			{
				m_HandshakeStepIndex = 0;
			}
			else
			{
				if (m_HandshakeStepIndex + 1 >= s_HandshakeSequence.Length || !(currentTime > m_HandshakeTimer + 0.1))
				{
					return;
				}
				m_HandshakeStepIndex++;
			}
			m_HandshakeTimer = currentTime;
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type = s_HandshakeSequence[m_HandshakeStepIndex];
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDBluetooth command = global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Create(type);
			if (ExecuteCommand(ref command) <= 0)
			{
				global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDUSB command2 = global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchMagicOutputHIDUSB.Create(type);
				ExecuteCommand(ref command2);
			}
		}

		public void OnNextUpdate()
		{
			HandshakeTick();
		}

		public unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format)
			{
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState* ptr = (global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState*)((byte*)base.currentStatePtr + m_StateBlock.byteOffset);
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState* state = (global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState*)global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr)->state;
				if (state->leftStickX >= 120 && state->leftStickX <= 135 && state->leftStickY >= 120 && state->leftStickY <= 135 && state->rightStickX >= 120 && state->rightStickX <= 135 && state->rightStickY >= 120 && state->rightStickY <= 135 && state->buttons1 == ptr->buttons1 && state->buttons2 == ptr->buttons2)
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

		public unsafe bool PreProcessEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return global::UnityEngine.InputSystem.LowLevel.DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat == global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format;
			}
			if (eventPtr.type != 1398030676)
			{
				return true;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			uint stateSizeInBytes = ptr->stateSizeInBytes;
			if (ptr->stateFormat == global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format)
			{
				return true;
			}
			if (ptr->stateFormat != global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchHIDGenericInputReport.Format || stateSizeInBytes < sizeof(global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchHIDGenericInputReport))
			{
				return false;
			}
			global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchHIDGenericInputReport* state = (global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchHIDGenericInputReport*)ptr->state;
			if (state->reportId == 63 && stateSizeInBytes >= 12)
			{
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState switchProControllerHIDInputState = ((global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchSimpleInputReport*)ptr->state)->ToHIDInputReport();
				*(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState;
				ptr->stateFormat = global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (state->reportId == 48 && stateSizeInBytes >= 25)
			{
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState switchProControllerHIDInputState2 = ((global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchFullInputReport*)ptr->state)->ToHIDInputReport();
				*(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState2;
				ptr->stateFormat = global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (stateSizeInBytes == 8 || stateSizeInBytes == 9)
			{
				int num = ((stateSizeInBytes == 9) ? 1 : 0);
				global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState switchProControllerHIDInputState3 = ((global::UnityEngine.InputSystem.Switch.SwitchProControllerHID.SwitchInputOnlyReport*)((byte*)ptr->state + num))->ToHIDInputReport();
				*(global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState*)ptr->state = switchProControllerHIDInputState3;
				ptr->stateFormat = global::UnityEngine.InputSystem.Switch.LowLevel.SwitchProControllerHIDInputState.Format;
				return true;
			}
			return false;
		}
	}
}

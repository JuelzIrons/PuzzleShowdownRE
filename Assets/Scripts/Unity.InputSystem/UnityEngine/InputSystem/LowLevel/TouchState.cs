namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 56)]
	public struct TouchState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		internal const int kSizeInBytes = 56;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Touch ID", layout = "Integer", synthetic = true, dontReset = true)]
		public int touchId;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Position", dontReset = true)]
		public global::UnityEngine.Vector2 position;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Delta", layout = "Delta")]
		public global::UnityEngine.Vector2 delta;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Pressure", layout = "Axis")]
		public float pressure;

		[global::System.Runtime.InteropServices.FieldOffset(24)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Radius")]
		public global::UnityEngine.Vector2 radius;

		[global::System.Runtime.InteropServices.FieldOffset(32)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "phase", displayName = "Touch Phase", layout = "TouchPhase", synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "press", displayName = "Touch Contact?", layout = "TouchPress", useStateFrom = "phase")]
		public byte phaseId;

		[global::System.Runtime.InteropServices.FieldOffset(33)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "tapCount", displayName = "Tap Count", layout = "Integer")]
		public byte tapCount;

		[global::System.Runtime.InteropServices.FieldOffset(34)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		public byte displayIndex;

		[global::System.Runtime.InteropServices.FieldOffset(35)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "indirectTouch", displayName = "Indirect Touch?", layout = "Button", bit = 0u, synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "tap", displayName = "Tap", layout = "Button", bit = 4u)]
		public byte flags;

		[global::System.Runtime.InteropServices.FieldOffset(36)]
		internal uint updateStepCount;

		[global::System.Runtime.InteropServices.FieldOffset(40)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Start Time", layout = "Double", synthetic = true)]
		public double startTime;

		[global::System.Runtime.InteropServices.FieldOffset(48)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Start Position", synthetic = true)]
		public global::UnityEngine.Vector2 startPosition;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Format => new global::UnityEngine.InputSystem.Utilities.FourCC('T', 'O', 'U', 'C');

		public global::UnityEngine.InputSystem.TouchPhase phase
		{
			get
			{
				return (global::UnityEngine.InputSystem.TouchPhase)phaseId;
			}
			set
			{
				phaseId = (byte)value;
			}
		}

		public bool isNoneEndedOrCanceled
		{
			get
			{
				if (phase != global::UnityEngine.InputSystem.TouchPhase.None && phase != global::UnityEngine.InputSystem.TouchPhase.Ended)
				{
					return phase == global::UnityEngine.InputSystem.TouchPhase.Canceled;
				}
				return true;
			}
		}

		public bool isInProgress
		{
			get
			{
				if (phase != global::UnityEngine.InputSystem.TouchPhase.Began && phase != global::UnityEngine.InputSystem.TouchPhase.Moved)
				{
					return phase == global::UnityEngine.InputSystem.TouchPhase.Stationary;
				}
				return true;
			}
		}

		public bool isPrimaryTouch
		{
			get
			{
				return (flags & 8) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 8;
				}
				else
				{
					flags &= 247;
				}
			}
		}

		internal bool isOrphanedPrimaryTouch
		{
			get
			{
				return (flags & 0x40) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 64;
				}
				else
				{
					flags &= 191;
				}
			}
		}

		public bool isIndirectTouch
		{
			get
			{
				return (flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 1;
				}
				else
				{
					flags &= 254;
				}
			}
		}

		public bool isTap
		{
			get
			{
				return isTapPress;
			}
			set
			{
				isTapPress = value;
			}
		}

		internal bool isTapPress
		{
			get
			{
				return (flags & 0x10) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 16;
				}
				else
				{
					flags &= 239;
				}
			}
		}

		internal bool isTapRelease
		{
			get
			{
				return (flags & 0x20) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 32;
				}
				else
				{
					flags &= 223;
				}
			}
		}

		internal bool beganInSameFrame
		{
			get
			{
				return (flags & 0x80) != 0;
			}
			set
			{
				if (value)
				{
					flags |= 128;
				}
				else
				{
					flags &= 127;
				}
			}
		}

		public global::UnityEngine.InputSystem.Utilities.FourCC format => Format;

		public override string ToString()
		{
			return $"{{ id={touchId} phase={phase} pos={position} delta={delta} pressure={pressure} radius={radius} primary={isPrimaryTouch} }}";
		}
	}
}

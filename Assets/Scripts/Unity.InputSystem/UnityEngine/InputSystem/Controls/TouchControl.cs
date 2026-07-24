namespace UnityEngine.InputSystem.Controls
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.TouchState))]
	public class TouchControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.InputSystem.LowLevel.TouchState>
	{
		public global::UnityEngine.InputSystem.Controls.TouchPressControl press { get; set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl displayIndex { get; set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl touchId { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control position { get; set; }

		public global::UnityEngine.InputSystem.Controls.DeltaControl delta { get; set; }

		public global::UnityEngine.InputSystem.Controls.AxisControl pressure { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control radius { get; set; }

		public global::UnityEngine.InputSystem.Controls.TouchPhaseControl phase { get; set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl indirectTouch { get; set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl tap { get; set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl tapCount { get; set; }

		public global::UnityEngine.InputSystem.Controls.DoubleControl startTime { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control startPosition { get; set; }

		public bool isInProgress
		{
			get
			{
				global::UnityEngine.InputSystem.TouchPhase touchPhase = phase.value;
				if ((uint)(touchPhase - 1) <= 1u || touchPhase == global::UnityEngine.InputSystem.TouchPhase.Stationary)
				{
					return true;
				}
				return false;
			}
		}

		public TouchControl()
		{
			m_StateBlock.format = new global::UnityEngine.InputSystem.Utilities.FourCC('T', 'O', 'U', 'C');
		}

		protected override void FinishSetup()
		{
			press = GetChildControl<global::UnityEngine.InputSystem.Controls.TouchPressControl>("press");
			displayIndex = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("displayIndex");
			touchId = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("touchId");
			position = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("position");
			delta = GetChildControl<global::UnityEngine.InputSystem.Controls.DeltaControl>("delta");
			pressure = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("pressure");
			radius = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("radius");
			phase = GetChildControl<global::UnityEngine.InputSystem.Controls.TouchPhaseControl>("phase");
			indirectTouch = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("indirectTouch");
			tap = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("tap");
			tapCount = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("tapCount");
			startTime = GetChildControl<global::UnityEngine.InputSystem.Controls.DoubleControl>("startTime");
			startPosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("startPosition");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.InputSystem.LowLevel.TouchState ReadUnprocessedValueFromState(void* statePtr)
		{
			global::UnityEngine.InputSystem.LowLevel.TouchState* ptr = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			return *ptr;
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.InputSystem.LowLevel.TouchState value, void* statePtr)
		{
			global::UnityEngine.InputSystem.LowLevel.TouchState* destination = (global::UnityEngine.InputSystem.LowLevel.TouchState*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref value), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.InputSystem.LowLevel.TouchState>());
		}
	}
}

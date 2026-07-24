namespace UnityEngine.InputSystem.Controls
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(hideInUI = true)]
	public class TouchPhaseControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.InputSystem.TouchPhase>
	{
		public TouchPhaseControl()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInt;
		}

		public unsafe override global::UnityEngine.InputSystem.TouchPhase ReadUnprocessedValueFromState(void* statePtr)
		{
			return (global::UnityEngine.InputSystem.TouchPhase)base.stateBlock.ReadInt(statePtr);
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.InputSystem.TouchPhase value, void* statePtr)
		{
			*(global::UnityEngine.InputSystem.TouchPhase*)((byte*)statePtr + (int)m_StateBlock.byteOffset) = value;
		}
	}
}

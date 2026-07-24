namespace UnityEngine.InputSystem.Controls
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(hideInUI = true)]
	public class AnyKeyControl : global::UnityEngine.InputSystem.Controls.ButtonControl
	{
		public AnyKeyControl()
		{
			m_StateBlock.sizeInBits = 1u;
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit;
		}

		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			if (!this.CheckStateIsAtDefault(statePtr, null))
			{
				return 1f;
			}
			return 0f;
		}
	}
}

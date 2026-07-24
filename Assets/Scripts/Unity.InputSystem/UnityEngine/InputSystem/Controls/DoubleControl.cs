namespace UnityEngine.InputSystem.Controls
{
	public class DoubleControl : global::UnityEngine.InputSystem.InputControl<double>
	{
		public DoubleControl()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatDouble;
		}

		public unsafe override double ReadUnprocessedValueFromState(void* statePtr)
		{
			return m_StateBlock.ReadDouble(statePtr);
		}

		public unsafe override void WriteValueIntoState(double value, void* statePtr)
		{
			m_StateBlock.WriteDouble(statePtr, value);
		}
	}
}

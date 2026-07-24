namespace UnityEngine.InputSystem.Controls
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(hideInUI = true)]
	public class TouchPressControl : global::UnityEngine.InputSystem.Controls.ButtonControl
	{
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!global::UnityEngine.InputSystem.LowLevel.InputState.IsIntegerFormat(base.stateBlock.format))
			{
				throw new global::System.NotSupportedException($"Non-integer format '{base.stateBlock.format}' is not supported for TouchButtonControl '{this}'");
			}
		}

		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			global::UnityEngine.InputSystem.TouchPhase touchPhase = (global::UnityEngine.InputSystem.TouchPhase)global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ReadMultipleBitsAsUInt((byte*)statePtr + (int)m_StateBlock.byteOffset, m_StateBlock.bitOffset, m_StateBlock.sizeInBits);
			float num = 0f;
			if (touchPhase == global::UnityEngine.InputSystem.TouchPhase.Began || touchPhase == global::UnityEngine.InputSystem.TouchPhase.Stationary || touchPhase == global::UnityEngine.InputSystem.TouchPhase.Moved)
			{
				num = 1f;
			}
			return Preprocess(num);
		}

		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			throw new global::System.NotSupportedException();
		}
	}
}

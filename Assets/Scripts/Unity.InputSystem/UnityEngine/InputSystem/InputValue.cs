namespace UnityEngine.InputSystem
{
	[global::System.Diagnostics.DebuggerDisplay("Value = {Get()}")]
	public class InputValue
	{
		internal global::UnityEngine.InputSystem.InputAction.CallbackContext? m_Context;

		public bool isPressed => Get<float>() >= global::UnityEngine.InputSystem.Controls.ButtonControl.s_GlobalDefaultButtonPressPoint;

		public object Get()
		{
			return m_Context.Value.ReadValueAsObject();
		}

		public TValue Get<TValue>() where TValue : struct
		{
			if (!m_Context.HasValue)
			{
				throw new global::System.InvalidOperationException("Values can only be retrieved while in message callbacks");
			}
			return m_Context.Value.ReadValue<TValue>();
		}
	}
}

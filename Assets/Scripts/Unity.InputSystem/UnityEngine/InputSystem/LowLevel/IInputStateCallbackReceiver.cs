namespace UnityEngine.InputSystem.LowLevel
{
	public interface IInputStateCallbackReceiver
	{
		void OnNextUpdate();

		void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr);

		bool GetStateOffsetForEvent(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, ref uint offset);
	}
}

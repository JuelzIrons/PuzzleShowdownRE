namespace UnityEngine.InputSystem.LowLevel
{
	public interface IInputStateChangeMonitor
	{
		void NotifyControlStateChanged(global::UnityEngine.InputSystem.InputControl control, double time, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, long monitorIndex);

		void NotifyTimerExpired(global::UnityEngine.InputSystem.InputControl control, double time, long monitorIndex, int timerIndex);
	}
}

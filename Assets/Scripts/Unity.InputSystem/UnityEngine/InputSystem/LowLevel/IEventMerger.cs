namespace UnityEngine.InputSystem.LowLevel
{
	internal interface IEventMerger
	{
		bool MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr);
	}
}

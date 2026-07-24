namespace UnityEngine.Rendering
{
	public interface IPerFrameHistoryAccessTracker
	{
		void RequestAccess<Type>() where Type : global::UnityEngine.Rendering.ContextItem;
	}
}

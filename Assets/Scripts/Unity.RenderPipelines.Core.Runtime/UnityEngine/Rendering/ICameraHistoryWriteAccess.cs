namespace UnityEngine.Rendering
{
	public interface ICameraHistoryWriteAccess
	{
		bool IsAccessRequested<Type>() where Type : global::UnityEngine.Rendering.ContextItem;

		Type GetHistoryForWrite<Type>() where Type : global::UnityEngine.Rendering.ContextItem, new();

		bool IsWritten<Type>() where Type : global::UnityEngine.Rendering.ContextItem;
	}
}

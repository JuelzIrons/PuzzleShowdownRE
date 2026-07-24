namespace UnityEngine.Rendering
{
	public interface ICameraHistoryReadAccess
	{
		public delegate void HistoryRequestDelegate(global::UnityEngine.Rendering.IPerFrameHistoryAccessTracker historyAccess);

		event global::UnityEngine.Rendering.ICameraHistoryReadAccess.HistoryRequestDelegate OnGatherHistoryRequests;

		Type GetHistoryForRead<Type>() where Type : global::UnityEngine.Rendering.ContextItem;
	}
}

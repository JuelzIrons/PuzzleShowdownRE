namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMoveMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IMoveHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMove", base.gameObject, eventData);
		}
	}
}

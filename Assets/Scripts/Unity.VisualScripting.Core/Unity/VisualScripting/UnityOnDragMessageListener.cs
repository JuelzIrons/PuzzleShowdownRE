namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnDragMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDrag", base.gameObject, eventData);
		}
	}
}

namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnEndDragMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IEndDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnEndDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnEndDrag", base.gameObject, eventData);
		}
	}
}

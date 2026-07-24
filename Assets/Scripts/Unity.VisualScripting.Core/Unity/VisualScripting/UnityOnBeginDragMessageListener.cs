namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnBeginDragMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IBeginDragHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnBeginDrag(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBeginDrag", base.gameObject, eventData);
		}
	}
}

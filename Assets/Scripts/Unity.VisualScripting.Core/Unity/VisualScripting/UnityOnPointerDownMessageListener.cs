namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnPointerDownMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerDown", base.gameObject, eventData);
		}
	}
}

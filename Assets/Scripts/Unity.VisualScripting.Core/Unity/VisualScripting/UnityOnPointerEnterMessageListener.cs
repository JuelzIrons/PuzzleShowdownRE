namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnPointerEnterMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerEnter", base.gameObject, eventData);
		}
	}
}

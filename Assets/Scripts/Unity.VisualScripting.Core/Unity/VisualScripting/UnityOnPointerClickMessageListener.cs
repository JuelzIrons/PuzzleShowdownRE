namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnPointerClickMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerClick", base.gameObject, eventData);
		}
	}
}

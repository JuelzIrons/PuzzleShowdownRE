namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnPointerUpMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerUp", base.gameObject, eventData);
		}
	}
}

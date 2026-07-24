namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnPointerExitMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnPointerExit", base.gameObject, eventData);
		}
	}
}

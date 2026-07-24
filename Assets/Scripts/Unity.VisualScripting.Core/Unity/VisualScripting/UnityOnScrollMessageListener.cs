namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnScrollMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IScrollHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnScroll(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnScroll", base.gameObject, eventData);
		}
	}
}

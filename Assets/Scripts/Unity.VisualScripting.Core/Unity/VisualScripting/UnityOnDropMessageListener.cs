namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnDropMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IDropHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnDrop(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDrop", base.gameObject, eventData);
		}
	}
}

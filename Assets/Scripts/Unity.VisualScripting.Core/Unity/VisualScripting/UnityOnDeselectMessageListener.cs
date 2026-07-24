namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnDeselectMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.IDeselectHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnDeselect", base.gameObject, eventData);
		}
	}
}

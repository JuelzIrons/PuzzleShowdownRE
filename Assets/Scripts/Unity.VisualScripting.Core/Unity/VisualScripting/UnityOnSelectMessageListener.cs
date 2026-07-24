namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnSelectMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnSelect", base.gameObject, eventData);
		}
	}
}

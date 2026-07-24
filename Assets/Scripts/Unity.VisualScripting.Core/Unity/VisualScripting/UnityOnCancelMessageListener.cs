namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCancelMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.ICancelHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnCancel(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCancel", base.gameObject, eventData);
		}
	}
}

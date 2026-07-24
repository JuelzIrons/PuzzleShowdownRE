namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnSubmitMessageListener : global::Unity.VisualScripting.MessageListener, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.EventSystems.IEventSystemHandler
	{
		public void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnSubmit", base.gameObject, eventData);
		}
	}
}

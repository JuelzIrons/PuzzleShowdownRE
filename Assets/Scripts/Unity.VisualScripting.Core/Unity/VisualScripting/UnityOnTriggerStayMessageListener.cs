namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerStayMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerStay(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerStay", base.gameObject, other);
		}
	}
}

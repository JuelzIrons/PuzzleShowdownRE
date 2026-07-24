namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerEnterMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerEnter(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerEnter", base.gameObject, other);
		}
	}
}

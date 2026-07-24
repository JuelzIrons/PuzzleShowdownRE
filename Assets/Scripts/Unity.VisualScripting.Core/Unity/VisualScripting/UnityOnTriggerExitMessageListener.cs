namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerExitMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerExit(global::UnityEngine.Collider other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerExit", base.gameObject, other);
		}
	}
}

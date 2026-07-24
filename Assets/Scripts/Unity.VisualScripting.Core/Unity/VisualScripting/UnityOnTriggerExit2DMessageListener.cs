namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerExit2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerExit2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerExit2D", base.gameObject, other);
		}
	}
}

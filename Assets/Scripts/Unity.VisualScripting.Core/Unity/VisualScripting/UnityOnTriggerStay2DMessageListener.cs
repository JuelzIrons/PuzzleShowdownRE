namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerStay2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerStay2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerStay2D", base.gameObject, other);
		}
	}
}

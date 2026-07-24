namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTriggerEnter2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTriggerEnter2D(global::UnityEngine.Collider2D other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTriggerEnter2D", base.gameObject, other);
		}
	}
}

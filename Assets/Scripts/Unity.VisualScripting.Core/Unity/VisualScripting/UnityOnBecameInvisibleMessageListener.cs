namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnBecameInvisibleMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnBecameInvisible()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBecameInvisible", base.gameObject);
		}
	}
}

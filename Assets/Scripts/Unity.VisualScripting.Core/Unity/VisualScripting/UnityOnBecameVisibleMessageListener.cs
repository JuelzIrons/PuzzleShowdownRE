namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnBecameVisibleMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnBecameVisible()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnBecameVisible", base.gameObject);
		}
	}
}

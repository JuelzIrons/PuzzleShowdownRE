namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionExitMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionExit(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionExit", base.gameObject, collision);
		}
	}
}

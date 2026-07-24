namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionEnterMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionEnter(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionEnter", base.gameObject, collision);
		}
	}
}

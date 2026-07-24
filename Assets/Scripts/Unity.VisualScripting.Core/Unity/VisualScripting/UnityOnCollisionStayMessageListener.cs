namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionStayMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionStay(global::UnityEngine.Collision collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionStay", base.gameObject, collision);
		}
	}
}

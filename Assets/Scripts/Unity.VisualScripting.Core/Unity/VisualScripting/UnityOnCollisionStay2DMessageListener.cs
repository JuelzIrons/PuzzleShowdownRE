namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionStay2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionStay2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionStay2D", base.gameObject, collision);
		}
	}
}

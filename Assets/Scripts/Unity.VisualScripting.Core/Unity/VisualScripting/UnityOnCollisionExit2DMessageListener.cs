namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionExit2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionExit2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionExit2D", base.gameObject, collision);
		}
	}
}

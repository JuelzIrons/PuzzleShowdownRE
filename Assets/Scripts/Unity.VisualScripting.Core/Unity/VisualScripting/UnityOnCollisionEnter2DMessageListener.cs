namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnCollisionEnter2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnCollisionEnter2D(global::UnityEngine.Collision2D collision)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnCollisionEnter2D", base.gameObject, collision);
		}
	}
}

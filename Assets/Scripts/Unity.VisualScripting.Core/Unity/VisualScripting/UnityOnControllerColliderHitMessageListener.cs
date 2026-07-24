namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnControllerColliderHitMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnControllerColliderHit(global::UnityEngine.ControllerColliderHit hit)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnControllerColliderHit", base.gameObject, hit);
		}
	}
}

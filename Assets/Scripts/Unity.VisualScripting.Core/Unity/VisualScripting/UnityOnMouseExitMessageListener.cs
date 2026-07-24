namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseExitMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseExit()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseExit", base.gameObject);
		}
	}
}

namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseUpAsButtonMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseUpAsButton()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseUpAsButton", base.gameObject);
		}
	}
}

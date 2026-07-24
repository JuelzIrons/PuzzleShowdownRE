namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseOverMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseOver()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseOver", base.gameObject);
		}
	}
}

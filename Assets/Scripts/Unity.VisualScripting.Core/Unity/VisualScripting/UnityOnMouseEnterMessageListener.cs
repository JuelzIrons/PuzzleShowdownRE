namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseEnterMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseEnter()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseEnter", base.gameObject);
		}
	}
}

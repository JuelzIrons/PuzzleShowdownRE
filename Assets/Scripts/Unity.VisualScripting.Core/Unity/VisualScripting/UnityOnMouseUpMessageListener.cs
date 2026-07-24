namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseUpMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseUp()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseUp", base.gameObject);
		}
	}
}

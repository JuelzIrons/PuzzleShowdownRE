namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseDragMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseDrag()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseDrag", base.gameObject);
		}
	}
}

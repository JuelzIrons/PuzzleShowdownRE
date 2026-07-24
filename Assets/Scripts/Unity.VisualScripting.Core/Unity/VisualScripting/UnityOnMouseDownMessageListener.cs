namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnMouseDownMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnMouseDown()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnMouseDown", base.gameObject);
		}
	}
}

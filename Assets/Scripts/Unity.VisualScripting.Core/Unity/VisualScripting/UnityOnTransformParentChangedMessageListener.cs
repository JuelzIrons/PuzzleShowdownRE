namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTransformParentChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTransformParentChanged()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTransformParentChanged", base.gameObject);
		}
	}
}

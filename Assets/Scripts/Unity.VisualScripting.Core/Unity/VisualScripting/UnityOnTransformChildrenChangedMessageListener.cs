namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnTransformChildrenChangedMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnTransformChildrenChanged()
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnTransformChildrenChanged", base.gameObject);
		}
	}
}

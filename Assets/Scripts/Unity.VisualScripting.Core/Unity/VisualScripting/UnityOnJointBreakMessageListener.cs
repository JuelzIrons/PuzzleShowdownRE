namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnJointBreakMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnJointBreak(float breakForce)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnJointBreak", base.gameObject, breakForce);
		}
	}
}

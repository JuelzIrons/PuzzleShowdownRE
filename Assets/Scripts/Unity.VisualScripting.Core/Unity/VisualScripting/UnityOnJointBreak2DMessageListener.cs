namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnJointBreak2DMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnJointBreak2D(global::UnityEngine.Joint2D brokenJoint)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnJointBreak2D", base.gameObject, brokenJoint);
		}
	}
}

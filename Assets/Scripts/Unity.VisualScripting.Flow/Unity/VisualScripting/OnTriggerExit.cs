namespace Unity.VisualScripting
{
	public sealed class OnTriggerExit : global::Unity.VisualScripting.TriggerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTriggerExitMessageListener);

		protected override string hookName => "OnTriggerExit";
	}
}

namespace Unity.VisualScripting
{
	public sealed class OnTriggerEnter : global::Unity.VisualScripting.TriggerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTriggerEnterMessageListener);

		protected override string hookName => "OnTriggerEnter";
	}
}

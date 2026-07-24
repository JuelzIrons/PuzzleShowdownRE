namespace Unity.VisualScripting
{
	public sealed class OnTriggerStay : global::Unity.VisualScripting.TriggerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTriggerStayMessageListener);

		protected override string hookName => "OnTriggerStay";
	}
}

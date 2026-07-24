namespace Unity.VisualScripting
{
	public sealed class OnTriggerStay2D : global::Unity.VisualScripting.TriggerEvent2DUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTriggerStay2DMessageListener);

		protected override string hookName => "OnTriggerStay2D";
	}
}

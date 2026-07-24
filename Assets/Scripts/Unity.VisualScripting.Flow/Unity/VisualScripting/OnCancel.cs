namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(25)]
	public sealed class OnCancel : global::Unity.VisualScripting.GenericGuiEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCancelMessageListener);

		protected override string hookName => "OnCancel";
	}
}

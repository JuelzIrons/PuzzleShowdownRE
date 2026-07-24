namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(24)]
	public sealed class OnSubmit : global::Unity.VisualScripting.GenericGuiEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnSubmitMessageListener);

		protected override string hookName => "OnSubmit";
	}
}

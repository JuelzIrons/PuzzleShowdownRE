namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(23)]
	public sealed class OnDeselect : global::Unity.VisualScripting.GenericGuiEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnDeselectMessageListener);

		protected override string hookName => "OnDeselect";
	}
}

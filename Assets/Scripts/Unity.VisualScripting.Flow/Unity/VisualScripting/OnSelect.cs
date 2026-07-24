namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(22)]
	public sealed class OnSelect : global::Unity.VisualScripting.GenericGuiEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnSelectMessageListener);

		protected override string hookName => "OnSelect";
	}
}

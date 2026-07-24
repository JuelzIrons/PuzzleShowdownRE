namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(20)]
	public sealed class OnScroll : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnScrollMessageListener);

		protected override string hookName => "OnScroll";
	}
}

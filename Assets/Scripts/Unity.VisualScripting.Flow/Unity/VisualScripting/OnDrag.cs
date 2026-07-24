namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(17)]
	public sealed class OnDrag : global::Unity.VisualScripting.PointerEventUnit
	{
		protected override string hookName => "OnDrag";

		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnDragMessageListener);
	}
}

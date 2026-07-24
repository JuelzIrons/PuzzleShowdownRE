namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.OnDrag))]
	[global::Unity.VisualScripting.UnitOrder(18)]
	public sealed class OnEndDrag : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnEndDragMessageListener);

		protected override string hookName => "OnEndDrag";
	}
}

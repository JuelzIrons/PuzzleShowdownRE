namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.OnDrag))]
	[global::Unity.VisualScripting.UnitOrder(16)]
	public sealed class OnBeginDrag : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnBeginDragMessageListener);

		protected override string hookName => "OnBeginDrag";
	}
}

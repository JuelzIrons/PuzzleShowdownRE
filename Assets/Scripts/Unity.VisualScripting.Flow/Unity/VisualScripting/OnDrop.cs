namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.OnDrag))]
	[global::Unity.VisualScripting.UnitOrder(19)]
	public sealed class OnDrop : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnDropMessageListener);

		protected override string hookName => "OnDrop";
	}
}

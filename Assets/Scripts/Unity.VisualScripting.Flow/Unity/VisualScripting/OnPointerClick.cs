namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(11)]
	public sealed class OnPointerClick : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnPointerClickMessageListener);

		protected override string hookName => "OnPointerClick";
	}
}

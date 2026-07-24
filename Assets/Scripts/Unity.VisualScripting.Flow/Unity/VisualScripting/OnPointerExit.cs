namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.UnitOrder(15)]
	public sealed class OnPointerExit : global::Unity.VisualScripting.PointerEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnPointerExitMessageListener);

		protected override string hookName => "OnPointerExit";
	}
}

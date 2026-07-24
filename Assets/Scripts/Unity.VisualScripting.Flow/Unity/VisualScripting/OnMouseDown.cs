namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnMouseDown : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>, global::Unity.VisualScripting.IMouseEventUnit
	{
		protected override string hookName => "OnMouseDown";

		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnMouseDownMessageListener);
	}
}

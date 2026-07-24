namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnMouseUpAsButton : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>, global::Unity.VisualScripting.IMouseEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnMouseUpAsButtonMessageListener);

		protected override string hookName => "OnMouseUpAsButton";
	}
}

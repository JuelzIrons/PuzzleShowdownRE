namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnMouseDrag : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>, global::Unity.VisualScripting.IMouseEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnMouseDragMessageListener);

		protected override string hookName => "OnMouseDrag";
	}
}

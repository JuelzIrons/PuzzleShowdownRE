namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnMouseEnter : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>, global::Unity.VisualScripting.IMouseEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnMouseEnterMessageListener);

		protected override string hookName => "OnMouseEnter";
	}
}

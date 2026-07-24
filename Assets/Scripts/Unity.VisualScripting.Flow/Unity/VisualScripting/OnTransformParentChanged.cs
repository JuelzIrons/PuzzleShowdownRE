namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Hierarchy")]
	public sealed class OnTransformParentChanged : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTransformParentChangedMessageListener);

		protected override string hookName => "OnTransformParentChanged";
	}
}

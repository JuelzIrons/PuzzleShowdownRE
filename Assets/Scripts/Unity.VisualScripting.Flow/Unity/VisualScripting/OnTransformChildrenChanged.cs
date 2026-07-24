namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Hierarchy")]
	public sealed class OnTransformChildrenChanged : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnTransformChildrenChangedMessageListener);

		protected override string hookName => "OnTransformChildrenChanged";
	}
}

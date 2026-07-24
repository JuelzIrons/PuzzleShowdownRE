namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Animation")]
	public sealed class OnAnimatorMove : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.AnimatorMessageListener);

		protected override string hookName => "OnAnimatorMove";
	}
}

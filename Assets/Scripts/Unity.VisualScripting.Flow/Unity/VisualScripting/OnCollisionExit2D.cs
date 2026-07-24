namespace Unity.VisualScripting
{
	public sealed class OnCollisionExit2D : global::Unity.VisualScripting.CollisionEvent2DUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionExit2DMessageListener);

		protected override string hookName => "OnCollisionExit2D";
	}
}

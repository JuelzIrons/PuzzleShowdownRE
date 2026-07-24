namespace Unity.VisualScripting
{
	public sealed class OnCollisionExit : global::Unity.VisualScripting.CollisionEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionExitMessageListener);

		protected override string hookName => "OnCollisionExit";
	}
}

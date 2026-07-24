namespace Unity.VisualScripting
{
	public sealed class OnCollisionEnter : global::Unity.VisualScripting.CollisionEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionEnterMessageListener);

		protected override string hookName => "OnCollisionEnter";
	}
}

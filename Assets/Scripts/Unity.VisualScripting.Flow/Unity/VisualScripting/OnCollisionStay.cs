namespace Unity.VisualScripting
{
	public sealed class OnCollisionStay : global::Unity.VisualScripting.CollisionEventUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionStayMessageListener);

		protected override string hookName => "OnCollisionStay";
	}
}

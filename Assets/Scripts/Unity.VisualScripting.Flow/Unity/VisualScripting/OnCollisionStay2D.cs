namespace Unity.VisualScripting
{
	public sealed class OnCollisionStay2D : global::Unity.VisualScripting.CollisionEvent2DUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionStay2DMessageListener);

		protected override string hookName => "OnCollisionStay2D";
	}
}

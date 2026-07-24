namespace Unity.VisualScripting
{
	public sealed class OnCollisionEnter2D : global::Unity.VisualScripting.CollisionEvent2DUnit
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnCollisionEnter2DMessageListener);

		protected override string hookName => "OnCollisionEnter2D";
	}
}

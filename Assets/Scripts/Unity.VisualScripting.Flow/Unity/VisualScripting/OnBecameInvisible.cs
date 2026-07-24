namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Rendering")]
	public sealed class OnBecameInvisible : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnBecameInvisibleMessageListener);

		protected override string hookName => "OnBecameInvisible";
	}
}

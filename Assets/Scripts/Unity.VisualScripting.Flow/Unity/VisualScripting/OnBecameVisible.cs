namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Rendering")]
	public sealed class OnBecameVisible : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnBecameVisibleMessageListener);

		protected override string hookName => "OnBecameVisible";
	}
}

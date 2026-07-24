namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.Button))]
	[global::Unity.VisualScripting.UnitOrder(1)]
	public sealed class OnButtonClick : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "OnButtonClick";

		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnButtonClickMessageListener);
	}
}

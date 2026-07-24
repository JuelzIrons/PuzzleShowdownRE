namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.ScrollRect))]
	[global::Unity.VisualScripting.UnitOrder(7)]
	public sealed class OnScrollRectValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<global::UnityEngine.Vector2>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnScrollRectValueChangedMessageListener);

		protected override string hookName => "OnScrollRectValueChanged";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput<global::UnityEngine.Vector2>("value");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::UnityEngine.Vector2 value)
		{
			flow.SetValue(this.value, value);
		}
	}
}

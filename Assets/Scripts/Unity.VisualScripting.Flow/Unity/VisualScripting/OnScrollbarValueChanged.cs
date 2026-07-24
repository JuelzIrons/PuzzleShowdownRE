namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.Scrollbar))]
	[global::Unity.VisualScripting.UnitOrder(6)]
	public sealed class OnScrollbarValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<float>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnScrollbarValueChangedMessageListener);

		protected override string hookName => "OnScrollbarValueChanged";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput<float>("value");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, float value)
		{
			flow.SetValue(this.value, value);
		}
	}
}

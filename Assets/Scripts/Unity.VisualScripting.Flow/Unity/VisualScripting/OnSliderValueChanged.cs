namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.Slider))]
	[global::Unity.VisualScripting.UnitOrder(8)]
	public sealed class OnSliderValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<float>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnSliderValueChangedMessageListener);

		protected override string hookName => "OnSliderValueChanged";

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

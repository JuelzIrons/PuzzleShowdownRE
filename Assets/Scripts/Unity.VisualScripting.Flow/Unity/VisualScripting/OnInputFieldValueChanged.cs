namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.InputField))]
	[global::Unity.VisualScripting.UnitOrder(2)]
	public sealed class OnInputFieldValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<string>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnInputFieldValueChangedMessageListener);

		protected override string hookName => "OnInputFieldValueChanged";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput<string>("value");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, string value)
		{
			flow.SetValue(this.value, value);
		}
	}
}

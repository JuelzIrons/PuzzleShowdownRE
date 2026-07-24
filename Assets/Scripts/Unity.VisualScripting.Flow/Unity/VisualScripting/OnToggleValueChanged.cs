namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.Toggle))]
	[global::Unity.VisualScripting.UnitOrder(5)]
	public sealed class OnToggleValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<bool>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnToggleValueChangedMessageListener);

		protected override string hookName => "OnToggleValueChanged";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput<bool>("value");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, bool value)
		{
			flow.SetValue(this.value, value);
		}
	}
}

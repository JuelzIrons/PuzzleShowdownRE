namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.InputField))]
	[global::Unity.VisualScripting.UnitOrder(3)]
	public sealed class OnInputFieldEndEdit : global::Unity.VisualScripting.GameObjectEventUnit<string>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnInputFieldEndEditMessageListener);

		protected override string hookName => "OnInputFieldEndEdit";

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

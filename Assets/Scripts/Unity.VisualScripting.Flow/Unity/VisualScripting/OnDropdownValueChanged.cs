namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/GUI")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::UnityEngine.UI.Dropdown))]
	[global::Unity.VisualScripting.UnitOrder(4)]
	public sealed class OnDropdownValueChanged : global::Unity.VisualScripting.GameObjectEventUnit<int>
	{
		public override global::System.Type MessageListenerType => typeof(global::Unity.VisualScripting.UnityOnDropdownValueChangedMessageListener);

		protected override string hookName => "OnDropdownValueChanged";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput index { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput text { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			index = ValueOutput<int>("index");
			text = ValueOutput<string>("text");
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, int index)
		{
			flow.SetValue(this.index, index);
			flow.SetValue(text, flow.GetValue<global::UnityEngine.UI.Dropdown>(base.target).options[index].text);
		}
	}
}

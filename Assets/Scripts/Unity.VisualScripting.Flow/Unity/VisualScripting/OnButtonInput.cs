namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnButtonInput : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "Update";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Name")]
		public global::Unity.VisualScripting.ValueInput buttonName { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput action { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			buttonName = ValueInput("buttonName", string.Empty);
			action = ValueInput("action", global::Unity.VisualScripting.PressState.Down);
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			string value = flow.GetValue<string>(buttonName);
			global::Unity.VisualScripting.PressState value2 = flow.GetValue<global::Unity.VisualScripting.PressState>(action);
			return value2 switch
			{
				global::Unity.VisualScripting.PressState.Down => global::UnityEngine.Input.GetButtonDown(value), 
				global::Unity.VisualScripting.PressState.Up => global::UnityEngine.Input.GetButtonUp(value), 
				global::Unity.VisualScripting.PressState.Hold => global::UnityEngine.Input.GetButton(value), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.PressState>(value2), 
			};
		}
	}
}

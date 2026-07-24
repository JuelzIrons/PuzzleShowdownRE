namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnKeyboardInput : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "Update";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput key { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput action { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			key = ValueInput("key", global::UnityEngine.KeyCode.Space);
			action = ValueInput("action", global::Unity.VisualScripting.PressState.Down);
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			global::UnityEngine.KeyCode value = flow.GetValue<global::UnityEngine.KeyCode>(key);
			global::Unity.VisualScripting.PressState value2 = flow.GetValue<global::Unity.VisualScripting.PressState>(action);
			return value2 switch
			{
				global::Unity.VisualScripting.PressState.Down => global::UnityEngine.Input.GetKeyDown(value), 
				global::Unity.VisualScripting.PressState.Up => global::UnityEngine.Input.GetKeyUp(value), 
				global::Unity.VisualScripting.PressState.Hold => global::UnityEngine.Input.GetKey(value), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.PressState>(value2), 
			};
		}
	}
}

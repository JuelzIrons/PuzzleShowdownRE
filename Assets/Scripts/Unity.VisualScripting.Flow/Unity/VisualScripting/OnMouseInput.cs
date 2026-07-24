namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public sealed class OnMouseInput : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>, global::Unity.VisualScripting.IMouseEventUnit
	{
		protected override string hookName => "Update";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput button { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput action { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			button = ValueInput("button", global::Unity.VisualScripting.MouseButton.Left);
			action = ValueInput("action", global::Unity.VisualScripting.PressState.Down);
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			int value = (int)flow.GetValue<global::Unity.VisualScripting.MouseButton>(button);
			global::Unity.VisualScripting.PressState value2 = flow.GetValue<global::Unity.VisualScripting.PressState>(action);
			return value2 switch
			{
				global::Unity.VisualScripting.PressState.Down => global::UnityEngine.Input.GetMouseButtonDown(value), 
				global::Unity.VisualScripting.PressState.Up => global::UnityEngine.Input.GetMouseButtonUp(value), 
				global::Unity.VisualScripting.PressState.Hold => global::UnityEngine.Input.GetMouseButton(value), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.PressState>(value2), 
			};
		}
	}
}

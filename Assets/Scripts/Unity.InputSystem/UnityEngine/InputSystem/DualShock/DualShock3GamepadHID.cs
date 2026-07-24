namespace UnityEngine.InputSystem.DualShock
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.DualShock.LowLevel.DualShock3HIDInputReport), hideInUI = true, displayName = "PS3 Controller")]
	public class DualShock3GamepadHID : global::UnityEngine.InputSystem.DualShock.DualShockGamepad
	{
		public global::UnityEngine.InputSystem.Controls.ButtonControl leftTriggerButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightTriggerButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl playStationButton { get; protected set; }

		protected override void FinishSetup()
		{
			leftTriggerButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftTriggerButton");
			rightTriggerButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightTriggerButton");
			playStationButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("systemButton");
			base.FinishSetup();
		}
	}
}

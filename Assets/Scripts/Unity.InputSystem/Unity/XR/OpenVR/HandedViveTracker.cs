namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Handed Vive Tracker", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class HandedViveTracker : global::Unity.XR.OpenVR.ViveTracker
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl primary { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "JoystickOrPadPressed" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl trackpadPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		protected override void FinishSetup()
		{
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			primary = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("primary");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			trackpadPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("trackpadPressed");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			base.FinishSetup();
		}
	}
}

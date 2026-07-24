namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Vive Wand", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class ViveWand : global::UnityEngine.InputSystem.XR.XRControllerWithRumble
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl primary { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisClick", "joystickOrPadPressed" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl trackpadPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisTouch", "joystickOrPadTouched" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl trackpadTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Primary2DAxis" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control trackpad { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			primary = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("primary");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			trackpadPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("trackpadPressed");
			trackpadTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("trackpadTouched");
			trackpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("trackpad");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
		}
	}
}

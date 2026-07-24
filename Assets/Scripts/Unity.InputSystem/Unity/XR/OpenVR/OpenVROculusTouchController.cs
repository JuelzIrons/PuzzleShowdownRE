namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Oculus Touch Controller (OpenVR)", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OpenVROculusTouchController : global::UnityEngine.InputSystem.XR.XRControllerWithRumble
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.Vector2Control thumbstick { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Alternate" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl primaryButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Primary" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl secondaryButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisClicked" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl thumbstickClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisTouch" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl thumbstickTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			thumbstick = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("thumbstick");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			primaryButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("primaryButton");
			secondaryButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("secondaryButton");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			thumbstickClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("thumbstickClicked");
			thumbstickTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("thumbstickTouched");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
		}
	}
}

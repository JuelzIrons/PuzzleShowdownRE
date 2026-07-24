namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Windows MR Controller (OpenVR)", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OpenVRControllerWMR : global::UnityEngine.InputSystem.XR.XRController
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisClick", "joystickOrPadPressed" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadClick { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxisTouch", "joystickOrPadTouched" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadTouch { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl menu { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "secondary2DAxis" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "primary2DAxis" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control joystick { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			touchpadClick = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadClick");
			touchpadTouch = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadTouch");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			menu = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("menu");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
			joystick = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("joystick");
		}
	}
}

namespace Unity.XR.Oculus.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Oculus Touch Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OculusTouchController : global::UnityEngine.InputSystem.XR.XRControllerWithRumble
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Primary2DAxis", "Joystick" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control thumbstick { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "A", "X", "Alternate" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl primaryButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "B", "Y", "Primary" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl secondaryButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "GripButton" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl start { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "JoystickOrPadPressed", "thumbstickClick" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl thumbstickClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "ATouched", "XTouched", "ATouch", "XTouch" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl primaryTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "BTouched", "YTouched", "BTouch", "YTouch" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl secondaryTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "indexTouch", "indexNearTouched" })]
		public global::UnityEngine.InputSystem.Controls.AxisControl triggerTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "indexButton", "indexTouched" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "JoystickOrPadTouched", "thumbstickTouch" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "trackingState", layout = "Integer", aliases = new string[] { "controllerTrackingState" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "isTracked", layout = "Button", aliases = new string[] { "ControllerIsTracked" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "devicePosition", layout = "Vector3", aliases = new string[] { "controllerPosition" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "deviceRotation", layout = "Quaternion", aliases = new string[] { "controllerRotation" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl thumbstickTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "controllerVelocity" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "controllerAngularVelocity" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "controllerAcceleration" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "controllerAngularAcceleration" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularAcceleration { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			thumbstick = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("thumbstick");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			triggerTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("triggerTouched");
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			primaryButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("primaryButton");
			secondaryButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("secondaryButton");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			start = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("start");
			thumbstickClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("thumbstickClicked");
			primaryTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("primaryTouched");
			secondaryTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("secondaryTouched");
			thumbstickTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("thumbstickTouched");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			deviceAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAcceleration");
			deviceAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularAcceleration");
		}
	}
}

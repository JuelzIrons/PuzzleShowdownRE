namespace UnityEngine.XR.WindowsMR.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Windows MR Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class WMRSpatialController : global::UnityEngine.InputSystem.XR.XRControllerWithRumble
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Primary2DAxis", "thumbstickaxes" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control joystick { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Secondary2DAxis", "touchpadaxes" })]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "gripaxis" })]
		public global::UnityEngine.InputSystem.Controls.AxisControl grip { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "gripbutton" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl gripPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "Primary", "menubutton" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl menu { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "triggeraxis" })]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "triggerbutton" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "thumbstickpressed" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl joystickClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "joystickorpadpressed", "touchpadpressed" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "joystickorpadtouched", "touchpadtouched" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "gripVelocity" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "gripAngularVelocity" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl batteryLevel { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl sourceLossRisk { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control sourceLossMitigationDirection { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control pointerPosition { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "PointerOrientation" })]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl pointerRotation { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			joystick = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("joystick");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
			grip = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("grip");
			gripPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("gripPressed");
			menu = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("menu");
			joystickClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("joystickClicked");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			touchpadClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadClicked");
			touchpadTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchPadTouched");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			batteryLevel = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("batteryLevel");
			sourceLossRisk = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("sourceLossRisk");
			sourceLossMitigationDirection = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("sourceLossMitigationDirection");
			pointerPosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("pointerPosition");
			pointerRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("pointerRotation");
		}
	}
}

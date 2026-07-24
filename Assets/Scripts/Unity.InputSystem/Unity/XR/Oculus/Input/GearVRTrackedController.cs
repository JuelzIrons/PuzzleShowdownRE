namespace Unity.XR.Oculus.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "GearVR Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class GearVRTrackedController : global::UnityEngine.InputSystem.XR.XRController
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.AxisControl trigger { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl back { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl triggerPressed { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularAcceleration { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("trigger");
			back = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("back");
			triggerPressed = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("triggerPressed");
			touchpadClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadClicked");
			touchpadTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadTouched");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			deviceAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAcceleration");
			deviceAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularAcceleration");
		}
	}
}

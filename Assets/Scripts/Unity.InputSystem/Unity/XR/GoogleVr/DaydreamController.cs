namespace Unity.XR.GoogleVr
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Daydream Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class DaydreamController : global::UnityEngine.InputSystem.XR.XRController
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl volumeUp { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl recentered { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl volumeDown { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl recentering { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl app { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl home { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadClicked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadTouched { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAcceleration { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
			volumeUp = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("volumeUp");
			recentered = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("recentered");
			volumeDown = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("volumeDown");
			recentering = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("recentering");
			app = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("app");
			home = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("home");
			touchpadClicked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadClicked");
			touchpadTouched = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadTouched");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAcceleration");
		}
	}
}

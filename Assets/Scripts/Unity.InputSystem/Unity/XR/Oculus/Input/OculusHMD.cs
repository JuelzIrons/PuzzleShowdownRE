namespace Unity.XR.Oculus.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Oculus Headset", hideInUI = true)]
	public class OculusHMD : global::UnityEngine.InputSystem.XR.XRHMD
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "trackingState", layout = "Integer", aliases = new string[] { "devicetrackingstate" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "isTracked", layout = "Button", aliases = new string[] { "deviceistracked" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl userPresence { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyeAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyeAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyeAngularAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyeAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyeAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyeAngularAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyeAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyeAcceleration { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyeAngularAcceleration { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			userPresence = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("userPresence");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			deviceAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAcceleration");
			deviceAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularAcceleration");
			leftEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyeAngularVelocity");
			leftEyeAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyeAcceleration");
			leftEyeAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyeAngularAcceleration");
			rightEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyeAngularVelocity");
			rightEyeAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyeAcceleration");
			rightEyeAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyeAngularAcceleration");
			centerEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyeAngularVelocity");
			centerEyeAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyeAcceleration");
			centerEyeAngularAcceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyeAngularAcceleration");
		}
	}
}

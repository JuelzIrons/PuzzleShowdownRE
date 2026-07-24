namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "OpenVR Headset", hideInUI = true)]
	public class OpenVRHMD : global::UnityEngine.InputSystem.XR.XRHMD
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyeVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyeAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyeVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyeAngularVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyeVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyeAngularVelocity { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
			leftEyeVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyeVelocity");
			leftEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyeAngularVelocity");
			rightEyeVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyeVelocity");
			rightEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyeAngularVelocity");
			centerEyeVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyeVelocity");
			centerEyeAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyeAngularVelocity");
		}
	}
}

namespace UnityEngine.InputSystem.XR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR HMD", canRunInBackground = true)]
	public class XRHMD : global::UnityEngine.InputSystem.TrackedDevice
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyePosition { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl leftEyeRotation { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyePosition { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl rightEyeRotation { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control centerEyePosition { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl centerEyeRotation { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			centerEyePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("centerEyePosition");
			centerEyeRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("centerEyeRotation");
			leftEyePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyePosition");
			leftEyeRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("leftEyeRotation");
			rightEyePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyePosition");
			rightEyeRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("rightEyeRotation");
		}
	}
}

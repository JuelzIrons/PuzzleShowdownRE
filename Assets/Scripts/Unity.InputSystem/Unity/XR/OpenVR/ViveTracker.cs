namespace Unity.XR.OpenVR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Vive Tracker")]
	public class ViveTracker : global::UnityEngine.InputSystem.TrackedDevice
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceAngularVelocity { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			deviceAngularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceAngularVelocity");
		}
	}
}

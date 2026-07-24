namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Tracked Device", isGenericTypeOfDevice = true)]
	public class TrackedDevice : global::UnityEngine.InputSystem.InputDevice
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(synthetic = true)]
		public global::UnityEngine.InputSystem.Controls.IntegerControl trackingState { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(synthetic = true)]
		public global::UnityEngine.InputSystem.Controls.ButtonControl isTracked { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, dontReset = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control devicePosition { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, dontReset = true)]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl deviceRotation { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			trackingState = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("trackingState");
			isTracked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("isTracked");
			devicePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("devicePosition");
			deviceRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("deviceRotation");
		}
	}
}

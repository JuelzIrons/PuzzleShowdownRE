namespace UnityEngine.InputSystem.XR
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 60)]
	public struct PoseState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		internal const int kSizeInBytes = 60;

		internal static readonly global::UnityEngine.InputSystem.Utilities.FourCC s_Format = new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'o', 's', 'e');

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Is Tracked", layout = "Button", sizeInBits = 8u)]
		public bool isTracked;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Tracking State", layout = "Integer")]
		public global::UnityEngine.XR.InputTrackingState trackingState;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Position", noisy = true)]
		public global::UnityEngine.Vector3 position;

		[global::System.Runtime.InteropServices.FieldOffset(20)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Rotation", noisy = true)]
		public global::UnityEngine.Quaternion rotation;

		[global::System.Runtime.InteropServices.FieldOffset(36)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Velocity", noisy = true)]
		public global::UnityEngine.Vector3 velocity;

		[global::System.Runtime.InteropServices.FieldOffset(48)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Angular Velocity", noisy = true)]
		public global::UnityEngine.Vector3 angularVelocity;

		public global::UnityEngine.InputSystem.Utilities.FourCC format => s_Format;

		public PoseState(bool isTracked, global::UnityEngine.XR.InputTrackingState trackingState, global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, global::UnityEngine.Vector3 velocity, global::UnityEngine.Vector3 angularVelocity)
		{
			this.isTracked = isTracked;
			this.trackingState = trackingState;
			this.position = position;
			this.rotation = rotation;
			this.velocity = velocity;
			this.angularVelocity = angularVelocity;
		}
	}
}

namespace Unity.XR.Oculus.Input
{
	public class OculusTrackingReference : global::UnityEngine.InputSystem.TrackedDevice
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "trackingReferenceTrackingState" })]
		public new global::UnityEngine.InputSystem.Controls.IntegerControl trackingState { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "trackingReferenceIsTracked" })]
		public new global::UnityEngine.InputSystem.Controls.ButtonControl isTracked { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			trackingState = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("trackingState");
			isTracked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("isTracked");
		}
	}
}

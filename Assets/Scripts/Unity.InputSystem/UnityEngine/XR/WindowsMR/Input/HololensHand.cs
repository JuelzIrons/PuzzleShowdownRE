namespace UnityEngine.XR.WindowsMR.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "HoloLens Hand", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class HololensHand : global::UnityEngine.InputSystem.XR.XRController
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true, aliases = new string[] { "gripVelocity" })]
		public global::UnityEngine.InputSystem.Controls.Vector3Control deviceVelocity { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(aliases = new string[] { "triggerbutton" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl airTap { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl sourceLossRisk { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control sourceLossMitigationDirection { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			airTap = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("airTap");
			deviceVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("deviceVelocity");
			sourceLossRisk = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("sourceLossRisk");
			sourceLossMitigationDirection = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("sourceLossMitigationDirection");
		}
	}
}

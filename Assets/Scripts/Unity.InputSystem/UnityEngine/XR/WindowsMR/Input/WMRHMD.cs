namespace UnityEngine.XR.WindowsMR.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Windows MR Headset", hideInUI = true)]
	public class WMRHMD : global::UnityEngine.InputSystem.XR.XRHMD
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "devicePosition", layout = "Vector3", aliases = new string[] { "HeadPosition" })]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "deviceRotation", layout = "Quaternion", aliases = new string[] { "HeadRotation" })]
		public global::UnityEngine.InputSystem.Controls.ButtonControl userPresence { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			userPresence = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("userPresence");
		}
	}
}

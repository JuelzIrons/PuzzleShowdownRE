namespace Unity.XR.Oculus.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Oculus Headset (w/ on-headset controls)", hideInUI = true)]
	public class OculusHMDExtended : global::Unity.XR.Oculus.Input.OculusHMD
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl back { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			back = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("back");
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
		}
	}
}

namespace Unity.XR.Oculus.Input
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Oculus Remote", hideInUI = true)]
	public class OculusRemote : global::UnityEngine.InputSystem.InputDevice
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl back { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl start { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.Vector2Control touchpad { get; protected set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			back = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("back");
			start = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("start");
			touchpad = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("touchpad");
		}
	}
}

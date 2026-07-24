namespace UnityEngine.InputSystem.Controls
{
	public class StickControl : global::UnityEngine.InputSystem.Controls.Vector2Control
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Up")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "x", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "y", minValue = -1f, maxValue = 1f, layout = "Axis", processors = "axisDeadzone")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl up { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "y", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Down")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl down { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=-1,clampMax=0,invert", synthetic = true, displayName = "Left")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl left { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "x", processors = "axisDeadzone", parameters = "clamp=2,clampMin=0,clampMax=1", synthetic = true, displayName = "Right")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl right { get; set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			up = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("up");
			down = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("down");
			left = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("left");
			right = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("right");
		}
	}
}

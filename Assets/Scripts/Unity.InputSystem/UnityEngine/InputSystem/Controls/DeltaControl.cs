namespace UnityEngine.InputSystem.Controls
{
	[global::UnityEngine.Scripting.Preserve]
	public class DeltaControl : global::UnityEngine.InputSystem.Controls.Vector2Control
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Up")]
		[global::UnityEngine.Scripting.Preserve]
		public global::UnityEngine.InputSystem.Controls.AxisControl up { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "y", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Down")]
		[global::UnityEngine.Scripting.Preserve]
		public global::UnityEngine.InputSystem.Controls.AxisControl down { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=-3.402823E+38,clampMax=0,invert", synthetic = true, displayName = "Left")]
		[global::UnityEngine.Scripting.Preserve]
		public global::UnityEngine.InputSystem.Controls.AxisControl left { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(useStateFrom = "x", parameters = "clamp=1,clampMin=0,clampMax=3.402823E+38", synthetic = true, displayName = "Right")]
		[global::UnityEngine.Scripting.Preserve]
		public global::UnityEngine.InputSystem.Controls.AxisControl right { get; set; }

		protected override void FinishSetup()
		{
			base.FinishSetup();
			up = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("up");
			down = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("down");
			left = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("left");
			right = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("right");
		}
	}
}

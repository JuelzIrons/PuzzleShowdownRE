namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Hinge Angle")]
	public class HingeAngle : global::UnityEngine.InputSystem.Sensor
	{
		public global::UnityEngine.InputSystem.Controls.AxisControl angle { get; protected set; }

		public static global::UnityEngine.InputSystem.HingeAngle current { get; private set; }

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
		}

		protected override void FinishSetup()
		{
			angle = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("angle");
			base.FinishSetup();
		}
	}
}

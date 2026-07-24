namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Pressure")]
	public class PressureSensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Atmospheric Pressure", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl atmosphericPressure { get; protected set; }

		public static global::UnityEngine.InputSystem.PressureSensor current { get; private set; }

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
			atmosphericPressure = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("atmosphericPressure");
			base.FinishSetup();
		}
	}
}

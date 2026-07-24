namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Light")]
	public class LightSensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Light Level", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl lightLevel { get; protected set; }

		public static global::UnityEngine.InputSystem.LightSensor current { get; private set; }

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
			lightLevel = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("lightLevel");
			base.FinishSetup();
		}
	}
}

namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Ambient Temperature")]
	public class AmbientTemperatureSensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Ambient Temperature", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl ambientTemperature { get; protected set; }

		public static global::UnityEngine.InputSystem.AmbientTemperatureSensor current { get; private set; }

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
			ambientTemperature = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("ambientTemperature");
			base.FinishSetup();
		}
	}
}

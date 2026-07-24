namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Humidity")]
	public class HumiditySensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Relative Humidity", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl relativeHumidity { get; protected set; }

		public static global::UnityEngine.InputSystem.HumiditySensor current { get; private set; }

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
			relativeHumidity = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("relativeHumidity");
			base.FinishSetup();
		}
	}
}

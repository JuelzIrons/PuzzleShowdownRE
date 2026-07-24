namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.LinearAccelerationState), displayName = "Linear Acceleration")]
	public class LinearAccelerationSensor : global::UnityEngine.InputSystem.Sensor
	{
		public global::UnityEngine.InputSystem.Controls.Vector3Control acceleration { get; protected set; }

		public static global::UnityEngine.InputSystem.LinearAccelerationSensor current { get; private set; }

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
			acceleration = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}

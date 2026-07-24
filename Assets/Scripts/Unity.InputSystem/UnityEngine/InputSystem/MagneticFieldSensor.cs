namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Magnetic Field")]
	public class MagneticFieldSensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Magnetic Field", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.Vector3Control magneticField { get; protected set; }

		public static global::UnityEngine.InputSystem.MagneticFieldSensor current { get; private set; }

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
			magneticField = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("magneticField");
			base.FinishSetup();
		}
	}
}

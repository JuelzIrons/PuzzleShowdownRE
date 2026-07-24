namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Proximity")]
	public class ProximitySensor : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Distance", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.AxisControl distance { get; protected set; }

		public static global::UnityEngine.InputSystem.ProximitySensor current { get; private set; }

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
			distance = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("distance");
			base.FinishSetup();
		}
	}
}

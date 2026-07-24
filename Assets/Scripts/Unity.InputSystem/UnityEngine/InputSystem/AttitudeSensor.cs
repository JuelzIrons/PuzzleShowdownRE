namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.AttitudeState), displayName = "Attitude")]
	public class AttitudeSensor : global::UnityEngine.InputSystem.Sensor
	{
		public global::UnityEngine.InputSystem.Controls.QuaternionControl attitude { get; protected set; }

		public static global::UnityEngine.InputSystem.AttitudeSensor current { get; private set; }

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
			attitude = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("attitude");
			base.FinishSetup();
		}
	}
}

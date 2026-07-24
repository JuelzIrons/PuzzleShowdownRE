namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.GyroscopeState))]
	public class Gyroscope : global::UnityEngine.InputSystem.Sensor
	{
		public global::UnityEngine.InputSystem.Controls.Vector3Control angularVelocity { get; protected set; }

		public static global::UnityEngine.InputSystem.Gyroscope current { get; private set; }

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
			angularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("angularVelocity");
			base.FinishSetup();
		}
	}
}

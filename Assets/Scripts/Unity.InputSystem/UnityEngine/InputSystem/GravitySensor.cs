namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.GravityState), displayName = "Gravity")]
	public class GravitySensor : global::UnityEngine.InputSystem.Sensor
	{
		public global::UnityEngine.InputSystem.Controls.Vector3Control gravity { get; protected set; }

		public static global::UnityEngine.InputSystem.GravitySensor current { get; private set; }

		protected override void FinishSetup()
		{
			gravity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("gravity");
			base.FinishSetup();
		}

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
	}
}

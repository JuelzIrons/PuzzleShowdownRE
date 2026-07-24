namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Step Counter")]
	public class StepCounter : global::UnityEngine.InputSystem.Sensor
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Step Counter", noisy = true)]
		public global::UnityEngine.InputSystem.Controls.IntegerControl stepCounter { get; protected set; }

		public static global::UnityEngine.InputSystem.StepCounter current { get; private set; }

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
			stepCounter = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("stepCounter");
			base.FinishSetup();
		}
	}
}

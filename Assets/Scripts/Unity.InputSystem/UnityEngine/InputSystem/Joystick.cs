namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.JoystickState), isGenericTypeOfDevice = true)]
	public class Joystick : global::UnityEngine.InputSystem.InputDevice
	{
		private static int s_JoystickCount;

		private static global::UnityEngine.InputSystem.Joystick[] s_Joysticks;

		public global::UnityEngine.InputSystem.Controls.ButtonControl trigger { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.StickControl stick { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.AxisControl twist { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control hatswitch { get; protected set; }

		public static global::UnityEngine.InputSystem.Joystick current { get; private set; }

		public new static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Joystick> all => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Joystick>(s_Joysticks, 0, s_JoystickCount);

		protected override void FinishSetup()
		{
			trigger = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("{PrimaryTrigger}");
			stick = GetChildControl<global::UnityEngine.InputSystem.Controls.StickControl>("{Primary2DMotion}");
			twist = TryGetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("{Twist}");
			hatswitch = TryGetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("{Hatswitch}");
			base.FinishSetup();
		}

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnAdded()
		{
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref s_Joysticks, ref s_JoystickCount, this);
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
			int num = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.IndexOfReference(s_Joysticks, this, s_JoystickCount);
			if (num != -1)
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAtWithCapacity(s_Joysticks, ref s_JoystickCount, num);
			}
		}
	}
}

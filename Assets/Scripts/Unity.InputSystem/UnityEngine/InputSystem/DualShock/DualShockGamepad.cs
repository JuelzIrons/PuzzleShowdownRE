namespace UnityEngine.InputSystem.DualShock
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "PlayStation Controller")]
	public class DualShockGamepad : global::UnityEngine.InputSystem.Gamepad, global::UnityEngine.InputSystem.DualShock.IDualShockHaptics, global::UnityEngine.InputSystem.Haptics.IDualMotorRumble, global::UnityEngine.InputSystem.Haptics.IHaptics
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonWest", displayName = "Square", shortDisplayName = "Square")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonNorth", displayName = "Triangle", shortDisplayName = "Triangle")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonEast", displayName = "Circle", shortDisplayName = "Circle")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonSouth", displayName = "Cross", shortDisplayName = "Cross")]
		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public global::UnityEngine.InputSystem.Controls.ButtonControl touchpadButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "start", displayName = "Options")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl optionsButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "select", displayName = "Share")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl shareButton { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShoulder", displayName = "L1", shortDisplayName = "L1")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl L1 { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShoulder", displayName = "R1", shortDisplayName = "R1")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl R1 { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftTrigger", displayName = "L2", shortDisplayName = "L2")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl L2 { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightTrigger", displayName = "R2", shortDisplayName = "R2")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl R2 { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftStickPress", displayName = "L3", shortDisplayName = "L3")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl L3 { get; protected set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightStickPress", displayName = "R3", shortDisplayName = "R3")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl R3 { get; protected set; }

		public new static global::UnityEngine.InputSystem.DualShock.DualShockGamepad current { get; private set; }

		internal global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor hidDescriptor { get; private set; }

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
			base.FinishSetup();
			touchpadButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("touchpadButton");
			optionsButton = base.startButton;
			shareButton = base.selectButton;
			L1 = base.leftShoulder;
			R1 = base.rightShoulder;
			L2 = base.leftTrigger;
			R2 = base.rightTrigger;
			L3 = base.leftStickButton;
			R3 = base.rightStickButton;
			if (m_Description.capabilities != null && m_Description.interfaceName == "HID")
			{
				hidDescriptor = global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor.FromJson(m_Description.capabilities);
			}
		}

		public virtual void SetLightBarColor(global::UnityEngine.Color color)
		{
		}
	}
}

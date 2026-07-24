namespace UnityEngine.InputSystem.LowLevel
{
	internal struct JoystickState : global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo
	{
		public enum Button
		{
			HatSwitchUp = 0,
			HatSwitchDown = 1,
			HatSwitchLeft = 2,
			HatSwitchRight = 3,
			Trigger = 4
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "trigger", displayName = "Trigger", layout = "Button", usages = new string[] { "PrimaryTrigger", "PrimaryAction", "Submit" }, bit = 4u)]
		public int buttons;

		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Stick", layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone")]
		public global::UnityEngine.Vector2 stick;

		public static global::UnityEngine.InputSystem.Utilities.FourCC kFormat => new global::UnityEngine.InputSystem.Utilities.FourCC('J', 'O', 'Y');

		public global::UnityEngine.InputSystem.Utilities.FourCC format => kFormat;
	}
}

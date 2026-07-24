namespace UnityEngine.InputSystem.XInput
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(displayName = "Xbox Controller")]
	public class XInputController : global::UnityEngine.InputSystem.Gamepad
	{
		internal enum DeviceType
		{
			Gamepad = 0
		}

		public enum DeviceSubType
		{
			Unknown = 0,
			Gamepad = 1,
			Wheel = 2,
			ArcadeStick = 3,
			FlightStick = 4,
			DancePad = 5,
			Guitar = 6,
			GuitarAlternate = 7,
			DrumKit = 8,
			GuitarBass = 11,
			ArcadePad = 19
		}

		[global::System.Flags]
		public new enum DeviceFlags
		{
			ForceFeedbackSupported = 1,
			Wireless = 2,
			VoiceSupported = 4,
			PluginModulesSupported = 8,
			NoNavigation = 0x10
		}

		[global::System.Serializable]
		internal struct Capabilities
		{
			public global::UnityEngine.InputSystem.XInput.XInputController.DeviceType type;

			public global::UnityEngine.InputSystem.XInput.XInputController.DeviceSubType subType;

			public global::UnityEngine.InputSystem.XInput.XInputController.DeviceFlags flags;
		}

		private bool m_HaveParsedCapabilities;

		private global::UnityEngine.InputSystem.XInput.XInputController.DeviceSubType m_SubType;

		private global::UnityEngine.InputSystem.XInput.XInputController.DeviceFlags m_Flags;

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonSouth", displayName = "A")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonEast", displayName = "B")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonWest", displayName = "X")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "buttonNorth", displayName = "Y")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftShoulder", displayName = "Left Bumper", shortDisplayName = "LB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightShoulder", displayName = "Right Bumper", shortDisplayName = "RB")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "leftTrigger", shortDisplayName = "LT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "rightTrigger", shortDisplayName = "RT")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "start", displayName = "Menu", alias = "menu")]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "select", displayName = "View", alias = "view")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl menu { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl view { get; protected set; }

		public global::UnityEngine.InputSystem.XInput.XInputController.DeviceSubType subType
		{
			get
			{
				if (!m_HaveParsedCapabilities)
				{
					ParseCapabilities();
				}
				return m_SubType;
			}
		}

		public global::UnityEngine.InputSystem.XInput.XInputController.DeviceFlags flags
		{
			get
			{
				if (!m_HaveParsedCapabilities)
				{
					ParseCapabilities();
				}
				return m_Flags;
			}
		}

		protected override void FinishSetup()
		{
			base.FinishSetup();
			menu = base.startButton;
			view = base.selectButton;
		}

		private void ParseCapabilities()
		{
			if (!string.IsNullOrEmpty(base.description.capabilities))
			{
				global::UnityEngine.InputSystem.XInput.XInputController.Capabilities capabilities = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.XInput.XInputController.Capabilities>(base.description.capabilities);
				m_SubType = capabilities.subType;
				m_Flags = capabilities.flags;
			}
			m_HaveParsedCapabilities = true;
		}
	}
}

namespace Steamworks
{
	[global::System.Serializable]
	public struct SteamInputActionEvent_t
	{
		[global::System.Serializable]
		public struct AnalogAction_t
		{
			public global::Steamworks.InputAnalogActionHandle_t actionHandle;

			public global::Steamworks.InputAnalogActionData_t analogActionData;
		}

		[global::System.Serializable]
		public struct DigitalAction_t
		{
			public global::Steamworks.InputDigitalActionHandle_t actionHandle;

			public global::Steamworks.InputDigitalActionData_t digitalActionData;
		}

		[global::System.Serializable]
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
		public struct OptionValue
		{
			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.SteamInputActionEvent_t.AnalogAction_t analogAction;

			[global::System.Runtime.InteropServices.FieldOffset(0)]
			public global::Steamworks.SteamInputActionEvent_t.DigitalAction_t digitalAction;
		}

		public global::Steamworks.InputHandle_t controllerHandle;

		public global::Steamworks.ESteamInputActionEventType eEventType;

		public global::Steamworks.SteamInputActionEvent_t.OptionValue m_val;
	}
}

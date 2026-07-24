namespace UnityEngine.InputSystem.XR.Haptics
{
	public struct BufferedRumble
	{
		public global::UnityEngine.InputSystem.XR.Haptics.HapticCapabilities capabilities { get; private set; }

		private global::UnityEngine.InputSystem.InputDevice device { get; set; }

		public BufferedRumble(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			this.device = device;
			global::UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand command = global::UnityEngine.InputSystem.XR.Haptics.GetHapticCapabilitiesCommand.Create();
			device.ExecuteCommand(ref command);
			capabilities = command.capabilities;
		}

		public void EnqueueRumble(byte[] samples)
		{
			global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand command = global::UnityEngine.InputSystem.XR.Haptics.SendBufferedHapticCommand.Create(samples);
			device.ExecuteCommand(ref command);
		}
	}
}

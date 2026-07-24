namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 8)]
	public struct InitiateUserAccountPairingCommand : global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
	{
		public enum Result
		{
			SuccessfullyInitiated = 1,
			ErrorNotSupported = -1,
			ErrorAlreadyInProgress = -2
		}

		internal const int kSize = 8;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand baseCommand;

		public static global::UnityEngine.InputSystem.Utilities.FourCC Type => new global::UnityEngine.InputSystem.Utilities.FourCC('P', 'A', 'I', 'R');

		public global::UnityEngine.InputSystem.Utilities.FourCC typeStatic => Type;

		public static global::UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand Create()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InitiateUserAccountPairingCommand
			{
				baseCommand = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(Type)
			};
		}
	}
}

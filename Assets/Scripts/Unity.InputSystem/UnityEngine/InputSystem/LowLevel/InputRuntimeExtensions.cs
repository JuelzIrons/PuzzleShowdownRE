namespace UnityEngine.InputSystem.LowLevel
{
	internal static class InputRuntimeExtensions
	{
		public unsafe static long DeviceCommand<TCommand>(this global::UnityEngine.InputSystem.LowLevel.IInputRuntime runtime, int deviceId, ref TCommand command) where TCommand : struct, global::UnityEngine.InputSystem.LowLevel.IInputDeviceCommandInfo
		{
			if (runtime == null)
			{
				throw new global::System.ArgumentNullException("runtime");
			}
			return runtime.DeviceCommand(deviceId, (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref command));
		}
	}
}

namespace UnityEngine.InputSystem.DualShock
{
	internal static class DualShockSupport
	{
		public static void Initialize()
		{
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.DualShock.DualShockGamepad>();
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithCapability("vendorId", 1356).WithCapability("productId", 3570));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.DualShock.DualSenseGamepadHID>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithCapability("vendorId", 1356).WithCapability("productId", 3302));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithCapability("vendorId", 1356).WithCapability("productId", 2508));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayoutMatcher<global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID>(default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithCapability("vendorId", 1356).WithCapability("productId", 1476));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayoutMatcher<global::UnityEngine.InputSystem.DualShock.DualShock4GamepadHID>(default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithManufacturerContains("Sony").WithProduct("Wireless Controller"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.DualShock.DualShock3GamepadHID>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithCapability("vendorId", 1356).WithCapability("productId", 616));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayoutMatcher<global::UnityEngine.InputSystem.DualShock.DualShock3GamepadHID>(default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("HID").WithManufacturerContains("Sony").WithProduct("PLAYSTATION(R)3 Controller", supportRegex: false));
		}
	}
}

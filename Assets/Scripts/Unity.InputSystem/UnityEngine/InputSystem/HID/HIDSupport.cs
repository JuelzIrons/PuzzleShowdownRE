namespace UnityEngine.InputSystem.HID
{
	public static class HIDSupport
	{
		public struct HIDPageUsage
		{
			public global::UnityEngine.InputSystem.HID.HID.UsagePage page;

			public int usage;

			public HIDPageUsage(global::UnityEngine.InputSystem.HID.HID.UsagePage page, int usage)
			{
				this.page = page;
				this.usage = usage;
			}

			public HIDPageUsage(global::UnityEngine.InputSystem.HID.HID.GenericDesktop usage)
			{
				page = global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop;
				this.usage = (int)usage;
			}
		}

		private static global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage[] s_SupportedHIDUsages;

		public static global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage> supportedHIDUsages
		{
			get
			{
				return s_SupportedHIDUsages;
			}
			set
			{
				s_SupportedHIDUsages = value.ToArray();
				global::UnityEngine.InputSystem.InputSystem.s_Manager.AddAvailableDevicesThatAreNowRecognized();
				for (int i = 0; i < global::UnityEngine.InputSystem.InputSystem.devices.Count; i++)
				{
					global::UnityEngine.InputSystem.InputDevice inputDevice = global::UnityEngine.InputSystem.InputSystem.devices[i];
					if (inputDevice is global::UnityEngine.InputSystem.HID.HID hID && !global::System.Linq.Enumerable.Contains(s_SupportedHIDUsages, new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage(hID.hidDescriptor.usagePage, hID.hidDescriptor.usage)))
					{
						global::UnityEngine.InputSystem.InputSystem.RemoveLayout(inputDevice.layout);
						i--;
					}
				}
			}
		}

		internal static void Initialize()
		{
			s_SupportedHIDUsages = new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage[3]
			{
				new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage(global::UnityEngine.InputSystem.HID.HID.GenericDesktop.Joystick),
				new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage(global::UnityEngine.InputSystem.HID.HID.GenericDesktop.Gamepad),
				new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage(global::UnityEngine.InputSystem.HID.HID.GenericDesktop.MultiAxisController)
			};
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.HID.HID>();
			global::UnityEngine.InputSystem.InputSystem.onFindLayoutForDevice += global::UnityEngine.InputSystem.HID.HID.OnFindLayoutForDevice;
		}
	}
}

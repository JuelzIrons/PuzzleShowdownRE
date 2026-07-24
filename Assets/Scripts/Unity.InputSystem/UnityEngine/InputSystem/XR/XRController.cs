namespace UnityEngine.InputSystem.XR
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(commonUsages = new string[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = true, displayName = "XR Controller")]
	public class XRController : global::UnityEngine.InputSystem.TrackedDevice
	{
		public static global::UnityEngine.InputSystem.XR.XRController leftHand => global::UnityEngine.InputSystem.InputSystem.GetDevice<global::UnityEngine.InputSystem.XR.XRController>(global::UnityEngine.InputSystem.CommonUsages.LeftHand);

		public static global::UnityEngine.InputSystem.XR.XRController rightHand => global::UnityEngine.InputSystem.InputSystem.GetDevice<global::UnityEngine.InputSystem.XR.XRController>(global::UnityEngine.InputSystem.CommonUsages.RightHand);

		protected override void FinishSetup()
		{
			base.FinishSetup();
			global::UnityEngine.InputSystem.XR.XRDeviceDescriptor xRDeviceDescriptor = global::UnityEngine.InputSystem.XR.XRDeviceDescriptor.FromJson(base.description.capabilities);
			if (xRDeviceDescriptor != null)
			{
				if ((xRDeviceDescriptor.characteristics & global::UnityEngine.XR.InputDeviceCharacteristics.Left) != global::UnityEngine.XR.InputDeviceCharacteristics.None)
				{
					global::UnityEngine.InputSystem.InputSystem.SetDeviceUsage(this, global::UnityEngine.InputSystem.CommonUsages.LeftHand);
				}
				else if ((xRDeviceDescriptor.characteristics & global::UnityEngine.XR.InputDeviceCharacteristics.Right) != global::UnityEngine.XR.InputDeviceCharacteristics.None)
				{
					global::UnityEngine.InputSystem.InputSystem.SetDeviceUsage(this, global::UnityEngine.InputSystem.CommonUsages.RightHand);
				}
			}
		}
	}
}

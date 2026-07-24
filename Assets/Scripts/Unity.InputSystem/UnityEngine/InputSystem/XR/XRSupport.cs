namespace UnityEngine.InputSystem.XR
{
	internal static class XRSupport
	{
		public static void Initialize()
		{
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XR.PoseControl>("Pose");
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XR.BoneControl>("Bone");
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XR.EyesControl>("Eyes");
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XR.XRHMD>();
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.InputSystem.XR.XRController>();
			global::UnityEngine.InputSystem.InputSystem.onFindLayoutForDevice += global::UnityEngine.InputSystem.XR.XRLayoutBuilder.OnFindLayoutForDevice;
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.XR.WindowsMR.Input.WMRHMD>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("(Windows Mixed Reality HMD)|(Microsoft HoloLens)|(^(WindowsMR Headset))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.XR.WindowsMR.Input.WMRSpatialController>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("(^(Spatial Controller))|(^(OpenVR Controller\\(WindowsMR))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::UnityEngine.XR.WindowsMR.Input.HololensHand>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("(^(Hand -))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.OculusHMD>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(Oculus Rift)|^(Oculus Quest)|^(Oculus Go)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.OculusTouchController>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("(^(Oculus Touch Controller))|(^(Oculus Quest Controller))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.OculusRemote>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("Oculus Remote"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.OculusTrackingReference>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("((Tracking Reference)|(^(Oculus Rift [a-zA-Z0-9]* \\(Camera)))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.OculusHMDExtended>("GearVR", default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("Oculus HMD"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.Oculus.Input.GearVRTrackedController>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(Oculus Tracked Remote)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.GoogleVr.DaydreamHMD>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("Daydream HMD"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.GoogleVr.DaydreamController>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(Daydream Controller)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.OpenVRHMD>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(OpenVR Headset)|^(Vive Pro)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.OpenVRControllerWMR>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(OpenVR Controller\\(WindowsMR)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.ViveWand>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithManufacturer("HTC").WithProduct("^(OpenVR Controller\\(((Vive. Controller)|(VIVE. Controller)|(Vive Controller)))"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.OpenVROculusTouchController>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithProduct("^(OpenVR Controller\\(Oculus)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.ViveTracker>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithManufacturer("HTC").WithProduct("^(VIVE Tracker)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.HandedViveTracker>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithManufacturer("HTC").WithProduct("^(OpenVR Controller\\(VIVE Tracker)"));
			global::UnityEngine.InputSystem.InputSystem.RegisterLayout<global::Unity.XR.OpenVR.ViveLighthouse>(null, default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher).WithInterface("^(XRInput)").WithManufacturer("HTC").WithProduct("^(HTC V2-XD/XE)"));
		}
	}
}

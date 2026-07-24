namespace UnityEngine.InputSystem.XR
{
	[global::System.Serializable]
	public class XRDeviceDescriptor
	{
		public string deviceName;

		public string manufacturer;

		public string serialNumber;

		public global::UnityEngine.XR.InputDeviceCharacteristics characteristics;

		public int deviceId;

		public global::System.Collections.Generic.List<global::UnityEngine.InputSystem.XR.XRFeatureDescriptor> inputFeatures;

		public string ToJson()
		{
			return global::UnityEngine.JsonUtility.ToJson(this);
		}

		public static global::UnityEngine.InputSystem.XR.XRDeviceDescriptor FromJson(string json)
		{
			return global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.XR.XRDeviceDescriptor>(json);
		}
	}
}

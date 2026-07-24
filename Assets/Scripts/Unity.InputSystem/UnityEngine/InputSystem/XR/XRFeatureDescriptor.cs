namespace UnityEngine.InputSystem.XR
{
	[global::System.Serializable]
	public struct XRFeatureDescriptor
	{
		public string name;

		public global::System.Collections.Generic.List<global::UnityEngine.InputSystem.XR.UsageHint> usageHints;

		public global::UnityEngine.InputSystem.XR.FeatureType featureType;

		public uint customSize;
	}
}

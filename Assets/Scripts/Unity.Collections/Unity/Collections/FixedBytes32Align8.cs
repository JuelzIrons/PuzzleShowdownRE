namespace Unity.Collections
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 32)]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	internal struct FixedBytes32Align8
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		[global::UnityEngine.SerializeField]
		internal global::Unity.Collections.FixedBytes16Align8 offset0000;

		[global::System.Runtime.InteropServices.FieldOffset(16)]
		[global::UnityEngine.SerializeField]
		internal global::Unity.Collections.FixedBytes16Align8 offset0016;
	}
}

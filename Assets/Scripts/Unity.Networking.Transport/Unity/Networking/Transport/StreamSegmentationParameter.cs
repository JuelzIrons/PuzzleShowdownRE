namespace Unity.Networking.Transport
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct StreamSegmentationParameter : global::Unity.Networking.Transport.INetworkParameter
	{
		public bool Validate()
		{
			return true;
		}
	}
}

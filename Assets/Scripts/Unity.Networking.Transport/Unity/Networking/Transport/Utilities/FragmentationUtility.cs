namespace Unity.Networking.Transport.Utilities
{
	[global::System.Serializable]
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct FragmentationUtility
	{
		public struct Parameters : global::Unity.Networking.Transport.INetworkParameter
		{
			internal const int k_DefaultPayloadCapacity = 4096;

			internal const int k_MaxPayloadCapacity = 24115776;

			public int PayloadCapacity;

			public bool Validate()
			{
				bool result = true;
				if (PayloadCapacity <= 0)
				{
					result = false;
					global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) must be greater than 0", "PayloadCapacity", PayloadCapacity));
				}
				if (PayloadCapacity >= 24115776)
				{
					result = false;
					global::UnityEngine.Debug.LogError(string.Format("{0} value ({1}) can't be greater than {2}", "PayloadCapacity", PayloadCapacity, 24115776));
				}
				return result;
			}
		}
	}
}

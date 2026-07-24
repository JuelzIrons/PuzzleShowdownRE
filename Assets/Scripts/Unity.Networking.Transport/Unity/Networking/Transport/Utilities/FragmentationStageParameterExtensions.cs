namespace Unity.Networking.Transport.Utilities
{
	public static class FragmentationStageParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithFragmentationStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int payloadCapacity = 4096)
		{
			global::Unity.Networking.Transport.Utilities.FragmentationUtility.Parameters parameter = new global::Unity.Networking.Transport.Utilities.FragmentationUtility.Parameters
			{
				PayloadCapacity = payloadCapacity
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.Utilities.FragmentationUtility.Parameters GetFragmentationStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.Utilities.FragmentationUtility.Parameters>(out var parameter))
			{
				parameter.PayloadCapacity = 4096;
			}
			return parameter;
		}
	}
}

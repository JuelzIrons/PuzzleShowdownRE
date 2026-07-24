namespace Unity.Networking.Transport
{
	internal static class StreamSegmentationParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithStreamSegmentationParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			global::Unity.Networking.Transport.StreamSegmentationParameter parameter = default(global::Unity.Networking.Transport.StreamSegmentationParameter);
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}
	}
}

namespace Unity.Networking.Transport.Logging
{
	public static class LoggingParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithLoggingParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, global::Unity.Collections.FixedString32Bytes driverName)
		{
			global::Unity.Networking.Transport.Logging.LoggingParameter parameter = new global::Unity.Networking.Transport.Logging.LoggingParameter
			{
				DriverName = driverName
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}
	}
}

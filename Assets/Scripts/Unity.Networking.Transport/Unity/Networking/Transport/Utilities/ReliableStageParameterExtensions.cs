namespace Unity.Networking.Transport.Utilities
{
	public static class ReliableStageParameterExtensions
	{
		private const int k_DefaultWindowSize = 32;

		public static ref global::Unity.Networking.Transport.NetworkSettings WithReliableStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, int windowSize = 32, int minimumResendTime = 64, int maximumResendTime = 200)
		{
			global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters parameter = new global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters
			{
				WindowSize = windowSize,
				MinimumResendTime = minimumResendTime,
				MaximumResendTime = maximumResendTime
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
		}

		public static global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters GetReliableStageParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.Utilities.ReliableUtility.Parameters>(out var parameter))
			{
				parameter.WindowSize = 32;
				parameter.MinimumResendTime = 64;
				parameter.MaximumResendTime = 200;
			}
			return parameter;
		}
	}
}

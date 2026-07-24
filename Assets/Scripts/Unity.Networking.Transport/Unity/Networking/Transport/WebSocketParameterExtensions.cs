namespace Unity.Networking.Transport
{
	public static class WebSocketParameterExtensions
	{
		public static ref global::Unity.Networking.Transport.NetworkSettings WithWebSocketParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings, global::Unity.Collections.FixedString128Bytes path = default(global::Unity.Collections.FixedString128Bytes))
		{
			global::Unity.Networking.Transport.WebSocketParameter parameter = new global::Unity.Networking.Transport.WebSocketParameter
			{
				Path = ((path == ILSpyHelper_AsRefReadOnly(default(global::Unity.Collections.FixedString32Bytes))) ? ((global::Unity.Collections.FixedString128Bytes)"/") : path)
			};
			settings.AddRawParameterStruct(ref parameter);
			return ref settings;
			static ref readonly T ILSpyHelper_AsRefReadOnly<T>(in T temp)
			{
				//ILSpy generated this function to help ensure overload resolution can pick the overload using 'in'
				return ref temp;
			}
		}

		public static global::Unity.Networking.Transport.WebSocketParameter GetWebSocketParameters(this ref global::Unity.Networking.Transport.NetworkSettings settings)
		{
			if (!settings.TryGet<global::Unity.Networking.Transport.WebSocketParameter>(out var parameter))
			{
				parameter.Path = "/";
			}
			return parameter;
		}
	}
}

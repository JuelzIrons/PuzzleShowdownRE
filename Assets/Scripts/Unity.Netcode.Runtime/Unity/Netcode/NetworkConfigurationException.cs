namespace Unity.Netcode
{
	public class NetworkConfigurationException : global::System.Exception
	{
		public NetworkConfigurationException()
		{
		}

		public NetworkConfigurationException(string message)
			: base(message)
		{
		}

		public NetworkConfigurationException(string message, global::System.Exception inner)
			: base(message, inner)
		{
		}
	}
}

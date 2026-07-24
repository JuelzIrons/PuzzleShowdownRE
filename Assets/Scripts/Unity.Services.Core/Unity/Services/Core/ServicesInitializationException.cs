namespace Unity.Services.Core
{
	public class ServicesInitializationException : global::System.Exception
	{
		public ServicesInitializationException()
		{
		}

		public ServicesInitializationException(string message)
			: base(message)
		{
		}

		public ServicesInitializationException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

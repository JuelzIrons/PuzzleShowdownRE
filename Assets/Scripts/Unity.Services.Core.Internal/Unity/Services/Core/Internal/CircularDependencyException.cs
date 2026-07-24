namespace Unity.Services.Core.Internal
{
	public class CircularDependencyException : global::Unity.Services.Core.ServicesInitializationException
	{
		public CircularDependencyException()
		{
		}

		public CircularDependencyException(string message)
			: base(message)
		{
		}
	}
}

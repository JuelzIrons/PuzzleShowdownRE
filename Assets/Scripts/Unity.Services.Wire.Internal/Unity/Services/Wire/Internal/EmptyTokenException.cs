namespace Unity.Services.Wire.Internal
{
	public class EmptyTokenException : global::Unity.Services.Core.RequestFailedException
	{
		public EmptyTokenException()
			: base(23004, "The token provided by the token provider is empty or null.")
		{
		}
	}
}

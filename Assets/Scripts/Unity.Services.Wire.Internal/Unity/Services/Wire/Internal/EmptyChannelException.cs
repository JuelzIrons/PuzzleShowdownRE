namespace Unity.Services.Wire.Internal
{
	public class EmptyChannelException : global::Unity.Services.Core.RequestFailedException
	{
		public EmptyChannelException()
			: base(23005, "The channel provided by the token provider is empty or null.")
		{
		}
	}
}

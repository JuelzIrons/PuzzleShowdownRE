namespace Unity.VisualScripting
{
	public class InvalidConversionException : global::System.InvalidCastException
	{
		public InvalidConversionException()
		{
		}

		public InvalidConversionException(string message)
			: base(message)
		{
		}

		public InvalidConversionException(string message, global::System.Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

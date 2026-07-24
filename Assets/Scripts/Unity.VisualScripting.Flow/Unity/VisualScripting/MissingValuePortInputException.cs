namespace Unity.VisualScripting
{
	public sealed class MissingValuePortInputException : global::System.Exception
	{
		public MissingValuePortInputException(string key)
			: base("Missing input value for '" + key + "'.")
		{
		}
	}
}

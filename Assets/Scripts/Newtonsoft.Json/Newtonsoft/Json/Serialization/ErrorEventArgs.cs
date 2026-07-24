namespace Newtonsoft.Json.Serialization
{
	public class ErrorEventArgs : global::System.EventArgs
	{
		public object? CurrentObject { get; }

		public global::Newtonsoft.Json.Serialization.ErrorContext ErrorContext { get; }

		public ErrorEventArgs(object? currentObject, global::Newtonsoft.Json.Serialization.ErrorContext errorContext)
		{
			CurrentObject = currentObject;
			ErrorContext = errorContext;
		}
	}
}

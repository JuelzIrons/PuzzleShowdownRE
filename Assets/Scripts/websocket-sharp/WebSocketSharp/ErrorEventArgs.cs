namespace WebSocketSharp
{
	public class ErrorEventArgs : global::System.EventArgs
	{
		private global::System.Exception _exception;

		private string _message;

		public global::System.Exception Exception => _exception;

		public string Message => _message;

		internal ErrorEventArgs(string message)
			: this(message, null)
		{
		}

		internal ErrorEventArgs(string message, global::System.Exception exception)
		{
			_message = message;
			_exception = exception;
		}
	}
}

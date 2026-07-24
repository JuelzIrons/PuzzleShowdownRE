namespace WebSocketSharp.Net
{
	public class HttpBasicIdentity : global::System.Security.Principal.GenericIdentity
	{
		private string _password;

		public virtual string Password => _password;

		internal HttpBasicIdentity(string username, string password)
			: base(username, "Basic")
		{
			_password = password;
		}
	}
}

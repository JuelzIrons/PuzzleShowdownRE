namespace WebSocketSharp.Net
{
	public class HttpDigestIdentity : global::System.Security.Principal.GenericIdentity
	{
		private global::System.Collections.Specialized.NameValueCollection _parameters;

		public string Algorithm => _parameters["algorithm"];

		public string Cnonce => _parameters["cnonce"];

		public string Nc => _parameters["nc"];

		public string Nonce => _parameters["nonce"];

		public string Opaque => _parameters["opaque"];

		public string Qop => _parameters["qop"];

		public string Realm => _parameters["realm"];

		public string Response => _parameters["response"];

		public string Uri => _parameters["uri"];

		internal HttpDigestIdentity(global::System.Collections.Specialized.NameValueCollection parameters)
			: base(parameters["username"], "Digest")
		{
			_parameters = parameters;
		}

		internal bool IsValid(string password, string realm, string method, string entity)
		{
			global::System.Collections.Specialized.NameValueCollection nameValueCollection = new global::System.Collections.Specialized.NameValueCollection(_parameters);
			nameValueCollection["password"] = password;
			nameValueCollection["realm"] = realm;
			nameValueCollection["method"] = method;
			nameValueCollection["entity"] = entity;
			string text = global::WebSocketSharp.Net.AuthenticationResponse.CreateRequestDigest(nameValueCollection);
			return _parameters["response"] == text;
		}
	}
}

namespace WebSocketSharp.Net.WebSockets
{
	public abstract class WebSocketContext
	{
		public abstract global::WebSocketSharp.Net.CookieCollection CookieCollection { get; }

		public abstract global::System.Collections.Specialized.NameValueCollection Headers { get; }

		public abstract string Host { get; }

		public abstract bool IsAuthenticated { get; }

		public abstract bool IsLocal { get; }

		public abstract bool IsSecureConnection { get; }

		public abstract bool IsWebSocketRequest { get; }

		public abstract string Origin { get; }

		public abstract global::System.Collections.Specialized.NameValueCollection QueryString { get; }

		public abstract global::System.Uri RequestUri { get; }

		public abstract string SecWebSocketKey { get; }

		public abstract global::System.Collections.Generic.IEnumerable<string> SecWebSocketProtocols { get; }

		public abstract string SecWebSocketVersion { get; }

		public abstract global::System.Net.IPEndPoint ServerEndPoint { get; }

		public abstract global::System.Security.Principal.IPrincipal User { get; }

		public abstract global::System.Net.IPEndPoint UserEndPoint { get; }

		public abstract global::WebSocketSharp.WebSocket WebSocket { get; }
	}
}

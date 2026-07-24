namespace WebSocketSharp.Net.WebSockets
{
	public class HttpListenerWebSocketContext : global::WebSocketSharp.Net.WebSockets.WebSocketContext
	{
		private global::WebSocketSharp.Net.HttpListenerContext _context;

		private global::WebSocketSharp.WebSocket _websocket;

		internal global::WebSocketSharp.Logger Log => _context.Listener.Log;

		internal global::System.IO.Stream Stream => _context.Connection.Stream;

		public override global::WebSocketSharp.Net.CookieCollection CookieCollection => _context.Request.Cookies;

		public override global::System.Collections.Specialized.NameValueCollection Headers => _context.Request.Headers;

		public override string Host => _context.Request.UserHostName;

		public override bool IsAuthenticated => _context.Request.IsAuthenticated;

		public override bool IsLocal => _context.Request.IsLocal;

		public override bool IsSecureConnection => _context.Request.IsSecureConnection;

		public override bool IsWebSocketRequest => _context.Request.IsWebSocketRequest;

		public override string Origin => _context.Request.Headers["Origin"];

		public override global::System.Collections.Specialized.NameValueCollection QueryString => _context.Request.QueryString;

		public override global::System.Uri RequestUri => _context.Request.Url;

		public override string SecWebSocketKey => _context.Request.Headers["Sec-WebSocket-Key"];

		public override global::System.Collections.Generic.IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				string val = _context.Request.Headers["Sec-WebSocket-Protocol"];
				if (val == null || val.Length == 0)
				{
					yield break;
				}
				string[] array = val.Split(new char[1] { ',' });
				foreach (string elm in array)
				{
					string protocol = elm.Trim();
					if (protocol.Length != 0)
					{
						yield return protocol;
					}
				}
			}
		}

		public override string SecWebSocketVersion => _context.Request.Headers["Sec-WebSocket-Version"];

		public override global::System.Net.IPEndPoint ServerEndPoint => _context.Request.LocalEndPoint;

		public override global::System.Security.Principal.IPrincipal User => _context.User;

		public override global::System.Net.IPEndPoint UserEndPoint => _context.Request.RemoteEndPoint;

		public override global::WebSocketSharp.WebSocket WebSocket => _websocket;

		internal HttpListenerWebSocketContext(global::WebSocketSharp.Net.HttpListenerContext context, string protocol)
		{
			_context = context;
			_websocket = new global::WebSocketSharp.WebSocket(this, protocol);
		}

		internal void Close()
		{
			_context.Connection.Close(force: true);
		}

		internal void Close(global::WebSocketSharp.Net.HttpStatusCode code)
		{
			_context.Response.StatusCode = (int)code;
			_context.Response.Close();
		}

		public override string ToString()
		{
			return _context.Request.ToString();
		}
	}
}

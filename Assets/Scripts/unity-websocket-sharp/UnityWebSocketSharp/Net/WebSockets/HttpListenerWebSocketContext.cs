namespace UnityWebSocketSharp.Net.WebSockets
{
	internal class HttpListenerWebSocketContext : global::UnityWebSocketSharp.Net.WebSockets.WebSocketContext
	{
		private global::UnityWebSocketSharp.Net.HttpListenerContext _context;

		private global::UnityWebSocketSharp.WebSocket _websocket;

		internal global::UnityWebSocketSharp.Logger Log => _context.Listener.Log;

		internal global::System.IO.Stream Stream => _context.Connection.Stream;

		public override global::UnityWebSocketSharp.Net.CookieCollection CookieCollection => _context.Request.Cookies;

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
				string text = _context.Request.Headers["Sec-WebSocket-Protocol"];
				if (text == null || text.Length == 0)
				{
					yield break;
				}
				string[] array = text.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					if (text2.Length != 0)
					{
						yield return text2;
					}
				}
			}
		}

		public override string SecWebSocketVersion => _context.Request.Headers["Sec-WebSocket-Version"];

		public override global::System.Net.IPEndPoint ServerEndPoint => _context.Request.LocalEndPoint;

		public override global::System.Security.Principal.IPrincipal User => _context.User;

		public override global::System.Net.IPEndPoint UserEndPoint => _context.Request.RemoteEndPoint;

		public override global::UnityWebSocketSharp.WebSocket WebSocket => _websocket;

		internal HttpListenerWebSocketContext(global::UnityWebSocketSharp.Net.HttpListenerContext context, string protocol)
		{
			_context = context;
			_websocket = new global::UnityWebSocketSharp.WebSocket(this, protocol);
		}

		internal void Close()
		{
			_context.Connection.Close(force: true);
		}

		internal void Close(global::UnityWebSocketSharp.Net.HttpStatusCode code)
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

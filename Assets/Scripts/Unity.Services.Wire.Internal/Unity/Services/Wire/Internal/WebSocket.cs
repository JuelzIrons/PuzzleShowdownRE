namespace Unity.Services.Wire.Internal
{
	internal class WebSocket : global::Unity.Services.Wire.Internal.IWebSocket
	{
		private global::System.Net.Sockets.TcpClient _tcpClient;

		protected global::UnityWebSocketSharp.WebSocket ws;

		public event global::Unity.Services.Wire.Internal.WebSocketOpenEventHandler OnOpen;

		public event global::Unity.Services.Wire.Internal.WebSocketMessageEventHandler OnMessage;

		public event global::Unity.Services.Wire.Internal.WebSocketErrorEventHandler OnError;

		public event global::Unity.Services.Wire.Internal.WebSocketCloseEventHandler OnClose;

		public WebSocket(string url)
		{
			try
			{
				ws = new global::UnityWebSocketSharp.WebSocket(url)
				{
					GetNetworkStream = GetStream
				};
				ws.OnOpen += delegate
				{
					this.OnOpen?.Invoke();
				};
				ws.OnMessage += delegate(object sender, global::UnityWebSocketSharp.MessageEventArgs ev)
				{
					if (ev.RawData != null)
					{
						this.OnMessage?.Invoke(ev.RawData);
					}
				};
				ws.OnError += delegate(object sender, global::UnityWebSocketSharp.ErrorEventArgs ev)
				{
					this.OnError?.Invoke(ev.Message);
				};
				ws.OnClose += delegate(object sender, global::UnityWebSocketSharp.CloseEventArgs ev)
				{
					_tcpClient?.Dispose();
					_tcpClient = null;
					this.OnClose?.Invoke(global::Unity.Services.Wire.Internal.WebSocketHelpers.ParseCloseCodeEnum(ev.Code));
				};
			}
			catch (global::System.Exception inner)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("Failed to create WebSocket Client.", inner);
			}
		}

		public void Connect()
		{
			if (ws.ReadyState == global::UnityWebSocketSharp.WebSocketState.Open || ws.ReadyState == global::UnityWebSocketSharp.WebSocketState.Closing)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already connected or is closing.");
			}
			try
			{
				if (ws.IsSecure)
				{
					ws.SslConfiguration.EnabledSslProtocols = global::System.Security.Authentication.SslProtocols.Tls12;
				}
				ws.ConnectAsync();
			}
			catch (global::System.Exception inner)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("Failed to connect.", inner);
			}
		}

		public void Close(global::Unity.Services.Wire.Internal.WebSocketCloseCode code = global::Unity.Services.Wire.Internal.WebSocketCloseCode.Normal, string reason = null)
		{
			if (ws.ReadyState == global::UnityWebSocketSharp.WebSocketState.Closing)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already closing.");
			}
			if (ws.ReadyState == global::UnityWebSocketSharp.WebSocketState.Closed)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is already closed.");
			}
			try
			{
				ws.CloseAsync((ushort)code, reason);
			}
			catch (global::System.Exception inner)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("Failed to close the connection.", inner);
			}
		}

		public void Send(byte[] data)
		{
			if (ws.ReadyState != global::UnityWebSocketSharp.WebSocketState.Open)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketInvalidStateException("WebSocket is not in open state.");
			}
			try
			{
				ws.Send(data);
			}
			catch (global::System.Exception inner)
			{
				throw new global::Unity.Services.Wire.Internal.WebSocketUnexpectedException("Failed to send message.", inner);
			}
		}

		public global::Unity.Services.Wire.Internal.WebSocketState GetState()
		{
			return ws.ReadyState switch
			{
				global::UnityWebSocketSharp.WebSocketState.Connecting => global::Unity.Services.Wire.Internal.WebSocketState.Connecting, 
				global::UnityWebSocketSharp.WebSocketState.Open => global::Unity.Services.Wire.Internal.WebSocketState.Open, 
				global::UnityWebSocketSharp.WebSocketState.Closing => global::Unity.Services.Wire.Internal.WebSocketState.Closing, 
				global::UnityWebSocketSharp.WebSocketState.Closed => global::Unity.Services.Wire.Internal.WebSocketState.Closed, 
				_ => global::Unity.Services.Wire.Internal.WebSocketState.Closed, 
			};
		}

		private global::System.Net.Sockets.NetworkStream GetStream(string url, int port)
		{
			_tcpClient = new global::System.Net.Sockets.TcpClient(url, port);
			if (_tcpClient == null)
			{
				throw new global::System.Exception("Could not create a network stream because the construction of the required TcpClient failed!\r\nPlease ensure that the platform that you are running the application on supports the System.Net.Sockets library!");
			}
			return _tcpClient.GetStream();
		}
	}
}

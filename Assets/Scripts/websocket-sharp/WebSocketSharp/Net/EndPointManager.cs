namespace WebSocketSharp.Net
{
	internal sealed class EndPointManager
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Net.IPEndPoint, global::WebSocketSharp.Net.EndPointListener> _endpoints;

		static EndPointManager()
		{
			_endpoints = new global::System.Collections.Generic.Dictionary<global::System.Net.IPEndPoint, global::WebSocketSharp.Net.EndPointListener>();
		}

		private EndPointManager()
		{
		}

		private static void addPrefix(string uriPrefix, global::WebSocketSharp.Net.HttpListener listener)
		{
			global::WebSocketSharp.Net.HttpListenerPrefix httpListenerPrefix = new global::WebSocketSharp.Net.HttpListenerPrefix(uriPrefix, listener);
			global::System.Net.IPAddress iPAddress = convertToIPAddress(httpListenerPrefix.Host);
			if (iPAddress == null)
			{
				string message = "The URI prefix includes an invalid host.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message);
			}
			if (!iPAddress.IsLocal())
			{
				string message2 = "The URI prefix includes an invalid host.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message2);
			}
			if (!int.TryParse(httpListenerPrefix.Port, out var result))
			{
				string message3 = "The URI prefix includes an invalid port.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message3);
			}
			if (!result.IsPortNumber())
			{
				string message4 = "The URI prefix includes an invalid port.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message4);
			}
			string path = httpListenerPrefix.Path;
			if (path.IndexOf('%') != -1)
			{
				string message5 = "The URI prefix includes an invalid path.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message5);
			}
			if (path.IndexOf("//", global::System.StringComparison.Ordinal) != -1)
			{
				string message6 = "The URI prefix includes an invalid path.";
				throw new global::WebSocketSharp.Net.HttpListenerException(87, message6);
			}
			global::System.Net.IPEndPoint iPEndPoint = new global::System.Net.IPEndPoint(iPAddress, result);
			if (_endpoints.TryGetValue(iPEndPoint, out var value))
			{
				if (value.IsSecure ^ httpListenerPrefix.IsSecure)
				{
					string message7 = "The URI prefix includes an invalid scheme.";
					throw new global::WebSocketSharp.Net.HttpListenerException(87, message7);
				}
			}
			else
			{
				value = new global::WebSocketSharp.Net.EndPointListener(iPEndPoint, httpListenerPrefix.IsSecure, listener.CertificateFolderPath, listener.SslConfiguration, listener.ReuseAddress);
				_endpoints.Add(iPEndPoint, value);
			}
			value.AddPrefix(httpListenerPrefix);
		}

		private static global::System.Net.IPAddress convertToIPAddress(string hostname)
		{
			if (hostname == "*")
			{
				return global::System.Net.IPAddress.Any;
			}
			if (hostname == "+")
			{
				return global::System.Net.IPAddress.Any;
			}
			return hostname.ToIPAddress();
		}

		private static void removePrefix(string uriPrefix, global::WebSocketSharp.Net.HttpListener listener)
		{
			global::WebSocketSharp.Net.HttpListenerPrefix httpListenerPrefix = new global::WebSocketSharp.Net.HttpListenerPrefix(uriPrefix, listener);
			global::System.Net.IPAddress iPAddress = convertToIPAddress(httpListenerPrefix.Host);
			if (iPAddress == null || !iPAddress.IsLocal() || !int.TryParse(httpListenerPrefix.Port, out var result) || !result.IsPortNumber())
			{
				return;
			}
			string path = httpListenerPrefix.Path;
			if (path.IndexOf('%') == -1 && path.IndexOf("//", global::System.StringComparison.Ordinal) == -1)
			{
				global::System.Net.IPEndPoint key = new global::System.Net.IPEndPoint(iPAddress, result);
				if (_endpoints.TryGetValue(key, out var value) && !(value.IsSecure ^ httpListenerPrefix.IsSecure))
				{
					value.RemovePrefix(httpListenerPrefix);
				}
			}
		}

		internal static bool RemoveEndPoint(global::System.Net.IPEndPoint endpoint)
		{
			lock (((global::System.Collections.ICollection)_endpoints).SyncRoot)
			{
				return _endpoints.Remove(endpoint);
			}
		}

		public static void AddListener(global::WebSocketSharp.Net.HttpListener listener)
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			lock (((global::System.Collections.ICollection)_endpoints).SyncRoot)
			{
				try
				{
					foreach (string prefix in listener.Prefixes)
					{
						addPrefix(prefix, listener);
						list.Add(prefix);
					}
				}
				catch
				{
					foreach (string item in list)
					{
						removePrefix(item, listener);
					}
					throw;
				}
			}
		}

		public static void AddPrefix(string uriPrefix, global::WebSocketSharp.Net.HttpListener listener)
		{
			lock (((global::System.Collections.ICollection)_endpoints).SyncRoot)
			{
				addPrefix(uriPrefix, listener);
			}
		}

		public static void RemoveListener(global::WebSocketSharp.Net.HttpListener listener)
		{
			lock (((global::System.Collections.ICollection)_endpoints).SyncRoot)
			{
				foreach (string prefix in listener.Prefixes)
				{
					removePrefix(prefix, listener);
				}
			}
		}

		public static void RemovePrefix(string uriPrefix, global::WebSocketSharp.Net.HttpListener listener)
		{
			lock (((global::System.Collections.ICollection)_endpoints).SyncRoot)
			{
				removePrefix(uriPrefix, listener);
			}
		}
	}
}

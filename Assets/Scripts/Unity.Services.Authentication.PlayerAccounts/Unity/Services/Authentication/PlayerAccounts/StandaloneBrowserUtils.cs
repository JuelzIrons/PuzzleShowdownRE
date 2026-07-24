namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class StandaloneBrowserUtils : global::Unity.Services.Authentication.PlayerAccounts.IBrowserUtils
	{
		private global::System.Net.HttpListener m_HttpListener;

		private int? m_BoundPort;

		public event global::System.Action<string> AuthCodeReceivedEvent;

		public string GetRedirectUri()
		{
			return $"http://localhost:{m_BoundPort}/callback";
		}

		public bool Bind()
		{
			if (m_BoundPort.HasValue)
			{
				return true;
			}
			if (global::Unity.Services.Authentication.PlayerAccounts.HttpUtilities.TryBindListenerOnFreePort(out var httpListener, out var port))
			{
				m_HttpListener = httpListener;
				m_BoundPort = port;
				return true;
			}
			return false;
		}

		public async global::System.Threading.Tasks.Task LaunchUrlAsync(string url)
		{
			global::UnityEngine.Application.OpenURL(url);
			if (m_HttpListener.IsListening)
			{
				m_HttpListener.Stop();
			}
			m_HttpListener.Start();
			global::System.Net.HttpListenerContext httpListenerContext;
			try
			{
				httpListenerContext = await m_HttpListener.GetContextAsync();
			}
			catch (global::System.ObjectDisposedException ex)
			{
				global::Unity.Services.Authentication.PlayerAccounts.Logger.Log("HttpListener has been disposed." + ex.Message);
				return;
			}
			catch (global::System.Exception ex2)
			{
				global::Unity.Services.Authentication.PlayerAccounts.Logger.Log("HttpListener error." + ex2.Message);
				return;
			}
			SendBrowserResponse(httpListenerContext.Response, m_HttpListener);
			m_HttpListener.Stop();
			if (!global::Unity.Services.Authentication.PlayerAccounts.UriHelper.ParseQueryString(new global::System.Uri(url).Query).TryGetValue("state", out var value) || value == null)
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsExceptionHandler.HandleError("State parameter not found in URL", "state");
			}
			string authCode = GetAuthCode(httpListenerContext, value);
			this.AuthCodeReceivedEvent?.Invoke(authCode);
		}

		public void Dismiss()
		{
		}

		private static void SendBrowserResponse(global::System.Net.HttpListenerResponse response, global::System.Net.HttpListener http)
		{
			string s = "<html><body><b>DONE!</b><br>(You can return to your app and close this tab/window now)";
			byte[] bytes = global::System.Text.Encoding.UTF8.GetBytes(s);
			response.ContentLength64 = bytes.Length;
			global::System.IO.Stream responseOutput = response.OutputStream;
			responseOutput.WriteAsync(bytes, 0, bytes.Length).ContinueWith(delegate
			{
				responseOutput.Close();
				http.Stop();
			});
		}

		private static string GetAuthCode(global::System.Net.HttpListenerContext context, string state)
		{
			string text = context.Request.QueryString.Get("code");
			string text2 = context.Request.QueryString.Get("error");
			string text3 = context.Request.QueryString.Get("state");
			global::System.Uri uri = new global::System.Uri(context.Request.Url.AbsoluteUri);
			if (!string.IsNullOrEmpty(text2))
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsExceptionHandler.HandleError(text2);
			}
			if (string.IsNullOrEmpty(text))
			{
				global::System.Collections.Generic.Dictionary<string, string> dictionary = global::Unity.Services.Authentication.PlayerAccounts.UriHelper.ParseQueryString(uri.Fragment);
				text = dictionary["code"];
				text3 = dictionary["state"];
			}
			if (text3 != state)
			{
				throw global::Unity.Services.Authentication.PlayerAccounts.PlayerAccountsException.Create(10101, "Received request with invalid state (" + text3 + ")");
			}
			return text;
		}
	}
}

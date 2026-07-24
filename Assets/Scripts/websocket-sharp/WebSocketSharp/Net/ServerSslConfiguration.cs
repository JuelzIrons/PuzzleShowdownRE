namespace WebSocketSharp.Net
{
	public class ServerSslConfiguration
	{
		private bool _checkCertRevocation;

		private bool _clientCertRequired;

		private global::System.Net.Security.RemoteCertificateValidationCallback _clientCertValidationCallback;

		private global::System.Security.Authentication.SslProtocols _enabledSslProtocols;

		private global::System.Security.Cryptography.X509Certificates.X509Certificate2 _serverCert;

		public bool CheckCertificateRevocation
		{
			get
			{
				return _checkCertRevocation;
			}
			set
			{
				_checkCertRevocation = value;
			}
		}

		public bool ClientCertificateRequired
		{
			get
			{
				return _clientCertRequired;
			}
			set
			{
				_clientCertRequired = value;
			}
		}

		public global::System.Net.Security.RemoteCertificateValidationCallback ClientCertificateValidationCallback
		{
			get
			{
				if (_clientCertValidationCallback == null)
				{
					_clientCertValidationCallback = defaultValidateClientCertificate;
				}
				return _clientCertValidationCallback;
			}
			set
			{
				_clientCertValidationCallback = value;
			}
		}

		public global::System.Security.Authentication.SslProtocols EnabledSslProtocols
		{
			get
			{
				return _enabledSslProtocols;
			}
			set
			{
				_enabledSslProtocols = value;
			}
		}

		public global::System.Security.Cryptography.X509Certificates.X509Certificate2 ServerCertificate
		{
			get
			{
				return _serverCert;
			}
			set
			{
				_serverCert = value;
			}
		}

		public ServerSslConfiguration()
		{
			_enabledSslProtocols = global::System.Security.Authentication.SslProtocols.None;
		}

		public ServerSslConfiguration(global::WebSocketSharp.Net.ServerSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new global::System.ArgumentNullException("configuration");
			}
			_checkCertRevocation = configuration._checkCertRevocation;
			_clientCertRequired = configuration._clientCertRequired;
			_clientCertValidationCallback = configuration._clientCertValidationCallback;
			_enabledSslProtocols = configuration._enabledSslProtocols;
			_serverCert = configuration._serverCert;
		}

		private static bool defaultValidateClientCertificate(object sender, global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, global::System.Security.Cryptography.X509Certificates.X509Chain chain, global::System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}

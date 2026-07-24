namespace WebSocketSharp.Net
{
	public class ClientSslConfiguration
	{
		private bool _checkCertRevocation;

		private global::System.Net.Security.LocalCertificateSelectionCallback _clientCertSelectionCallback;

		private global::System.Security.Cryptography.X509Certificates.X509CertificateCollection _clientCerts;

		private global::System.Security.Authentication.SslProtocols _enabledSslProtocols;

		private global::System.Net.Security.RemoteCertificateValidationCallback _serverCertValidationCallback;

		private string _targetHost;

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

		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				return _clientCerts;
			}
			set
			{
				_clientCerts = value;
			}
		}

		public global::System.Net.Security.LocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			get
			{
				if (_clientCertSelectionCallback == null)
				{
					_clientCertSelectionCallback = defaultSelectClientCertificate;
				}
				return _clientCertSelectionCallback;
			}
			set
			{
				_clientCertSelectionCallback = value;
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

		public global::System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (_serverCertValidationCallback == null)
				{
					_serverCertValidationCallback = defaultValidateServerCertificate;
				}
				return _serverCertValidationCallback;
			}
			set
			{
				_serverCertValidationCallback = value;
			}
		}

		public string TargetHost
		{
			get
			{
				return _targetHost;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new global::System.ArgumentException("An empty string.", "value");
				}
				_targetHost = value;
			}
		}

		public ClientSslConfiguration(string targetHost)
		{
			if (targetHost == null)
			{
				throw new global::System.ArgumentNullException("targetHost");
			}
			if (targetHost.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "targetHost");
			}
			_targetHost = targetHost;
			_enabledSslProtocols = global::System.Security.Authentication.SslProtocols.None;
		}

		public ClientSslConfiguration(global::WebSocketSharp.Net.ClientSslConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new global::System.ArgumentNullException("configuration");
			}
			_checkCertRevocation = configuration._checkCertRevocation;
			_clientCertSelectionCallback = configuration._clientCertSelectionCallback;
			_clientCerts = configuration._clientCerts;
			_enabledSslProtocols = configuration._enabledSslProtocols;
			_serverCertValidationCallback = configuration._serverCertValidationCallback;
			_targetHost = configuration._targetHost;
		}

		private static global::System.Security.Cryptography.X509Certificates.X509Certificate defaultSelectClientCertificate(object sender, string targetHost, global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, global::System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, string[] acceptableIssuers)
		{
			return null;
		}

		private static bool defaultValidateServerCertificate(object sender, global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, global::System.Security.Cryptography.X509Certificates.X509Chain chain, global::System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}

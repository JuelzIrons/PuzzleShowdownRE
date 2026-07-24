namespace Unity.Netcode.Transports.UTP
{
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/api/Unity.Netcode.Transports.UTP.SecretsLoaderHelper.html")]
	public class SecretsLoaderHelper : global::UnityEngine.MonoBehaviour
	{
		internal struct ServerSecrets
		{
			public string ServerPrivate;

			public string ServerCertificate;
		}

		internal struct ClientSecrets
		{
			public string ServerCommonName;

			public string ClientCertificate;
		}

		[global::UnityEngine.Tooltip("Hostname")]
		[global::UnityEngine.SerializeField]
		private string m_ServerCommonName = "localhost";

		[global::UnityEngine.Tooltip("Client CA filepath. Useful with self-signed certificates")]
		[global::UnityEngine.SerializeField]
		private string m_ClientCAFilePath = "";

		[global::UnityEngine.Tooltip("Client CA Override. Only useful for development with self-signed certificates. Certificate content, for platforms that lack file access (WebGL)")]
		[global::UnityEngine.SerializeField]
		private string m_ClientCAOverride = "";

		[global::UnityEngine.Tooltip("Server Certificate filepath")]
		[global::UnityEngine.SerializeField]
		private string m_ServerCertificateFilePath = "";

		[global::UnityEngine.Tooltip("Server Private Key filepath")]
		[global::UnityEngine.SerializeField]
		private string m_ServerPrivateFilePath = "";

		private string m_ClientCA;

		private string m_ServerCertificate;

		private string m_ServerPrivate;

		public string ServerCommonName
		{
			get
			{
				return m_ServerCommonName;
			}
			set
			{
				m_ServerCommonName = value;
			}
		}

		public string ClientCAFilePath
		{
			get
			{
				return m_ClientCAFilePath;
			}
			set
			{
				m_ClientCAFilePath = value;
			}
		}

		public string ClientCAOverride
		{
			get
			{
				return m_ClientCAOverride;
			}
			set
			{
				m_ClientCAOverride = value;
			}
		}

		public string ServerCertificateFilePath
		{
			get
			{
				return m_ServerCertificateFilePath;
			}
			set
			{
				m_ServerCertificateFilePath = value;
			}
		}

		public string ServerPrivateFilePath
		{
			get
			{
				return m_ServerPrivateFilePath;
			}
			set
			{
				m_ServerPrivate = value;
			}
		}

		public string ClientCA
		{
			get
			{
				if (m_ClientCAOverride != "")
				{
					return m_ClientCAOverride;
				}
				return ReadFile(m_ClientCAFilePath, "Client Certificate");
			}
			set
			{
				m_ClientCA = value;
			}
		}

		public string ServerCertificate
		{
			get
			{
				return ReadFile(m_ServerCertificateFilePath, "Server Certificate");
			}
			set
			{
				m_ServerCertificate = value;
			}
		}

		public string ServerPrivate
		{
			get
			{
				return ReadFile(m_ServerPrivateFilePath, "Server Key");
			}
			set
			{
				m_ServerPrivate = value;
			}
		}

		private void Awake()
		{
			global::Unity.Netcode.Transports.UTP.SecretsLoaderHelper.ServerSecrets serverSecrets = default(global::Unity.Netcode.Transports.UTP.SecretsLoaderHelper.ServerSecrets);
			try
			{
				serverSecrets.ServerCertificate = ServerCertificate;
			}
			catch (global::System.Exception message)
			{
				global::UnityEngine.Debug.Log(message);
			}
			try
			{
				serverSecrets.ServerPrivate = ServerPrivate;
			}
			catch (global::System.Exception message2)
			{
				global::UnityEngine.Debug.Log(message2);
			}
			global::Unity.Netcode.Transports.UTP.SecretsLoaderHelper.ClientSecrets clientSecrets = default(global::Unity.Netcode.Transports.UTP.SecretsLoaderHelper.ClientSecrets);
			try
			{
				clientSecrets.ClientCertificate = ClientCA;
			}
			catch (global::System.Exception message3)
			{
				global::UnityEngine.Debug.Log(message3);
			}
			try
			{
				clientSecrets.ServerCommonName = ServerCommonName;
			}
			catch (global::System.Exception message4)
			{
				global::UnityEngine.Debug.Log(message4);
			}
			global::Unity.Netcode.Transports.UTP.UnityTransport component = GetComponent<global::Unity.Netcode.Transports.UTP.UnityTransport>();
			if (component == null)
			{
				global::UnityEngine.Debug.LogError("You need to select the UnityTransport protocol, in the NetworkManager, in order for the SecretsLoaderHelper component to be useful.");
				return;
			}
			component.SetServerSecrets(serverSecrets.ServerCertificate, serverSecrets.ServerPrivate);
			component.SetClientSecrets(clientSecrets.ServerCommonName, clientSecrets.ClientCertificate);
		}

		private static string ReadFile(string path, string label)
		{
			if (path == null || path == "")
			{
				return "";
			}
			string text = new global::System.IO.StreamReader(path).ReadToEnd();
			global::UnityEngine.Debug.Log((text.Length > 1) ? ("Successfully loaded " + text.Length + " byte(s) from " + label) : ("Could not read " + label + " file"));
			return text;
		}
	}
}

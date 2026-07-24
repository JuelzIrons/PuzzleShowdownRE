namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority
{
	public class ConnectionManager : global::UnityEngine.MonoBehaviour
	{
		public enum ConnectionState
		{
			Disconnected = 0,
			Connecting = 1,
			Connected = 2
		}

		[global::UnityEngine.SerializeField]
		private int m_MaxPlayers = 10;

		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		private global::Unity.Services.Multiplayer.ISession m_Session;

		public global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState State { get; private set; }

		private async void Awake()
		{
			m_NetworkManager = global::UnityEngine.Object.FindFirstObjectByType<global::Unity.Netcode.NetworkManager>();
			m_NetworkManager.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
			m_NetworkManager.OnClientConnectedCallback += OnClientConnectedCallback;
			await global::Unity.Services.Core.UnityServices.InitializeAsync();
		}

		public async void Disconnect()
		{
			if (m_Session != null)
			{
				await m_Session.LeaveAsync();
			}
			State = global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Disconnected;
		}

		public async global::System.Threading.Tasks.Task CreateOrJoinSessionAsync(string sessionName, string profileName)
		{
			if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(sessionName))
			{
				global::UnityEngine.Debug.LogError("Please provide a player and session name, to login.");
				return;
			}
			State = global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Connecting;
			try
			{
				if (!global::Unity.Services.Authentication.AuthenticationService.Instance.IsSignedIn)
				{
					global::Unity.Services.Authentication.AuthenticationService.Instance.SwitchProfile(profileName);
					await global::Unity.Services.Authentication.AuthenticationService.Instance.SignInAnonymouslyAsync();
				}
				global::Unity.Services.Multiplayer.SessionOptions sessionOptions = global::Unity.Services.Multiplayer.SessionOptionsExtensions.WithDistributedAuthorityNetwork(new global::Unity.Services.Multiplayer.SessionOptions
				{
					Name = sessionName,
					MaxPlayers = m_MaxPlayers
				});
				m_Session = await global::Unity.Services.Multiplayer.MultiplayerService.Instance.CreateOrJoinSessionAsync(sessionName, sessionOptions);
				State = global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Connected;
			}
			catch (global::System.Exception exception)
			{
				State = global::Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority.ConnectionManager.ConnectionState.Disconnected;
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		private void OnClientConnectedCallback(ulong clientId)
		{
			if (m_NetworkManager.LocalClientId == clientId)
			{
				global::UnityEngine.Debug.Log(string.Format("Client-{0} is connected and can spawn {1}s.", clientId, "NetworkObject"));
			}
		}

		private void OnSessionOwnerPromoted(ulong sessionOwnerPromoted)
		{
			if (m_NetworkManager.LocalClient.IsSessionOwner)
			{
				global::UnityEngine.Debug.Log($"Client-{m_NetworkManager.LocalClientId} is the session owner!");
			}
		}
	}
}

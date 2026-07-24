namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.CreateAssetMenu(fileName = "WidgetConfiguration", menuName = "Multiplayer/Widgets/WidgetConfiguration")]
	public class WidgetConfiguration : global::UnityEngine.ScriptableObject
	{
		public string Name;

		[global::UnityEngine.Serialization.FormerlySerializedAs("connectionType")]
		[global::UnityEngine.Header("Multiplayer Services Settings")]
		[global::UnityEngine.Header("Connection Settings")]
		public global::Unity.Multiplayer.Widgets.ConnectionType ConnectionType = global::Unity.Multiplayer.Widgets.ConnectionType.Relay;

		[global::UnityEngine.Serialization.FormerlySerializedAs("connectionMode")]
		[global::UnityEngine.Header("Direct Connection Settings")]
		public global::Unity.Multiplayer.Widgets.ConnectionMode ConnectionMode;

		[global::UnityEngine.Serialization.FormerlySerializedAs("IpAddress")]
		[global::UnityEngine.Tooltip("Listen for incoming connection at this address. This is the local IP address that the host should use. To listen on all interfaces Use 0.0.0.0 when using ConnectionMode.Publish. For a Listen connection the default allows for local testing.")]
		public string ListenIpAddress = "127.0.0.1";

		[global::UnityEngine.Tooltip("Address that clients should use when connecting. This is the external/public IP address that clients should use as the publish IP.")]
		public string PublishIpAddress = "127.0.0.1";

		[global::UnityEngine.Tooltip("0 selects a randomly available port on the machine and uses the chosen value as the publish port. If a non-zero value is used, the port number applies to both listen and publish addresses.")]
		public int Port;

		[global::UnityEngine.Tooltip("Custom NetworkHandler that is used during Session Creation. Can be null. If null, the default NetworkHandler from the Multiplayer Service Package is used.")]
		public global::Unity.Multiplayer.Widgets.CustomWidgetsNetworkHandler NetworkHandler;

		[global::UnityEngine.Header("Session Settings")]
		public int MaxPlayers = 4;

		[global::UnityEngine.Serialization.FormerlySerializedAs("JoinVoiceChannel")]
		[global::UnityEngine.Header("Voice Settings")]
		[global::UnityEngine.Tooltip("Automatically join a voice channel for a session when joining the session.")]
		public bool EnableVoiceChat;

		private void OnValidate()
		{
			bool flag = false;
			if (ConnectionType == global::Unity.Multiplayer.Widgets.ConnectionType.Unsupported)
			{
				ConnectionType = global::Unity.Multiplayer.Widgets.ConnectionType.DistributedAuthority;
				flag = true;
			}
		}
	}
}

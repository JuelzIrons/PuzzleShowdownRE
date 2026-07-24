namespace Unity.Services.Lobbies
{
	public struct ChangedOrRemovedLobbyValue<T>
	{
		public static readonly global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T> RemoveThisValue = new global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T>(default(T), global::Unity.Services.Lobbies.LobbyValueChangeType.Removed);

		public T Value { get; }

		public bool Removed => ChangeType == global::Unity.Services.Lobbies.LobbyValueChangeType.Removed;

		public bool Changed
		{
			get
			{
				if (ChangeType != global::Unity.Services.Lobbies.LobbyValueChangeType.Changed)
				{
					return ChangeType == global::Unity.Services.Lobbies.LobbyValueChangeType.Added;
				}
				return true;
			}
		}

		public bool Added => ChangeType == global::Unity.Services.Lobbies.LobbyValueChangeType.Added;

		public global::Unity.Services.Lobbies.LobbyValueChangeType ChangeType { get; }

		public ChangedOrRemovedLobbyValue(T value, global::Unity.Services.Lobbies.LobbyValueChangeType status)
		{
			Value = value;
			ChangeType = status;
		}
	}
}

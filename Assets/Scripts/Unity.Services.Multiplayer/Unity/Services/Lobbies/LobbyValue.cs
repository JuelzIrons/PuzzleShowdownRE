namespace Unity.Services.Lobbies
{
	public static class LobbyValue
	{
		public static global::Unity.Services.Lobbies.ChangedLobbyValue<T> Changed<T>(T value)
		{
			return new global::Unity.Services.Lobbies.ChangedLobbyValue<T>(value);
		}

		public static global::Unity.Services.Lobbies.ChangedLobbyValue<T> Added<T>(T value)
		{
			global::Unity.Services.Lobbies.ChangedLobbyValue<T> result = new global::Unity.Services.Lobbies.ChangedLobbyValue<T>(value);
			result.Added = true;
			return result;
		}

		public static global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T> ChangedNotRemoved<T>(T value)
		{
			return new global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T>(value, global::Unity.Services.Lobbies.LobbyValueChangeType.Changed);
		}

		public static global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T> ChangeAdded<T>(T value)
		{
			return new global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T>(value, global::Unity.Services.Lobbies.LobbyValueChangeType.Added);
		}

		public static global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T> Removed<T>()
		{
			return global::Unity.Services.Lobbies.ChangedOrRemovedLobbyValue<T>.RemoveThisValue;
		}
	}
}

namespace Unity.Services.Multiplayer
{
	public interface ISessionInfo
	{
		string Name { get; }

		string Id { get; }

		string Upid { get; }

		string HostId { get; }

		int AvailableSlots { get; }

		int MaxPlayers { get; }

		bool IsLocked { get; }

		bool HasPassword { get; }

		global::System.DateTime LastUpdated { get; }

		global::System.DateTime Created { get; }

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.SessionProperty> Properties { get; }
	}
}

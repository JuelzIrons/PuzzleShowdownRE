namespace Unity.Services.Multiplayer
{
	public interface IReadOnlyPlayer
	{
		string Id { get; }

		string AllocationId { get; }

		global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> Properties { get; }

		global::System.DateTime Joined { get; }

		global::System.DateTime LastUpdated { get; }

		internal global::Unity.Services.Multiplayer.ISession Session { get; }
	}
}

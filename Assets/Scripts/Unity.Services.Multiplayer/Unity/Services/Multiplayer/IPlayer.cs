namespace Unity.Services.Multiplayer
{
	public interface IPlayer : global::Unity.Services.Multiplayer.IReadOnlyPlayer
	{
		void SetAllocationId(string allocationId);

		void SetProperties(global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> properties);

		void SetProperty(string key, global::Unity.Services.Multiplayer.PlayerProperty property);
	}
}

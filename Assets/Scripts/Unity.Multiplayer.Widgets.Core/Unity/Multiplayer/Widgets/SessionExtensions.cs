namespace Unity.Multiplayer.Widgets
{
	internal static class SessionExtensions
	{
		internal static string GetPlayerName(this global::Unity.Services.Multiplayer.ISession session, string playerId)
		{
			foreach (global::Unity.Services.Multiplayer.IReadOnlyPlayer player in session.Players)
			{
				if (player.Id == playerId)
				{
					return player.Properties["w_PlayerName"].Value;
				}
			}
			return null;
		}
	}
}

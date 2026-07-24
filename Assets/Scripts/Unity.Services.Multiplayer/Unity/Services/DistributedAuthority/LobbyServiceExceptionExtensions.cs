namespace Unity.Services.DistributedAuthority
{
	internal static class LobbyServiceExceptionExtensions
	{
		private const string k_AlreadyMemberDetail = "already a member";

		public static bool IsAlreadyMemberError(this global::Unity.Services.Lobbies.LobbyServiceException lex)
		{
			if (lex.Reason == global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyConflict)
			{
				return lex.Message.Contains("already a member");
			}
			return false;
		}
	}
}

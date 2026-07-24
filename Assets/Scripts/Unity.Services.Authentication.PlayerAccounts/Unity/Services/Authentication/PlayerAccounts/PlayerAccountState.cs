namespace Unity.Services.Authentication.PlayerAccounts
{
	internal enum PlayerAccountState
	{
		SignedOut = 0,
		SigningIn = 1,
		Authorized = 2,
		Refreshing = 3,
		Expired = 4
	}
}

namespace Unity.Netcode
{
	public enum SendTo
	{
		Owner = 0,
		NotOwner = 1,
		Server = 2,
		NotServer = 3,
		Me = 4,
		NotMe = 5,
		Everyone = 6,
		ClientsAndHost = 7,
		Authority = 8,
		NotAuthority = 9,
		SpecifiedInParams = 10
	}
}

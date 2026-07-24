namespace Unity.Services.Authentication
{
	internal interface IProfile
	{
		string Current { get; set; }

		event global::System.Action<global::Unity.Services.Authentication.ProfileEventArgs> ProfileChange;
	}
}

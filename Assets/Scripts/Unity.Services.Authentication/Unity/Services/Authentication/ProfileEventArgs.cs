namespace Unity.Services.Authentication
{
	internal class ProfileEventArgs : global::System.EventArgs
	{
		public string Profile { get; }

		public ProfileEventArgs(string profile)
		{
			Profile = profile;
		}
	}
}

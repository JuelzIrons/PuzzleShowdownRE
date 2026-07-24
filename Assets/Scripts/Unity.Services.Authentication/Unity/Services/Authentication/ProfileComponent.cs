namespace Unity.Services.Authentication
{
	internal class ProfileComponent : global::Unity.Services.Authentication.IProfile
	{
		private string _current;

		public string Current
		{
			get
			{
				return _current;
			}
			set
			{
				SetProfile(value);
			}
		}

		public event global::System.Action<global::Unity.Services.Authentication.ProfileEventArgs> ProfileChange;

		internal ProfileComponent(string profile)
		{
			SetProfile(profile);
		}

		public void SetProfile(string profile)
		{
			_current = profile;
			try
			{
				this.ProfileChange?.Invoke(new global::Unity.Services.Authentication.ProfileEventArgs(_current));
			}
			catch (global::System.Exception exception)
			{
				global::Unity.Services.Authentication.Logger.LogException(exception);
			}
		}
	}
}

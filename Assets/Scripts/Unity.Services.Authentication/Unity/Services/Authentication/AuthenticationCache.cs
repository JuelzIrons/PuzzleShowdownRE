namespace Unity.Services.Authentication
{
	internal class AuthenticationCache : global::Unity.Services.Authentication.IAuthenticationCache, global::Unity.Services.Authentication.ICache
	{
		private global::Unity.Services.Core.Configuration.Internal.ICloudProjectId m_CloudProjectId;

		private global::Unity.Services.Authentication.IProfile m_Profile;

		public string CloudProjectId => m_CloudProjectId.GetCloudProjectId();

		public string Profile => m_Profile.Current;

		private string Prefix => CloudProjectId + "." + Profile + ".unity.services.authentication.";

		private string OldPrefix => "unity.services.authentication.";

		public AuthenticationCache(global::Unity.Services.Core.Configuration.Internal.ICloudProjectId cloudProjectId, global::Unity.Services.Authentication.IProfile profile)
		{
			m_CloudProjectId = cloudProjectId;
			m_Profile = profile;
		}

		public bool HasKey(string key)
		{
			return global::UnityEngine.PlayerPrefs.HasKey(GetKey(key));
		}

		public void DeleteKey(string key)
		{
			global::UnityEngine.PlayerPrefs.DeleteKey(GetKey(key));
		}

		[global::JetBrains.Annotations.CanBeNull]
		public string GetString(string key)
		{
			if (!HasKey(key))
			{
				return null;
			}
			return global::UnityEngine.PlayerPrefs.GetString(GetKey(key));
		}

		public void SetString(string key, string value)
		{
			global::UnityEngine.PlayerPrefs.SetString(GetKey(key), value);
			global::UnityEngine.PlayerPrefs.Save();
		}

		public void Migrate(string key)
		{
			string oldKey = GetOldKey(key);
			if (global::UnityEngine.PlayerPrefs.HasKey(oldKey))
			{
				global::UnityEngine.PlayerPrefs.SetString(GetKey(key), global::UnityEngine.PlayerPrefs.GetString(oldKey));
				global::UnityEngine.PlayerPrefs.DeleteKey(oldKey);
			}
		}

		internal string GetKey(string key)
		{
			return Prefix + key;
		}

		internal string GetOldKey(string key)
		{
			return OldPrefix + key;
		}
	}
}

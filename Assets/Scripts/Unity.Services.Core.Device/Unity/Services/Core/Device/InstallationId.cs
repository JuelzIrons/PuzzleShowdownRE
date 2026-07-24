namespace Unity.Services.Core.Device
{
	internal class InstallationId : global::Unity.Services.Core.Device.Internal.IInstallationId, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private const string k_UnityInstallationIdKey = "UnityInstallationId";

		internal string Identifier;

		internal global::Unity.Services.Core.Device.IUserIdentifierProvider UnityAdsIdentifierProvider;

		internal global::Unity.Services.Core.Device.IUserIdentifierProvider UnityAnalyticsIdentifierProvider;

		internal global::Unity.Services.Core.Device.IUserIdentifierProvider UnityEngineIdentifierProvider;

		public InstallationId()
		{
			UnityAdsIdentifierProvider = new global::Unity.Services.Core.Device.UnityAdsIdentifier();
			UnityAnalyticsIdentifierProvider = new global::Unity.Services.Core.Device.UnityAnalyticsIdentifier();
			UnityEngineIdentifierProvider = new global::Unity.Services.Core.Device.UnityEngineIdentifier();
		}

		public string GetOrCreateIdentifier()
		{
			if (string.IsNullOrEmpty(Identifier))
			{
				CreateIdentifier();
			}
			return Identifier;
		}

		public void CreateIdentifier()
		{
			Identifier = ReadIdentifierFromFile();
			if (!string.IsNullOrEmpty(Identifier))
			{
				return;
			}
			string userId = UnityEngineIdentifierProvider.UserId;
			if (!string.IsNullOrEmpty(userId))
			{
				Identifier = userId;
				WriteIdentifierToFile(Identifier);
				return;
			}
			string userId2 = UnityAnalyticsIdentifierProvider.UserId;
			string userId3 = UnityAdsIdentifierProvider.UserId;
			if (!string.IsNullOrEmpty(userId2))
			{
				Identifier = userId2;
			}
			else if (!string.IsNullOrEmpty(userId3))
			{
				Identifier = userId3;
			}
			else
			{
				Identifier = GenerateGuid();
			}
			WriteIdentifierToFile(Identifier);
			if (string.IsNullOrEmpty(userId2))
			{
				UnityAnalyticsIdentifierProvider.UserId = Identifier;
			}
			if (string.IsNullOrEmpty(userId3))
			{
				UnityAdsIdentifierProvider.UserId = Identifier;
			}
		}

		private static string ReadIdentifierFromFile()
		{
			return global::UnityEngine.PlayerPrefs.GetString("UnityInstallationId");
		}

		private static void WriteIdentifierToFile(string identifier)
		{
			global::UnityEngine.PlayerPrefs.SetString("UnityInstallationId", identifier);
			global::UnityEngine.PlayerPrefs.Save();
		}

		private static string GenerateGuid()
		{
			return global::System.Guid.NewGuid().ToString();
		}
	}
}

namespace Unity.Services.Core.Device
{
	internal class UnityAnalyticsIdentifier : global::Unity.Services.Core.Device.IUserIdentifierProvider
	{
		private const string k_PlayerUserIdKey = "unity.cloud_userid";

		public string UserId
		{
			get
			{
				return global::UnityEngine.PlayerPrefs.GetString("unity.cloud_userid");
			}
			set
			{
				try
				{
					global::UnityEngine.PlayerPrefs.SetString("unity.cloud_userid", value);
					global::UnityEngine.PlayerPrefs.Save();
				}
				catch (global::System.Exception)
				{
				}
			}
		}
	}
}

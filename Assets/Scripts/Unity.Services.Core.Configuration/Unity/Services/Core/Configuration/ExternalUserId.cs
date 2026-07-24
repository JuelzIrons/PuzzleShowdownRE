namespace Unity.Services.Core.Configuration
{
	internal class ExternalUserId : global::Unity.Services.Core.Configuration.Internal.IExternalUserId, global::Unity.Services.Core.Internal.IServiceComponent
	{
		public string UserId => global::Unity.Services.Core.UnityServices.ExternalUserIdProperty.UserId;

		public event global::System.Action<string> UserIdChanged
		{
			add
			{
				global::Unity.Services.Core.UnityServices.ExternalUserIdProperty.UserIdChanged += value;
			}
			remove
			{
				global::Unity.Services.Core.UnityServices.ExternalUserIdProperty.UserIdChanged -= value;
			}
		}
	}
}

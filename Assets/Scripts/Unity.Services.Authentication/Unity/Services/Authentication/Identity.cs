namespace Unity.Services.Authentication
{
	public sealed class Identity
	{
		public string TypeId;

		public string UserId;

		internal Identity(global::Unity.Services.Authentication.ExternalIdentity externalIdentity)
		{
			if (externalIdentity != null)
			{
				TypeId = externalIdentity.ProviderId;
				UserId = externalIdentity.ExternalId;
			}
		}
	}
}

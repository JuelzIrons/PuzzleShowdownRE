namespace Unity.Services.Core.Device
{
	internal class UnityEngineIdentifier : global::Unity.Services.Core.Device.IUserIdentifierProvider
	{
		public string UserId
		{
			get
			{
				return global::UnityEngine.Identifiers.Identifiers.installationId;
			}
			set
			{
			}
		}
	}
}

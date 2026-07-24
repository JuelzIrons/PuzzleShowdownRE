namespace Unity.Services.Core.Configuration
{
	internal class CloudProjectId : global::Unity.Services.Core.Configuration.Internal.ICloudProjectId, global::Unity.Services.Core.Internal.IServiceComponent
	{
		public string GetCloudProjectId()
		{
			return global::UnityEngine.Application.cloudProjectId;
		}
	}
}

namespace Unity.Services.Core
{
	internal static class UnityServicesBuilder
	{
		internal delegate global::Unity.Services.Core.IUnityServices CreationDelegate(string servicesId);

		internal static global::Unity.Services.Core.UnityServicesBuilder.CreationDelegate InstanceCreationDelegate { get; set; }

		public static global::Unity.Services.Core.IUnityServices Create(string servicesId)
		{
			if (InstanceCreationDelegate == null)
			{
				throw new global::Unity.Services.Core.ServicesCreationException("Error creating services. The creation delegate has not been initialized.");
			}
			return InstanceCreationDelegate(servicesId);
		}
	}
}

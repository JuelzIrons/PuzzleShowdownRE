namespace Unity.Services.Core
{
	public static class UnityServicesExtensions
	{
		public static global::Unity.Services.Authentication.IAuthenticationService GetAuthenticationService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Authentication.IAuthenticationService>();
		}
	}
}

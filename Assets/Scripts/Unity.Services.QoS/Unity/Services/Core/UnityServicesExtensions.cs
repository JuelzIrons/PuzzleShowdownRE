namespace Unity.Services.Core
{
	public static class UnityServicesExtensions
	{
		public static global::Unity.Services.Qos.IQosService GetQosService(this global::Unity.Services.Core.IUnityServices unityServices)
		{
			return unityServices.GetService<global::Unity.Services.Qos.IQosService>();
		}
	}
}

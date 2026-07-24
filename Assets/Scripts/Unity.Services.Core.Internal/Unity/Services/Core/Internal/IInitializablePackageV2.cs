namespace Unity.Services.Core.Internal
{
	public interface IInitializablePackageV2 : global::Unity.Services.Core.Internal.IInitializablePackage
	{
		void Register(global::Unity.Services.Core.Internal.CorePackageRegistry registry);

		global::System.Threading.Tasks.Task InitializeInstanceAsync(global::Unity.Services.Core.Internal.CoreRegistry registry);
	}
}

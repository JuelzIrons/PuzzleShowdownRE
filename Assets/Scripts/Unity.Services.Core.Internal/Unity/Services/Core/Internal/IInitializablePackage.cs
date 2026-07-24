namespace Unity.Services.Core.Internal
{
	public interface IInitializablePackage
	{
		global::System.Threading.Tasks.Task Initialize(global::Unity.Services.Core.Internal.CoreRegistry registry);
	}
}

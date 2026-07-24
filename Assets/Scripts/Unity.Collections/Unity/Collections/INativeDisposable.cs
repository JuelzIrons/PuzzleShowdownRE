namespace Unity.Collections
{
	public interface INativeDisposable : global::System.IDisposable
	{
		global::Unity.Jobs.JobHandle Dispose(global::Unity.Jobs.JobHandle inputDeps);
	}
}

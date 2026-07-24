namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public interface IRayTracingAccelStruct : global::System.IDisposable
	{
		int AddInstance(global::UnityEngine.Rendering.UnifiedRayTracing.MeshInstanceDesc meshInstance);

		void RemoveInstance(int instanceHandle);

		void ClearInstances();

		void UpdateInstanceTransform(int instanceHandle, global::UnityEngine.Matrix4x4 localToWorldMatrix);

		void UpdateInstanceID(int instanceHandle, uint instanceID);

		void UpdateInstanceMask(int instanceHandle, uint mask);

		void Build(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer);

		ulong GetBuildScratchBufferRequiredSizeInBytes();
	}
}

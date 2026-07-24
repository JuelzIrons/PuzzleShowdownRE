namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public interface IRayTracingShader
	{
		void SetAccelerationStructure(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct);

		void SetIntParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, int val);

		void SetFloatParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, float val);

		void SetVectorParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Vector4 val);

		void SetMatrixParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Matrix4x4 val);

		void SetTextureParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer);

		void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer);

		void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, uint width, uint height, uint depth);

		void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, global::UnityEngine.GraphicsBuffer argsBuffer);

		void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size);

		void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size);

		ulong GetTraceScratchBufferRequiredSizeInBytes(uint width, uint height, uint depth);

		global::Unity.Mathematics.uint3 GetThreadGroupSizes();
	}
}

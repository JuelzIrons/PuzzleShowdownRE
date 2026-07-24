namespace UnityEngine.Rendering
{
	public interface IComputeCommandBuffer : global::UnityEngine.Rendering.IBaseCommandBuffer
	{
		void SetComputeFloatParam(global::UnityEngine.ComputeShader computeShader, int nameID, float val);

		void SetComputeIntParam(global::UnityEngine.ComputeShader computeShader, int nameID, int val);

		void SetComputeVectorParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Vector4 val);

		void SetComputeVectorArrayParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Vector4[] values);

		void SetComputeMatrixParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Matrix4x4 val);

		void SetComputeMatrixArrayParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Matrix4x4[] values);

		void SetRayTracingShaderPass(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string passName);

		void SetBufferData(global::UnityEngine.ComputeBuffer buffer, global::System.Array data);

		void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::System.Collections.Generic.List<T> data) where T : struct;

		void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::Unity.Collections.NativeArray<T> data) where T : struct;

		void SetBufferData(global::UnityEngine.ComputeBuffer buffer, global::System.Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count);

		void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::System.Collections.Generic.List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::Unity.Collections.NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		void SetBufferCounterValue(global::UnityEngine.ComputeBuffer buffer, uint counterValue);

		void SetBufferData(global::UnityEngine.GraphicsBuffer buffer, global::System.Array data);

		void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::System.Collections.Generic.List<T> data) where T : struct;

		void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::Unity.Collections.NativeArray<T> data) where T : struct;

		void SetBufferData(global::UnityEngine.GraphicsBuffer buffer, global::System.Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count);

		void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::System.Collections.Generic.List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::Unity.Collections.NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct;

		void SetBufferCounterValue(global::UnityEngine.GraphicsBuffer buffer, uint counterValue);

		void SetComputeFloatParam(global::UnityEngine.ComputeShader computeShader, string name, float val);

		void SetComputeIntParam(global::UnityEngine.ComputeShader computeShader, string name, int val);

		void SetComputeVectorParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Vector4 val);

		void SetComputeVectorArrayParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Vector4[] values);

		void SetComputeMatrixParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Matrix4x4 val);

		void SetComputeMatrixArrayParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Matrix4x4[] values);

		void SetComputeFloatParams(global::UnityEngine.ComputeShader computeShader, string name, params float[] values);

		void SetComputeFloatParams(global::UnityEngine.ComputeShader computeShader, int nameID, params float[] values);

		void SetComputeIntParams(global::UnityEngine.ComputeShader computeShader, string name, params int[] values);

		void SetComputeIntParams(global::UnityEngine.ComputeShader computeShader, int nameID, params int[] values);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.ComputeBuffer buffer);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.ComputeBuffer buffer);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.GraphicsBufferHandle bufferHandle);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.GraphicsBufferHandle bufferHandle);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.GraphicsBuffer buffer);

		void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.GraphicsBuffer buffer);

		void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size);

		void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.ComputeBuffer buffer, int offset, int size);

		void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size);

		void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.GraphicsBuffer buffer, int offset, int size);

		void SetComputeParamsFromMaterial(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.Material material);

		void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.ComputeBuffer indirectBuffer, uint argsOffset);

		void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.GraphicsBuffer indirectBuffer, uint argsOffset);

		void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure);

		void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure, global::UnityEngine.Vector3 relativeOrigin);

		void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure, global::UnityEngine.Rendering.RayTracingAccelerationStructure.BuildSettings buildSettings);

		void SetRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure);

		void SetRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure);

		void SetRayTracingAccelerationStructure(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure);

		void SetRayTracingAccelerationStructure(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.ComputeBuffer buffer);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.ComputeBuffer buffer);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBuffer buffer);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBuffer buffer);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBufferHandle bufferHandle);

		void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBufferHandle bufferHandle);

		void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size);

		void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.ComputeBuffer buffer, int offset, int size);

		void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size);

		void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBuffer buffer, int offset, int size);

		void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt);

		void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt);

		void SetRayTracingFloatParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, float val);

		void SetRayTracingFloatParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, float val);

		void SetRayTracingFloatParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params float[] values);

		void SetRayTracingFloatParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params float[] values);

		void SetRayTracingIntParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, int val);

		void SetRayTracingIntParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, int val);

		void SetRayTracingIntParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params int[] values);

		void SetRayTracingIntParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params int[] values);

		void SetRayTracingVectorParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Vector4 val);

		void SetRayTracingVectorParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Vector4 val);

		void SetRayTracingVectorArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params global::UnityEngine.Vector4[] values);

		void SetRayTracingVectorArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params global::UnityEngine.Vector4[] values);

		void SetRayTracingMatrixParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Matrix4x4 val);

		void SetRayTracingMatrixParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Matrix4x4 val);

		void SetRayTracingMatrixArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params global::UnityEngine.Matrix4x4[] values);

		void SetRayTracingMatrixArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params global::UnityEngine.Matrix4x4[] values);

		void DispatchRays(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, global::UnityEngine.Camera camera);

		void DispatchRays(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string rayGenName, global::UnityEngine.GraphicsBuffer argsBuffer, uint argsOffset, global::UnityEngine.Camera camera);

		void CopyCounterValue(global::UnityEngine.ComputeBuffer src, global::UnityEngine.ComputeBuffer dst, uint dstOffsetBytes);

		void CopyCounterValue(global::UnityEngine.GraphicsBuffer src, global::UnityEngine.ComputeBuffer dst, uint dstOffsetBytes);

		void CopyCounterValue(global::UnityEngine.ComputeBuffer src, global::UnityEngine.GraphicsBuffer dst, uint dstOffsetBytes);

		void CopyCounterValue(global::UnityEngine.GraphicsBuffer src, global::UnityEngine.GraphicsBuffer dst, uint dstOffsetBytes);
	}
}

namespace UnityEngine.Rendering
{
	public class ComputeCommandBuffer : global::UnityEngine.Rendering.BaseCommandBuffer, global::UnityEngine.Rendering.IComputeCommandBuffer, global::UnityEngine.Rendering.IBaseCommandBuffer
	{
		internal ComputeCommandBuffer(global::UnityEngine.Rendering.CommandBuffer wrapped, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass executingPass, bool isAsync)
			: base(wrapped, executingPass, isAsync)
		{
		}

		public void SetInvertCulling(bool invertCulling)
		{
			m_WrappedCommandBuffer.SetInvertCulling(invertCulling);
		}

		public void SetComputeFloatParam(global::UnityEngine.ComputeShader computeShader, int nameID, float val)
		{
			m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, nameID, val);
		}

		public void SetComputeIntParam(global::UnityEngine.ComputeShader computeShader, int nameID, int val)
		{
			m_WrappedCommandBuffer.SetComputeIntParam(computeShader, nameID, val);
		}

		public void SetComputeVectorParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Vector4 val)
		{
			m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, nameID, val);
		}

		public void SetComputeVectorArrayParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, nameID, values);
		}

		public void SetComputeMatrixParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Matrix4x4 val)
		{
			m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, nameID, val);
		}

		public void SetComputeMatrixArrayParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, nameID, values);
		}

		public void SetRayTracingShaderPass(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string passName)
		{
			m_WrappedCommandBuffer.SetRayTracingShaderPass(rayTracingShader, passName);
		}

		public void SetViewport(global::UnityEngine.Rect pixelRect)
		{
			m_WrappedCommandBuffer.SetViewport(pixelRect);
		}

		public void EnableScissorRect(global::UnityEngine.Rect scissor)
		{
			m_WrappedCommandBuffer.EnableScissorRect(scissor);
		}

		public void DisableScissorRect()
		{
			m_WrappedCommandBuffer.DisableScissorRect();
		}

		public void SetGlobalFloat(int nameID, float value)
		{
			m_WrappedCommandBuffer.SetGlobalFloat(nameID, value);
		}

		public void SetGlobalInt(int nameID, int value)
		{
			m_WrappedCommandBuffer.SetGlobalInt(nameID, value);
		}

		public void SetGlobalInteger(int nameID, int value)
		{
			m_WrappedCommandBuffer.SetGlobalInteger(nameID, value);
		}

		public void SetGlobalVector(int nameID, global::UnityEngine.Vector4 value)
		{
			m_WrappedCommandBuffer.SetGlobalVector(nameID, value);
		}

		public void SetGlobalColor(int nameID, global::UnityEngine.Color value)
		{
			m_WrappedCommandBuffer.SetGlobalColor(nameID, value);
		}

		public void SetGlobalMatrix(int nameID, global::UnityEngine.Matrix4x4 value)
		{
			m_WrappedCommandBuffer.SetGlobalMatrix(nameID, value);
		}

		public void EnableShaderKeyword(string keyword)
		{
			m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
		}

		public void EnableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword)
		{
			m_WrappedCommandBuffer.EnableKeyword(in keyword);
		}

		public void EnableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			m_WrappedCommandBuffer.EnableKeyword(material, in keyword);
		}

		public void EnableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			m_WrappedCommandBuffer.EnableKeyword(computeShader, in keyword);
		}

		public void DisableShaderKeyword(string keyword)
		{
			m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
		}

		public void DisableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword)
		{
			m_WrappedCommandBuffer.DisableKeyword(in keyword);
		}

		public void DisableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			m_WrappedCommandBuffer.DisableKeyword(material, in keyword);
		}

		public void DisableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			m_WrappedCommandBuffer.DisableKeyword(computeShader, in keyword);
		}

		public void SetKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword, bool value)
		{
			m_WrappedCommandBuffer.SetKeyword(in keyword, value);
		}

		public void SetKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value)
		{
			m_WrappedCommandBuffer.SetKeyword(material, in keyword, value);
		}

		public void SetKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value)
		{
			m_WrappedCommandBuffer.SetKeyword(computeShader, in keyword, value);
		}

		public void SetViewProjectionMatrices(global::UnityEngine.Matrix4x4 view, global::UnityEngine.Matrix4x4 proj)
		{
			m_WrappedCommandBuffer.SetViewProjectionMatrices(view, proj);
		}

		public void SetGlobalDepthBias(float bias, float slopeBias)
		{
			m_WrappedCommandBuffer.SetGlobalDepthBias(bias, slopeBias);
		}

		public void SetGlobalFloatArray(int nameID, float[] values)
		{
			m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		public void SetGlobalVectorArray(int nameID, global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		public void SetGlobalMatrixArray(int nameID, global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		public void SetLateLatchProjectionMatrices(global::UnityEngine.Matrix4x4[] projectionMat)
		{
			m_WrappedCommandBuffer.SetLateLatchProjectionMatrices(projectionMat);
		}

		public void MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID)
		{
			m_WrappedCommandBuffer.MarkLateLatchMatrixShaderPropertyID(matrixPropertyType, shaderPropertyID);
		}

		public void UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType matrixPropertyType)
		{
			m_WrappedCommandBuffer.UnmarkLateLatchMatrix(matrixPropertyType);
		}

		public void BeginSample(string name)
		{
			m_WrappedCommandBuffer.BeginSample(name);
		}

		public void EndSample(string name)
		{
			m_WrappedCommandBuffer.EndSample(name);
		}

		public void BeginSample(global::UnityEngine.Profiling.CustomSampler sampler)
		{
			m_WrappedCommandBuffer.BeginSample(sampler);
		}

		public void EndSample(global::UnityEngine.Profiling.CustomSampler sampler)
		{
			m_WrappedCommandBuffer.EndSample(sampler);
		}

		public void BeginSample(global::Unity.Profiling.ProfilerMarker marker)
		{
		}

		public void EndSample(global::Unity.Profiling.ProfilerMarker marker)
		{
		}

		public void IncrementUpdateCount(global::UnityEngine.Rendering.RenderTargetIdentifier dest)
		{
			m_WrappedCommandBuffer.IncrementUpdateCount(dest);
		}

		public void SetBufferData(global::UnityEngine.ComputeBuffer buffer, global::System.Array data)
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::System.Collections.Generic.List<T> data) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::Unity.Collections.NativeArray<T> data) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData(global::UnityEngine.ComputeBuffer buffer, global::System.Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::System.Collections.Generic.List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferData<T>(global::UnityEngine.ComputeBuffer buffer, global::Unity.Collections.NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferCounterValue(global::UnityEngine.ComputeBuffer buffer, uint counterValue)
		{
			m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		public void SetBufferData(global::UnityEngine.GraphicsBuffer buffer, global::System.Array data)
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::System.Collections.Generic.List<T> data) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::Unity.Collections.NativeArray<T> data) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data);
		}

		public void SetBufferData(global::UnityEngine.GraphicsBuffer buffer, global::System.Array data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count)
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::System.Collections.Generic.List<T> data, int managedBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, managedBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferData<T>(global::UnityEngine.GraphicsBuffer buffer, global::Unity.Collections.NativeArray<T> data, int nativeBufferStartIndex, int graphicsBufferStartIndex, int count) where T : struct
		{
			m_WrappedCommandBuffer.SetBufferData(buffer, data, nativeBufferStartIndex, graphicsBufferStartIndex, count);
		}

		public void SetBufferCounterValue(global::UnityEngine.GraphicsBuffer buffer, uint counterValue)
		{
			m_WrappedCommandBuffer.SetBufferCounterValue(buffer, counterValue);
		}

		public void SetupCameraProperties(global::UnityEngine.Camera camera)
		{
			m_WrappedCommandBuffer.SetupCameraProperties(camera);
		}

		public void InvokeOnRenderObjectCallbacks()
		{
			m_WrappedCommandBuffer.InvokeOnRenderObjectCallbacks();
		}

		public void SetComputeFloatParam(global::UnityEngine.ComputeShader computeShader, string name, float val)
		{
			m_WrappedCommandBuffer.SetComputeFloatParam(computeShader, name, val);
		}

		public void SetComputeIntParam(global::UnityEngine.ComputeShader computeShader, string name, int val)
		{
			m_WrappedCommandBuffer.SetComputeIntParam(computeShader, name, val);
		}

		public void SetComputeVectorParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Vector4 val)
		{
			m_WrappedCommandBuffer.SetComputeVectorParam(computeShader, name, val);
		}

		public void SetComputeVectorArrayParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetComputeVectorArrayParam(computeShader, name, values);
		}

		public void SetComputeMatrixParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Matrix4x4 val)
		{
			m_WrappedCommandBuffer.SetComputeMatrixParam(computeShader, name, val);
		}

		public void SetComputeMatrixArrayParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetComputeMatrixArrayParam(computeShader, name, values);
		}

		public void SetComputeFloatParams(global::UnityEngine.ComputeShader computeShader, string name, params float[] values)
		{
			m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, name, values);
		}

		public void SetComputeFloatParams(global::UnityEngine.ComputeShader computeShader, int nameID, params float[] values)
		{
			m_WrappedCommandBuffer.SetComputeFloatParams(computeShader, nameID, values);
		}

		public void SetComputeIntParams(global::UnityEngine.ComputeShader computeShader, string name, params int[] values)
		{
			m_WrappedCommandBuffer.SetComputeIntParams(computeShader, name, values);
		}

		public void SetComputeIntParams(global::UnityEngine.ComputeShader computeShader, int nameID, params int[] values)
		{
			m_WrappedCommandBuffer.SetComputeIntParams(computeShader, nameID, values);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, name, rt, mipLevel, element);
		}

		public void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element)
		{
			m_WrappedCommandBuffer.SetComputeTextureParam(computeShader, kernelIndex, nameID, rt, mipLevel, element);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.ComputeBuffer buffer)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.ComputeBuffer buffer)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.GraphicsBufferHandle bufferHandle)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, bufferHandle);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.GraphicsBufferHandle bufferHandle)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, bufferHandle);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.GraphicsBuffer buffer)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, nameID, buffer);
		}

		public void SetComputeBufferParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.GraphicsBuffer buffer)
		{
			m_WrappedCommandBuffer.SetComputeBufferParam(computeShader, kernelIndex, name, buffer);
		}

		public void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		public void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		public void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, nameID, buffer, offset, size);
		}

		public void SetComputeConstantBufferParam(global::UnityEngine.ComputeShader computeShader, string name, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetComputeConstantBufferParam(computeShader, name, buffer, offset, size);
		}

		public void SetComputeParamsFromMaterial(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.Material material)
		{
			m_WrappedCommandBuffer.SetComputeParamsFromMaterial(computeShader, kernelIndex, material);
		}

		public void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		public void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.ComputeBuffer indirectBuffer, uint argsOffset)
		{
			m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		public void DispatchCompute(global::UnityEngine.ComputeShader computeShader, int kernelIndex, global::UnityEngine.GraphicsBuffer indirectBuffer, uint argsOffset)
		{
			m_WrappedCommandBuffer.DispatchCompute(computeShader, kernelIndex, indirectBuffer, argsOffset);
		}

		public void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure)
		{
			m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure);
		}

		public void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure, global::UnityEngine.Vector3 relativeOrigin)
		{
			m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure, relativeOrigin);
		}

		public void BuildRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingAccelerationStructure accelerationStructure, global::UnityEngine.Rendering.RayTracingAccelerationStructure.BuildSettings buildSettings)
		{
			m_WrappedCommandBuffer.BuildRayTracingAccelerationStructure(accelerationStructure, buildSettings);
		}

		public void SetRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, name, rayTracingAccelerationStructure);
		}

		public void SetRayTracingAccelerationStructure(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(rayTracingShader, nameID, rayTracingAccelerationStructure);
		}

		public void SetRayTracingAccelerationStructure(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, name, rayTracingAccelerationStructure);
		}

		public void SetRayTracingAccelerationStructure(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RayTracingAccelerationStructure rayTracingAccelerationStructure)
		{
			m_WrappedCommandBuffer.SetRayTracingAccelerationStructure(computeShader, kernelIndex, nameID, rayTracingAccelerationStructure);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.ComputeBuffer buffer)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.ComputeBuffer buffer)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBuffer buffer)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, buffer);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBuffer buffer)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, buffer);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBufferHandle bufferHandle)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, name, bufferHandle);
		}

		public void SetRayTracingBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBufferHandle bufferHandle)
		{
			m_WrappedCommandBuffer.SetRayTracingBufferParam(rayTracingShader, nameID, bufferHandle);
		}

		public void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		public void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		public void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, nameID, buffer, offset, size);
		}

		public void SetRayTracingConstantBufferParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			m_WrappedCommandBuffer.SetRayTracingConstantBufferParam(rayTracingShader, name, buffer, offset, size);
		}

		public void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt)
		{
			m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, name, rt);
		}

		public void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle rt)
		{
			m_WrappedCommandBuffer.SetRayTracingTextureParam(rayTracingShader, nameID, rt);
		}

		public void SetRayTracingFloatParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, float val)
		{
			m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, name, val);
		}

		public void SetRayTracingFloatParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, float val)
		{
			m_WrappedCommandBuffer.SetRayTracingFloatParam(rayTracingShader, nameID, val);
		}

		public void SetRayTracingFloatParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params float[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, name, values);
		}

		public void SetRayTracingFloatParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params float[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingFloatParams(rayTracingShader, nameID, values);
		}

		public void SetRayTracingIntParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, int val)
		{
			m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, name, val);
		}

		public void SetRayTracingIntParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, int val)
		{
			m_WrappedCommandBuffer.SetRayTracingIntParam(rayTracingShader, nameID, val);
		}

		public void SetRayTracingIntParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params int[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, name, values);
		}

		public void SetRayTracingIntParams(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params int[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingIntParams(rayTracingShader, nameID, values);
		}

		public void SetRayTracingVectorParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Vector4 val)
		{
			m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, name, val);
		}

		public void SetRayTracingVectorParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Vector4 val)
		{
			m_WrappedCommandBuffer.SetRayTracingVectorParam(rayTracingShader, nameID, val);
		}

		public void SetRayTracingVectorArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, name, values);
		}

		public void SetRayTracingVectorArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingVectorArrayParam(rayTracingShader, nameID, values);
		}

		public void SetRayTracingMatrixParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Matrix4x4 val)
		{
			m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, name, val);
		}

		public void SetRayTracingMatrixParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Matrix4x4 val)
		{
			m_WrappedCommandBuffer.SetRayTracingMatrixParam(rayTracingShader, nameID, val);
		}

		public void SetRayTracingMatrixArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, params global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, name, values);
		}

		public void SetRayTracingMatrixArrayParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, params global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetRayTracingMatrixArrayParam(rayTracingShader, nameID, values);
		}

		public void DispatchRays(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string rayGenName, uint width, uint height, uint depth, global::UnityEngine.Camera camera)
		{
			m_WrappedCommandBuffer.DispatchRays(rayTracingShader, rayGenName, width, height, depth, camera);
		}

		public void DispatchRays(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string rayGenName, global::UnityEngine.GraphicsBuffer argsBuffer, uint argsOffset, global::UnityEngine.Camera camera)
		{
			m_WrappedCommandBuffer.DispatchRays(rayTracingShader, rayGenName, argsBuffer, argsOffset, camera);
		}

		public void CopyCounterValue(global::UnityEngine.ComputeBuffer src, global::UnityEngine.ComputeBuffer dst, uint dstOffsetBytes)
		{
			m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		public void CopyCounterValue(global::UnityEngine.GraphicsBuffer src, global::UnityEngine.ComputeBuffer dst, uint dstOffsetBytes)
		{
			m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		public void CopyCounterValue(global::UnityEngine.ComputeBuffer src, global::UnityEngine.GraphicsBuffer dst, uint dstOffsetBytes)
		{
			m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		public void CopyCounterValue(global::UnityEngine.GraphicsBuffer src, global::UnityEngine.GraphicsBuffer dst, uint dstOffsetBytes)
		{
			m_WrappedCommandBuffer.CopyCounterValue(src, dst, dstOffsetBytes);
		}

		public void SetGlobalFloat(string name, float value)
		{
			m_WrappedCommandBuffer.SetGlobalFloat(name, value);
		}

		public void SetGlobalInt(string name, int value)
		{
			m_WrappedCommandBuffer.SetGlobalInt(name, value);
		}

		public void SetGlobalInteger(string name, int value)
		{
			m_WrappedCommandBuffer.SetGlobalInteger(name, value);
		}

		public void SetGlobalVector(string name, global::UnityEngine.Vector4 value)
		{
			m_WrappedCommandBuffer.SetGlobalVector(name, value);
		}

		public void SetGlobalColor(string name, global::UnityEngine.Color value)
		{
			m_WrappedCommandBuffer.SetGlobalColor(name, value);
		}

		public void SetGlobalMatrix(string name, global::UnityEngine.Matrix4x4 value)
		{
			m_WrappedCommandBuffer.SetGlobalMatrix(name, value);
		}

		public void SetGlobalFloatArray(string propertyName, global::System.Collections.Generic.List<float> values)
		{
			m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		public void SetGlobalFloatArray(int nameID, global::System.Collections.Generic.List<float> values)
		{
			m_WrappedCommandBuffer.SetGlobalFloatArray(nameID, values);
		}

		public void SetGlobalFloatArray(string propertyName, float[] values)
		{
			m_WrappedCommandBuffer.SetGlobalFloatArray(propertyName, values);
		}

		public void SetGlobalVectorArray(string propertyName, global::System.Collections.Generic.List<global::UnityEngine.Vector4> values)
		{
			m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		public void SetGlobalVectorArray(int nameID, global::System.Collections.Generic.List<global::UnityEngine.Vector4> values)
		{
			m_WrappedCommandBuffer.SetGlobalVectorArray(nameID, values);
		}

		public void SetGlobalVectorArray(string propertyName, global::UnityEngine.Vector4[] values)
		{
			m_WrappedCommandBuffer.SetGlobalVectorArray(propertyName, values);
		}

		public void SetGlobalMatrixArray(string propertyName, global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> values)
		{
			m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		public void SetGlobalMatrixArray(int nameID, global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> values)
		{
			m_WrappedCommandBuffer.SetGlobalMatrixArray(nameID, values);
		}

		public void SetGlobalMatrixArray(string propertyName, global::UnityEngine.Matrix4x4[] values)
		{
			m_WrappedCommandBuffer.SetGlobalMatrixArray(propertyName, values);
		}

		public void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value)
		{
			m_WrappedCommandBuffer.SetGlobalTexture(name, value);
		}

		public void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value)
		{
			m_WrappedCommandBuffer.SetGlobalTexture(nameID, value);
		}

		public void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value, global::UnityEngine.Rendering.RenderTextureSubElement element)
		{
			m_WrappedCommandBuffer.SetGlobalTexture(name, value, element);
		}

		public void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value, global::UnityEngine.Rendering.RenderTextureSubElement element)
		{
			m_WrappedCommandBuffer.SetGlobalTexture(nameID, value, element);
		}

		public void SetGlobalBuffer(string name, global::UnityEngine.ComputeBuffer value)
		{
			m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		public void SetGlobalBuffer(int nameID, global::UnityEngine.ComputeBuffer value)
		{
			m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		public void SetGlobalBuffer(string name, global::UnityEngine.GraphicsBuffer value)
		{
			m_WrappedCommandBuffer.SetGlobalBuffer(name, value);
		}

		public void SetGlobalBuffer(int nameID, global::UnityEngine.GraphicsBuffer value)
		{
			m_WrappedCommandBuffer.SetGlobalBuffer(nameID, value);
		}

		public void SetGlobalConstantBuffer(global::UnityEngine.ComputeBuffer buffer, int nameID, int offset, int size)
		{
			m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		public void SetGlobalConstantBuffer(global::UnityEngine.ComputeBuffer buffer, string name, int offset, int size)
		{
			m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		public void SetGlobalConstantBuffer(global::UnityEngine.GraphicsBuffer buffer, int nameID, int offset, int size)
		{
			m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, nameID, offset, size);
		}

		public void SetGlobalConstantBuffer(global::UnityEngine.GraphicsBuffer buffer, string name, int offset, int size)
		{
			m_WrappedCommandBuffer.SetGlobalConstantBuffer(buffer, name, offset, size);
		}

		public void SetShadowSamplingMode(global::UnityEngine.Rendering.RenderTargetIdentifier shadowmap, global::UnityEngine.Rendering.ShadowSamplingMode mode)
		{
			m_WrappedCommandBuffer.SetShadowSamplingMode(shadowmap, mode);
		}

		public void SetSinglePassStereo(global::UnityEngine.Rendering.SinglePassStereoMode mode)
		{
			m_WrappedCommandBuffer.SetSinglePassStereo(mode);
		}

		public void IssuePluginEvent(global::System.IntPtr callback, int eventID)
		{
			m_WrappedCommandBuffer.IssuePluginEvent(callback, eventID);
		}

		public void IssuePluginEventAndData(global::System.IntPtr callback, int eventID, global::System.IntPtr data)
		{
			m_WrappedCommandBuffer.IssuePluginEventAndData(callback, eventID, data);
		}

		public void IssuePluginCustomBlit(global::System.IntPtr callback, uint command, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Rendering.RenderTargetIdentifier dest, uint commandParam, uint commandFlags)
		{
			m_WrappedCommandBuffer.IssuePluginCustomBlit(callback, command, source, dest, commandParam, commandFlags);
		}

		public void IssuePluginCustomTextureUpdateV2(global::System.IntPtr callback, global::UnityEngine.Texture targetTexture, uint userData)
		{
			m_WrappedCommandBuffer.IssuePluginCustomTextureUpdateV2(callback, targetTexture, userData);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.EnableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword)
		{
			EnableKeyword(in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.EnableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			EnableKeyword(material, in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.EnableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			EnableKeyword(computeShader, in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.DisableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword)
		{
			DisableKeyword(in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.DisableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			DisableKeyword(material, in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.DisableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword)
		{
			DisableKeyword(computeShader, in keyword);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.SetKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword, bool value)
		{
			SetKeyword(in keyword, value);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.SetKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value)
		{
			SetKeyword(material, in keyword, value);
		}

		void global::UnityEngine.Rendering.IBaseCommandBuffer.SetKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value)
		{
			SetKeyword(computeShader, in keyword, value);
		}
	}
}

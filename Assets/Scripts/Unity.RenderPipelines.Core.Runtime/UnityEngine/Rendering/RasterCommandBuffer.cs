namespace UnityEngine.Rendering
{
	public class RasterCommandBuffer : global::UnityEngine.Rendering.BaseCommandBuffer, global::UnityEngine.Rendering.IRasterCommandBuffer, global::UnityEngine.Rendering.IBaseCommandBuffer
	{
		internal RasterCommandBuffer(global::UnityEngine.Rendering.CommandBuffer wrapped, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphPass executingPass, bool isAsync)
			: base(wrapped, executingPass, isAsync)
		{
		}

		public void SetInvertCulling(bool invertCulling)
		{
			m_WrappedCommandBuffer.SetInvertCulling(invertCulling);
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

		public void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor)
		{
			m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor);
		}

		public void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor, float depth)
		{
			m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth);
		}

		public void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor, float depth, uint stencil)
		{
			m_WrappedCommandBuffer.ClearRenderTarget(clearDepth, clearColor, backgroundColor, depth, stencil);
		}

		public void ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags clearFlags, global::UnityEngine.Color backgroundColor, float depth, uint stencil)
		{
			m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColor, depth, stencil);
		}

		public void ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags clearFlags, global::UnityEngine.Color[] backgroundColors, float depth, uint stencil)
		{
			m_WrappedCommandBuffer.ClearRenderTarget(clearFlags, backgroundColors, depth, stencil);
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

		public void SetInstanceMultiplier(uint multiplier)
		{
			m_WrappedCommandBuffer.SetInstanceMultiplier(multiplier);
		}

		public void SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode foveatedRenderingMode)
		{
			m_WrappedCommandBuffer.SetFoveatedRenderingMode(foveatedRenderingMode);
		}

		public void SetWireframe(bool enable)
		{
			m_WrappedCommandBuffer.SetWireframe(enable);
		}

		public void ConfigureFoveatedRendering(global::System.IntPtr platformData)
		{
			m_WrappedCommandBuffer.ConfigureFoveatedRendering(platformData);
		}

		public void SetupCameraProperties(global::UnityEngine.Camera camera)
		{
			m_WrappedCommandBuffer.SetupCameraProperties(camera);
		}

		public void InvokeOnRenderObjectCallbacks()
		{
			m_WrappedCommandBuffer.InvokeOnRenderObjectCallbacks();
		}

		public void SetShadingRateFragmentSize(global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize)
		{
			m_WrappedCommandBuffer.SetShadingRateFragmentSize(shadingRateFragmentSize);
		}

		public void SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner)
		{
			m_WrappedCommandBuffer.SetShadingRateCombiner(stage, combiner);
		}

		public void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex, int shaderPass, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
		}

		public void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex, int shaderPass)
		{
			m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass);
		}

		public void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex)
		{
			m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material, submeshIndex);
		}

		public void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material)
		{
			m_WrappedCommandBuffer.DrawMesh(mesh, matrix, material);
		}

		public void DrawMultipleMeshes(global::UnityEngine.Matrix4x4[] matrices, global::UnityEngine.Mesh[] meshes, int[] subsetIndices, int count, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMultipleMeshes(matrices, meshes, subsetIndices, count, material, shaderPass, properties);
		}

		public void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material, int submeshIndex, int shaderPass)
		{
			m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex, shaderPass);
		}

		public void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material, int submeshIndex)
		{
			m_WrappedCommandBuffer.DrawRenderer(renderer, material, submeshIndex);
		}

		public void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material)
		{
			m_WrappedCommandBuffer.DrawRenderer(renderer, material);
		}

		public void DrawRendererList(global::UnityEngine.Rendering.RendererList rendererList)
		{
			m_WrappedCommandBuffer.DrawRendererList(rendererList);
		}

		public void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount, int instanceCount, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount, properties);
		}

		public void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount, int instanceCount)
		{
			m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount, instanceCount);
		}

		public void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount)
		{
			m_WrappedCommandBuffer.DrawProcedural(matrix, material, shaderPass, topology, vertexCount);
		}

		public void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount, int instanceCount, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount, properties);
		}

		public void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount, int instanceCount)
		{
			m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount, instanceCount);
		}

		public void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount)
		{
			m_WrappedCommandBuffer.DrawProcedural(indexBuffer, matrix, material, shaderPass, topology, indexCount);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		public void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(matrix, material, shaderPass, topology, bufferWithArgs);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset, properties);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs, argsOffset);
		}

		public void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawProceduralIndirect(indexBuffer, matrix, material, shaderPass, topology, bufferWithArgs);
		}

		public void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices, int count, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count, properties);
		}

		public void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices, int count)
		{
			m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices, count);
		}

		public void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices)
		{
			m_WrappedCommandBuffer.DrawMeshInstanced(mesh, submeshIndex, material, shaderPass, matrices);
		}

		public void DrawMeshInstancedProcedural(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, int count, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedProcedural(mesh, submeshIndex, material, shaderPass, count, properties);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset, properties);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs, argsOffset);
		}

		public void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs)
		{
			m_WrappedCommandBuffer.DrawMeshInstancedIndirect(mesh, submeshIndex, material, shaderPass, bufferWithArgs);
		}

		public void DrawOcclusionMesh(global::UnityEngine.RectInt normalizedCamViewport)
		{
			m_WrappedCommandBuffer.DrawOcclusionMesh(normalizedCamViewport);
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

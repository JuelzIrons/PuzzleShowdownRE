namespace UnityEngine.Rendering
{
	public interface IRasterCommandBuffer : global::UnityEngine.Rendering.IBaseCommandBuffer
	{
		void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor);

		void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor, float depth);

		void ClearRenderTarget(bool clearDepth, bool clearColor, global::UnityEngine.Color backgroundColor, float depth, uint stencil);

		void ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags clearFlags, global::UnityEngine.Color backgroundColor, float depth, uint stencil);

		void ClearRenderTarget(global::UnityEngine.Rendering.RTClearFlags clearFlags, global::UnityEngine.Color[] backgroundColors, float depth, uint stencil);

		void SetInstanceMultiplier(uint multiplier);

		void SetFoveatedRenderingMode(global::UnityEngine.Rendering.FoveatedRenderingMode foveatedRenderingMode);

		void SetWireframe(bool enable);

		void ConfigureFoveatedRendering(global::System.IntPtr platformData);

		void SetShadingRateFragmentSize(global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize);

		void SetShadingRateCombiner(global::UnityEngine.Rendering.ShadingRateCombinerStage stage, global::UnityEngine.Rendering.ShadingRateCombiner combiner);

		void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex, int shaderPass, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex, int shaderPass);

		void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int submeshIndex);

		void DrawMesh(global::UnityEngine.Mesh mesh, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material);

		void DrawMultipleMeshes(global::UnityEngine.Matrix4x4[] matrices, global::UnityEngine.Mesh[] meshes, int[] subsetIndices, int count, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material, int submeshIndex, int shaderPass);

		void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material, int submeshIndex);

		void DrawRenderer(global::UnityEngine.Renderer renderer, global::UnityEngine.Material material);

		void DrawRendererList(global::UnityEngine.Rendering.RendererList rendererList);

		void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount, int instanceCount, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount, int instanceCount);

		void DrawProcedural(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int vertexCount);

		void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount, int instanceCount, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount, int instanceCount);

		void DrawProcedural(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, int indexCount);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.ComputeBuffer bufferWithArgs);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset);

		void DrawProceduralIndirect(global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset);

		void DrawProceduralIndirect(global::UnityEngine.GraphicsBuffer indexBuffer, global::UnityEngine.Matrix4x4 matrix, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.MeshTopology topology, global::UnityEngine.GraphicsBuffer bufferWithArgs);

		void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices, int count, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices, int count);

		void DrawMeshInstanced(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.Matrix4x4[] matrices);

		void DrawMeshInstancedProcedural(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, int count, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs, int argsOffset);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.ComputeBuffer bufferWithArgs);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset, global::UnityEngine.MaterialPropertyBlock properties);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs, int argsOffset);

		void DrawMeshInstancedIndirect(global::UnityEngine.Mesh mesh, int submeshIndex, global::UnityEngine.Material material, int shaderPass, global::UnityEngine.GraphicsBuffer bufferWithArgs);

		void DrawOcclusionMesh(global::UnityEngine.RectInt normalizedCamViewport);
	}
}

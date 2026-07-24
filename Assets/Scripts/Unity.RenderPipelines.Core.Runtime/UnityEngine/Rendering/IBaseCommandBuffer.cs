namespace UnityEngine.Rendering
{
	public interface IBaseCommandBuffer
	{
		void SetInvertCulling(bool invertCulling);

		void SetViewport(global::UnityEngine.Rect pixelRect);

		void EnableScissorRect(global::UnityEngine.Rect scissor);

		void DisableScissorRect();

		void SetGlobalFloat(int nameID, float value);

		void SetGlobalInt(int nameID, int value);

		void SetGlobalInteger(int nameID, int value);

		void SetGlobalVector(int nameID, global::UnityEngine.Vector4 value);

		void SetGlobalColor(int nameID, global::UnityEngine.Color value);

		void SetGlobalMatrix(int nameID, global::UnityEngine.Matrix4x4 value);

		void EnableShaderKeyword(string keyword);

		void EnableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword);

		void EnableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword);

		void EnableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword);

		void DisableShaderKeyword(string keyword);

		void DisableKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword);

		void DisableKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword);

		void DisableKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword);

		void SetKeyword(in global::UnityEngine.Rendering.GlobalKeyword keyword, bool value);

		void SetKeyword(global::UnityEngine.Material material, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value);

		void SetKeyword(global::UnityEngine.ComputeShader computeShader, in global::UnityEngine.Rendering.LocalKeyword keyword, bool value);

		void SetViewProjectionMatrices(global::UnityEngine.Matrix4x4 view, global::UnityEngine.Matrix4x4 proj);

		void SetGlobalDepthBias(float bias, float slopeBias);

		void SetGlobalFloatArray(int nameID, float[] values);

		void SetGlobalVectorArray(int nameID, global::UnityEngine.Vector4[] values);

		void SetGlobalMatrixArray(int nameID, global::UnityEngine.Matrix4x4[] values);

		void SetLateLatchProjectionMatrices(global::UnityEngine.Matrix4x4[] projectionMat);

		void MarkLateLatchMatrixShaderPropertyID(global::UnityEngine.Rendering.CameraLateLatchMatrixType matrixPropertyType, int shaderPropertyID);

		void UnmarkLateLatchMatrix(global::UnityEngine.Rendering.CameraLateLatchMatrixType matrixPropertyType);

		void BeginSample(string name);

		void EndSample(string name);

		void BeginSample(global::UnityEngine.Profiling.CustomSampler sampler);

		void EndSample(global::UnityEngine.Profiling.CustomSampler sampler);

		void BeginSample(global::Unity.Profiling.ProfilerMarker marker);

		void EndSample(global::Unity.Profiling.ProfilerMarker marker);

		void IncrementUpdateCount(global::UnityEngine.Rendering.RenderTargetIdentifier dest);

		void SetupCameraProperties(global::UnityEngine.Camera camera);

		void InvokeOnRenderObjectCallbacks();

		void SetGlobalFloat(string name, float value);

		void SetGlobalInt(string name, int value);

		void SetGlobalInteger(string name, int value);

		void SetGlobalVector(string name, global::UnityEngine.Vector4 value);

		void SetGlobalColor(string name, global::UnityEngine.Color value);

		void SetGlobalMatrix(string name, global::UnityEngine.Matrix4x4 value);

		void SetGlobalFloatArray(string propertyName, global::System.Collections.Generic.List<float> values);

		void SetGlobalFloatArray(int nameID, global::System.Collections.Generic.List<float> values);

		void SetGlobalFloatArray(string propertyName, float[] values);

		void SetGlobalVectorArray(string propertyName, global::System.Collections.Generic.List<global::UnityEngine.Vector4> values);

		void SetGlobalVectorArray(int nameID, global::System.Collections.Generic.List<global::UnityEngine.Vector4> values);

		void SetGlobalVectorArray(string propertyName, global::UnityEngine.Vector4[] values);

		void SetGlobalMatrixArray(string propertyName, global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> values);

		void SetGlobalMatrixArray(int nameID, global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> values);

		void SetGlobalMatrixArray(string propertyName, global::UnityEngine.Matrix4x4[] values);

		void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value);

		void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value);

		void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderGraphModule.TextureHandle value, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetGlobalBuffer(string name, global::UnityEngine.ComputeBuffer value);

		void SetGlobalBuffer(int nameID, global::UnityEngine.ComputeBuffer value);

		void SetGlobalBuffer(string name, global::UnityEngine.GraphicsBuffer value);

		void SetGlobalBuffer(int nameID, global::UnityEngine.GraphicsBuffer value);

		void SetGlobalConstantBuffer(global::UnityEngine.ComputeBuffer buffer, int nameID, int offset, int size);

		void SetGlobalConstantBuffer(global::UnityEngine.ComputeBuffer buffer, string name, int offset, int size);

		void SetGlobalConstantBuffer(global::UnityEngine.GraphicsBuffer buffer, int nameID, int offset, int size);

		void SetGlobalConstantBuffer(global::UnityEngine.GraphicsBuffer buffer, string name, int offset, int size);

		void SetShadowSamplingMode(global::UnityEngine.Rendering.RenderTargetIdentifier shadowmap, global::UnityEngine.Rendering.ShadowSamplingMode mode);

		void SetSinglePassStereo(global::UnityEngine.Rendering.SinglePassStereoMode mode);

		void IssuePluginEvent(global::System.IntPtr callback, int eventID);

		void IssuePluginEventAndData(global::System.IntPtr callback, int eventID, global::System.IntPtr data);

		void IssuePluginCustomBlit(global::System.IntPtr callback, uint command, global::UnityEngine.Rendering.RenderTargetIdentifier source, global::UnityEngine.Rendering.RenderTargetIdentifier dest, uint commandParam, uint commandFlags);

		void IssuePluginCustomTextureUpdateV2(global::System.IntPtr callback, global::UnityEngine.Texture targetTexture, uint userData);
	}
}

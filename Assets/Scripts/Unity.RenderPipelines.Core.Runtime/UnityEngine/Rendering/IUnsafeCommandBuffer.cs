namespace UnityEngine.Rendering
{
	public interface IUnsafeCommandBuffer : global::UnityEngine.Rendering.IBaseCommandBuffer, global::UnityEngine.Rendering.IRasterCommandBuffer, global::UnityEngine.Rendering.IComputeCommandBuffer
	{
		void RequestAsyncReadback(global::UnityEngine.ComputeBuffer src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.GraphicsBuffer src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.ComputeBuffer src, int size, int offset, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.GraphicsBuffer src, int size, int offset, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, global::UnityEngine.TextureFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, global::UnityEngine.Experimental.Rendering.GraphicsFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::UnityEngine.TextureFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadback(global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback);

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.ComputeBuffer src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.ComputeBuffer src, int size, int offset, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.GraphicsBuffer src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.GraphicsBuffer src, int size, int offset, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, global::UnityEngine.TextureFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, global::UnityEngine.Experimental.Rendering.GraphicsFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::UnityEngine.TextureFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void RequestAsyncReadbackIntoNativeArray<T>(ref global::Unity.Collections.NativeArray<T> output, global::UnityEngine.Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, global::UnityEngine.Experimental.Rendering.GraphicsFormat dstFormat, global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> callback) where T : struct;

		void Clear();

		void ClearRandomWriteTargets();

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt, global::UnityEngine.Rendering.RenderBufferLoadAction loadAction, global::UnityEngine.Rendering.RenderBufferStoreAction storeAction);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel, global::UnityEngine.CubemapFace cubemapFace);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel, global::UnityEngine.CubemapFace cubemapFace, int depthSlice);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier color, global::UnityEngine.Rendering.RenderTargetIdentifier depth);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier color, global::UnityEngine.Rendering.RenderTargetIdentifier depth, int mipLevel);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier color, global::UnityEngine.Rendering.RenderTargetIdentifier depth, int mipLevel, global::UnityEngine.CubemapFace cubemapFace);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier color, global::UnityEngine.Rendering.RenderTargetIdentifier depth, int mipLevel, global::UnityEngine.CubemapFace cubemapFace, int depthSlice);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier color, global::UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, global::UnityEngine.Rendering.RenderTargetIdentifier depth, global::UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, global::UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier[] colors, global::UnityEngine.Rendering.RenderTargetIdentifier depth);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetIdentifier[] colors, global::UnityEngine.Rendering.RenderTargetIdentifier depth, int mipLevel, global::UnityEngine.CubemapFace cubemapFace, int depthSlice);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetBinding binding, int mipLevel, global::UnityEngine.CubemapFace cubemapFace, int depthSlice);

		void SetRenderTarget(global::UnityEngine.Rendering.RenderTargetBinding binding);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, string name, global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetComputeTextureParam(global::UnityEngine.ComputeShader computeShader, int kernelIndex, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt, int mipLevel, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, string name, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetRayTracingTextureParam(global::UnityEngine.Rendering.RayTracingShader rayTracingShader, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void GenerateMips(global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void GenerateMips(global::UnityEngine.RenderTexture rt);

		void SetRandomWriteTarget(int index, global::UnityEngine.Rendering.RenderTargetIdentifier rt);

		void SetRandomWriteTarget(int index, global::UnityEngine.ComputeBuffer buffer, bool preserveCounterValue);

		void SetRandomWriteTarget(int index, global::UnityEngine.ComputeBuffer buffer);

		void SetRandomWriteTarget(int index, global::UnityEngine.GraphicsBuffer buffer, bool preserveCounterValue);

		void SetRandomWriteTarget(int index, global::UnityEngine.GraphicsBuffer buffer);

		void CopyTexture(global::UnityEngine.Rendering.RenderTargetIdentifier src, global::UnityEngine.Rendering.RenderTargetIdentifier dst);

		void CopyTexture(global::UnityEngine.Rendering.RenderTargetIdentifier src, int srcElement, global::UnityEngine.Rendering.RenderTargetIdentifier dst, int dstElement);

		void CopyTexture(global::UnityEngine.Rendering.RenderTargetIdentifier src, int srcElement, int srcMip, global::UnityEngine.Rendering.RenderTargetIdentifier dst, int dstElement, int dstMip);

		void CopyTexture(global::UnityEngine.Rendering.RenderTargetIdentifier src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, global::UnityEngine.Rendering.RenderTargetIdentifier dst, int dstElement, int dstMip, int dstX, int dstY);

		void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderTargetIdentifier value);

		void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier value);

		void SetGlobalTexture(string name, global::UnityEngine.Rendering.RenderTargetIdentifier value, global::UnityEngine.Rendering.RenderTextureSubElement element);

		void SetGlobalTexture(int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier value, global::UnityEngine.Rendering.RenderTextureSubElement element);
	}
}

namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal static class GraphicsHelpers
	{
		public static long MaxGraphicsBufferSizeInBytes => global::UnityEngine.SystemInfo.maxGraphicsBufferSize;

		public static float MaxGraphicsBufferSizeInGigaBytes => (float)MaxGraphicsBufferSizeInBytes / 1024f / 1024f / 1024f;

		public static void CopyBuffer(global::UnityEngine.ComputeShader copyShader, global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer src, int srcOffsetInDWords, global::UnityEngine.GraphicsBuffer dst, int dstOffsetInDwords, int sizeInDWords)
		{
			int num = sizeInDWords;
			cmd.SetComputeBufferParam(copyShader, 0, "_SrcBuffer", src);
			cmd.SetComputeBufferParam(copyShader, 0, "_DstBuffer", dst);
			while (num > 0)
			{
				int num2 = global::Unity.Mathematics.math.min(num, 134215680);
				cmd.SetComputeIntParam(copyShader, "_SrcOffset", srcOffsetInDWords);
				cmd.SetComputeIntParam(copyShader, "_DstOffset", dstOffsetInDwords);
				cmd.SetComputeIntParam(copyShader, "_Size", num2);
				cmd.DispatchCompute(copyShader, 0, DivUp(num2, 2048), 1, 1);
				num -= num2;
				srcOffsetInDWords += num2;
				dstOffsetInDwords += num2;
			}
		}

		public static void CopyBuffer(global::UnityEngine.ComputeShader copyShader, global::UnityEngine.GraphicsBuffer src, int srcOffsetInDWords, global::UnityEngine.GraphicsBuffer dst, int dstOffsetInDwords, int sizeInDwords)
		{
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = new global::UnityEngine.Rendering.CommandBuffer();
			CopyBuffer(copyShader, commandBuffer, src, srcOffsetInDWords, dst, dstOffsetInDwords, sizeInDwords);
			global::UnityEngine.Graphics.ExecuteCommandBuffer(commandBuffer);
		}

		public static bool ReallocateBuffer(global::UnityEngine.ComputeShader copyShader, int oldCapacity, int newCapacity, int elementSizeInBytes, ref global::UnityEngine.GraphicsBuffer buffer)
		{
			int stride = buffer.stride;
			global::UnityEngine.GraphicsBuffer graphicsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)((long)newCapacity * (long)elementSizeInBytes / stride), stride);
			if (!graphicsBuffer.IsValid())
			{
				return false;
			}
			CopyBuffer(copyShader, buffer, 0, graphicsBuffer, 0, (int)((long)oldCapacity * (long)elementSizeInBytes / 4));
			buffer.Dispose();
			buffer = graphicsBuffer;
			return true;
		}

		public static int DivUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		public static int DivUp(int x, uint y)
		{
			return (x + (int)y - 1) / (int)y;
		}

		public static uint DivUp(uint x, uint y)
		{
			return (x + y - 1) / y;
		}

		public static global::Unity.Mathematics.uint3 DivUp(global::Unity.Mathematics.uint3 x, global::Unity.Mathematics.uint3 y)
		{
			return (x + y - 1u) / y;
		}

		public static void Flush(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			global::UnityEngine.Graphics.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			global::UnityEngine.GL.Flush();
		}
	}
}

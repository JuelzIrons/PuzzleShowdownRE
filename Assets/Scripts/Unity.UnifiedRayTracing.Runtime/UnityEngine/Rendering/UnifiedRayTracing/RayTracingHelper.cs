namespace UnityEngine.Rendering.UnifiedRayTracing
{
	public static class RayTracingHelper
	{
		public const global::UnityEngine.GraphicsBuffer.Target ScratchBufferTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		public static global::UnityEngine.GraphicsBuffer CreateDispatchIndirectBuffer()
		{
			return new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.CopySource | global::UnityEngine.GraphicsBuffer.Target.Structured | global::UnityEngine.GraphicsBuffer.Target.IndirectArguments, 3, 4);
		}

		public static global::UnityEngine.GraphicsBuffer CreateScratchBufferForBuildAndDispatch(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader, uint dispatchWidth, uint dispatchHeight, uint dispatchDepth)
		{
			ulong num = global::System.Math.Max(accelStruct.GetBuildScratchBufferRequiredSizeInBytes(), shader.GetTraceScratchBufferRequiredSizeInBytes(dispatchWidth, dispatchHeight, dispatchDepth));
			if (num == 0L)
			{
				return null;
			}
			return new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(num / 4), 4);
		}

		public static global::UnityEngine.GraphicsBuffer CreateScratchBufferForBuild(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct)
		{
			ulong buildScratchBufferRequiredSizeInBytes = accelStruct.GetBuildScratchBufferRequiredSizeInBytes();
			if (buildScratchBufferRequiredSizeInBytes == 0L)
			{
				return null;
			}
			return new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(buildScratchBufferRequiredSizeInBytes / 4), 4);
		}

		public static global::UnityEngine.GraphicsBuffer CreateScratchBufferForTrace(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader, uint dispatchWidth, uint dispatchHeight, uint dispatchDepth)
		{
			ulong traceScratchBufferRequiredSizeInBytes = shader.GetTraceScratchBufferRequiredSizeInBytes(dispatchWidth, dispatchHeight, dispatchDepth);
			if (traceScratchBufferRequiredSizeInBytes == 0L)
			{
				return null;
			}
			return new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(traceScratchBufferRequiredSizeInBytes / 4), 4);
		}

		public static void ResizeScratchBufferForTrace(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader shader, uint dispatchWidth, uint dispatchHeight, uint dispatchDepth, ref global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			ulong traceScratchBufferRequiredSizeInBytes = shader.GetTraceScratchBufferRequiredSizeInBytes(dispatchWidth, dispatchHeight, dispatchDepth);
			if (traceScratchBufferRequiredSizeInBytes != 0L)
			{
				_ = scratchBuffer;
				if (scratchBuffer == null || (ulong)(scratchBuffer.count * scratchBuffer.stride) < traceScratchBufferRequiredSizeInBytes)
				{
					scratchBuffer?.Dispose();
					scratchBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(traceScratchBufferRequiredSizeInBytes / 4), 4);
				}
			}
		}

		public static void ResizeScratchBufferForBuild(global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct, ref global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			ulong buildScratchBufferRequiredSizeInBytes = accelStruct.GetBuildScratchBufferRequiredSizeInBytes();
			if (buildScratchBufferRequiredSizeInBytes != 0L)
			{
				_ = scratchBuffer;
				if (scratchBuffer == null || (ulong)(scratchBuffer.count * scratchBuffer.stride) < buildScratchBufferRequiredSizeInBytes)
				{
					scratchBuffer?.Dispose();
					scratchBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)(buildScratchBufferRequiredSizeInBytes / 4), 4);
				}
			}
		}
	}
}

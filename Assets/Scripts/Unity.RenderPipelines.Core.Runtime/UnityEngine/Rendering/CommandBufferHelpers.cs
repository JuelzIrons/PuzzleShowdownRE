namespace UnityEngine.Rendering
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct CommandBufferHelpers
	{
		internal static global::UnityEngine.Rendering.RasterCommandBuffer rasterCmd = new global::UnityEngine.Rendering.RasterCommandBuffer(null, null, isAsync: false);

		internal static global::UnityEngine.Rendering.ComputeCommandBuffer computeCmd = new global::UnityEngine.Rendering.ComputeCommandBuffer(null, null, isAsync: false);

		internal static global::UnityEngine.Rendering.UnsafeCommandBuffer unsafeCmd = new global::UnityEngine.Rendering.UnsafeCommandBuffer(null, null, isAsync: false);

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Rendering.RasterCommandBuffer GetRasterCommandBuffer(global::UnityEngine.Rendering.CommandBuffer baseBuffer)
		{
			rasterCmd.m_WrappedCommandBuffer = baseBuffer;
			return rasterCmd;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Rendering.ComputeCommandBuffer GetComputeCommandBuffer(global::UnityEngine.Rendering.CommandBuffer baseBuffer)
		{
			computeCmd.m_WrappedCommandBuffer = baseBuffer;
			return computeCmd;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Rendering.UnsafeCommandBuffer GetUnsafeCommandBuffer(global::UnityEngine.Rendering.CommandBuffer baseBuffer)
		{
			unsafeCmd.m_WrappedCommandBuffer = baseBuffer;
			return unsafeCmd;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.Rendering.CommandBuffer GetNativeCommandBuffer(global::UnityEngine.Rendering.UnsafeCommandBuffer baseBuffer)
		{
			return baseBuffer.m_WrappedCommandBuffer;
		}

		public static void VFXManager_ProcessCameraCommand(global::UnityEngine.Camera cam, global::UnityEngine.Rendering.UnsafeCommandBuffer cmd, global::UnityEngine.VFX.VFXCameraXRSettings camXRSettings, global::UnityEngine.Rendering.CullingResults results)
		{
			global::UnityEngine.VFX.VFXManager.ProcessCameraCommand(cam, cmd.m_WrappedCommandBuffer, camXRSettings, results);
		}
	}
}

namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal class ComputeRayTracingShader : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader
	{
		private readonly global::UnityEngine.ComputeShader m_Shader;

		private readonly int m_KernelIndex;

		private readonly int m_ComputeIndirectDispatchDimsKernelIndex;

		private global::Unity.Mathematics.uint3 m_ThreadGroupSizes;

		private readonly global::UnityEngine.GraphicsBuffer m_DispatchBuffer;

		internal ComputeRayTracingShader(global::UnityEngine.ComputeShader shader, string dispatchFuncName, global::UnityEngine.GraphicsBuffer dispatchBuffer)
		{
			m_Shader = shader;
			m_KernelIndex = m_Shader.FindKernel(dispatchFuncName);
			m_ComputeIndirectDispatchDimsKernelIndex = m_Shader.FindKernel("ComputeIndirectDispatchDims");
			m_Shader.GetKernelThreadGroupSizes(m_KernelIndex, out m_ThreadGroupSizes.x, out m_ThreadGroupSizes.y, out m_ThreadGroupSizes.z);
			m_DispatchBuffer = dispatchBuffer;
		}

		public global::Unity.Mathematics.uint3 GetThreadGroupSizes()
		{
			return m_ThreadGroupSizes;
		}

		public void SetAccelerationStructure(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct)
		{
			(accelStruct as global::UnityEngine.Rendering.UnifiedRayTracing.ComputeRayTracingAccelStruct).Bind(cmd, name, this);
		}

		public void SetIntParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, int val)
		{
			cmd.SetComputeIntParam(m_Shader, nameID, val);
		}

		public void SetFloatParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, float val)
		{
			cmd.SetComputeFloatParam(m_Shader, nameID, val);
		}

		public void SetVectorParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Vector4 val)
		{
			cmd.SetComputeVectorParam(m_Shader, nameID, val);
		}

		public void SetMatrixParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Matrix4x4 val)
		{
			cmd.SetComputeMatrixParam(m_Shader, nameID, val);
		}

		public void SetTextureParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt)
		{
			cmd.SetComputeTextureParam(m_Shader, m_KernelIndex, nameID, rt);
		}

		public void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer)
		{
			cmd.SetComputeBufferParam(m_Shader, m_KernelIndex, nameID, buffer);
		}

		public void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer)
		{
			cmd.SetComputeBufferParam(m_Shader, m_KernelIndex, nameID, buffer);
		}

		public void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			cmd.SetComputeConstantBufferParam(m_Shader, nameID, buffer, offset, size);
		}

		public void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			cmd.SetComputeConstantBufferParam(m_Shader, nameID, buffer, offset, size);
		}

		public void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, uint width, uint height, uint depth)
		{
			GetTraceScratchBufferRequiredSizeInBytes(width, height, depth);
			_ = 0;
			cmd.SetComputeBufferParam(m_Shader, m_KernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_Stack, scratchBuffer);
			cmd.SetBufferData(m_DispatchBuffer, new uint[3] { width, height, depth });
			SetBufferParam(cmd, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_DispatchDims, m_DispatchBuffer);
			uint threadGroupsX = (uint)global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.DivUp((int)width, m_ThreadGroupSizes.x);
			uint threadGroupsY = (uint)global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.DivUp((int)height, m_ThreadGroupSizes.y);
			uint threadGroupsZ = (uint)global::UnityEngine.Rendering.UnifiedRayTracing.GraphicsHelpers.DivUp((int)depth, m_ThreadGroupSizes.z);
			cmd.DispatchCompute(m_Shader, m_KernelIndex, (int)threadGroupsX, (int)threadGroupsY, (int)threadGroupsZ);
		}

		public void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, global::UnityEngine.GraphicsBuffer argsBuffer)
		{
			SetIndirectDispatchDimensions(cmd, argsBuffer);
			DispatchIndirect(cmd, scratchBuffer, argsBuffer);
		}

		internal void SetIndirectDispatchDimensions(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer argsBuffer)
		{
			cmd.SetComputeBufferParam(m_Shader, m_ComputeIndirectDispatchDimsKernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_DispatchDims, argsBuffer);
			cmd.SetComputeBufferParam(m_Shader, m_ComputeIndirectDispatchDimsKernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_DispatchDimsInWorkgroups, m_DispatchBuffer);
			cmd.DispatchCompute(m_Shader, m_ComputeIndirectDispatchDimsKernelIndex, 1, 1, 1);
		}

		internal void DispatchIndirect(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, global::UnityEngine.GraphicsBuffer argsBuffer)
		{
			cmd.SetComputeBufferParam(m_Shader, m_KernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_Stack, scratchBuffer);
			cmd.SetComputeBufferParam(m_Shader, m_KernelIndex, global::UnityEngine.Rendering.UnifiedRayTracing.SID._UnifiedRT_DispatchDims, argsBuffer);
			cmd.DispatchCompute(m_Shader, m_KernelIndex, m_DispatchBuffer, 0u);
		}

		public ulong GetTraceScratchBufferRequiredSizeInBytes(uint width, uint height, uint depth)
		{
			return global::UnityEngine.Rendering.RadeonRays.RadeonRaysAPI.GetTraceMemoryRequirements(width * height * depth) * 4;
		}
	}
}

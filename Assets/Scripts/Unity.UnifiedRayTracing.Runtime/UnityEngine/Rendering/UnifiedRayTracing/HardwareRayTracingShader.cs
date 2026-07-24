namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal class HardwareRayTracingShader : global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingShader
	{
		private readonly global::UnityEngine.Rendering.RayTracingShader m_Shader;

		private readonly string m_ShaderDispatchFuncName;

		internal HardwareRayTracingShader(global::UnityEngine.Rendering.RayTracingShader shader, string dispatchFuncName, global::UnityEngine.GraphicsBuffer unused)
		{
			m_Shader = shader;
			m_ShaderDispatchFuncName = dispatchFuncName;
		}

		public global::Unity.Mathematics.uint3 GetThreadGroupSizes()
		{
			return new global::Unity.Mathematics.uint3(1u, 1u, 1u);
		}

		public void SetAccelerationStructure(global::UnityEngine.Rendering.CommandBuffer cmd, string name, global::UnityEngine.Rendering.UnifiedRayTracing.IRayTracingAccelStruct accelStruct)
		{
			cmd.SetRayTracingShaderPass(m_Shader, "RayTracing");
			global::UnityEngine.Rendering.UnifiedRayTracing.HardwareRayTracingAccelStruct hardwareRayTracingAccelStruct = accelStruct as global::UnityEngine.Rendering.UnifiedRayTracing.HardwareRayTracingAccelStruct;
			cmd.SetRayTracingAccelerationStructure(m_Shader, global::UnityEngine.Shader.PropertyToID(name + "accelStruct"), hardwareRayTracingAccelStruct.accelStruct);
		}

		public void SetIntParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, int val)
		{
			cmd.SetRayTracingIntParam(m_Shader, nameID, val);
		}

		public void SetFloatParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, float val)
		{
			cmd.SetRayTracingFloatParam(m_Shader, nameID, val);
		}

		public void SetVectorParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Vector4 val)
		{
			cmd.SetRayTracingVectorParam(m_Shader, nameID, val);
		}

		public void SetMatrixParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Matrix4x4 val)
		{
			cmd.SetRayTracingMatrixParam(m_Shader, nameID, val);
		}

		public void SetTextureParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.Rendering.RenderTargetIdentifier rt)
		{
			cmd.SetRayTracingTextureParam(m_Shader, nameID, rt);
		}

		public void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer)
		{
			cmd.SetRayTracingBufferParam(m_Shader, nameID, buffer);
		}

		public void SetBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer)
		{
			cmd.SetRayTracingBufferParam(m_Shader, nameID, buffer);
		}

		public void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.GraphicsBuffer buffer, int offset, int size)
		{
			cmd.SetRayTracingConstantBufferParam(m_Shader, nameID, buffer, offset, size);
		}

		public void SetConstantBufferParam(global::UnityEngine.Rendering.CommandBuffer cmd, int nameID, global::UnityEngine.ComputeBuffer buffer, int offset, int size)
		{
			cmd.SetRayTracingConstantBufferParam(m_Shader, nameID, buffer, offset, size);
		}

		public void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, uint width, uint height, uint depth)
		{
			cmd.DispatchRays(m_Shader, m_ShaderDispatchFuncName, width, height, depth);
		}

		public void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer scratchBuffer, global::UnityEngine.GraphicsBuffer argsBuffer)
		{
			cmd.DispatchRays(m_Shader, m_ShaderDispatchFuncName, argsBuffer, 0u);
		}

		public ulong GetTraceScratchBufferRequiredSizeInBytes(uint width, uint height, uint depth)
		{
			return 0uL;
		}
	}
}

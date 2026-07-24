namespace UnityEngine.Rendering
{
	public class ConstantBuffer
	{
		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.ConstantBufferBase> m_RegisteredConstantBuffers = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ConstantBufferBase>();

		public static void PushGlobal<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.SetGlobal(cmd, shaderId);
		}

		public static void PushGlobal<CBType>(global::UnityEngine.Rendering.BaseCommandBuffer cmd, in CBType data, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.SetGlobal(cmd, shaderId);
		}

		public static void PushGlobal<CBType>(in CBType data, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.SetGlobal(shaderId);
		}

		public static void Push<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data, global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(cmd, cs, shaderId);
		}

		public static void Push<CBType>(global::UnityEngine.Rendering.IComputeCommandBuffer cmd, in CBType data, global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd as global::UnityEngine.Rendering.BaseCommandBuffer, in data);
			instance.Set(cmd, cs, shaderId);
		}

		public static void Push<CBType>(in CBType data, global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(cs, shaderId);
		}

		public static void Push<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data, global::UnityEngine.Material mat, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(mat, shaderId);
		}

		public static void Push<CBType>(global::UnityEngine.Rendering.BaseCommandBuffer cmd, in CBType data, global::UnityEngine.Material mat, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(mat, shaderId);
		}

		public static void Push<CBType>(in CBType data, global::UnityEngine.Material mat, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance = global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(mat, shaderId);
		}

		public static void UpdateData<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.UpdateData(cmd, in data);
		}

		public static void UpdateData<CBType>(global::UnityEngine.Rendering.BaseCommandBuffer cmd, in CBType data) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.UpdateData(cmd, in data);
		}

		public static void UpdateData<CBType>(in CBType data) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.UpdateData(in data);
		}

		public static void SetGlobal<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.SetGlobal(cmd, shaderId);
		}

		public static void SetGlobal<CBType>(global::UnityEngine.Rendering.BaseCommandBuffer cmd, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.SetGlobal(cmd, shaderId);
		}

		public static void SetGlobal<CBType>(int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.SetGlobal(shaderId);
		}

		public static void Set<CBType>(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.Set(cmd, cs, shaderId);
		}

		public static void Set<CBType>(global::UnityEngine.Rendering.IComputeCommandBuffer cmd, global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.Set(cmd, cs, shaderId);
		}

		public static void Set<CBType>(global::UnityEngine.ComputeShader cs, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.Set(cs, shaderId);
		}

		public static void Set<CBType>(global::UnityEngine.Material mat, int shaderId) where CBType : struct
		{
			global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>.instance.Set(mat, shaderId);
		}

		public static void ReleaseAll()
		{
			foreach (global::UnityEngine.Rendering.ConstantBufferBase registeredConstantBuffer in m_RegisteredConstantBuffers)
			{
				registeredConstantBuffer.Release();
			}
			m_RegisteredConstantBuffers.Clear();
		}

		internal static void Register(global::UnityEngine.Rendering.ConstantBufferBase cb)
		{
			m_RegisteredConstantBuffers.Add(cb);
		}
	}
	public class ConstantBuffer<CBType> : global::UnityEngine.Rendering.ConstantBufferBase where CBType : struct
	{
		private global::System.Collections.Generic.HashSet<int> m_GlobalBindings = new global::System.Collections.Generic.HashSet<int>();

		private CBType[] m_Data = new CBType[1];

		private global::UnityEngine.ComputeBuffer m_GPUConstantBuffer;

		public ConstantBuffer()
		{
			m_GPUConstantBuffer = new global::UnityEngine.ComputeBuffer(1, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<CBType>(), global::UnityEngine.ComputeBufferType.Constant);
		}

		public void UpdateData(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data)
		{
			m_Data[0] = data;
			cmd.SetBufferData(m_GPUConstantBuffer, m_Data);
		}

		public void UpdateData(global::UnityEngine.Rendering.BaseCommandBuffer cmd, in CBType data)
		{
			UpdateData(cmd.m_WrappedCommandBuffer, in data);
		}

		public void UpdateData(in CBType data)
		{
			m_Data[0] = data;
			m_GPUConstantBuffer.SetData(m_Data);
		}

		public void SetGlobal(global::UnityEngine.Rendering.CommandBuffer cmd, int shaderId)
		{
			m_GlobalBindings.Add(shaderId);
			cmd.SetGlobalConstantBuffer(m_GPUConstantBuffer, shaderId, 0, m_GPUConstantBuffer.stride);
		}

		public void SetGlobal(global::UnityEngine.Rendering.BaseCommandBuffer cmd, int shaderId)
		{
			SetGlobal(cmd.m_WrappedCommandBuffer, shaderId);
		}

		public void SetGlobal(int shaderId)
		{
			m_GlobalBindings.Add(shaderId);
			global::UnityEngine.Shader.SetGlobalConstantBuffer(shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void Set(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader cs, int shaderId)
		{
			cmd.SetComputeConstantBufferParam(cs, shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void Set(global::UnityEngine.Rendering.IComputeCommandBuffer cmd, global::UnityEngine.ComputeShader cs, int shaderId)
		{
			cmd.SetComputeConstantBufferParam(cs, shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void Set(global::UnityEngine.ComputeShader cs, int shaderId)
		{
			cs.SetConstantBuffer(shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void Set(global::UnityEngine.Material mat, int shaderId)
		{
			mat.SetConstantBuffer(shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void Set(global::UnityEngine.MaterialPropertyBlock mpb, int shaderId)
		{
			mpb.SetConstantBuffer(shaderId, m_GPUConstantBuffer, 0, m_GPUConstantBuffer.stride);
		}

		public void PushGlobal(global::UnityEngine.Rendering.CommandBuffer cmd, in CBType data, int shaderId)
		{
			UpdateData(cmd, in data);
			SetGlobal(cmd, shaderId);
		}

		public void PushGlobal(global::UnityEngine.Rendering.BaseCommandBuffer cmd, in CBType data, int shaderId)
		{
			UpdateData(cmd, in data);
			SetGlobal(cmd, shaderId);
		}

		public void PushGlobal(in CBType data, int shaderId)
		{
			UpdateData(in data);
			SetGlobal(shaderId);
		}

		public override void Release()
		{
			foreach (int globalBinding in m_GlobalBindings)
			{
				global::UnityEngine.Shader.SetGlobalConstantBuffer(globalBinding, (global::UnityEngine.ComputeBuffer)null, 0, 0);
			}
			m_GlobalBindings.Clear();
			global::UnityEngine.Rendering.CoreUtils.SafeRelease(m_GPUConstantBuffer);
		}
	}
}

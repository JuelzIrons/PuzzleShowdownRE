namespace UnityEngine.Rendering.Universal
{
	internal class ShaderData : global::System.IDisposable
	{
		private static global::UnityEngine.Rendering.Universal.ShaderData m_Instance;

		private global::UnityEngine.ComputeBuffer m_LightDataBuffer;

		private global::UnityEngine.ComputeBuffer m_LightIndicesBuffer;

		private global::UnityEngine.ComputeBuffer m_AdditionalLightShadowParamsStructuredBuffer;

		private global::UnityEngine.ComputeBuffer m_AdditionalLightShadowSliceMatricesStructuredBuffer;

		internal static global::UnityEngine.Rendering.Universal.ShaderData instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new global::UnityEngine.Rendering.Universal.ShaderData();
				}
				return m_Instance;
			}
		}

		private ShaderData()
		{
		}

		public void Dispose()
		{
			DisposeBuffer(ref m_LightDataBuffer);
			DisposeBuffer(ref m_LightIndicesBuffer);
			DisposeBuffer(ref m_AdditionalLightShadowParamsStructuredBuffer);
			DisposeBuffer(ref m_AdditionalLightShadowSliceMatricesStructuredBuffer);
		}

		internal global::UnityEngine.ComputeBuffer GetLightDataBuffer(int size)
		{
			return GetOrUpdateBuffer<global::UnityEngine.Rendering.Universal.ShaderInput.LightData>(ref m_LightDataBuffer, size);
		}

		internal global::UnityEngine.ComputeBuffer GetLightIndicesBuffer(int size)
		{
			return GetOrUpdateBuffer<int>(ref m_LightIndicesBuffer, size);
		}

		internal global::UnityEngine.ComputeBuffer GetAdditionalLightShadowParamsStructuredBuffer(int size)
		{
			return GetOrUpdateBuffer<global::UnityEngine.Vector4>(ref m_AdditionalLightShadowParamsStructuredBuffer, size);
		}

		internal global::UnityEngine.ComputeBuffer GetAdditionalLightShadowSliceMatricesStructuredBuffer(int size)
		{
			return GetOrUpdateBuffer<global::UnityEngine.Matrix4x4>(ref m_AdditionalLightShadowSliceMatricesStructuredBuffer, size);
		}

		private global::UnityEngine.ComputeBuffer GetOrUpdateBuffer<T>(ref global::UnityEngine.ComputeBuffer buffer, int size) where T : struct
		{
			if (buffer == null)
			{
				buffer = new global::UnityEngine.ComputeBuffer(size, global::System.Runtime.InteropServices.Marshal.SizeOf<T>());
			}
			else if (size > buffer.count)
			{
				buffer.Dispose();
				buffer = new global::UnityEngine.ComputeBuffer(size, global::System.Runtime.InteropServices.Marshal.SizeOf<T>());
			}
			return buffer;
		}

		private void DisposeBuffer(ref global::UnityEngine.ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Dispose();
				buffer = null;
			}
		}
	}
}

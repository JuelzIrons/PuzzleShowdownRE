namespace UnityEngine.Rendering
{
	internal struct GPUInstanceDataBufferGrower : global::System.IDisposable
	{
		private static class CopyInstancesKernelIDs
		{
			public static readonly int _InputValidComponentCounts = global::UnityEngine.Shader.PropertyToID("_InputValidComponentCounts");

			public static readonly int _InstanceCounts = global::UnityEngine.Shader.PropertyToID("_InstanceCounts");

			public static readonly int _InstanceOffset = global::UnityEngine.Shader.PropertyToID("_InstanceOffset");

			public static readonly int _OutputInstanceOffset = global::UnityEngine.Shader.PropertyToID("_OutputInstanceOffset");

			public static readonly int _ValidComponentIndices = global::UnityEngine.Shader.PropertyToID("_ValidComponentIndices");

			public static readonly int _ComponentByteCounts = global::UnityEngine.Shader.PropertyToID("_ComponentByteCounts");

			public static readonly int _InputComponentAddresses = global::UnityEngine.Shader.PropertyToID("_InputComponentAddresses");

			public static readonly int _OutputComponentAddresses = global::UnityEngine.Shader.PropertyToID("_OutputComponentAddresses");

			public static readonly int _InputComponentInstanceIndexRanges = global::UnityEngine.Shader.PropertyToID("_InputComponentInstanceIndexRanges");

			public static readonly int _InputBuffer = global::UnityEngine.Shader.PropertyToID("_InputBuffer");

			public static readonly int _OutputBuffer = global::UnityEngine.Shader.PropertyToID("_OutputBuffer");
		}

		public struct GPUResources : global::System.IDisposable
		{
			public global::UnityEngine.ComputeShader cs;

			public int kernelId;

			public void LoadShaders(global::UnityEngine.Rendering.GPUResidentDrawerResources resources)
			{
				if (cs == null)
				{
					cs = resources.instanceDataBufferCopyKernels;
					kernelId = cs.FindKernel("MainCopyInstances");
				}
			}

			public void CreateResources()
			{
			}

			public void Dispose()
			{
				cs = null;
			}
		}

		private global::UnityEngine.Rendering.GPUInstanceDataBuffer m_SrcBuffer;

		private global::UnityEngine.Rendering.GPUInstanceDataBuffer m_DstBuffer;

		public unsafe GPUInstanceDataBufferGrower(global::UnityEngine.Rendering.GPUInstanceDataBuffer sourceBuffer, in global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo)
		{
			m_SrcBuffer = sourceBuffer;
			m_DstBuffer = null;
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				if (instanceNumInfo.InstanceNums[i] > sourceBuffer.instanceNumInfo.InstanceNums[i])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			global::UnityEngine.Rendering.GPUInstanceDataBufferBuilder gPUInstanceDataBufferBuilder = default(global::UnityEngine.Rendering.GPUInstanceDataBufferBuilder);
			foreach (global::UnityEngine.Rendering.GPUInstanceComponentDesc description in sourceBuffer.descriptions)
			{
				gPUInstanceDataBufferBuilder.AddComponent(description.propertyID, description.isOverriden, description.byteSize, description.isPerInstance, description.instanceType, description.componentGroup);
			}
			m_DstBuffer = gPUInstanceDataBufferBuilder.Build(in instanceNumInfo);
			gPUInstanceDataBufferBuilder.Dispose();
		}

		public global::UnityEngine.Rendering.GPUInstanceDataBuffer SubmitToGpu(ref global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.GPUResources gpuResources)
		{
			if (m_DstBuffer == null)
			{
				return m_SrcBuffer;
			}
			if (m_SrcBuffer.instanceNumInfo.GetTotalInstanceNum() == 0)
			{
				return m_DstBuffer;
			}
			gpuResources.CreateResources();
			gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputValidComponentCounts, m_SrcBuffer.perInstanceComponentCount);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._ValidComponentIndices, m_SrcBuffer.validComponentsIndicesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._ComponentByteCounts, m_SrcBuffer.componentByteCountsGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputComponentAddresses, m_SrcBuffer.componentAddressesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputComponentInstanceIndexRanges, m_SrcBuffer.componentInstanceIndexRangesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputComponentAddresses, m_DstBuffer.componentAddressesGpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InputBuffer, m_SrcBuffer.gpuBuffer);
			gpuResources.cs.SetBuffer(gpuResources.kernelId, global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputBuffer, m_DstBuffer.gpuBuffer);
			for (int i = 0; i < 2; i++)
			{
				int instanceNum = m_SrcBuffer.instanceNumInfo.GetInstanceNum((global::UnityEngine.Rendering.InstanceType)i);
				if (instanceNum > 0)
				{
					int val = m_SrcBuffer.instancesNumPrefixSum[i];
					int val2 = m_DstBuffer.instancesNumPrefixSum[i];
					gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InstanceCounts, instanceNum);
					gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._InstanceOffset, val);
					gpuResources.cs.SetInt(global::UnityEngine.Rendering.GPUInstanceDataBufferGrower.CopyInstancesKernelIDs._OutputInstanceOffset, val2);
					gpuResources.cs.Dispatch(gpuResources.kernelId, (instanceNum + 63) / 64, 1, 1);
				}
			}
			return m_DstBuffer;
		}

		public void Dispose()
		{
		}
	}
}

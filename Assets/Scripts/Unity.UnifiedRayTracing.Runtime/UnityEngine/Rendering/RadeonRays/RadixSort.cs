namespace UnityEngine.Rendering.RadeonRays
{
	internal class RadixSort
	{
		private readonly global::UnityEngine.ComputeShader shaderBitHistogram;

		private readonly int kernelBitHistogram;

		private readonly global::UnityEngine.ComputeShader shaderScatter;

		private readonly int kernelScatter;

		private readonly global::UnityEngine.Rendering.RadeonRays.Scan scan;

		private const uint kKeysPerThread = 4u;

		private const uint kGroupSize = 256u;

		private const uint kKeysPerGroup = 1024u;

		private const int kNumBitsPerPass = 4;

		public RadixSort(global::UnityEngine.Rendering.RadeonRays.RadeonRaysShaders shaders)
		{
			shaderBitHistogram = shaders.bitHistogram;
			kernelBitHistogram = shaderBitHistogram.FindKernel("BitHistogram");
			shaderScatter = shaders.scatter;
			kernelScatter = shaderScatter.FindKernel("Scatter");
			scan = new global::UnityEngine.Rendering.RadeonRays.Scan(shaders);
		}

		public void Execute(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer buffer, uint inputKeysOffset, uint outputKeysOffset, uint inputValuesOffset, uint outputValuesOffset, uint scratchDataOffset, uint size)
		{
			uint num = 16 * global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(size, 1024u);
			uint threadGroupsX = global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(size, 1024u);
			uint num2 = scratchDataOffset + size;
			uint num3 = num2 + size;
			uint scratchDataOffset2 = num3 + num;
			uint num4 = outputKeysOffset;
			uint num5 = outputValuesOffset;
			uint num6 = scratchDataOffset;
			uint num7 = num2;
			for (uint num8 = 0u; num8 < 32; num8 += 4)
			{
				cmd.SetComputeIntParam(shaderBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_num_keys, (int)size);
				cmd.SetComputeIntParam(shaderBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_num_blocks, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(size, 1024u));
				cmd.SetComputeIntParam(shaderBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_bit_shift, (int)num8);
				cmd.SetComputeBufferParam(shaderBitHistogram, kernelBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_buffer, buffer);
				cmd.SetComputeIntParam(shaderBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_input_keys_offset, (int)((num8 == 0) ? inputKeysOffset : num4));
				cmd.SetComputeIntParam(shaderBitHistogram, global::UnityEngine.Rendering.RadeonRays.SID.g_group_histograms_offset, (int)num3);
				cmd.DispatchCompute(shaderBitHistogram, kernelBitHistogram, (int)threadGroupsX, 1, 1);
				scan.Execute(cmd, buffer, num3, num3, scratchDataOffset2, num);
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_num_keys, (int)size);
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_num_blocks, (int)global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(size, 1024u));
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_constants_bit_shift, (int)num8);
				cmd.SetComputeBufferParam(shaderScatter, kernelScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_buffer, buffer);
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_input_keys_offset, (int)((num8 == 0) ? inputKeysOffset : num4));
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_group_histograms_offset, (int)num3);
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_output_keys_offset, (int)num6);
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_input_values_offset, (int)((num8 == 0) ? inputValuesOffset : num5));
				cmd.SetComputeIntParam(shaderScatter, global::UnityEngine.Rendering.RadeonRays.SID.g_output_values_offset, (int)num7);
				cmd.DispatchCompute(shaderScatter, kernelScatter, (int)threadGroupsX, 1, 1);
				uint num9 = num4;
				uint num10 = num6;
				num6 = num9;
				num4 = num10;
				uint num11 = num5;
				num10 = num7;
				num7 = num11;
				num5 = num10;
			}
		}

		public static ulong GetScratchDataSizeInDwords(uint size)
		{
			uint num = 16 * global::UnityEngine.Rendering.RadeonRays.Common.CeilDivide(size, 1024u);
			return (ulong)(0L + (long)num + (2 * size + 1024)) + global::UnityEngine.Rendering.RadeonRays.Scan.GetScratchDataSizeInDwords(num);
		}
	}
}

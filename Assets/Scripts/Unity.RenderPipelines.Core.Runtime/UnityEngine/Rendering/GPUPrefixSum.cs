namespace UnityEngine.Rendering
{
	public struct GPUPrefixSum
	{
		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Utilities\\GPUPrefixSum\\GPUPrefixSum.Data.cs")]
		internal static class ShaderDefs
		{
			public const int GroupSize = 128;

			public const int ArgsBufferStride = 16;

			public const int ArgsBufferUpper = 0;

			public const int ArgsBufferLower = 8;

			public static int DivUpGroup(int value)
			{
				return (value + 128 - 1) / 128;
			}

			public static int AlignUpGroup(int value)
			{
				return DivUpGroup(value) * 128;
			}

			public static void CalculateTotalBufferSize(int maxElementCount, out int totalSize, out int levelCounts)
			{
				int num = (totalSize = AlignUpGroup(maxElementCount));
				levelCounts = 1;
				while (num > 128)
				{
					num = AlignUpGroup(DivUpGroup(num));
					totalSize += num;
					levelCounts++;
				}
			}
		}

		[global::UnityEngine.Rendering.GenerateHLSL(global::UnityEngine.Rendering.PackingRules.Exact, true, false, false, 1, false, false, false, -1, ".\\Library\\PackageCache\\com.unity.render-pipelines.core@04ab0eefa0c3\\Runtime\\Utilities\\GPUPrefixSum\\GPUPrefixSum.Data.cs")]
		public struct LevelOffsets
		{
			public uint count;

			public uint offset;

			public uint parentOffset;
		}

		public struct RenderGraphResources
		{
			internal int alignedElementCount;

			internal int maxBufferCount;

			internal int maxLevelCount;

			internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle prefixBuffer0;

			internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle prefixBuffer1;

			internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle totalLevelCountBuffer;

			internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle levelOffsetBuffer;

			internal global::UnityEngine.Rendering.RenderGraphModule.BufferHandle indirectDispatchArgsBuffer;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle output => prefixBuffer0;

			[global::System.Obsolete("This Create signature is deprecated and will be removed in the future. Please use Create(IBaseRenderGraphBuilder) instead. #from(6000.3)")]
			public static global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources Create(int newMaxElementCount, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphBuilder builder, bool outputIsTemp = false)
			{
				global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources result = default(global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources);
				result.Initialize(newMaxElementCount, renderGraph, builder, outputIsTemp);
				return result;
			}

			private void Initialize(int newMaxElementCount, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphBuilder builder, bool outputIsTemp = false)
			{
				newMaxElementCount = global::System.Math.Max(newMaxElementCount, 1);
				global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.CalculateTotalBufferSize(newMaxElementCount, out var totalSize, out var levelCounts);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc bufferDesc = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(totalSize, 4, global::UnityEngine.GraphicsBuffer.Target.Raw);
				bufferDesc.name = "prefixBuffer0";
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc = bufferDesc;
				prefixBuffer0 = (outputIsTemp ? builder.CreateTransientBuffer(in desc) : builder.WriteBuffer(renderGraph.CreateBuffer(in desc)));
				prefixBuffer1 = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(newMaxElementCount, 4, global::UnityEngine.GraphicsBuffer.Target.Raw)
				{
					name = "prefixBuffer1"
				});
				totalLevelCountBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(1, 4, global::UnityEngine.GraphicsBuffer.Target.Raw)
				{
					name = "totalLevelCountBuffer"
				});
				levelOffsetBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(levelCounts, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.GPUPrefixSum.LevelOffsets>(), global::UnityEngine.GraphicsBuffer.Target.Structured)
				{
					name = "levelOffsetBuffer"
				});
				indirectDispatchArgsBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(16 * levelCounts, 4, global::UnityEngine.GraphicsBuffer.Target.Structured | global::UnityEngine.GraphicsBuffer.Target.IndirectArguments)
				{
					name = "indirectDispatchArgsBuffer"
				});
				alignedElementCount = global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.AlignUpGroup(newMaxElementCount);
				maxBufferCount = totalSize;
				maxLevelCount = levelCounts;
			}

			public static global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources Create(int newMaxElementCount, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder, bool outputIsTemp = false)
			{
				global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources result = default(global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources);
				result.Initialize(newMaxElementCount, renderGraph, builder, outputIsTemp);
				return result;
			}

			private void Initialize(int newMaxElementCount, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder, bool outputIsTemp = false)
			{
				newMaxElementCount = global::System.Math.Max(newMaxElementCount, 1);
				global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.CalculateTotalBufferSize(newMaxElementCount, out var totalSize, out var levelCounts);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc bufferDesc = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(totalSize, 4, global::UnityEngine.GraphicsBuffer.Target.Raw);
				bufferDesc.name = "prefixBuffer0";
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc = bufferDesc;
				if (outputIsTemp)
				{
					prefixBuffer0 = builder.CreateTransientBuffer(in desc);
				}
				else
				{
					prefixBuffer0 = renderGraph.CreateBuffer(in desc);
					builder.UseBuffer(in prefixBuffer0, global::UnityEngine.Rendering.RenderGraphModule.AccessFlags.Write);
				}
				prefixBuffer1 = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(newMaxElementCount, 4, global::UnityEngine.GraphicsBuffer.Target.Raw)
				{
					name = "prefixBuffer1"
				});
				totalLevelCountBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(1, 4, global::UnityEngine.GraphicsBuffer.Target.Raw)
				{
					name = "totalLevelCountBuffer"
				});
				levelOffsetBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(levelCounts, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.GPUPrefixSum.LevelOffsets>(), global::UnityEngine.GraphicsBuffer.Target.Structured)
				{
					name = "levelOffsetBuffer"
				});
				indirectDispatchArgsBuffer = builder.CreateTransientBuffer(new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(16 * levelCounts, 4, global::UnityEngine.GraphicsBuffer.Target.Structured | global::UnityEngine.GraphicsBuffer.Target.IndirectArguments)
				{
					name = "indirectDispatchArgsBuffer"
				});
				alignedElementCount = global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.AlignUpGroup(newMaxElementCount);
				maxBufferCount = totalSize;
				maxLevelCount = levelCounts;
			}
		}

		public struct SupportResources
		{
			internal bool ownsResources;

			internal int alignedElementCount;

			internal int maxBufferCount;

			internal int maxLevelCount;

			internal global::UnityEngine.GraphicsBuffer prefixBuffer0;

			internal global::UnityEngine.GraphicsBuffer prefixBuffer1;

			internal global::UnityEngine.GraphicsBuffer totalLevelCountBuffer;

			internal global::UnityEngine.GraphicsBuffer levelOffsetBuffer;

			internal global::UnityEngine.GraphicsBuffer indirectDispatchArgsBuffer;

			public global::UnityEngine.GraphicsBuffer output => prefixBuffer0;

			public static global::UnityEngine.Rendering.GPUPrefixSum.SupportResources Create(int maxElementCount)
			{
				global::UnityEngine.Rendering.GPUPrefixSum.SupportResources result = new global::UnityEngine.Rendering.GPUPrefixSum.SupportResources
				{
					alignedElementCount = 0,
					ownsResources = true
				};
				result.Resize(maxElementCount);
				return result;
			}

			public static global::UnityEngine.Rendering.GPUPrefixSum.SupportResources Load(global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources shaderGraphResources)
			{
				global::UnityEngine.Rendering.GPUPrefixSum.SupportResources result = new global::UnityEngine.Rendering.GPUPrefixSum.SupportResources
				{
					alignedElementCount = 0,
					ownsResources = false
				};
				result.LoadFromShaderGraph(shaderGraphResources);
				return result;
			}

			internal void Resize(int newMaxElementCount)
			{
				if (!ownsResources)
				{
					throw new global::System.Exception("Cannot resize resources unless they are owned. Use GpuPrefixSumSupportResources.Create() for this.");
				}
				newMaxElementCount = global::System.Math.Max(newMaxElementCount, 1);
				if (alignedElementCount < newMaxElementCount)
				{
					Dispose();
					global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.CalculateTotalBufferSize(newMaxElementCount, out var totalSize, out var levelCounts);
					alignedElementCount = global::UnityEngine.Rendering.GPUPrefixSum.ShaderDefs.AlignUpGroup(newMaxElementCount);
					maxBufferCount = totalSize;
					maxLevelCount = levelCounts;
					prefixBuffer0 = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, totalSize, 4);
					prefixBuffer1 = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, newMaxElementCount, 4);
					totalLevelCountBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Raw, 1, 4);
					levelOffsetBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, levelCounts, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.GPUPrefixSum.LevelOffsets>());
					indirectDispatchArgsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.IndirectArguments, 16 * levelCounts, 4);
				}
			}

			private void LoadFromShaderGraph(global::UnityEngine.Rendering.GPUPrefixSum.RenderGraphResources shaderGraphResources)
			{
				alignedElementCount = shaderGraphResources.alignedElementCount;
				maxBufferCount = shaderGraphResources.maxBufferCount;
				maxLevelCount = shaderGraphResources.maxLevelCount;
				prefixBuffer0 = shaderGraphResources.prefixBuffer0;
				prefixBuffer1 = shaderGraphResources.prefixBuffer1;
				totalLevelCountBuffer = shaderGraphResources.totalLevelCountBuffer;
				levelOffsetBuffer = shaderGraphResources.levelOffsetBuffer;
				indirectDispatchArgsBuffer = shaderGraphResources.indirectDispatchArgsBuffer;
			}

			public void Dispose()
			{
				if (alignedElementCount != 0 && ownsResources)
				{
					alignedElementCount = 0;
					TryFreeBuffer(prefixBuffer0);
					TryFreeBuffer(prefixBuffer1);
					TryFreeBuffer(levelOffsetBuffer);
					TryFreeBuffer(indirectDispatchArgsBuffer);
					TryFreeBuffer(totalLevelCountBuffer);
				}
				static void TryFreeBuffer(global::UnityEngine.GraphicsBuffer resource)
				{
					if (resource != null)
					{
						resource.Dispose();
						resource = null;
					}
				}
			}
		}

		public struct DirectArgs
		{
			public bool exclusive;

			public int inputCount;

			public global::UnityEngine.GraphicsBuffer input;

			public global::UnityEngine.Rendering.GPUPrefixSum.SupportResources supportResources;
		}

		public struct IndirectDirectArgs
		{
			public bool exclusive;

			public int inputCountBufferByteOffset;

			public global::UnityEngine.ComputeBuffer inputCountBuffer;

			public global::UnityEngine.GraphicsBuffer input;

			public global::UnityEngine.Rendering.GPUPrefixSum.SupportResources supportResources;
		}

		public struct SystemResources
		{
			public global::UnityEngine.ComputeShader computeAsset;

			internal int kernelCalculateLevelDispatchArgsFromConst;

			internal int kernelCalculateLevelDispatchArgsFromBuffer;

			internal int kernelPrefixSumOnGroup;

			internal int kernelPrefixSumOnGroupExclusive;

			internal int kernelPrefixSumNextInput;

			internal int kernelPrefixSumResolveParent;

			internal int kernelPrefixSumResolveParentExclusive;

			internal void LoadKernels()
			{
				if (!(computeAsset == null))
				{
					kernelCalculateLevelDispatchArgsFromConst = computeAsset.FindKernel("MainCalculateLevelDispatchArgsFromConst");
					kernelCalculateLevelDispatchArgsFromBuffer = computeAsset.FindKernel("MainCalculateLevelDispatchArgsFromBuffer");
					kernelPrefixSumOnGroup = computeAsset.FindKernel("MainPrefixSumOnGroup");
					kernelPrefixSumOnGroupExclusive = computeAsset.FindKernel("MainPrefixSumOnGroupExclusive");
					kernelPrefixSumNextInput = computeAsset.FindKernel("MainPrefixSumNextInput");
					kernelPrefixSumResolveParent = computeAsset.FindKernel("MainPrefixSumResolveParent");
					kernelPrefixSumResolveParentExclusive = computeAsset.FindKernel("MainPrefixSumResolveParentExclusive");
				}
			}
		}

		private static class ShaderIDs
		{
			public static readonly int _InputBuffer = global::UnityEngine.Shader.PropertyToID("_InputBuffer");

			public static readonly int _OutputBuffer = global::UnityEngine.Shader.PropertyToID("_OutputBuffer");

			public static readonly int _InputCountBuffer = global::UnityEngine.Shader.PropertyToID("_InputCountBuffer");

			public static readonly int _TotalLevelsBuffer = global::UnityEngine.Shader.PropertyToID("_TotalLevelsBuffer");

			public static readonly int _OutputTotalLevelsBuffer = global::UnityEngine.Shader.PropertyToID("_OutputTotalLevelsBuffer");

			public static readonly int _OutputDispatchLevelArgsBuffer = global::UnityEngine.Shader.PropertyToID("_OutputDispatchLevelArgsBuffer");

			public static readonly int _LevelsOffsetsBuffer = global::UnityEngine.Shader.PropertyToID("_LevelsOffsetsBuffer");

			public static readonly int _OutputLevelsOffsetsBuffer = global::UnityEngine.Shader.PropertyToID("_OutputLevelsOffsetsBuffer");

			public static readonly int _PrefixSumIntArgs = global::UnityEngine.Shader.PropertyToID("_PrefixSumIntArgs");
		}

		private global::UnityEngine.Rendering.GPUPrefixSum.SystemResources resources;

		public GPUPrefixSum(global::UnityEngine.Rendering.GPUPrefixSum.SystemResources resources)
		{
			this.resources = resources;
			this.resources.LoadKernels();
		}

		private unsafe global::UnityEngine.Vector4 PackPrefixSumArgs(int a, int b, int c, int d)
		{
			return new global::UnityEngine.Vector4(*(float*)(&a), *(float*)(&b), *(float*)(&c), *(float*)(&d));
		}

		internal void ExecuteCommonIndirect(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, global::UnityEngine.GraphicsBuffer inputBuffer, in global::UnityEngine.Rendering.GPUPrefixSum.SupportResources supportResources, bool isExclusive)
		{
			int kernelIndex = (isExclusive ? resources.kernelPrefixSumOnGroupExclusive : resources.kernelPrefixSumOnGroup);
			int kernelIndex2 = (isExclusive ? resources.kernelPrefixSumResolveParentExclusive : resources.kernelPrefixSumResolveParent);
			for (int i = 0; i < supportResources.maxLevelCount; i++)
			{
				global::UnityEngine.Vector4 val = PackPrefixSumArgs(0, 0, 0, i);
				cmdBuffer.SetComputeVectorParam(resources.computeAsset, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, val);
				if (i == 0)
				{
					cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._InputBuffer, inputBuffer);
				}
				else
				{
					cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._InputBuffer, supportResources.prefixBuffer1);
				}
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._TotalLevelsBuffer, supportResources.totalLevelCountBuffer);
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer0);
				cmdBuffer.DispatchCompute(resources.computeAsset, kernelIndex, supportResources.indirectDispatchArgsBuffer, (uint)(i * 16 * 4));
				if (i != supportResources.maxLevelCount - 1)
				{
					cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelPrefixSumNextInput, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._InputBuffer, supportResources.prefixBuffer0);
					cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelPrefixSumNextInput, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
					cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelPrefixSumNextInput, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer1);
					cmdBuffer.DispatchCompute(resources.computeAsset, resources.kernelPrefixSumNextInput, supportResources.indirectDispatchArgsBuffer, (uint)((i + 1) * 16 * 4));
				}
			}
			for (int num = supportResources.maxLevelCount - 1; num >= 1; num--)
			{
				global::UnityEngine.Vector4 val2 = PackPrefixSumArgs(0, 0, 0, num);
				cmdBuffer.SetComputeVectorParam(resources.computeAsset, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, val2);
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex2, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._InputBuffer, inputBuffer);
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex2, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputBuffer, supportResources.prefixBuffer0);
				cmdBuffer.SetComputeBufferParam(resources.computeAsset, kernelIndex2, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._LevelsOffsetsBuffer, supportResources.levelOffsetBuffer);
				cmdBuffer.DispatchCompute(resources.computeAsset, kernelIndex2, supportResources.indirectDispatchArgsBuffer, (uint)(((num - 1) * 16 + 8) * 4));
			}
		}

		public void DispatchDirect(global::UnityEngine.Rendering.IComputeCommandBuffer cmdBuffer, in global::UnityEngine.Rendering.GPUPrefixSum.DirectArgs arguments)
		{
			if (cmdBuffer is global::UnityEngine.Rendering.BaseCommandBuffer baseCommandBuffer)
			{
				DispatchDirect(baseCommandBuffer.m_WrappedCommandBuffer, in arguments);
			}
		}

		public void DispatchDirect(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, in global::UnityEngine.Rendering.GPUPrefixSum.DirectArgs arguments)
		{
			if (arguments.supportResources.prefixBuffer0 == null || arguments.supportResources.prefixBuffer1 == null)
			{
				throw new global::System.Exception("Support resources are not valid.");
			}
			if (arguments.input == null)
			{
				throw new global::System.Exception("Input source buffer cannot be null.");
			}
			if (arguments.inputCount > arguments.supportResources.alignedElementCount)
			{
				throw new global::System.Exception("Input count exceeds maximum count of support resources. Ensure to create support resources with enough space.");
			}
			global::UnityEngine.Vector4 val = PackPrefixSumArgs(arguments.inputCount, arguments.supportResources.maxLevelCount, 0, 0);
			cmdBuffer.SetComputeVectorParam(resources.computeAsset, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, val);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromConst, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputLevelsOffsetsBuffer, arguments.supportResources.levelOffsetBuffer);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromConst, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputDispatchLevelArgsBuffer, arguments.supportResources.indirectDispatchArgsBuffer);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromConst, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputTotalLevelsBuffer, arguments.supportResources.totalLevelCountBuffer);
			cmdBuffer.DispatchCompute(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromConst, 1, 1, 1);
			ExecuteCommonIndirect(cmdBuffer, arguments.input, in arguments.supportResources, arguments.exclusive);
		}

		public void DispatchIndirect(global::UnityEngine.Rendering.IComputeCommandBuffer cmdBuffer, in global::UnityEngine.Rendering.GPUPrefixSum.IndirectDirectArgs arguments)
		{
			if (cmdBuffer is global::UnityEngine.Rendering.BaseCommandBuffer baseCommandBuffer)
			{
				DispatchIndirect(baseCommandBuffer.m_WrappedCommandBuffer, in arguments);
			}
		}

		public void DispatchIndirect(global::UnityEngine.Rendering.CommandBuffer cmdBuffer, in global::UnityEngine.Rendering.GPUPrefixSum.IndirectDirectArgs arguments)
		{
			if (arguments.supportResources.prefixBuffer0 == null || arguments.supportResources.prefixBuffer1 == null)
			{
				throw new global::System.Exception("Support resources are not valid.");
			}
			if (arguments.input == null || arguments.inputCountBuffer == null)
			{
				throw new global::System.Exception("Input source buffer and inputCountBuffer cannot be null.");
			}
			global::UnityEngine.Vector4 val = PackPrefixSumArgs(0, arguments.supportResources.maxLevelCount, arguments.inputCountBufferByteOffset, 0);
			cmdBuffer.SetComputeVectorParam(resources.computeAsset, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._PrefixSumIntArgs, val);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromBuffer, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._InputCountBuffer, arguments.inputCountBuffer);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromBuffer, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputLevelsOffsetsBuffer, arguments.supportResources.levelOffsetBuffer);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromBuffer, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputDispatchLevelArgsBuffer, arguments.supportResources.indirectDispatchArgsBuffer);
			cmdBuffer.SetComputeBufferParam(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromBuffer, global::UnityEngine.Rendering.GPUPrefixSum.ShaderIDs._OutputTotalLevelsBuffer, arguments.supportResources.totalLevelCountBuffer);
			cmdBuffer.DispatchCompute(resources.computeAsset, resources.kernelCalculateLevelDispatchArgsFromBuffer, 1, 1, 1);
			ExecuteCommonIndirect(cmdBuffer, arguments.input, in arguments.supportResources, arguments.exclusive);
		}
	}
}

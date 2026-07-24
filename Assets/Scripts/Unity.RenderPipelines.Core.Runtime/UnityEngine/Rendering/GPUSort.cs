namespace UnityEngine.Rendering
{
	public struct GPUSort
	{
		private enum Stage
		{
			LocalBMS = 0,
			LocalDisperse = 1,
			BigFlip = 2,
			BigDisperse = 3
		}

		public struct Args
		{
			public uint count;

			public uint maxDepth;

			public global::UnityEngine.GraphicsBuffer inputKeys;

			public global::UnityEngine.GraphicsBuffer inputValues;

			public global::UnityEngine.Rendering.GPUSort.SupportResources resources;

			internal int workGroupCount;
		}

		public struct RenderGraphResources
		{
			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle sortBufferKeys;

			public global::UnityEngine.Rendering.RenderGraphModule.BufferHandle sortBufferValues;

			[global::System.Obsolete("This Create signature is deprecated and will be removed in the future. Please use Create(IBaseRenderGraphBuilder) instead. #from(6000.3)")]
			public static global::UnityEngine.Rendering.GPUSort.RenderGraphResources Create(int count, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphBuilder builder)
			{
				global::UnityEngine.GraphicsBuffer.Target target = global::UnityEngine.GraphicsBuffer.Target.CopyDestination | global::UnityEngine.GraphicsBuffer.Target.Raw;
				global::UnityEngine.Rendering.GPUSort.RenderGraphResources result = default(global::UnityEngine.Rendering.GPUSort.RenderGraphResources);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(count, 4, target)
				{
					name = "Keys"
				};
				result.sortBufferKeys = builder.CreateTransientBuffer(in desc);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc2 = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(count, 4, target)
				{
					name = "Values"
				};
				result.sortBufferValues = builder.CreateTransientBuffer(in desc2);
				return result;
			}

			public static global::UnityEngine.Rendering.GPUSort.RenderGraphResources Create(int count, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph renderGraph, global::UnityEngine.Rendering.RenderGraphModule.IBaseRenderGraphBuilder builder)
			{
				global::UnityEngine.GraphicsBuffer.Target target = global::UnityEngine.GraphicsBuffer.Target.CopyDestination | global::UnityEngine.GraphicsBuffer.Target.Raw;
				global::UnityEngine.Rendering.GPUSort.RenderGraphResources result = default(global::UnityEngine.Rendering.GPUSort.RenderGraphResources);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(count, 4, target)
				{
					name = "Keys"
				};
				result.sortBufferKeys = builder.CreateTransientBuffer(in desc);
				global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc2 = new global::UnityEngine.Rendering.RenderGraphModule.BufferDesc(count, 4, target)
				{
					name = "Values"
				};
				result.sortBufferValues = builder.CreateTransientBuffer(in desc2);
				return result;
			}
		}

		public struct SupportResources
		{
			public global::UnityEngine.GraphicsBuffer sortBufferKeys;

			public global::UnityEngine.GraphicsBuffer sortBufferValues;

			public static global::UnityEngine.Rendering.GPUSort.SupportResources Load(global::UnityEngine.Rendering.GPUSort.RenderGraphResources renderGraphResources)
			{
				return new global::UnityEngine.Rendering.GPUSort.SupportResources
				{
					sortBufferKeys = renderGraphResources.sortBufferKeys,
					sortBufferValues = renderGraphResources.sortBufferValues
				};
			}

			public void Dispose()
			{
				if (sortBufferKeys != null)
				{
					sortBufferKeys.Dispose();
					sortBufferKeys = null;
				}
				if (sortBufferValues != null)
				{
					sortBufferValues.Dispose();
					sortBufferValues = null;
				}
			}
		}

		public struct SystemResources
		{
			public global::UnityEngine.ComputeShader computeAsset;
		}

		private const uint kWorkGroupSize = 1024u;

		private global::UnityEngine.Rendering.LocalKeyword[] m_Keywords;

		private global::UnityEngine.Rendering.GPUSort.SystemResources resources;

		public GPUSort(global::UnityEngine.Rendering.GPUSort.SystemResources resources)
		{
			this.resources = resources;
			m_Keywords = new global::UnityEngine.Rendering.LocalKeyword[4]
			{
				new global::UnityEngine.Rendering.LocalKeyword(resources.computeAsset, "STAGE_BMS"),
				new global::UnityEngine.Rendering.LocalKeyword(resources.computeAsset, "STAGE_LOCAL_DISPERSE"),
				new global::UnityEngine.Rendering.LocalKeyword(resources.computeAsset, "STAGE_BIG_FLIP"),
				new global::UnityEngine.Rendering.LocalKeyword(resources.computeAsset, "STAGE_BIG_DISPERSE")
			};
		}

		private void DispatchStage(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.GPUSort.Args args, uint h, global::UnityEngine.Rendering.GPUSort.Stage stage)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, global::UnityEngine.Rendering.ProfilingSampler.Get(stage)))
			{
				global::UnityEngine.Rendering.LocalKeyword[] keywords = m_Keywords;
				for (int i = 0; i < keywords.Length; i++)
				{
					global::UnityEngine.Rendering.LocalKeyword keyword = keywords[i];
					cmd.SetKeyword(resources.computeAsset, in keyword, value: false);
				}
				cmd.SetKeyword(resources.computeAsset, in m_Keywords[(int)stage], value: true);
				cmd.SetComputeIntParam(resources.computeAsset, "_H", (int)h);
				cmd.SetComputeIntParam(resources.computeAsset, "_Total", (int)args.count);
				cmd.SetComputeBufferParam(resources.computeAsset, 0, "_KeyBuffer", args.resources.sortBufferKeys);
				cmd.SetComputeBufferParam(resources.computeAsset, 0, "_ValueBuffer", args.resources.sortBufferValues);
				cmd.DispatchCompute(resources.computeAsset, 0, args.workGroupCount, 1, 1);
			}
		}

		private void CopyBuffer(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer src, global::UnityEngine.GraphicsBuffer dst)
		{
			global::UnityEngine.Rendering.LocalKeyword[] keywords = m_Keywords;
			for (int i = 0; i < keywords.Length; i++)
			{
				global::UnityEngine.Rendering.LocalKeyword keyword = keywords[i];
				cmd.SetKeyword(resources.computeAsset, in keyword, value: false);
			}
			int num = src.count * src.stride / 4;
			cmd.SetComputeBufferParam(resources.computeAsset, 1, "_CopySrcBuffer", src);
			cmd.SetComputeBufferParam(resources.computeAsset, 1, "_CopyDstBuffer", dst);
			cmd.SetComputeIntParam(resources.computeAsset, "_CopyEntriesCount", num);
			cmd.DispatchCompute(resources.computeAsset, 1, (num + 63) / 64, 1, 1);
		}

		internal static int DivRoundUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		public void Dispatch(global::UnityEngine.Rendering.IComputeCommandBuffer cmd, global::UnityEngine.Rendering.GPUSort.Args args)
		{
			if (cmd is global::UnityEngine.Rendering.BaseCommandBuffer baseCommandBuffer)
			{
				Dispatch(baseCommandBuffer.m_WrappedCommandBuffer, args);
			}
		}

		public void Dispatch(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.GPUSort.Args args)
		{
			uint count = args.count;
			CopyBuffer(cmd, args.inputKeys, args.resources.sortBufferKeys);
			CopyBuffer(cmd, args.inputValues, args.resources.sortBufferValues);
			args.workGroupCount = global::System.Math.Max(1, DivRoundUp((int)count, 2048));
			uint num = global::System.Math.Min(2048u, args.maxDepth);
			DispatchStage(cmd, args, num, global::UnityEngine.Rendering.GPUSort.Stage.LocalBMS);
			for (num *= 2; num <= global::System.Math.Min(count, args.maxDepth); num *= 2)
			{
				DispatchStage(cmd, args, num, global::UnityEngine.Rendering.GPUSort.Stage.BigFlip);
				for (uint num2 = num / 2; num2 > 1; num2 /= 2)
				{
					if (num2 <= 2048)
					{
						DispatchStage(cmd, args, num2, global::UnityEngine.Rendering.GPUSort.Stage.LocalDisperse);
						break;
					}
					DispatchStage(cmd, args, num2, global::UnityEngine.Rendering.GPUSort.Stage.BigDisperse);
				}
			}
		}
	}
}

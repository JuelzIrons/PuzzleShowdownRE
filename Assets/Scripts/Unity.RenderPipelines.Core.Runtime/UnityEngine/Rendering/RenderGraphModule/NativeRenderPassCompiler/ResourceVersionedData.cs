namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal struct ResourceVersionedData
	{
		public bool written;

		public int writePassId;

		public int numReaders;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void SetWritingPass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int passId)
		{
			writePassId = passId;
			written = true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void RegisterReadingPass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int passId, int index)
		{
			ctx.resources.readerData[h.iType][ctx.resources.IndexReader(in h, numReaders)] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData(passId, index);
			numReaders++;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public void RemoveReadingPass(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx, in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int passId)
		{
			int num = 0;
			while (num < numReaders)
			{
				ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData reference = ref ctx.resources.readerData[h.iType].ElementAt(ctx.resources.IndexReader(in h, num));
				if (reference.passId == passId)
				{
					if (num < numReaders - 1)
					{
						reference = ctx.resources.readerData[h.iType][ctx.resources.IndexReader(in h, numReaders - 1)];
					}
					numReaders--;
				}
				else
				{
					num++;
				}
			}
		}
	}
}

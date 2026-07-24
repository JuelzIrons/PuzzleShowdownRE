internal class RenderGraphCompilationCache
{
	private struct HashEntry<T>
	{
		public int hash;

		public int lastFrameUsed;

		public T compiledGraph;
	}

	private global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph>> m_HashEntries = new global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph>>();

	private global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData>> m_NativeHashEntries = new global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData>>();

	private global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph> m_CompiledGraphPool = new global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph>();

	private global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData> m_NativeCompiledGraphPool = new global::System.Collections.Generic.Stack<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData>();

	private static global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph>>.SortComparer s_EntryComparer = HashEntryComparer;

	private static global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData>>.SortComparer s_NativeEntryComparer = HashEntryComparer;

	private const int k_CachedGraphCount = 20;

	private static int s_Hash;

	private static int HashEntryComparer<T>(RenderGraphCompilationCache.HashEntry<T> a, RenderGraphCompilationCache.HashEntry<T> b)
	{
		if (a.lastFrameUsed < b.lastFrameUsed)
		{
			return -1;
		}
		if (a.lastFrameUsed > b.lastFrameUsed)
		{
			return 1;
		}
		return 0;
	}

	public RenderGraphCompilationCache()
	{
		for (int i = 0; i < 20; i++)
		{
			m_CompiledGraphPool.Push(new global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph());
			m_NativeCompiledGraphPool.Push(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData());
		}
	}

	private bool GetCompilationCache<T>(int hash, int frameIndex, out T outGraph, global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<T>> hashEntries, global::System.Collections.Generic.Stack<T> pool, global::UnityEngine.Rendering.DynamicArray<RenderGraphCompilationCache.HashEntry<T>>.SortComparer comparer) where T : global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.ICompiledGraph
	{
		s_Hash = hash;
		int num = hashEntries.FindIndex((RenderGraphCompilationCache.HashEntry<T> hashEntry) => hashEntry.hash == s_Hash);
		if (num != -1)
		{
			ref RenderGraphCompilationCache.HashEntry<T> reference = ref hashEntries[num];
			outGraph = reference.compiledGraph;
			reference.lastFrameUsed = frameIndex;
			return true;
		}
		if (pool.Count != 0)
		{
			RenderGraphCompilationCache.HashEntry<T> value = new RenderGraphCompilationCache.HashEntry<T>
			{
				hash = hash,
				lastFrameUsed = frameIndex,
				compiledGraph = pool.Pop()
			};
			hashEntries.Add(in value);
			outGraph = value.compiledGraph;
			return false;
		}
		global::UnityEngine.Rendering.DynamicArrayExtensions.QuickSort(hashEntries, comparer);
		ref RenderGraphCompilationCache.HashEntry<T> reference2 = ref hashEntries[0];
		reference2.hash = hash;
		reference2.lastFrameUsed = frameIndex;
		reference2.compiledGraph.Clear();
		outGraph = reference2.compiledGraph;
		return false;
	}

	public bool GetCompilationCache(int hash, int frameIndex, out global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph outGraph)
	{
		return GetCompilationCache(hash, frameIndex, out outGraph, m_HashEntries, m_CompiledGraphPool, s_EntryComparer);
	}

	public bool GetCompilationCache(int hash, int frameIndex, out global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData outGraph)
	{
		return GetCompilationCache(hash, frameIndex, out outGraph, m_NativeHashEntries, m_NativeCompiledGraphPool, s_NativeEntryComparer);
	}

	public void Clear()
	{
		for (int i = 0; i < m_HashEntries.size; i++)
		{
			m_HashEntries[i].compiledGraph.Clear();
			m_CompiledGraphPool.Push(m_HashEntries[i].compiledGraph);
		}
		m_HashEntries.Clear();
		for (int j = 0; j < m_NativeHashEntries.size; j++)
		{
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData compiledGraph = m_NativeHashEntries[j].compiledGraph;
			compiledGraph.Clear();
			m_NativeCompiledGraphPool.Push(compiledGraph);
		}
		m_NativeHashEntries.Clear();
	}

	public void Cleanup()
	{
		for (int i = 0; i < m_HashEntries.size; i++)
		{
			m_HashEntries[i].compiledGraph.Clear();
		}
		m_HashEntries.Clear();
		global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.CompiledGraph[] array = m_CompiledGraphPool.ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			array[j].Clear();
		}
		for (int k = 0; k < m_NativeHashEntries.size; k++)
		{
			m_NativeHashEntries[k].compiledGraph.Dispose();
		}
		m_NativeHashEntries.Clear();
		global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData[] array2 = m_NativeCompiledGraphPool.ToArray();
		for (int l = 0; l < array2.Length; l++)
		{
			array2[l].Dispose();
		}
	}
}

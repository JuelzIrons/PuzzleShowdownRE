namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal class CompilerContextData : global::System.IDisposable, global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.ICompiledGraph
	{
		public ref struct PassIterator
		{
			private readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData m_Ctx;

			private int m_Index;

			public ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData Current => ref m_Ctx.passData.ElementAt(m_Index);

			public PassIterator(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
			{
				m_Ctx = ctx;
				m_Index = -1;
			}

			public bool MoveNext()
			{
				return ++m_Index < m_Ctx.passData.Length;
			}

			public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.PassIterator GetEnumerator()
			{
				return this;
			}
		}

		public ref struct NativePassIterator
		{
			private readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData m_Ctx;

			private int m_Index;

			public ref readonly global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData Current => ref m_Ctx.nativePassData.ElementAt(m_Index);

			public NativePassIterator(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData ctx)
			{
				m_Ctx = ctx;
				m_Index = -1;
			}

			public bool MoveNext()
			{
				bool flag;
				do
				{
					m_Index++;
					flag = m_Index < m_Ctx.nativePassData.Length;
				}
				while (flag && !m_Ctx.nativePassData.ElementAt(m_Index).IsValid());
				return flag;
			}

			public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator GetEnumerator()
			{
				return this;
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourcesData resources;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> passData;

		public global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.GraphicsFence> fences;

		public global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name> passNames;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData> inputData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData> outputData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData> fragmentData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> sampledData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> createData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle> destroyData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData> randomAccessResourceData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData> nativePassData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.SubPassDescriptor> nativeSubPassData;

		private bool m_AreNativeListsAllocated;

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.PassIterator Passes => new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.PassIterator(this);

		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator NativePasses => new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator(this);

		public CompilerContextData()
		{
			fences = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.GraphicsFence>();
			resources = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourcesData();
			passNames = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name>(0, resize: false);
		}

		private void AllocateNativeDataStructuresIfNeeded(int estimatedNumPasses)
		{
			if (!m_AreNativeListsAllocated)
			{
				passData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData>(estimatedNumPasses, global::Unity.Collections.AllocatorManager.Persistent);
				inputData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassInputData>(estimatedNumPasses * 2, global::Unity.Collections.AllocatorManager.Persistent);
				outputData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassOutputData>(estimatedNumPasses * 2, global::Unity.Collections.AllocatorManager.Persistent);
				fragmentData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData>(estimatedNumPasses * 4, global::Unity.Collections.AllocatorManager.Persistent);
				sampledData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>(estimatedNumPasses * 2, global::Unity.Collections.AllocatorManager.Persistent);
				randomAccessResourceData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData>(4, global::Unity.Collections.AllocatorManager.Persistent);
				nativePassData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData>(estimatedNumPasses, global::Unity.Collections.AllocatorManager.Persistent);
				nativeSubPassData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.SubPassDescriptor>(estimatedNumPasses, global::Unity.Collections.AllocatorManager.Persistent);
				createData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>(estimatedNumPasses * 2, global::Unity.Collections.AllocatorManager.Persistent);
				destroyData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>(estimatedNumPasses * 2, global::Unity.Collections.AllocatorManager.Persistent);
				m_AreNativeListsAllocated = true;
			}
		}

		public void Initialize(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resourceRegistry, int estimatedNumPasses)
		{
			resources.Initialize(resourceRegistry);
			passNames.Reserve(estimatedNumPasses);
			AllocateNativeDataStructuresIfNeeded(estimatedNumPasses);
		}

		public void Clear()
		{
			passNames.Clear();
			resources.Clear();
			if (m_AreNativeListsAllocated)
			{
				passData.Clear();
				fences.Clear();
				inputData.Clear();
				outputData.Clear();
				fragmentData.Clear();
				sampledData.Clear();
				randomAccessResourceData.Clear();
				nativePassData.Clear();
				nativeSubPassData.Clear();
				createData.Clear();
				destroyData.Clear();
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData UnversionedResourceData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return ref resources.unversionedData[h.iType].ElementAt(h.index);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData VersionedResourceData(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return ref resources[h];
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::System.ReadOnlySpan<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData> Readers(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			int first = resources.IndexReader(in h, 0);
			int numReaders = resources[h].numReaders;
			return global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativeListExtensions.MakeReadOnlySpan(ref resources.readerData[h.iType], first, numReaders);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData ResourceReader(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int i)
		{
			_ = ref resources[h];
			return ref resources.readerData[h.iType].ElementAt(resources.IndexReader(in h, 0) + i);
		}

		public bool TryAddToFragmentList(in global::UnityEngine.Rendering.RenderGraphModule.TextureAccess access, int listFirstIndex, int numItems, out string errorMessage)
		{
			errorMessage = null;
			for (int i = listFirstIndex; i < listFirstIndex + numItems; i++)
			{
				if (fragmentData.ElementAt(i).resource.index == access.textureHandle.handle.index)
				{
					return false;
				}
			}
			fragmentData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassFragmentData(in access.textureHandle.handle, access.flags, access.mipLevel, access.depthSlice));
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name GetFullPassName(int passId)
		{
			return passNames[passId];
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string GetPassName(int passId)
		{
			return passNames[passId].name;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string GetResourceName(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return resources.resourceNames[h.iType][h.index].name;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string GetResourceVersionedName(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return GetResourceName(in h) + " V" + h.version;
		}

		public bool TryAddToRandomAccessResourceList(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int randomWriteSlotIndex, bool preserveCounterValue, int listFirstIndex, int numItems, out string errorMessage)
		{
			errorMessage = null;
			for (int i = listFirstIndex; i < listFirstIndex + numItems; i++)
			{
				if (randomAccessResourceData[i].resource.index == h.index && randomAccessResourceData[i].resource.type == h.type)
				{
					if (randomAccessResourceData[i].resource.version != h.version)
					{
						errorMessage = "A pass is using UseTextureRandomWrite on two versions of the same resource.  Make sure you only access the latest version.";
					}
					return false;
				}
			}
			randomAccessResourceData.Add(new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassRandomWriteData(in h, randomWriteSlotIndex, preserveCounterValue));
			return true;
		}

		public void TagAllPasses(int value)
		{
			for (int i = 0; i < passData.Length; i++)
			{
				passData.ElementAt(i).tag = value;
			}
		}

		public void CullAllPasses(bool isCulled)
		{
			for (int i = 0; i < passData.Length; i++)
			{
				passData.ElementAt(i).culled = isCulled;
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin GetTextureUVOrigin(in global::UnityEngine.Rendering.RenderGraphModule.TextureHandle targetHandle)
		{
			if (targetHandle.handle.IsValid())
			{
				if (UnversionedResourceData(in targetHandle.handle).textureUVOrigin != global::UnityEngine.Rendering.RenderGraphModule.TextureUVOriginSelection.TopLeft)
				{
					return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
				}
				return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.TopLeft;
			}
			return global::UnityEngine.Rendering.RenderGraphModule.TextureUVOrigin.BottomLeft;
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> GetPasses()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.PassData>();
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.PassIterator enumerator = Passes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
			return list;
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData> GetNativePasses()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.NativePassData>();
			global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.CompilerContextData.NativePassIterator enumerator = NativePasses.GetEnumerator();
			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
			return list;
		}

		~CompilerContextData()
		{
			Cleanup();
		}

		public void Dispose()
		{
			Cleanup();
			global::System.GC.SuppressFinalize(this);
		}

		private void Cleanup()
		{
			resources.Dispose();
			if (m_AreNativeListsAllocated)
			{
				passData.Dispose();
				inputData.Dispose();
				outputData.Dispose();
				fragmentData.Dispose();
				sampledData.Dispose();
				createData.Dispose();
				destroyData.Dispose();
				randomAccessResourceData.Dispose();
				nativePassData.Dispose();
				nativeSubPassData.Dispose();
				m_AreNativeListsAllocated = false;
			}
		}
	}
}

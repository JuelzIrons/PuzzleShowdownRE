namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal class ResourcesData
	{
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData>[] unversionedData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData>[] versionedData;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData>[] readerData;

		public int[] MaxVersions;

		public int[] MaxReaders;

		public global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name>[] resourceNames;

		public ref global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData this[global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h] => ref versionedData[h.iType].ElementAt(Index(in h));

		public ResourcesData()
		{
			unversionedData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData>[3];
			versionedData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceVersionedData>[3];
			readerData = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceReaderData>[3];
			resourceNames = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name>[3];
			MaxVersions = new int[3];
			MaxReaders = new int[3];
			for (int i = 0; i < 3; i++)
			{
				resourceNames[i] = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name>(0);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < 3; i++)
			{
				if (unversionedData[i].IsCreated)
				{
					unversionedData[i].Clear();
				}
				if (versionedData[i].IsCreated)
				{
					versionedData[i].Clear();
				}
				if (readerData[i].IsCreated)
				{
					readerData[i].Clear();
				}
				resourceNames[i].Clear();
			}
		}

		private void AllocateAndResizeNativeListIfNeeded<T>(ref global::Unity.Collections.NativeList<T> nativeList, int size, global::Unity.Collections.NativeArrayOptions options) where T : unmanaged
		{
			if (!nativeList.IsCreated)
			{
				nativeList = new global::Unity.Collections.NativeList<T>(size, global::Unity.Collections.AllocatorManager.Persistent);
			}
			nativeList.Resize(size, options);
		}

		public void Initialize(global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources)
		{
			for (int i = 0; i < 3; i++)
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type = (global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType)i;
				int resourceCount = resources.GetResourceCount(type);
				uint num = 0u;
				uint num2 = 0u;
				AllocateAndResizeNativeListIfNeeded(ref unversionedData[i], resourceCount, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				resourceNames[i].Resize(resourceCount, keepContent: true);
				if (resourceCount > 0)
				{
					global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData value = default(global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData);
					value.InitializeNullResource();
					unversionedData[i][0] = value;
					resourceNames[i][0] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name("");
				}
				for (int j = 1; j < resourceCount; j++)
				{
					global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle res = new global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle(j, type, shared: false);
					global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphResource resourceLowLevel = resources.GetResourceLowLevel(in res);
					resourceNames[i][j] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.Name(resourceLowLevel.GetName());
					switch (i)
					{
					case 0:
					{
						global::UnityEngine.Rendering.RenderGraphModule.TextureResource textureResource = resourceLowLevel as global::UnityEngine.Rendering.RenderGraphModule.TextureResource;
						resources.GetRenderTargetInfo(in res, out var outInfo);
						ref global::UnityEngine.Rendering.RenderGraphModule.TextureDesc desc3 = ref textureResource.desc;
						bool isResourceShared3 = resources.IsRenderGraphResourceShared(in res);
						unversionedData[i][j] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData(textureResource, ref outInfo, ref desc3, isResourceShared3);
						break;
					}
					case 1:
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.BufferDesc desc2 = ref (resourceLowLevel as global::UnityEngine.Rendering.RenderGraphModule.BufferResource).desc;
						bool isResourceShared2 = resources.IsRenderGraphResourceShared(in res);
						unversionedData[i][j] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData(resourceLowLevel, ref desc2, isResourceShared2);
						break;
					}
					case 2:
					{
						ref global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureDesc desc = ref (resourceLowLevel as global::UnityEngine.Rendering.RenderGraphModule.RayTracingAccelerationStructureResource).desc;
						bool isResourceShared = resources.IsRenderGraphResourceShared(in res);
						unversionedData[i][j] = new global::UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler.ResourceUnversionedData(resourceLowLevel, ref desc, isResourceShared);
						break;
					}
					default:
						throw new global::System.Exception("Unsupported resource type: " + i);
					}
					num = global::System.Math.Max(num, resourceLowLevel.readCount);
					num2 = global::System.Math.Max(num2, resourceLowLevel.writeCount);
				}
				MaxReaders[i] = (int)(num + 1);
				MaxVersions[i] = (int)(num2 + 1);
				AllocateAndResizeNativeListIfNeeded(ref versionedData[i], MaxVersions[i] * resourceCount, global::Unity.Collections.NativeArrayOptions.ClearMemory);
				AllocateAndResizeNativeListIfNeeded(ref readerData[i], MaxVersions[i] * MaxReaders[i] * resourceCount, global::Unity.Collections.NativeArrayOptions.ClearMemory);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int Index(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h)
		{
			return h.index * MaxVersions[h.iType] + h.version;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public int IndexReader(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int readerID)
		{
			return (h.index * MaxVersions[h.iType] + h.version) * MaxReaders[h.iType] + readerID;
		}

		public void Dispose()
		{
			for (int i = 0; i < 3; i++)
			{
				if (versionedData[i].IsCreated)
				{
					versionedData[i].Dispose();
				}
				if (unversionedData[i].IsCreated)
				{
					unversionedData[i].Dispose();
				}
				if (readerData[i].IsCreated)
				{
					readerData[i].Dispose();
				}
			}
		}
	}
}

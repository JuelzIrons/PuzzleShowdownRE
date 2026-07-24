namespace UnityEngine.Rendering
{
	public sealed class ProbeVolumeBakingSet : global::UnityEngine.ScriptableObject, global::UnityEngine.ISerializationCallbackReceiver
	{
		internal enum Version
		{
			Initial = 0,
			RemoveProbeVolumeSceneData = 1,
			AssetsAlwaysReferenced = 2
		}

		[global::System.Serializable]
		internal class PerScenarioDataInfo
		{
			public int sceneHash;

			public global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellDataAsset;

			public global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellOptionalDataAsset;

			public global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellProbeOcclusionDataAsset;

			private bool m_HasValidData;

			public void Initialize(global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
			{
				m_HasValidData = ComputeHasValidData(shBands);
			}

			public bool IsValid()
			{
				if (cellDataAsset != null)
				{
					return cellDataAsset.IsValid();
				}
				return false;
			}

			public bool HasValidData(global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
			{
				return m_HasValidData;
			}

			public bool ComputeHasValidData(global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
			{
				if (cellDataAsset.FileExists())
				{
					if (shBands != global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL1)
					{
						return cellOptionalDataAsset.FileExists();
					}
					return true;
				}
				return false;
			}
		}

		[global::System.Serializable]
		internal struct CellCounts
		{
			public int bricksCount;

			public int chunksCount;

			public void Add(global::UnityEngine.Rendering.ProbeVolumeBakingSet.CellCounts o)
			{
				bricksCount += o.bricksCount;
				chunksCount += o.chunksCount;
			}
		}

		[global::System.Serializable]
		private struct SerializedPerSceneCellList
		{
			public string sceneGUID;

			public global::System.Collections.Generic.List<int> cellList;
		}

		[global::System.Serializable]
		internal struct ProbeLayerMask
		{
			public global::UnityEngine.RenderingLayerMask mask;

			public string name;
		}

		[global::UnityEngine.SerializeField]
		internal bool singleSceneMode = true;

		[global::UnityEngine.SerializeField]
		internal bool dialogNoProbeVolumeInSetShown;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeBakingProcessSettings settings;

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<string> m_SceneGUIDs = new global::System.Collections.Generic.List<string>();

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("This is now contained in the SceneBakeData structure. #from(2023.3)")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("scenesToNotBake")]
		internal global::System.Collections.Generic.List<string> obsoleteScenesToNotBake = new global::System.Collections.Generic.List<string>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("lightingScenarios")]
		internal global::System.Collections.Generic.List<string> m_LightingScenarios = new global::System.Collections.Generic.List<string>();

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc> cellDescs = new global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc>();

		internal global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellData> cellDataMap = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellData>();

		private global::System.Collections.Generic.List<int> m_TotalIndexList = new global::System.Collections.Generic.List<int>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumeBakingSet.SerializedPerSceneCellList> m_SerializedPerSceneCellList;

		internal global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<int>> perSceneCellLists = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<int>>();

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellSharedDataAsset;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.SerializedDictionary<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo> scenarios = new global::UnityEngine.Rendering.SerializedDictionary<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo>();

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellBricksDataAsset;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeStreamableAsset cellSupportDataAsset;

		[global::UnityEngine.SerializeField]
		internal int chunkSizeInBricks;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Vector3Int maxCellPosition;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Vector3Int minCellPosition;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Bounds globalBounds;

		[global::UnityEngine.SerializeField]
		internal int bakedSimplificationLevels = -1;

		[global::UnityEngine.SerializeField]
		internal float bakedMinDistanceBetweenProbes = -1f;

		[global::UnityEngine.SerializeField]
		internal bool bakedProbeOcclusion;

		[global::UnityEngine.SerializeField]
		internal int bakedSkyOcclusionValue = -1;

		[global::UnityEngine.SerializeField]
		internal int bakedSkyShadingDirectionValue = -1;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Vector3 bakedProbeOffset = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		internal int bakedMaskCount = 1;

		[global::UnityEngine.SerializeField]
		internal global::Unity.Mathematics.uint4 bakedLayerMasks;

		[global::UnityEngine.SerializeField]
		internal int maxSHChunkCount = -1;

		[global::UnityEngine.SerializeField]
		internal int L0ChunkSize;

		[global::UnityEngine.SerializeField]
		internal int L1ChunkSize;

		[global::UnityEngine.SerializeField]
		internal int L2TextureChunkSize;

		[global::UnityEngine.SerializeField]
		internal int ProbeOcclusionChunkSize;

		[global::UnityEngine.SerializeField]
		internal int sharedValidityMaskChunkSize;

		[global::UnityEngine.SerializeField]
		internal int sharedSkyOcclusionL0L1ChunkSize;

		[global::UnityEngine.SerializeField]
		internal int sharedSkyShadingDirectionIndicesChunkSize;

		[global::UnityEngine.SerializeField]
		internal int sharedDataChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportPositionChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportValidityChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportTouchupChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportLayerMaskChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportOffsetsChunkSize;

		[global::UnityEngine.SerializeField]
		internal int supportDataChunkSize;

		[global::UnityEngine.SerializeField]
		internal string lightingScenario = global::UnityEngine.Rendering.ProbeReferenceVolume.defaultLightingScenario;

		private string m_OtherScenario;

		private float m_ScenarioBlendingFactor;

		private global::Unity.IO.LowLevel.Unsafe.ReadCommandArray m_ReadCommandArray;

		private global::Unity.Collections.NativeArray<global::Unity.IO.LowLevel.Unsafe.ReadCommand> m_ReadCommandBuffer;

		private global::System.Collections.Generic.Stack<global::Unity.Collections.NativeArray<byte>> m_ReadOperationScratchBuffers = new global::System.Collections.Generic.Stack<global::Unity.Collections.NativeArray<byte>>();

		private global::System.Collections.Generic.List<int> m_PrunedIndexList = new global::System.Collections.Generic.List<int>();

		private global::System.Collections.Generic.List<int> m_PrunedScenarioIndexList = new global::System.Collections.Generic.List<int>();

		internal const int k_MaxSkyOcclusionBakingSamples = 8192;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Rendering.ProbeVolumeBakingSet.Version version = global::UnityEngine.Rendering.CoreUtils.GetLastEnumValue<global::UnityEngine.Rendering.ProbeVolumeBakingSet.Version>();

		[global::UnityEngine.SerializeField]
		internal bool freezePlacement;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Vector3 probeOffset = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.Range(2f, 5f)]
		public int simplificationLevels = 3;

		[global::UnityEngine.Min(0.1f)]
		public float minDistanceBetweenProbes = 1f;

		public global::UnityEngine.LayerMask renderersLayerMask = -1;

		[global::UnityEngine.Min(0f)]
		public float minRendererVolumeSize = 0.1f;

		public bool skyOcclusion;

		[global::UnityEngine.Rendering.Logarithmic(1, 8192)]
		public int skyOcclusionBakingSamples = 2048;

		[global::UnityEngine.Range(0f, 5f)]
		public int skyOcclusionBakingBounces = 2;

		[global::UnityEngine.Range(0f, 1f)]
		public float skyOcclusionAverageAlbedo = 0.6f;

		public bool skyOcclusionBackFaceCulling;

		public bool skyOcclusionShadingDirection;

		[global::UnityEngine.SerializeField]
		internal bool useRenderingLayers;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.Rendering.ProbeVolumeBakingSet.ProbeLayerMask[] renderingLayerMasks;

		private bool m_HasSupportData;

		private bool m_SharedDataIsValid;

		private bool m_UseStreamingAsset = true;

		internal bool hasDilation
		{
			get
			{
				if (settings.dilationSettings.enableDilation)
				{
					return settings.dilationSettings.dilationDistance > 0f;
				}
				return false;
			}
		}

		public global::System.Collections.Generic.IReadOnlyList<string> sceneGUIDs => m_SceneGUIDs;

		public global::System.Collections.Generic.IReadOnlyList<string> lightingScenarios => m_LightingScenarios;

		internal bool bakedSkyOcclusion
		{
			get
			{
				if (bakedSkyOcclusionValue > 0)
				{
					return true;
				}
				return false;
			}
			set
			{
				bakedSkyOcclusionValue = (value ? 1 : 0);
			}
		}

		internal bool bakedSkyShadingDirection
		{
			get
			{
				if (bakedSkyShadingDirectionValue > 0)
				{
					return true;
				}
				return false;
			}
			set
			{
				bakedSkyShadingDirectionValue = (value ? 1 : 0);
			}
		}

		internal string otherScenario => m_OtherScenario;

		internal float scenarioBlendingFactor => m_ScenarioBlendingFactor;

		public int cellSizeInBricks => GetCellSizeInBricks(bakedSimplificationLevels);

		public int maxSubdivision => GetMaxSubdivision(bakedSimplificationLevels);

		public float minBrickSize => GetMinBrickSize(bakedMinDistanceBetweenProbes);

		public float cellSizeInMeters => (float)cellSizeInBricks * minBrickSize;

		internal global::Unity.Mathematics.uint4 ComputeRegionMasks()
		{
			global::Unity.Mathematics.uint4 result = 0u;
			if (!useRenderingLayers || renderingLayerMasks == null)
			{
				result.x = uint.MaxValue;
			}
			else
			{
				for (int i = 0; i < renderingLayerMasks.Length; i++)
				{
					result[i] = renderingLayerMasks[i].mask;
				}
			}
			return result;
		}

		internal static int GetCellSizeInBricks(int simplificationLevels)
		{
			return (int)global::UnityEngine.Mathf.Pow(3f, simplificationLevels);
		}

		internal static int GetMaxSubdivision(int simplificationLevels)
		{
			return simplificationLevels + 1;
		}

		internal static float GetMinBrickSize(float minDistanceBetweenProbes)
		{
			return global::UnityEngine.Mathf.Max(0.01f, minDistanceBetweenProbes * 3f);
		}

		private void OnValidate()
		{
			singleSceneMode &= m_SceneGUIDs.Count <= 1;
			if (m_LightingScenarios.Count == 0)
			{
				m_LightingScenarios = new global::System.Collections.Generic.List<string> { global::UnityEngine.Rendering.ProbeReferenceVolume.defaultLightingScenario };
			}
			settings.Upgrade();
		}

		private void OnEnable()
		{
			Migrate();
			m_HasSupportData = ComputeHasSupportData();
			m_SharedDataIsValid = ComputeHasValidSharedData();
		}

		internal void Migrate()
		{
			if (version != global::UnityEngine.Rendering.CoreUtils.GetLastEnumValue<global::UnityEngine.Rendering.ProbeVolumeBakingSet.Version>())
			{
				_ = version;
				_ = 1;
				if (version < global::UnityEngine.Rendering.ProbeVolumeBakingSet.Version.AssetsAlwaysReferenced)
				{
					_ = global::UnityEngine.Rendering.ProbeReferenceVolume.instance.isInitialized;
				}
			}
			if (sharedValidityMaskChunkSize == 0)
			{
				sharedValidityMaskChunkSize = global::UnityEngine.Rendering.ProbeBrickPool.GetChunkSizeInProbeCount();
			}
			if (settings.virtualOffsetSettings.validityThreshold == 0f)
			{
				settings.virtualOffsetSettings.validityThreshold = 0.25f;
			}
		}

		private bool ComputeHasValidSharedData()
		{
			if (cellSharedDataAsset != null && cellSharedDataAsset.FileExists())
			{
				return cellBricksDataAsset.FileExists();
			}
			return false;
		}

		internal bool HasValidSharedData()
		{
			return m_SharedDataIsValid;
		}

		internal bool CheckCompatibleCellLayout()
		{
			if (simplificationLevels == bakedSimplificationLevels && minDistanceBetweenProbes == bakedMinDistanceBetweenProbes && skyOcclusion == bakedSkyOcclusion && skyOcclusionShadingDirection == bakedSkyShadingDirection && settings.virtualOffsetSettings.useVirtualOffset == (supportOffsetsChunkSize != 0))
			{
				return useRenderingLayers == (bakedMaskCount != 1);
			}
			return false;
		}

		private bool ComputeHasSupportData()
		{
			if (cellSupportDataAsset != null && cellSupportDataAsset.IsValid())
			{
				return cellSupportDataAsset.FileExists();
			}
			return false;
		}

		internal bool HasSupportData()
		{
			return m_HasSupportData;
		}

		public bool HasBakedData(string scenario = null)
		{
			if (scenario == null)
			{
				return scenarios.ContainsKey(global::UnityEngine.Rendering.ProbeReferenceVolume.defaultLightingScenario);
			}
			if (!global::UnityEngine.Rendering.ProbeReferenceVolume.instance.supportLightingScenarios && scenario != global::UnityEngine.Rendering.ProbeReferenceVolume.defaultLightingScenario)
			{
				return false;
			}
			return scenarios.ContainsKey(scenario);
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (!m_LightingScenarios.Contains(lightingScenario))
			{
				if (m_LightingScenarios.Count != 0)
				{
					lightingScenario = m_LightingScenarios[0];
				}
				else
				{
					lightingScenario = global::UnityEngine.Rendering.ProbeReferenceVolume.defaultLightingScenario;
				}
			}
			perSceneCellLists.Clear();
			foreach (global::UnityEngine.Rendering.ProbeVolumeBakingSet.SerializedPerSceneCellList serializedPerSceneCell in m_SerializedPerSceneCellList)
			{
				perSceneCellLists.Add(serializedPerSceneCell.sceneGUID, serializedPerSceneCell.cellList);
			}
			if (m_OtherScenario == "")
			{
				m_OtherScenario = null;
			}
			if (bakedSimplificationLevels == -1)
			{
				bakedSimplificationLevels = simplificationLevels;
				bakedMinDistanceBetweenProbes = minDistanceBetweenProbes;
			}
			if (bakedSkyOcclusionValue == -1)
			{
				bakedSkyOcclusion = false;
			}
			if (bakedSkyShadingDirectionValue == -1)
			{
				bakedSkyShadingDirection = false;
			}
			if (cellDescs.Count == 0)
			{
				return;
			}
			global::System.Collections.Generic.Dictionary<int, global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc>.ValueCollection.Enumerator enumerator2 = cellDescs.Values.GetEnumerator();
			enumerator2.MoveNext();
			if (enumerator2.Current.bricksCount != 0)
			{
				return;
			}
			foreach (global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc value in cellDescs.Values)
			{
				value.bricksCount = value.probeCount / 64;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_SerializedPerSceneCellList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.ProbeVolumeBakingSet.SerializedPerSceneCellList>();
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<int>> perSceneCellList in perSceneCellLists)
			{
				m_SerializedPerSceneCellList.Add(new global::UnityEngine.Rendering.ProbeVolumeBakingSet.SerializedPerSceneCellList
				{
					sceneGUID = perSceneCellList.Key,
					cellList = perSceneCellList.Value
				});
			}
		}

		internal void Initialize(bool useStreamingAsset)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in scenarios)
			{
				scenario.Value.Initialize(global::UnityEngine.Rendering.ProbeReferenceVolume.instance.shBands);
			}
			if (!useStreamingAsset)
			{
				m_UseStreamingAsset = false;
				m_TotalIndexList.Clear();
				foreach (int key in cellDescs.Keys)
				{
					m_TotalIndexList.Add(key);
				}
				ResolveAllCellData();
			}
			if (global::UnityEngine.Rendering.ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				BlendLightingScenario(null, 0f);
			}
		}

		internal void Cleanup()
		{
			if (cellSharedDataAsset != null)
			{
				cellSharedDataAsset.Dispose();
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in scenarios)
				{
					if (scenario.Value.IsValid())
					{
						scenario.Value.cellDataAsset.Dispose();
						scenario.Value.cellOptionalDataAsset.Dispose();
						scenario.Value.cellProbeOcclusionDataAsset.Dispose();
					}
				}
			}
			if (m_ReadCommandBuffer.IsCreated)
			{
				m_ReadCommandBuffer.Dispose();
			}
			foreach (global::Unity.Collections.NativeArray<byte> readOperationScratchBuffer in m_ReadOperationScratchBuffers)
			{
				readOperationScratchBuffer.Dispose();
			}
			m_ReadOperationScratchBuffers.Clear();
		}

		internal void SetActiveScenario(string scenario, bool verbose = true)
		{
			if (lightingScenario == scenario)
			{
				return;
			}
			if (!m_LightingScenarios.Contains(scenario))
			{
				if (verbose)
				{
					global::UnityEngine.Debug.LogError("Scenario '" + scenario + "' does not exist.");
				}
				return;
			}
			if (!scenarios.ContainsKey(scenario) && verbose)
			{
				global::UnityEngine.Debug.LogError("Scenario '" + scenario + "' has not been baked.");
			}
			lightingScenario = scenario;
			m_ScenarioBlendingFactor = 0f;
			if (global::UnityEngine.Rendering.ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.ScenarioBlendingChanged(scenarioChanged: true);
			}
			else
			{
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.UnloadAllCells();
			}
		}

		internal void BlendLightingScenario(string otherScenario, float blendingFactor)
		{
			if (!string.IsNullOrEmpty(otherScenario) && !global::UnityEngine.Rendering.ProbeReferenceVolume.instance.supportScenarioBlending)
			{
				return;
			}
			if (otherScenario != null && !m_LightingScenarios.Contains(otherScenario))
			{
				global::UnityEngine.Debug.LogError("Scenario '" + otherScenario + "' does not exist.");
				return;
			}
			if (otherScenario != null && !scenarios.ContainsKey(otherScenario))
			{
				global::UnityEngine.Debug.LogError("Scenario '" + otherScenario + "' has not been baked.");
				return;
			}
			blendingFactor = global::UnityEngine.Mathf.Clamp01(blendingFactor);
			if (otherScenario == lightingScenario || string.IsNullOrEmpty(otherScenario))
			{
				otherScenario = null;
			}
			if (otherScenario == null)
			{
				blendingFactor = 0f;
			}
			if (!(otherScenario == m_OtherScenario) || !global::UnityEngine.Mathf.Approximately(blendingFactor, m_ScenarioBlendingFactor))
			{
				bool scenarioChanged = otherScenario != m_OtherScenario;
				m_OtherScenario = otherScenario;
				m_ScenarioBlendingFactor = blendingFactor;
				global::UnityEngine.Rendering.ProbeReferenceVolume.instance.ScenarioBlendingChanged(scenarioChanged);
			}
		}

		internal int GetBakingHashCode()
		{
			return ((((maxCellPosition.GetHashCode() * 23 + minCellPosition.GetHashCode()) * 23 + globalBounds.GetHashCode()) * 23 + cellSizeInBricks.GetHashCode()) * 23 + simplificationLevels.GetHashCode()) * 23 + minDistanceBetweenProbes.GetHashCode();
		}

		private static int AlignUp16(int count)
		{
			int num = 16;
			int num2 = count % num;
			return count + ((num2 != 0) ? (num - num2) : 0);
		}

		private global::Unity.Collections.NativeArray<T> GetSubArray<T>(global::Unity.Collections.NativeArray<byte> input, int count, ref int offset) where T : struct
		{
			int num = count * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>();
			if (offset + num > input.Length)
			{
				return default(global::Unity.Collections.NativeArray<T>);
			}
			global::Unity.Collections.NativeArray<T> result = input.GetSubArray(offset, num).Reinterpret<T>(1);
			offset = AlignUp16(offset + num);
			return result;
		}

		private global::Unity.Collections.NativeArray<byte> RequestScratchBuffer(int size)
		{
			if (m_ReadOperationScratchBuffers.Count == 0)
			{
				return new global::Unity.Collections.NativeArray<byte>(size, global::Unity.Collections.Allocator.Persistent);
			}
			global::Unity.Collections.NativeArray<byte> result = m_ReadOperationScratchBuffers.Pop();
			if (result.Length < size)
			{
				result.Dispose();
				return new global::Unity.Collections.NativeArray<byte>(size, global::Unity.Collections.Allocator.Persistent);
			}
			return result;
		}

		private unsafe bool FileExists(string path)
		{
			global::Unity.IO.LowLevel.Unsafe.FileInfoResult fileInfoResult = default(global::Unity.IO.LowLevel.Unsafe.FileInfoResult);
			global::Unity.IO.LowLevel.Unsafe.AsyncReadManager.GetFileInfo(path, &fileInfoResult).JobHandle.Complete();
			return fileInfoResult.FileState == global::Unity.IO.LowLevel.Unsafe.FileState.Exists;
		}

		private unsafe global::Unity.Collections.NativeArray<T> LoadStreambleAssetData<T>(global::UnityEngine.Rendering.ProbeVolumeStreamableAsset asset, global::System.Collections.Generic.List<int> cellIndices) where T : struct
		{
			if (!m_UseStreamingAsset)
			{
				return asset.asset.GetData<byte>().Reinterpret<T>(1);
			}
			if (!FileExists(asset.GetAssetPath()))
			{
				asset.RefreshAssetPath();
				if (!FileExists(asset.GetAssetPath()))
				{
					if (asset.HasValidAssetReference())
					{
						return asset.asset.GetData<byte>().Reinterpret<T>(1);
					}
					return default(global::Unity.Collections.NativeArray<T>);
				}
			}
			if (!m_ReadCommandBuffer.IsCreated || m_ReadCommandBuffer.Length < cellIndices.Count)
			{
				if (m_ReadCommandBuffer.IsCreated)
				{
					m_ReadCommandBuffer.Dispose();
				}
				m_ReadCommandBuffer = new global::Unity.Collections.NativeArray<global::Unity.IO.LowLevel.Unsafe.ReadCommand>(cellIndices.Count, global::Unity.Collections.Allocator.Persistent);
			}
			int num = 0;
			int num2 = 0;
			foreach (int cellIndex in cellIndices)
			{
				_ = cellDescs[cellIndex];
				global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc streamableCellDesc = asset.streamableCellDescs[cellIndex];
				global::Unity.IO.LowLevel.Unsafe.ReadCommand value = new global::Unity.IO.LowLevel.Unsafe.ReadCommand
				{
					Offset = streamableCellDesc.offset,
					Size = streamableCellDesc.elementCount * asset.elementSize,
					Buffer = null
				};
				m_ReadCommandBuffer[num2++] = value;
				num += (int)value.Size;
			}
			global::Unity.Collections.NativeArray<byte> nativeArray = RequestScratchBuffer(num);
			num2 = 0;
			long num3 = 0L;
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			foreach (int cellIndex2 in cellIndices)
			{
				_ = cellIndex2;
				global::Unity.IO.LowLevel.Unsafe.ReadCommand value2 = m_ReadCommandBuffer[num2];
				value2.Buffer = unsafePtr + num3;
				num3 += value2.Size;
				m_ReadCommandBuffer[num2++] = value2;
			}
			m_ReadCommandArray.CommandCount = cellIndices.Count;
			m_ReadCommandArray.ReadCommands = (global::Unity.IO.LowLevel.Unsafe.ReadCommand*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_ReadCommandBuffer);
			global::Unity.IO.LowLevel.Unsafe.ReadHandle readHandle = global::Unity.IO.LowLevel.Unsafe.AsyncReadManager.Read(asset.OpenFile(), m_ReadCommandArray);
			readHandle.JobHandle.Complete();
			asset.CloseFile();
			readHandle.Dispose();
			return nativeArray.Reinterpret<T>(1);
		}

		private void ReleaseStreamableAssetData<T>(global::Unity.Collections.NativeArray<T> buffer) where T : struct
		{
			if (m_UseStreamingAsset)
			{
				m_ReadOperationScratchBuffers.Push(buffer.Reinterpret<byte>(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<T>()));
			}
		}

		private void PruneCellIndexList(global::System.Collections.Generic.List<int> cellIndices, global::System.Collections.Generic.List<int> prunedIndexList)
		{
			prunedIndexList.Clear();
			foreach (int cellIndex in cellIndices)
			{
				if (!cellDataMap.ContainsKey(cellIndex))
				{
					prunedIndexList.Add(cellIndex);
				}
			}
		}

		private void PruneCellIndexListForScenario(global::System.Collections.Generic.List<int> cellIndices, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo scenarioData, global::System.Collections.Generic.List<int> prunedIndexList)
		{
			prunedIndexList.Clear();
			foreach (int cellIndex in cellIndices)
			{
				if (scenarioData.cellDataAsset.streamableCellDescs.ContainsKey(cellIndex))
				{
					prunedIndexList.Add(cellIndex);
				}
			}
		}

		internal global::System.Collections.Generic.List<int> GetSceneCellIndexList(string sceneGUID)
		{
			if (perSceneCellLists.TryGetValue(sceneGUID, out var value))
			{
				return value;
			}
			return null;
		}

		private bool ResolveAllCellData()
		{
			if (ResolveSharedCellData(m_TotalIndexList))
			{
				return ResolvePerScenarioCellData(m_TotalIndexList);
			}
			return false;
		}

		internal bool ResolveCellData(global::System.Collections.Generic.List<int> cellIndices)
		{
			if (!m_UseStreamingAsset)
			{
				return true;
			}
			if (cellIndices == null)
			{
				return false;
			}
			PruneCellIndexList(cellIndices, m_PrunedIndexList);
			if (global::UnityEngine.Rendering.ProbeReferenceVolume.instance.diskStreamingEnabled)
			{
				foreach (int prunedIndex in m_PrunedIndexList)
				{
					global::UnityEngine.Rendering.ProbeReferenceVolume.CellData cellData = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellData();
					foreach (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in scenarios)
					{
						cellData.scenarios.Add(scenario.Key, default(global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData));
					}
					cellDataMap.Add(prunedIndex, cellData);
				}
				return true;
			}
			if (ResolveSharedCellData(m_PrunedIndexList))
			{
				return ResolvePerScenarioCellData(m_PrunedIndexList);
			}
			return false;
		}

		private void ResolveSharedCellData(global::System.Collections.Generic.List<int> cellIndices, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> bricksData, global::Unity.Collections.NativeArray<byte> cellSharedData, global::Unity.Collections.NativeArray<byte> cellSupportData)
		{
			global::UnityEngine.Rendering.ProbeReferenceVolume instance = global::UnityEngine.Rendering.ProbeReferenceVolume.instance;
			bool flag = cellSupportData.Length != 0;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < cellIndices.Count; i++)
			{
				int key = cellIndices[i];
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellData cellData = new global::UnityEngine.Rendering.ProbeReferenceVolume.CellData();
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc cellDesc = cellDescs[key];
				int bricksCount = cellDesc.bricksCount;
				int shChunkCount = cellDesc.shChunkCount;
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> subArray = bricksData.GetSubArray(num3, bricksCount);
				global::Unity.Collections.NativeArray<byte> subArray2 = cellSharedData.GetSubArray(num, sharedValidityMaskChunkSize * shChunkCount);
				num += sharedValidityMaskChunkSize * shChunkCount;
				cellData.bricks = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick>(subArray, global::Unity.Collections.Allocator.Persistent) : subArray);
				cellData.validityNeighMaskData = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray2, global::Unity.Collections.Allocator.Persistent) : subArray2);
				if (bakedSkyOcclusion)
				{
					if (instance.skyOcclusion)
					{
						global::Unity.Collections.NativeArray<ushort> nativeArray = cellSharedData.GetSubArray(num, sharedSkyOcclusionL0L1ChunkSize * shChunkCount).Reinterpret<ushort>(1);
						cellData.skyOcclusionDataL0L1 = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<ushort>(nativeArray, global::Unity.Collections.Allocator.Persistent) : nativeArray);
					}
					num += sharedSkyOcclusionL0L1ChunkSize * shChunkCount;
					if (bakedSkyShadingDirection)
					{
						if (instance.skyOcclusion && instance.skyOcclusionShadingDirection)
						{
							global::Unity.Collections.NativeArray<byte> subArray3 = cellSharedData.GetSubArray(num, sharedSkyShadingDirectionIndicesChunkSize * shChunkCount);
							cellData.skyShadingDirectionIndices = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray3, global::Unity.Collections.Allocator.Persistent) : subArray3);
						}
						num += sharedSkyShadingDirectionIndicesChunkSize * shChunkCount;
					}
				}
				if (flag)
				{
					global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> nativeArray2 = cellSupportData.GetSubArray(num2, shChunkCount * supportPositionChunkSize).Reinterpret<global::UnityEngine.Vector3>(1);
					num2 += shChunkCount * supportPositionChunkSize;
					cellData.probePositions = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(nativeArray2, global::Unity.Collections.Allocator.Persistent) : nativeArray2);
					global::Unity.Collections.NativeArray<float> nativeArray3 = cellSupportData.GetSubArray(num2, shChunkCount * supportValidityChunkSize).Reinterpret<float>(1);
					num2 += shChunkCount * supportValidityChunkSize;
					cellData.validity = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<float>(nativeArray3, global::Unity.Collections.Allocator.Persistent) : nativeArray3);
					global::Unity.Collections.NativeArray<float> nativeArray4 = cellSupportData.GetSubArray(num2, shChunkCount * supportTouchupChunkSize).Reinterpret<float>(1);
					num2 += shChunkCount * supportTouchupChunkSize;
					cellData.touchupVolumeInteraction = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<float>(nativeArray4, global::Unity.Collections.Allocator.Persistent) : nativeArray4);
					if (supportLayerMaskChunkSize != 0)
					{
						global::Unity.Collections.NativeArray<byte> nativeArray5 = cellSupportData.GetSubArray(num2, shChunkCount * supportLayerMaskChunkSize).Reinterpret<byte>(1);
						num2 += shChunkCount * supportLayerMaskChunkSize;
						cellData.layer = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(nativeArray5, global::Unity.Collections.Allocator.Persistent) : nativeArray5);
					}
					if (supportOffsetsChunkSize != 0)
					{
						global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> nativeArray6 = cellSupportData.GetSubArray(num2, shChunkCount * supportOffsetsChunkSize).Reinterpret<global::UnityEngine.Vector3>(1);
						num2 += shChunkCount * supportOffsetsChunkSize;
						cellData.offsetVectors = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(nativeArray6, global::Unity.Collections.Allocator.Persistent) : nativeArray6);
					}
				}
				cellDataMap.Add(key, cellData);
				num3 += bricksCount;
				num4 += shChunkCount;
			}
		}

		internal bool ResolveSharedCellData(global::System.Collections.Generic.List<int> cellIndices)
		{
			if (cellSharedDataAsset == null || !cellSharedDataAsset.IsValid())
			{
				return false;
			}
			if (!HasValidSharedData())
			{
				global::UnityEngine.Debug.LogError("One or more data file missing for baking set " + base.name + ". Cannot load shared data.");
				return false;
			}
			global::Unity.Collections.NativeArray<byte> nativeArray = LoadStreambleAssetData<byte>(cellSharedDataAsset, cellIndices);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ProbeBrickIndex.Brick> nativeArray2 = LoadStreambleAssetData<global::UnityEngine.Rendering.ProbeBrickIndex.Brick>(cellBricksDataAsset, cellIndices);
			bool num = HasSupportData();
			global::Unity.Collections.NativeArray<byte> nativeArray3 = (num ? LoadStreambleAssetData<byte>(cellSupportDataAsset, cellIndices) : default(global::Unity.Collections.NativeArray<byte>));
			ResolveSharedCellData(cellIndices, nativeArray2, nativeArray, nativeArray3);
			ReleaseStreamableAssetData(nativeArray);
			ReleaseStreamableAssetData(nativeArray2);
			if (num)
			{
				ReleaseStreamableAssetData(nativeArray3);
			}
			return true;
		}

		internal bool ResolvePerScenarioCellData(global::System.Collections.Generic.List<int> cellIndices)
		{
			bool flag = global::UnityEngine.Rendering.ProbeReferenceVolume.instance.shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo> scenario in scenarios)
			{
				string key = scenario.Key;
				global::UnityEngine.Rendering.ProbeVolumeBakingSet.PerScenarioDataInfo value = scenario.Value;
				PruneCellIndexListForScenario(cellIndices, value, m_PrunedScenarioIndexList);
				if (!value.HasValidData(global::UnityEngine.Rendering.ProbeReferenceVolume.instance.shBands))
				{
					global::UnityEngine.Debug.LogError("One or more data file missing for baking set " + key + " scenario " + lightingScenario + ". Cannot load scenario data.");
					return false;
				}
				global::Unity.Collections.NativeArray<byte> nativeArray = LoadStreambleAssetData<byte>(value.cellDataAsset, m_PrunedScenarioIndexList);
				global::Unity.Collections.NativeArray<byte> nativeArray2 = (flag ? LoadStreambleAssetData<byte>(value.cellOptionalDataAsset, m_PrunedScenarioIndexList) : default(global::Unity.Collections.NativeArray<byte>));
				global::Unity.Collections.NativeArray<byte> nativeArray3 = (bakedProbeOcclusion ? LoadStreambleAssetData<byte>(value.cellProbeOcclusionDataAsset, m_PrunedScenarioIndexList) : default(global::Unity.Collections.NativeArray<byte>));
				if (!ResolvePerScenarioCellData(nativeArray, nativeArray2, nativeArray3, key, m_PrunedScenarioIndexList))
				{
					global::UnityEngine.Debug.LogError("Baked data for scenario '" + key + "' cannot be loaded.");
					return false;
				}
				ReleaseStreamableAssetData(nativeArray);
				if (flag)
				{
					ReleaseStreamableAssetData(nativeArray2);
				}
				if (bakedProbeOcclusion)
				{
					ReleaseStreamableAssetData(nativeArray3);
				}
			}
			return true;
		}

		internal bool ResolvePerScenarioCellData(global::Unity.Collections.NativeArray<byte> cellData, global::Unity.Collections.NativeArray<byte> cellOptionalData, global::Unity.Collections.NativeArray<byte> cellProbeOcclusionData, string scenario, global::System.Collections.Generic.List<int> cellIndices)
		{
			if (!cellData.IsCreated)
			{
				return false;
			}
			bool isCreated = cellOptionalData.IsCreated;
			bool flag = cellProbeOcclusionData.IsCreated && cellProbeOcclusionData.Length > 0;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < cellIndices.Count; i++)
			{
				int key = cellIndices[i];
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellData cellData2 = cellDataMap[key];
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc cellDesc = cellDescs[key];
				global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData value = default(global::UnityEngine.Rendering.ProbeReferenceVolume.CellData.PerScenarioData);
				int shChunkCount = cellDesc.shChunkCount;
				global::Unity.Collections.NativeArray<ushort> nativeArray = cellData.GetSubArray(num, L0ChunkSize * shChunkCount).Reinterpret<ushort>(1);
				global::Unity.Collections.NativeArray<byte> subArray = cellData.GetSubArray(num + L0ChunkSize * shChunkCount, L1ChunkSize * shChunkCount);
				global::Unity.Collections.NativeArray<byte> subArray2 = cellData.GetSubArray(num + (L0ChunkSize + L1ChunkSize) * shChunkCount, L1ChunkSize * shChunkCount);
				value.shL0L1RxData = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<ushort>(nativeArray, global::Unity.Collections.Allocator.Persistent) : nativeArray);
				value.shL1GL1RyData = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray, global::Unity.Collections.Allocator.Persistent) : subArray);
				value.shL1BL1RzData = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray2, global::Unity.Collections.Allocator.Persistent) : subArray2);
				if (isCreated)
				{
					int num4 = shChunkCount * L2TextureChunkSize;
					global::Unity.Collections.NativeArray<byte> subArray3 = cellOptionalData.GetSubArray(num2, num4);
					global::Unity.Collections.NativeArray<byte> subArray4 = cellOptionalData.GetSubArray(num2 + num4, num4);
					global::Unity.Collections.NativeArray<byte> subArray5 = cellOptionalData.GetSubArray(num2 + num4 * 2, num4);
					global::Unity.Collections.NativeArray<byte> subArray6 = cellOptionalData.GetSubArray(num2 + num4 * 3, num4);
					value.shL2Data_0 = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray3, global::Unity.Collections.Allocator.Persistent) : subArray3);
					value.shL2Data_1 = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray4, global::Unity.Collections.Allocator.Persistent) : subArray4);
					value.shL2Data_2 = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray5, global::Unity.Collections.Allocator.Persistent) : subArray5);
					value.shL2Data_3 = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray6, global::Unity.Collections.Allocator.Persistent) : subArray6);
				}
				if (flag)
				{
					global::Unity.Collections.NativeArray<byte> subArray7 = cellProbeOcclusionData.GetSubArray(num3, ProbeOcclusionChunkSize * shChunkCount);
					value.probeOcclusion = (m_UseStreamingAsset ? new global::Unity.Collections.NativeArray<byte>(subArray7, global::Unity.Collections.Allocator.Persistent) : subArray7);
				}
				num += (L0ChunkSize + 2 * L1ChunkSize) * shChunkCount;
				num2 += L2TextureChunkSize * 4 * shChunkCount;
				num3 += ProbeOcclusionChunkSize * shChunkCount;
				cellData2.scenarios.Add(scenario, value);
			}
			return true;
		}

		internal void ReleaseCell(int cellIndex)
		{
			cellDataMap[cellIndex].Cleanup(cleanScenarioList: true);
			cellDataMap.Remove(cellIndex);
		}

		internal global::UnityEngine.Rendering.ProbeReferenceVolume.CellDesc GetCellDesc(int cellIndex)
		{
			if (cellDescs.TryGetValue(cellIndex, out var value))
			{
				return value;
			}
			return null;
		}

		internal global::UnityEngine.Rendering.ProbeReferenceVolume.CellData GetCellData(int cellIndex)
		{
			if (cellDataMap.TryGetValue(cellIndex, out var value))
			{
				return value;
			}
			return null;
		}

		internal int GetChunkGPUMemory(global::UnityEngine.Rendering.ProbeVolumeSHBands shBands)
		{
			int num = L0ChunkSize + 2 * L1ChunkSize + sharedDataChunkSize;
			if (shBands == global::UnityEngine.Rendering.ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				num += 4 * L2TextureChunkSize;
			}
			if (bakedProbeOcclusion)
			{
				num += ProbeOcclusionChunkSize;
			}
			return num;
		}

		internal bool HasSameSceneGUIDs(global::UnityEngine.Rendering.ProbeVolumeBakingSet other)
		{
			global::System.Collections.Generic.IReadOnlyList<string> readOnlyList = other.sceneGUIDs;
			if (m_SceneGUIDs.Count != readOnlyList.Count)
			{
				return false;
			}
			for (int i = 0; i < m_SceneGUIDs.Count; i++)
			{
				if (m_SceneGUIDs[i] != readOnlyList[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}

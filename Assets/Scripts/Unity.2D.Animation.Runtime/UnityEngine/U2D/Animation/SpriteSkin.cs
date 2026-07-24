namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.Scripting.Preserve]
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DefaultExecutionOrder(10)]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.SpriteRenderer))]
	[global::UnityEngine.AddComponentMenu("2D Animation/Sprite Skin")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.U2D.Experimental.Animation")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SpriteSkin.html")]
	public sealed class SpriteSkin : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.Common.IPreviewable, global::UnityEngine.Animations.IAnimationPreviewable, global::UnityEngine.ISerializationCallbackReceiver
	{
		internal static class Profiling
		{
			public static readonly global::Unity.Profiling.ProfilerMarker cacheCurrentSprite = new global::Unity.Profiling.ProfilerMarker("SpriteSkin.CacheCurrentSprite");

			public static readonly global::Unity.Profiling.ProfilerMarker cacheHierarchy = new global::Unity.Profiling.ProfilerMarker("SpriteSkin.CacheHierarchy");

			public static readonly global::Unity.Profiling.ProfilerMarker getSpriteBonesTransformFromGuid = new global::Unity.Profiling.ProfilerMarker("SpriteSkin.GetSpriteBoneTransformsFromGuid");

			public static readonly global::Unity.Profiling.ProfilerMarker getSpriteBonesTransformFromPath = new global::Unity.Profiling.ProfilerMarker("SpriteSkin.GetSpriteBoneTransformsFromPath");
		}

		internal struct TransformData
		{
			public string fullName;

			public global::UnityEngine.Transform transform;
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Transform m_RootBone;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Transform[] m_BoneTransforms = global::System.Array.Empty<global::UnityEngine.Transform>();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Bounds m_Bounds;

		[global::UnityEngine.SerializeField]
		private bool m_AlwaysUpdate = true;

		[global::UnityEngine.SerializeField]
		private bool m_AutoRebind;

		private global::UnityEngine.SpriteRenderer m_SpriteRenderer;

		private int m_CurrentDeformSprite;

		private int m_SpriteId;

		private bool m_IsValid;

		private global::UnityEngine.U2D.Animation.SpriteSkinState m_State;

		private bool m_ForceCpuDeformation;

		private int m_TextureId;

		private int m_TransformId;

		private global::Unity.Collections.NativeArray<int> m_BoneTransformId;

		private int m_RootBoneTransformId;

		private global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector3> m_SpriteVertices;

		private global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector4> m_SpriteTangents;

		private global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> m_SpriteBoneWeights;

		private global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Matrix4x4> m_SpriteBindPoses;

		private bool m_SpriteHasTangents;

		private int m_SpriteVertexStreamSize;

		private int m_SpriteVertexCount;

		private int m_SpriteTangentVertexOffset;

		private int m_DataIndex = -1;

		private bool m_BoneCacheUpdateToDate;

		internal global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>> hierarchyCache = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>>();

		private global::Unity.Collections.NativeArray<int> m_OutlineIndexCache;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> m_StaticOutlineVertexCache;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> m_DeformedOutlineVertexCache;

		private global::UnityEngine.Sprite m_Sprite;

		private global::UnityEngine.U2D.Animation.BaseDeformationSystem m_DeformationSystem;

		private int _outlineDependencyCount;

		private static bool g_OutlineDataIsAlwaysRequired = true;

		internal global::Unity.Collections.NativeArray<int> boneTransformId => m_BoneTransformId;

		internal int rootBoneTransformId => m_RootBoneTransformId;

		internal global::UnityEngine.U2D.Animation.DeformationMethods currentDeformationMethod { get; private set; }

		internal global::UnityEngine.U2D.Animation.BaseDeformationSystem DeformationSystem => m_DeformationSystem;

		internal bool isOutlineDataRequired
		{
			get
			{
				if (_outlineDependencyCount <= 0)
				{
					return g_OutlineDataIsAlwaysRequired;
				}
				return true;
			}
		}

		internal global::Unity.Collections.NativeArray<int> outlineIndices => m_OutlineIndexCache;

		internal unsafe global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> outlineVertices
		{
			get
			{
				if (currentDeformationMethod == global::UnityEngine.U2D.Animation.DeformationMethods.Gpu && !forceCpuDeformation)
				{
					global::Unity.Collections.NativeArray<byte> nativeArray = m_DeformationSystem?.GetDeformableBufferForSpriteSkin(this) ?? default(global::Unity.Collections.NativeArray<byte>);
					if (nativeArray == default(global::Unity.Collections.NativeArray<byte>))
					{
						return default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>);
					}
					return global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::UnityEngine.Vector3>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), nativeArray.Length / global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Vector3>(), global::Unity.Collections.Allocator.None);
				}
				return m_DeformedOutlineVertexCache;
			}
		}

		internal int vertexDeformationHash
		{
			get
			{
				if (m_DeformationSystem == null)
				{
					return 0;
				}
				return m_DeformationSystem.GetLastDeformedFrame(this);
			}
		}

		internal global::UnityEngine.Sprite sprite => m_Sprite;

		internal global::UnityEngine.SpriteRenderer spriteRenderer => m_SpriteRenderer;

		internal global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> spriteBoneWeights => m_SpriteBoneWeights;

		internal int dataIndex => m_DataIndex;

		public bool autoRebind
		{
			get
			{
				return m_AutoRebind;
			}
			set
			{
				if (m_AutoRebind != value)
				{
					m_AutoRebind = value;
					if (base.isActiveAndEnabled)
					{
						CacheHierarchy();
						m_CurrentDeformSprite = 0;
						CacheCurrentSprite(m_AutoRebind);
					}
					else
					{
						hierarchyCache.Clear();
						CacheValidFlag();
					}
				}
			}
		}

		public global::UnityEngine.Transform[] boneTransforms => m_BoneTransforms;

		public global::UnityEngine.Transform rootBone => m_RootBone;

		internal global::UnityEngine.Bounds bounds
		{
			get
			{
				return m_Bounds;
			}
			set
			{
				m_Bounds = value;
			}
		}

		public bool alwaysUpdate
		{
			get
			{
				return m_AlwaysUpdate;
			}
			set
			{
				m_AlwaysUpdate = value;
			}
		}

		public bool forceCpuDeformation
		{
			get
			{
				return m_ForceCpuDeformation;
			}
			set
			{
				if (m_ForceCpuDeformation != value)
				{
					m_ForceCpuDeformation = value;
					if (base.isActiveAndEnabled)
					{
						UpdateSpriteDeformationData();
						m_DeformationSystem?.CopyToSpriteSkinData(this);
					}
				}
			}
		}

		internal bool isValid => this.Validate() == global::UnityEngine.U2D.Animation.SpriteSkinState.Ready;

		internal void RegisterOutlineDependency()
		{
			_outlineDependencyCount++;
			g_OutlineDataIsAlwaysRequired = false;
		}

		internal void UnregisterOutlineDependency()
		{
			_outlineDependencyCount = ((_outlineDependencyCount > 0) ? (_outlineDependencyCount - 1) : 0);
		}

		internal void SetDataIndex(int index)
		{
			m_DataIndex = index;
		}

		public global::UnityEngine.U2D.Animation.SpriteSkinState SetBoneTransforms(global::UnityEngine.Transform[] boneTransformsArray)
		{
			m_BoneTransforms = boneTransformsArray;
			if (base.isActiveAndEnabled)
			{
				OnBoneTransformChanged();
			}
			else
			{
				CacheValidFlag();
			}
			return m_State;
		}

		public global::UnityEngine.U2D.Animation.SpriteSkinState SetRootBone(global::UnityEngine.Transform rootBoneTransform)
		{
			m_RootBone = rootBoneTransform;
			if (base.isActiveAndEnabled)
			{
				CacheHierarchy();
				OnBoneTransformChanged();
			}
			else
			{
				hierarchyCache.Clear();
				CacheValidFlag();
			}
			return m_State;
		}

		public bool ResetBindPose()
		{
			if (!isValid)
			{
				return false;
			}
			global::UnityEngine.U2D.SpriteBone[] bones = spriteRenderer.sprite.GetBones();
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				global::UnityEngine.Transform transform = boneTransforms[i];
				global::UnityEngine.U2D.SpriteBone spriteBone = bones[i];
				if (spriteBone.parentId != -1)
				{
					transform.localPosition = spriteBone.position;
					transform.localRotation = spriteBone.rotation;
					transform.localScale = global::UnityEngine.Vector3.one;
				}
			}
			return true;
		}

		internal void Awake()
		{
			m_SpriteRenderer = GetComponent<global::UnityEngine.SpriteRenderer>();
			m_Sprite = m_SpriteRenderer.sprite;
			m_SpriteId = ((m_Sprite != null) ? m_Sprite.GetInstanceID() : 0);
		}

		private void OnEnable()
		{
			Awake();
			m_TransformId = base.gameObject.transform.GetInstanceID();
			currentDeformationMethod = (global::UnityEngine.U2D.Animation.SpriteSkinUtility.CanSpriteSkinUseGpuDeformation(this) ? global::UnityEngine.U2D.Animation.DeformationMethods.Gpu : global::UnityEngine.U2D.Animation.DeformationMethods.Cpu);
			CacheCurrentSprite(m_AutoRebind);
			UpdateSpriteDeformationData();
			if (hierarchyCache.Count == 0)
			{
				CacheHierarchy();
			}
			RefreshBoneTransforms();
			global::UnityEngine.U2D.Animation.DeformationManager.instance.AddSpriteSkin(this);
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.instance.AddSpriteSkin(this);
			m_SpriteRenderer.RegisterSpriteChangeCallback(OnSpriteChanged);
		}

		private void OnDisable()
		{
			m_SpriteRenderer.UnregisterSpriteChangeCallback(OnSpriteChanged);
			DeactivateSkinning();
			global::UnityEngine.U2D.Animation.BufferManager.instance.ReturnBuffer(GetInstanceID());
			m_DeformationSystem?.RemoveSpriteSkin(this);
			m_DeformationSystem = null;
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.instance.RemoveSpriteSkin(this);
			ResetBoneTransformIdCache();
			DisposeOutlineCaches();
		}

		private void RefreshBoneTransforms()
		{
			global::UnityEngine.U2D.Animation.DeformationManager.instance.RemoveBoneTransforms(this);
			CacheBoneTransformIds();
			global::UnityEngine.U2D.Animation.DeformationManager.instance.AddSpriteSkinBoneTransform(this);
			CacheValidFlag();
		}

		private void OnSpriteChanged(global::UnityEngine.SpriteRenderer updatedSpriteRenderer)
		{
			m_Sprite = updatedSpriteRenderer.sprite;
			m_SpriteId = ((m_Sprite != null) ? m_Sprite.GetInstanceID() : 0);
		}

		private void CacheBoneTransformIds()
		{
			m_BoneCacheUpdateToDate = true;
			int num = 0;
			for (int i = 0; i < boneTransforms?.Length; i++)
			{
				if (boneTransforms[i] != null)
				{
					num++;
				}
			}
			if (m_BoneTransformId != default(global::Unity.Collections.NativeArray<int>) && m_BoneTransformId.IsCreated)
			{
				global::UnityEngine.U2D.Animation.NativeArrayHelpers.ResizeIfNeeded(ref m_BoneTransformId, num);
			}
			else
			{
				m_BoneTransformId = new global::Unity.Collections.NativeArray<int>(num, global::Unity.Collections.Allocator.Persistent);
			}
			m_RootBoneTransformId = ((rootBone != null) ? rootBone.GetInstanceID() : 0);
			int j = 0;
			int num2 = 0;
			for (; j < boneTransforms?.Length; j++)
			{
				if (boneTransforms[j] != null)
				{
					m_BoneTransformId[num2] = boneTransforms[j].GetInstanceID();
					num2++;
				}
			}
		}

		private void OnBoneTransformChanged()
		{
			RefreshBoneTransforms();
			m_DeformationSystem?.CopyToSpriteSkinData(this);
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.instance.BoneTransformsChanged(this);
		}

		public void OnBeforeSerialize()
		{
			OnBeforeSerializeBatch();
		}

		public void OnAfterDeserialize()
		{
			OnAfterSerializeBatch();
		}

		private void OnBeforeSerializeBatch()
		{
		}

		private void OnAfterSerializeBatch()
		{
		}

		private global::UnityEngine.U2D.Animation.SpriteSkinState CacheValidFlag()
		{
			m_State = this.Validate();
			m_IsValid = m_State == global::UnityEngine.U2D.Animation.SpriteSkinState.Ready;
			if (!m_IsValid)
			{
				DeactivateSkinning();
			}
			return m_State;
		}

		internal bool BatchValidate()
		{
			if (!m_BoneCacheUpdateToDate)
			{
				RefreshBoneTransforms();
			}
			CacheCurrentSprite(m_AutoRebind);
			bool flag = m_CurrentDeformSprite != 0;
			if (m_IsValid && flag && m_SpriteRenderer.enabled)
			{
				if (!alwaysUpdate)
				{
					return m_SpriteRenderer.isVisible;
				}
				return true;
			}
			return false;
		}

		private void Reset()
		{
			Awake();
			if (base.isActiveAndEnabled)
			{
				CacheValidFlag();
				if (!m_BoneCacheUpdateToDate)
				{
					RefreshBoneTransforms();
				}
				m_DeformationSystem?.CopyToSpriteSkinData(this);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void ResetBoneTransformIdCache()
		{
			m_BoneTransformId.DisposeIfCreated();
			m_BoneTransformId = default(global::Unity.Collections.NativeArray<int>);
			m_RootBoneTransformId = -1;
			m_BoneCacheUpdateToDate = false;
		}

		public bool HasCurrentDeformedVertices()
		{
			if (!m_IsValid)
			{
				return false;
			}
			if (m_DataIndex >= 0 && m_DeformationSystem != null)
			{
				return m_DeformationSystem.IsSpriteSkinActiveForDeformation(this);
			}
			return false;
		}

		internal global::Unity.Collections.NativeArray<byte> GetCurrentDeformedVertices()
		{
			if (!m_IsValid)
			{
				throw new global::System.InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (m_DataIndex < 0)
			{
				throw new global::System.InvalidOperationException("There are no currently deformed vertices.");
			}
			global::Unity.Collections.NativeArray<byte> obj = m_DeformationSystem?.GetDeformableBufferForSpriteSkin(this) ?? default(global::Unity.Collections.NativeArray<byte>);
			if (obj == default(global::Unity.Collections.NativeArray<byte>))
			{
				throw new global::System.InvalidOperationException("There are no currently deformed vertices.");
			}
			return obj;
		}

		internal global::Unity.Collections.NativeSlice<global::UnityEngine.U2D.Animation.PositionVertex> GetCurrentDeformedVertexPositions()
		{
			if (!m_IsValid)
			{
				throw new global::System.InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				throw new global::System.InvalidOperationException("This SpriteSkin has deformed tangents");
			}
			if (!sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Position))
			{
				throw new global::System.InvalidOperationException("This SpriteSkin does not have deformed positions.");
			}
			return global::Unity.Collections.NativeSliceExtensions.Slice(GetCurrentDeformedVertices()).SliceConvert<global::UnityEngine.U2D.Animation.PositionVertex>();
		}

		internal global::Unity.Collections.NativeSlice<global::UnityEngine.U2D.Animation.PositionTangentVertex> GetCurrentDeformedVertexPositionsAndTangents()
		{
			if (!m_IsValid)
			{
				throw new global::System.InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (!sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				throw new global::System.InvalidOperationException("This SpriteSkin does not have deformed tangents");
			}
			if (!sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Position))
			{
				throw new global::System.InvalidOperationException("This SpriteSkin does not have deformed positions.");
			}
			return global::Unity.Collections.NativeSliceExtensions.Slice(GetCurrentDeformedVertices()).SliceConvert<global::UnityEngine.U2D.Animation.PositionTangentVertex>();
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector3> GetDeformedVertexPositionData()
		{
			if (!m_IsValid)
			{
				throw new global::System.InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (!sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Position))
			{
				throw new global::System.InvalidOperationException("Sprite does not have vertex position data.");
			}
			return new global::UnityEngine.U2D.Animation.NativeCustomSliceEnumerator<global::UnityEngine.Vector3>(global::Unity.Collections.NativeSliceExtensions.Slice(GetCurrentDeformedVertices(), sprite.GetVertexStreamOffset(global::UnityEngine.Rendering.VertexAttribute.Position)), m_SpriteVertexCount, m_SpriteVertexStreamSize);
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Vector4> GetDeformedVertexTangentData()
		{
			if (!m_IsValid)
			{
				throw new global::System.InvalidOperationException("The SpriteSkin deformation is not valid.");
			}
			if (!sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				throw new global::System.InvalidOperationException("Sprite does not have vertex tangent data.");
			}
			return new global::UnityEngine.U2D.Animation.NativeCustomSliceEnumerator<global::UnityEngine.Vector4>(global::Unity.Collections.NativeSliceExtensions.Slice(GetCurrentDeformedVertices(), sprite.GetVertexStreamOffset(global::UnityEngine.Rendering.VertexAttribute.Tangent)), m_SpriteVertexCount, m_SpriteVertexStreamSize);
		}

		private void DisposeOutlineCaches()
		{
			m_OutlineIndexCache.DisposeIfCreated();
			m_StaticOutlineVertexCache.DisposeIfCreated();
			m_DeformedOutlineVertexCache.DisposeIfCreated();
			m_OutlineIndexCache = default(global::Unity.Collections.NativeArray<int>);
			m_StaticOutlineVertexCache = default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>);
			m_DeformedOutlineVertexCache = default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>);
		}

		public void OnPreviewUpdate()
		{
		}

		internal void PostDeform()
		{
			if ((currentDeformationMethod == global::UnityEngine.U2D.Animation.DeformationMethods.Cpu || forceCpuDeformation) && isOutlineDataRequired)
			{
				UpdateDeformedOutlineCache();
			}
		}

		private void CacheCurrentSprite(bool rebind)
		{
			if (m_CurrentDeformSprite == m_SpriteId)
			{
				return;
			}
			using (global::UnityEngine.U2D.Animation.SpriteSkin.Profiling.cacheCurrentSprite.Auto())
			{
				DeactivateSkinning();
				m_CurrentDeformSprite = m_SpriteId;
				if (rebind && m_CurrentDeformSprite != 0 && rootBone != null)
				{
					if (!global::UnityEngine.U2D.Animation.SpriteSkinHelpers.GetSpriteBonesTransforms(this, out var outTransform))
					{
						global::UnityEngine.Debug.LogWarning("Rebind failed for " + base.name + ". Could not find all bones required by the Sprite: " + sprite.name + ".");
					}
					SetBoneTransforms(outTransform);
				}
				UpdateSpriteDeformationData();
				m_DeformationSystem?.CopyToSpriteSkinData(this);
				CacheValidFlag();
			}
		}

		private void UpdateSpriteDeformationData()
		{
			CacheSpriteOutline();
			if (sprite == null)
			{
				m_TextureId = 0;
				m_SpriteVertices = global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector3>.Default();
				m_SpriteTangents = global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector4>.Default();
				m_SpriteBoneWeights = global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight>.Default();
				m_SpriteBindPoses = global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Matrix4x4>.Default();
				m_SpriteHasTangents = false;
				m_SpriteVertexStreamSize = 0;
				m_SpriteVertexCount = 0;
				m_SpriteTangentVertexOffset = 0;
				return;
			}
			m_TextureId = ((sprite.texture != null) ? sprite.texture.GetInstanceID() : 0);
			if (currentDeformationMethod == global::UnityEngine.U2D.Animation.DeformationMethods.Cpu || forceCpuDeformation)
			{
				m_SpriteVertices = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector3>(sprite.GetVertexAttribute<global::UnityEngine.Vector3>(global::UnityEngine.Rendering.VertexAttribute.Position));
				m_SpriteVertexCount = sprite.GetVertexCount();
				m_SpriteVertexStreamSize = sprite.GetVertexStreamSize();
				m_SpriteTangents = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector4>(sprite.GetVertexAttribute<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.VertexAttribute.Tangent));
				m_SpriteHasTangents = sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent);
				m_SpriteTangentVertexOffset = sprite.GetVertexStreamOffset(global::UnityEngine.Rendering.VertexAttribute.Tangent);
			}
			else
			{
				m_SpriteVertices = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector3>(m_StaticOutlineVertexCache);
				m_SpriteVertexCount = m_SpriteVertices.length;
				m_SpriteVertexStreamSize = 12;
				m_SpriteTangents = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Vector4>(sprite.GetVertexAttribute<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.VertexAttribute.Tangent));
				m_SpriteHasTangents = false;
				m_SpriteTangentVertexOffset = 0;
			}
			m_SpriteBoneWeights = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight>(sprite.GetVertexAttribute<global::UnityEngine.BoneWeight>(global::UnityEngine.Rendering.VertexAttribute.BlendWeight));
			m_SpriteBindPoses = new global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.Matrix4x4>(sprite.GetBindPoses());
		}

		private void UpdateDeformedOutlineCache()
		{
			if (!(sprite == null) && m_OutlineIndexCache.IsCreated && m_DeformedOutlineVertexCache.IsCreated && HasCurrentDeformedVertices())
			{
				global::Unity.Collections.NativeArray<byte> buffer = GetCurrentDeformedVertices();
				global::Unity.Collections.NativeArray<int> indices = m_OutlineIndexCache;
				global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.SetVertexPositionFromByteBuffer(in buffer, in indices, ref m_DeformedOutlineVertexCache, m_SpriteVertexStreamSize);
			}
		}

		private void CacheSpriteOutline()
		{
			DisposeOutlineCaches();
			if (!(sprite == null))
			{
				CacheOutlineIndices(out var maxIndex);
				int cacheSize = maxIndex + 1;
				CacheOutlineVertices(cacheSize);
			}
		}

		private void CacheOutlineIndices(out int maxIndex)
		{
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outlineEdges = global::UnityEngine.U2D.Animation.MeshUtilities.GetOutlineEdges(sprite.GetIndices());
			m_OutlineIndexCache = new global::Unity.Collections.NativeArray<int>(outlineEdges.Length * 2, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			maxIndex = 0;
			for (int i = 0; i < outlineEdges.Length; i++)
			{
				int x = outlineEdges[i].x;
				int y = outlineEdges[i].y;
				m_OutlineIndexCache[i * 2] = x;
				m_OutlineIndexCache[i * 2 + 1] = y;
				if (x > maxIndex)
				{
					maxIndex = x;
				}
				if (y > maxIndex)
				{
					maxIndex = y;
				}
			}
			outlineEdges.Dispose();
		}

		private void CacheOutlineVertices(int cacheSize)
		{
			m_StaticOutlineVertexCache = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(cacheSize, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertexAttribute = sprite.GetVertexAttribute<global::UnityEngine.Vector3>(global::UnityEngine.Rendering.VertexAttribute.Position);
			for (int i = 0; i < m_OutlineIndexCache.Length; i++)
			{
				int index = m_OutlineIndexCache[i];
				m_StaticOutlineVertexCache[index] = vertexAttribute[index];
			}
			if (currentDeformationMethod == global::UnityEngine.U2D.Animation.DeformationMethods.Cpu || forceCpuDeformation)
			{
				m_DeformedOutlineVertexCache = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(cacheSize, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			}
		}

		internal void CopyToSpriteSkinData(ref global::UnityEngine.U2D.Animation.SpriteSkinData data)
		{
			if (!m_BoneCacheUpdateToDate)
			{
				RefreshBoneTransforms();
			}
			CacheCurrentSprite(m_AutoRebind);
			data.vertices = m_SpriteVertices;
			data.boneWeights = m_SpriteBoneWeights;
			data.bindPoses = m_SpriteBindPoses;
			data.tangents = m_SpriteTangents;
			data.hasTangents = m_SpriteHasTangents;
			data.spriteVertexStreamSize = m_SpriteVertexStreamSize;
			data.spriteVertexCount = m_SpriteVertexCount;
			data.tangentVertexOffset = m_SpriteTangentVertexOffset;
			data.transformId = m_TransformId;
			data.boneTransformId = new global::UnityEngine.U2D.Animation.NativeCustomSlice<int>(m_BoneTransformId);
			data.deformVerticesStartPos = -1;
		}

		internal bool NeedToUpdateDeformationCache()
		{
			bool num = ((sprite.texture != null) ? sprite.texture.GetInstanceID() : 0) != m_TextureId;
			if (num)
			{
				UpdateSpriteDeformationData();
				global::UnityEngine.U2D.Animation.BaseDeformationSystem deformationSystem = m_DeformationSystem;
				if (deformationSystem == null)
				{
					return num;
				}
				deformationSystem.CopyToSpriteSkinData(this);
			}
			return num;
		}

		internal void CacheHierarchy(bool forceCreateCache = false)
		{
			using (global::UnityEngine.U2D.Animation.SpriteSkin.Profiling.cacheHierarchy.Auto())
			{
				hierarchyCache.Clear();
				if (rootBone == null || (!m_AutoRebind && !forceCreateCache))
				{
					return;
				}
				int num = CountChildren(rootBone);
				hierarchyCache.EnsureCapacity(num + 1);
				global::UnityEngine.U2D.Animation.SpriteSkinHelpers.CacheChildren(rootBone, hierarchyCache);
				foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>> item in hierarchyCache)
				{
					if (item.Value.Count != 1)
					{
						int count = item.Value.Count;
						for (int i = 0; i < count; i++)
						{
							global::UnityEngine.U2D.Animation.SpriteSkin.TransformData value = item.Value[i];
							value.fullName = global::UnityEngine.U2D.Animation.SpriteSkinHelpers.GenerateTransformPath(rootBone, value.transform);
							item.Value[i] = value;
						}
					}
				}
			}
		}

		internal void DeactivateSkinning()
		{
			if (m_SpriteRenderer != null)
			{
				global::UnityEngine.Sprite sprite = this.sprite;
				if (sprite != null)
				{
					global::UnityEngine.U2D.Common.InternalEngineBridge.SetLocalAABB(m_SpriteRenderer, sprite.bounds);
				}
				m_SpriteRenderer.DeactivateDeformableBuffer();
			}
		}

		internal void ResetSprite()
		{
			m_CurrentDeformSprite = 0;
			CacheValidFlag();
		}

		internal void SetDeformationSystem(global::UnityEngine.U2D.Animation.BaseDeformationSystem newDeformationSystem)
		{
			m_DeformationSystem = newDeformationSystem;
			currentDeformationMethod = m_DeformationSystem.deformationMethod;
		}

		private static int CountChildren(global::UnityEngine.Transform transform)
		{
			int childCount = transform.childCount;
			int num = childCount;
			for (int i = 0; i < childCount; i++)
			{
				num += CountChildren(transform.GetChild(i));
			}
			return num;
		}
	}
}

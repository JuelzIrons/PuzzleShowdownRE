namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.TrackClipType(typeof(global::UnityEngine.Timeline.AnimationPlayableAsset), false)]
	[global::UnityEngine.Timeline.TrackBindingType(typeof(global::UnityEngine.Animator))]
	[global::UnityEngine.ExcludeFromPreset]
	public class AnimationTrack : global::UnityEngine.Timeline.TrackAsset, global::UnityEngine.Timeline.ILayerable
	{
		private static class AnimationTrackUpgrade
		{
			public static void ConvertRotationsToEuler(global::UnityEngine.Timeline.AnimationTrack track)
			{
				track.m_EulerAngles = track.m_Rotation.eulerAngles;
				track.m_InfiniteClipOffsetEulerAngles = track.m_OpenClipOffsetRotation.eulerAngles;
			}

			public static void ConvertRootMotion(global::UnityEngine.Timeline.AnimationTrack track)
			{
				track.m_TrackOffset = global::UnityEngine.Timeline.TrackOffset.Auto;
				if (!track.m_ApplyOffsets)
				{
					track.m_Position = global::UnityEngine.Vector3.zero;
					track.m_EulerAngles = global::UnityEngine.Vector3.zero;
				}
			}

			public static void ConvertInfiniteTrack(global::UnityEngine.Timeline.AnimationTrack track)
			{
				track.m_InfiniteClip = track.m_AnimClip;
				track.m_AnimClip = null;
			}
		}

		private const string k_DefaultInfiniteClipName = "Recorded";

		private const string k_DefaultRecordableClipName = "Recorded";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipPreExtrapolation")]
		private global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_InfiniteClipPreExtrapolation;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipPostExtrapolation")]
		private global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation m_InfiniteClipPostExtrapolation;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipOffsetPosition")]
		private global::UnityEngine.Vector3 m_InfiniteClipOffsetPosition = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipOffsetEulerAngles")]
		private global::UnityEngine.Vector3 m_InfiniteClipOffsetEulerAngles = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipTimeOffset")]
		private double m_InfiniteClipTimeOffset;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_OpenClipRemoveOffset")]
		private bool m_InfiniteClipRemoveOffset;

		[global::UnityEngine.SerializeField]
		private bool m_InfiniteClipApplyFootIK = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode mInfiniteClipLoop;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.MatchTargetFields m_MatchTargetFields = global::UnityEngine.Timeline.MatchTargetFieldConstants.All;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_Position = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_EulerAngles = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AvatarMask m_AvatarMask;

		[global::UnityEngine.SerializeField]
		private bool m_ApplyAvatarMask = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TrackOffset m_TrackOffset;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.AnimationClip m_InfiniteClip;

		private static readonly global::System.Collections.Generic.Queue<global::UnityEngine.Transform> s_CachedQueue = new global::System.Collections.Generic.Queue<global::UnityEngine.Transform>(100);

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Use m_InfiniteClipOffsetEulerAngles Instead", false)]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Quaternion m_OpenClipOffsetRotation = global::UnityEngine.Quaternion.identity;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Use m_RotationEuler Instead", false)]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Quaternion m_Rotation = global::UnityEngine.Quaternion.identity;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Use m_RootTransformOffsetMode", false)]
		[global::UnityEngine.HideInInspector]
		private bool m_ApplyOffsets;

		public global::UnityEngine.Vector3 position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public global::UnityEngine.Quaternion rotation
		{
			get
			{
				return global::UnityEngine.Quaternion.Euler(m_EulerAngles);
			}
			set
			{
				m_EulerAngles = value.eulerAngles;
			}
		}

		public global::UnityEngine.Vector3 eulerAngles
		{
			get
			{
				return m_EulerAngles;
			}
			set
			{
				m_EulerAngles = value;
			}
		}

		[global::System.Obsolete("applyOffset is deprecated. Use trackOffset instead", true)]
		public bool applyOffsets
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public global::UnityEngine.Timeline.TrackOffset trackOffset
		{
			get
			{
				return m_TrackOffset;
			}
			set
			{
				m_TrackOffset = value;
			}
		}

		public global::UnityEngine.Timeline.MatchTargetFields matchTargetFields
		{
			get
			{
				return m_MatchTargetFields;
			}
			set
			{
				m_MatchTargetFields = value & global::UnityEngine.Timeline.MatchTargetFieldConstants.All;
			}
		}

		public global::UnityEngine.AnimationClip infiniteClip
		{
			get
			{
				return m_InfiniteClip;
			}
			internal set
			{
				m_InfiniteClip = value;
			}
		}

		internal bool infiniteClipRemoveOffset
		{
			get
			{
				return m_InfiniteClipRemoveOffset;
			}
			set
			{
				m_InfiniteClipRemoveOffset = value;
			}
		}

		public global::UnityEngine.AvatarMask avatarMask
		{
			get
			{
				return m_AvatarMask;
			}
			set
			{
				m_AvatarMask = value;
			}
		}

		public bool applyAvatarMask
		{
			get
			{
				return m_ApplyAvatarMask;
			}
			set
			{
				m_ApplyAvatarMask = value;
			}
		}

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				yield return global::UnityEngine.Animations.AnimationPlayableBinding.Create(base.name, this);
			}
		}

		public bool inClipMode
		{
			get
			{
				if (base.clips != null)
				{
					return base.clips.Length != 0;
				}
				return false;
			}
		}

		public global::UnityEngine.Vector3 infiniteClipOffsetPosition
		{
			get
			{
				return m_InfiniteClipOffsetPosition;
			}
			set
			{
				m_InfiniteClipOffsetPosition = value;
			}
		}

		public global::UnityEngine.Quaternion infiniteClipOffsetRotation
		{
			get
			{
				return global::UnityEngine.Quaternion.Euler(m_InfiniteClipOffsetEulerAngles);
			}
			set
			{
				m_InfiniteClipOffsetEulerAngles = value.eulerAngles;
			}
		}

		public global::UnityEngine.Vector3 infiniteClipOffsetEulerAngles
		{
			get
			{
				return m_InfiniteClipOffsetEulerAngles;
			}
			set
			{
				m_InfiniteClipOffsetEulerAngles = value;
			}
		}

		internal bool infiniteClipApplyFootIK
		{
			get
			{
				return m_InfiniteClipApplyFootIK;
			}
			set
			{
				m_InfiniteClipApplyFootIK = value;
			}
		}

		internal double infiniteClipTimeOffset
		{
			get
			{
				return m_InfiniteClipTimeOffset;
			}
			set
			{
				m_InfiniteClipTimeOffset = value;
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation infiniteClipPreExtrapolation
		{
			get
			{
				return m_InfiniteClipPreExtrapolation;
			}
			set
			{
				m_InfiniteClipPreExtrapolation = value;
			}
		}

		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation infiniteClipPostExtrapolation
		{
			get
			{
				return m_InfiniteClipPostExtrapolation;
			}
			set
			{
				m_InfiniteClipPostExtrapolation = value;
			}
		}

		internal global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode infiniteClipLoop
		{
			get
			{
				return mInfiniteClipLoop;
			}
			set
			{
				mInfiniteClipLoop = value;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("openClipOffsetPosition has been deprecated. Use infiniteClipOffsetPosition instead. (UnityUpgradable) -> infiniteClipOffsetPosition", true)]
		public global::UnityEngine.Vector3 openClipOffsetPosition
		{
			get
			{
				return infiniteClipOffsetPosition;
			}
			set
			{
				infiniteClipOffsetPosition = value;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("openClipOffsetRotation has been deprecated. Use infiniteClipOffsetRotation instead. (UnityUpgradable) -> infiniteClipOffsetRotation", true)]
		public global::UnityEngine.Quaternion openClipOffsetRotation
		{
			get
			{
				return infiniteClipOffsetRotation;
			}
			set
			{
				infiniteClipOffsetRotation = value;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("openClipOffsetEulerAngles has been deprecated. Use infiniteClipOffsetEulerAngles instead. (UnityUpgradable) -> infiniteClipOffsetEulerAngles", true)]
		public global::UnityEngine.Vector3 openClipOffsetEulerAngles
		{
			get
			{
				return infiniteClipOffsetEulerAngles;
			}
			set
			{
				infiniteClipOffsetEulerAngles = value;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("openClipPreExtrapolation has been deprecated. Use infiniteClipPreExtrapolation instead. (UnityUpgradable) -> infiniteClipPreExtrapolation", true)]
		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation openClipPreExtrapolation
		{
			get
			{
				return infiniteClipPreExtrapolation;
			}
			set
			{
				infiniteClipPreExtrapolation = value;
			}
		}

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("openClipPostExtrapolation has been deprecated. Use infiniteClipPostExtrapolation instead. (UnityUpgradable) -> infiniteClipPostExtrapolation", true)]
		public global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation openClipPostExtrapolation
		{
			get
			{
				return infiniteClipPostExtrapolation;
			}
			set
			{
				infiniteClipPostExtrapolation = value;
			}
		}

		internal override bool CanCompileClips()
		{
			if (!base.muted)
			{
				if (m_Clips.Count <= 0)
				{
					if (m_InfiniteClip != null)
					{
						return !m_InfiniteClip.empty;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		[global::UnityEngine.ContextMenu("Reset Offsets")]
		private void ResetOffsets()
		{
			m_Position = global::UnityEngine.Vector3.zero;
			m_EulerAngles = global::UnityEngine.Vector3.zero;
			UpdateClipOffsets();
		}

		public global::UnityEngine.Timeline.TimelineClip CreateClip(global::UnityEngine.AnimationClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			global::UnityEngine.Timeline.TimelineClip timelineClip = CreateClip<global::UnityEngine.Timeline.AnimationPlayableAsset>();
			AssignAnimationClip(timelineClip, clip);
			return timelineClip;
		}

		public void CreateInfiniteClip(string infiniteClipName)
		{
			if (inClipMode)
			{
				global::UnityEngine.Debug.LogWarning("CreateInfiniteClip cannot create an infinite clip for an AnimationTrack that contains one or more Timeline Clips.");
			}
			else if (!(m_InfiniteClip != null))
			{
				m_InfiniteClip = global::UnityEngine.Timeline.TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(infiniteClipName) ? "Recorded" : infiniteClipName, this, isLegacy: false);
			}
		}

		public global::UnityEngine.Timeline.TimelineClip CreateRecordableClip(string animClipName)
		{
			global::UnityEngine.AnimationClip clip = global::UnityEngine.Timeline.TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(animClipName) ? "Recorded" : animClipName, this, isLegacy: false);
			global::UnityEngine.Timeline.TimelineClip timelineClip = CreateClip(clip);
			timelineClip.displayName = animClipName;
			timelineClip.recordable = true;
			timelineClip.start = 0.0;
			timelineClip.duration = 1.0;
			global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = timelineClip.asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
			if (animationPlayableAsset != null)
			{
				animationPlayableAsset.removeStartOffset = false;
			}
			return timelineClip;
		}

		protected override void OnCreateClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation clipExtrapolation = global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.None;
			if (!base.isSubTrack)
			{
				clipExtrapolation = global::UnityEngine.Timeline.TimelineClip.ClipExtrapolation.Hold;
			}
			clip.preExtrapolationMode = clipExtrapolation;
			clip.postExtrapolationMode = clipExtrapolation;
		}

		protected internal override int CalculateItemsHash()
		{
			return global::UnityEngine.Timeline.TrackAsset.GetAnimationClipHash(m_InfiniteClip).CombineHash(base.CalculateItemsHash());
		}

		internal void UpdateClipOffsets()
		{
		}

		private global::UnityEngine.Playables.Playable CompileTrackPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Timeline.AnimationTrack track, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree, global::UnityEngine.Timeline.AppliedOffsetMode mode)
		{
			global::UnityEngine.Animations.AnimationMixerPlayable animationMixerPlayable = global::UnityEngine.Animations.AnimationMixerPlayable.Create(graph, track.clips.Length);
			for (int i = 0; i < track.clips.Length; i++)
			{
				global::UnityEngine.Timeline.TimelineClip timelineClip = track.clips[i];
				global::UnityEngine.Playables.PlayableAsset playableAsset = timelineClip.asset as global::UnityEngine.Playables.PlayableAsset;
				if (!(playableAsset == null))
				{
					global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = playableAsset as global::UnityEngine.Timeline.AnimationPlayableAsset;
					if (animationPlayableAsset != null)
					{
						animationPlayableAsset.appliedOffsetMode = mode;
					}
					global::UnityEngine.Playables.Playable playable = playableAsset.CreatePlayable(graph, go);
					if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
					{
						global::UnityEngine.Timeline.RuntimeClip item = new global::UnityEngine.Timeline.RuntimeClip(timelineClip, playable, animationMixerPlayable);
						tree.Add(item);
						graph.Connect(playable, 0, animationMixerPlayable, i);
						global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationMixerPlayable, i, 0f);
					}
				}
			}
			if (!track.AnimatesRootTransform())
			{
				return animationMixerPlayable;
			}
			return ApplyTrackOffset(graph, animationMixerPlayable, go, mode);
		}

		global::UnityEngine.Playables.Playable global::UnityEngine.Timeline.ILayerable.CreateLayerMixer(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, int inputCount)
		{
			return global::UnityEngine.Playables.Playable.Null;
		}

		internal override global::UnityEngine.Playables.Playable CreateMixerPlayableGraph(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree)
		{
			if (base.isSubTrack)
			{
				throw new global::System.InvalidOperationException("Nested animation tracks should never be asked to create a graph directly");
			}
			global::System.Collections.Generic.List<global::UnityEngine.Timeline.AnimationTrack> list = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.AnimationTrack>();
			if (CanCompileClips())
			{
				list.Add(this);
			}
			global::UnityEngine.Transform genericRootNode = GetGenericRootNode(go);
			bool flag = AnimatesRootTransform();
			bool flag2 = flag && !IsRootTransformDisabledByMask(go, genericRootNode);
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				global::UnityEngine.Timeline.AnimationTrack animationTrack = childTrack as global::UnityEngine.Timeline.AnimationTrack;
				if (animationTrack != null && animationTrack.CanCompileClips())
				{
					bool flag3 = animationTrack.AnimatesRootTransform();
					flag |= animationTrack.AnimatesRootTransform();
					flag2 |= flag3 && !animationTrack.IsRootTransformDisabledByMask(go, genericRootNode);
					list.Add(animationTrack);
				}
			}
			global::UnityEngine.Timeline.AppliedOffsetMode offsetMode = GetOffsetMode(go, flag2);
			int defaultBlendCount = GetDefaultBlendCount();
			global::UnityEngine.Animations.AnimationLayerMixerPlayable animationLayerMixerPlayable = CreateGroupMixer(graph, go, list.Count + defaultBlendCount);
			for (int i = 0; i < list.Count; i++)
			{
				int num = i + defaultBlendCount;
				global::UnityEngine.Timeline.AppliedOffsetMode mode = offsetMode;
				if (offsetMode != global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform && list[i].IsRootTransformDisabledByMask(go, genericRootNode))
				{
					mode = global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform;
				}
				global::UnityEngine.Playables.Playable source = (list[i].inClipMode ? CompileTrackPlayable(graph, list[i], go, tree, mode) : list[i].CreateInfiniteTrackPlayable(graph, go, tree, mode));
				graph.Connect(source, 0, animationLayerMixerPlayable, num);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationLayerMixerPlayable, num, (!list[i].inClipMode) ? 1 : 0);
				if (list[i].applyAvatarMask && list[i].avatarMask != null)
				{
					animationLayerMixerPlayable.SetLayerMaskFromAvatarMask((uint)num, list[i].avatarMask);
				}
			}
			bool flag4 = RequiresMotionXPlayable(offsetMode, go);
			flag4 |= defaultBlendCount > 0 && RequiresMotionXPlayable(GetOffsetMode(go, flag), go);
			AttachDefaultBlend(graph, animationLayerMixerPlayable, flag4);
			global::UnityEngine.Playables.Playable playable = animationLayerMixerPlayable;
			if (flag4)
			{
				global::UnityEngine.Animations.AnimationMotionXToDeltaPlayable animationMotionXToDeltaPlayable = global::UnityEngine.Animations.AnimationMotionXToDeltaPlayable.Create(graph);
				graph.Connect(playable, 0, animationMotionXToDeltaPlayable, 0);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationMotionXToDeltaPlayable, 0, 1f);
				animationMotionXToDeltaPlayable.SetAbsoluteMotion(UsesAbsoluteMotion(offsetMode));
				playable = animationMotionXToDeltaPlayable;
			}
			return playable;
		}

		private int GetDefaultBlendCount()
		{
			return 0;
		}

		private void AttachDefaultBlend(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Animations.AnimationLayerMixerPlayable mixer, bool requireOffset)
		{
		}

		private global::UnityEngine.Playables.Playable AttachOffsetPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable playable, global::UnityEngine.Vector3 pos, global::UnityEngine.Quaternion rot)
		{
			global::UnityEngine.Animations.AnimationOffsetPlayable animationOffsetPlayable = global::UnityEngine.Animations.AnimationOffsetPlayable.Create(graph, pos, rot, 1);
			global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationOffsetPlayable, 0, 1f);
			graph.Connect(playable, 0, animationOffsetPlayable, 0);
			return animationOffsetPlayable;
		}

		private bool RequiresMotionXPlayable(global::UnityEngine.Timeline.AppliedOffsetMode mode, global::UnityEngine.GameObject gameObject)
		{
			switch (mode)
			{
			case global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform:
				return false;
			case global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy:
			{
				global::UnityEngine.Animator binding = GetBinding((gameObject != null) ? gameObject.GetComponent<global::UnityEngine.Playables.PlayableDirector>() : null);
				if (binding != null)
				{
					return binding.hasRootMotion;
				}
				return false;
			}
			default:
				return true;
			}
		}

		private static bool UsesAbsoluteMotion(global::UnityEngine.Timeline.AppliedOffsetMode mode)
		{
			if (mode != global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffset)
			{
				return mode != global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy;
			}
			return false;
		}

		private bool HasController(global::UnityEngine.GameObject gameObject)
		{
			global::UnityEngine.Animator binding = GetBinding((gameObject != null) ? gameObject.GetComponent<global::UnityEngine.Playables.PlayableDirector>() : null);
			if (binding != null)
			{
				return binding.runtimeAnimatorController != null;
			}
			return false;
		}

		internal global::UnityEngine.Animator GetBinding(global::UnityEngine.Playables.PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			global::UnityEngine.Object key = this;
			if (base.isSubTrack)
			{
				key = base.parent;
			}
			global::UnityEngine.Object obj = null;
			if (director != null)
			{
				obj = director.GetGenericBinding(key);
			}
			global::UnityEngine.Animator animator = null;
			if (obj != null)
			{
				animator = obj as global::UnityEngine.Animator;
				global::UnityEngine.GameObject gameObject = obj as global::UnityEngine.GameObject;
				if (animator == null && gameObject != null)
				{
					animator = gameObject.GetComponent<global::UnityEngine.Animator>();
				}
			}
			return animator;
		}

		private static global::UnityEngine.Animations.AnimationLayerMixerPlayable CreateGroupMixer(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, int inputCount)
		{
			return global::UnityEngine.Animations.AnimationLayerMixerPlayable.Create(graph, inputCount, singleLayerOptimization: false);
		}

		private global::UnityEngine.Playables.Playable CreateInfiniteTrackPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree, global::UnityEngine.Timeline.AppliedOffsetMode mode)
		{
			if (m_InfiniteClip == null)
			{
				return global::UnityEngine.Playables.Playable.Null;
			}
			global::UnityEngine.Animations.AnimationMixerPlayable animationMixerPlayable = global::UnityEngine.Animations.AnimationMixerPlayable.Create(graph, 1);
			global::UnityEngine.Playables.Playable playable = global::UnityEngine.Timeline.AnimationPlayableAsset.CreatePlayable(graph, m_InfiniteClip, m_InfiniteClipOffsetPosition, m_InfiniteClipOffsetEulerAngles, removeStartOffset: false, mode, infiniteClipApplyFootIK, global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode.Off);
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
			{
				tree.Add(new global::UnityEngine.Timeline.InfiniteRuntimeClip(playable));
				graph.Connect(playable, 0, animationMixerPlayable, 0);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationMixerPlayable, 0, 1f);
			}
			if (!AnimatesRootTransform())
			{
				return animationMixerPlayable;
			}
			return (base.isSubTrack ? ((global::UnityEngine.Timeline.AnimationTrack)base.parent) : this).ApplyTrackOffset(graph, animationMixerPlayable, go, mode);
		}

		private global::UnityEngine.Playables.Playable ApplyTrackOffset(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable root, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.AppliedOffsetMode mode)
		{
			if (mode == global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy || mode == global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffset || mode == global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform)
			{
				return root;
			}
			global::UnityEngine.Vector3 vector = position;
			global::UnityEngine.Quaternion quaternion = rotation;
			global::UnityEngine.Animations.AnimationOffsetPlayable animationOffsetPlayable = global::UnityEngine.Animations.AnimationOffsetPlayable.Create(graph, vector, quaternion, 1);
			graph.Connect(root, 0, animationOffsetPlayable, 0);
			global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationOffsetPlayable, 0, 1f);
			return animationOffsetPlayable;
		}

		internal override void GetEvaluationTime(out double outStart, out double outDuration)
		{
			if (inClipMode)
			{
				base.GetEvaluationTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = global::UnityEngine.Timeline.TimelineClip.kMaxTimeValue;
		}

		internal override void GetSequenceTime(out double outStart, out double outDuration)
		{
			if (inClipMode)
			{
				base.GetSequenceTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = global::System.Math.Max(GetNotificationDuration(), global::UnityEngine.Timeline.TimeUtility.GetAnimationClipLength(m_InfiniteClip));
		}

		private void AssignAnimationClip(global::UnityEngine.Timeline.TimelineClip clip, global::UnityEngine.AnimationClip animClip)
		{
			if (clip == null || animClip == null)
			{
				return;
			}
			if (animClip.legacy)
			{
				throw new global::System.InvalidOperationException("Legacy Animation Clips are not supported");
			}
			global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = clip.asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
			if (animationPlayableAsset != null)
			{
				animationPlayableAsset.clip = animClip;
				animationPlayableAsset.name = animClip.name;
				double num = animationPlayableAsset.duration;
				if (!double.IsInfinity(num) && num >= global::UnityEngine.Timeline.TimelineClip.kMinDuration && num < global::UnityEngine.Timeline.TimelineClip.kMaxTimeValue)
				{
					clip.duration = num;
				}
			}
			clip.displayName = animClip.name;
		}

		public override void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
		}

		private void GetAnimationClips(global::System.Collections.Generic.List<global::UnityEngine.AnimationClip> animClips)
		{
			global::UnityEngine.Timeline.TimelineClip[] array = base.clips;
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = array[i].asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
				if (animationPlayableAsset != null && animationPlayableAsset.clip != null)
				{
					animClips.Add(animationPlayableAsset.clip);
				}
			}
			if (m_InfiniteClip != null)
			{
				animClips.Add(m_InfiniteClip);
			}
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				global::UnityEngine.Timeline.AnimationTrack animationTrack = childTrack as global::UnityEngine.Timeline.AnimationTrack;
				if (animationTrack != null)
				{
					animationTrack.GetAnimationClips(animClips);
				}
			}
		}

		private global::UnityEngine.Timeline.AppliedOffsetMode GetOffsetMode(global::UnityEngine.GameObject go, bool animatesRootTransform)
		{
			if (!animatesRootTransform)
			{
				return global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform;
			}
			if (m_TrackOffset == global::UnityEngine.Timeline.TrackOffset.ApplyTransformOffsets)
			{
				return global::UnityEngine.Timeline.AppliedOffsetMode.TransformOffset;
			}
			if (m_TrackOffset == global::UnityEngine.Timeline.TrackOffset.ApplySceneOffsets)
			{
				if (!global::UnityEngine.Application.isPlaying)
				{
					return global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetEditor;
				}
				return global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffset;
			}
			if (HasController(go))
			{
				if (!global::UnityEngine.Application.isPlaying)
				{
					return global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacyEditor;
				}
				return global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy;
			}
			return global::UnityEngine.Timeline.AppliedOffsetMode.TransformOffsetLegacy;
		}

		private bool IsRootTransformDisabledByMask(global::UnityEngine.GameObject gameObject, global::UnityEngine.Transform genericRootNode)
		{
			if (avatarMask == null || !applyAvatarMask)
			{
				return false;
			}
			global::UnityEngine.Animator binding = GetBinding((gameObject != null) ? gameObject.GetComponent<global::UnityEngine.Playables.PlayableDirector>() : null);
			if (binding == null)
			{
				return false;
			}
			if (binding.isHuman)
			{
				return !avatarMask.GetHumanoidBodyPartActive(global::UnityEngine.AvatarMaskBodyPart.Root);
			}
			if (avatarMask.transformCount == 0)
			{
				return false;
			}
			if (genericRootNode == null)
			{
				if (string.IsNullOrEmpty(avatarMask.GetTransformPath(0)))
				{
					return !avatarMask.GetTransformActive(0);
				}
				return false;
			}
			for (int i = 0; i < avatarMask.transformCount; i++)
			{
				if (genericRootNode == binding.transform.Find(avatarMask.GetTransformPath(i)))
				{
					return !avatarMask.GetTransformActive(i);
				}
			}
			return false;
		}

		private global::UnityEngine.Transform GetGenericRootNode(global::UnityEngine.GameObject gameObject)
		{
			global::UnityEngine.Animator binding = GetBinding((gameObject != null) ? gameObject.GetComponent<global::UnityEngine.Playables.PlayableDirector>() : null);
			if (binding == null)
			{
				return null;
			}
			if (binding.isHuman)
			{
				return null;
			}
			if (binding.avatar == null)
			{
				return null;
			}
			string rootMotionBoneName = binding.avatar.humanDescription.m_RootMotionBoneName;
			if (rootMotionBoneName == binding.name || string.IsNullOrEmpty(rootMotionBoneName))
			{
				return null;
			}
			return FindInHierarchyBreadthFirst(binding.transform, rootMotionBoneName);
		}

		internal bool AnimatesRootTransform()
		{
			if (global::UnityEngine.Timeline.AnimationPlayableAsset.HasRootTransforms(m_InfiniteClip))
			{
				return true;
			}
			foreach (global::UnityEngine.Timeline.TimelineClip clip in GetClips())
			{
				global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = clip.asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
				if (animationPlayableAsset != null && animationPlayableAsset.hasRootTransforms)
				{
					return true;
				}
			}
			return false;
		}

		private static global::UnityEngine.Transform FindInHierarchyBreadthFirst(global::UnityEngine.Transform t, string name)
		{
			s_CachedQueue.Clear();
			s_CachedQueue.Enqueue(t);
			while (s_CachedQueue.Count > 0)
			{
				global::UnityEngine.Transform transform = s_CachedQueue.Dequeue();
				if (transform.name == name)
				{
					return transform;
				}
				for (int i = 0; i < transform.childCount; i++)
				{
					s_CachedQueue.Enqueue(transform.GetChild(i));
				}
			}
			return null;
		}

		internal override void OnUpgradeFromVersion(int oldVersion)
		{
			if (oldVersion < 1)
			{
				global::UnityEngine.Timeline.AnimationTrack.AnimationTrackUpgrade.ConvertRotationsToEuler(this);
			}
			if (oldVersion < 2)
			{
				global::UnityEngine.Timeline.AnimationTrack.AnimationTrackUpgrade.ConvertRootMotion(this);
			}
			if (oldVersion < 3)
			{
				global::UnityEngine.Timeline.AnimationTrack.AnimationTrackUpgrade.ConvertInfiniteTrack(this);
			}
		}
	}
}

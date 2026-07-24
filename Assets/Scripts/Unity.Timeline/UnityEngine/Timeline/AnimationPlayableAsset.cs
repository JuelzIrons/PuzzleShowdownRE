namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.NotKeyable]
	public class AnimationPlayableAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.Timeline.ITimelineClipAsset, global::UnityEngine.Timeline.IPropertyPreview, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum LoopMode
		{
			[global::UnityEngine.Tooltip("Use the loop time setting from the source AnimationClip.")]
			UseSourceAsset = 0,
			[global::UnityEngine.Tooltip("The source AnimationClip loops during playback.")]
			On = 1,
			[global::UnityEngine.Tooltip("The source AnimationClip does not loop during playback.")]
			Off = 2
		}

		private enum Versions
		{
			Initial = 0,
			RotationAsEuler = 1
		}

		private static class AnimationPlayableAssetUpgrade
		{
			public static void ConvertRotationToEuler(global::UnityEngine.Timeline.AnimationPlayableAsset asset)
			{
				asset.m_EulerAngles = asset.m_Rotation.eulerAngles;
			}
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.AnimationClip m_Clip;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_Position = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Vector3 m_EulerAngles = global::UnityEngine.Vector3.zero;

		[global::UnityEngine.SerializeField]
		private bool m_UseTrackMatchFields = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.MatchTargetFields m_MatchTargetFields = global::UnityEngine.Timeline.MatchTargetFieldConstants.All;

		[global::UnityEngine.SerializeField]
		private bool m_RemoveStartOffset = true;

		[global::UnityEngine.SerializeField]
		private bool m_ApplyFootIK = true;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode m_Loop;

		private static readonly int k_LatestVersion = 1;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.SerializeField]
		[global::System.Obsolete("Use m_RotationEuler Instead", false)]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Quaternion m_Rotation = global::UnityEngine.Quaternion.identity;

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

		public bool useTrackMatchFields
		{
			get
			{
				return m_UseTrackMatchFields;
			}
			set
			{
				m_UseTrackMatchFields = value;
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
				m_MatchTargetFields = value;
			}
		}

		public bool removeStartOffset
		{
			get
			{
				return m_RemoveStartOffset;
			}
			set
			{
				m_RemoveStartOffset = value;
			}
		}

		public bool applyFootIK
		{
			get
			{
				return m_ApplyFootIK;
			}
			set
			{
				m_ApplyFootIK = value;
			}
		}

		public global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop
		{
			get
			{
				return m_Loop;
			}
			set
			{
				m_Loop = value;
			}
		}

		internal bool hasRootTransforms
		{
			get
			{
				if (m_Clip != null)
				{
					return HasRootTransforms(m_Clip);
				}
				return false;
			}
		}

		internal global::UnityEngine.Timeline.AppliedOffsetMode appliedOffsetMode { get; set; }

		public global::UnityEngine.AnimationClip clip
		{
			get
			{
				return m_Clip;
			}
			set
			{
				if (value != null)
				{
					base.name = "AnimationPlayableAsset of " + value.name;
				}
				m_Clip = value;
			}
		}

		public override double duration
		{
			get
			{
				double animationClipLength = global::UnityEngine.Timeline.TimeUtility.GetAnimationClipLength(clip);
				if (animationClipLength < 1.401298464324817E-45)
				{
					return base.duration;
				}
				return animationClipLength;
			}
		}

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				yield return global::UnityEngine.Animations.AnimationPlayableBinding.Create(base.name, this);
			}
		}

		public global::UnityEngine.Timeline.ClipCaps clipCaps
		{
			get
			{
				global::UnityEngine.Timeline.ClipCaps clipCaps = global::UnityEngine.Timeline.ClipCaps.Extrapolation | global::UnityEngine.Timeline.ClipCaps.SpeedMultiplier | global::UnityEngine.Timeline.ClipCaps.Blending;
				if (m_Clip != null && m_Loop != global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode.Off && (m_Loop != global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode.UseSourceAsset || m_Clip.isLooping))
				{
					clipCaps |= global::UnityEngine.Timeline.ClipCaps.Looping;
				}
				if (m_Clip != null && !m_Clip.empty)
				{
					clipCaps |= global::UnityEngine.Timeline.ClipCaps.ClipIn;
				}
				return clipCaps;
			}
		}

		public override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			return CreatePlayable(graph, m_Clip, position, eulerAngles, removeStartOffset, appliedOffsetMode, applyFootIK, m_Loop);
		}

		internal static global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.AnimationClip clip, global::UnityEngine.Vector3 positionOffset, global::UnityEngine.Vector3 eulerOffset, bool removeStartOffset, global::UnityEngine.Timeline.AppliedOffsetMode mode, bool applyFootIK, global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode loop)
		{
			if (clip == null || clip.legacy)
			{
				return global::UnityEngine.Playables.Playable.Null;
			}
			global::UnityEngine.Animations.AnimationClipPlayable animationClipPlayable = global::UnityEngine.Animations.AnimationClipPlayable.Create(graph, clip);
			animationClipPlayable.SetRemoveStartOffset(removeStartOffset);
			animationClipPlayable.SetApplyFootIK(applyFootIK);
			animationClipPlayable.SetOverrideLoopTime(loop != global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode.UseSourceAsset);
			animationClipPlayable.SetLoopTime(loop == global::UnityEngine.Timeline.AnimationPlayableAsset.LoopMode.On);
			global::UnityEngine.Playables.Playable playable = animationClipPlayable;
			if (ShouldApplyScaleRemove(mode))
			{
				global::UnityEngine.Animations.AnimationRemoveScalePlayable animationRemoveScalePlayable = global::UnityEngine.Animations.AnimationRemoveScalePlayable.Create(graph, 1);
				graph.Connect(playable, 0, animationRemoveScalePlayable, 0);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationRemoveScalePlayable, 0, 1f);
				playable = animationRemoveScalePlayable;
			}
			if (ShouldApplyOffset(mode, clip))
			{
				global::UnityEngine.Animations.AnimationOffsetPlayable animationOffsetPlayable = global::UnityEngine.Animations.AnimationOffsetPlayable.Create(graph, positionOffset, global::UnityEngine.Quaternion.Euler(eulerOffset), 1);
				graph.Connect(playable, 0, animationOffsetPlayable, 0);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(animationOffsetPlayable, 0, 1f);
				playable = animationOffsetPlayable;
			}
			return playable;
		}

		private static bool ShouldApplyOffset(global::UnityEngine.Timeline.AppliedOffsetMode mode, global::UnityEngine.AnimationClip clip)
		{
			if (mode == global::UnityEngine.Timeline.AppliedOffsetMode.NoRootTransform || mode == global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy)
			{
				return false;
			}
			return HasRootTransforms(clip);
		}

		private static bool ShouldApplyScaleRemove(global::UnityEngine.Timeline.AppliedOffsetMode mode)
		{
			if (mode != global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacyEditor && mode != global::UnityEngine.Timeline.AppliedOffsetMode.SceneOffsetLegacy)
			{
				return mode == global::UnityEngine.Timeline.AppliedOffsetMode.TransformOffsetLegacy;
			}
			return true;
		}

		public void ResetOffsets()
		{
			position = global::UnityEngine.Vector3.zero;
			eulerAngles = global::UnityEngine.Vector3.zero;
		}

		public void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
			driver.AddFromClip(m_Clip);
		}

		internal static bool HasRootTransforms(global::UnityEngine.AnimationClip clip)
		{
			if (clip == null || clip.empty)
			{
				return false;
			}
			if (!clip.hasRootMotion && !clip.hasGenericRootTransform && !clip.hasMotionCurves)
			{
				return clip.hasRootCurves;
			}
			return true;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_Version = k_LatestVersion;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (m_Version < k_LatestVersion)
			{
				OnUpgradeFromVersion(m_Version);
			}
		}

		private void OnUpgradeFromVersion(int oldVersion)
		{
			if (oldVersion < 1)
			{
				global::UnityEngine.Timeline.AnimationPlayableAsset.AnimationPlayableAssetUpgrade.ConvertRotationToEuler(this);
			}
		}
	}
}

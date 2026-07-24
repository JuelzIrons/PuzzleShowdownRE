namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.ExcludeFromPreset]
	public class TimelineAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.Timeline.ITimelineClipAsset, global::UnityEngine.Timeline.IPropertyPreview
	{
		private enum Versions
		{
			Initial = 0
		}

		private static class TimelineAssetUpgrade
		{
		}

		[global::System.Obsolete("MediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
		public enum MediaType
		{
			Animation = 0,
			Audio = 1,
			Texture = 2,
			[global::System.Obsolete("Use Texture MediaType instead. (UnityUpgradable) -> UnityEngine.Timeline.TimelineAsset/MediaType.Texture", false)]
			Video = 2,
			Script = 3,
			Hybrid = 4,
			Group = 5
		}

		public enum DurationMode
		{
			BasedOnClips = 0,
			FixedLength = 1
		}

		[global::System.Serializable]
		public class EditorSettings
		{
			internal static readonly double kMinFrameRate = global::UnityEngine.Timeline.TimeUtility.kFrameRateEpsilon;

			internal static readonly double kMaxFrameRate = 1000.0;

			internal static readonly double kDefaultFrameRate = 60.0;

			[global::UnityEngine.HideInInspector]
			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Timeline.FrameRateField]
			private double m_Framerate = kDefaultFrameRate;

			[global::UnityEngine.HideInInspector]
			[global::UnityEngine.SerializeField]
			private bool m_ScenePreview = true;

			[global::System.Obsolete("EditorSettings.fps has been deprecated. Use editorSettings.frameRate instead.", false)]
			public float fps
			{
				get
				{
					return (float)m_Framerate;
				}
				set
				{
					m_Framerate = global::UnityEngine.Mathf.Clamp(value, (float)kMinFrameRate, (float)kMaxFrameRate);
				}
			}

			public double frameRate
			{
				get
				{
					return m_Framerate;
				}
				set
				{
					m_Framerate = GetValidFrameRate(value);
				}
			}

			public bool scenePreview
			{
				get
				{
					return m_ScenePreview;
				}
				set
				{
					m_ScenePreview = value;
				}
			}

			public void SetStandardFrameRate(global::UnityEngine.Timeline.StandardFrameRates enumValue)
			{
				global::UnityEngine.Playables.FrameRate frameRate = global::UnityEngine.Timeline.TimeUtility.ToFrameRate(enumValue);
				if (!frameRate.IsValid())
				{
					throw new global::System.ArgumentException($"StandardFrameRates {enumValue.ToString()}, is not defined");
				}
				m_Framerate = frameRate.rate;
			}
		}

		private const int k_LatestVersion = 0;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> m_Tracks;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		private double m_FixedDuration;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Timeline.TrackAsset[] m_CacheOutputTracks;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> m_CacheRootTracks;

		[global::System.NonSerialized]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Timeline.TrackAsset[] m_CacheFlattenedTracks;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineAsset.EditorSettings m_EditorSettings = new global::UnityEngine.Timeline.TimelineAsset.EditorSettings();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.TimelineAsset.DurationMode m_DurationMode;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.MarkerTrack m_MarkerTrack;

		public global::UnityEngine.Timeline.TimelineAsset.EditorSettings editorSettings => m_EditorSettings;

		public override double duration
		{
			get
			{
				if (m_DurationMode == global::UnityEngine.Timeline.TimelineAsset.DurationMode.BasedOnClips)
				{
					global::UnityEngine.Timeline.DiscreteTime discreteTime = CalculateItemsDuration();
					if (discreteTime <= 0)
					{
						return 0.0;
					}
					return (double)discreteTime.OneTickBefore();
				}
				return m_FixedDuration;
			}
		}

		public double fixedDuration
		{
			get
			{
				global::UnityEngine.Timeline.DiscreteTime discreteTime = (global::UnityEngine.Timeline.DiscreteTime)m_FixedDuration;
				if (discreteTime <= 0)
				{
					return 0.0;
				}
				return (double)discreteTime.OneTickBefore();
			}
			set
			{
				m_FixedDuration = global::System.Math.Max(0.0, value);
			}
		}

		public global::UnityEngine.Timeline.TimelineAsset.DurationMode durationMode
		{
			get
			{
				return m_DurationMode;
			}
			set
			{
				m_DurationMode = value;
			}
		}

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				foreach (global::UnityEngine.Timeline.TrackAsset outputTrack in GetOutputTracks())
				{
					foreach (global::UnityEngine.Playables.PlayableBinding output in outputTrack.outputs)
					{
						yield return output;
					}
				}
			}
		}

		public global::UnityEngine.Timeline.ClipCaps clipCaps
		{
			get
			{
				global::UnityEngine.Timeline.ClipCaps clipCaps = global::UnityEngine.Timeline.ClipCaps.All;
				foreach (global::UnityEngine.Timeline.TrackAsset rootTrack in GetRootTracks())
				{
					global::UnityEngine.Timeline.TimelineClip[] clips = rootTrack.clips;
					foreach (global::UnityEngine.Timeline.TimelineClip timelineClip in clips)
					{
						clipCaps &= timelineClip.clipCaps;
					}
				}
				return clipCaps;
			}
		}

		public int outputTrackCount
		{
			get
			{
				UpdateOutputTrackCache();
				return m_CacheOutputTracks.Length;
			}
		}

		public int rootTrackCount
		{
			get
			{
				UpdateRootTrackCache();
				return m_CacheRootTracks.Count;
			}
		}

		internal global::UnityEngine.Timeline.TrackAsset[] flattenedTracks
		{
			get
			{
				if (m_CacheFlattenedTracks == null)
				{
					global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> allTracks = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>(m_Tracks.Count * 2);
					UpdateRootTrackCache();
					allTracks.AddRange(m_CacheRootTracks);
					for (int i = 0; i < m_CacheRootTracks.Count; i++)
					{
						AddSubTracksRecursive(m_CacheRootTracks[i], ref allTracks);
					}
					m_CacheFlattenedTracks = allTracks.ToArray();
				}
				return m_CacheFlattenedTracks;
			}
		}

		public global::UnityEngine.Timeline.MarkerTrack markerTrack => m_MarkerTrack;

		internal global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> trackObjects => m_Tracks;

		private void UpgradeToLatestVersion()
		{
		}

		private void OnValidate()
		{
			editorSettings.frameRate = GetValidFrameRate(editorSettings.frameRate);
		}

		public global::UnityEngine.Timeline.TrackAsset GetRootTrack(int index)
		{
			UpdateRootTrackCache();
			return m_CacheRootTracks[index];
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> GetRootTracks()
		{
			UpdateRootTrackCache();
			return m_CacheRootTracks;
		}

		public global::UnityEngine.Timeline.TrackAsset GetOutputTrack(int index)
		{
			UpdateOutputTrackCache();
			return m_CacheOutputTracks[index];
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> GetOutputTracks()
		{
			UpdateOutputTrackCache();
			return m_CacheOutputTracks;
		}

		private static double GetValidFrameRate(double frameRate)
		{
			return global::System.Math.Min(global::System.Math.Max(frameRate, global::UnityEngine.Timeline.TimelineAsset.EditorSettings.kMinFrameRate), global::UnityEngine.Timeline.TimelineAsset.EditorSettings.kMaxFrameRate);
		}

		private void UpdateRootTrackCache()
		{
			if (m_CacheRootTracks != null)
			{
				return;
			}
			if (m_Tracks == null)
			{
				m_CacheRootTracks = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>();
				return;
			}
			m_CacheRootTracks = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>(m_Tracks.Count);
			if (markerTrack != null)
			{
				m_CacheRootTracks.Add(markerTrack);
			}
			foreach (global::UnityEngine.ScriptableObject track in m_Tracks)
			{
				global::UnityEngine.Timeline.TrackAsset trackAsset = track as global::UnityEngine.Timeline.TrackAsset;
				if (trackAsset != null)
				{
					m_CacheRootTracks.Add(trackAsset);
				}
			}
		}

		private void UpdateOutputTrackCache()
		{
			if (m_CacheOutputTracks != null)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> list = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>();
			global::UnityEngine.Timeline.TrackAsset[] array = flattenedTracks;
			foreach (global::UnityEngine.Timeline.TrackAsset trackAsset in array)
			{
				if (trackAsset != null && trackAsset.GetType() != typeof(global::UnityEngine.Timeline.GroupTrack) && !trackAsset.isSubTrack)
				{
					list.Add(trackAsset);
				}
			}
			m_CacheOutputTracks = list.ToArray();
		}

		internal void AddTrackInternal(global::UnityEngine.Timeline.TrackAsset track)
		{
			m_Tracks.Add(track);
			track.parent = this;
			Invalidate();
		}

		internal void RemoveTrack(global::UnityEngine.Timeline.TrackAsset track)
		{
			m_Tracks.Remove(track);
			Invalidate();
			global::UnityEngine.Timeline.TrackAsset trackAsset = track.parent as global::UnityEngine.Timeline.TrackAsset;
			if (trackAsset != null)
			{
				trackAsset.RemoveSubTrack(track);
			}
		}

		public override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			bool autoRebalance = false;
			bool createOutputs = graph.GetPlayableCount() == 0;
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimelinePlayable> scriptPlayable = global::UnityEngine.Timeline.TimelinePlayable.Create(graph, GetOutputTracks(), go, autoRebalance, createOutputs);
			global::UnityEngine.Playables.PlayableExtensions.SetDuration(scriptPlayable, duration);
			global::UnityEngine.Playables.PlayableExtensions.SetPropagateSetTime(scriptPlayable, value: true);
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(scriptPlayable))
			{
				return global::UnityEngine.Playables.Playable.Null;
			}
			return scriptPlayable;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_Version = 0;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Invalidate();
			if (m_Version < 0)
			{
				UpgradeToLatestVersion();
			}
		}

		private void __internalAwake()
		{
			if (m_Tracks == null)
			{
				m_Tracks = new global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject>();
			}
			for (int num = m_Tracks.Count - 1; num >= 0; num--)
			{
				global::UnityEngine.Timeline.TrackAsset trackAsset = m_Tracks[num] as global::UnityEngine.Timeline.TrackAsset;
				if (trackAsset != null)
				{
					trackAsset.parent = this;
				}
			}
		}

		public void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
			foreach (global::UnityEngine.Timeline.TrackAsset outputTrack in GetOutputTracks())
			{
				if (!outputTrack.mutedInHierarchy)
				{
					outputTrack.GatherProperties(director, driver);
				}
			}
		}

		public void CreateMarkerTrack()
		{
			if (m_MarkerTrack == null)
			{
				m_MarkerTrack = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.Timeline.MarkerTrack>();
				global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(m_MarkerTrack, this);
				m_MarkerTrack.parent = this;
				m_MarkerTrack.name = "Markers";
				Invalidate();
			}
		}

		internal void RemoveMarkerTrack()
		{
			if (m_MarkerTrack != null)
			{
				global::UnityEngine.Timeline.MarkerTrack childAsset = m_MarkerTrack;
				m_MarkerTrack = null;
				global::UnityEngine.Timeline.TimelineCreateUtilities.RemoveAssetFromObject(childAsset, this);
				Invalidate();
			}
		}

		internal void Invalidate()
		{
			m_CacheRootTracks = null;
			m_CacheOutputTracks = null;
			m_CacheFlattenedTracks = null;
		}

		internal void UpdateFixedDurationWithItemsDuration()
		{
			m_FixedDuration = (double)CalculateItemsDuration();
		}

		private global::UnityEngine.Timeline.DiscreteTime CalculateItemsDuration()
		{
			global::UnityEngine.Timeline.DiscreteTime discreteTime = new global::UnityEngine.Timeline.DiscreteTime(0);
			global::UnityEngine.Timeline.TrackAsset[] array = flattenedTracks;
			foreach (global::UnityEngine.Timeline.TrackAsset trackAsset in array)
			{
				if (!trackAsset.mutedInHierarchy)
				{
					discreteTime = global::UnityEngine.Timeline.DiscreteTime.Max(discreteTime, (global::UnityEngine.Timeline.DiscreteTime)trackAsset.end);
				}
			}
			if (discreteTime <= 0)
			{
				return new global::UnityEngine.Timeline.DiscreteTime(0);
			}
			return discreteTime;
		}

		private static void AddSubTracksRecursive(global::UnityEngine.Timeline.TrackAsset track, ref global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> allTracks)
		{
			if (track == null)
			{
				return;
			}
			allTracks.AddRange(track.GetChildTracks());
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in track.GetChildTracks())
			{
				AddSubTracksRecursive(childTrack, ref allTracks);
			}
		}

		public global::UnityEngine.Timeline.TrackAsset CreateTrack(global::System.Type type, global::UnityEngine.Timeline.TrackAsset parent, string name)
		{
			if (parent != null && parent.timelineAsset != this)
			{
				throw new global::System.InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(global::UnityEngine.Timeline.TrackAsset).IsAssignableFrom(type))
			{
				throw new global::System.InvalidOperationException("Supplied type must be a track asset");
			}
			if (parent != null && !global::UnityEngine.Timeline.TimelineCreateUtilities.ValidateParentTrack(parent, type))
			{
				throw new global::System.InvalidOperationException("Cannot assign a child of type " + type.Name + " to a parent of type " + parent.GetType().Name);
			}
			string text = name;
			if (string.IsNullOrEmpty(text))
			{
				text = type.Name;
			}
			string text2 = text;
			text2 = ((!(parent != null)) ? global::UnityEngine.Timeline.TimelineCreateUtilities.GenerateUniqueActorName(trackObjects, text) : global::UnityEngine.Timeline.TimelineCreateUtilities.GenerateUniqueActorName(parent.subTracksObjects, text));
			return AllocateTrack(parent, text2, type);
		}

		public T CreateTrack<T>(global::UnityEngine.Timeline.TrackAsset parent, string trackName) where T : global::UnityEngine.Timeline.TrackAsset, new()
		{
			return (T)CreateTrack(typeof(T), parent, trackName);
		}

		public T CreateTrack<T>(string trackName) where T : global::UnityEngine.Timeline.TrackAsset, new()
		{
			return (T)CreateTrack(typeof(T), null, trackName);
		}

		public T CreateTrack<T>() where T : global::UnityEngine.Timeline.TrackAsset, new()
		{
			return (T)CreateTrack(typeof(T), null, null);
		}

		public bool DeleteClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip == null || clip.GetParentTrack() == null)
			{
				return false;
			}
			if (this != clip.GetParentTrack().timelineAsset)
			{
				global::UnityEngine.Debug.LogError("Cannot delete a clip from this timeline");
				return false;
			}
			if (clip.curves != null)
			{
				global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (clip.asset != null)
			{
				DeleteRecordedAnimation(clip);
				global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.asset);
			}
			global::UnityEngine.Timeline.TrackAsset parentTrack = clip.GetParentTrack();
			parentTrack.RemoveClip(clip);
			parentTrack.CalculateExtrapolationTimes();
			return true;
		}

		public bool DeleteTrack(global::UnityEngine.Timeline.TrackAsset track)
		{
			if (track.timelineAsset != this)
			{
				return false;
			}
			_ = track.parent as global::UnityEngine.Timeline.TrackAsset != null;
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in track.GetChildTracks())
			{
				DeleteTrack(childTrack);
			}
			DeleteRecordedAnimation(track);
			foreach (global::UnityEngine.Timeline.TimelineClip item in new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>(track.clips))
			{
				DeleteClip(item);
			}
			RemoveTrack(track);
			global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, this, track);
			return true;
		}

		internal void MoveLastTrackBefore(global::UnityEngine.Timeline.TrackAsset asset)
		{
			if (m_Tracks == null || m_Tracks.Count < 2 || asset == null)
			{
				return;
			}
			global::UnityEngine.ScriptableObject scriptableObject = m_Tracks[m_Tracks.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < m_Tracks.Count - 1; i++)
			{
				if (m_Tracks[i] == asset)
				{
					for (int num = m_Tracks.Count - 1; num > i; num--)
					{
						m_Tracks[num] = m_Tracks[num - 1];
					}
					m_Tracks[i] = scriptableObject;
					Invalidate();
					break;
				}
			}
		}

		private global::UnityEngine.Timeline.TrackAsset AllocateTrack(global::UnityEngine.Timeline.TrackAsset trackAssetParent, string trackName, global::System.Type trackType)
		{
			if (trackAssetParent != null && trackAssetParent.timelineAsset != this)
			{
				throw new global::System.InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(global::UnityEngine.Timeline.TrackAsset).IsAssignableFrom(trackType))
			{
				throw new global::System.InvalidOperationException("Supplied type must be a track asset");
			}
			global::UnityEngine.Timeline.TrackAsset trackAsset = (global::UnityEngine.Timeline.TrackAsset)global::UnityEngine.ScriptableObject.CreateInstance(trackType);
			trackAsset.name = trackName;
			global::UnityEngine.Playables.PlayableAsset masterAsset = ((trackAssetParent != null) ? ((global::UnityEngine.Playables.PlayableAsset)trackAssetParent) : ((global::UnityEngine.Playables.PlayableAsset)this));
			global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(trackAsset, masterAsset);
			if (trackAssetParent != null)
			{
				trackAssetParent.AddChild(trackAsset);
			}
			else
			{
				AddTrackInternal(trackAsset);
			}
			return trackAsset;
		}

		private void DeleteRecordedAnimation(global::UnityEngine.Timeline.TrackAsset track)
		{
			global::UnityEngine.Timeline.AnimationTrack animationTrack = track as global::UnityEngine.Timeline.AnimationTrack;
			if (animationTrack != null && animationTrack.infiniteClip != null)
			{
				global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, track, animationTrack.infiniteClip);
			}
			if (track.curves != null)
			{
				global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, track, track.curves);
			}
		}

		private void DeleteRecordedAnimation(global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (clip == null)
			{
				return;
			}
			if (clip.curves != null)
			{
				global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (clip.recordable)
			{
				global::UnityEngine.Timeline.AnimationPlayableAsset animationPlayableAsset = clip.asset as global::UnityEngine.Timeline.AnimationPlayableAsset;
				if (!(animationPlayableAsset == null) && !(animationPlayableAsset.clip == null))
				{
					global::UnityEngine.Timeline.TimelineUndo.PushDestroyUndo(this, animationPlayableAsset, animationPlayableAsset.clip);
				}
			}
		}
	}
}

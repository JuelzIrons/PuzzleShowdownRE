namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.IgnoreOnPlayableTrack]
	public abstract class TrackAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.ISerializationCallbackReceiver, global::UnityEngine.Timeline.IPropertyPreview, global::UnityEngine.Timeline.ICurvesOwner
	{
		internal enum Versions
		{
			Initial = 0,
			RotationAsEuler = 1,
			RootMotionUpgrade = 2,
			AnimatedTrackProperties = 3
		}

		private static class TrackAssetUpgrade
		{
		}

		private struct TransientBuildData
		{
			public global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> trackList;

			public global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip> clipList;

			public global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker> markerList;

			public static global::UnityEngine.Timeline.TrackAsset.TransientBuildData Create()
			{
				return new global::UnityEngine.Timeline.TrackAsset.TransientBuildData
				{
					trackList = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>(20),
					clipList = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>(500),
					markerList = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker>(100)
				};
			}

			public void Clear()
			{
				trackList.Clear();
				clipList.Clear();
				markerList.Clear();
			}
		}

		private const int k_LatestVersion = 3;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::System.Obsolete("Please use m_InfiniteClip (on AnimationTrack) instead.", false)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_animClip")]
		internal global::UnityEngine.AnimationClip m_AnimClip;

		private static global::UnityEngine.Timeline.TrackAsset.TransientBuildData s_BuildData = global::UnityEngine.Timeline.TrackAsset.TransientBuildData.Create();

		internal const string kDefaultCurvesName = "Track Parameters";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private bool m_Locked;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private bool m_Muted;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private string m_CustomPlayableFullTypename = string.Empty;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.AnimationClip m_Curves;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Playables.PlayableAsset m_Parent;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> m_Children;

		[global::System.NonSerialized]
		private int m_ItemsHash;

		[global::System.NonSerialized]
		private global::UnityEngine.Timeline.TimelineClip[] m_ClipsCache;

		private global::UnityEngine.Timeline.DiscreteTime m_Start;

		private global::UnityEngine.Timeline.DiscreteTime m_End;

		private bool m_CacheSorted;

		private bool m_BlendsValid = true;

		private bool? m_SupportsNotifications;

		private static global::UnityEngine.Timeline.TrackAsset[] s_EmptyCache = new global::UnityEngine.Timeline.TrackAsset[0];

		private global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> m_ChildTrackCache;

		private static global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.Timeline.TrackBindingTypeAttribute> s_TrackBindingTypeAttributeCache = new global::System.Collections.Generic.Dictionary<global::System.Type, global::UnityEngine.Timeline.TrackBindingTypeAttribute>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		protected internal global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip> m_Clips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Timeline.MarkerList m_Markers = new global::UnityEngine.Timeline.MarkerList(0);

		public double start
		{
			get
			{
				UpdateDuration();
				return (double)m_Start;
			}
		}

		public double end
		{
			get
			{
				UpdateDuration();
				return (double)m_End;
			}
		}

		public sealed override double duration
		{
			get
			{
				UpdateDuration();
				return (double)(m_End - m_Start);
			}
		}

		public bool muted
		{
			get
			{
				return m_Muted;
			}
			set
			{
				m_Muted = value;
			}
		}

		public bool mutedInHierarchy
		{
			get
			{
				if (muted)
				{
					return true;
				}
				global::UnityEngine.Timeline.TrackAsset trackAsset = this;
				while (trackAsset.parent as global::UnityEngine.Timeline.TrackAsset != null)
				{
					trackAsset = (global::UnityEngine.Timeline.TrackAsset)trackAsset.parent;
					if (trackAsset as global::UnityEngine.Timeline.GroupTrack != null)
					{
						return trackAsset.mutedInHierarchy;
					}
				}
				return false;
			}
		}

		public global::UnityEngine.Timeline.TimelineAsset timelineAsset
		{
			get
			{
				global::UnityEngine.Timeline.TrackAsset trackAsset = this;
				while (trackAsset != null)
				{
					if (trackAsset.parent == null)
					{
						return null;
					}
					global::UnityEngine.Timeline.TimelineAsset timelineAsset = trackAsset.parent as global::UnityEngine.Timeline.TimelineAsset;
					if (timelineAsset != null)
					{
						return timelineAsset;
					}
					trackAsset = trackAsset.parent as global::UnityEngine.Timeline.TrackAsset;
				}
				return null;
			}
		}

		public global::UnityEngine.Playables.PlayableAsset parent
		{
			get
			{
				return m_Parent;
			}
			internal set
			{
				m_Parent = value;
			}
		}

		internal global::UnityEngine.Timeline.TimelineClip[] clips
		{
			get
			{
				if (m_Clips == null)
				{
					m_Clips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>();
				}
				if (m_ClipsCache == null)
				{
					m_CacheSorted = false;
					m_ClipsCache = m_Clips.ToArray();
				}
				return m_ClipsCache;
			}
		}

		internal bool blendsValid
		{
			get
			{
				return m_BlendsValid;
			}
			set
			{
				m_BlendsValid = value;
			}
		}

		public virtual bool isEmpty
		{
			get
			{
				if (!hasClips && !hasCurves)
				{
					return GetMarkerCount() == 0;
				}
				return false;
			}
		}

		public bool hasClips
		{
			get
			{
				if (m_Clips != null)
				{
					return m_Clips.Count != 0;
				}
				return false;
			}
		}

		public bool hasCurves
		{
			get
			{
				if (m_Curves != null)
				{
					return !m_Curves.empty;
				}
				return false;
			}
		}

		public bool isSubTrack
		{
			get
			{
				global::UnityEngine.Timeline.TrackAsset trackAsset = parent as global::UnityEngine.Timeline.TrackAsset;
				if (trackAsset != null)
				{
					return trackAsset.GetType() == GetType();
				}
				return false;
			}
		}

		public override global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableBinding> outputs
		{
			get
			{
				if (!s_TrackBindingTypeAttributeCache.TryGetValue(GetType(), out var value))
				{
					value = (global::UnityEngine.Timeline.TrackBindingTypeAttribute)global::System.Attribute.GetCustomAttribute(GetType(), typeof(global::UnityEngine.Timeline.TrackBindingTypeAttribute));
					s_TrackBindingTypeAttributeCache.Add(GetType(), value);
				}
				global::System.Type type = value?.type;
				yield return global::UnityEngine.Playables.ScriptPlayableBinding.Create(base.name, this, type);
			}
		}

		internal string customPlayableTypename
		{
			get
			{
				return m_CustomPlayableFullTypename;
			}
			set
			{
				m_CustomPlayableFullTypename = value;
			}
		}

		public global::UnityEngine.AnimationClip curves
		{
			get
			{
				return m_Curves;
			}
			internal set
			{
				m_Curves = value;
			}
		}

		string global::UnityEngine.Timeline.ICurvesOwner.defaultCurvesName => "Track Parameters";

		global::UnityEngine.Object global::UnityEngine.Timeline.ICurvesOwner.asset => this;

		global::UnityEngine.Object global::UnityEngine.Timeline.ICurvesOwner.assetOwner => timelineAsset;

		global::UnityEngine.Timeline.TrackAsset global::UnityEngine.Timeline.ICurvesOwner.targetTrack => this;

		internal global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> subTracksObjects => m_Children;

		public bool locked
		{
			get
			{
				return m_Locked;
			}
			set
			{
				m_Locked = value;
			}
		}

		public bool lockedInHierarchy
		{
			get
			{
				if (locked)
				{
					return true;
				}
				global::UnityEngine.Timeline.TrackAsset trackAsset = this;
				while (trackAsset.parent as global::UnityEngine.Timeline.TrackAsset != null)
				{
					trackAsset = (global::UnityEngine.Timeline.TrackAsset)trackAsset.parent;
					if (trackAsset as global::UnityEngine.Timeline.GroupTrack != null)
					{
						return trackAsset.lockedInHierarchy;
					}
				}
				return false;
			}
		}

		public bool supportsNotifications
		{
			get
			{
				if (!m_SupportsNotifications.HasValue)
				{
					m_SupportsNotifications = global::UnityEngine.Timeline.NotificationUtilities.TrackTypeSupportsNotifications(GetType());
				}
				return m_SupportsNotifications.Value;
			}
		}

		internal static event global::System.Action<global::UnityEngine.Timeline.TimelineClip, global::UnityEngine.GameObject, global::UnityEngine.Playables.Playable> OnClipPlayableCreate;

		internal static event global::System.Action<global::UnityEngine.Timeline.TrackAsset, global::UnityEngine.GameObject, global::UnityEngine.Playables.Playable> OnTrackAnimationPlayableCreate;

		protected virtual void OnBeforeTrackSerialize()
		{
		}

		protected virtual void OnAfterTrackDeserialize()
		{
		}

		internal virtual void OnUpgradeFromVersion(int oldVersion)
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_Version = 3;
			if (m_Children != null)
			{
				for (int num = m_Children.Count - 1; num >= 0; num--)
				{
					global::UnityEngine.Timeline.TrackAsset trackAsset = m_Children[num] as global::UnityEngine.Timeline.TrackAsset;
					if (trackAsset != null && trackAsset.parent != this)
					{
						trackAsset.parent = this;
					}
				}
			}
			OnBeforeTrackSerialize();
			this.ComputeBlendsFromOverlaps();
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			m_ClipsCache = null;
			Invalidate();
			if (m_Version < 3)
			{
				UpgradeToLatestVersion();
				OnUpgradeFromVersion(m_Version);
			}
			foreach (global::UnityEngine.Timeline.IMarker marker in GetMarkers())
			{
				marker.Initialize(this);
			}
			OnAfterTrackDeserialize();
		}

		private void UpgradeToLatestVersion()
		{
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TimelineClip> GetClips()
		{
			return clips;
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> GetChildTracks()
		{
			UpdateChildTrackCache();
			return m_ChildTrackCache;
		}

		private void __internalAwake()
		{
			if (m_Clips == null)
			{
				m_Clips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>();
			}
			m_ChildTrackCache = null;
			if (m_Children == null)
			{
				m_Children = new global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject>();
			}
		}

		public void CreateCurves(string curvesClipName)
		{
			if (!(m_Curves != null))
			{
				m_Curves = global::UnityEngine.Timeline.TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? "Track Parameters" : curvesClipName, this, isLegacy: true);
			}
		}

		public virtual global::UnityEngine.Playables.Playable CreateTrackMixer(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, int inputCount)
		{
			return global::UnityEngine.Playables.Playable.Create(graph, inputCount);
		}

		public sealed override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			return global::UnityEngine.Playables.Playable.Null;
		}

		public global::UnityEngine.Timeline.TimelineClip CreateDefaultClip()
		{
			object[] customAttributes = GetType().GetCustomAttributes(typeof(global::UnityEngine.Timeline.TrackClipTypeAttribute), inherit: true);
			global::System.Type type = null;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is global::UnityEngine.Timeline.TrackClipTypeAttribute trackClipTypeAttribute && typeof(global::UnityEngine.Playables.IPlayableAsset).IsAssignableFrom(trackClipTypeAttribute.inspectedType) && typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(trackClipTypeAttribute.inspectedType))
				{
					type = trackClipTypeAttribute.inspectedType;
					break;
				}
			}
			if (type == null)
			{
				global::UnityEngine.Debug.LogWarning("Cannot create a default clip for type " + GetType());
				return null;
			}
			return CreateAndAddNewClipOfType(type);
		}

		public global::UnityEngine.Timeline.TimelineClip CreateClip<T>() where T : global::UnityEngine.ScriptableObject, global::UnityEngine.Playables.IPlayableAsset
		{
			return CreateClip(typeof(T));
		}

		public bool DeleteClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (!m_Clips.Contains(clip))
			{
				throw new global::System.InvalidOperationException("Cannot delete clip since it is not a child of the TrackAsset.");
			}
			if (timelineAsset != null)
			{
				return timelineAsset.DeleteClip(clip);
			}
			return false;
		}

		public global::UnityEngine.Timeline.IMarker CreateMarker(global::System.Type type, double time)
		{
			return m_Markers.CreateMarker(type, time, this);
		}

		public T CreateMarker<T>(double time) where T : global::UnityEngine.ScriptableObject, global::UnityEngine.Timeline.IMarker
		{
			return (T)CreateMarker(typeof(T), time);
		}

		public bool DeleteMarker(global::UnityEngine.Timeline.IMarker marker)
		{
			return m_Markers.Remove(marker);
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.IMarker> GetMarkers()
		{
			return m_Markers.GetMarkers();
		}

		public int GetMarkerCount()
		{
			return m_Markers.Count;
		}

		public global::UnityEngine.Timeline.IMarker GetMarker(int idx)
		{
			return m_Markers[idx];
		}

		internal global::UnityEngine.Timeline.TimelineClip CreateClip(global::System.Type requestedType)
		{
			if (ValidateClipType(requestedType))
			{
				return CreateAndAddNewClipOfType(requestedType);
			}
			throw new global::System.InvalidOperationException("Clips of type " + requestedType?.ToString() + " are not permitted on tracks of type " + GetType());
		}

		internal global::UnityEngine.Timeline.TimelineClip CreateAndAddNewClipOfType(global::System.Type requestedType)
		{
			global::UnityEngine.Timeline.TimelineClip timelineClip = CreateClipOfType(requestedType);
			AddClip(timelineClip);
			return timelineClip;
		}

		internal global::UnityEngine.Timeline.TimelineClip CreateClipOfType(global::System.Type requestedType)
		{
			if (!ValidateClipType(requestedType))
			{
				throw new global::System.InvalidOperationException("Clips of type " + requestedType?.ToString() + " are not permitted on tracks of type " + GetType());
			}
			global::UnityEngine.ScriptableObject scriptableObject = global::UnityEngine.ScriptableObject.CreateInstance(requestedType);
			if (scriptableObject == null)
			{
				throw new global::System.InvalidOperationException("Could not create an instance of the ScriptableObject type " + requestedType.Name);
			}
			scriptableObject.name = requestedType.Name;
			global::UnityEngine.Timeline.TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, this);
			return CreateClipFromAsset(scriptableObject);
		}

		internal global::UnityEngine.Timeline.TimelineClip CreateClipFromPlayableAsset(global::UnityEngine.Playables.IPlayableAsset asset)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (asset as global::UnityEngine.ScriptableObject == null)
			{
				throw new global::System.ArgumentException("CreateClipFromPlayableAsset  only supports ScriptableObject-derived Types");
			}
			if (!ValidateClipType(asset.GetType()))
			{
				throw new global::System.InvalidOperationException("Clips of type " + asset.GetType()?.ToString() + " are not permitted on tracks of type " + GetType());
			}
			return CreateClipFromAsset(asset as global::UnityEngine.ScriptableObject);
		}

		private global::UnityEngine.Timeline.TimelineClip CreateClipFromAsset(global::UnityEngine.ScriptableObject playableAsset)
		{
			global::UnityEngine.Timeline.TimelineClip timelineClip = CreateNewClipContainerInternal();
			timelineClip.displayName = playableAsset.name;
			timelineClip.asset = playableAsset;
			if (playableAsset is global::UnityEngine.Playables.IPlayableAsset { duration: var num } && !double.IsInfinity(num) && num > 0.0)
			{
				timelineClip.duration = global::System.Math.Min(global::System.Math.Max(num, global::UnityEngine.Timeline.TimelineClip.kMinDuration), global::UnityEngine.Timeline.TimelineClip.kMaxTimeValue);
			}
			try
			{
				OnCreateClip(timelineClip);
				return timelineClip;
			}
			catch (global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogError(ex.Message, playableAsset);
				return null;
			}
		}

		internal global::System.Collections.Generic.IEnumerable<global::UnityEngine.ScriptableObject> GetMarkersRaw()
		{
			return m_Markers.GetRawMarkerList();
		}

		internal void ClearMarkers()
		{
			m_Markers.Clear();
		}

		internal void AddMarker(global::UnityEngine.ScriptableObject e)
		{
			m_Markers.Add(e);
		}

		internal bool DeleteMarkerRaw(global::UnityEngine.ScriptableObject marker)
		{
			return m_Markers.Remove(marker, timelineAsset, this);
		}

		private int GetTimeRangeHash()
		{
			double num = double.MaxValue;
			double num2 = double.MinValue;
			_ = m_Markers.Count;
			for (int i = 0; i < m_Markers.Count; i++)
			{
				global::UnityEngine.Timeline.IMarker marker = m_Markers[i];
				if (marker is global::UnityEngine.Playables.INotification)
				{
					if (marker.time < num)
					{
						num = marker.time;
					}
					if (marker.time > num2)
					{
						num2 = marker.time;
					}
				}
			}
			return num.GetHashCode().CombineHash(num2.GetHashCode());
		}

		internal void AddClip(global::UnityEngine.Timeline.TimelineClip newClip)
		{
			if (!m_Clips.Contains(newClip))
			{
				m_Clips.Add(newClip);
				m_ClipsCache = null;
				if (newClip.SupportsBlending())
				{
					blendsValid = false;
				}
			}
		}

		private global::UnityEngine.Playables.Playable CreateNotificationsPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable mixerPlayable, global::UnityEngine.GameObject go, global::UnityEngine.Playables.Playable timelinePlayable)
		{
			s_BuildData.markerList.Clear();
			GatherNotifications(s_BuildData.markerList);
			global::UnityEngine.Playables.PlayableDirector component;
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> scriptPlayable = ((!go.TryGetComponent<global::UnityEngine.Playables.PlayableDirector>(out component)) ? global::UnityEngine.Timeline.NotificationUtilities.CreateNotificationsPlayable(graph, s_BuildData.markerList, timelineAsset) : global::UnityEngine.Timeline.NotificationUtilities.CreateNotificationsPlayable(graph, s_BuildData.markerList, component));
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(scriptPlayable))
			{
				scriptPlayable.GetBehaviour().timeSource = timelinePlayable;
				if (global::UnityEngine.Playables.PlayableExtensions.IsValid(mixerPlayable))
				{
					global::UnityEngine.Playables.PlayableExtensions.SetInputCount(scriptPlayable, 1);
					graph.Connect(mixerPlayable, 0, scriptPlayable, 0);
					global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(scriptPlayable, mixerPlayable, 1f);
				}
			}
			return scriptPlayable;
		}

		internal global::UnityEngine.Playables.Playable CreatePlayableGraph(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree, global::UnityEngine.Playables.Playable timelinePlayable)
		{
			UpdateDuration();
			global::UnityEngine.Playables.Playable playable = global::UnityEngine.Playables.Playable.Null;
			if (CanCreateMixerRecursive())
			{
				playable = CreateMixerPlayableGraph(graph, go, tree);
			}
			global::UnityEngine.Playables.Playable playable2 = CreateNotificationsPlayable(graph, playable, go, timelinePlayable);
			s_BuildData.Clear();
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2) && !global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
			{
				global::UnityEngine.Debug.LogErrorFormat("Track {0} of type {1} has no notifications and returns an invalid mixer Playable", base.name, GetType().FullName);
				return global::UnityEngine.Playables.Playable.Create(graph);
			}
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2))
			{
				return playable;
			}
			return playable2;
		}

		internal virtual global::UnityEngine.Playables.Playable CompileClips(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::System.Collections.Generic.IList<global::UnityEngine.Timeline.TimelineClip> timelineClips, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree)
		{
			global::UnityEngine.Playables.Playable playable = CreateTrackMixer(graph, go, timelineClips.Count);
			for (int i = 0; i < timelineClips.Count; i++)
			{
				global::UnityEngine.Playables.Playable playable2 = CreatePlayable(graph, go, timelineClips[i]);
				if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2))
				{
					global::UnityEngine.Playables.PlayableExtensions.SetDuration(playable2, timelineClips[i].duration);
					global::UnityEngine.Timeline.RuntimeClip item = new global::UnityEngine.Timeline.RuntimeClip(timelineClips[i], playable2, playable);
					tree.Add(item);
					graph.Connect(playable2, 0, playable, i);
					global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(playable, i, 0f);
				}
			}
			ConfigureTrackAnimation(tree, go, playable);
			return playable;
		}

		private void GatherCompilableTracks(global::System.Collections.Generic.IList<global::UnityEngine.Timeline.TrackAsset> tracks)
		{
			if (!muted && CanCreateTrackMixer())
			{
				tracks.Add(this);
			}
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				if (childTrack != null)
				{
					childTrack.GatherCompilableTracks(tracks);
				}
			}
		}

		private void GatherNotifications(global::System.Collections.Generic.List<global::UnityEngine.Timeline.IMarker> markers)
		{
			if (!muted && CanCompileNotifications())
			{
				markers.AddRange(GetMarkers());
			}
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				if (childTrack != null)
				{
					childTrack.GatherNotifications(markers);
				}
			}
		}

		internal virtual global::UnityEngine.Playables.Playable CreateMixerPlayableGraph(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go, global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree)
		{
			if (tree == null)
			{
				throw new global::System.ArgumentException("IntervalTree argument cannot be null", "tree");
			}
			if (go == null)
			{
				throw new global::System.ArgumentException("GameObject argument cannot be null", "go");
			}
			s_BuildData.Clear();
			GatherCompilableTracks(s_BuildData.trackList);
			if (s_BuildData.trackList.Count == 0)
			{
				return global::UnityEngine.Playables.Playable.Null;
			}
			global::UnityEngine.Playables.Playable playable = global::UnityEngine.Playables.Playable.Null;
			if (this is global::UnityEngine.Timeline.ILayerable layerable)
			{
				playable = layerable.CreateLayerMixer(graph, go, s_BuildData.trackList.Count);
			}
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
			{
				for (int i = 0; i < s_BuildData.trackList.Count; i++)
				{
					global::UnityEngine.Playables.Playable playable2 = s_BuildData.trackList[i].CompileClips(graph, go, s_BuildData.trackList[i].clips, tree);
					if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2))
					{
						graph.Connect(playable2, 0, playable, i);
						global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(playable, i, 1f);
					}
				}
				return playable;
			}
			if (s_BuildData.trackList.Count == 1)
			{
				return s_BuildData.trackList[0].CompileClips(graph, go, s_BuildData.trackList[0].clips, tree);
			}
			for (int j = 0; j < s_BuildData.trackList.Count; j++)
			{
				s_BuildData.clipList.AddRange(s_BuildData.trackList[j].clips);
			}
			return CompileClips(graph, go, s_BuildData.clipList, tree);
		}

		internal void ConfigureTrackAnimation(global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> tree, global::UnityEngine.GameObject go, global::UnityEngine.Playables.Playable blend)
		{
			if (hasCurves)
			{
				global::UnityEngine.Animations.AnimationPlayableExtensions.SetAnimatedProperties(blend, m_Curves);
				tree.Add(new global::UnityEngine.Timeline.InfiniteRuntimeClip(blend));
				if (global::UnityEngine.Timeline.TrackAsset.OnTrackAnimationPlayableCreate != null)
				{
					global::UnityEngine.Timeline.TrackAsset.OnTrackAnimationPlayableCreate(this, go, blend);
				}
			}
		}

		internal void SortClips()
		{
			_ = clips;
			if (!m_CacheSorted)
			{
				global::System.Array.Sort(clips, (global::UnityEngine.Timeline.TimelineClip clip1, global::UnityEngine.Timeline.TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
				m_CacheSorted = true;
			}
		}

		internal void ClearClipsInternal()
		{
			m_Clips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimelineClip>();
			m_ClipsCache = null;
		}

		internal void ClearSubTracksInternal()
		{
			m_Children = new global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject>();
			Invalidate();
		}

		internal void OnClipMove(global::UnityEngine.Timeline.ITimelineClipAsset clip)
		{
			m_CacheSorted = false;
			if (clip != null && clip.clipCaps.HasAny(global::UnityEngine.Timeline.ClipCaps.Blending))
			{
				m_BlendsValid = false;
			}
		}

		internal global::UnityEngine.Timeline.TimelineClip CreateNewClipContainerInternal()
		{
			global::UnityEngine.Timeline.TimelineClip timelineClip = new global::UnityEngine.Timeline.TimelineClip(this);
			timelineClip.asset = null;
			double val = 0.0;
			for (int i = 0; i < m_Clips.Count - 1; i++)
			{
				double num = m_Clips[i].duration;
				if (double.IsInfinity(num))
				{
					num = global::UnityEngine.Timeline.TimelineClip.kDefaultClipDurationInSeconds;
				}
				val = global::System.Math.Max(val, m_Clips[i].start + num);
			}
			timelineClip.mixInCurve = global::UnityEngine.AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			timelineClip.mixOutCurve = global::UnityEngine.AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
			timelineClip.start = val;
			timelineClip.duration = global::UnityEngine.Timeline.TimelineClip.kDefaultClipDurationInSeconds;
			timelineClip.displayName = "untitled";
			return timelineClip;
		}

		internal void AddChild(global::UnityEngine.Timeline.TrackAsset child)
		{
			if (!(child == null))
			{
				m_Children.Add(child);
				child.parent = this;
				Invalidate();
			}
		}

		internal void MoveLastTrackBefore(global::UnityEngine.Timeline.TrackAsset asset)
		{
			if (m_Children == null || m_Children.Count < 2 || asset == null)
			{
				return;
			}
			global::UnityEngine.ScriptableObject scriptableObject = m_Children[m_Children.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < m_Children.Count - 1; i++)
			{
				if (m_Children[i] == asset)
				{
					for (int num = m_Children.Count - 1; num > i; num--)
					{
						m_Children[num] = m_Children[num - 1];
					}
					m_Children[i] = scriptableObject;
					Invalidate();
					break;
				}
			}
		}

		internal bool RemoveSubTrack(global::UnityEngine.Timeline.TrackAsset child)
		{
			if (m_Children.Remove(child))
			{
				Invalidate();
				child.parent = null;
				return true;
			}
			return false;
		}

		internal void RemoveClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
			m_Clips.Remove(clip);
			m_ClipsCache = null;
			if (clip.SupportsBlending())
			{
				blendsValid = false;
			}
		}

		internal virtual void GetEvaluationTime(out double outStart, out double outDuration)
		{
			outStart = 0.0;
			outDuration = 1.0;
			outStart = double.PositiveInfinity;
			double num = double.NegativeInfinity;
			if (hasCurves)
			{
				outStart = 0.0;
				num = global::UnityEngine.Timeline.TimeUtility.GetAnimationClipLength(curves);
			}
			global::UnityEngine.Timeline.TimelineClip[] array = clips;
			foreach (global::UnityEngine.Timeline.TimelineClip timelineClip in array)
			{
				outStart = global::System.Math.Min(timelineClip.start, outStart);
				num = global::System.Math.Max(timelineClip.end, num);
			}
			if (HasNotifications())
			{
				double notificationDuration = GetNotificationDuration();
				outStart = global::System.Math.Min(notificationDuration, outStart);
				num = global::System.Math.Max(notificationDuration, num);
			}
			if (double.IsInfinity(outStart) || double.IsInfinity(num))
			{
				outStart = (outDuration = 0.0);
			}
			else
			{
				outDuration = num - outStart;
			}
		}

		internal virtual void GetSequenceTime(out double outStart, out double outDuration)
		{
			GetEvaluationTime(out outStart, out outDuration);
		}

		public virtual void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
			global::UnityEngine.GameObject gameObjectBinding = GetGameObjectBinding(director);
			if (gameObjectBinding != null)
			{
				driver.PushActiveGameObject(gameObjectBinding);
			}
			if (hasCurves)
			{
				driver.AddObjectProperties(this, m_Curves);
			}
			global::UnityEngine.Timeline.TimelineClip[] array = clips;
			foreach (global::UnityEngine.Timeline.TimelineClip timelineClip in array)
			{
				if (timelineClip.curves != null && timelineClip.asset != null)
				{
					driver.AddObjectProperties(timelineClip.asset, timelineClip.curves);
				}
				if (timelineClip.asset is global::UnityEngine.Timeline.IPropertyPreview propertyPreview)
				{
					propertyPreview.GatherProperties(director, driver);
				}
			}
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				if (childTrack != null)
				{
					childTrack.GatherProperties(director, driver);
				}
			}
			if (gameObjectBinding != null)
			{
				driver.PopActiveGameObject();
			}
		}

		internal global::UnityEngine.GameObject GetGameObjectBinding(global::UnityEngine.Playables.PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			global::UnityEngine.Object genericBinding = director.GetGenericBinding(this);
			global::UnityEngine.GameObject gameObject = genericBinding as global::UnityEngine.GameObject;
			if (gameObject != null)
			{
				return gameObject;
			}
			global::UnityEngine.Component component = genericBinding as global::UnityEngine.Component;
			if (component != null)
			{
				return component.gameObject;
			}
			return null;
		}

		internal bool ValidateClipType(global::System.Type clipType)
		{
			object[] customAttributes = GetType().GetCustomAttributes(typeof(global::UnityEngine.Timeline.TrackClipTypeAttribute), inherit: true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (((global::UnityEngine.Timeline.TrackClipTypeAttribute)customAttributes[i]).inspectedType.IsAssignableFrom(clipType))
				{
					return true;
				}
			}
			if (typeof(global::UnityEngine.Timeline.PlayableTrack).IsAssignableFrom(GetType()) && typeof(global::UnityEngine.Playables.IPlayableAsset).IsAssignableFrom(clipType))
			{
				return typeof(global::UnityEngine.ScriptableObject).IsAssignableFrom(clipType);
			}
			return false;
		}

		protected virtual void OnCreateClip(global::UnityEngine.Timeline.TimelineClip clip)
		{
		}

		private void UpdateDuration()
		{
			int num = CalculateItemsHash();
			if (num != m_ItemsHash)
			{
				m_ItemsHash = num;
				GetSequenceTime(out var outStart, out var outDuration);
				m_Start = (global::UnityEngine.Timeline.DiscreteTime)outStart;
				m_End = (global::UnityEngine.Timeline.DiscreteTime)(outStart + outDuration);
				this.CalculateExtrapolationTimes();
			}
		}

		protected internal virtual int CalculateItemsHash()
		{
			return global::UnityEngine.Timeline.HashUtility.CombineHash(GetClipsHash(), GetAnimationClipHash(m_Curves), GetTimeRangeHash());
		}

		protected virtual global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject gameObject, global::UnityEngine.Timeline.TimelineClip clip)
		{
			if (!graph.IsValid())
			{
				throw new global::System.ArgumentException("graph must be a valid PlayableGraph");
			}
			if (clip == null)
			{
				throw new global::System.ArgumentNullException("clip");
			}
			if (clip.asset is global::UnityEngine.Playables.IPlayableAsset playableAsset)
			{
				global::UnityEngine.Playables.Playable playable = playableAsset.CreatePlayable(graph, gameObject);
				if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
				{
					global::UnityEngine.Animations.AnimationPlayableExtensions.SetAnimatedProperties(playable, clip.curves);
					global::UnityEngine.Playables.PlayableExtensions.SetSpeed(playable, clip.timeScale);
					if (global::UnityEngine.Timeline.TrackAsset.OnClipPlayableCreate != null)
					{
						global::UnityEngine.Timeline.TrackAsset.OnClipPlayableCreate(clip, gameObject, playable);
					}
				}
				return playable;
			}
			return global::UnityEngine.Playables.Playable.Null;
		}

		internal void Invalidate()
		{
			m_ChildTrackCache = null;
			global::UnityEngine.Timeline.TimelineAsset timelineAsset = this.timelineAsset;
			if (timelineAsset != null)
			{
				timelineAsset.Invalidate();
			}
		}

		internal double GetNotificationDuration()
		{
			if (!supportsNotifications)
			{
				return 0.0;
			}
			double num = 0.0;
			int count = m_Markers.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.Timeline.IMarker marker = m_Markers[i];
				if (marker is global::UnityEngine.Playables.INotification)
				{
					num = global::System.Math.Max(num, marker.time);
				}
			}
			return num;
		}

		internal virtual bool CanCompileClips()
		{
			if (!hasClips)
			{
				return hasCurves;
			}
			return true;
		}

		public virtual bool CanCreateTrackMixer()
		{
			return CanCompileClips();
		}

		internal bool IsCompilable()
		{
			if (typeof(global::UnityEngine.Timeline.GroupTrack).IsAssignableFrom(GetType()))
			{
				return false;
			}
			bool flag = !mutedInHierarchy && (CanCreateTrackMixer() || CanCompileNotifications());
			if (!flag)
			{
				foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
				{
					if (childTrack.IsCompilable())
					{
						return true;
					}
				}
			}
			return flag;
		}

		private void UpdateChildTrackCache()
		{
			if (m_ChildTrackCache != null)
			{
				return;
			}
			if (m_Children == null || m_Children.Count == 0)
			{
				m_ChildTrackCache = s_EmptyCache;
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> list = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>(m_Children.Count);
			for (int i = 0; i < m_Children.Count; i++)
			{
				global::UnityEngine.Timeline.TrackAsset trackAsset = m_Children[i] as global::UnityEngine.Timeline.TrackAsset;
				if (trackAsset != null)
				{
					list.Add(trackAsset);
				}
			}
			m_ChildTrackCache = list;
		}

		internal virtual int Hash()
		{
			return clips.Length + (m_Markers.Count << 16);
		}

		private int GetClipsHash()
		{
			int num = 0;
			foreach (global::UnityEngine.Timeline.TimelineClip clip in m_Clips)
			{
				num = num.CombineHash(clip.Hash());
			}
			return num;
		}

		protected static int GetAnimationClipHash(global::UnityEngine.AnimationClip clip)
		{
			int num = 0;
			if (clip != null && !clip.empty)
			{
				num = num.CombineHash(clip.frameRate.GetHashCode()).CombineHash(clip.length.GetHashCode());
			}
			return num;
		}

		private bool HasNotifications()
		{
			return m_Markers.HasNotifications();
		}

		private bool CanCompileNotifications()
		{
			if (supportsNotifications)
			{
				return m_Markers.HasNotifications();
			}
			return false;
		}

		private bool CanCreateMixerRecursive()
		{
			if (CanCreateTrackMixer())
			{
				return true;
			}
			foreach (global::UnityEngine.Timeline.TrackAsset childTrack in GetChildTracks())
			{
				if (childTrack.CanCreateMixerRecursive())
				{
					return true;
				}
			}
			return false;
		}
	}
}

namespace UnityEngine.Timeline
{
	public class TimelinePlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		private readonly struct TrackCacheManager : global::System.IDisposable
		{
			public readonly global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.AnimationTrack> trackCache;

			public TrackCacheManager(global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.AnimationTrack> cache, global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.Timeline.RuntimeElement> activeRuntimeElements)
			{
				trackCache = cache;
				GetTrackAssetsFromRuntimeElements(activeRuntimeElements);
			}

			public void Dispose()
			{
				trackCache.Clear();
			}

			private void GetTrackAssetsFromRuntimeElements(global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.Timeline.RuntimeElement> activeRuntimeElements)
			{
				for (int i = 0; i < activeRuntimeElements.Count; i++)
				{
					if (activeRuntimeElements[i] is global::UnityEngine.Timeline.RuntimeClip runtimeClip && runtimeClip.clip?.GetParentTrack() is global::UnityEngine.Timeline.AnimationTrack item)
					{
						trackCache.Add(item);
					}
				}
			}
		}

		private static global::Unity.Profiling.ProfilerMarker k_CreateTimelineGraphMarker = new global::Unity.Profiling.ProfilerMarker(global::Unity.Profiling.ProfilerCategory.Scripts, "Timeline.CreatePlayableGraph");

		private static global::Unity.Profiling.ProfilerMarker k_CreateTimelineTrackMarker = new global::Unity.Profiling.ProfilerMarker(global::Unity.Profiling.ProfilerCategory.Scripts, "Timeline.CreateTrackPlayable");

		private static global::Unity.Profiling.ProfilerMarker k_CreateTimelineTrackOutputsMarker = new global::Unity.Profiling.ProfilerMarker(global::Unity.Profiling.ProfilerCategory.Scripts, "Timeline.CreateTrackPlayableOutputs");

		private static global::Unity.Profiling.ProfilerMarker m_findActiveClipsMarker = new global::Unity.Profiling.ProfilerMarker(global::Unity.Profiling.ProfilerCategory.Scripts, "TimelinePlayable.GetActiveClips");

		private static global::Unity.Profiling.ProfilerMarker m_SetClipsLocalTimeMarker = new global::Unity.Profiling.ProfilerMarker(global::Unity.Profiling.ProfilerCategory.Scripts, "TimelinePlayable.SetActiveClipsTime");

		private global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement> m_IntervalTree = new global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement>();

		private global::System.Collections.Generic.List<global::UnityEngine.Timeline.RuntimeElement> m_ActiveClips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.RuntimeElement>();

		private global::System.Collections.Generic.List<global::UnityEngine.Timeline.RuntimeElement> m_CurrentListOfActiveClips;

		private int m_ActiveBit;

		private global::System.Collections.Generic.Dictionary<global::UnityEngine.Timeline.TrackAsset, global::UnityEngine.Playables.Playable> m_PlayableCache = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Timeline.TrackAsset, global::UnityEngine.Playables.Playable>();

		internal static bool muteAudioScrubbing = true;

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.Timeline.AnimationTrack, global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback>> m_EvaluateCallbacks = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Timeline.AnimationTrack, global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback>>();

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback> m_AlwaysEvaluateCallbacks = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback>();

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.ITimelineEvaluateCallback> m_ForceEvaluateNextEvaluate = new global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.ITimelineEvaluateCallback>();

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.ITimelineEvaluateCallback> m_InvokedThisFrame = new global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.ITimelineEvaluateCallback>();

		private readonly global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.AnimationTrack> m_ActiveTracksToEvaluateCache = new global::System.Collections.Generic.HashSet<global::UnityEngine.Timeline.AnimationTrack>();

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimelinePlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> tracks, global::UnityEngine.GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new global::System.ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new global::System.ArgumentNullException("GameObject parameter is null", "go");
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimelinePlayable> scriptPlayable = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimelinePlayable>.Create(graph);
			global::UnityEngine.Playables.PlayableExtensions.SetTraversalMode(scriptPlayable, global::UnityEngine.Playables.PlayableTraversalMode.Passthrough);
			scriptPlayable.GetBehaviour().Compile(graph, scriptPlayable, tracks, go, autoRebalance, createOutputs);
			return scriptPlayable;
		}

		public void Compile(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable timelinePlayable, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> tracks, global::UnityEngine.GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new global::System.ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new global::System.ArgumentNullException("GameObject parameter is null", "go");
			}
			global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset> list = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TrackAsset>(tracks);
			int capacity = list.Count * 2 + list.Count;
			m_CurrentListOfActiveClips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.RuntimeElement>(capacity);
			m_ActiveClips = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.RuntimeElement>(capacity);
			m_EvaluateCallbacks.Clear();
			m_AlwaysEvaluateCallbacks.Clear();
			m_PlayableCache.Clear();
			CompileTrackList(graph, timelinePlayable, list, go, createOutputs);
		}

		private void CompileTrackList(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable timelinePlayable, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.TrackAsset> tracks, global::UnityEngine.GameObject go, bool createOutputs)
		{
			foreach (global::UnityEngine.Timeline.TrackAsset track in tracks)
			{
				if (track.IsCompilable() && !m_PlayableCache.ContainsKey(track))
				{
					track.SortClips();
					track.ComputeBlendsFromOverlaps();
					CreateTrackPlayable(graph, timelinePlayable, track, go, createOutputs);
				}
			}
		}

		private void CreateTrackOutput(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Timeline.TrackAsset track, global::UnityEngine.GameObject go, global::UnityEngine.Playables.Playable playable, int port)
		{
			if (track.isSubTrack)
			{
				return;
			}
			foreach (global::UnityEngine.Playables.PlayableBinding output in track.outputs)
			{
				global::UnityEngine.Playables.PlayableOutput playableOutput = output.CreateOutput(graph);
				global::UnityEngine.Playables.PlayableOutputExtensions.SetReferenceObject(playableOutput, output.sourceObject);
				global::UnityEngine.Playables.PlayableOutputExtensions.SetSourcePlayable(playableOutput, playable, port);
				global::UnityEngine.Playables.PlayableOutputExtensions.SetWeight(playableOutput, 1f);
				if (track is global::UnityEngine.Timeline.AnimationTrack track2)
				{
					AddPlayableOutputCallbacks(track2, playableOutput);
				}
				if (playableOutput.IsPlayableOutputOfType<global::UnityEngine.Audio.AudioPlayableOutput>())
				{
					((global::UnityEngine.Audio.AudioPlayableOutput)playableOutput).SetEvaluateOnSeek(!muteAudioScrubbing);
				}
				if (track.timelineAsset.markerTrack == track)
				{
					global::UnityEngine.Playables.PlayableDirector component = go.GetComponent<global::UnityEngine.Playables.PlayableDirector>();
					global::UnityEngine.Playables.PlayableOutputExtensions.SetUserData(playableOutput, component);
					global::UnityEngine.Playables.INotificationReceiver[] components = go.GetComponents<global::UnityEngine.Playables.INotificationReceiver>();
					foreach (global::UnityEngine.Playables.INotificationReceiver receiver in components)
					{
						global::UnityEngine.Playables.PlayableOutputExtensions.AddNotificationReceiver(playableOutput, receiver);
					}
				}
			}
		}

		private global::UnityEngine.Playables.Playable CreateTrackPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable timelinePlayable, global::UnityEngine.Timeline.TrackAsset track, global::UnityEngine.GameObject go, bool createOutputs)
		{
			if (!track.IsCompilable())
			{
				return timelinePlayable;
			}
			if (m_PlayableCache.TryGetValue(track, out var value))
			{
				return value;
			}
			if (track.name == "root")
			{
				return timelinePlayable;
			}
			global::UnityEngine.Timeline.TrackAsset trackAsset = track.parent as global::UnityEngine.Timeline.TrackAsset;
			global::UnityEngine.Playables.Playable playable = ((trackAsset != null) ? CreateTrackPlayable(graph, timelinePlayable, trackAsset, go, createOutputs) : timelinePlayable);
			global::UnityEngine.Playables.Playable playable2 = track.CreatePlayableGraph(graph, go, m_IntervalTree, timelinePlayable);
			bool flag = false;
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2))
			{
				throw new global::System.InvalidOperationException(track.name + "(" + track.GetType()?.ToString() + ") did not produce a valid playable.");
			}
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(playable) && global::UnityEngine.Playables.PlayableExtensions.IsValid(playable2))
			{
				int inputCount = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(playable);
				global::UnityEngine.Playables.PlayableExtensions.SetInputCount(playable, inputCount + 1);
				flag = graph.Connect(playable2, 0, playable, inputCount);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(playable, inputCount, 1f);
			}
			if (createOutputs && flag)
			{
				CreateTrackOutput(graph, track, go, playable, global::UnityEngine.Playables.PlayableExtensions.GetInputCount(playable) - 1);
			}
			CacheTrack(track, playable2);
			return playable2;
		}

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			Evaluate(playable, info);
		}

		private void Evaluate(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData frameData)
		{
			if (m_IntervalTree == null)
			{
				return;
			}
			double time = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
			m_ActiveBit = ((m_ActiveBit == 0) ? 1 : 0);
			m_CurrentListOfActiveClips.Clear();
			m_IntervalTree.IntersectsWith(global::UnityEngine.Timeline.DiscreteTime.GetNearestTick(time), m_CurrentListOfActiveClips);
			foreach (global::UnityEngine.Timeline.RuntimeElement currentListOfActiveClip in m_CurrentListOfActiveClips)
			{
				currentListOfActiveClip.intervalBit = m_ActiveBit;
			}
			double rootDuration = (double)new global::UnityEngine.Timeline.DiscreteTime(global::UnityEngine.Playables.PlayableExtensions.GetDuration(playable));
			foreach (global::UnityEngine.Timeline.RuntimeElement activeClip in m_ActiveClips)
			{
				if (activeClip.intervalBit != m_ActiveBit)
				{
					activeClip.DisableAt(time, rootDuration, frameData);
				}
			}
			m_ActiveClips.Clear();
			for (int i = 0; i < m_CurrentListOfActiveClips.Count; i++)
			{
				m_CurrentListOfActiveClips[i].EvaluateAt(time, frameData);
				m_ActiveClips.Add(m_CurrentListOfActiveClips[i]);
			}
			InvokeOutputCallbacks(m_CurrentListOfActiveClips);
		}

		private void CacheTrack(global::UnityEngine.Timeline.TrackAsset track, global::UnityEngine.Playables.Playable playable)
		{
			m_PlayableCache[track] = playable;
		}

		private static void ForAOTCompilationOnly()
		{
			new global::System.Collections.Generic.List<global::UnityEngine.Timeline.IntervalTree<global::UnityEngine.Timeline.RuntimeElement>.Entry>();
		}

		private void AddPlayableOutputCallbacks(global::UnityEngine.Timeline.AnimationTrack track, global::UnityEngine.Playables.PlayableOutput playableOutput)
		{
			AddOutputWeightProcessor(track, (global::UnityEngine.Animations.AnimationPlayableOutput)playableOutput);
		}

		private void AddOutputWeightProcessor(global::UnityEngine.Timeline.AnimationTrack track, global::UnityEngine.Animations.AnimationPlayableOutput animOutput)
		{
			global::UnityEngine.Timeline.AnimationOutputWeightProcessor animationOutputWeightProcessor = new global::UnityEngine.Timeline.AnimationOutputWeightProcessor(animOutput);
			if (track.inClipMode)
			{
				AddEvaluateCallback(track, animationOutputWeightProcessor);
			}
			else
			{
				m_AlwaysEvaluateCallbacks.Add(animationOutputWeightProcessor);
			}
			m_ForceEvaluateNextEvaluate.Add(animationOutputWeightProcessor);
		}

		private void AddEvaluateCallback(global::UnityEngine.Timeline.AnimationTrack track, global::UnityEngine.Timeline.ITimelineEvaluateCallback callback)
		{
			if (m_EvaluateCallbacks.TryGetValue(track, out var value))
			{
				value.Add(callback);
				return;
			}
			m_EvaluateCallbacks[track] = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback> { callback };
		}

		private void InvokeOutputCallbacks(global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.Timeline.RuntimeElement> activeRuntimeElements)
		{
			foreach (global::UnityEngine.Timeline.ITimelineEvaluateCallback item in m_ForceEvaluateNextEvaluate)
			{
				item.Evaluate();
				m_InvokedThisFrame.Add(item);
			}
			m_ForceEvaluateNextEvaluate.Clear();
			if (activeRuntimeElements.Count > 0)
			{
				global::UnityEngine.Timeline.TimelinePlayable.TrackCacheManager trackCacheManager = new global::UnityEngine.Timeline.TimelinePlayable.TrackCacheManager(m_ActiveTracksToEvaluateCache, activeRuntimeElements);
				try
				{
					foreach (global::UnityEngine.Timeline.AnimationTrack item2 in trackCacheManager.trackCache)
					{
						if (!TryGetCallbackList(item2, out var list))
						{
							continue;
						}
						foreach (global::UnityEngine.Timeline.ITimelineEvaluateCallback item3 in list)
						{
							if (!m_InvokedThisFrame.Contains(item3))
							{
								item3.Evaluate();
								m_InvokedThisFrame.Add(item3);
								m_ForceEvaluateNextEvaluate.Add(item3);
							}
						}
					}
				}
				finally
				{
					((global::System.IDisposable)trackCacheManager/*cast due to .constrained prefix*/).Dispose();
				}
			}
			else
			{
				foreach (global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback> value in m_EvaluateCallbacks.Values)
				{
					foreach (global::UnityEngine.Timeline.ITimelineEvaluateCallback item4 in value)
					{
						if (!m_InvokedThisFrame.Contains(item4))
						{
							item4.Evaluate();
						}
					}
				}
			}
			foreach (global::UnityEngine.Timeline.ITimelineEvaluateCallback alwaysEvaluateCallback in m_AlwaysEvaluateCallbacks)
			{
				alwaysEvaluateCallback.Evaluate();
			}
			m_InvokedThisFrame.Clear();
		}

		private bool TryGetCallbackList(global::UnityEngine.Timeline.AnimationTrack track, out global::System.Collections.Generic.List<global::UnityEngine.Timeline.ITimelineEvaluateCallback> list)
		{
			if (track == null)
			{
				list = null;
				return false;
			}
			if (m_EvaluateCallbacks.TryGetValue(track, out list))
			{
				return true;
			}
			return TryGetCallbackList(track.parent as global::UnityEngine.Timeline.AnimationTrack, out list);
		}
	}
}

namespace UnityEngine.Timeline
{
	public class DirectorControlPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		public enum PauseAction
		{
			StopDirector = 0,
			PauseDirector = 1
		}

		public global::UnityEngine.Playables.PlayableDirector director;

		public global::UnityEngine.Timeline.DirectorControlPlayable.PauseAction pauseAction;

		private bool m_SyncTime;

		private double m_AssetDuration = double.MaxValue;

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.DirectorControlPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.PlayableDirector director)
		{
			if (director == null)
			{
				return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.DirectorControlPlayable>.Null;
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.DirectorControlPlayable> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.DirectorControlPlayable>.Create(graph);
			result.GetBehaviour().director = director;
			return result;
		}

		public override void OnPlayableDestroy(global::UnityEngine.Playables.Playable playable)
		{
			if (director != null && director.playableAsset != null)
			{
				director.Stop();
			}
		}

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (!(director == null) && director.isActiveAndEnabled && !(director.playableAsset == null))
			{
				m_SyncTime |= info.evaluationType == global::UnityEngine.Playables.FrameData.EvaluationType.Evaluate || DetectDiscontinuity(playable, info);
				SyncSpeed(info.effectiveSpeed);
				SyncStart(global::UnityEngine.Playables.PlayableExtensions.GetGraph(playable), global::UnityEngine.Playables.PlayableExtensions.GetTime(playable));
			}
		}

		public override void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			m_SyncTime = true;
			if (director != null && director.playableAsset != null)
			{
				m_AssetDuration = director.playableAsset.duration;
			}
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (director != null && director.playableAsset != null)
			{
				if (info.effectivePlayState == global::UnityEngine.Playables.PlayState.Playing || (info.effectivePlayState == global::UnityEngine.Playables.PlayState.Paused && pauseAction == global::UnityEngine.Timeline.DirectorControlPlayable.PauseAction.PauseDirector))
				{
					director.Pause();
				}
				else
				{
					director.Stop();
				}
			}
		}

		public override void ProcessFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info, object playerData)
		{
			if (director == null || !director.isActiveAndEnabled || director.playableAsset == null)
			{
				return;
			}
			if (m_SyncTime || DetectOutOfSync(playable))
			{
				UpdateTime(playable);
				if (director.playableGraph.IsValid())
				{
					director.playableGraph.Evaluate();
					director.playableGraph.SynchronizeEvaluation(global::UnityEngine.Playables.PlayableExtensions.GetGraph(playable));
				}
				else
				{
					director.Evaluate();
				}
			}
			m_SyncTime = false;
			SyncStop(global::UnityEngine.Playables.PlayableExtensions.GetGraph(playable), global::UnityEngine.Playables.PlayableExtensions.GetTime(playable));
		}

		private void SyncSpeed(double speed)
		{
			if (!director.playableGraph.IsValid())
			{
				return;
			}
			int rootPlayableCount = director.playableGraph.GetRootPlayableCount();
			for (int i = 0; i < rootPlayableCount; i++)
			{
				global::UnityEngine.Playables.Playable rootPlayable = director.playableGraph.GetRootPlayable(i);
				if (global::UnityEngine.Playables.PlayableExtensions.IsValid(rootPlayable))
				{
					global::UnityEngine.Playables.PlayableExtensions.SetSpeed(rootPlayable, speed);
				}
			}
		}

		private void SyncStart(global::UnityEngine.Playables.PlayableGraph graph, double time)
		{
			if (director.state != global::UnityEngine.Playables.PlayState.Playing && graph.IsPlaying() && (director.extrapolationMode != global::UnityEngine.Playables.DirectorWrapMode.None || !(time > m_AssetDuration)))
			{
				if (graph.IsMatchFrameRateEnabled())
				{
					director.Play(graph.GetFrameRate());
				}
				else
				{
					director.Play();
				}
			}
		}

		private void SyncStop(global::UnityEngine.Playables.PlayableGraph graph, double time)
		{
			if (director.state != global::UnityEngine.Playables.PlayState.Paused && (!graph.IsPlaying() || (director.extrapolationMode == global::UnityEngine.Playables.DirectorWrapMode.None && !(time < m_AssetDuration))) && director.state != global::UnityEngine.Playables.PlayState.Paused && ((director.extrapolationMode == global::UnityEngine.Playables.DirectorWrapMode.None && time > m_AssetDuration) || !graph.IsPlaying()))
			{
				director.Pause();
			}
		}

		private bool DetectDiscontinuity(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			return global::System.Math.Abs(global::UnityEngine.Playables.PlayableExtensions.GetTime(playable) - global::UnityEngine.Playables.PlayableExtensions.GetPreviousTime(playable) - info.m_DeltaTime * (double)info.m_EffectiveSpeed) > global::UnityEngine.Timeline.DiscreteTime.tickValue;
		}

		private bool DetectOutOfSync(global::UnityEngine.Playables.Playable playable)
		{
			double num = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
			if (global::UnityEngine.Playables.PlayableExtensions.GetTime(playable) >= m_AssetDuration)
			{
				switch (director.extrapolationMode)
				{
				case global::UnityEngine.Playables.DirectorWrapMode.None:
					num = m_AssetDuration;
					break;
				case global::UnityEngine.Playables.DirectorWrapMode.Hold:
					num = m_AssetDuration;
					break;
				case global::UnityEngine.Playables.DirectorWrapMode.Loop:
					num %= m_AssetDuration;
					break;
				}
			}
			if (!global::UnityEngine.Mathf.Approximately((float)num, (float)director.time))
			{
				return true;
			}
			return false;
		}

		private void UpdateTime(global::UnityEngine.Playables.Playable playable)
		{
			double num = global::System.Math.Max(0.1, director.playableAsset.duration);
			switch (director.extrapolationMode)
			{
			case global::UnityEngine.Playables.DirectorWrapMode.Hold:
				director.time = global::System.Math.Min(num, global::System.Math.Max(0.0, global::UnityEngine.Playables.PlayableExtensions.GetTime(playable)));
				break;
			case global::UnityEngine.Playables.DirectorWrapMode.Loop:
				director.time = global::System.Math.Max(0.0, global::UnityEngine.Playables.PlayableExtensions.GetTime(playable) % num);
				break;
			case global::UnityEngine.Playables.DirectorWrapMode.None:
				director.time = global::System.Math.Min(num, global::System.Math.Max(0.0, global::UnityEngine.Playables.PlayableExtensions.GetTime(playable)));
				break;
			}
		}
	}
}

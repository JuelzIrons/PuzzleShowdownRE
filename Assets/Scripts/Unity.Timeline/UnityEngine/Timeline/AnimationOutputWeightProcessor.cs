namespace UnityEngine.Timeline
{
	internal class AnimationOutputWeightProcessor : global::UnityEngine.Timeline.ITimelineEvaluateCallback
	{
		private struct WeightInfo
		{
			public global::UnityEngine.Playables.Playable mixer;

			public global::UnityEngine.Playables.Playable parentMixer;

			public int port;
		}

		private global::UnityEngine.Animations.AnimationPlayableOutput m_Output;

		private global::UnityEngine.Animations.AnimationMotionXToDeltaPlayable m_MotionXPlayable;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo> m_Mixers = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo>();

		public AnimationOutputWeightProcessor(global::UnityEngine.Animations.AnimationPlayableOutput output)
		{
			m_Output = output;
			global::UnityEngine.Playables.PlayableOutputExtensions.SetWeight(output, 0f);
			FindMixers();
		}

		private void FindMixers()
		{
			global::UnityEngine.Playables.Playable sourcePlayable = global::UnityEngine.Playables.PlayableOutputExtensions.GetSourcePlayable(m_Output);
			int sourceOutputPort = global::UnityEngine.Playables.PlayableOutputExtensions.GetSourceOutputPort(m_Output);
			m_Mixers.Clear();
			FindMixers(sourcePlayable, sourceOutputPort, global::UnityEngine.Playables.PlayableExtensions.GetInput(sourcePlayable, sourceOutputPort));
		}

		private void FindMixers(global::UnityEngine.Playables.Playable parent, int port, global::UnityEngine.Playables.Playable node)
		{
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(node))
			{
				return;
			}
			global::System.Type playableType = node.GetPlayableType();
			if (playableType == typeof(global::UnityEngine.Animations.AnimationMixerPlayable) || playableType == typeof(global::UnityEngine.Animations.AnimationLayerMixerPlayable))
			{
				int inputCount = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(node);
				for (int i = 0; i < inputCount; i++)
				{
					FindMixers(node, i, global::UnityEngine.Playables.PlayableExtensions.GetInput(node, i));
				}
				global::UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo item = new global::UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo
				{
					parentMixer = parent,
					mixer = node,
					port = port
				};
				m_Mixers.Add(item);
			}
			else
			{
				int inputCount2 = global::UnityEngine.Playables.PlayableExtensions.GetInputCount(node);
				for (int j = 0; j < inputCount2; j++)
				{
					FindMixers(parent, port, global::UnityEngine.Playables.PlayableExtensions.GetInput(node, j));
				}
			}
		}

		public void Evaluate()
		{
			float num = 1f;
			global::UnityEngine.Playables.PlayableOutputExtensions.SetWeight(m_Output, 1f);
			for (int i = 0; i < m_Mixers.Count; i++)
			{
				global::UnityEngine.Timeline.AnimationOutputWeightProcessor.WeightInfo weightInfo = m_Mixers[i];
				num = global::UnityEngine.Timeline.WeightUtility.NormalizeMixer(weightInfo.mixer);
				global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(weightInfo.parentMixer, weightInfo.port, num);
			}
			if (global::UnityEngine.Application.isPlaying)
			{
				global::UnityEngine.Playables.PlayableOutputExtensions.SetWeight(m_Output, num);
			}
		}
	}
}

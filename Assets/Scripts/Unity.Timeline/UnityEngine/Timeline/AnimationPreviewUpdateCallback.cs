namespace UnityEngine.Timeline
{
	internal class AnimationPreviewUpdateCallback : global::UnityEngine.Timeline.ITimelineEvaluateCallback
	{
		private global::UnityEngine.Animations.AnimationPlayableOutput m_Output;

		private global::UnityEngine.Playables.PlayableGraph m_Graph;

		private global::System.Collections.Generic.List<global::UnityEngine.Animations.IAnimationWindowPreview> m_PreviewComponents;

		public AnimationPreviewUpdateCallback(global::UnityEngine.Animations.AnimationPlayableOutput output)
		{
			m_Output = output;
			global::UnityEngine.Playables.Playable sourcePlayable = global::UnityEngine.Playables.PlayableOutputExtensions.GetSourcePlayable(m_Output);
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(sourcePlayable))
			{
				m_Graph = global::UnityEngine.Playables.PlayableExtensions.GetGraph(sourcePlayable);
			}
		}

		public void Evaluate()
		{
			if (!m_Graph.IsValid())
			{
				return;
			}
			if (m_PreviewComponents == null)
			{
				FetchPreviewComponents();
			}
			foreach (global::UnityEngine.Animations.IAnimationWindowPreview previewComponent in m_PreviewComponents)
			{
				previewComponent?.UpdatePreviewGraph(m_Graph);
			}
		}

		private void FetchPreviewComponents()
		{
			m_PreviewComponents = new global::System.Collections.Generic.List<global::UnityEngine.Animations.IAnimationWindowPreview>();
			global::UnityEngine.Animator target = m_Output.GetTarget();
			if (!(target == null))
			{
				global::UnityEngine.GameObject gameObject = target.gameObject;
				m_PreviewComponents.AddRange(gameObject.GetComponents<global::UnityEngine.Animations.IAnimationWindowPreview>());
			}
		}
	}
}

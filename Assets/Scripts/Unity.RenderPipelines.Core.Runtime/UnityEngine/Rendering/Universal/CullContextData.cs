namespace UnityEngine.Rendering.Universal
{
	public class CullContextData : global::UnityEngine.Rendering.ContextItem
	{
		internal global::UnityEngine.Rendering.ScriptableRenderContext? m_RenderContext;

		public override void Reset()
		{
			m_RenderContext = null;
		}

		public void SetRenderContext(in global::UnityEngine.Rendering.ScriptableRenderContext renderContext)
		{
			m_RenderContext = renderContext;
		}

		public global::UnityEngine.Rendering.CullingResults Cull(ref global::UnityEngine.Rendering.ScriptableCullingParameters parameters)
		{
			if (!m_RenderContext.HasValue)
			{
				throw new global::System.InvalidOperationException("The ScriptableRenderContext member is not set.");
			}
			return m_RenderContext.Value.Cull(ref parameters);
		}

		public void CullShadowCasters(global::UnityEngine.Rendering.CullingResults cullingResults, global::UnityEngine.Rendering.ShadowCastersCullingInfos shadowCastersCullingInfos)
		{
			if (!m_RenderContext.HasValue)
			{
				throw new global::System.InvalidOperationException("The ScriptableRenderContext member is not set.");
			}
			m_RenderContext.Value.CullShadowCasters(cullingResults, shadowCastersCullingInfos);
		}
	}
}

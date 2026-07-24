namespace UnityEngine.Rendering.Sampling
{
	internal sealed class SamplingResources : global::System.IDisposable
	{
		internal enum ResourceType
		{
			BlueNoiseTextures = 1,
			SobolMatrices = 2,
			All = 3
		}

		private global::UnityEngine.Texture2D m_SobolScramblingTile;

		private global::UnityEngine.Texture2D m_SobolRankingTile;

		private global::UnityEngine.Texture2D m_SobolOwenScrambled256Samples;

		private global::UnityEngine.GraphicsBuffer m_SobolBuffer;

		public static readonly uint[] sobolMatrices = global::UnityEngine.Rendering.Sampling.SobolData.SobolMatrices;

		public static void Bind(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.Sampling.SamplingResources resources)
		{
			if (resources.m_SobolScramblingTile != null)
			{
				cmd.SetGlobalTexture(global::UnityEngine.Shader.PropertyToID("_SobolScramblingTile"), resources.m_SobolScramblingTile);
				cmd.SetGlobalTexture(global::UnityEngine.Shader.PropertyToID("_SobolRankingTile"), resources.m_SobolRankingTile);
				cmd.SetGlobalTexture(global::UnityEngine.Shader.PropertyToID("_SobolOwenScrambledSequence"), resources.m_SobolOwenScrambled256Samples);
			}
			if (resources.m_SobolBuffer != null)
			{
				cmd.SetGlobalBuffer("_SobolMatricesBuffer", resources.m_SobolBuffer);
			}
		}

		public void Dispose()
		{
			m_SobolBuffer?.Dispose();
		}
	}
}

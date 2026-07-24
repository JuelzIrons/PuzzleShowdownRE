namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawDBufferSystem : global::UnityEngine.Rendering.Universal.DecalDrawSystem
	{
		public DecalDrawDBufferSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
			: base("DecalDrawIntoDBufferSystem.Execute", entityManager)
		{
		}

		protected override int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexDBuffer;
		}
	}
}

namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawGBufferSystem : global::UnityEngine.Rendering.Universal.DecalDrawSystem
	{
		public DecalDrawGBufferSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
			: base("DecalDrawGBufferSystem.Execute", entityManager)
		{
		}

		protected override int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexGBuffer;
		}
	}
}

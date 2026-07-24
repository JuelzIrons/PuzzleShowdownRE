namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawScreenSpaceSystem : global::UnityEngine.Rendering.Universal.DecalDrawSystem
	{
		public DecalDrawScreenSpaceSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
			: base("DecalDrawScreenSpaceSystem.Execute", entityManager)
		{
		}

		protected override int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexScreenSpace;
		}
	}
}

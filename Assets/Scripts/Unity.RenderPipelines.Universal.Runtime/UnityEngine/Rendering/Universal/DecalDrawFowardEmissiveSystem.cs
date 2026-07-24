namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawFowardEmissiveSystem : global::UnityEngine.Rendering.Universal.DecalDrawSystem
	{
		public DecalDrawFowardEmissiveSystem(global::UnityEngine.Rendering.Universal.DecalEntityManager entityManager)
			: base("DecalDrawFowardEmissiveSystem.Execute", entityManager)
		{
		}

		protected override int GetPassIndex(global::UnityEngine.Rendering.Universal.DecalCachedChunk decalCachedChunk)
		{
			return decalCachedChunk.passIndexEmissive;
		}
	}
}

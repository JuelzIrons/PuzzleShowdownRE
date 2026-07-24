namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("")]
	public sealed class UnityOnParticleCollisionMessageListener : global::Unity.VisualScripting.MessageListener
	{
		private void OnParticleCollision(global::UnityEngine.GameObject other)
		{
			global::Unity.VisualScripting.EventBus.Trigger("OnParticleCollision", base.gameObject, other);
		}
	}
}

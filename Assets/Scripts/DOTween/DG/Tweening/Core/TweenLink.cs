namespace DG.Tweening.Core
{
	internal class TweenLink
	{
		public readonly global::UnityEngine.GameObject target;

		public readonly global::DG.Tweening.LinkBehaviour behaviour;

		public bool lastSeenActive;

		public TweenLink(global::UnityEngine.GameObject target, global::DG.Tweening.LinkBehaviour behaviour)
		{
			this.target = target;
			this.behaviour = behaviour;
			lastSeenActive = target.activeInHierarchy;
		}
	}
}

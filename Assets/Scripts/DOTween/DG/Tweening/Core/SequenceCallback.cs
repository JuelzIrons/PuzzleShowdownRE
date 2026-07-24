namespace DG.Tweening.Core
{
	internal class SequenceCallback : global::DG.Tweening.Core.ABSSequentiable
	{
		public SequenceCallback(float sequencedPosition, global::DG.Tweening.TweenCallback callback)
		{
			tweenType = global::DG.Tweening.TweenType.Callback;
			base.sequencedPosition = sequencedPosition;
			onStart = callback;
		}
	}
}

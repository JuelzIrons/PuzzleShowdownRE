namespace UnityEngine.Timeline
{
	internal abstract class RuntimeClipBase : global::UnityEngine.Timeline.RuntimeElement
	{
		public abstract double start { get; }

		public abstract double duration { get; }

		public override long intervalStart => global::UnityEngine.Timeline.DiscreteTime.GetNearestTick(start);

		public override long intervalEnd => global::UnityEngine.Timeline.DiscreteTime.GetNearestTick(start + duration);
	}
}

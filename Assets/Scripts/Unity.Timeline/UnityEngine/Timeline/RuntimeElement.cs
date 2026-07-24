namespace UnityEngine.Timeline
{
	internal abstract class RuntimeElement : global::UnityEngine.Timeline.IInterval
	{
		public abstract long intervalStart { get; }

		public abstract long intervalEnd { get; }

		public int intervalBit { get; set; }

		public abstract bool enable { set; }

		public abstract void EvaluateAt(double localTime, global::UnityEngine.Playables.FrameData frameData);

		public abstract void DisableAt(double localTime, double rootDuration, global::UnityEngine.Playables.FrameData frameData);
	}
}

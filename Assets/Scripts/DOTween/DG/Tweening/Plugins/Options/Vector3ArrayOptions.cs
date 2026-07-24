namespace DG.Tweening.Plugins.Options
{
	public struct Vector3ArrayOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public global::DG.Tweening.AxisConstraint axisConstraint;

		public bool snapping;

		internal float[] durations;

		public void Reset()
		{
			axisConstraint = global::DG.Tweening.AxisConstraint.None;
			snapping = false;
			durations = null;
		}
	}
}

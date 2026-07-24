namespace DG.Tweening.Plugins.Options
{
	public struct VectorOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public global::DG.Tweening.AxisConstraint axisConstraint;

		public bool snapping;

		public void Reset()
		{
			axisConstraint = global::DG.Tweening.AxisConstraint.None;
			snapping = false;
		}
	}
}

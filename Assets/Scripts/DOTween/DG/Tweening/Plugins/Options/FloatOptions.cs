namespace DG.Tweening.Plugins.Options
{
	public struct FloatOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public bool snapping;

		public void Reset()
		{
			snapping = false;
		}
	}
}

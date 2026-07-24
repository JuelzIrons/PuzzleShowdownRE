namespace DG.Tweening.Plugins.Options
{
	public struct ColorOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public bool alphaOnly;

		public void Reset()
		{
			alphaOnly = false;
		}
	}
}

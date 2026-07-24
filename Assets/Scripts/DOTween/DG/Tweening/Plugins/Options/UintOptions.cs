namespace DG.Tweening.Plugins.Options
{
	public struct UintOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public bool isNegativeChangeValue;

		public void Reset()
		{
			isNegativeChangeValue = false;
		}
	}
}

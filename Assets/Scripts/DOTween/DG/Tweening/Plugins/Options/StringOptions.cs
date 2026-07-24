namespace DG.Tweening.Plugins.Options
{
	public struct StringOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public bool richTextEnabled;

		public global::DG.Tweening.ScrambleMode scrambleMode;

		public char[] scrambledChars;

		internal int startValueStrippedLength;

		internal int changeValueStrippedLength;

		public void Reset()
		{
			richTextEnabled = false;
			scrambleMode = global::DG.Tweening.ScrambleMode.None;
			scrambledChars = null;
			startValueStrippedLength = (changeValueStrippedLength = 0);
		}
	}
}

namespace UnityEngine.Rendering.Universal
{
	public class UniversalPostProcessingData : global::UnityEngine.Rendering.ContextItem
	{
		public bool isEnabled;

		public global::UnityEngine.Rendering.Universal.ColorGradingMode gradingMode;

		public int lutSize;

		public bool useFastSRGBLinearConversion;

		public bool supportScreenSpaceLensFlare;

		public bool supportDataDrivenLensFlare;

		public override void Reset()
		{
			isEnabled = false;
			gradingMode = global::UnityEngine.Rendering.Universal.ColorGradingMode.LowDynamicRange;
			lutSize = 0;
			useFastSRGBLinearConversion = false;
			supportScreenSpaceLensFlare = false;
			supportDataDrivenLensFlare = false;
		}
	}
}

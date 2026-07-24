namespace UnityEngine.Rendering.Universal
{
	public struct PostProcessingData
	{
		private global::UnityEngine.Rendering.ContextContainer frameData;

		internal global::UnityEngine.Rendering.Universal.UniversalPostProcessingData universalPostProcessingData => frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>();

		public ref global::UnityEngine.Rendering.Universal.ColorGradingMode gradingMode => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().gradingMode;

		public ref int lutSize => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().lutSize;

		public ref bool useFastSRGBLinearConversion => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().useFastSRGBLinearConversion;

		public ref bool supportScreenSpaceLensFlare => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().supportScreenSpaceLensFlare;

		public ref bool supportDataDrivenLensFlare => ref frameData.Get<global::UnityEngine.Rendering.Universal.UniversalPostProcessingData>().supportDataDrivenLensFlare;

		internal PostProcessingData(global::UnityEngine.Rendering.ContextContainer frameData)
		{
			this.frameData = frameData;
		}
	}
}

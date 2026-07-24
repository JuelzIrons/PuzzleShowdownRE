namespace UnityEngine.Rendering
{
	internal struct ProbeVolumeShadingParameters
	{
		public float normalBias;

		public float viewBias;

		public bool scaleBiasByMinDistanceBetweenProbes;

		public float samplingNoise;

		public float weight;

		public global::UnityEngine.Rendering.APVLeakReductionMode leakReductionMode;

		public int frameIndexForNoise;

		public float reflNormalizationLowerClamp;

		public float reflNormalizationUpperClamp;

		public float skyOcclusionIntensity;

		public bool skyOcclusionShadingDirection;

		public int regionCount;

		public global::Unity.Mathematics.uint4 regionLayerMasks;

		public global::UnityEngine.Vector3 worldOffset;
	}
}

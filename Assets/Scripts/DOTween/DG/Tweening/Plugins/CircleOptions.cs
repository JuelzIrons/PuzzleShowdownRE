namespace DG.Tweening.Plugins
{
	public struct CircleOptions : global::DG.Tweening.Plugins.Options.IPlugOptions
	{
		public float endValueDegrees;

		public bool relativeCenter;

		public bool snapping;

		internal global::UnityEngine.Vector2 center;

		internal float radius;

		internal float startValueDegrees;

		internal bool initialized;

		public void Reset()
		{
			initialized = false;
			startValueDegrees = (endValueDegrees = 0f);
			relativeCenter = false;
			snapping = false;
		}

		public void Initialize(global::UnityEngine.Vector2 startValue, global::UnityEngine.Vector2 endValue)
		{
			initialized = true;
			center = endValue;
			if (relativeCenter)
			{
				center = startValue + center;
			}
			radius = global::UnityEngine.Vector2.Distance(center, startValue);
			global::UnityEngine.Vector2 vector = startValue - center;
			startValueDegrees = global::UnityEngine.Mathf.Atan2(vector.x, vector.y) * 57.29578f;
		}
	}
}

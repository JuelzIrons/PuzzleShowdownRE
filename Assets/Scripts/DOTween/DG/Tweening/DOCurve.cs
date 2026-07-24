namespace DG.Tweening
{
	public static class DOCurve
	{
		public static class CubicBezier
		{
			public static global::UnityEngine.Vector3 GetPointOnSegment(global::UnityEngine.Vector3 startPoint, global::UnityEngine.Vector3 startControlPoint, global::UnityEngine.Vector3 endPoint, global::UnityEngine.Vector3 endControlPoint, float factor)
			{
				float num = 1f - factor;
				float num2 = factor * factor;
				float num3 = num * num;
				float num4 = num3 * num;
				float num5 = num2 * factor;
				return num4 * startPoint + 3f * num3 * factor * startControlPoint + 3f * num * num2 * endControlPoint + num5 * endPoint;
			}

			public static global::UnityEngine.Vector3[] GetSegmentPointCloud(global::UnityEngine.Vector3 startPoint, global::UnityEngine.Vector3 startControlPoint, global::UnityEngine.Vector3 endPoint, global::UnityEngine.Vector3 endControlPoint, int resolution = 10)
			{
				if (resolution < 2)
				{
					resolution = 2;
				}
				global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[resolution];
				float num = 1f / (float)(resolution - 1);
				for (int i = 0; i < resolution; i++)
				{
					array[i] = GetPointOnSegment(startPoint, startControlPoint, endPoint, endControlPoint, num * (float)i);
				}
				return array;
			}

			public static void GetSegmentPointCloud(global::System.Collections.Generic.List<global::UnityEngine.Vector3> addToList, global::UnityEngine.Vector3 startPoint, global::UnityEngine.Vector3 startControlPoint, global::UnityEngine.Vector3 endPoint, global::UnityEngine.Vector3 endControlPoint, int resolution = 10)
			{
				if (resolution < 2)
				{
					resolution = 2;
				}
				float num = 1f / (float)(resolution - 1);
				for (int i = 0; i < resolution; i++)
				{
					addToList.Add(GetPointOnSegment(startPoint, startControlPoint, endPoint, endControlPoint, num * (float)i));
				}
			}
		}
	}
}

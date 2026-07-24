namespace UnityEngine.Timeline
{
	internal static class CurveEditUtility
	{
		public static global::UnityEngine.AnimationCurve CreateMatchingCurve(global::UnityEngine.AnimationCurve curve)
		{
			global::UnityEngine.Keyframe[] keys = curve.keys;
			for (int i = 0; i != keys.Length; i++)
			{
				if (!float.IsPositiveInfinity(keys[i].inTangent))
				{
					keys[i].inTangent = 0f - keys[i].inTangent;
				}
				if (!float.IsPositiveInfinity(keys[i].outTangent))
				{
					keys[i].outTangent = 0f - keys[i].outTangent;
				}
				keys[i].value = 1f - keys[i].value;
			}
			return new global::UnityEngine.AnimationCurve(keys);
		}
	}
}

namespace UnityEngine.Rendering
{
	public class KeyframeUtility
	{
		public static void ResetAnimationCurve(global::UnityEngine.AnimationCurve curve)
		{
			curve.ClearKeys();
		}

		private static global::UnityEngine.Keyframe LerpSingleKeyframe(global::UnityEngine.Keyframe lhs, global::UnityEngine.Keyframe rhs, float t)
		{
			return new global::UnityEngine.Keyframe
			{
				time = global::UnityEngine.Mathf.Lerp(lhs.time, rhs.time, t),
				value = global::UnityEngine.Mathf.Lerp(lhs.value, rhs.value, t),
				inTangent = global::UnityEngine.Mathf.Lerp(lhs.inTangent, rhs.inTangent, t),
				outTangent = global::UnityEngine.Mathf.Lerp(lhs.outTangent, rhs.outTangent, t),
				inWeight = global::UnityEngine.Mathf.Lerp(lhs.inWeight, rhs.inWeight, t),
				outWeight = global::UnityEngine.Mathf.Lerp(lhs.outWeight, rhs.outWeight, t),
				weightedMode = lhs.weightedMode
			};
		}

		private static global::UnityEngine.Keyframe GetKeyframeAndClampEdge([global::System.Diagnostics.CodeAnalysis.DisallowNull] global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> keys, int index)
		{
			int num = keys.Length - 1;
			if (index < 0 || index > num)
			{
				global::UnityEngine.Debug.LogWarning("Invalid index in GetKeyframeAndClampEdge. This is likely a bug.");
				return default(global::UnityEngine.Keyframe);
			}
			global::UnityEngine.Keyframe result = keys[index];
			if (index == 0)
			{
				result.inTangent = 0f;
			}
			if (index == num)
			{
				result.outTangent = 0f;
			}
			return result;
		}

		private static global::UnityEngine.Keyframe FetchKeyFromIndexClampEdge([global::System.Diagnostics.CodeAnalysis.DisallowNull] global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> keys, int index, float segmentStartTime, float segmentEndTime)
		{
			float time = global::UnityEngine.Mathf.Min(segmentStartTime, keys[0].time);
			float time2 = global::UnityEngine.Mathf.Max(segmentEndTime, keys[keys.Length - 1].time);
			float value = keys[0].value;
			float value2 = keys[keys.Length - 1].value;
			global::UnityEngine.Keyframe result;
			if (index < 0)
			{
				result = new global::UnityEngine.Keyframe(time, value, 0f, 0f);
			}
			else
			{
				if (index < keys.Length)
				{
					return GetKeyframeAndClampEdge(keys, index);
				}
				_ = keys[keys.Length - 1];
				result = new global::UnityEngine.Keyframe(time2, value2, 0f, 0f);
			}
			return result;
		}

		private static void EvalCurveSegmentAndDeriv(out float dstValue, out float dstDeriv, global::UnityEngine.Keyframe lhsKey, global::UnityEngine.Keyframe rhsKey, float desiredTime)
		{
			float num = global::UnityEngine.Mathf.Clamp(desiredTime, lhsKey.time, rhsKey.time);
			float num2 = global::UnityEngine.Mathf.Max(rhsKey.time - lhsKey.time, 0.0001f);
			float num3 = rhsKey.value - lhsKey.value;
			float num4 = 1f / num2;
			float num5 = num4 * num4;
			float outTangent = lhsKey.outTangent;
			float inTangent = rhsKey.inTangent;
			float num6 = outTangent * num2;
			float num7 = inTangent * num2;
			float num8 = (num6 + num7 - num3 - num3) * num5 * num4;
			float num9 = (num3 + num3 + num3 - num6 - num6 - num7) * num5;
			float num10 = outTangent;
			float value = lhsKey.value;
			float num11 = global::UnityEngine.Mathf.Clamp(num - lhsKey.time, 0f, num2);
			dstValue = num11 * (num11 * (num11 * num8 + num9) + num10) + value;
			dstDeriv = num11 * (3f * num11 * num8 + 2f * num9) + num10;
		}

		private static global::UnityEngine.Keyframe EvalKeyAtTime([global::System.Diagnostics.CodeAnalysis.DisallowNull] global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> keys, int lhsIndex, int rhsIndex, float startTime, float endTime, float currTime)
		{
			global::UnityEngine.Keyframe lhsKey = FetchKeyFromIndexClampEdge(keys, lhsIndex, startTime, endTime);
			global::UnityEngine.Keyframe rhsKey = FetchKeyFromIndexClampEdge(keys, rhsIndex, startTime, endTime);
			EvalCurveSegmentAndDeriv(out var dstValue, out var dstDeriv, lhsKey, rhsKey, currTime);
			return new global::UnityEngine.Keyframe(currTime, dstValue, dstDeriv, dstDeriv);
		}

		public static void InterpAnimationCurve(ref global::UnityEngine.AnimationCurve lhsAndResultCurve, [global::System.Diagnostics.CodeAnalysis.DisallowNull] global::UnityEngine.AnimationCurve rhsCurve, float t)
		{
			if (t <= 0f || rhsCurve.length == 0)
			{
				return;
			}
			if (t >= 1f || lhsAndResultCurve.length == 0)
			{
				lhsAndResultCurve.CopyFrom(rhsCurve);
				return;
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> keys = new global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe>(lhsAndResultCurve.length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> keys2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe>(rhsCurve.length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < lhsAndResultCurve.length; i++)
			{
				keys[i] = lhsAndResultCurve[i];
			}
			for (int j = 0; j < rhsCurve.length; j++)
			{
				keys2[j] = rhsCurve[j];
			}
			float startTime = global::UnityEngine.Mathf.Min(keys[0].time, keys2[0].time);
			float endTime = global::UnityEngine.Mathf.Max(keys[lhsAndResultCurve.length - 1].time, keys2[rhsCurve.length - 1].time);
			int length = lhsAndResultCurve.length + rhsCurve.length;
			int num = 0;
			global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Keyframe>(length, global::Unity.Collections.Allocator.Temp);
			int num2 = 0;
			int num3 = 0;
			while (num2 < keys.Length || num3 < keys2.Length)
			{
				bool flag = num2 < keys.Length;
				bool flag2 = num3 < keys2.Length;
				global::UnityEngine.Keyframe keyframe = default(global::UnityEngine.Keyframe);
				global::UnityEngine.Keyframe keyframe2 = default(global::UnityEngine.Keyframe);
				if (flag && flag2)
				{
					keyframe = GetKeyframeAndClampEdge(keys, num2);
					keyframe2 = GetKeyframeAndClampEdge(keys2, num3);
					if (keyframe.time == keyframe2.time)
					{
						num2++;
						num3++;
					}
					else if (keyframe.time < keyframe2.time)
					{
						keyframe2 = EvalKeyAtTime(keys2, num3 - 1, num3, startTime, endTime, keyframe.time);
						num2++;
					}
					else
					{
						keyframe = EvalKeyAtTime(keys, num2 - 1, num2, startTime, endTime, keyframe2.time);
						num3++;
					}
				}
				else if (flag)
				{
					keyframe = GetKeyframeAndClampEdge(keys, num2);
					keyframe2 = EvalKeyAtTime(keys2, num3 - 1, num3, startTime, endTime, keyframe.time);
					num2++;
				}
				else
				{
					keyframe2 = GetKeyframeAndClampEdge(keys2, num3);
					keyframe = EvalKeyAtTime(keys, num2 - 1, num2, startTime, endTime, keyframe2.time);
					num3++;
				}
				global::UnityEngine.Keyframe value = LerpSingleKeyframe(keyframe, keyframe2, t);
				nativeArray[num] = value;
				num++;
			}
			ResetAnimationCurve(lhsAndResultCurve);
			for (int k = 0; k < num; k++)
			{
				lhsAndResultCurve.AddKey(nativeArray[k]);
			}
			nativeArray.Dispose();
		}
	}
}

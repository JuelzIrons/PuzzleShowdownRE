namespace UnityEngine.U2D
{
	public static class BezierUtility
	{
		private static global::UnityEngine.Vector3[] s_TempPoints = new global::UnityEngine.Vector3[3];

		public static global::UnityEngine.Vector3 BezierPoint(global::UnityEngine.Vector3 startRightTangent, global::UnityEngine.Vector3 startPosition, global::UnityEngine.Vector3 endPosition, global::UnityEngine.Vector3 endLeftTangent, float t)
		{
			float num = 1f - t;
			float num2 = 3f * num * t;
			return num * num * num * startPosition + num2 * num * startRightTangent + num2 * t * endLeftTangent + t * t * t * endPosition;
		}

		internal static float GetSpritePixelWidth(global::UnityEngine.Sprite sprite)
		{
			global::Unity.Mathematics.float4 float5 = new global::Unity.Mathematics.float4(sprite.pixelsPerUnit, sprite.pivot.y / sprite.textureRect.height, sprite.rect.width, sprite.rect.height);
			global::Unity.Mathematics.float4 float6 = new global::Unity.Mathematics.float4(sprite.border.x, sprite.border.y, sprite.border.z, sprite.border.w);
			float num = 1f / float5.x;
			global::Unity.Mathematics.float2 obj = new global::Unity.Mathematics.float2(float5.z, float5.w) * num;
			float6 *= num;
			float x = float6.x;
			return obj.x - float6.z - x;
		}

		internal static float BezierLength(global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapePoints, int splineDetail, ref float smallestSegment)
		{
			int num = shapePoints.Length - 1;
			float num2 = 0f;
			float num3 = splineDetail - 1;
			for (int i = 0; i < num; i++)
			{
				int index = i + 1;
				global::UnityEngine.U2D.ShapeControlPoint shapeControlPoint = shapePoints[i];
				global::UnityEngine.U2D.ShapeControlPoint shapeControlPoint2 = shapePoints[index];
				global::UnityEngine.Vector3 position = shapeControlPoint.position;
				global::UnityEngine.Vector3 position2 = shapeControlPoint2.position;
				global::UnityEngine.Vector3 vector = position;
				global::UnityEngine.Vector3 startRightTangent = position + shapeControlPoint.rightTangent;
				global::UnityEngine.Vector3 endLeftTangent = position2 + shapeControlPoint2.leftTangent;
				for (int j = 1; j < splineDetail; j++)
				{
					float t = (float)j / num3;
					global::UnityEngine.Vector3 vector2 = BezierPoint(startRightTangent, position, position2, endLeftTangent, t);
					float num4 = global::Unity.Mathematics.math.distance(vector2, vector);
					num2 += num4;
					vector = vector2;
				}
			}
			float num5 = num3 * (float)num;
			float x = num2 / (num5 * 1.08f);
			smallestSegment = global::Unity.Mathematics.math.min(x, smallestSegment);
			return num2;
		}

		internal static global::UnityEngine.Vector3 ClosestPointOnCurve(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 startPosition, global::UnityEngine.Vector3 endPosition, global::UnityEngine.Vector3 startTangent, global::UnityEngine.Vector3 endTangent, float sqrError, out float t)
		{
			global::UnityEngine.Vector3 v = endPosition - startPosition;
			global::UnityEngine.Vector3 v2 = startTangent - startPosition;
			global::UnityEngine.Vector3 v3 = endTangent - endPosition;
			if (Colinear(v2, v, sqrError) && Colinear(v3, v, sqrError))
			{
				return ClosestPointToSegment(point, startPosition, endPosition, out t);
			}
			float startT = 0f;
			float endT = 0.5f;
			float startT2 = 0.5f;
			float endT2 = 1f;
			SplitBezier(0.5f, startPosition, endPosition, startTangent, endTangent, out var leftStartPosition, out var leftEndPosition, out var leftStartTangent, out var leftEndTangent, out var rightStartPosition, out var rightEndPosition, out var rightStartTangent, out var rightEndTangent);
			global::UnityEngine.Vector3 vector = ClosestPointOnCurveIterative(point, leftStartPosition, leftEndPosition, leftStartTangent, leftEndTangent, sqrError, ref startT, ref endT);
			global::UnityEngine.Vector3 vector2 = ClosestPointOnCurveIterative(point, rightStartPosition, rightEndPosition, rightStartTangent, rightEndTangent, sqrError, ref startT2, ref endT2);
			if ((point - vector).sqrMagnitude < (point - vector2).sqrMagnitude)
			{
				t = startT;
				return vector;
			}
			t = startT2;
			return vector2;
		}

		internal static global::UnityEngine.Vector3 ClosestPointOnCurveFast(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 startPosition, global::UnityEngine.Vector3 endPosition, global::UnityEngine.Vector3 startTangent, global::UnityEngine.Vector3 endTangent, float sqrError, out float t)
		{
			float startT = 0f;
			float endT = 1f;
			global::UnityEngine.Vector3 result = ClosestPointOnCurveIterative(point, startPosition, endPosition, startTangent, endTangent, sqrError, ref startT, ref endT);
			t = startT;
			return result;
		}

		private static global::UnityEngine.Vector3 ClosestPointOnCurveIterative(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 startPosition, global::UnityEngine.Vector3 endPosition, global::UnityEngine.Vector3 startTangent, global::UnityEngine.Vector3 endTangent, float sqrError, ref float startT, ref float endT)
		{
			while ((startPosition - endPosition).sqrMagnitude > sqrError)
			{
				global::UnityEngine.Vector3 v = endPosition - startPosition;
				global::UnityEngine.Vector3 v2 = startTangent - startPosition;
				global::UnityEngine.Vector3 v3 = endTangent - endPosition;
				if (Colinear(v2, v, sqrError) && Colinear(v3, v, sqrError))
				{
					float t;
					global::UnityEngine.Vector3 result = ClosestPointToSegment(point, startPosition, endPosition, out t);
					t *= endT - startT;
					startT += t;
					endT -= t;
					return result;
				}
				SplitBezier(0.5f, startPosition, endPosition, startTangent, endTangent, out var leftStartPosition, out var leftEndPosition, out var leftStartTangent, out var leftEndTangent, out var rightStartPosition, out var rightEndPosition, out var rightStartTangent, out var rightEndTangent);
				s_TempPoints[0] = leftStartPosition;
				s_TempPoints[1] = leftStartTangent;
				s_TempPoints[2] = leftEndTangent;
				float num = SqrDistanceToPolyLine(point, s_TempPoints);
				s_TempPoints[0] = rightEndPosition;
				s_TempPoints[1] = rightEndTangent;
				s_TempPoints[2] = rightStartTangent;
				float num2 = SqrDistanceToPolyLine(point, s_TempPoints);
				if (num < num2)
				{
					startPosition = leftStartPosition;
					endPosition = leftEndPosition;
					startTangent = leftStartTangent;
					endTangent = leftEndTangent;
					endT -= (endT - startT) * 0.5f;
				}
				else
				{
					startPosition = rightStartPosition;
					endPosition = rightEndPosition;
					startTangent = rightStartTangent;
					endTangent = rightEndTangent;
					startT += (endT - startT) * 0.5f;
				}
			}
			return endPosition;
		}

		internal static void SplitBezier(float t, global::UnityEngine.Vector3 startPosition, global::UnityEngine.Vector3 endPosition, global::UnityEngine.Vector3 startRightTangent, global::UnityEngine.Vector3 endLeftTangent, out global::UnityEngine.Vector3 leftStartPosition, out global::UnityEngine.Vector3 leftEndPosition, out global::UnityEngine.Vector3 leftStartTangent, out global::UnityEngine.Vector3 leftEndTangent, out global::UnityEngine.Vector3 rightStartPosition, out global::UnityEngine.Vector3 rightEndPosition, out global::UnityEngine.Vector3 rightStartTangent, out global::UnityEngine.Vector3 rightEndTangent)
		{
			global::UnityEngine.Vector3 vector = startRightTangent - startPosition;
			global::UnityEngine.Vector3 vector2 = endLeftTangent - endPosition;
			global::UnityEngine.Vector3 vector3 = endLeftTangent - startRightTangent;
			global::UnityEngine.Vector3 vector4 = startPosition + vector * t;
			global::UnityEngine.Vector3 vector5 = endPosition + vector2 * (1f - t);
			global::UnityEngine.Vector3 vector6 = startRightTangent + vector3 * t;
			global::UnityEngine.Vector3 vector7 = vector4 + (vector6 - vector4) * t;
			global::UnityEngine.Vector3 vector8 = vector5 + (vector6 - vector5) * (1f - t);
			global::UnityEngine.Vector3 vector9 = vector8 - vector7;
			global::UnityEngine.Vector3 vector10 = vector7 + vector9 * t;
			leftStartPosition = startPosition;
			leftEndPosition = vector10;
			leftStartTangent = vector4;
			leftEndTangent = vector7;
			rightStartPosition = vector10;
			rightEndPosition = endPosition;
			rightStartTangent = vector8;
			rightEndTangent = vector5;
		}

		internal static global::UnityEngine.Vector3 ClosestPointToSegment(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 segmentStart, global::UnityEngine.Vector3 segmentEnd, out float t)
		{
			global::UnityEngine.Vector3 lhs = point - segmentStart;
			global::UnityEngine.Vector3 vector = segmentEnd - segmentStart;
			global::UnityEngine.Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			float num = global::UnityEngine.Vector3.Dot(lhs, normalized);
			if (num <= 0f)
			{
				num = 0f;
			}
			else if (num >= magnitude)
			{
				num = magnitude;
			}
			t = num / magnitude;
			return segmentStart + vector * t;
		}

		private static float SqrDistanceToPolyLine(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3[] points)
		{
			float num = float.MaxValue;
			for (int i = 0; i < points.Length - 1; i++)
			{
				float num2 = SqrDistanceToSegment(point, points[i], points[i + 1]);
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num;
		}

		private static float SqrDistanceToSegment(global::UnityEngine.Vector3 point, global::UnityEngine.Vector3 segmentStart, global::UnityEngine.Vector3 segmentEnd)
		{
			global::UnityEngine.Vector3 lhs = point - segmentStart;
			global::UnityEngine.Vector3 vector = segmentEnd - segmentStart;
			global::UnityEngine.Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			float num = global::UnityEngine.Vector3.Dot(lhs, normalized);
			if (num <= 0f)
			{
				return (point - segmentStart).sqrMagnitude;
			}
			if (num >= magnitude)
			{
				return (point - segmentEnd).sqrMagnitude;
			}
			return global::UnityEngine.Vector3.Cross(lhs, normalized).sqrMagnitude;
		}

		private static bool Colinear(global::UnityEngine.Vector3 v1, global::UnityEngine.Vector3 v2, float error = 0.0001f)
		{
			return global::UnityEngine.Mathf.Abs(v1.x * v2.y - v1.y * v2.x + v1.x * v2.z - v1.z * v2.x + v1.y * v2.z - v1.z * v2.y) < error;
		}
	}
}

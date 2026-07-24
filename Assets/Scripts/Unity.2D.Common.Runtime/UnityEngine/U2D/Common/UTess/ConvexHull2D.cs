namespace UnityEngine.U2D.Common.UTess
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct ConvexHull2D
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		private struct F3Compare : global::System.Collections.Generic.IComparer<global::Unity.Mathematics.float3>
		{
			public int Compare(global::Unity.Mathematics.float3 x, global::Unity.Mathematics.float3 y)
			{
				if (x.x != y.x)
				{
					if (!(x.x < y.x))
					{
						return 1;
					}
					return -1;
				}
				return 0;
			}
		}

		private static readonly float kEpsilon = 1E-05f;

		private static float DistancePointToLine(global::Unity.Mathematics.float2 pq, global::Unity.Mathematics.float2 p0, global::Unity.Mathematics.float2 p1)
		{
			global::Unity.Mathematics.float2 float5 = p1 - p0;
			float num = global::Unity.Mathematics.math.dot(pq - p0, float5);
			if (num <= 0f)
			{
				return global::Unity.Mathematics.math.length(p0 - pq);
			}
			float num2 = global::Unity.Mathematics.math.dot(float5, float5);
			if (num2 <= num)
			{
				return global::Unity.Mathematics.math.length(p1 - pq);
			}
			float num3 = num / num2;
			return global::Unity.Mathematics.math.length(p0 + float5 * num3 - pq);
		}

		private static float Sign(global::Unity.Mathematics.float2 p1, global::Unity.Mathematics.float2 p2, global::Unity.Mathematics.float2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		private static bool PointInTriangle(global::Unity.Mathematics.float2 pt, global::Unity.Mathematics.float2 v1, global::Unity.Mathematics.float2 v2, global::Unity.Mathematics.float2 v3)
		{
			float num = Sign(pt, v1, v2);
			float num2 = Sign(pt, v2, v3);
			float num3 = Sign(pt, v3, v1);
			bool flag = num < 0f || num2 < 0f || num3 < 0f;
			bool flag2 = num > 0f || num2 > 0f || num3 > 0f;
			return !(flag && flag2);
		}

		private static void FetchPointsOutsideTriangle(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> input, int inputCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output, ref int outputCount, global::Unity.Mathematics.float2 lp, global::Unity.Mathematics.float2 p, global::Unity.Mathematics.float2 rp)
		{
			for (int i = 0; i < inputCount; i++)
			{
				if (!PointInTriangle(input[i], lp, p, rp))
				{
					output[outputCount++] = input[i];
				}
			}
		}

		private static void FetchPointsOnRight(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> input, int inputCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output, ref int outputCount, global::Unity.Mathematics.float2 l, global::Unity.Mathematics.float2 r)
		{
			global::Unity.Mathematics.float2 float5 = r - l;
			for (int i = 0; i < inputCount; i++)
			{
				global::Unity.Mathematics.float2 float6 = r - input[i];
				if (float5.x * float6.y - float5.y * float6.x > 0f)
				{
					output[outputCount++] = input[i];
				}
			}
		}

		private unsafe static void FetchPoints(global::Unity.Mathematics.float2* input, int inputCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> lp, ref int lpCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> rp, ref int rpCount, global::Unity.Mathematics.float2 l, global::Unity.Mathematics.float2 r)
		{
			global::Unity.Mathematics.float2 float5 = r - l;
			for (int i = 0; i < inputCount; i++)
			{
				global::Unity.Mathematics.float2 float6 = input[i];
				global::Unity.Mathematics.float2 float7 = r - float6;
				float num = float5.x * float7.y - float5.y * float7.x;
				if (num > 0f)
				{
					lp[lpCount++] = float6;
				}
				if (num < 0f)
				{
					rp[rpCount++] = float6;
				}
			}
		}

		private static void Generate(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output, ref int outputCount, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> input, int inputCount, global::Unity.Mathematics.float2 l, global::Unity.Mathematics.float2 r)
		{
			global::Unity.Mathematics.float2 p = new global::Unity.Mathematics.float2(l.x, l.y);
			global::Unity.Mathematics.float2 p2 = new global::Unity.Mathematics.float2(r.x, r.y);
			global::Unity.Mathematics.float2 float5 = new global::Unity.Mathematics.float2(0f, 0f);
			float num = 1E-05f;
			float num2 = num;
			float num3 = num;
			for (int i = 0; i < inputCount; i++)
			{
				num2 = DistancePointToLine(input[i], p, p2);
				if (num2 > num3)
				{
					float5 = input[i];
					num3 = num2;
				}
			}
			if (num3 != num)
			{
				output[outputCount++] = float5;
				int outputCount2 = 0;
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(inputCount, global::Unity.Collections.Allocator.Temp);
				FetchPointsOutsideTriangle(ref input, inputCount, ref output2, ref outputCount2, l, float5, r);
				int outputCount3 = 0;
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output3 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(outputCount2, global::Unity.Collections.Allocator.Temp);
				FetchPointsOnRight(ref output2, outputCount2, ref output3, ref outputCount3, l, float5);
				if (outputCount3 != 0)
				{
					Generate(ref output, ref outputCount, ref output3, outputCount3, l, float5);
				}
				int outputCount4 = 0;
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output4 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(outputCount2, global::Unity.Collections.Allocator.Temp);
				FetchPointsOnRight(ref output2, outputCount2, ref output4, ref outputCount4, float5, r);
				if (outputCount4 != 0)
				{
					Generate(ref output, ref outputCount, ref output4, outputCount4, float5, r);
				}
				output4.Dispose();
				output3.Dispose();
				output2.Dispose();
			}
		}

		private unsafe static int CheckSide(global::Unity.Mathematics.float2* convex, int start, int end, global::Unity.Mathematics.float2 p, global::Unity.Mathematics.float2 d)
		{
			int num = 0;
			int num2 = 0;
			for (int i = start; i < end; i++)
			{
				global::Unity.Mathematics.float2 y = convex[i] - p;
				float num3 = global::Unity.Mathematics.math.dot(d, y);
				num = ((num3 > 0f) ? (num + 1) : num);
				num2 = ((num3 < 0f) ? (num2 + 1) : num2);
				if (num != 0 && num2 != 0)
				{
					return 0;
				}
			}
			if (num <= 0)
			{
				return -1;
			}
			return 1;
		}

		public unsafe static bool CheckCollisionSeparatingAxis(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> convex1_, int start1, int end1, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> convex2_, int start2, int end2)
		{
			global::Unity.Mathematics.float2* unsafeReadOnlyPtr = (global::Unity.Mathematics.float2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(convex1_);
			global::Unity.Mathematics.float2* unsafeReadOnlyPtr2 = (global::Unity.Mathematics.float2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(convex2_);
			int num = start1;
			int num2 = end1 - 1;
			while (num < end1)
			{
				global::Unity.Mathematics.float2 p = unsafeReadOnlyPtr[num];
				global::Unity.Mathematics.float2 float5 = unsafeReadOnlyPtr[num] - unsafeReadOnlyPtr[num2];
				float5 = new global::Unity.Mathematics.float2(float5.y, 0f - float5.x);
				if (CheckSide(unsafeReadOnlyPtr2, start2, end2, p, float5) > 0)
				{
					return false;
				}
				num2 = num++;
			}
			int num3 = start2;
			int num4 = end2 - 1;
			while (num3 < end2)
			{
				global::Unity.Mathematics.float2 p2 = unsafeReadOnlyPtr2[num3];
				global::Unity.Mathematics.float2 float6 = unsafeReadOnlyPtr2[num3] - unsafeReadOnlyPtr2[num4];
				float6 = new global::Unity.Mathematics.float2(float6.y, 0f - float6.x);
				if (CheckSide(unsafeReadOnlyPtr, start1, end1, p2, float6) > 0)
				{
					return false;
				}
				num4 = num3++;
			}
			return true;
		}

		internal static bool LineLineIntersection(global::Unity.Mathematics.float2 p1, global::Unity.Mathematics.float2 p2, global::Unity.Mathematics.float2 p3, global::Unity.Mathematics.float2 p4, ref global::Unity.Mathematics.float2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (global::Unity.Mathematics.math.abs(num5) < kEpsilon)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 >= 0f - kEpsilon && num8 <= 1f + kEpsilon)
			{
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				return true;
			}
			return false;
		}

		public unsafe static global::Unity.Mathematics.float3 Generate(ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> result, ref global::Unity.Mathematics.float4 aabb, ref int pointCount, int seed, global::UnityEngine.Vector2* vertexInput, int vertexCount, float extrude)
		{
			global::Unity.Mathematics.float2* unsafePtr = (global::Unity.Mathematics.float2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(result);
			global::Unity.Mathematics.float2 zero = global::Unity.Mathematics.float2.zero;
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			global::Unity.Mathematics.float3 zero2 = global::Unity.Mathematics.float3.zero;
			global::Unity.Mathematics.float2 float5 = default(global::Unity.Mathematics.float2);
			global::Unity.Mathematics.float2 float6 = default(global::Unity.Mathematics.float2);
			float5.x = (float6.y = float.MaxValue);
			global::Unity.Mathematics.float2 float7 = default(global::Unity.Mathematics.float2);
			global::Unity.Mathematics.float2 float8 = default(global::Unity.Mathematics.float2);
			float7.x = (float8.y = float.MinValue);
			float5.y = (float7.y = (float8.x = (float6.x = 0f)));
			int outputCount = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> output = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(vertexCount, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < vertexCount; i++)
			{
				float5 = ((float5.x > ((global::Unity.Mathematics.float2*)vertexInput)[i].x) ? ((global::Unity.Mathematics.float2*)vertexInput)[i] : float5);
				float7 = ((float7.x < ((global::Unity.Mathematics.float2*)vertexInput)[i].x) ? ((global::Unity.Mathematics.float2*)vertexInput)[i] : float7);
				float6 = ((float6.y > ((global::Unity.Mathematics.float2*)vertexInput)[i].y) ? ((global::Unity.Mathematics.float2*)vertexInput)[i] : float6);
				float8 = ((float8.y > ((global::Unity.Mathematics.float2*)vertexInput)[i].y) ? ((global::Unity.Mathematics.float2*)vertexInput)[i] : float8);
			}
			output[outputCount++] = float5;
			output[outputCount++] = float7;
			int lpCount = 0;
			int rpCount = 0;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> lp = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(vertexCount, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> rp = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(vertexCount, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			FetchPoints((global::Unity.Mathematics.float2*)vertexInput, vertexCount, ref lp, ref lpCount, ref rp, ref rpCount, float5, float7);
			if (lpCount != 0)
			{
				Generate(ref output, ref outputCount, ref lp, lpCount, float5, float7);
			}
			if (rpCount != 0)
			{
				Generate(ref output, ref outputCount, ref rp, rpCount, float7, float5);
			}
			if (outputCount >= 3)
			{
				global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>(outputCount, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
				global::Unity.Mathematics.float3* unsafePtr2 = (global::Unity.Mathematics.float3*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
				global::Unity.Mathematics.float2 float9 = float7 - float5;
				for (int j = 0; j < outputCount; j++)
				{
					global::Unity.Mathematics.float3 xyx = output[j].xyx;
					xyx.z = 0f;
					if (j > 1)
					{
						global::Unity.Mathematics.float2 float10 = float7 - xyx.xy;
						xyx.z = float9.x * float10.y - float9.y * float10.x;
					}
					unsafePtr2[j] = xyx;
				}
				global::UnityEngine.U2D.Common.UTess.ModuleHandle.InsertionSort<global::Unity.Mathematics.float3, global::UnityEngine.U2D.Common.UTess.ConvexHull2D.F3Compare>(unsafePtr2, 0, outputCount - 1, default(global::UnityEngine.U2D.Common.UTess.ConvexHull2D.F3Compare));
				unsafePtr[pointCount] = float5;
				zero += unsafePtr[pointCount++];
				for (int k = 0; k < outputCount; k++)
				{
					if (unsafePtr2[k].z > 0f)
					{
						unsafePtr[pointCount] = unsafePtr2[k].xy;
						zero += unsafePtr[pointCount++];
					}
				}
				unsafePtr[pointCount] = float7;
				zero += unsafePtr[pointCount++];
				for (int num3 = outputCount - 1; num3 > 0; num3--)
				{
					if (unsafePtr2[num3].z < 0f)
					{
						unsafePtr[pointCount] = unsafePtr2[num3].xy;
						zero += unsafePtr[pointCount++];
					}
				}
				zero /= (float)pointCount;
				unsafePtr[pointCount++] = float5;
				float5.x = (float6.y = float.MaxValue);
				float7.x = (float8.y = float.MinValue);
				float5.y = (float7.y = (float8.x = (float6.x = 0f)));
				for (int l = 0; l < pointCount; l++)
				{
					global::Unity.Mathematics.float2 float11 = unsafePtr[l];
					global::Unity.Mathematics.float2 float12 = global::Unity.Mathematics.math.normalizesafe(float11 - zero);
					unsafePtr[l] = zero + float12 * (global::Unity.Mathematics.math.length(float11 - zero) + extrude);
					float5 = ((float5.x > unsafePtr[l].x) ? unsafePtr[l] : float5);
					float7 = ((float7.x < unsafePtr[l].x) ? unsafePtr[l] : float7);
					float6 = ((float6.y > unsafePtr[l].y) ? unsafePtr[l] : float6);
					float8 = ((float8.y < unsafePtr[l].y) ? unsafePtr[l] : float8);
				}
				num = float5.x;
				num2 = float6.y;
				zero2.x = float7.x - float5.x;
				zero2.y = float8.y - float6.y;
				zero = new global::Unity.Mathematics.float2(zero.x - num, zero.y - num2);
				float num4 = 9999999f;
				float num5 = 9999999f;
				float num6 = -9999999f;
				float num7 = -9999999f;
				for (int m = 0; m < pointCount; m++)
				{
					global::Unity.Mathematics.float2 float13 = (unsafePtr[m] = new global::Unity.Mathematics.float2((int)global::Unity.Mathematics.math.floor(unsafePtr[m].x - num), (int)global::Unity.Mathematics.math.floor(unsafePtr[m].y - num2)));
					num4 = ((float13.x < num4) ? float13.x : num4);
					num6 = ((float13.x > num6) ? float13.x : num6);
					num5 = ((float13.y < num5) ? float13.y : num5);
					num7 = ((float13.y > num7) ? float13.y : num7);
					if (m != 0)
					{
						zero2.z += global::UnityEngine.U2D.Common.UTess.ModuleHandle.TriangleArea(unsafePtr[m], zero, unsafePtr[m - 1]);
					}
				}
				aabb.z = num6 - num4 / 2f;
				aabb.w = num7 - num5 / 2f;
				aabb.x = num4 + aabb.z;
				aabb.y = num5 + aabb.w;
				nativeArray.Dispose();
			}
			else
			{
				global::UnityEngine.Debug.Log("[failed to generate convex hull2d]");
			}
			rp.Dispose();
			lp.Dispose();
			output.Dispose();
			return zero2;
		}
	}
}

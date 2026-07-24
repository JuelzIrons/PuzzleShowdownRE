namespace UnityEngine.Rendering
{
	internal struct ReceiverPlanes
	{
		public global::Unity.Collections.NativeList<global::UnityEngine.Plane> planes;

		public int lightFacingPlaneCount;

		private static bool IsSignBitSet(float x)
		{
			return global::Unity.Mathematics.math.asuint(x) >> 31 != 0;
		}

		internal global::Unity.Collections.NativeArray<global::UnityEngine.Plane> LightFacingFrustumPlaneSubArray()
		{
			return planes.AsArray().GetSubArray(0, lightFacingPlaneCount);
		}

		internal global::Unity.Collections.NativeArray<global::UnityEngine.Plane> SilhouettePlaneSubArray()
		{
			return planes.AsArray().GetSubArray(lightFacingPlaneCount, planes.Length - lightFacingPlaneCount);
		}

		internal static global::UnityEngine.Rendering.ReceiverPlanes CreateEmptyForTesting(global::Unity.Collections.Allocator allocator)
		{
			return new global::UnityEngine.Rendering.ReceiverPlanes
			{
				planes = new global::Unity.Collections.NativeList<global::UnityEngine.Plane>(allocator),
				lightFacingPlaneCount = 0
			};
		}

		internal void Dispose(global::Unity.Jobs.JobHandle job)
		{
			planes.Dispose(job);
		}

		internal static global::UnityEngine.Rendering.ReceiverPlanes Create(in global::UnityEngine.Rendering.BatchCullingContext cc, global::Unity.Collections.Allocator allocator)
		{
			global::UnityEngine.Rendering.ReceiverPlanes result = new global::UnityEngine.Rendering.ReceiverPlanes
			{
				planes = new global::Unity.Collections.NativeList<global::UnityEngine.Plane>(allocator),
				lightFacingPlaneCount = 0
			};
			if (cc.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Light && cc.receiverPlaneCount != 0)
			{
				bool flag = false;
				if (cc.cullingSplits.Length > 0)
				{
					global::UnityEngine.Matrix4x4 cullingMatrix = cc.cullingSplits[0].cullingMatrix;
					flag = cullingMatrix[15] == 1f && cullingMatrix[11] == 0f && cullingMatrix[7] == 0f && cullingMatrix[3] == 0f;
				}
				if (flag)
				{
					global::UnityEngine.Vector3 vector = -cc.localToWorldMatrix.GetColumn(2);
					int num = 0;
					for (int i = 0; i < cc.receiverPlaneCount; i++)
					{
						global::UnityEngine.Plane value = cc.cullingPlanes[cc.receiverPlaneOffset + i];
						if (IsSignBitSet(global::UnityEngine.Vector3.Dot(value.normal, vector)))
						{
							num |= 1 << i;
						}
						else
						{
							result.planes.Add(in value);
						}
					}
					result.lightFacingPlaneCount = result.planes.Length;
					if (cc.receiverPlaneCount == 6)
					{
						for (int j = 0; j < cc.receiverPlaneCount; j++)
						{
							for (int k = j + 1; k < cc.receiverPlaneCount; k++)
							{
								if (j / 2 != k / 2 && (((num >> j) ^ (num >> k)) & 1) != 0)
								{
									int num4;
									int num5;
									if (((num >> j) & 1) != 0)
									{
										int num2 = k;
										int num3 = j;
										num4 = num2;
										num5 = num3;
									}
									else
									{
										int num6 = j;
										int num3 = k;
										num4 = num6;
										num5 = num3;
									}
									global::UnityEngine.Plane plane = cc.cullingPlanes[cc.receiverPlaneOffset + num4];
									global::UnityEngine.Plane plane2 = cc.cullingPlanes[cc.receiverPlaneOffset + num5];
									global::Unity.Mathematics.float4 a = new global::Unity.Mathematics.float4(plane.normal, plane.distance);
									global::Unity.Mathematics.float4 b = new global::Unity.Mathematics.float4(plane2.normal, plane2.distance);
									global::Unity.Mathematics.float4 x = global::UnityEngine.Rendering.Line.PlaneContainingLineWithNormalPerpendicularToVector(global::UnityEngine.Rendering.Line.LineOfPlaneIntersectingPlane(a, b), vector);
									x /= global::Unity.Mathematics.math.length(x.xyz);
									if (!global::Unity.Mathematics.math.any(global::Unity.Mathematics.math.isnan(x)))
									{
										result.planes.Add(new global::UnityEngine.Plane(x.xyz, x.w));
									}
								}
							}
						}
					}
				}
				else
				{
					global::UnityEngine.Vector3 position = cc.localToWorldMatrix.GetPosition();
					int num7 = 0;
					for (int l = 0; l < cc.receiverPlaneCount; l++)
					{
						global::UnityEngine.Plane value2 = cc.cullingPlanes[cc.receiverPlaneOffset + l];
						if (IsSignBitSet(value2.GetDistanceToPoint(position)))
						{
							num7 |= 1 << l;
						}
						else
						{
							result.planes.Add(in value2);
						}
					}
					result.lightFacingPlaneCount = result.planes.Length;
					if (cc.receiverPlaneCount == 6)
					{
						for (int m = 0; m < cc.receiverPlaneCount; m++)
						{
							for (int n = m + 1; n < cc.receiverPlaneCount; n++)
							{
								if (m / 2 != n / 2 && (((num7 >> m) ^ (num7 >> n)) & 1) != 0)
								{
									int num9;
									int num10;
									if (((num7 >> m) & 1) != 0)
									{
										int num8 = n;
										int num3 = m;
										num9 = num8;
										num10 = num3;
									}
									else
									{
										int num11 = m;
										int num3 = n;
										num9 = num11;
										num10 = num3;
									}
									global::UnityEngine.Plane plane3 = cc.cullingPlanes[cc.receiverPlaneOffset + num9];
									global::UnityEngine.Plane plane4 = cc.cullingPlanes[cc.receiverPlaneOffset + num10];
									global::Unity.Mathematics.float4 a2 = new global::Unity.Mathematics.float4(plane3.normal, plane3.distance);
									global::Unity.Mathematics.float4 b2 = new global::Unity.Mathematics.float4(plane4.normal, plane4.distance);
									global::Unity.Mathematics.float4 x2 = global::UnityEngine.Rendering.Line.PlaneContainingLineAndPoint(global::UnityEngine.Rendering.Line.LineOfPlaneIntersectingPlane(a2, b2), position);
									x2 /= global::Unity.Mathematics.math.length(x2.xyz);
									if (!global::Unity.Mathematics.math.any(global::Unity.Mathematics.math.isnan(x2)))
									{
										result.planes.Add(new global::UnityEngine.Plane(x2.xyz, x2.w));
									}
								}
							}
						}
					}
				}
			}
			return result;
		}
	}
}

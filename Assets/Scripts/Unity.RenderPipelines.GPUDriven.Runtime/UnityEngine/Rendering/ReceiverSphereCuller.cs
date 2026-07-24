namespace UnityEngine.Rendering
{
	internal struct ReceiverSphereCuller
	{
		internal struct SplitInfo
		{
			public global::Unity.Mathematics.float4 receiverSphereLightSpace;

			public float cascadeBlendCullingFactor;
		}

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo> splitInfos;

		public global::Unity.Mathematics.float3x3 worldToLightSpaceRotation;

		internal static global::UnityEngine.Rendering.ReceiverSphereCuller CreateEmptyForTesting(global::Unity.Collections.Allocator allocator)
		{
			return new global::UnityEngine.Rendering.ReceiverSphereCuller
			{
				splitInfos = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo>(0, allocator),
				worldToLightSpaceRotation = global::Unity.Mathematics.float3x3.identity
			};
		}

		internal void Dispose(global::Unity.Jobs.JobHandle job)
		{
			splitInfos.Dispose(job);
		}

		internal bool UseReceiverPlanes()
		{
			return splitInfos.Length == 0;
		}

		internal static global::UnityEngine.Rendering.ReceiverSphereCuller Create(in global::UnityEngine.Rendering.BatchCullingContext cc, global::Unity.Collections.Allocator allocator)
		{
			int num = cc.cullingSplits.Length;
			bool flag = num > 1;
			for (int i = 0; i < num; i++)
			{
				if (!(cc.cullingSplits[i].sphereRadius > 0f))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				num = 0;
			}
			global::Unity.Mathematics.float3x3 v = (global::Unity.Mathematics.float3x3)cc.localToWorldMatrix;
			global::UnityEngine.Rendering.ReceiverSphereCuller result = new global::UnityEngine.Rendering.ReceiverSphereCuller
			{
				splitInfos = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo>(num, allocator),
				worldToLightSpaceRotation = global::Unity.Mathematics.math.transpose(v)
			};
			result.splitInfos.ResizeUninitialized(num);
			for (int j = 0; j < num; j++)
			{
				global::UnityEngine.Rendering.CullingSplit cullingSplit = cc.cullingSplits[j];
				global::Unity.Mathematics.float4 receiverSphereLightSpace = new global::Unity.Mathematics.float4(global::Unity.Mathematics.math.mul(result.worldToLightSpaceRotation, cullingSplit.sphereCenter), cullingSplit.sphereRadius);
				result.splitInfos[j] = new global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo
				{
					receiverSphereLightSpace = receiverSphereLightSpace,
					cascadeBlendCullingFactor = cullingSplit.cascadeBlendCullingFactor
				};
			}
			return result;
		}

		internal static float DistanceUntilCylinderFullyCrossesPlane(global::Unity.Mathematics.float3 cylinderCenter, global::Unity.Mathematics.float3 cylinderDirection, float cylinderRadius, global::UnityEngine.Plane plane)
		{
			float y = 0.001f;
			float num = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.abs(global::Unity.Mathematics.math.dot(plane.normal, cylinderDirection)), y);
			float num2 = (global::Unity.Mathematics.math.dot(plane.normal, cylinderCenter) + plane.distance) / num;
			float num3 = global::Unity.Mathematics.math.sqrt(global::Unity.Mathematics.math.max(1f - num * num, 0f));
			float num4 = cylinderRadius * num3 / num;
			return num2 + num4;
		}

		internal static uint ComputeSplitVisibilityMask(global::Unity.Collections.NativeArray<global::UnityEngine.Plane> lightFacingFrustumPlanes, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo> splitInfos, global::Unity.Mathematics.float3x3 worldToLightSpaceRotation, in global::UnityEngine.Rendering.AABB bounds)
		{
			global::Unity.Mathematics.float3 center = bounds.center;
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.mul(worldToLightSpaceRotation, bounds.center);
			float num = global::Unity.Mathematics.math.length(bounds.extents);
			global::Unity.Mathematics.float3 c = global::Unity.Mathematics.math.transpose(worldToLightSpaceRotation).c2;
			float x = float.PositiveInfinity;
			for (int i = 0; i < lightFacingFrustumPlanes.Length; i++)
			{
				x = global::Unity.Mathematics.math.min(x, DistanceUntilCylinderFullyCrossesPlane(center, c, num, lightFacingFrustumPlanes[i]));
			}
			x = global::Unity.Mathematics.math.max(x, 0f);
			uint num2 = 0u;
			int length = splitInfos.Length;
			for (int j = 0; j < length; j++)
			{
				global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo splitInfo = splitInfos[j];
				global::Unity.Mathematics.float3 xyz = splitInfo.receiverSphereLightSpace.xyz;
				float w = splitInfo.receiverSphereLightSpace.w;
				global::Unity.Mathematics.float3 float6 = float5 - xyz;
				float num3 = global::Unity.Mathematics.math.lengthsq(num + w) - global::Unity.Mathematics.math.lengthsq(float6.xy);
				if (!(num3 < 0f) && (!(float6.z > 0f) || !(global::Unity.Mathematics.math.lengthsq(float6.z) > num3)))
				{
					num2 |= (uint)(1 << j);
					float num4 = w * splitInfo.cascadeBlendCullingFactor;
					global::Unity.Mathematics.float3 x2 = float6 + new global::Unity.Mathematics.float3(0f, 0f, x);
					float num5 = num4 - num;
					float num6 = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.lengthsq(float6), global::Unity.Mathematics.math.lengthsq(x2));
					if (num5 > 0f && num6 < global::Unity.Mathematics.math.lengthsq(num5))
					{
						break;
					}
				}
			}
			return num2;
		}
	}
}

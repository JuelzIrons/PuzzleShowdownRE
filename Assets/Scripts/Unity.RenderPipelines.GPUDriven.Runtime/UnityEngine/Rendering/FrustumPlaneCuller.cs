namespace UnityEngine.Rendering
{
	internal struct FrustumPlaneCuller
	{
		internal struct PlanePacket4
		{
			public global::Unity.Mathematics.float4 nx;

			public global::Unity.Mathematics.float4 ny;

			public global::Unity.Mathematics.float4 nz;

			public global::Unity.Mathematics.float4 d;

			public global::Unity.Mathematics.float4 nxAbs;

			public global::Unity.Mathematics.float4 nyAbs;

			public global::Unity.Mathematics.float4 nzAbs;

			public PlanePacket4(global::Unity.Collections.NativeArray<global::UnityEngine.Plane> planes, int offset, int limit)
			{
				global::UnityEngine.Plane plane = planes[global::UnityEngine.Mathf.Min(offset, limit)];
				global::UnityEngine.Plane plane2 = planes[global::UnityEngine.Mathf.Min(offset + 1, limit)];
				global::UnityEngine.Plane plane3 = planes[global::UnityEngine.Mathf.Min(offset + 2, limit)];
				global::UnityEngine.Plane plane4 = planes[global::UnityEngine.Mathf.Min(offset + 3, limit)];
				nx = new global::Unity.Mathematics.float4(plane.normal.x, plane2.normal.x, plane3.normal.x, plane4.normal.x);
				ny = new global::Unity.Mathematics.float4(plane.normal.y, plane2.normal.y, plane3.normal.y, plane4.normal.y);
				nz = new global::Unity.Mathematics.float4(plane.normal.z, plane2.normal.z, plane3.normal.z, plane4.normal.z);
				d = new global::Unity.Mathematics.float4(plane.distance, plane2.distance, plane3.distance, plane4.distance);
				nxAbs = global::Unity.Mathematics.math.abs(nx);
				nyAbs = global::Unity.Mathematics.math.abs(ny);
				nzAbs = global::Unity.Mathematics.math.abs(nz);
			}
		}

		internal struct SplitInfo
		{
			public int packetCount;
		}

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4> planePackets;

		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo> splitInfos;

		internal void Dispose(global::Unity.Jobs.JobHandle job)
		{
			planePackets.Dispose(job);
			splitInfos.Dispose(job);
		}

		internal static global::UnityEngine.Rendering.FrustumPlaneCuller Create(in global::UnityEngine.Rendering.BatchCullingContext cc, global::Unity.Collections.NativeArray<global::UnityEngine.Plane> receiverPlanes, in global::UnityEngine.Rendering.ReceiverSphereCuller receiverSphereCuller, global::Unity.Collections.Allocator allocator)
		{
			int length = cc.cullingSplits.Length;
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				int num2 = receiverPlanes.Length + cc.cullingSplits[i].cullingPlaneCount;
				num += (num2 + 3) / 4;
			}
			global::UnityEngine.Rendering.FrustumPlaneCuller result = new global::UnityEngine.Rendering.FrustumPlaneCuller
			{
				planePackets = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4>(num, allocator),
				splitInfos = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo>(length, allocator)
			};
			result.planePackets.ResizeUninitialized(num);
			result.splitInfos.ResizeUninitialized(length);
			global::Unity.Collections.NativeList<global::UnityEngine.Plane> nativeList = new global::Unity.Collections.NativeList<global::UnityEngine.Plane>(global::Unity.Collections.Allocator.Temp);
			int num3 = 0;
			for (int j = 0; j < length; j++)
			{
				global::UnityEngine.Rendering.CullingSplit cullingSplit = cc.cullingSplits[j];
				nativeList.Clear();
				for (int k = 0; k < cullingSplit.cullingPlaneCount; k++)
				{
					nativeList.Add(cc.cullingPlanes[cullingSplit.cullingPlaneOffset + k]);
				}
				if (receiverSphereCuller.UseReceiverPlanes())
				{
					nativeList.AddRange(receiverPlanes);
				}
				int num4 = (nativeList.Length + 3) / 4;
				result.splitInfos[j] = new global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo
				{
					packetCount = num4
				};
				for (int l = 0; l < num4; l++)
				{
					result.planePackets[num3 + l] = new global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4(nativeList.AsArray(), 4 * l, nativeList.Length - 1);
				}
				num3 += num4;
			}
			nativeList.Dispose();
			return result;
		}

		internal static uint ComputeSplitVisibilityMask(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4> planePackets, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo> splitInfos, in global::UnityEngine.Rendering.AABB bounds)
		{
			global::Unity.Mathematics.float4 xxxx = bounds.center.xxxx;
			global::Unity.Mathematics.float4 yyyy = bounds.center.yyyy;
			global::Unity.Mathematics.float4 zzzz = bounds.center.zzzz;
			global::Unity.Mathematics.float4 xxxx2 = bounds.extents.xxxx;
			global::Unity.Mathematics.float4 yyyy2 = bounds.extents.yyyy;
			global::Unity.Mathematics.float4 zzzz2 = bounds.extents.zzzz;
			uint num = 0u;
			int num2 = 0;
			int length = splitInfos.Length;
			for (int i = 0; i < length; i++)
			{
				global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo splitInfo = splitInfos[i];
				global::Unity.Mathematics.bool4 x = new global::Unity.Mathematics.bool4(v: false);
				for (int j = 0; j < splitInfo.packetCount; j++)
				{
					global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4 planePacket = planePackets[num2 + j];
					global::Unity.Mathematics.float4 float5 = planePacket.nx * xxxx + planePacket.ny * yyyy + planePacket.nz * zzzz + planePacket.d;
					global::Unity.Mathematics.float4 float6 = planePacket.nxAbs * xxxx2 + planePacket.nyAbs * yyyy2 + planePacket.nzAbs * zzzz2;
					x |= float5 + float6 < global::Unity.Mathematics.float4.zero;
				}
				if (!global::Unity.Mathematics.math.any(x))
				{
					num |= (uint)(1 << i);
				}
				num2 += splitInfo.packetCount;
			}
			return num;
		}
	}
}

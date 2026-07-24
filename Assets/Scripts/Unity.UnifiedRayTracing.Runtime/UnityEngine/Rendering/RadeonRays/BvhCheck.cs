namespace UnityEngine.Rendering.RadeonRays
{
	internal class BvhCheck
	{
		public class VertexBuffers
		{
			public global::UnityEngine.GraphicsBuffer vertices;

			public global::UnityEngine.GraphicsBuffer indices;

			public uint vertexBufferOffset;

			public uint vertexCount;

			public uint vertexStride = 3u;

			public uint indexBufferOffset;

			public global::UnityEngine.Rendering.RadeonRays.IndexFormat indexFormat;

			public uint indexCount;
		}

		private sealed class VertexBuffersCPU
		{
			public float[] vertices;

			public uint[] indices;

			public uint vertexStride;
		}

		private struct Triangle
		{
			public global::Unity.Mathematics.float3 v0;

			public global::Unity.Mathematics.float3 v1;

			public global::Unity.Mathematics.float3 v2;
		}

		private const uint kInvalidID = uint.MaxValue;

		public static global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers Convert(global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo info)
		{
			return new global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers
			{
				vertices = info.vertices,
				indices = info.triangleIndices,
				vertexBufferOffset = (uint)info.verticesStartOffset,
				vertexCount = info.vertexCount,
				vertexStride = info.vertexStride,
				indexBufferOffset = (uint)info.indicesStartOffset,
				indexCount = info.triangleCount * 3,
				indexFormat = info.indexFormat
			};
		}

		public static double SurfaceArea(global::UnityEngine.Rendering.RadeonRays.AABB aabb)
		{
			global::Unity.Mathematics.float3 float5 = aabb.Max - aabb.Min;
			return 2f * (float5.x * float5.y + float5.x * float5.z + float5.z * float5.y);
		}

		public static double NodeSahCost(uint nodeAddr, global::UnityEngine.Rendering.RadeonRays.AABB nodeAabb, global::UnityEngine.Rendering.RadeonRays.AABB parentAabb)
		{
			double num = (IsLeafNode(nodeAddr) ? ((float)GetLeafNodePrimCount(nodeAddr)) : 1.2f);
			double num2 = SurfaceArea(nodeAabb);
			double num3 = SurfaceArea(parentAabb);
			return num * num2 / num3;
		}

		public static double CheckConsistency(global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers bvhVertexBuffers, global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct bvh, uint primitiveCount)
		{
			return CheckConsistency(bvhVertexBuffers, bvh.bvh, bvh.bvhOffset, bvh.bvhLeaves, bvh.bvhLeavesOffset, primitiveCount);
		}

		public static double CheckConsistency(global::UnityEngine.GraphicsBuffer bvhBuffer, uint bvhBufferOffset, uint primitiveCount)
		{
			return CheckConsistency(null, bvhBuffer, bvhBufferOffset, null, 0u, primitiveCount);
		}

		private static double CheckConsistency(global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers bvhVertexBuffers, global::UnityEngine.GraphicsBuffer bvhBuffer, uint bvhBufferOffset, global::UnityEngine.GraphicsBuffer bvhLeavesBuffer, uint bvhLeavesBufferOffset, uint primitiveCount)
		{
			global::UnityEngine.Rendering.RadeonRays.BvhHeader[] array = new global::UnityEngine.Rendering.RadeonRays.BvhHeader[1];
			bvhBuffer.GetData(array, 0, (int)bvhBufferOffset, 1);
			return CheckConsistency(bvhVertexBuffers, bvhBuffer, bvhBufferOffset + 1, bvhLeavesBuffer, bvhLeavesBufferOffset, array[0], primitiveCount);
		}

		public static int ExtractBits(uint value, int startBit, int count)
		{
			return (int)((uint)((1 << count) - 1 << startBit) & value) >> startBit;
		}

		public static bool IsLeafNode(uint nodeAddr)
		{
			return (nodeAddr & int.MinValue) != 0;
		}

		public static uint GetLeafNodeFirstPrim(uint nodeAddr)
		{
			return nodeAddr & 0x1FFFFFFF;
		}

		public static uint GetLeafNodePrimCount(uint nodeAddr)
		{
			return (uint)(ExtractBits(nodeAddr, 29, 2) + 1);
		}

		private static double CheckConsistency(global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers bvhVertexBuffers, global::UnityEngine.GraphicsBuffer bvhBuffer, uint bvhBufferOffset, global::UnityEngine.GraphicsBuffer bvhLeavesBuffer, uint bvhLeavesBufferOffset, global::UnityEngine.Rendering.RadeonRays.BvhHeader header, uint primitiveCount)
		{
			uint leafNodeCount = header.leafNodeCount;
			uint root = header.root;
			uint bvhNodeCount = global::UnityEngine.Rendering.RadeonRays.HlbvhBuilder.GetBvhNodeCount(leafNodeCount);
			bool flag = bvhVertexBuffers == null;
			global::UnityEngine.Rendering.RadeonRays.BvhNode[] array = new global::UnityEngine.Rendering.RadeonRays.BvhNode[bvhNodeCount];
			bvhBuffer.GetData(array, 0, (int)bvhBufferOffset, (int)bvhNodeCount);
			global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffersCPU bvhVertexBuffers2 = null;
			global::Unity.Mathematics.uint4[] array2 = null;
			if (!flag)
			{
				bvhVertexBuffers2 = DownloadVertexData(bvhVertexBuffers);
				array2 = new global::Unity.Mathematics.uint4[primitiveCount];
				bvhLeavesBuffer.GetData(array2, 0, (int)bvhLeavesBufferOffset, (int)primitiveCount);
			}
			uint num = 0u;
			global::UnityEngine.Rendering.RadeonRays.AABB aabb = GetAabb(bvhVertexBuffers2, array, array2, root, flag);
			double num2 = 0.0;
			global::System.Collections.Generic.Queue<(uint, uint)> queue = new global::System.Collections.Generic.Queue<(uint, uint)>();
			queue.Enqueue((root, uint.MaxValue));
			while (queue.Count != 0)
			{
				uint item = queue.Dequeue().Item1;
				global::UnityEngine.Rendering.RadeonRays.AABB aabb2 = GetAabb(bvhVertexBuffers2, array, array2, item, flag);
				num2 += NodeSahCost(item, aabb2, aabb);
				if (flag)
				{
					IsLeafNode(item);
				}
				if (IsLeafNode(item))
				{
					num += (flag ? 1 : GetLeafNodePrimCount(item));
					continue;
				}
				global::UnityEngine.Rendering.RadeonRays.BvhNode bvhNode = array[item];
				global::UnityEngine.Rendering.RadeonRays.AABB aabb3 = GetAabb(bvhVertexBuffers2, array, array2, bvhNode.child0, flag);
				global::UnityEngine.Rendering.RadeonRays.AABB aabb4 = GetAabb(bvhVertexBuffers2, array, array2, bvhNode.child1, flag);
				aabb2.Contains(aabb3);
				aabb2.Contains(aabb4);
				queue.Enqueue((bvhNode.child0, item));
				queue.Enqueue((bvhNode.child1, item));
			}
			return num2;
		}

		private static global::Unity.Mathematics.uint3 GetFaceIndices(uint[] indices, uint triangleIdx)
		{
			return new global::Unity.Mathematics.uint3(indices[3 * triangleIdx], indices[3 * triangleIdx + 1], indices[3 * triangleIdx + 2]);
		}

		private static global::Unity.Mathematics.float3 GetVertex(float[] vertices, uint stride, uint idx)
		{
			uint num = idx * stride;
			return new global::Unity.Mathematics.float3(vertices[num], vertices[num + 1], vertices[num + 2]);
		}

		private static global::UnityEngine.Rendering.RadeonRays.BvhCheck.Triangle GetTriangle(float[] vertices, uint stride, global::Unity.Mathematics.uint3 idx)
		{
			global::UnityEngine.Rendering.RadeonRays.BvhCheck.Triangle result = default(global::UnityEngine.Rendering.RadeonRays.BvhCheck.Triangle);
			result.v0 = GetVertex(vertices, stride, idx.x);
			result.v1 = GetVertex(vertices, stride, idx.y);
			result.v2 = GetVertex(vertices, stride, idx.z);
			return result;
		}

		private static global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffersCPU DownloadVertexData(global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffers vertexBuffers)
		{
			global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffersCPU vertexBuffersCPU = new global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffersCPU();
			vertexBuffersCPU.vertices = new float[vertexBuffers.vertexCount * vertexBuffers.vertexStride];
			vertexBuffersCPU.indices = new uint[vertexBuffers.indexCount];
			vertexBuffersCPU.vertexStride = vertexBuffers.vertexStride;
			if (vertexBuffers.indexFormat == global::UnityEngine.Rendering.RadeonRays.IndexFormat.Int32)
			{
				vertexBuffers.indices.GetData(vertexBuffersCPU.indices, 0, (int)vertexBuffers.indexBufferOffset, (int)vertexBuffers.indexCount);
			}
			else
			{
				ushort[] array = new ushort[vertexBuffers.indexCount];
				vertexBuffers.indices.GetData(array, 0, (int)vertexBuffers.indexBufferOffset, (int)vertexBuffers.indexCount);
				for (int i = 0; i < vertexBuffers.indexCount; i++)
				{
					vertexBuffersCPU.indices[i] = array[i];
				}
			}
			vertexBuffers.vertices.GetData(vertexBuffersCPU.vertices, 0, (int)vertexBuffers.vertexBufferOffset, (int)(vertexBuffers.vertexCount * vertexBuffers.vertexStride));
			return vertexBuffersCPU;
		}

		private static global::UnityEngine.Rendering.RadeonRays.AABB GetAabb(global::UnityEngine.Rendering.RadeonRays.BvhCheck.VertexBuffersCPU bvhVertexBuffers, global::UnityEngine.Rendering.RadeonRays.BvhNode[] bvhNodes, global::Unity.Mathematics.uint4[] bvhLeafNodes, uint nodeAddr, bool isTopLevel)
		{
			global::UnityEngine.Rendering.RadeonRays.AABB aABB = new global::UnityEngine.Rendering.RadeonRays.AABB();
			if (!IsLeafNode(nodeAddr))
			{
				global::UnityEngine.Rendering.RadeonRays.BvhNode bvhNode = bvhNodes[nodeAddr];
				global::UnityEngine.Rendering.RadeonRays.AABB aabb = new global::UnityEngine.Rendering.RadeonRays.AABB(bvhNode.aabb0_min, bvhNode.aabb0_max);
				aABB.Encapsulate(aabb);
				global::UnityEngine.Rendering.RadeonRays.AABB aabb2 = new global::UnityEngine.Rendering.RadeonRays.AABB(bvhNode.aabb1_min, bvhNode.aabb1_max);
				aABB.Encapsulate(aabb2);
			}
			else if (!isTopLevel)
			{
				int leafNodeFirstPrim = (int)GetLeafNodeFirstPrim(nodeAddr);
				int leafNodePrimCount = (int)GetLeafNodePrimCount(nodeAddr);
				for (int i = 0; i < leafNodePrimCount; i++)
				{
					uint num = (uint)(i + leafNodeFirstPrim);
					global::Unity.Mathematics.uint3 xyz = bvhLeafNodes[num].xyz;
					GetFaceIndices(bvhVertexBuffers.indices, bvhLeafNodes[num].w);
					global::UnityEngine.Rendering.RadeonRays.BvhCheck.Triangle triangle = GetTriangle(bvhVertexBuffers.vertices, bvhVertexBuffers.vertexStride, xyz);
					aABB.Encapsulate(triangle.v0);
					aABB.Encapsulate(triangle.v1);
					aABB.Encapsulate(triangle.v2);
				}
			}
			return aABB;
		}
	}
}

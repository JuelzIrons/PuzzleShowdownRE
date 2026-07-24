namespace UnityEngine.Rendering.RadeonRays
{
	internal class RadeonRaysAPI : global::System.IDisposable
	{
		private readonly global::UnityEngine.Rendering.RadeonRays.HlbvhBuilder buildBvh;

		private readonly global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder buildTopLevelBvh;

		private readonly global::UnityEngine.Rendering.RadeonRays.RestructureBvh restructureBvh;

		public const global::UnityEngine.GraphicsBuffer.Target BufferTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		public RadeonRaysAPI(global::UnityEngine.Rendering.RadeonRays.RadeonRaysShaders shaders)
		{
			buildBvh = new global::UnityEngine.Rendering.RadeonRays.HlbvhBuilder(shaders);
			buildTopLevelBvh = new global::UnityEngine.Rendering.RadeonRays.HlbvhTopLevelBuilder(shaders);
			restructureBvh = new global::UnityEngine.Rendering.RadeonRays.RestructureBvh(shaders);
		}

		public void Dispose()
		{
			restructureBvh.Dispose();
		}

		public static int BvhInternalNodeSizeInDwords()
		{
			return global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.BvhNode>() / 4;
		}

		public static int BvhInternalNodeSizeInBytes()
		{
			return global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.BvhNode>();
		}

		public static int BvhLeafNodeSizeInBytes()
		{
			return global::System.Runtime.InteropServices.Marshal.SizeOf<global::Unity.Mathematics.uint4>();
		}

		public static int BvhLeafNodeSizeInDwords()
		{
			return global::System.Runtime.InteropServices.Marshal.SizeOf<global::Unity.Mathematics.uint4>() / 4;
		}

		public void BuildMeshAccelStruct(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo buildInfo, global::UnityEngine.Rendering.RadeonRays.BuildFlags buildFlags, global::UnityEngine.GraphicsBuffer scratchBuffer, in global::UnityEngine.Rendering.RadeonRays.BottomLevelLevelAccelStruct result)
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Metal)
			{
				buildFlags |= global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild;
			}
			buildBvh.Execute(cmd, buildInfo.vertices, buildInfo.verticesStartOffset, buildInfo.vertexStride, buildInfo.triangleIndices, buildInfo.indicesStartOffset, buildInfo.baseIndex, buildInfo.indexFormat, buildInfo.triangleCount, scratchBuffer, in result);
			if ((buildFlags & global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild) == 0)
			{
				restructureBvh.Execute(cmd, buildInfo.vertices, buildInfo.verticesStartOffset, buildInfo.vertexStride, buildInfo.triangleCount, scratchBuffer, in result);
			}
		}

		public global::UnityEngine.Rendering.RadeonRays.MeshBuildMemoryRequirements GetMeshBuildMemoryRequirements(global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo buildInfo, global::UnityEngine.Rendering.RadeonRays.BuildFlags buildFlags)
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Metal)
			{
				buildFlags |= global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild;
			}
			global::UnityEngine.Rendering.RadeonRays.MeshBuildMemoryRequirements result = default(global::UnityEngine.Rendering.RadeonRays.MeshBuildMemoryRequirements);
			result.bvhSizeInDwords = buildBvh.GetResultDataSizeInDwords(buildInfo.triangleCount);
			result.bvhLeavesSizeInDwords = (ulong)(buildInfo.triangleCount * BvhLeafNodeSizeInDwords());
			result.buildScratchSizeInDwords = buildBvh.GetScratchDataSizeInDwords(buildInfo.triangleCount);
			ulong y = (((buildFlags & global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild) == 0) ? restructureBvh.GetScratchDataSizeInDwords(buildInfo.triangleCount) : 0);
			result.buildScratchSizeInDwords = global::Unity.Mathematics.math.max(result.buildScratchSizeInDwords, y);
			return result;
		}

		public global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct BuildSceneAccelStruct(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.GraphicsBuffer meshAccelStructsBuffer, global::UnityEngine.Rendering.RadeonRays.Instance[] instances, global::UnityEngine.GraphicsBuffer scratchBuffer)
		{
			global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct = default(global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct);
			if (instances.Length == 0)
			{
				buildTopLevelBvh.CreateEmpty(ref accelStruct);
				return accelStruct;
			}
			buildTopLevelBvh.AllocateResultBuffers((uint)instances.Length, ref accelStruct);
			global::UnityEngine.Rendering.RadeonRays.InstanceInfo[] array = new global::UnityEngine.Rendering.RadeonRays.InstanceInfo[instances.Length];
			for (uint num = 0u; num < instances.Length; num++)
			{
				array[num] = new global::UnityEngine.Rendering.RadeonRays.InstanceInfo
				{
					blasOffset = (int)instances[num].meshAccelStructOffset,
					instanceMask = (int)instances[num].instanceMask,
					vertexOffset = (int)instances[num].vertexOffset,
					indexOffset = (int)instances[num].meshAccelStructLeavesOffset,
					localToWorldTransform = instances[num].localToWorldTransform,
					disableTriangleCulling = ((!instances[num].triangleCullingEnabled) ? 1073741824u : 0u),
					invertTriangleCulling = (instances[num].invertTriangleCulling ? 2147483648u : 0u),
					userInstanceID = instances[num].userInstanceID,
					isOpaque = (instances[num].isOpaque ? 1 : 0)
				};
			}
			accelStruct.instanceInfos.SetData(array);
			accelStruct.bottomLevelBvhs = meshAccelStructsBuffer;
			accelStruct.instanceCount = (uint)instances.Length;
			buildTopLevelBvh.Execute(cmd, scratchBuffer, ref accelStruct);
			return accelStruct;
		}

		public global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct CreateSceneAccelStructBuffers(global::UnityEngine.GraphicsBuffer meshAccelStructsBuffer, uint tlasSizeInDwords, global::UnityEngine.Rendering.RadeonRays.Instance[] instances)
		{
			global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct accelStruct = default(global::UnityEngine.Rendering.RadeonRays.TopLevelAccelStruct);
			if (instances.Length == 0)
			{
				buildTopLevelBvh.CreateEmpty(ref accelStruct);
				return accelStruct;
			}
			global::UnityEngine.Rendering.RadeonRays.InstanceInfo[] array = new global::UnityEngine.Rendering.RadeonRays.InstanceInfo[instances.Length];
			for (uint num = 0u; num < instances.Length; num++)
			{
				array[num] = new global::UnityEngine.Rendering.RadeonRays.InstanceInfo
				{
					blasOffset = (int)instances[num].meshAccelStructOffset,
					instanceMask = (int)instances[num].instanceMask,
					vertexOffset = (int)instances[num].vertexOffset,
					indexOffset = (int)instances[num].meshAccelStructLeavesOffset,
					localToWorldTransform = instances[num].localToWorldTransform,
					disableTriangleCulling = ((!instances[num].triangleCullingEnabled) ? 1073741824u : 0u),
					invertTriangleCulling = (instances[num].invertTriangleCulling ? 2147483648u : 0u),
					userInstanceID = instances[num].userInstanceID,
					worldToLocalTransform = instances[num].localToWorldTransform.Inverse()
				};
			}
			accelStruct.instanceInfos = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, instances.Length, global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.InstanceInfo>());
			accelStruct.instanceInfos.SetData(array);
			accelStruct.bottomLevelBvhs = meshAccelStructsBuffer;
			accelStruct.topLevelBvh = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, (int)tlasSizeInDwords / BvhInternalNodeSizeInDwords(), global::System.Runtime.InteropServices.Marshal.SizeOf<global::UnityEngine.Rendering.RadeonRays.BvhNode>());
			accelStruct.instanceCount = (uint)instances.Length;
			return accelStruct;
		}

		public global::UnityEngine.Rendering.RadeonRays.SceneBuildMemoryRequirements GetSceneBuildMemoryRequirements(uint instanceCount)
		{
			return new global::UnityEngine.Rendering.RadeonRays.SceneBuildMemoryRequirements
			{
				buildScratchSizeInDwords = buildTopLevelBvh.GetScratchDataSizeInDwords(instanceCount)
			};
		}

		public global::UnityEngine.Rendering.RadeonRays.SceneMemoryRequirements GetSceneMemoryRequirements(global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo[] buildInfos, global::UnityEngine.Rendering.RadeonRays.BuildFlags buildFlags)
		{
			if (global::UnityEngine.SystemInfo.graphicsDeviceType == global::UnityEngine.Rendering.GraphicsDeviceType.Metal)
			{
				buildFlags |= global::UnityEngine.Rendering.RadeonRays.BuildFlags.PreferFastBuild;
			}
			global::UnityEngine.Rendering.RadeonRays.SceneMemoryRequirements sceneMemoryRequirements = new global::UnityEngine.Rendering.RadeonRays.SceneMemoryRequirements();
			sceneMemoryRequirements.buildScratchSizeInDwords = 0uL;
			sceneMemoryRequirements.bottomLevelBvhSizeInNodes = new ulong[buildInfos.Length];
			sceneMemoryRequirements.bottomLevelBvhOffsetInNodes = new uint[buildInfos.Length];
			sceneMemoryRequirements.bottomLevelBvhLeavesSizeInNodes = new ulong[buildInfos.Length];
			sceneMemoryRequirements.bottomLevelBvhLeavesOffsetInNodes = new uint[buildInfos.Length];
			int num = 0;
			uint num2 = 0u;
			uint num3 = 0u;
			foreach (global::UnityEngine.Rendering.RadeonRays.MeshBuildInfo buildInfo in buildInfos)
			{
				global::UnityEngine.Rendering.RadeonRays.MeshBuildMemoryRequirements meshBuildMemoryRequirements = GetMeshBuildMemoryRequirements(buildInfo, buildFlags);
				sceneMemoryRequirements.buildScratchSizeInDwords = global::Unity.Mathematics.math.max(sceneMemoryRequirements.buildScratchSizeInDwords, meshBuildMemoryRequirements.buildScratchSizeInDwords);
				sceneMemoryRequirements.bottomLevelBvhSizeInNodes[num] = meshBuildMemoryRequirements.bvhSizeInDwords / (ulong)BvhInternalNodeSizeInDwords();
				sceneMemoryRequirements.bottomLevelBvhOffsetInNodes[num] = num2;
				sceneMemoryRequirements.bottomLevelBvhLeavesSizeInNodes[num] = meshBuildMemoryRequirements.bvhLeavesSizeInDwords / (ulong)BvhLeafNodeSizeInDwords();
				sceneMemoryRequirements.bottomLevelBvhLeavesOffsetInNodes[num] = num3;
				num2 += (uint)(int)(meshBuildMemoryRequirements.bvhSizeInDwords / (ulong)BvhInternalNodeSizeInDwords());
				num3 += (uint)(int)(meshBuildMemoryRequirements.bvhLeavesSizeInDwords / (ulong)BvhLeafNodeSizeInDwords());
				num++;
			}
			sceneMemoryRequirements.totalBottomLevelBvhSizeInNodes = num2;
			sceneMemoryRequirements.totalBottomLevelBvhLeavesSizeInNodes = num3;
			ulong scratchDataSizeInDwords = buildTopLevelBvh.GetScratchDataSizeInDwords((uint)buildInfos.Length);
			sceneMemoryRequirements.buildScratchSizeInDwords = global::Unity.Mathematics.math.max(sceneMemoryRequirements.buildScratchSizeInDwords, scratchDataSizeInDwords);
			return sceneMemoryRequirements;
		}

		public static ulong GetTraceMemoryRequirements(uint rayCount)
		{
			return 64 * rayCount;
		}
	}
}

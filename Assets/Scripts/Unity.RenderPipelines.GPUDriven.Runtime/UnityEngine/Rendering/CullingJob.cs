namespace UnityEngine.Rendering
{
	[global::Unity.Burst.BurstCompile]
	internal struct CullingJob : global::Unity.Jobs.IJobParallelFor
	{
		private enum CrossFadeType
		{
			kDisabled = 0,
			kCrossFadeOut = 1,
			kCrossFadeIn = 2,
			kVisible = 3
		}

		public const int k_BatchSize = 32;

		public const uint k_MeshLodCrossfadeActive = 64u;

		public const uint k_MeshLodCrossfadeSignBit = 128u;

		public const uint k_MeshLodCrossfadeBits = 192u;

		public const uint k_LODFadeOff = 255u;

		public const uint k_LODFadeZeroPacked = 127u;

		public const uint k_LODFadeIsSpeedTree = 256u;

		private const uint k_InvalidCrossFadeAndLevel = uint.MaxValue;

		private const uint k_VisibilityMaskNotVisible = 0u;

		private const float k_SmallMeshTransitionWidth = 0.1f;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.BinningConfig binningConfig;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.BatchCullingViewType viewType;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Mathematics.float3 cameraPosition;

		[global::Unity.Collections.ReadOnly]
		public float sqrMeshLodSelectionConstant;

		[global::Unity.Collections.ReadOnly]
		public float sqrScreenRelativeMetric;

		[global::Unity.Collections.ReadOnly]
		public float minScreenRelativeHeight;

		[global::Unity.Collections.ReadOnly]
		public bool isOrtho;

		[global::Unity.Collections.ReadOnly]
		public bool cullLightmappedShadowCasters;

		[global::Unity.Collections.ReadOnly]
		public int maxLOD;

		[global::Unity.Collections.ReadOnly]
		public uint cullingLayerMask;

		[global::Unity.Collections.ReadOnly]
		public ulong sceneCullingMask;

		[global::Unity.Collections.ReadOnly]
		public bool animateCrossFades;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.FrustumPlaneCuller.PlanePacket4> frustumPlanePackets;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.FrustumPlaneCuller.SplitInfo> frustumSplitInfos;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Plane> lightFacingFrustumPlanes;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.ReceiverSphereCuller.SplitInfo> receiverSplitInfos;

		public global::Unity.Mathematics.float3x3 worldToLightSpaceRotation;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.CPUInstanceData.ReadOnly instanceData;

		[global::Unity.Collections.ReadOnly]
		public global::UnityEngine.Rendering.CPUSharedInstanceData.ReadOnly sharedInstanceData;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		[global::Unity.Burst.NoAlias]
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeList<global::UnityEngine.Rendering.LODGroupCullingData> lodGroupCullingData;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
		[global::Unity.Collections.ReadOnly]
		public global::System.IntPtr occlusionBuffer;

		[global::Unity.Collections.LowLevel.Unsafe.NativeDisableContainerSafetyRestriction]
		public global::UnityEngine.Rendering.CPUPerCameraInstanceData.PerCameraInstanceDataArrays cameraInstanceData;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<byte> rendererVisibilityMasks;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<byte> rendererMeshLodSettings;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		[global::Unity.Collections.WriteOnly]
		public global::Unity.Collections.NativeArray<byte> rendererCrossFadeValues;

		private static uint PackFloatToUint8(float percent)
		{
			uint valueToClamp = (uint)((1f + percent) * 127f + 0.5f);
			if (percent < 0f)
			{
				return global::Unity.Mathematics.math.clamp(valueToClamp, 0u, 126u);
			}
			return global::Unity.Mathematics.math.clamp(valueToClamp, 128u, 254u);
		}

		private unsafe uint CalculateLODVisibility(int instanceIndex, int sharedInstanceIndex, global::UnityEngine.Rendering.InstanceFlags instanceFlags)
		{
			uint num = sharedInstanceData.lodGroupAndMasks[sharedInstanceIndex];
			if (num == uint.MaxValue)
			{
				if (viewType >= global::UnityEngine.Rendering.BatchCullingViewType.SelectionOutline || (instanceFlags & global::UnityEngine.Rendering.InstanceFlags.SmallMeshCulling) == 0 || minScreenRelativeHeight == 0f)
				{
					return 255u;
				}
				ref readonly global::UnityEngine.Rendering.AABB reference = ref instanceData.worldAABBs.UnsafeElementAt(instanceIndex);
				float num2 = global::Unity.Mathematics.math.sqrt(isOrtho ? sqrScreenRelativeMetric : global::UnityEngine.Rendering.LODRenderingUtils.CalculateSqrPerspectiveDistance(reference.center, cameraPosition, sqrScreenRelativeMetric));
				global::Unity.Mathematics.float3 float5 = reference.extents * 2f;
				float size = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.max(float5.x, float5.y), float5.z);
				float num3 = global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(minScreenRelativeHeight, size);
				if (num3 < num2)
				{
					return 127u;
				}
				float relativeScreenHeight = minScreenRelativeHeight + 0.1f * minScreenRelativeHeight;
				float num4 = global::UnityEngine.Mathf.Max(0f, num3 - global::UnityEngine.Rendering.LODRenderingUtils.CalculateLODDistance(relativeScreenHeight, size));
				float num5 = (num3 - num2) / num4;
				if (!(num5 > 1f))
				{
					return PackFloatToUint8(num5);
				}
				return 255u;
			}
			uint index = num >> 8;
			uint num6 = num & 0xFF;
			ref global::UnityEngine.Rendering.LODGroupCullingData reference2 = ref lodGroupCullingData.ElementAt((int)index);
			if (reference2.forceLODMask != 0)
			{
				if ((reference2.forceLODMask & num6) == 0)
				{
					return 127u;
				}
				return 255u;
			}
			float num7 = (isOrtho ? sqrScreenRelativeMetric : global::UnityEngine.Rendering.LODRenderingUtils.CalculateSqrPerspectiveDistance(reference2.worldSpaceReferencePoint, cameraPosition, sqrScreenRelativeMetric));
			uint num8 = (uint)(-1 << maxLOD);
			num6 &= num8;
			int num9 = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.tzcnt(num6) - 1, maxLOD);
			num6 >>= num9;
			while (num6 != 0)
			{
				float num10 = ((num9 == maxLOD) ? 0f : reference2.sqrDistances[num9 - 1]);
				float num11 = reference2.sqrDistances[num9];
				if (num7 < num10)
				{
					break;
				}
				if (num7 > num11)
				{
					num9++;
					num6 >>= 1;
					continue;
				}
				global::UnityEngine.Rendering.CullingJob.CrossFadeType crossFadeType = (global::UnityEngine.Rendering.CullingJob.CrossFadeType)(num6 & 3);
				switch (crossFadeType)
				{
				case global::UnityEngine.Rendering.CullingJob.CrossFadeType.kDisabled:
					return 127u;
				case global::UnityEngine.Rendering.CullingJob.CrossFadeType.kVisible:
					return 255u;
				}
				float num12 = global::Unity.Mathematics.math.sqrt(num7);
				float num13 = global::Unity.Mathematics.math.sqrt(num11);
				if (reference2.percentageFlags[num9])
				{
					switch (crossFadeType)
					{
					case global::UnityEngine.Rendering.CullingJob.CrossFadeType.kCrossFadeIn:
						return 128u;
					case global::UnityEngine.Rendering.CullingJob.CrossFadeType.kCrossFadeOut:
					{
						float num14 = ((num9 > 0) ? global::Unity.Mathematics.math.sqrt(reference2.sqrDistances[num9 - 1]) : reference2.worldSpaceSize);
						return PackFloatToUint8(global::Unity.Mathematics.math.max(num12 - num14, 0f) / (num13 - num14)) | 0x100;
					}
					}
					continue;
				}
				float num15 = reference2.transitionDistances[num9];
				float num16 = num13 - num12;
				if (num16 < num15)
				{
					float num17 = num16 / num15;
					if (crossFadeType == global::UnityEngine.Rendering.CullingJob.CrossFadeType.kCrossFadeIn)
					{
						num17 = 0f - num17;
					}
					return PackFloatToUint8(num17);
				}
				if (crossFadeType != global::UnityEngine.Rendering.CullingJob.CrossFadeType.kCrossFadeOut)
				{
					return 127u;
				}
				return 255u;
			}
			return 127u;
		}

		private uint CalculateVisibilityMask(int instanceIndex, int sharedInstanceIndex, global::UnityEngine.Rendering.InstanceFlags instanceFlags)
		{
			if (cullingLayerMask == 0)
			{
				return 0u;
			}
			if ((cullingLayerMask & (1 << sharedInstanceData.gameObjectLayers[sharedInstanceIndex])) == 0L)
			{
				return 0u;
			}
			if (cullLightmappedShadowCasters && (instanceFlags & global::UnityEngine.Rendering.InstanceFlags.AffectsLightmaps) != global::UnityEngine.Rendering.InstanceFlags.None)
			{
				return 0u;
			}
			if (viewType == global::UnityEngine.Rendering.BatchCullingViewType.Camera && (instanceFlags & global::UnityEngine.Rendering.InstanceFlags.IsShadowsOnly) != global::UnityEngine.Rendering.InstanceFlags.None)
			{
				return 0u;
			}
			if (viewType == global::UnityEngine.Rendering.BatchCullingViewType.Light && (instanceFlags & global::UnityEngine.Rendering.InstanceFlags.IsShadowsOff) != global::UnityEngine.Rendering.InstanceFlags.None)
			{
				return 0u;
			}
			ref readonly global::UnityEngine.Rendering.AABB reference = ref instanceData.worldAABBs.UnsafeElementAt(instanceIndex);
			uint num = global::UnityEngine.Rendering.FrustumPlaneCuller.ComputeSplitVisibilityMask(frustumPlanePackets, frustumSplitInfos, in reference);
			if (num != 0 && receiverSplitInfos.Length > 0)
			{
				num &= global::UnityEngine.Rendering.ReceiverSphereCuller.ComputeSplitVisibilityMask(lightFacingFrustumPlanes, receiverSplitInfos, worldToLightSpaceRotation, in reference);
			}
			if (num != 0 && occlusionBuffer != global::System.IntPtr.Zero)
			{
				num = (global::UnityEngine.Rendering.BatchRendererGroup.OcclusionTestAABB(occlusionBuffer, reference.ToBounds()) ? num : 0u);
			}
			return num;
		}

		private uint ComputeMeshLODLevel(int instanceIndex, int sharedInstanceIndex)
		{
			ref readonly global::UnityEngine.Rendering.GPUDrivenRendererMeshLodData reference = ref instanceData.meshLodData.UnsafeElementAt(instanceIndex);
			global::UnityEngine.Rendering.GPUDrivenMeshLodInfo gPUDrivenMeshLodInfo = sharedInstanceData.meshLodInfos[sharedInstanceIndex];
			if (reference.forceLod >= 0)
			{
				return (uint)global::Unity.Mathematics.math.clamp(reference.forceLod, 0, gPUDrivenMeshLodInfo.levelCount - 1);
			}
			ref readonly global::UnityEngine.Rendering.AABB reference2 = ref instanceData.worldAABBs.UnsafeElementAt(instanceIndex);
			float num = global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.lengthsq(reference2.extents), 1E-05f) * 4f;
			return (uint)global::Unity.Mathematics.math.floor(global::Unity.Mathematics.math.clamp(global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.log2(global::System.Math.Sqrt((isOrtho ? sqrMeshLodSelectionConstant : global::UnityEngine.Rendering.LODRenderingUtils.CalculateSqrPerspectiveDistance(reference2.center, cameraPosition, sqrMeshLodSelectionConstant)) / num)) * (double)gPUDrivenMeshLodInfo.lodSlope + (double)gPUDrivenMeshLodInfo.lodBias, 0.0) + (double)reference.lodSelectionBias, 0.0, gPUDrivenMeshLodInfo.levelCount - 1));
		}

		private uint ComputeMeshLODCrossfade(int instanceIndex, ref uint meshLodLevel)
		{
			byte b = cameraInstanceData.meshLods[instanceIndex];
			if (b == byte.MaxValue)
			{
				cameraInstanceData.meshLods[instanceIndex] = (byte)meshLodLevel;
				return 255u;
			}
			byte b2 = cameraInstanceData.crossFades[instanceIndex];
			if (b2 == byte.MaxValue)
			{
				if (b == meshLodLevel)
				{
					return 255u;
				}
				cameraInstanceData.meshLods[instanceIndex] = (byte)meshLodLevel;
				cameraInstanceData.crossFades[instanceIndex] = (byte)((meshLodLevel >= b) ? 1u : 128u);
				meshLodLevel = b;
				return 255u;
			}
			if ((long)(b2 - 1) % 127L == 0L)
			{
				meshLodLevel = b;
				return 255u;
			}
			meshLodLevel = (uint)(b | (((uint)b2 > 127u) ? 192 : 64));
			return b2;
		}

		private void EnforcePreviousFrameMeshLOD(int instanceIndex, ref uint meshLodLevel)
		{
			ref byte reference = ref cameraInstanceData.meshLods.ElementAt(instanceIndex);
			if (reference != byte.MaxValue)
			{
				meshLodLevel = reference;
			}
		}

		public void Execute(int instanceIndex)
		{
			global::UnityEngine.Rendering.InstanceHandle instance = instanceData.instances[instanceIndex];
			int num = sharedInstanceData.InstanceToIndex(in instanceData, instance);
			global::UnityEngine.Rendering.InstanceFlags instanceFlags = sharedInstanceData.flags[num].instanceFlags;
			uint num2 = CalculateVisibilityMask(instanceIndex, num, instanceFlags);
			if (num2 == 0)
			{
				rendererVisibilityMasks[instance.index] = 0;
				return;
			}
			uint num3 = CalculateLODVisibility(instanceIndex, num, instanceFlags);
			if (num3 == 127)
			{
				rendererVisibilityMasks[instance.index] = 0;
				return;
			}
			if (binningConfig.supportsMotionCheck)
			{
				bool flag = instanceData.movedInPreviousFrameBits.Get(instanceIndex);
				num2 = (num2 << 1) | (uint)(flag ? 1 : 0);
			}
			uint meshLodLevel = 0u;
			bool flag2 = (instanceFlags & global::UnityEngine.Rendering.InstanceFlags.HasMeshLod) != 0;
			if (flag2)
			{
				meshLodLevel = ComputeMeshLODLevel(instanceIndex, num);
			}
			if (binningConfig.supportsCrossFade)
			{
				if (flag2 && animateCrossFades)
				{
					if (num3 == 255)
					{
						num3 = ComputeMeshLODCrossfade(instanceIndex, ref meshLodLevel);
					}
					else
					{
						EnforcePreviousFrameMeshLOD(instanceIndex, ref meshLodLevel);
					}
				}
				num2 = (num2 << 1) | ((num3 < 255) ? 1u : 0u);
			}
			rendererVisibilityMasks[instance.index] = (byte)num2;
			rendererMeshLodSettings[instance.index] = (byte)meshLodLevel;
			rendererCrossFadeValues[instance.index] = (byte)(num3 & 0xFF);
		}
	}
}

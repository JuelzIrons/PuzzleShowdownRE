namespace UnityEngine.Rendering.Universal
{
	internal class DecalCachedChunk : global::UnityEngine.Rendering.Universal.DecalChunk
	{
		public global::UnityEngine.MaterialPropertyBlock propertyBlock;

		public int passIndexDBuffer;

		public int passIndexEmissive;

		public int passIndexScreenSpace;

		public int passIndexGBuffer;

		public int drawOrder;

		public bool isCreated;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> decalToWorlds;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> normalToWorlds;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> sizeOffsets;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> drawDistances;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> angleFades;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4> uvScaleBias;

		public global::Unity.Collections.NativeArray<int> layerMasks;

		public global::Unity.Collections.NativeArray<ulong> sceneLayerMasks;

		public global::Unity.Collections.NativeArray<float> fadeFactors;

		public global::Unity.Collections.NativeArray<global::UnityEngine.BoundingSphere> boundingSpheres;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.DecalScaleMode> scaleModes;

		public global::Unity.Collections.NativeArray<uint> renderingLayerMasks;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> positions;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.quaternion> rotation;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> scales;

		public global::Unity.Collections.NativeArray<bool> dirty;

		public global::UnityEngine.BoundingSphere[] boundingSphereArray;

		public override void RemoveAtSwapBack(int entityIndex)
		{
			RemoveAtSwapBack(ref decalToWorlds, entityIndex, base.count);
			RemoveAtSwapBack(ref normalToWorlds, entityIndex, base.count);
			RemoveAtSwapBack(ref sizeOffsets, entityIndex, base.count);
			RemoveAtSwapBack(ref drawDistances, entityIndex, base.count);
			RemoveAtSwapBack(ref angleFades, entityIndex, base.count);
			RemoveAtSwapBack(ref uvScaleBias, entityIndex, base.count);
			RemoveAtSwapBack(ref layerMasks, entityIndex, base.count);
			RemoveAtSwapBack(ref sceneLayerMasks, entityIndex, base.count);
			RemoveAtSwapBack(ref fadeFactors, entityIndex, base.count);
			RemoveAtSwapBack(ref boundingSphereArray, entityIndex, base.count);
			RemoveAtSwapBack(ref boundingSpheres, entityIndex, base.count);
			RemoveAtSwapBack(ref scaleModes, entityIndex, base.count);
			RemoveAtSwapBack(ref renderingLayerMasks, entityIndex, base.count);
			RemoveAtSwapBack(ref positions, entityIndex, base.count);
			RemoveAtSwapBack(ref rotation, entityIndex, base.count);
			RemoveAtSwapBack(ref scales, entityIndex, base.count);
			RemoveAtSwapBack(ref dirty, entityIndex, base.count);
			base.count--;
		}

		public override void SetCapacity(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref decalToWorlds, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref normalToWorlds, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref sizeOffsets, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref drawDistances, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref angleFades, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref uvScaleBias, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref layerMasks, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref sceneLayerMasks, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref fadeFactors, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref boundingSpheres, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref scaleModes, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref renderingLayerMasks, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref positions, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref rotation, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref scales, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref dirty, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref boundingSphereArray, newCapacity);
			base.capacity = newCapacity;
		}

		public override void Dispose()
		{
			if (base.capacity != 0)
			{
				decalToWorlds.Dispose();
				normalToWorlds.Dispose();
				sizeOffsets.Dispose();
				drawDistances.Dispose();
				angleFades.Dispose();
				uvScaleBias.Dispose();
				layerMasks.Dispose();
				sceneLayerMasks.Dispose();
				fadeFactors.Dispose();
				boundingSpheres.Dispose();
				scaleModes.Dispose();
				renderingLayerMasks.Dispose();
				positions.Dispose();
				rotation.Dispose();
				scales.Dispose();
				dirty.Dispose();
				base.count = 0;
				base.capacity = 0;
			}
		}
	}
}

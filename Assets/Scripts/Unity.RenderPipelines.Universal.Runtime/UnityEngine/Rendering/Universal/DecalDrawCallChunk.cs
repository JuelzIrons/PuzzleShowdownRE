namespace UnityEngine.Rendering.Universal
{
	internal class DecalDrawCallChunk : global::UnityEngine.Rendering.Universal.DecalChunk
	{
		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> decalToWorlds;

		public global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> normalToDecals;

		public global::Unity.Collections.NativeArray<float> renderingLayerMasks;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.DecalSubDrawCall> subCalls;

		public global::Unity.Collections.NativeArray<int> subCallCounts;

		public int subCallCount
		{
			get
			{
				return subCallCounts[0];
			}
			set
			{
				subCallCounts[0] = value;
			}
		}

		public override void RemoveAtSwapBack(int entityIndex)
		{
			RemoveAtSwapBack(ref decalToWorlds, entityIndex, base.count);
			RemoveAtSwapBack(ref normalToDecals, entityIndex, base.count);
			RemoveAtSwapBack(ref renderingLayerMasks, entityIndex, base.count);
			RemoveAtSwapBack(ref subCalls, entityIndex, base.count);
			base.count--;
		}

		public override void SetCapacity(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref decalToWorlds, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref normalToDecals, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref renderingLayerMasks, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref subCalls, newCapacity);
			base.capacity = newCapacity;
		}

		public override void Dispose()
		{
			subCallCounts.Dispose();
			if (base.capacity != 0)
			{
				decalToWorlds.Dispose();
				normalToDecals.Dispose();
				renderingLayerMasks.Dispose();
				subCalls.Dispose();
				base.count = 0;
				base.capacity = 0;
			}
		}
	}
}

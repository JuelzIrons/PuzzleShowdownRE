namespace UnityEngine.Rendering.Universal
{
	internal class DecalEntityChunk : global::UnityEngine.Rendering.Universal.DecalChunk
	{
		public global::UnityEngine.Material material;

		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.DecalEntity> decalEntities;

		public global::UnityEngine.Rendering.Universal.DecalProjector[] decalProjectors;

		public global::UnityEngine.Jobs.TransformAccessArray transformAccessArray;

		public override void Push()
		{
			base.count++;
		}

		public override void RemoveAtSwapBack(int entityIndex)
		{
			RemoveAtSwapBack(ref decalEntities, entityIndex, base.count);
			RemoveAtSwapBack(ref decalProjectors, entityIndex, base.count);
			transformAccessArray.RemoveAtSwapBack(entityIndex);
			base.count--;
		}

		public override void SetCapacity(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref decalEntities, newCapacity);
			ResizeNativeArray(ref transformAccessArray, decalProjectors, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref decalProjectors, newCapacity);
			base.capacity = newCapacity;
		}

		public override void Dispose()
		{
			if (base.capacity != 0)
			{
				decalEntities.Dispose();
				transformAccessArray.Dispose();
				decalProjectors = null;
				base.count = 0;
				base.capacity = 0;
			}
		}
	}
}

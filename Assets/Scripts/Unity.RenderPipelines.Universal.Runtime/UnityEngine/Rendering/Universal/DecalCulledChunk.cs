namespace UnityEngine.Rendering.Universal
{
	internal class DecalCulledChunk : global::UnityEngine.Rendering.Universal.DecalChunk
	{
		public global::UnityEngine.Vector3 cameraPosition;

		public ulong sceneCullingMask;

		public int cullingMask;

		public global::UnityEngine.CullingGroup cullingGroups;

		public int[] visibleDecalIndexArray;

		public global::Unity.Collections.NativeArray<int> visibleDecalIndices;

		public int visibleDecalCount;

		public override void RemoveAtSwapBack(int entityIndex)
		{
			RemoveAtSwapBack(ref visibleDecalIndexArray, entityIndex, base.count);
			RemoveAtSwapBack(ref visibleDecalIndices, entityIndex, base.count);
			base.count--;
		}

		public override void SetCapacity(int newCapacity)
		{
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref visibleDecalIndexArray, newCapacity);
			global::UnityEngine.Rendering.ArrayExtensions.ResizeArray(ref visibleDecalIndices, newCapacity);
			if (cullingGroups == null)
			{
				cullingGroups = new global::UnityEngine.CullingGroup();
			}
			base.capacity = newCapacity;
		}

		public override void Dispose()
		{
			if (base.capacity != 0)
			{
				visibleDecalIndices.Dispose();
				visibleDecalIndexArray = null;
				base.count = 0;
				base.capacity = 0;
				cullingGroups.Dispose();
				cullingGroups = null;
			}
		}
	}
}

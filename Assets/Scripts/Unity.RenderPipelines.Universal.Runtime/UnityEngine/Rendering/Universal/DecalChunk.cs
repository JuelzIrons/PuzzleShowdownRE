namespace UnityEngine.Rendering.Universal
{
	internal abstract class DecalChunk : global::System.IDisposable
	{
		public int count { get; protected set; }

		public int capacity { get; protected set; }

		public global::Unity.Jobs.JobHandle currentJobHandle { get; set; }

		public virtual void Push()
		{
			count++;
		}

		public abstract void RemoveAtSwapBack(int index);

		public abstract void SetCapacity(int capacity);

		public virtual void Dispose()
		{
		}

		protected void ResizeNativeArray(ref global::UnityEngine.Jobs.TransformAccessArray array, global::UnityEngine.Rendering.Universal.DecalProjector[] decalProjectors, int capacity)
		{
			global::UnityEngine.Jobs.TransformAccessArray transformAccessArray = new global::UnityEngine.Jobs.TransformAccessArray(capacity);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					transformAccessArray.Add(decalProjectors[i].transform);
				}
				array.Dispose();
			}
			array = transformAccessArray;
		}

		protected void RemoveAtSwapBack<T>(ref global::Unity.Collections.NativeArray<T> array, int index, int count) where T : struct
		{
			array[index] = array[count - 1];
		}

		protected void RemoveAtSwapBack<T>(ref T[] array, int index, int count)
		{
			array[index] = array[count - 1];
		}
	}
}

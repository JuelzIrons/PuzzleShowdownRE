namespace UnityEngine.Rendering.RenderGraphModule
{
	public sealed class RenderGraphObjectPool
	{
		private class SharedObjectPoolBase
		{
			public virtual void Clear()
			{
			}
		}

		private class SharedObjectPool<T> : global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase where T : class, new()
		{
			private static readonly global::UnityEngine.Pool.ObjectPool<T> s_Pool = AllocatePool();

			private static global::UnityEngine.Pool.ObjectPool<T> AllocatePool()
			{
				global::UnityEngine.Pool.ObjectPool<T> result = new global::UnityEngine.Pool.ObjectPool<T>(() => new T());
				s_AllocatedPools.Add((global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase)new global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T>());
				return result;
			}

			public override void Clear()
			{
				s_Pool.Clear();
			}

			public static T Get()
			{
				return s_Pool.Get();
			}

			public static void Release(T toRelease)
			{
				s_Pool.Release(toRelease);
			}
		}

		private static global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools = new global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase>();

		private global::System.Collections.Generic.Dictionary<(global::System.Type, int), global::System.Collections.Generic.Stack<object>> m_ArrayPool = new global::System.Collections.Generic.Dictionary<(global::System.Type, int), global::System.Collections.Generic.Stack<object>>();

		private global::System.Collections.Generic.List<(object, (global::System.Type, int))> m_AllocatedArrays = new global::System.Collections.Generic.List<(object, (global::System.Type, int))>();

		private global::System.Collections.Generic.List<global::UnityEngine.MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks = new global::System.Collections.Generic.List<global::UnityEngine.MaterialPropertyBlock>();

		internal RenderGraphObjectPool()
		{
		}

		public T[] GetTempArray<T>(int size)
		{
			if (!m_ArrayPool.TryGetValue((typeof(T), size), out var value))
			{
				value = new global::System.Collections.Generic.Stack<object>();
				m_ArrayPool.Add((typeof(T), size), value);
			}
			T[] array = ((value.Count > 0) ? ((T[])value.Pop()) : new T[size]);
			m_AllocatedArrays.Add((array, (typeof(T), size)));
			return array;
		}

		public global::UnityEngine.MaterialPropertyBlock GetTempMaterialPropertyBlock()
		{
			global::UnityEngine.MaterialPropertyBlock materialPropertyBlock = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<global::UnityEngine.MaterialPropertyBlock>.Get();
			materialPropertyBlock.Clear();
			m_AllocatedMaterialPropertyBlocks.Add(materialPropertyBlock);
			return materialPropertyBlock;
		}

		internal void ReleaseAllTempAlloc()
		{
			foreach (var allocatedArray in m_AllocatedArrays)
			{
				m_ArrayPool.TryGetValue(allocatedArray.Item2, out var value);
				value.Push(allocatedArray.Item1);
			}
			m_AllocatedArrays.Clear();
			foreach (global::UnityEngine.MaterialPropertyBlock allocatedMaterialPropertyBlock in m_AllocatedMaterialPropertyBlocks)
			{
				global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<global::UnityEngine.MaterialPropertyBlock>.Release(allocatedMaterialPropertyBlock);
			}
			m_AllocatedMaterialPropertyBlocks.Clear();
		}

		internal bool IsEmpty()
		{
			if (m_AllocatedArrays.Count == 0)
			{
				return m_AllocatedMaterialPropertyBlocks.Count == 0;
			}
			return false;
		}

		internal T Get<T>() where T : class, new()
		{
			return global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T>.Get();
		}

		internal void Release<T>(T value) where T : class, new()
		{
			global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T>.Release(value);
		}

		internal void Cleanup()
		{
			m_AllocatedArrays.Clear();
			m_AllocatedMaterialPropertyBlocks.Clear();
			m_ArrayPool.Clear();
			global::UnityEngine.Rendering.DynamicArray<global::UnityEngine.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase>.Iterator enumerator = s_AllocatedPools.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Clear();
			}
		}
	}
}

namespace UnityEngine.Rendering
{
	public class ContextContainer : global::System.IDisposable
	{
		private static class TypeId<T>
		{
			public static uint value = s_TypeCount++;
		}

		private struct Item
		{
			public global::UnityEngine.Rendering.ContextItem storage;

			public bool isSet;
		}

		private global::UnityEngine.Rendering.ContextContainer.Item[] m_Items = new global::UnityEngine.Rendering.ContextContainer.Item[64];

		private global::System.Collections.Generic.List<uint> m_ActiveItemIndices = new global::System.Collections.Generic.List<uint>();

		private static uint s_TypeCount;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public T Get<T>() where T : global::UnityEngine.Rendering.ContextItem, new()
		{
			uint value = global::UnityEngine.Rendering.ContextContainer.TypeId<T>.value;
			if (!Contains(value))
			{
				throw new global::System.InvalidOperationException("Type " + typeof(T).FullName + " has not been created yet.");
			}
			return (T)m_Items[value].storage;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public T Create<T>() where T : global::UnityEngine.Rendering.ContextItem, new()
		{
			uint value = global::UnityEngine.Rendering.ContextContainer.TypeId<T>.value;
			if (Contains(value))
			{
				throw new global::System.InvalidOperationException("Type " + typeof(T).FullName + " has already been created.");
			}
			return CreateAndGetData<T>(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public T GetOrCreate<T>() where T : global::UnityEngine.Rendering.ContextItem, new()
		{
			uint value = global::UnityEngine.Rendering.ContextContainer.TypeId<T>.value;
			if (Contains(value))
			{
				return (T)m_Items[value].storage;
			}
			return CreateAndGetData<T>(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Contains<T>() where T : global::UnityEngine.Rendering.ContextItem, new()
		{
			uint value = global::UnityEngine.Rendering.ContextContainer.TypeId<T>.value;
			return Contains(value);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private bool Contains(uint typeId)
		{
			if (typeId < m_Items.Length)
			{
				return m_Items[typeId].isSet;
			}
			return false;
		}

		private T CreateAndGetData<T>(uint typeId) where T : global::UnityEngine.Rendering.ContextItem, new()
		{
			if (m_Items.Length <= typeId)
			{
				global::UnityEngine.Rendering.ContextContainer.Item[] array = new global::UnityEngine.Rendering.ContextContainer.Item[global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.ceilpow2(s_TypeCount), m_Items.Length * 2)];
				for (int i = 0; i < m_Items.Length; i++)
				{
					array[i] = m_Items[i];
				}
				m_Items = array;
			}
			m_ActiveItemIndices.Add(typeId);
			ref global::UnityEngine.Rendering.ContextContainer.Item reference = ref m_Items[typeId];
			ref global::UnityEngine.Rendering.ContextItem storage = ref reference.storage;
			if (storage == null)
			{
				storage = new T();
			}
			reference.isSet = true;
			return (T)reference.storage;
		}

		public void Dispose()
		{
			foreach (uint activeItemIndex in m_ActiveItemIndices)
			{
				ref global::UnityEngine.Rendering.ContextContainer.Item reference = ref m_Items[activeItemIndex];
				reference.storage.Reset();
				reference.isSet = false;
			}
			m_ActiveItemIndices.Clear();
		}
	}
}

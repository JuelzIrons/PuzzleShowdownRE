namespace UnityEngine.Rendering
{
	public class ObjectPool<T> where T : new()
	{
		public struct PooledObject : global::System.IDisposable
		{
			private readonly T m_ToReturn;

			private readonly global::UnityEngine.Rendering.ObjectPool<T> m_Pool;

			internal PooledObject(T value, global::UnityEngine.Rendering.ObjectPool<T> pool)
			{
				m_ToReturn = value;
				m_Pool = pool;
			}

			void global::System.IDisposable.Dispose()
			{
				m_Pool.Release(m_ToReturn);
			}
		}

		private readonly global::System.Collections.Generic.Stack<T> m_Stack = new global::System.Collections.Generic.Stack<T>();

		private readonly global::UnityEngine.Events.UnityAction<T> m_ActionOnGet;

		private readonly global::UnityEngine.Events.UnityAction<T> m_ActionOnRelease;

		private readonly bool m_CollectionCheck = true;

		public int countAll { get; private set; }

		public int countActive => countAll - countInactive;

		public int countInactive => m_Stack.Count;

		public ObjectPool(global::UnityEngine.Events.UnityAction<T> actionOnGet, global::UnityEngine.Events.UnityAction<T> actionOnRelease, bool collectionCheck = true)
		{
			m_ActionOnGet = actionOnGet;
			m_ActionOnRelease = actionOnRelease;
			m_CollectionCheck = collectionCheck;
		}

		public T Get()
		{
			T val;
			if (m_Stack.Count == 0)
			{
				val = new T();
				countAll++;
			}
			else
			{
				val = m_Stack.Pop();
			}
			if (m_ActionOnGet != null)
			{
				m_ActionOnGet(val);
			}
			return val;
		}

		public global::UnityEngine.Rendering.ObjectPool<T>.PooledObject Get(out T v)
		{
			return new global::UnityEngine.Rendering.ObjectPool<T>.PooledObject(v = Get(), this);
		}

		public void Release(T element)
		{
			if (m_ActionOnRelease != null)
			{
				m_ActionOnRelease(element);
			}
			m_Stack.Push(element);
		}
	}
}

namespace Unity.VisualScripting
{
	public struct NoAllocEnumerator<T> : global::System.Collections.Generic.IEnumerator<T>, global::System.Collections.IEnumerator, global::System.IDisposable
	{
		private readonly global::System.Collections.Generic.IList<T> list;

		private int index;

		private T current;

		private bool exceeded;

		public T Current => current;

		object global::System.Collections.IEnumerator.Current
		{
			get
			{
				if (exceeded)
				{
					throw new global::System.InvalidOperationException();
				}
				return Current;
			}
		}

		public NoAllocEnumerator(global::System.Collections.Generic.IList<T> list)
		{
			this = default(global::Unity.VisualScripting.NoAllocEnumerator<T>);
			this.list = list;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			if (index < list.Count)
			{
				current = list[index];
				index++;
				return true;
			}
			index = list.Count + 1;
			current = default(T);
			exceeded = true;
			return false;
		}

		void global::System.Collections.IEnumerator.Reset()
		{
			throw new global::System.InvalidOperationException();
		}
	}
}

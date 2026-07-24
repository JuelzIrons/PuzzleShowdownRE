namespace UnityEngine.InputSystem.Utilities
{
	internal struct OneOrMore<TValue, TList> : global::System.Collections.Generic.IReadOnlyList<TValue>, global::System.Collections.Generic.IEnumerable<TValue>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IReadOnlyCollection<TValue> where TList : global::System.Collections.Generic.IReadOnlyList<TValue>
	{
		private class Enumerator : global::System.Collections.Generic.IEnumerator<TValue>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			internal int m_Index = -1;

			internal global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList> m_List;

			public TValue Current => m_List[m_Index];

			object global::System.Collections.IEnumerator.Current => Current;

			public bool MoveNext()
			{
				m_Index++;
				if (m_Index >= m_List.Count)
				{
					return false;
				}
				return true;
			}

			public void Reset()
			{
				m_Index = -1;
			}

			public void Dispose()
			{
			}
		}

		private readonly bool m_IsSingle;

		private readonly TValue m_Single;

		private readonly TList m_Multiple;

		public int Count
		{
			get
			{
				if (!m_IsSingle)
				{
					return m_Multiple.Count;
				}
				return 1;
			}
		}

		public TValue this[int index]
		{
			get
			{
				if (!m_IsSingle)
				{
					return m_Multiple[index];
				}
				if (index < 0 || index > 1)
				{
					throw new global::System.ArgumentOutOfRangeException("index");
				}
				return m_Single;
			}
		}

		public OneOrMore(TValue single)
		{
			m_IsSingle = true;
			m_Single = single;
			m_Multiple = default(TList);
		}

		public OneOrMore(TList multiple)
		{
			m_IsSingle = false;
			m_Single = default(TValue);
			m_Multiple = multiple;
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList>(TValue single)
		{
			return new global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList>(single);
		}

		public static implicit operator global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList>(TList multiple)
		{
			return new global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList>(multiple);
		}

		public global::System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.Utilities.OneOrMore<TValue, TList>.Enumerator
			{
				m_List = this
			};
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

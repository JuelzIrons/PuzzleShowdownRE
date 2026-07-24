namespace UnityEngine.InputSystem.EnhancedTouch
{
	public struct TouchHistory : global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.InputSystem.EnhancedTouch.Touch>, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.EnhancedTouch.Touch>, global::System.Collections.IEnumerable, global::System.Collections.Generic.IReadOnlyCollection<global::UnityEngine.InputSystem.EnhancedTouch.Touch>
	{
		private class Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.EnhancedTouch.Touch>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private readonly global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory m_Owner;

			private int m_Index;

			public global::UnityEngine.InputSystem.EnhancedTouch.Touch Current => m_Owner[m_Index];

			object global::System.Collections.IEnumerator.Current => Current;

			internal Enumerator(global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory owner)
			{
				m_Owner = owner;
				m_Index = -1;
			}

			public bool MoveNext()
			{
				if (m_Index >= m_Owner.Count - 1)
				{
					return false;
				}
				m_Index++;
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

		private readonly global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState> m_History;

		private readonly global::UnityEngine.InputSystem.EnhancedTouch.Finger m_Finger;

		private readonly int m_Count;

		private readonly int m_StartIndex;

		private readonly uint m_Version;

		public int Count => m_Count;

		public global::UnityEngine.InputSystem.EnhancedTouch.Touch this[int index]
		{
			get
			{
				CheckValid();
				if (index < 0 || index >= Count)
				{
					throw new global::System.ArgumentOutOfRangeException($"Index {index} is out of range for history with {Count} entries", "index");
				}
				return new global::UnityEngine.InputSystem.EnhancedTouch.Touch(m_Finger, m_History[m_StartIndex - index]);
			}
		}

		internal TouchHistory(global::UnityEngine.InputSystem.EnhancedTouch.Finger finger, global::UnityEngine.InputSystem.LowLevel.InputStateHistory<global::UnityEngine.InputSystem.LowLevel.TouchState> history, int startIndex = -1, int count = -1)
		{
			m_Finger = finger;
			m_History = history;
			m_Version = history.version;
			m_Count = ((count >= 0) ? count : m_History.Count);
			m_StartIndex = ((startIndex >= 0) ? startIndex : (m_History.Count - 1));
		}

		public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.EnhancedTouch.Touch> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.EnhancedTouch.TouchHistory.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void CheckValid()
		{
			if (m_Finger == null || m_History == null)
			{
				throw new global::System.InvalidOperationException("Touch history not initialized");
			}
			if (m_History.version != m_Version)
			{
				throw new global::System.InvalidOperationException("Touch history is no longer valid; the recorded history has been changed");
			}
		}
	}
}

namespace UnityEngine.InputSystem.LowLevel
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit, Size = 132)]
	public struct IMECompositionString : global::System.Collections.Generic.IEnumerable<char>, global::System.Collections.IEnumerable
	{
		internal struct Enumerator : global::System.Collections.Generic.IEnumerator<char>, global::System.Collections.IEnumerator, global::System.IDisposable
		{
			private global::UnityEngine.InputSystem.LowLevel.IMECompositionString m_CompositionString;

			private char m_CurrentCharacter;

			private int m_CurrentIndex;

			public char Current => m_CurrentCharacter;

			object global::System.Collections.IEnumerator.Current => Current;

			public Enumerator(global::UnityEngine.InputSystem.LowLevel.IMECompositionString compositionString)
			{
				m_CompositionString = compositionString;
				m_CurrentCharacter = '\0';
				m_CurrentIndex = -1;
			}

			public unsafe bool MoveNext()
			{
				int count = m_CompositionString.Count;
				m_CurrentIndex++;
				if (m_CurrentIndex == count)
				{
					return false;
				}
				fixed (char* buffer = m_CompositionString.buffer)
				{
					m_CurrentCharacter = buffer[m_CurrentIndex];
				}
				return true;
			}

			public void Reset()
			{
				m_CurrentIndex = -1;
			}

			public void Dispose()
			{
			}
		}

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		private int size;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		private unsafe fixed char buffer[64];

		public int Count => size;

		public unsafe char this[int index]
		{
			get
			{
				if (index >= Count || index < 0)
				{
					throw new global::System.ArgumentOutOfRangeException("index");
				}
				fixed (char* ptr = buffer)
				{
					return ptr[index];
				}
			}
		}

		public unsafe IMECompositionString(string characters)
		{
			if (string.IsNullOrEmpty(characters))
			{
				size = 0;
				return;
			}
			size = characters.Length;
			for (int i = 0; i < size; i++)
			{
				buffer[i] = characters[i];
			}
		}

		public unsafe override string ToString()
		{
			fixed (char* value = buffer)
			{
				return new string(value, 0, size);
			}
		}

		public global::System.Collections.Generic.IEnumerator<char> GetEnumerator()
		{
			return new global::UnityEngine.InputSystem.LowLevel.IMECompositionString.Enumerator(this);
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

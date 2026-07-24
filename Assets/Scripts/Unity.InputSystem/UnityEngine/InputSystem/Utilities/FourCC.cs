namespace UnityEngine.InputSystem.Utilities
{
	public struct FourCC : global::System.IEquatable<global::UnityEngine.InputSystem.Utilities.FourCC>
	{
		private int m_Code;

		public FourCC(int code)
		{
			m_Code = code;
		}

		public FourCC(char a, char b = ' ', char c = ' ', char d = ' ')
		{
			m_Code = (int)(((uint)a << 24) | ((uint)b << 16) | ((uint)c << 8) | d);
		}

		public FourCC(string str)
		{
			this = default(global::UnityEngine.InputSystem.Utilities.FourCC);
			if (str == null)
			{
				throw new global::System.ArgumentNullException("str");
			}
			int length = str.Length;
			if (length < 1 || length > 4)
			{
				throw new global::System.ArgumentException("FourCC string must be one to four characters long!", "str");
			}
			char c = str[0];
			char c2 = ((length > 1) ? str[1] : ' ');
			char c3 = ((length > 2) ? str[2] : ' ');
			char c4 = ((length > 3) ? str[3] : ' ');
			m_Code = (int)(((uint)c << 24) | ((uint)c2 << 16) | ((uint)c3 << 8) | c4);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(global::UnityEngine.InputSystem.Utilities.FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static implicit operator global::UnityEngine.InputSystem.Utilities.FourCC(int i)
		{
			return new global::UnityEngine.InputSystem.Utilities.FourCC(i);
		}

		public override string ToString()
		{
			return $"{(char)(m_Code >> 24)}{(char)((m_Code & 0xFF0000) >> 16)}{(char)((m_Code & 0xFF00) >> 8)}{(char)(m_Code & 0xFF)}";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::UnityEngine.InputSystem.Utilities.FourCC other)
		{
			return m_Code == other.m_Code;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Utilities.FourCC other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return m_Code;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(global::UnityEngine.InputSystem.Utilities.FourCC left, global::UnityEngine.InputSystem.Utilities.FourCC right)
		{
			return left.m_Code == right.m_Code;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(global::UnityEngine.InputSystem.Utilities.FourCC left, global::UnityEngine.InputSystem.Utilities.FourCC right)
		{
			return left.m_Code != right.m_Code;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::UnityEngine.InputSystem.Utilities.FourCC FromInt32(int i)
		{
			return i;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static int ToInt32(global::UnityEngine.InputSystem.Utilities.FourCC fourCC)
		{
			return fourCC.m_Code;
		}
	}
}

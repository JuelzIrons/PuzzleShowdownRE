namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	public struct BitArray128 : global::UnityEngine.Rendering.IBitArray
	{
		[global::UnityEngine.SerializeField]
		private ulong data1;

		[global::UnityEngine.SerializeField]
		private ulong data2;

		public uint capacity => 128u;

		public bool allFalse
		{
			get
			{
				if (data1 == 0L)
				{
					return data2 == 0;
				}
				return false;
			}
		}

		public bool allTrue
		{
			get
			{
				if (data1 == ulong.MaxValue)
				{
					return data2 == ulong.MaxValue;
				}
				return false;
			}
		}

		public string humanizedData => global::System.Text.RegularExpressions.Regex.Replace(string.Format("{0, " + 64u + "}", global::System.Convert.ToString((long)data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + global::System.Text.RegularExpressions.Regex.Replace(string.Format("{0, " + 64u + "}", global::System.Convert.ToString((long)data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');

		public bool this[uint index]
		{
			get
			{
				if (index >= 64)
				{
					return (data2 & (ulong)(1L << (int)(index - 64))) != 0;
				}
				return (data1 & (ulong)(1L << (int)index)) != 0;
			}
			set
			{
				if (index < 64)
				{
					data1 = (value ? (data1 | (ulong)(1L << (int)index)) : (data1 & (ulong)(~(1L << (int)index))));
				}
				else
				{
					data2 = (value ? (data2 | (ulong)(1L << (int)(index - 64))) : (data2 & (ulong)(~(1L << (int)(index - 64)))));
				}
			}
		}

		public BitArray128(ulong initValue1, ulong initValue2)
		{
			data1 = initValue1;
			data2 = initValue2;
		}

		public BitArray128(global::System.Collections.Generic.IEnumerable<uint> bitIndexTrue)
		{
			data1 = (data2 = 0uL);
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int num = global::System.Linq.Enumerable.Count(bitIndexTrue) - 1; num >= 0; num--)
			{
				uint num2 = global::System.Linq.Enumerable.ElementAt(bitIndexTrue, num);
				if (num2 < 64)
				{
					data1 |= (ulong)(1L << (int)num2);
				}
				else if (num2 < capacity)
				{
					data2 |= (ulong)(1L << (int)(num2 - 64));
				}
			}
		}

		public static global::UnityEngine.Rendering.BitArray128 operator ~(global::UnityEngine.Rendering.BitArray128 a)
		{
			return new global::UnityEngine.Rendering.BitArray128(~a.data1, ~a.data2);
		}

		public static global::UnityEngine.Rendering.BitArray128 operator |(global::UnityEngine.Rendering.BitArray128 a, global::UnityEngine.Rendering.BitArray128 b)
		{
			return new global::UnityEngine.Rendering.BitArray128(a.data1 | b.data1, a.data2 | b.data2);
		}

		public static global::UnityEngine.Rendering.BitArray128 operator &(global::UnityEngine.Rendering.BitArray128 a, global::UnityEngine.Rendering.BitArray128 b)
		{
			return new global::UnityEngine.Rendering.BitArray128(a.data1 & b.data1, a.data2 & b.data2);
		}

		public global::UnityEngine.Rendering.IBitArray BitAnd(global::UnityEngine.Rendering.IBitArray other)
		{
			return this & (global::UnityEngine.Rendering.BitArray128)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitOr(global::UnityEngine.Rendering.IBitArray other)
		{
			return this | (global::UnityEngine.Rendering.BitArray128)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitNot()
		{
			return ~this;
		}

		public static bool operator ==(global::UnityEngine.Rendering.BitArray128 a, global::UnityEngine.Rendering.BitArray128 b)
		{
			if (a.data1 == b.data1)
			{
				return a.data2 == b.data2;
			}
			return false;
		}

		public static bool operator !=(global::UnityEngine.Rendering.BitArray128 a, global::UnityEngine.Rendering.BitArray128 b)
		{
			if (a.data1 == b.data1)
			{
				return a.data2 != b.data2;
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.BitArray128 bitArray && data1.Equals(bitArray.data1))
			{
				return data2.Equals(bitArray.data2);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (1755735569 * -1521134295 + data1.GetHashCode()) * -1521134295 + data2.GetHashCode();
		}
	}
}

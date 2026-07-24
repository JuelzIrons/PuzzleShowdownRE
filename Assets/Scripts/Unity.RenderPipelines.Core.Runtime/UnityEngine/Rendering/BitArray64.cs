namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	public struct BitArray64 : global::UnityEngine.Rendering.IBitArray
	{
		[global::UnityEngine.SerializeField]
		private ulong data;

		public uint capacity => 64u;

		public bool allFalse => data == 0;

		public bool allTrue => data == ulong.MaxValue;

		public string humanizedData => global::System.Text.RegularExpressions.Regex.Replace(string.Format("{0, " + capacity + "}", global::System.Convert.ToString((long)data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');

		public bool this[uint index]
		{
			get
			{
				return global::UnityEngine.Rendering.BitArrayUtilities.Get64(index, data);
			}
			set
			{
				global::UnityEngine.Rendering.BitArrayUtilities.Set64(index, ref data, value);
			}
		}

		public BitArray64(ulong initValue)
		{
			data = initValue;
		}

		public BitArray64(global::System.Collections.Generic.IEnumerable<uint> bitIndexTrue)
		{
			data = 0uL;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int num = global::System.Linq.Enumerable.Count(bitIndexTrue) - 1; num >= 0; num--)
			{
				uint num2 = global::System.Linq.Enumerable.ElementAt(bitIndexTrue, num);
				if (num2 < capacity)
				{
					data |= (ulong)(1L << (int)num2);
				}
			}
		}

		public static global::UnityEngine.Rendering.BitArray64 operator ~(global::UnityEngine.Rendering.BitArray64 a)
		{
			return new global::UnityEngine.Rendering.BitArray64(~a.data);
		}

		public static global::UnityEngine.Rendering.BitArray64 operator |(global::UnityEngine.Rendering.BitArray64 a, global::UnityEngine.Rendering.BitArray64 b)
		{
			return new global::UnityEngine.Rendering.BitArray64(a.data | b.data);
		}

		public static global::UnityEngine.Rendering.BitArray64 operator &(global::UnityEngine.Rendering.BitArray64 a, global::UnityEngine.Rendering.BitArray64 b)
		{
			return new global::UnityEngine.Rendering.BitArray64(a.data & b.data);
		}

		public global::UnityEngine.Rendering.IBitArray BitAnd(global::UnityEngine.Rendering.IBitArray other)
		{
			return this & (global::UnityEngine.Rendering.BitArray64)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitOr(global::UnityEngine.Rendering.IBitArray other)
		{
			return this | (global::UnityEngine.Rendering.BitArray64)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitNot()
		{
			return ~this;
		}

		public static bool operator ==(global::UnityEngine.Rendering.BitArray64 a, global::UnityEngine.Rendering.BitArray64 b)
		{
			return a.data == b.data;
		}

		public static bool operator !=(global::UnityEngine.Rendering.BitArray64 a, global::UnityEngine.Rendering.BitArray64 b)
		{
			return a.data != b.data;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.BitArray64 bitArray)
			{
				return bitArray.data == data;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return 1768953197 + data.GetHashCode();
		}
	}
}

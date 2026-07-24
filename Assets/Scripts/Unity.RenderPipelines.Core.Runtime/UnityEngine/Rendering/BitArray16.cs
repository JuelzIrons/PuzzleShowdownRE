namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	public struct BitArray16 : global::UnityEngine.Rendering.IBitArray
	{
		[global::UnityEngine.SerializeField]
		private ushort data;

		public uint capacity => 16u;

		public bool allFalse => data == 0;

		public bool allTrue => data == ushort.MaxValue;

		public string humanizedData => global::System.Text.RegularExpressions.Regex.Replace(string.Format("{0, " + capacity + "}", global::System.Convert.ToString(data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');

		public bool this[uint index]
		{
			get
			{
				return global::UnityEngine.Rendering.BitArrayUtilities.Get16(index, data);
			}
			set
			{
				global::UnityEngine.Rendering.BitArrayUtilities.Set16(index, ref data, value);
			}
		}

		public BitArray16(ushort initValue)
		{
			data = initValue;
		}

		public BitArray16(global::System.Collections.Generic.IEnumerable<uint> bitIndexTrue)
		{
			data = 0;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int num = global::System.Linq.Enumerable.Count(bitIndexTrue) - 1; num >= 0; num--)
			{
				uint num2 = global::System.Linq.Enumerable.ElementAt(bitIndexTrue, num);
				if (num2 < capacity)
				{
					data |= (ushort)(1 << (int)num2);
				}
			}
		}

		public static global::UnityEngine.Rendering.BitArray16 operator ~(global::UnityEngine.Rendering.BitArray16 a)
		{
			return new global::UnityEngine.Rendering.BitArray16((ushort)(~a.data));
		}

		public static global::UnityEngine.Rendering.BitArray16 operator |(global::UnityEngine.Rendering.BitArray16 a, global::UnityEngine.Rendering.BitArray16 b)
		{
			return new global::UnityEngine.Rendering.BitArray16((ushort)(a.data | b.data));
		}

		public static global::UnityEngine.Rendering.BitArray16 operator &(global::UnityEngine.Rendering.BitArray16 a, global::UnityEngine.Rendering.BitArray16 b)
		{
			return new global::UnityEngine.Rendering.BitArray16((ushort)(a.data & b.data));
		}

		public global::UnityEngine.Rendering.IBitArray BitAnd(global::UnityEngine.Rendering.IBitArray other)
		{
			return this & (global::UnityEngine.Rendering.BitArray16)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitOr(global::UnityEngine.Rendering.IBitArray other)
		{
			return this | (global::UnityEngine.Rendering.BitArray16)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitNot()
		{
			return ~this;
		}

		public static bool operator ==(global::UnityEngine.Rendering.BitArray16 a, global::UnityEngine.Rendering.BitArray16 b)
		{
			return a.data == b.data;
		}

		public static bool operator !=(global::UnityEngine.Rendering.BitArray16 a, global::UnityEngine.Rendering.BitArray16 b)
		{
			return a.data != b.data;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.BitArray16 bitArray)
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

namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	public struct BitArray32 : global::UnityEngine.Rendering.IBitArray
	{
		[global::UnityEngine.SerializeField]
		private uint data;

		public uint capacity => 32u;

		public bool allFalse => data == 0;

		public bool allTrue => data == uint.MaxValue;

		private string humanizedVersion => global::System.Convert.ToString(data, 2);

		public string humanizedData => global::System.Text.RegularExpressions.Regex.Replace(string.Format("{0, " + capacity + "}", global::System.Convert.ToString(data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');

		public bool this[uint index]
		{
			get
			{
				return global::UnityEngine.Rendering.BitArrayUtilities.Get32(index, data);
			}
			set
			{
				global::UnityEngine.Rendering.BitArrayUtilities.Set32(index, ref data, value);
			}
		}

		public BitArray32(uint initValue)
		{
			data = initValue;
		}

		public BitArray32(global::System.Collections.Generic.IEnumerable<uint> bitIndexTrue)
		{
			data = 0u;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int num = global::System.Linq.Enumerable.Count(bitIndexTrue) - 1; num >= 0; num--)
			{
				uint num2 = global::System.Linq.Enumerable.ElementAt(bitIndexTrue, num);
				if (num2 < capacity)
				{
					data |= (uint)(1 << (int)num2);
				}
			}
		}

		public global::UnityEngine.Rendering.IBitArray BitAnd(global::UnityEngine.Rendering.IBitArray other)
		{
			return this & (global::UnityEngine.Rendering.BitArray32)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitOr(global::UnityEngine.Rendering.IBitArray other)
		{
			return this | (global::UnityEngine.Rendering.BitArray32)(object)other;
		}

		public global::UnityEngine.Rendering.IBitArray BitNot()
		{
			return ~this;
		}

		public static global::UnityEngine.Rendering.BitArray32 operator ~(global::UnityEngine.Rendering.BitArray32 a)
		{
			return new global::UnityEngine.Rendering.BitArray32(~a.data);
		}

		public static global::UnityEngine.Rendering.BitArray32 operator |(global::UnityEngine.Rendering.BitArray32 a, global::UnityEngine.Rendering.BitArray32 b)
		{
			return new global::UnityEngine.Rendering.BitArray32(a.data | b.data);
		}

		public static global::UnityEngine.Rendering.BitArray32 operator &(global::UnityEngine.Rendering.BitArray32 a, global::UnityEngine.Rendering.BitArray32 b)
		{
			return new global::UnityEngine.Rendering.BitArray32(a.data & b.data);
		}

		public static bool operator ==(global::UnityEngine.Rendering.BitArray32 a, global::UnityEngine.Rendering.BitArray32 b)
		{
			return a.data == b.data;
		}

		public static bool operator !=(global::UnityEngine.Rendering.BitArray32 a, global::UnityEngine.Rendering.BitArray32 b)
		{
			return a.data != b.data;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Rendering.BitArray32 bitArray)
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

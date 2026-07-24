namespace Unity.Collections
{
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::Unity.Collections.BitField64DebugView))]
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct BitField64
	{
		public ulong Value;

		public BitField64(ulong initialValue = 0uL)
		{
			Value = initialValue;
		}

		public void Clear()
		{
			Value = 0uL;
		}

		public void SetBits(int pos, bool value)
		{
			Value = global::Unity.Collections.Bitwise.SetBits(Value, pos, 1uL, value);
		}

		public void SetBits(int pos, bool value, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			Value = global::Unity.Collections.Bitwise.SetBits(Value, pos, mask, value);
		}

		public ulong GetBits(int pos, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			return global::Unity.Collections.Bitwise.ExtractBits(Value, pos, mask);
		}

		public bool IsSet(int pos)
		{
			return GetBits(pos) != 0;
		}

		public bool TestNone(int pos, int numBits = 1)
		{
			return GetBits(pos, numBits) == 0;
		}

		public bool TestAny(int pos, int numBits = 1)
		{
			return GetBits(pos, numBits) != 0;
		}

		public bool TestAll(int pos, int numBits = 1)
		{
			ulong num = ulong.MaxValue >> 64 - numBits;
			return num == global::Unity.Collections.Bitwise.ExtractBits(Value, pos, num);
		}

		public int CountBits()
		{
			return global::Unity.Mathematics.math.countbits(Value);
		}

		public int CountLeadingZeros()
		{
			return global::Unity.Mathematics.math.lzcnt(Value);
		}

		public int CountTrailingZeros()
		{
			return global::Unity.Mathematics.math.tzcnt(Value);
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[global::System.Diagnostics.Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 63 || numBits == 0 || numBits > 64 || pos + numBits > 64)
			{
				throw new global::System.ArgumentException($"BitField32 invalid arguments: pos {pos} (must be 0-63), numBits {numBits} (must be 1-64).");
			}
		}
	}
}

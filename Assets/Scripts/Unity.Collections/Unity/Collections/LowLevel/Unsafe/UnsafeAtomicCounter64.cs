namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct UnsafeAtomicCounter64
	{
		public unsafe long* Counter;

		public unsafe UnsafeAtomicCounter64(void* ptr)
		{
			Counter = (long*)ptr;
		}

		public unsafe void Reset(long value = 0L)
		{
			*Counter = value;
		}

		public unsafe long Add(long value)
		{
			return global::System.Threading.Interlocked.Add(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<long>(Counter), value) - value;
		}

		public long Sub(long value)
		{
			return Add(-value);
		}

		public unsafe long AddSat(long value, long max = long.MaxValue)
		{
			long num = *Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num >= max) ? max : global::Unity.Mathematics.math.min(max, num + value));
				num = global::System.Threading.Interlocked.CompareExchange(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<long>(Counter), num, num2);
			}
			while (num2 != num && num2 != max);
			return num2;
		}

		public unsafe long SubSat(long value, long min = long.MinValue)
		{
			long num = *Counter;
			long num2;
			do
			{
				num2 = num;
				num = ((num <= min) ? min : global::Unity.Mathematics.math.max(min, num - value));
				num = global::System.Threading.Interlocked.CompareExchange(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<long>(Counter), num, num2);
			}
			while (num2 != num && num2 != min);
			return num2;
		}
	}
}

namespace Unity.Collections.LowLevel.Unsafe
{
	[global::Unity.Collections.GenerateTestsForBurstCompatibility]
	public struct UnsafeAtomicCounter32
	{
		public unsafe int* Counter;

		public unsafe UnsafeAtomicCounter32(void* ptr)
		{
			Counter = (int*)ptr;
		}

		public unsafe void Reset(int value = 0)
		{
			*Counter = value;
		}

		public unsafe int Add(int value)
		{
			return global::System.Threading.Interlocked.Add(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(Counter), value) - value;
		}

		public int Sub(int value)
		{
			return Add(-value);
		}

		public unsafe int AddSat(int value, int max = int.MaxValue)
		{
			int num = *Counter;
			int num2;
			do
			{
				num2 = num;
				num = ((num >= max) ? max : global::Unity.Mathematics.math.min(max, num + value));
				num = global::System.Threading.Interlocked.CompareExchange(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(Counter), num, num2);
			}
			while (num2 != num && num2 != max);
			return num2;
		}

		public unsafe int SubSat(int value, int min = int.MinValue)
		{
			int num = *Counter;
			int num2;
			do
			{
				num2 = num;
				num = ((num <= min) ? min : global::Unity.Mathematics.math.max(min, num - value));
				num = global::System.Threading.Interlocked.CompareExchange(ref global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<int>(Counter), num, num2);
			}
			while (num2 != num && num2 != min);
			return num2;
		}
	}
}

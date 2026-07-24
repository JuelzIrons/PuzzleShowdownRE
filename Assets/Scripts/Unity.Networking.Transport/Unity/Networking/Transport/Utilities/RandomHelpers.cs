namespace Unity.Networking.Transport.Utilities
{
	internal static class RandomHelpers
	{
		private class SharedRandomKey
		{
		}

		private static readonly global::Unity.Burst.SharedStatic<long> s_SharedSeed;

		static RandomHelpers()
		{
			s_SharedSeed = global::Unity.Burst.SharedStatic<long>.GetOrCreateUnsafe(16u, 1093067541219126605L, 0L);
			s_SharedSeed.Data = 0L;
		}

		internal static global::Unity.Mathematics.Random GetRandomGenerator()
		{
			if (s_SharedSeed.Data == 0L)
			{
				global::System.Threading.Interlocked.CompareExchange(ref s_SharedSeed.Data, (long)global::Unity.Networking.Transport.Utilities.TimerHelpers.GetTicks(), 0L);
			}
			return new global::Unity.Mathematics.Random((uint)(global::System.Threading.Interlocked.Increment(ref s_SharedSeed.Data) % uint.MaxValue + 1));
		}

		internal static ushort GetRandomUShort()
		{
			return (ushort)GetRandomGenerator().NextUInt(1u, 65534u);
		}

		internal unsafe static global::Unity.Networking.Transport.ConnectionToken GetRandomConnectionToken()
		{
			global::Unity.Networking.Transport.ConnectionToken result = default(global::Unity.Networking.Transport.ConnectionToken);
			global::Unity.Mathematics.Random randomGenerator = GetRandomGenerator();
			for (int i = 0; i < 8; i++)
			{
				result.Value[i] = (byte)(randomGenerator.NextUInt() & 0xFF);
			}
			return result;
		}
	}
}

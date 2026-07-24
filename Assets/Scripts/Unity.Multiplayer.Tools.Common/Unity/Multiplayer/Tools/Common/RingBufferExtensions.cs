namespace Unity.Multiplayer.Tools.Common
{
	internal static class RingBufferExtensions
	{
		public static int Max(this global::Unity.Multiplayer.Tools.Common.RingBuffer<int> ring)
		{
			int num = 0;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num = global::System.Math.Max(num, ring[i]);
			}
			return num;
		}

		public static long Max(this global::Unity.Multiplayer.Tools.Common.RingBuffer<long> ring)
		{
			long num = 0L;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num = global::System.Math.Max(num, ring[i]);
			}
			return num;
		}

		public static float Max(this global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ring)
		{
			float num = 0f;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num = global::System.Math.Max(num, ring[i]);
			}
			return num;
		}

		public static int Sum(this global::Unity.Multiplayer.Tools.Common.RingBuffer<int> ring)
		{
			int num = 0;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static long Sum(this global::Unity.Multiplayer.Tools.Common.RingBuffer<long> ring)
		{
			long num = 0L;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static float Sum(this global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ring)
		{
			float num = 0f;
			int length = ring.Length;
			for (int i = 0; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static int SumLastN(this global::Unity.Multiplayer.Tools.Common.RingBuffer<int> ring, int n)
		{
			int num = 0;
			int length = ring.Length;
			for (int i = length - n; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static long SumLastN(this global::Unity.Multiplayer.Tools.Common.RingBuffer<long> ring, int n)
		{
			long num = 0L;
			int length = ring.Length;
			for (int i = length - n; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static float SumLastN(this global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ring, int n)
		{
			float num = 0f;
			int length = ring.Length;
			for (int i = length - n; i < length; i++)
			{
				num += ring[i];
			}
			return num;
		}

		public static float Average(this global::Unity.Multiplayer.Tools.Common.RingBuffer<int> ring)
		{
			return (float)ring.Sum() / (float)ring.Length;
		}

		public static float Average(this global::Unity.Multiplayer.Tools.Common.RingBuffer<long> ring)
		{
			return (float)ring.Sum() / (float)ring.Length;
		}

		public static float Average(this global::Unity.Multiplayer.Tools.Common.RingBuffer<float> ring)
		{
			return ring.Sum() / (float)ring.Length;
		}
	}
}

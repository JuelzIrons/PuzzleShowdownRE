namespace Newtonsoft.Json.Utilities
{
	internal static class BufferUtils
	{
		public static char[] RentBuffer(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, int minSize)
		{
			if (bufferPool == null)
			{
				return new char[minSize];
			}
			return bufferPool.Rent(minSize);
		}

		public static void ReturnBuffer(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, char[]? buffer)
		{
			bufferPool?.Return(buffer);
		}

		public static char[] EnsureBufferSize(global::Newtonsoft.Json.IArrayPool<char>? bufferPool, int size, char[]? buffer)
		{
			if (bufferPool == null)
			{
				return new char[size];
			}
			if (buffer != null)
			{
				bufferPool.Return(buffer);
			}
			return bufferPool.Rent(size);
		}
	}
}

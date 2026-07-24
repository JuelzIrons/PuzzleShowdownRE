namespace Unity.Networking.Transport.TLS
{
	public struct FixedPEMString
	{
		private const int k_BufferLength = 16384;

		private unsafe fixed byte m_Buffer[16384];

		private int m_Length;

		public const int MaxLength = 16383;

		public int Length => m_Length;

		public unsafe FixedPEMString(string pem)
		{
			byte[] bytes = global::System.Text.Encoding.ASCII.GetBytes(pem);
			if (bytes.Length > 16383)
			{
				throw new global::System.ArgumentException(string.Format("String is too large to fit in {0} (length: {1}, capacity: {2}).", "FixedPEMString", bytes.Length, 16383));
			}
			fixed (byte* source = bytes)
			{
				fixed (byte* buffer = m_Buffer)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, source, bytes.Length);
					buffer[bytes.Length] = 0;
				}
			}
			m_Length = bytes.Length;
		}

		internal unsafe FixedPEMString(ref global::Unity.Collections.FixedString4096Bytes pem)
		{
			fixed (byte* buffer = m_Buffer)
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, pem.GetUnsafePtr(), pem.Length);
				buffer[pem.Length] = 0;
			}
			m_Length = pem.Length;
		}

		internal unsafe byte* GetUnsafePtr()
		{
			fixed (byte* buffer = m_Buffer)
			{
				return buffer;
			}
		}
	}
}

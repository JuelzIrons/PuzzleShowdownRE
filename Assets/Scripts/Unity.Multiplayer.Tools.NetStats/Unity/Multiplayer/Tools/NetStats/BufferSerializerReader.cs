namespace Unity.Multiplayer.Tools.NetStats
{
	internal struct BufferSerializerReader : global::Unity.Multiplayer.Tools.NetStats.IReaderWriter
	{
		private global::Unity.Multiplayer.Tools.NetStats.FastBufferReader m_Reader;

		public bool IsReader => true;

		public bool IsWriter => false;

		public BufferSerializerReader(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader reader)
		{
			m_Reader = reader;
		}

		public global::Unity.Multiplayer.Tools.NetStats.FastBufferReader GetFastBufferReader()
		{
			return m_Reader;
		}

		public global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter GetFastBufferWriter()
		{
			throw new global::System.InvalidOperationException("Cannot retrieve a FastBufferWriter from a serializer where IsWriter = false");
		}

		public void SerializeValue(ref string s, bool oneByteChars = false)
		{
			m_Reader.ReadValueSafe(out s, oneByteChars);
		}

		public void SerializeValue<T>(ref T[] array) where T : unmanaged
		{
			m_Reader.ReadValueSafe(out array);
		}

		public void SerializeValue(ref byte value)
		{
			m_Reader.ReadByteSafe(out value);
		}

		public void SerializeValue<T>(ref T value) where T : unmanaged
		{
			m_Reader.ReadValueSafe(out value);
		}

		public void SerializeNetworkSerializable<T>(ref T value) where T : global::Unity.Multiplayer.Tools.NetStats.INetworkSerializable, new()
		{
			m_Reader.ReadNetworkSerializable(out value);
		}

		public bool PreCheck(int amount)
		{
			return m_Reader.TryBeginRead(amount);
		}

		public void SerializeValuePreChecked(ref string s, bool oneByteChars = false)
		{
			m_Reader.ReadValue(out s, oneByteChars);
		}

		public void SerializeValuePreChecked<T>(ref T[] array) where T : unmanaged
		{
			m_Reader.ReadValue(out array);
		}

		public void SerializeValuePreChecked(ref byte value)
		{
			m_Reader.ReadValue(out value);
		}

		public void SerializeValuePreChecked<T>(ref T value) where T : unmanaged
		{
			m_Reader.ReadValue(out value);
		}
	}
}

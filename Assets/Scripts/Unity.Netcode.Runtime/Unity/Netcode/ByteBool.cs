namespace Unity.Netcode
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	internal struct ByteBool
	{
		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public bool BoolValue;

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		public byte ByteValue;

		public byte Collapse()
		{
			return ByteValue = (byte)(((ByteValue >> 7) | (ByteValue >> 6) | (ByteValue >> 5) | (ByteValue >> 4) | (ByteValue >> 3) | (ByteValue >> 2) | (ByteValue >> 1) | ByteValue) & 1);
		}

		public byte Collapse(bool b)
		{
			BoolValue = b;
			return Collapse();
		}
	}
}

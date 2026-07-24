namespace Unity.Networking.Transport
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct NetworkEvent
	{
		public enum Type : short
		{
			Empty = 0,
			Data = 1,
			Connect = 2,
			Disconnect = 3
		}

		[global::System.Runtime.InteropServices.FieldOffset(0)]
		internal global::Unity.Networking.Transport.NetworkEvent.Type type;

		[global::System.Runtime.InteropServices.FieldOffset(2)]
		internal short pipelineId;

		[global::System.Runtime.InteropServices.FieldOffset(4)]
		internal int connectionId;

		[global::System.Runtime.InteropServices.FieldOffset(8)]
		internal int offset;

		[global::System.Runtime.InteropServices.FieldOffset(12)]
		internal int size;
	}
}

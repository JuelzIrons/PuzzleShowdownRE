namespace Unity.Networking.Transport
{
	internal struct ConnectionPayload
	{
		public unsafe fixed byte Data[1472];

		public int Length;
	}
}

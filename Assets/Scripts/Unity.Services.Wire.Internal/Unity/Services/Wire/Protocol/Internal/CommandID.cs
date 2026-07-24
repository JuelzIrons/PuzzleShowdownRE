namespace Unity.Services.Wire.Protocol.Internal
{
	internal static class CommandID
	{
		public static uint currentId { get; private set; }

		public static uint GenerateNewId()
		{
			return ++currentId;
		}
	}
}

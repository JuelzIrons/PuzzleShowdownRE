namespace Unity.Services.Wire.Internal
{
	public class CommandInterruptedException : global::Unity.Services.Core.RequestFailedException
	{
		public global::Unity.Services.Wire.Internal.CentrifugeCloseCode m_Code { get; private set; }

		public CommandInterruptedException(string reason, global::Unity.Services.Wire.Internal.CentrifugeCloseCode code)
			: base(23002, "Command interrupted, reason: " + reason)
		{
			m_Code = code;
		}
	}
}

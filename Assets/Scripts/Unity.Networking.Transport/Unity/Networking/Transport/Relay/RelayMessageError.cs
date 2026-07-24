namespace Unity.Networking.Transport.Relay
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	internal struct RelayMessageError
	{
		public const int k_Length = 21;

		public global::Unity.Networking.Transport.Relay.RelayMessageHeader Header;

		public global::Unity.Networking.Transport.Relay.RelayAllocationId AllocationId;

		public byte ErrorCode;

		public void LogError()
		{
			switch (ErrorCode)
			{
			case 0:
				global::UnityEngine.Debug.LogError("Received error message from Relay: invalid protocol version. Make sure your Unity Transport package is up to date.");
				break;
			case 1:
				global::UnityEngine.Debug.LogError("Received error message from Relay: player timed out due to inactivity.");
				break;
			case 2:
				global::UnityEngine.Debug.LogError("Received error message from Relay: unauthorized.");
				break;
			case 3:
				global::UnityEngine.Debug.LogError("Received error message from Relay: allocation ID client mismatch.");
				break;
			case 4:
				global::UnityEngine.Debug.LogError("Received error message from Relay: allocation ID not found.");
				break;
			case 5:
				global::UnityEngine.Debug.LogError("Received error message from Relay: not connected.");
				break;
			case 6:
				global::UnityEngine.Debug.LogError("Received error message from Relay: self-connect not allowed.");
				break;
			default:
				global::UnityEngine.Debug.LogError($"Received error message from Relay with unknown error code {ErrorCode}");
				break;
			}
			if (ErrorCode == 1 || ErrorCode == 4)
			{
				global::UnityEngine.Debug.LogError("Relay allocation is invalid. See NetworkDriver.GetRelayConnectionStatus and RelayConnectionStatus.AllocationInvalid for details on how to handle this situation.");
			}
		}
	}
}

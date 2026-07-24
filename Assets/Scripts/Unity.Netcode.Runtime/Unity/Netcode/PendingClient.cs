namespace Unity.Netcode
{
	public class PendingClient
	{
		public enum State
		{
			PendingConnection = 0,
			PendingApproval = 1
		}

		internal global::UnityEngine.Coroutine ApprovalCoroutine;

		public ulong ClientId { get; internal set; }

		public global::Unity.Netcode.PendingClient.State ConnectionState { get; internal set; }
	}
}

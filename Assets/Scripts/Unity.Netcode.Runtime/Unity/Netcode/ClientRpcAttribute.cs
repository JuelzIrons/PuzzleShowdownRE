namespace Unity.Netcode
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
	public class ClientRpcAttribute : global::Unity.Netcode.RpcAttribute
	{
		public ClientRpcAttribute()
			: base(global::Unity.Netcode.SendTo.NotServer)
		{
		}
	}
}

namespace Unity.Netcode
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
	public class ServerRpcAttribute : global::Unity.Netcode.RpcAttribute
	{
		[global::System.Obsolete("ServerRpc with RequireOwnership is deprecated. Use [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)] or [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Owner)] instead.)]")]
		public new bool RequireOwnership;

		public ServerRpcAttribute()
			: base(global::Unity.Netcode.SendTo.Server)
		{
		}
	}
}

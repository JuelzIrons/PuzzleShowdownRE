namespace Unity.Netcode
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method)]
	public class RpcAttribute : global::System.Attribute
	{
		public struct RpcAttributeParams
		{
			public global::Unity.Netcode.RpcDelivery Delivery;

			[global::System.Obsolete("RequireOwnership is deprecated. Please use InvokePermission instead.")]
			public bool RequireOwnership;

			public global::Unity.Netcode.RpcInvokePermission InvokePermission;

			public bool DeferLocal;

			public bool AllowTargetOverride;
		}

		public global::Unity.Netcode.RpcDelivery Delivery;

		public global::Unity.Netcode.RpcInvokePermission InvokePermission;

		[global::System.Obsolete("RequireOwnership is deprecated. Please use InvokePermission = RpcInvokePermission.Owner or InvokePermission = RpcInvokePermission.Everyone instead.")]
		public bool RequireOwnership;

		public bool DeferLocal;

		public bool AllowTargetOverride;

		public RpcAttribute(global::Unity.Netcode.SendTo target)
		{
		}

		public RpcAttribute()
		{
		}
	}
}

namespace Unity.Netcode
{
	public struct RpcSendParams
	{
		public global::Unity.Netcode.BaseRpcTarget Target;

		public global::Unity.Netcode.LocalDeferMode LocalDeferMode;

		public static implicit operator global::Unity.Netcode.RpcSendParams(global::Unity.Netcode.BaseRpcTarget target)
		{
			return new global::Unity.Netcode.RpcSendParams
			{
				Target = target
			};
		}

		public static implicit operator global::Unity.Netcode.RpcSendParams(global::Unity.Netcode.LocalDeferMode deferMode)
		{
			return new global::Unity.Netcode.RpcSendParams
			{
				LocalDeferMode = deferMode
			};
		}
	}
}

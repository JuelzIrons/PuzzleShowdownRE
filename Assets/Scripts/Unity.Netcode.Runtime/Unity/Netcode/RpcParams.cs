namespace Unity.Netcode
{
	public struct RpcParams
	{
		public global::Unity.Netcode.RpcSendParams Send;

		public global::Unity.Netcode.RpcReceiveParams Receive;

		public static implicit operator global::Unity.Netcode.RpcParams(global::Unity.Netcode.RpcSendParams send)
		{
			return new global::Unity.Netcode.RpcParams
			{
				Send = send
			};
		}

		public static implicit operator global::Unity.Netcode.RpcParams(global::Unity.Netcode.BaseRpcTarget target)
		{
			return new global::Unity.Netcode.RpcParams
			{
				Send = new global::Unity.Netcode.RpcSendParams
				{
					Target = target
				}
			};
		}

		public static implicit operator global::Unity.Netcode.RpcParams(global::Unity.Netcode.LocalDeferMode deferMode)
		{
			return new global::Unity.Netcode.RpcParams
			{
				Send = new global::Unity.Netcode.RpcSendParams
				{
					LocalDeferMode = deferMode
				}
			};
		}

		public static implicit operator global::Unity.Netcode.RpcParams(global::Unity.Netcode.RpcReceiveParams receive)
		{
			return new global::Unity.Netcode.RpcParams
			{
				Receive = receive
			};
		}
	}
}

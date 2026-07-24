namespace Unity.Netcode
{
	internal class LocalSendRpcTarget : global::Unity.Netcode.BaseRpcTarget
	{
		public override void Dispose()
		{
		}

		internal override void Send(global::Unity.Netcode.NetworkBehaviour behaviour, ref global::Unity.Netcode.RpcMessage message, global::Unity.Netcode.NetworkDelivery delivery, global::Unity.Netcode.RpcParams rpcParams)
		{
			global::Unity.Netcode.NetworkManager networkManager = behaviour.NetworkManager;
			global::Unity.Netcode.NetworkContext context = new global::Unity.Netcode.NetworkContext
			{
				SenderId = m_NetworkManager.LocalClientId,
				Timestamp = networkManager.RealTimeProvider.RealTimeSinceStartup,
				SystemOwner = networkManager,
				Header = default(global::Unity.Netcode.NetworkMessageHeader),
				SerializedHeaderSize = 0,
				MessageSize = 0u
			};
			if (rpcParams.Send.LocalDeferMode == global::Unity.Netcode.LocalDeferMode.Defer)
			{
				using (global::Unity.Netcode.FastBufferWriter writer = new global::Unity.Netcode.FastBufferWriter(message.WriteBuffer.Length + global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Netcode.RpcMetadata>(), global::Unity.Collections.Allocator.Temp, int.MaxValue))
				{
					message.Serialize(writer, message.Version);
					using global::Unity.Netcode.FastBufferReader reader = new global::Unity.Netcode.FastBufferReader(writer, global::Unity.Collections.Allocator.None);
					context.Header = new global::Unity.Netcode.NetworkMessageHeader
					{
						MessageSize = (uint)reader.Length,
						MessageType = m_NetworkManager.MessageManager.GetMessageType(typeof(global::Unity.Netcode.RpcMessage))
					};
					networkManager.DeferredMessageManager.DeferMessage(global::Unity.Netcode.IDeferredNetworkMessageManager.TriggerType.OnNextFrame, 0uL, reader, ref context);
					_ = reader.Length;
					return;
				}
			}
			using global::Unity.Netcode.FastBufferReader readBuffer = new global::Unity.Netcode.FastBufferReader(message.WriteBuffer, global::Unity.Collections.Allocator.None);
			message.ReadBuffer = readBuffer;
			message.Handle(ref context);
			_ = readBuffer.Length;
		}

		internal LocalSendRpcTarget(global::Unity.Netcode.NetworkManager manager)
			: base(manager)
		{
		}
	}
}

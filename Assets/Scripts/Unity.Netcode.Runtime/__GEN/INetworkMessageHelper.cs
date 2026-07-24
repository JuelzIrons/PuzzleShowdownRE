namespace __GEN
{
	internal class INetworkMessageHelper
	{
		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		internal static void InitializeMessages()
		{
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.AnticipationCounterSyncPingMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.AnticipationCounterSyncPingMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.AnticipationCounterSyncPingMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.AnticipationCounterSyncPongMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.AnticipationCounterSyncPongMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.AnticipationCounterSyncPongMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ChangeOwnershipMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ChangeOwnershipMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ChangeOwnershipMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ClientConnectedMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ClientConnectedMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ClientConnectedMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ClientDisconnectedMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ClientDisconnectedMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ClientDisconnectedMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ConnectionApprovedMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ConnectionApprovedMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ConnectionApprovedMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ConnectionRequestMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ConnectionRequestMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ConnectionRequestMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.CreateObjectMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.CreateObjectMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.CreateObjectMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.DestroyObjectMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.DestroyObjectMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.DestroyObjectMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.DisconnectReasonMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.DisconnectReasonMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.DisconnectReasonMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.NamedMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.NamedMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.NamedMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.NetworkTransformMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.NetworkTransformMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.NetworkTransformMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.NetworkVariableDeltaMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.NetworkVariableDeltaMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.NetworkVariableDeltaMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ParentSyncMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ParentSyncMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ParentSyncMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ProxyMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ProxyMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ProxyMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ServerRpcMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ServerRpcMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ServerRpcMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ClientRpcMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ClientRpcMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ClientRpcMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.RpcMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.RpcMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.RpcMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ForwardServerRpcMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ForwardServerRpcMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ForwardServerRpcMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ForwardClientRpcMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ForwardClientRpcMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ForwardClientRpcMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.SceneEventMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.SceneEventMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.SceneEventMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.ServerLogMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.ServerLogMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.ServerLogMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.SessionOwnerMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.SessionOwnerMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.SessionOwnerMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.TimeSyncMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.TimeSyncMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.TimeSyncMessage>
			});
			global::Unity.Netcode.ILPPMessageProvider.__network_message_types.Add(new global::Unity.Netcode.NetworkMessageManager.MessageWithHandler
			{
				MessageType = typeof(global::Unity.Netcode.UnnamedMessage),
				Handler = global::Unity.Netcode.NetworkMessageManager.ReceiveMessage<global::Unity.Netcode.UnnamedMessage>,
				GetVersion = global::Unity.Netcode.NetworkMessageManager.CreateMessageAndGetVersion<global::Unity.Netcode.UnnamedMessage>
			});
		}
	}
}

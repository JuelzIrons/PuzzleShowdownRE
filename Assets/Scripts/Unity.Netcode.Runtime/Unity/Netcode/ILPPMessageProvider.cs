namespace Unity.Netcode
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct ILPPMessageProvider : global::Unity.Netcode.INetworkMessageProvider
	{
		internal static readonly global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler> __network_message_types = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler>();

		internal static bool IntegrationTestNoMessages;

		internal static global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.NetworkMessageTypes> GetMessageTypesMap()
		{
			return new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.NetworkMessageTypes>
			{
				{
					typeof(global::Unity.Netcode.ConnectionApprovedMessage),
					global::Unity.Netcode.NetworkMessageTypes.ConnectionApproved
				},
				{
					typeof(global::Unity.Netcode.ConnectionRequestMessage),
					global::Unity.Netcode.NetworkMessageTypes.ConnectionRequest
				},
				{
					typeof(global::Unity.Netcode.ChangeOwnershipMessage),
					global::Unity.Netcode.NetworkMessageTypes.ChangeOwnership
				},
				{
					typeof(global::Unity.Netcode.ClientConnectedMessage),
					global::Unity.Netcode.NetworkMessageTypes.ClientConnected
				},
				{
					typeof(global::Unity.Netcode.ClientDisconnectedMessage),
					global::Unity.Netcode.NetworkMessageTypes.ClientDisconnected
				},
				{
					typeof(global::Unity.Netcode.ClientRpcMessage),
					global::Unity.Netcode.NetworkMessageTypes.ClientRpc
				},
				{
					typeof(global::Unity.Netcode.CreateObjectMessage),
					global::Unity.Netcode.NetworkMessageTypes.CreateObject
				},
				{
					typeof(global::Unity.Netcode.DestroyObjectMessage),
					global::Unity.Netcode.NetworkMessageTypes.DestroyObject
				},
				{
					typeof(global::Unity.Netcode.DisconnectReasonMessage),
					global::Unity.Netcode.NetworkMessageTypes.DisconnectReason
				},
				{
					typeof(global::Unity.Netcode.ForwardClientRpcMessage),
					global::Unity.Netcode.NetworkMessageTypes.ForwardClientRpc
				},
				{
					typeof(global::Unity.Netcode.ForwardServerRpcMessage),
					global::Unity.Netcode.NetworkMessageTypes.ForwardServerRpc
				},
				{
					typeof(global::Unity.Netcode.NamedMessage),
					global::Unity.Netcode.NetworkMessageTypes.NamedMessage
				},
				{
					typeof(global::Unity.Netcode.NetworkTransformMessage),
					global::Unity.Netcode.NetworkMessageTypes.NetworkTransformMessage
				},
				{
					typeof(global::Unity.Netcode.NetworkVariableDeltaMessage),
					global::Unity.Netcode.NetworkMessageTypes.NetworkVariableDelta
				},
				{
					typeof(global::Unity.Netcode.ParentSyncMessage),
					global::Unity.Netcode.NetworkMessageTypes.ParentSync
				},
				{
					typeof(global::Unity.Netcode.ProxyMessage),
					global::Unity.Netcode.NetworkMessageTypes.Proxy
				},
				{
					typeof(global::Unity.Netcode.RpcMessage),
					global::Unity.Netcode.NetworkMessageTypes.Rpc
				},
				{
					typeof(global::Unity.Netcode.SceneEventMessage),
					global::Unity.Netcode.NetworkMessageTypes.SceneEvent
				},
				{
					typeof(global::Unity.Netcode.ServerLogMessage),
					global::Unity.Netcode.NetworkMessageTypes.ServerLog
				},
				{
					typeof(global::Unity.Netcode.ServerRpcMessage),
					global::Unity.Netcode.NetworkMessageTypes.ServerRpc
				},
				{
					typeof(global::Unity.Netcode.TimeSyncMessage),
					global::Unity.Netcode.NetworkMessageTypes.TimeSync
				},
				{
					typeof(global::Unity.Netcode.UnnamedMessage),
					global::Unity.Netcode.NetworkMessageTypes.Unnamed
				},
				{
					typeof(global::Unity.Netcode.SessionOwnerMessage),
					global::Unity.Netcode.NetworkMessageTypes.SessionOwner
				},
				{
					typeof(global::Unity.Netcode.AnticipationCounterSyncPingMessage),
					global::Unity.Netcode.NetworkMessageTypes.AnticipationCounterSyncPingMessage
				},
				{
					typeof(global::Unity.Netcode.AnticipationCounterSyncPongMessage),
					global::Unity.Netcode.NetworkMessageTypes.AnticipationCounterSyncPongMessage
				}
			};
		}

		public global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler> GetMessages()
		{
			if (IntegrationTestNoMessages)
			{
				return new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler>();
			}
			int length = global::System.Enum.GetValues(typeof(global::Unity.Netcode.NetworkMessageTypes)).Length;
			if (__network_message_types.Count != length)
			{
				throw new global::System.Exception($"Allowed types is not equal to the number of message type indices! Allowed Count: {__network_message_types.Count} | Index Count: {length}");
			}
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkMessageManager.MessageWithHandler>();
			global::Unity.Netcode.NetworkMessageManager.MessageWithHandler item = default(global::Unity.Netcode.NetworkMessageManager.MessageWithHandler);
			for (int i = 0; i < length; i++)
			{
				list.Add(item);
			}
			global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.NetworkMessageTypes> messageTypesMap = GetMessageTypesMap();
			if (messageTypesMap.Count != length)
			{
				throw new global::System.Exception($"Message type to Message type index count mistmatch! Table Count: {messageTypesMap.Count} | Index Count: {length}");
			}
			foreach (global::Unity.Netcode.NetworkMessageManager.MessageWithHandler _network_message_type in __network_message_types)
			{
				if (!messageTypesMap.ContainsKey(_network_message_type.MessageType))
				{
					throw new global::System.Exception($"Missing message type from lookup table: {_network_message_type.MessageType}");
				}
				list[(int)messageTypesMap[_network_message_type.MessageType]] = _network_message_type;
			}
			return list;
		}
	}
}

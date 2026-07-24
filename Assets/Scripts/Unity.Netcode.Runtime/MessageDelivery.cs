internal static class MessageDelivery
{
	private static global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkMessageTypes, global::Unity.Netcode.NetworkDelivery> s_MessageToDelivery = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkMessageTypes, global::Unity.Netcode.NetworkDelivery>();

	private static global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.NetworkMessageTypes> s_MessageToMessageType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.NetworkMessageTypes>();

	private static global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkMessageTypes> s_SkipMessageTypes = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkMessageTypes>
	{
		global::Unity.Netcode.NetworkMessageTypes.NamedMessage,
		global::Unity.Netcode.NetworkMessageTypes.Unnamed
	};

	[global::UnityEngine.RuntimeInitializeOnLoadMethod]
	private static void OnApplicationStart()
	{
		UpdateMessageTypes();
	}

	private static void UpdateMessageTypes()
	{
		s_MessageToDelivery.Clear();
		foreach (global::Unity.Netcode.NetworkMessageTypes value in global::System.Enum.GetValues(typeof(global::Unity.Netcode.NetworkMessageTypes)))
		{
			if (!s_SkipMessageTypes.Contains(value))
			{
				s_MessageToDelivery.Add(value, global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
			}
		}
		s_MessageToMessageType = global::Unity.Netcode.ILPPMessageProvider.GetMessageTypesMap();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ChangeOwnershipMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientConnectedMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientDisconnectedMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ConnectionRequestMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ConnectionApprovedMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.CreateObjectMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.DestroyObjectMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.NetworkTransformMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.NetworkVariableDeltaMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ParentSyncMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.RpcMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ClientRpcMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ServerRpcMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.SceneEventMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.ServerLogMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.SessionOwnerMessage>.Initialize();
		global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.TimeSyncMessage>.Initialize();
	}

	internal static global::Unity.Netcode.NetworkDelivery GetDelivery(global::System.Type type)
	{
		if (type == null || s_SkipMessageTypes.Contains(s_MessageToMessageType[type]))
		{
			return global::Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced;
		}
		return GetDelivery(s_MessageToMessageType[type]);
	}

	internal static global::Unity.Netcode.NetworkDelivery GetDelivery(global::Unity.Netcode.NetworkMessageTypes messageType)
	{
		if (s_SkipMessageTypes.Contains(messageType))
		{
			throw new global::System.Exception($"{messageType} is not registered in the message type to network delivery map!");
		}
		return s_MessageToDelivery[messageType];
	}
}

namespace Unity.Netcode
{
	internal static class MessageDeliveryType<T> where T : global::Unity.Netcode.INetworkMessage
	{
		internal static global::Unity.Netcode.NetworkDelivery DefaultDelivery { get; private set; }

		internal static void Initialize()
		{
			DefaultDelivery = MessageDelivery.GetDelivery(typeof(T));
		}
	}
}

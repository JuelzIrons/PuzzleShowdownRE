namespace Unity.Services.Wire.Internal
{
	internal interface ISubscriptionRepository
	{
		bool IsEmpty { get; }

		event global::System.Action<int> SubscriptionCountChanged;

		bool IsAlreadySubscribed(global::Unity.Services.Wire.Internal.Subscription sub);

		bool IsRecovering(global::Unity.Services.Wire.Internal.Subscription sub);

		void OnSubscriptionComplete(global::Unity.Services.Wire.Internal.Subscription sub, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult result);

		global::Unity.Services.Wire.Internal.Subscription GetSub(global::Unity.Services.Wire.Internal.Subscription sub);

		global::Unity.Services.Wire.Internal.Subscription GetSub(string channel);

		global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Wire.Internal.Subscription>> GetAll();

		void RemoveSub(global::Unity.Services.Wire.Internal.Subscription sub);

		void OnSocketClosed();

		void RecoverSubscriptions(global::Unity.Services.Wire.Protocol.Internal.Reply reply);

		void Clear();
	}
}

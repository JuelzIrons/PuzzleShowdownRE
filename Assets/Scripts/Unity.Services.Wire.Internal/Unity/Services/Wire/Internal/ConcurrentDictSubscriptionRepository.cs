namespace Unity.Services.Wire.Internal
{
	internal class ConcurrentDictSubscriptionRepository : global::Unity.Services.Wire.Internal.ISubscriptionRepository
	{
		public global::System.Collections.Concurrent.ConcurrentDictionary<string, global::Unity.Services.Wire.Internal.Subscription> Subscriptions;

		public bool IsEmpty => Subscriptions.IsEmpty;

		public event global::System.Action<int> SubscriptionCountChanged;

		public ConcurrentDictSubscriptionRepository()
		{
			Subscriptions = new global::System.Collections.Concurrent.ConcurrentDictionary<string, global::Unity.Services.Wire.Internal.Subscription>();
		}

		public void Clear()
		{
			Subscriptions.Clear();
		}

		public bool IsAlreadySubscribed(string alias)
		{
			return GetSub(alias)?.IsConnected ?? false;
		}

		public bool IsAlreadySubscribed(global::Unity.Services.Wire.Internal.Subscription sub)
		{
			return IsAlreadySubscribed(sub.Channel);
		}

		public bool IsRecovering(global::Unity.Services.Wire.Internal.Subscription sub)
		{
			if (string.IsNullOrEmpty(sub.Channel))
			{
				return false;
			}
			if (Subscriptions.ContainsKey(sub.Channel))
			{
				return !sub.IsConnected;
			}
			return false;
		}

		public void OnSubscriptionComplete(global::Unity.Services.Wire.Internal.Subscription sub, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult res)
		{
			if (res.offset != sub.Offset)
			{
				try
				{
					global::Unity.Services.Wire.Protocol.Internal.Publication[] publications = res.publications;
					foreach (global::Unity.Services.Wire.Protocol.Internal.Publication publication in publications)
					{
						sub.ProcessPublication(publication);
					}
					sub.Offset = res.offset;
				}
				catch (global::System.Exception)
				{
				}
			}
			bool num = IsRecovering(sub);
			sub.OnConnectivityChangeReceived(connected: true);
			if (!num)
			{
				Subscriptions.TryAdd(sub.Channel, sub);
				this.SubscriptionCountChanged?.Invoke(Subscriptions.Count);
			}
		}

		public global::Unity.Services.Wire.Internal.Subscription GetSub(string channel)
		{
			if (string.IsNullOrEmpty(channel))
			{
				return null;
			}
			if (Subscriptions.ContainsKey(channel))
			{
				Subscriptions.TryGetValue(channel, out var value);
				return value;
			}
			return null;
		}

		public global::Unity.Services.Wire.Internal.Subscription GetSub(global::Unity.Services.Wire.Internal.Subscription sub)
		{
			return GetSub(sub.Channel);
		}

		public void RemoveSub(global::Unity.Services.Wire.Internal.Subscription sub)
		{
			if (!string.IsNullOrEmpty(sub.Channel) && Subscriptions.ContainsKey(sub.Channel))
			{
				Subscriptions.TryRemove(sub.Channel, out var _);
				sub.OnUnsubscriptionComplete();
				this.SubscriptionCountChanged?.Invoke(Subscriptions.Count);
			}
		}

		public void OnSocketClosed()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Wire.Internal.Subscription> subscription in Subscriptions)
			{
				subscription.Value.OnConnectivityChangeReceived(connected: false);
			}
		}

		public void RecoverSubscriptions(global::Unity.Services.Wire.Protocol.Internal.Reply reply)
		{
			global::Unity.Services.Wire.Protocol.Internal.ConnectResult connect = reply.connect;
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult> subs = connect.subs;
			if (subs == null || subs.Count <= 0)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeResult> sub2 in connect.subs)
			{
				global::Unity.Services.Wire.Internal.Subscription sub = GetSub(sub2.Key);
				if (sub != null)
				{
					OnSubscriptionComplete(sub, sub2.Value);
				}
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Wire.Internal.Subscription>> GetAll()
		{
			return Subscriptions.ToArray();
		}
	}
}

namespace Unity.Services.Wire.Protocol.Internal
{
	[global::Newtonsoft.Json.JsonObject(ItemNullValueHandling = global::Newtonsoft.Json.NullValueHandling.Ignore)]
	internal class SubscribeRequest
	{
		public string channel;

		public string token;

		public bool recover;

		public ulong offset;

		public string epoch;

		[global::UnityEngine.Scripting.Preserve]
		public SubscribeRequest()
		{
		}

		public static async global::System.Threading.Tasks.Task<global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest>> getRequestFromRepo(global::Unity.Services.Wire.Internal.ISubscriptionRepository repository)
		{
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest> subscriptionRequests = new global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest>();
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Wire.Internal.Subscription> subIterator in repository.GetAll())
			{
				string text;
				try
				{
					text = await subIterator.Value.RetrieveTokenAsync();
				}
				catch (global::System.Exception ex)
				{
					subIterator.Value.OnError("Failed to retrieve token: " + ex.Message);
					continue;
				}
				subscriptionRequests.Add(subIterator.Key, new global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest
				{
					recover = repository.IsRecovering(subIterator.Value),
					token = text
				});
			}
			return subscriptionRequests;
		}
	}
}

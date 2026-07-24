namespace Unity.Services.Wire.Internal
{
	public interface IChannel : global::System.IDisposable
	{
		event global::System.Action<string> MessageReceived;

		event global::System.Action<byte[]> BinaryMessageReceived;

		event global::System.Action KickReceived;

		event global::System.Action<global::Unity.Services.Wire.Internal.SubscriptionState> NewStateReceived;

		event global::System.Action<string> ErrorReceived;

		global::System.Threading.Tasks.Task SubscribeAsync();

		global::System.Threading.Tasks.Task UnsubscribeAsync();
	}
}

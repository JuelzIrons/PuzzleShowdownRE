namespace Unity.Services.Wire.Internal
{
	internal class Subscription : global::Unity.Services.Wire.Internal.IChannel, global::System.IDisposable
	{
		public string Channel;

		public ulong Offset;

		public string Epoch;

		private global::Unity.Services.Wire.Internal.SubscriptionState m_State = global::Unity.Services.Wire.Internal.SubscriptionState.Unsynced;

		private global::Unity.Services.Wire.Internal.IChannelTokenProvider m_TokenProvider;

		private bool m_Disposed;

		public bool IsConnected => SubscriptionState == global::Unity.Services.Wire.Internal.SubscriptionState.Synced;

		public global::Unity.Services.Wire.Internal.SubscriptionState SubscriptionState
		{
			get
			{
				return m_State;
			}
			private set
			{
				SetState(value);
			}
		}

		private string ChannelDisplay
		{
			get
			{
				if (!string.IsNullOrEmpty(Channel))
				{
					return Channel;
				}
				return "unknown";
			}
		}

		public event global::System.Action<string> MessageReceived;

		public event global::System.Action<byte[]> BinaryMessageReceived;

		public event global::System.Action KickReceived;

		public event global::System.Action<global::Unity.Services.Wire.Internal.SubscriptionState> NewStateReceived;

		public event global::System.Action<global::System.Threading.Tasks.TaskCompletionSource<bool>> UnsubscribeReceived;

		public event global::System.Action<global::System.Threading.Tasks.TaskCompletionSource<bool>> SubscribeReceived;

		public event global::System.Action<string> ErrorReceived;

		public event global::System.Action DisposeReceived;

		public Subscription(global::Unity.Services.Wire.Internal.IChannelTokenProvider tokenProvider)
		{
			m_TokenProvider = tokenProvider;
			Offset = 0uL;
			m_Disposed = false;
		}

		public async global::System.Threading.Tasks.Task<string> RetrieveTokenAsync()
		{
			global::Unity.Services.Wire.Internal.ChannelToken channelToken;
			try
			{
				channelToken = await m_TokenProvider.GetTokenAsync();
			}
			catch (global::System.Exception innerException)
			{
				throw new global::Unity.Services.Core.RequestFailedException(23006, "Exception caught while running the token retriever.", innerException);
			}
			ValidateTokenData(channelToken.ChannelName, channelToken.Token);
			Channel = channelToken.ChannelName;
			return channelToken.Token;
		}

		internal void SetState(global::Unity.Services.Wire.Internal.SubscriptionState state)
		{
			if (m_State != state)
			{
				m_State = state;
				this.NewStateReceived?.Invoke(m_State);
			}
		}

		private void ValidateTokenData(string channel, string token)
		{
			if (string.IsNullOrEmpty(channel))
			{
				throw new global::Unity.Services.Wire.Internal.EmptyChannelException();
			}
			if (string.IsNullOrEmpty(token))
			{
				throw new global::Unity.Services.Wire.Internal.EmptyTokenException();
			}
			if (!string.IsNullOrEmpty(Channel) && Channel != channel)
			{
				throw new global::Unity.Services.Wire.Internal.ChannelChangedException(channel, Channel);
			}
		}

		internal void ProcessPublication(global::Unity.Services.Wire.Protocol.Internal.Publication publication)
		{
			try
			{
				this.MessageReceived?.Invoke(publication.data.payload);
				this.BinaryMessageReceived?.Invoke(global::System.Text.Encoding.UTF8.GetBytes(publication.data.payload));
			}
			finally
			{
				Offset = publication.offset;
			}
		}

		internal void OnUnsubscriptionComplete()
		{
			SubscriptionState = global::Unity.Services.Wire.Internal.SubscriptionState.Unsubscribed;
		}

		public void OnKickReceived()
		{
			SubscriptionState = global::Unity.Services.Wire.Internal.SubscriptionState.Unsubscribed;
			this.KickReceived?.Invoke();
		}

		public void OnConnectivityChangeReceived(bool connected)
		{
			SubscriptionState = (connected ? global::Unity.Services.Wire.Internal.SubscriptionState.Synced : global::Unity.Services.Wire.Internal.SubscriptionState.Unsynced);
		}

		~Subscription()
		{
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		internal void Dispose(bool disposing)
		{
			if (m_Disposed)
			{
				return;
			}
			m_Disposed = true;
			try
			{
				if (disposing)
				{
					this.UnsubscribeReceived?.Invoke(new global::System.Threading.Tasks.TaskCompletionSource<bool>());
				}
				else
				{
					this.DisposeReceived?.Invoke();
				}
			}
			catch (global::System.Exception arg)
			{
				this.ErrorReceived?.Invoke($"Exception raised during disposal of the Channel: ${arg}");
			}
			m_TokenProvider = null;
			this.DisposeReceived = null;
			this.UnsubscribeReceived = null;
			this.MessageReceived = null;
			this.BinaryMessageReceived = null;
			this.NewStateReceived = null;
			this.KickReceived = null;
			this.SubscribeReceived = null;
			this.ErrorReceived = null;
		}

		public global::System.Threading.Tasks.Task SubscribeAsync()
		{
			if (m_Disposed)
			{
				throw new global::System.ObjectDisposedException(ChannelDisplay);
			}
			SubscriptionState = global::Unity.Services.Wire.Internal.SubscriptionState.Subscribing;
			global::System.Threading.Tasks.TaskCompletionSource<bool> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<bool>();
			this.SubscribeReceived?.Invoke(taskCompletionSource);
			return taskCompletionSource.Task;
		}

		public global::System.Threading.Tasks.Task UnsubscribeAsync()
		{
			if (m_Disposed)
			{
				throw new global::System.ObjectDisposedException(ChannelDisplay);
			}
			global::System.Threading.Tasks.TaskCompletionSource<bool> taskCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<bool>();
			this.UnsubscribeReceived?.Invoke(taskCompletionSource);
			return taskCompletionSource.Task;
		}

		internal void OnError(string reason)
		{
			SubscriptionState = global::Unity.Services.Wire.Internal.SubscriptionState.Error;
			this.ErrorReceived?.Invoke(reason);
		}
	}
}

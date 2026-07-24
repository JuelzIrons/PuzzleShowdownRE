namespace Unity.Services.Lobbies.Internal
{
	internal class LobbyChannel : global::Unity.Services.Lobbies.ILobbyEvents
	{
		private readonly global::Unity.Services.Wire.Internal.IChannel channelSubscription;

		private int mostRecentEventVersion;

		private readonly global::Unity.Services.Authentication.Internal.IPlayerId playerIdComponent;

		private readonly string lobbyId;

		private readonly global::Unity.Services.Lobbies.ILobbyService lobbyService;

		private readonly global::System.Collections.Generic.SortedList<int, global::Unity.Services.Lobbies.ILobbyChanges> eventProcessQueue;

		private readonly object eventLock = new object();

		private readonly object mostRecentEventVersionLock = new object();

		public LobbyEventCallbacks Callbacks { get; }

		internal LobbyChannel(global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::Unity.Services.Wire.Internal.IChannel channel, LobbyEventCallbacks callbacks, string lobbyId, global::Unity.Services.Lobbies.ILobbyService lobbyService)
		{
			global::Unity.Services.Lobbies.Internal.LobbyChannel lobbyChannel = this;
			playerIdComponent = playerId;
			channelSubscription = channel;
			Callbacks = callbacks;
			eventProcessQueue = new global::System.Collections.Generic.SortedList<int, global::Unity.Services.Lobbies.ILobbyChanges>();
			this.lobbyId = lobbyId;
			this.lobbyService = lobbyService;
			channelSubscription.MessageReceived += async delegate(string payload)
			{
				await lobbyChannel.OnLobbySubscriptionMessage(payload, callbacks);
			};
			channelSubscription.KickReceived += delegate
			{
				lobbyChannel.OnLobbySubscriptionKick(callbacks);
			};
			channelSubscription.NewStateReceived += delegate(global::Unity.Services.Wire.Internal.SubscriptionState state)
			{
				lobbyChannel.OnLobbySubscriptionNewState(state, callbacks);
			};
		}

		public async global::System.Threading.Tasks.Task SubscribeAsync()
		{
			try
			{
				await channelSubscription.SubscribeAsync();
			}
			catch (global::Unity.Services.Core.RequestFailedException ex)
			{
				switch (ex.ErrorCode)
				{
				case 23002:
				case 23003:
					throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.SubscriptionToLobbyLostWhileBusy, "The connection was lost or dropped while attempting to subscribe.", ex);
				default:
					throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyEventServiceConnectionError, $"There was an error when trying to connect to the lobby service for events. Ensure a valid Lobby ID was sent. Error Code[{ex.ErrorCode}].", ex);
				case 23008:
					break;
				}
			}
		}

		public async global::System.Threading.Tasks.Task UnsubscribeAsync()
		{
			try
			{
				await channelSubscription.UnsubscribeAsync();
			}
			catch (global::Unity.Services.Core.RequestFailedException ex)
			{
				switch (ex.ErrorCode)
				{
				case 23002:
					throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.SubscriptionToLobbyLostWhileBusy, "The connection was lost or dropped while attempting to unsubscribe.", ex);
				case 23009:
					throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.AlreadyUnsubscribedFromLobby, "You are already unsubscribed from this lobby, you do not need to unsubscribe again.", ex);
				default:
					throw new global::Unity.Services.Lobbies.LobbyServiceException(global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyEventServiceConnectionError, $"There was an error when trying to connect to the lobby service for events. Ensure a valid Lobby ID was sent. Error Code[{ex.ErrorCode}].", ex);
				}
			}
		}

		private async global::System.Threading.Tasks.Task OnLobbySubscriptionMessage(string payload, LobbyEventCallbacks callbacks)
		{
			try
			{
				global::Unity.Services.Lobbies.LobbyPatcherChanges lobbyChanges = LobbyPatcher.GetLobbyChanges(payload);
				if (mostRecentEventVersion < lobbyChanges.Version.Value)
				{
					lock (mostRecentEventVersionLock)
					{
						mostRecentEventVersion = lobbyChanges.Version.Value;
					}
				}
				await HandleLobbyChanges(lobbyChanges, callbacks);
			}
			catch (global::System.Exception exception)
			{
				global::Unity.Services.Multiplayer.Logger.LogException(exception);
			}
		}

		private void OnLobbySubscriptionKick(LobbyEventCallbacks callbacks)
		{
			callbacks.InvokeKickedFromLobby();
		}

		private void OnLobbySubscriptionNewState(global::Unity.Services.Wire.Internal.SubscriptionState state, LobbyEventCallbacks callbacks)
		{
			switch (state)
			{
			case global::Unity.Services.Wire.Internal.SubscriptionState.Unsubscribed:
				(lobbyService as global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal).GetLobbyCacher().ResetLobby(lobbyId);
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Unsubscribed);
				break;
			case global::Unity.Services.Wire.Internal.SubscriptionState.Subscribing:
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Subscribing);
				break;
			case global::Unity.Services.Wire.Internal.SubscriptionState.Synced:
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Subscribed);
				break;
			case global::Unity.Services.Wire.Internal.SubscriptionState.Unsynced:
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Unsynced);
				break;
			case global::Unity.Services.Wire.Internal.SubscriptionState.Error:
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Error);
				break;
			default:
				callbacks.InvokeLobbyEventConnectionStateChanged(global::Unity.Services.Lobbies.LobbyEventConnectionState.Unknown);
				break;
			}
		}

		private async global::System.Threading.Tasks.Task HandleLobbyChanges(global::Unity.Services.Lobbies.ILobbyChanges changes, LobbyEventCallbacks callbacks)
		{
			lock (eventLock)
			{
				eventProcessQueue.Add(changes.Version.Value, changes);
				if (eventProcessQueue.Count > 1)
				{
					return;
				}
			}
			int num = 0;
			lock (eventLock)
			{
				num = eventProcessQueue.Count;
			}
			while (num != 0)
			{
				global::Unity.Services.Lobbies.ILobbyChanges nextToProcess;
				lock (eventLock)
				{
					nextToProcess = eventProcessQueue.Values[0];
				}
				try
				{
					await ProcessEvent(nextToProcess, callbacks);
				}
				catch (global::System.Exception exception)
				{
					global::Unity.Services.Multiplayer.Logger.LogException(exception);
				}
				finally
				{
					lock (eventLock)
					{
						eventProcessQueue.Remove(nextToProcess.Version.Value);
						num = eventProcessQueue.Count;
					}
				}
				nextToProcess = null;
			}
		}

		private async global::System.Threading.Tasks.Task ProcessEvent(global::Unity.Services.Lobbies.ILobbyChanges nextToProcess, LobbyEventCallbacks callbacks)
		{
			int value = nextToProcess.Version.Value;
			global::Lobbies.SDK.LobbyCacher.LobbyCacher lobbyCacher = (lobbyService as global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal).GetLobbyCacher();
			if (!nextToProcess.LobbyDeleted && (!lobbyCacher.TryGetLobbyCache(lobbyId, out var cachedLobby) || value != cachedLobby.Version + 1))
			{
				if (cachedLobby != null && value <= cachedLobby.Version)
				{
					return;
				}
				await lobbyService.GetLobbyAsync(lobbyId);
				lobbyCacher.TryGetLobbyCache(lobbyId, out cachedLobby);
			}
			lobbyCacher.UpdateLobbyCache(lobbyId, nextToProcess);
		}

		private bool ResolveTrivialEvent(global::Unity.Services.Lobbies.ILobbyChanges changes, LobbyEventCallbacks callbacks, global::Unity.Services.Lobbies.Models.Lobby cachedLobby)
		{
			if (cachedLobby == null)
			{
				return false;
			}
			int value = changes.Version.Value;
			if (value <= cachedLobby.Version)
			{
				return true;
			}
			if (value == cachedLobby.Version + 1)
			{
				if (!WasRemovedFromLobby(changes, cachedLobby))
				{
					changes.ApplyToLobby(cachedLobby);
				}
				callbacks.InvokeLobbyChanged(changes);
				return true;
			}
			return false;
		}

		private bool WasRemovedFromLobby(global::Unity.Services.Lobbies.ILobbyChanges changes, global::Unity.Services.Lobbies.Models.Lobby cachedLobby)
		{
			global::Lobbies.SDK.LobbyCacher.LobbyCacher lobbyCacher = (lobbyService as global::Unity.Services.Lobbies.Internal.ILobbyServiceInternal).GetLobbyCacher();
			if (cachedLobby == null || !changes.PlayerLeft.Changed)
			{
				return false;
			}
			foreach (int item in changes.PlayerLeft.Value)
			{
				if (cachedLobby.Players[item].Id.Equals(playerIdComponent.PlayerId))
				{
					lobbyCacher.RemoveLobbyCache(lobbyId);
					return true;
				}
			}
			return false;
		}
	}
}

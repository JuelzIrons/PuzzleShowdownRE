namespace Unity.Services.Wire.Internal
{
	internal class Client : global::Unity.Services.Wire.Internal.IWire, global::Unity.Services.Core.Internal.IServiceComponent
	{
		internal enum ConnectionState
		{
			Disconnected = 0,
			Connected = 1,
			Connecting = 2,
			Disconnecting = 3
		}

		public readonly global::Unity.Services.Wire.Internal.ISubscriptionRepository SubscriptionRepository;

		private global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Internal.Client.ConnectionState> m_ConnectionCompletionSource;

		private global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Internal.Client.ConnectionState> m_DisconnectionCompletionSource;

		internal global::Unity.Services.Wire.Internal.Client.ConnectionState m_ConnectionState;

		private global::Unity.Services.Wire.Internal.IWebSocket m_WebsocketClient;

		internal global::Unity.Services.Wire.Internal.IBackoffStrategy m_Backoff;

		private readonly global::Unity.Services.Wire.Internal.CommandManager m_CommandManager;

		private readonly global::Unity.Services.Wire.Internal.Configuration m_Config;

		private readonly global::Unity.Services.Core.Telemetry.Internal.IMetrics m_Metrics;

		private readonly global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils m_ThreadUtils;

		private readonly global::Unity.Services.Wire.Internal.IWebsocketFactory m_WebsocketFactory;

		internal bool m_WantConnected;

		internal byte[] k_PongMessage;

		private bool m_Disabled;

		private bool m_Pong;

		private uint m_ServerPingIntervalS;

		private global::System.TimeSpan m_NetworkCheckInterval = global::System.TimeSpan.FromSeconds(5.0);

		private global::Unity.Services.Core.Scheduler.Internal.IActionScheduler m_ActionScheduler;

		private long m_PingDeadlineScheduledId;

		private long m_ReconnectionActionId;

		private event global::System.Action m_OnConnected;

		public Client(global::Unity.Services.Wire.Internal.Configuration config, global::Unity.Services.Core.Scheduler.Internal.IActionScheduler actionScheduler, global::Unity.Services.Core.Telemetry.Internal.IMetrics metrics, global::Unity.Services.Core.Threading.Internal.IUnityThreadUtils threadUtils, global::Unity.Services.Wire.Internal.IWebsocketFactory websocketFactory)
		{
			k_PongMessage = global::System.Text.Encoding.UTF8.GetBytes("{}");
			m_ThreadUtils = threadUtils;
			m_Config = InitNetworkUtil(config);
			m_Metrics = metrics;
			m_ActionScheduler = actionScheduler;
			m_WebsocketFactory = websocketFactory;
			SubscriptionRepository = new global::Unity.Services.Wire.Internal.ConcurrentDictSubscriptionRepository();
			SubscriptionRepository.SubscriptionCountChanged += delegate(int subscriptionCount)
			{
				m_Metrics.SendGaugeMetric("subscription_count", subscriptionCount);
			};
			m_Backoff = new global::Unity.Services.Wire.Internal.ExponentialBackoffStrategy();
			m_CommandManager = new global::Unity.Services.Wire.Internal.CommandManager(config, actionScheduler);
		}

		internal global::Unity.Services.Wire.Internal.Configuration InitNetworkUtil(global::Unity.Services.Wire.Internal.Configuration config)
		{
			if (config.NetworkUtil == null)
			{
				config.NetworkUtil = new global::Unity.Services.Wire.Internal.NetworkUtil();
			}
			return config;
		}

		private async global::System.Threading.Tasks.Task<global::Unity.Services.Wire.Protocol.Internal.Reply> SendCommandAsync(uint id, global::Unity.Services.Wire.Protocol.Internal.Command command)
		{
			if (m_Disabled || !m_WantConnected)
			{
				return null;
			}
			global::System.DateTime time = global::System.DateTime.Now;
			global::System.Collections.Generic.Dictionary<string, string> tags = new global::System.Collections.Generic.Dictionary<string, string> { 
			{
				"method",
				command.GetMethod()
			} };
			m_CommandManager.RegisterCommand(id);
			if (m_WebsocketClient.GetState() != global::Unity.Services.Wire.Internal.WebSocketState.Open && m_ConnectionCompletionSource != null)
			{
				await m_ConnectionCompletionSource.Task;
			}
			try
			{
				m_WebsocketClient.Send(command.GetBytes());
				global::Unity.Services.Wire.Protocol.Internal.Reply result = await m_CommandManager.WaitForCommandAsync(id);
				tags.Add("result", "success");
				m_Metrics.SendHistogramMetric("command", (global::System.DateTime.Now - time).TotalMilliseconds, tags);
				return result;
			}
			catch (global::System.Exception)
			{
				tags.Add("result", "failure");
				m_Metrics.SendHistogramMetric("command", (global::System.DateTime.Now - time).TotalMilliseconds, tags);
				throw;
			}
		}

		internal void OnIdentityChanged(string playerId)
		{
			if (m_Disabled)
			{
				return;
			}
			m_ThreadUtils.Send((global::System.Func<global::System.Threading.Tasks.Task>)async delegate
			{
				try
				{
					bool reconnect = !string.IsNullOrEmpty(m_Config.token.AccessToken);
					await ResetAsync(reconnect);
				}
				catch (global::System.Exception)
				{
				}
			});
		}

		internal global::System.Threading.Tasks.Task DisconnectAsync()
		{
			if (m_ReconnectionActionId > 0)
			{
				m_ActionScheduler.CancelAction(m_ReconnectionActionId);
			}
			if (m_DisconnectionCompletionSource != null)
			{
				return m_DisconnectionCompletionSource.Task;
			}
			m_WantConnected = false;
			if (m_WebsocketClient == null)
			{
				ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnected);
				return global::System.Threading.Tasks.Task.CompletedTask;
			}
			m_DisconnectionCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Internal.Client.ConnectionState>();
			ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnecting);
			m_WebsocketClient.Close();
			m_WebsocketClient = null;
			return m_DisconnectionCompletionSource.Task;
		}

		internal async global::System.Threading.Tasks.Task ResetAsync(bool reconnect)
		{
			await DisconnectAsync();
			m_CommandManager.Clear();
			SubscriptionRepository.Clear();
			if (reconnect)
			{
				await ConnectAsync();
			}
		}

		public async global::System.Threading.Tasks.Task ConnectAsync()
		{
			if (m_ReconnectionActionId > 0)
			{
				m_ActionScheduler.CancelAction(m_ReconnectionActionId);
				m_ReconnectionActionId = 0L;
			}
			if (m_ConnectionState != global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected)
			{
				if (m_ConnectionState == global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnecting)
				{
					await m_DisconnectionCompletionSource.Task;
				}
				if (m_ConnectionState == global::Unity.Services.Wire.Internal.Client.ConnectionState.Connecting)
				{
					await m_ConnectionCompletionSource.Task;
					return;
				}
				ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState.Connecting);
				m_WantConnected = true;
				InitWebsocket();
				m_WebsocketClient.Connect();
				await m_ConnectionCompletionSource.Task;
			}
		}

		internal async void OnWebsocketOpen()
		{
			_ = 1;
			try
			{
				global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest> subscriptionRequests = await global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest.getRequestFromRepo(SubscriptionRepository);
				if (m_Config.token.AccessToken == null)
				{
					throw new global::Unity.Services.Wire.Internal.EmptyTokenException();
				}
				global::Unity.Services.Wire.Protocol.Internal.Command command = new global::Unity.Services.Wire.Protocol.Internal.Command(new global::Unity.Services.Wire.Protocol.Internal.ConnectRequest(m_Config.token.AccessToken, subscriptionRequests));
				try
				{
					global::Unity.Services.Wire.Protocol.Internal.Reply reply = await SendCommandAsync(command.id, command);
					m_Backoff.Reset();
					SubscriptionRepository.RecoverSubscriptions(reply);
					ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected);
					m_Pong = reply.connect.pong;
					m_ServerPingIntervalS = reply.connect.ping;
					SetupPingDeadline();
				}
				catch (global::Unity.Services.Wire.Internal.CommandInterruptedException ex)
				{
					m_ConnectionCompletionSource.TrySetException(new global::Unity.Services.Wire.Internal.ConnectionFailedException($"Socket closed during connection attempt: {ex.m_Code}"));
					m_WebsocketClient?.Close();
				}
				catch (global::System.Exception exception)
				{
					m_ConnectionCompletionSource.TrySetException(exception);
					m_WebsocketClient?.Close();
				}
			}
			catch (global::System.Exception)
			{
			}
		}

		private void SetupPingDeadline()
		{
			if (m_ServerPingIntervalS != 0)
			{
				m_PingDeadlineScheduledId = m_ActionScheduler.ScheduleAction(PingDeadline, m_ServerPingIntervalS + (uint)m_Config.MaxServerPingDelay);
			}
		}

		private void CancelPingDeadline()
		{
			if (m_PingDeadlineScheduledId != 0L)
			{
				m_ActionScheduler.CancelAction(m_PingDeadlineScheduledId);
				m_PingDeadlineScheduledId = 0L;
			}
		}

		private void PingDeadline()
		{
			m_PingDeadlineScheduledId = 0L;
			if (m_ConnectionState == global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected)
			{
				m_WebsocketClient.Close();
			}
		}

		internal void OnWebsocketMessage(byte[] payload)
		{
			global::System.Collections.Generic.IEnumerable<string> enumerable = global::Unity.Services.Wire.Internal.BatchMessagesUtil.SplitMessages(payload);
			m_Metrics.SendSumMetric("message_received", global::System.Linq.Enumerable.Count(enumerable));
			foreach (string item in enumerable)
			{
				global::Unity.Services.Wire.Protocol.Internal.Reply reply = global::Unity.Services.Wire.Protocol.Internal.Reply.FromJson(item);
				if (reply.id != 0)
				{
					HandleCommandReply(reply);
				}
				else if (reply.push != null)
				{
					try
					{
						HandlePushMessage(reply.push);
					}
					catch (global::System.NotImplementedException)
					{
					}
					catch (global::System.Exception)
					{
					}
				}
				else
				{
					HandleServerPing();
				}
			}
		}

		private void HandleServerPing()
		{
			CancelPingDeadline();
			if (m_Pong)
			{
				try
				{
					m_WebsocketClient.Send(k_PongMessage);
				}
				catch (global::Unity.Services.Wire.Internal.WebSocketInvalidStateException)
				{
					return;
				}
			}
			SetupPingDeadline();
		}

		private void OnWebsocketError(string msg)
		{
			m_Metrics.SendSumMetric("websocket_error");
		}

		internal void OnWebsocketClose(global::Unity.Services.Wire.Internal.WebSocketCloseCode originalCode)
		{
			try
			{
				CancelPingDeadline();
				ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnected);
				m_CommandManager.OnDisconnect(new global::Unity.Services.Wire.Internal.CommandInterruptedException($"websocket disconnected: {(global::Unity.Services.Wire.Internal.CentrifugeCloseCode)originalCode}", (global::Unity.Services.Wire.Internal.CentrifugeCloseCode)originalCode));
				if (m_DisconnectionCompletionSource != null)
				{
					m_DisconnectionCompletionSource.SetResult(global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnected);
					m_DisconnectionCompletionSource = null;
				}
				if (!m_WantConnected || !ShouldReconnect((global::Unity.Services.Wire.Internal.CentrifugeCloseCode)originalCode))
				{
					return;
				}
				if (!m_Config.NetworkUtil.IsInternetReachable())
				{
					m_WantConnected = false;
					m_ReconnectionActionId = m_ActionScheduler.ScheduleAction(delegate
					{
						CheckNetworkState(m_NetworkCheckInterval);
					}, m_NetworkCheckInterval.TotalSeconds);
				}
				else
				{
					float num = ((originalCode == (global::Unity.Services.Wire.Internal.WebSocketCloseCode)4333) ? 10f : m_Backoff.GetNext());
					m_ActionScheduler.ScheduleAction(delegate
					{
						ConnectAsync();
					}, num);
				}
			}
			catch (global::System.Exception)
			{
			}
		}

		internal long CheckNetworkState(global::System.TimeSpan duration)
		{
			global::System.TimeSpan timeSpan = global::System.TimeSpan.FromSeconds(1.0);
			if (duration < timeSpan)
			{
				duration = timeSpan;
			}
			if (m_Config.NetworkUtil.IsInternetReachable())
			{
				if (m_ReconnectionActionId > 0)
				{
					m_ActionScheduler.CancelAction(m_ReconnectionActionId);
				}
				m_ReconnectionActionId = 0L;
				ConnectAsync();
				return m_ReconnectionActionId;
			}
			return m_ActionScheduler.ScheduleAction(delegate
			{
				CheckNetworkState(duration);
			}, duration.TotalSeconds);
		}

		private void InitWebsocket()
		{
			if (m_WebsocketClient != null)
			{
				m_WebsocketClient.OnOpen -= WebsocketOpenListener;
				m_WebsocketClient.OnMessage -= WebsocketMessageListener;
				m_WebsocketClient.OnError -= WebsocketErrorListener;
				m_WebsocketClient.OnClose -= WebsocketCloseListener;
			}
			m_WebsocketClient = m_WebsocketFactory.CreateInstance(m_Config.address);
			m_WebsocketClient.OnOpen += WebsocketOpenListener;
			m_WebsocketClient.OnMessage += WebsocketMessageListener;
			m_WebsocketClient.OnError += WebsocketErrorListener;
			m_WebsocketClient.OnClose += WebsocketCloseListener;
		}

		private bool ShouldReconnect(global::Unity.Services.Wire.Internal.CentrifugeCloseCode code)
		{
			switch (code)
			{
			case global::Unity.Services.Wire.Internal.CentrifugeCloseCode.WebsocketUnsupportedData:
			case global::Unity.Services.Wire.Internal.CentrifugeCloseCode.WebsocketMandatoryExtension:
			case global::Unity.Services.Wire.Internal.CentrifugeCloseCode.InvalidToken:
			case global::Unity.Services.Wire.Internal.CentrifugeCloseCode.ForceNoReconnect:
				return false;
			default:
				return true;
			}
		}

		private void ChangeConnectionState(global::Unity.Services.Wire.Internal.Client.ConnectionState state)
		{
			global::System.Collections.Generic.Dictionary<string, string> tags = new global::System.Collections.Generic.Dictionary<string, string> { 
			{
				"state",
				state.ToString()
			} };
			m_Metrics.SendSumMetric("connection_state_change", 1.0, tags);
			m_ConnectionState = state;
			switch (state)
			{
			case global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnected:
				SubscriptionRepository.OnSocketClosed();
				break;
			case global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected:
				m_ConnectionCompletionSource.SetResult(global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected);
				m_ConnectionCompletionSource = null;
				this.m_OnConnected?.Invoke();
				this.m_OnConnected = null;
				break;
			case global::Unity.Services.Wire.Internal.Client.ConnectionState.Connecting:
				m_ConnectionCompletionSource = new global::System.Threading.Tasks.TaskCompletionSource<global::Unity.Services.Wire.Internal.Client.ConnectionState>();
				break;
			default:
				throw new global::System.NotImplementedException();
			case global::Unity.Services.Wire.Internal.Client.ConnectionState.Disconnecting:
				break;
			}
		}

		private void HandlePushMessage(global::Unity.Services.Wire.Protocol.Internal.Push push)
		{
			global::System.Collections.Generic.Dictionary<string, string> tags = new global::System.Collections.Generic.Dictionary<string, string> { 
			{
				"push_type",
				push.GetPushType()
			} };
			m_Metrics.SendSumMetric("push_received", 1.0, tags);
			if (push.IsUnsub())
			{
				global::Unity.Services.Wire.Internal.Subscription sub = SubscriptionRepository.GetSub(push.channel);
				if (sub != null)
				{
					sub.OnKickReceived();
					SubscriptionRepository.RemoveSub(sub);
				}
			}
			else
			{
				if (!push.IsPub())
				{
					throw new global::System.NotImplementedException();
				}
				SubscriptionRepository.GetSub(push.channel)?.ProcessPublication(push.pub);
			}
		}

		private void HandleCommandReply(global::Unity.Services.Wire.Protocol.Internal.Reply reply)
		{
			m_CommandManager.OnCommandReplyReceived(reply);
		}

		private async global::System.Threading.Tasks.Task SubscribeAsync(global::Unity.Services.Wire.Internal.Subscription subscription)
		{
			if (m_Disabled)
			{
				return;
			}
			if (m_ConnectionState != global::Unity.Services.Wire.Internal.Client.ConnectionState.Connected)
			{
				global::System.Threading.Tasks.TaskCompletionSource<bool> tcs = new global::System.Threading.Tasks.TaskCompletionSource<bool>();
				m_OnConnected += delegate
				{
					tcs.SetResult(result: true);
				};
				try
				{
					await ConnectAsync();
				}
				catch (global::System.Exception)
				{
				}
				await tcs.Task;
			}
			try
			{
				string token = await subscription.RetrieveTokenAsync();
				if (SubscriptionRepository.IsAlreadySubscribed(subscription))
				{
					throw new global::Unity.Services.Wire.Internal.AlreadySubscribedException(subscription.Channel);
				}
				bool recover = SubscriptionRepository.IsRecovering(subscription);
				global::Unity.Services.Wire.Protocol.Internal.Command command = new global::Unity.Services.Wire.Protocol.Internal.Command(new global::Unity.Services.Wire.Protocol.Internal.SubscribeRequest
				{
					channel = subscription.Channel,
					token = token,
					recover = recover,
					offset = subscription.Offset
				});
				global::Unity.Services.Wire.Protocol.Internal.Reply reply = await SendCommandAsync(command.id, command);
				subscription.Epoch = reply.subscribe.epoch;
				SubscriptionRepository.OnSubscriptionComplete(subscription, reply.subscribe);
			}
			catch (global::System.Exception ex2)
			{
				subscription.OnError("Subscription failed: " + ex2.Message);
				throw;
			}
		}

		public global::Unity.Services.Wire.Internal.IChannel CreateChannel(global::Unity.Services.Wire.Internal.IChannelTokenProvider tokenProvider)
		{
			global::Unity.Services.Wire.Internal.Subscription subscription = new global::Unity.Services.Wire.Internal.Subscription(tokenProvider);
			subscription.UnsubscribeReceived += async delegate(global::System.Threading.Tasks.TaskCompletionSource<bool> completionSource)
			{
				try
				{
					if (SubscriptionRepository.IsAlreadySubscribed(subscription))
					{
						await UnsubscribeAsync(subscription);
					}
					else
					{
						SubscriptionRepository.RemoveSub(subscription);
					}
					completionSource.SetResult(result: true);
				}
				catch (global::System.Exception exception)
				{
					completionSource.SetException(exception);
				}
			};
			subscription.SubscribeReceived += async delegate(global::System.Threading.Tasks.TaskCompletionSource<bool> completionSource)
			{
				try
				{
					await SubscribeAsync(subscription);
					completionSource.SetResult(result: true);
				}
				catch (global::System.Exception exception)
				{
					completionSource.SetException(exception);
				}
			};
			subscription.KickReceived += delegate
			{
				SubscriptionRepository.RemoveSub(subscription);
			};
			subscription.DisposeReceived += delegate
			{
				SubscriptionRepository.RemoveSub(subscription);
			};
			return subscription;
		}

		public void Disable()
		{
			m_Disabled = true;
			m_ThreadUtils.Send((global::System.Func<global::System.Threading.Tasks.Task>)DisconnectAsync);
		}

		private async global::System.Threading.Tasks.Task UnsubscribeAsync(global::Unity.Services.Wire.Internal.Subscription subscription)
		{
			if (m_WantConnected && !m_Disabled)
			{
				if (!SubscriptionRepository.IsAlreadySubscribed(subscription))
				{
					throw new global::Unity.Services.Wire.Internal.AlreadyUnsubscribedException(subscription.Channel);
				}
				global::Unity.Services.Wire.Protocol.Internal.Command command = new global::Unity.Services.Wire.Protocol.Internal.Command(new global::Unity.Services.Wire.Protocol.Internal.UnsubscribeRequest
				{
					channel = subscription.Channel
				});
				await SendCommandAsync(command.id, command);
				SubscriptionRepository.RemoveSub(subscription);
			}
		}

		private async void WebsocketOpenListener()
		{
			await m_ThreadUtils.PostAsync(OnWebsocketOpen);
		}

		private async void WebsocketCloseListener(global::Unity.Services.Wire.Internal.WebSocketCloseCode code)
		{
			await m_ThreadUtils.PostAsync(delegate
			{
				OnWebsocketClose(code);
			});
		}

		private async void WebsocketErrorListener(string msg)
		{
			await m_ThreadUtils.PostAsync(delegate
			{
				OnWebsocketError(msg);
			});
		}

		private async void WebsocketMessageListener(byte[] data)
		{
			await m_ThreadUtils.PostAsync(delegate
			{
				OnWebsocketMessage(data);
			});
		}
	}
}

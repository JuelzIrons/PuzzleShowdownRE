namespace Unity.Netcode
{
	internal class SceneEventProgress
	{
		internal delegate bool OnCompletedDelegate(global::Unity.Netcode.SceneEventProgress sceneEventProgress);

		internal global::System.Collections.Generic.List<ulong> ClientsThatDisconnected = new global::System.Collections.Generic.List<ulong>();

		internal float WhenSceneEventHasTimedOut;

		internal global::Unity.Netcode.SceneEventProgress.OnCompletedDelegate OnComplete;

		internal global::System.Action<uint> OnSceneEventCompleted;

		internal uint SceneEventId;

		private global::UnityEngine.Coroutine m_TimeOutCoroutine;

		private global::UnityEngine.AsyncOperation m_AsyncOperation;

		internal global::UnityEngine.SceneManagement.LoadSceneMode LoadSceneMode;

		internal global::System.Collections.Generic.Dictionary<ulong, bool> ClientsProcessingSceneEvent { get; } = new global::System.Collections.Generic.Dictionary<ulong, bool>();

		internal uint SceneHash { get; set; }

		internal global::System.Guid Guid { get; } = global::System.Guid.NewGuid();

		private global::Unity.Netcode.NetworkManager m_NetworkManager { get; }

		internal global::Unity.Netcode.SceneEventProgressStatus Status { get; set; }

		internal global::Unity.Netcode.SceneEventType SceneEventType { get; set; }

		internal bool HasTimedOut()
		{
			return WhenSceneEventHasTimedOut <= m_NetworkManager.RealTimeProvider.RealTimeSinceStartup;
		}

		internal global::System.Collections.Generic.List<ulong> GetClientsWithStatus(bool completedSceneEvent)
		{
			global::System.Collections.Generic.List<ulong> list = new global::System.Collections.Generic.List<ulong>();
			if (completedSceneEvent)
			{
				if ((m_NetworkManager.IsHost || m_NetworkManager.LocalClient.IsSessionOwner) && m_AsyncOperation.isDone)
				{
					list.Add(m_NetworkManager.LocalClientId);
				}
				foreach (global::System.Collections.Generic.KeyValuePair<ulong, bool> item in ClientsProcessingSceneEvent)
				{
					if (item.Value == completedSceneEvent)
					{
						list.Add(item.Key);
					}
				}
			}
			else
			{
				if (m_NetworkManager.IsHost && !m_AsyncOperation.isDone)
				{
					list.Add(m_NetworkManager.LocalClientId);
				}
				list.AddRange(ClientsThatDisconnected);
			}
			return list;
		}

		internal SceneEventProgress(global::Unity.Netcode.NetworkManager networkManager, global::Unity.Netcode.SceneEventProgressStatus status = global::Unity.Netcode.SceneEventProgressStatus.Started)
		{
			if (status == global::Unity.Netcode.SceneEventProgressStatus.Started)
			{
				m_NetworkManager = networkManager;
				WhenSceneEventHasTimedOut = networkManager.RealTimeProvider.RealTimeSinceStartup + (float)networkManager.NetworkConfig.LoadSceneTimeOut;
				if ((networkManager.IsServer && !networkManager.DistributedAuthorityMode) || (networkManager.DistributedAuthorityMode && networkManager.LocalClient.IsSessionOwner))
				{
					m_NetworkManager.OnClientDisconnectCallback += OnClientDisconnectCallback;
					foreach (ulong connectedClientId in networkManager.ConnectionManager.ConnectedClientIds)
					{
						if ((networkManager.DistributedAuthorityMode || connectedClientId != 0L) && (!networkManager.DistributedAuthorityMode || networkManager.CurrentSessionOwner != connectedClientId))
						{
							ClientsProcessingSceneEvent.Add(connectedClientId, value: false);
						}
					}
					m_TimeOutCoroutine = m_NetworkManager.StartCoroutine(TimeOutSceneEventProgress());
				}
			}
			Status = status;
		}

		private void OnClientDisconnectCallback(ulong clientId)
		{
			if (ClientsProcessingSceneEvent.ContainsKey(clientId))
			{
				ClientsThatDisconnected.Add(clientId);
				ClientsProcessingSceneEvent.Remove(clientId);
			}
		}

		internal global::System.Collections.IEnumerator TimeOutSceneEventProgress()
		{
			global::UnityEngine.WaitForSeconds waitForNetworkTick = new global::UnityEngine.WaitForSeconds(1f / (float)m_NetworkManager.NetworkConfig.TickRate);
			while (!HasTimedOut())
			{
				yield return waitForNetworkTick;
				TryFinishingSceneEventProgress();
			}
		}

		internal void ClientFinishedSceneEvent(ulong clientId)
		{
			if (ClientsProcessingSceneEvent.ContainsKey(clientId))
			{
				ClientsProcessingSceneEvent[clientId] = true;
				TryFinishingSceneEventProgress();
			}
		}

		internal bool IsUnloading()
		{
			global::Unity.Netcode.SceneEventType sceneEventType = SceneEventType;
			return sceneEventType == global::Unity.Netcode.SceneEventType.Unload || sceneEventType == global::Unity.Netcode.SceneEventType.UnloadComplete || sceneEventType == global::Unity.Netcode.SceneEventType.UnloadEventCompleted;
		}

		private bool HasFinished()
		{
			if (!IsNetworkSessionActive())
			{
				return true;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<ulong, bool> item in ClientsProcessingSceneEvent)
			{
				if (!item.Value)
				{
					return false;
				}
			}
			if (m_AsyncOperation != null)
			{
				return m_AsyncOperation.isDone;
			}
			return false;
		}

		internal void SetAsyncOperation(global::UnityEngine.AsyncOperation asyncOperation)
		{
			m_AsyncOperation = asyncOperation;
			m_AsyncOperation.completed += delegate
			{
				if (IsNetworkSessionActive())
				{
					OnSceneEventCompleted?.Invoke(SceneEventId);
				}
				TryFinishingSceneEventProgress();
			};
		}

		internal bool IsNetworkSessionActive()
		{
			if (m_NetworkManager != null && m_NetworkManager.IsListening)
			{
				return !m_NetworkManager.ShutdownInProgress;
			}
			return false;
		}

		internal void TryFinishingSceneEventProgress()
		{
			if (HasFinished() || HasTimedOut())
			{
				if (IsNetworkSessionActive())
				{
					OnComplete?.Invoke(this);
					m_NetworkManager.SceneManager.SceneEventProgressTracking.Remove(Guid);
					m_NetworkManager.OnClientDisconnectCallback -= OnClientDisconnectCallback;
				}
				if (m_TimeOutCoroutine != null && m_NetworkManager != null)
				{
					m_NetworkManager.StopCoroutine(m_TimeOutCoroutine);
				}
			}
		}
	}
}

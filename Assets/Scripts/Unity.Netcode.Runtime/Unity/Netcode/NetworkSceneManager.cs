namespace Unity.Netcode
{
	public class NetworkSceneManager : global::System.IDisposable
	{
		public delegate void SceneEventDelegate(global::Unity.Netcode.SceneEvent sceneEvent);

		public delegate void OnLoadDelegateHandler(ulong clientId, string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::UnityEngine.AsyncOperation asyncOperation);

		public delegate void OnUnloadDelegateHandler(ulong clientId, string sceneName, global::UnityEngine.AsyncOperation asyncOperation);

		public delegate void OnSynchronizeDelegateHandler(ulong clientId);

		public delegate void OnEventCompletedDelegateHandler(string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::System.Collections.Generic.List<ulong> clientsCompleted, global::System.Collections.Generic.List<ulong> clientsTimedOut);

		public delegate void OnLoadCompleteDelegateHandler(ulong clientId, string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode);

		public delegate void OnUnloadCompleteDelegateHandler(ulong clientId, string sceneName);

		public delegate void OnSynchronizeCompleteDelegateHandler(ulong clientId);

		public delegate bool VerifySceneBeforeLoadingDelegateHandler(int sceneIndex, string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode);

		public delegate bool VerifySceneBeforeUnloadingDelegateHandler(global::UnityEngine.SceneManagement.Scene scene);

		internal class SceneUnloadEventHandler
		{
			private static global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkManager, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler>> s_Instances = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkManager, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler>>();

			private global::Unity.Netcode.NetworkSceneManager m_NetworkSceneManager;

			private global::UnityEngine.AsyncOperation m_AsyncOperation;

			private global::UnityEngine.SceneManagement.LoadSceneMode m_LoadSceneMode;

			private ulong m_ClientId;

			private global::UnityEngine.SceneManagement.Scene m_Scene;

			private bool m_ShuttingDown;

			internal static void RegisterScene(global::Unity.Netcode.NetworkSceneManager networkSceneManager, global::UnityEngine.SceneManagement.Scene scene, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::UnityEngine.AsyncOperation asyncOperation = null)
			{
				global::Unity.Netcode.NetworkManager networkManager = networkSceneManager.NetworkManager;
				if (!s_Instances.ContainsKey(networkManager))
				{
					s_Instances.Add(networkManager, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler>());
				}
				ulong localClientId = networkManager.LocalClientId;
				s_Instances[networkManager].Add(new global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler(networkSceneManager, scene, localClientId, loadSceneMode, asyncOperation));
			}

			private static void SceneUnloadComplete(global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler sceneUnloadEventHandler)
			{
				if (sceneUnloadEventHandler == null || sceneUnloadEventHandler.m_NetworkSceneManager == null || sceneUnloadEventHandler.m_NetworkSceneManager.NetworkManager == null)
				{
					return;
				}
				global::Unity.Netcode.NetworkManager networkManager = sceneUnloadEventHandler.m_NetworkSceneManager.NetworkManager;
				if (s_Instances.ContainsKey(networkManager))
				{
					s_Instances[networkManager].Remove(sceneUnloadEventHandler);
					if (s_Instances[networkManager].Count == 0)
					{
						s_Instances.Remove(networkManager);
					}
				}
			}

			internal static void Shutdown()
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkManager, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler>> s_Instance in s_Instances)
				{
					foreach (global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler item in s_Instance.Value)
					{
						item.OnShutdown();
					}
					s_Instance.Value.Clear();
				}
				s_Instances.Clear();
			}

			private void OnShutdown()
			{
				m_ShuttingDown = true;
				global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= SceneUnloaded;
			}

			private void SceneUnloaded(global::UnityEngine.SceneManagement.Scene scene)
			{
				if (m_Scene.handle == scene.handle && !m_ShuttingDown)
				{
					if (m_NetworkSceneManager != null && m_NetworkSceneManager.NetworkManager != null)
					{
						m_NetworkSceneManager.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
						{
							AsyncOperation = m_AsyncOperation,
							SceneEventType = global::Unity.Netcode.SceneEventType.UnloadComplete,
							SceneName = m_Scene.name,
							ScenePath = m_Scene.path,
							LoadSceneMode = m_LoadSceneMode,
							ClientId = m_ClientId
						});
						m_NetworkSceneManager.OnUnloadComplete?.Invoke(m_ClientId, m_Scene.name);
					}
					global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= SceneUnloaded;
					SceneUnloadComplete(this);
				}
			}

			private SceneUnloadEventHandler(global::Unity.Netcode.NetworkSceneManager networkSceneManager, global::UnityEngine.SceneManagement.Scene scene, ulong clientId, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::UnityEngine.AsyncOperation asyncOperation = null)
			{
				m_LoadSceneMode = loadSceneMode;
				m_AsyncOperation = asyncOperation;
				m_NetworkSceneManager = networkSceneManager;
				m_ClientId = clientId;
				m_Scene = scene;
				global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneUnloaded;
				m_NetworkSceneManager.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					AsyncOperation = m_AsyncOperation,
					SceneEventType = global::Unity.Netcode.SceneEventType.Unload,
					SceneName = m_Scene.name,
					ScenePath = m_Scene.path,
					LoadSceneMode = m_LoadSceneMode,
					ClientId = clientId
				});
				m_NetworkSceneManager.OnUnload?.Invoke(networkSceneManager.NetworkManager.LocalClientId, m_Scene.name, null);
			}
		}

		internal struct DeferredObjectsMovedEvent
		{
			internal ulong OwnerId;

			internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.List<ulong>> ObjectsMigratedTable;
		}

		internal struct DeferredObjectCreation
		{
			internal ulong SenderId;

			internal uint MessageSize;

			internal ulong[] ObserverIds;

			internal ulong[] NewObserverIds;

			internal global::Unity.Netcode.NetworkObject.SerializedObject SerializedObject;

			internal global::Unity.Netcode.FastBufferReader FastBufferReader;
		}

		public enum MapTypes
		{
			ServerToClient = 0,
			ClientToServer = 1
		}

		public struct SceneMap : global::Unity.Netcode.INetworkSerializable
		{
			public global::Unity.Netcode.NetworkSceneManager.MapTypes MapType;

			public global::UnityEngine.SceneManagement.Scene Scene;

			public bool ScenePresent;

			public string SceneName;

			public int ServerHandle;

			public int MappedLocalHandle;

			public int LocalHandle;

			public global::UnityEngine.SceneManagement.SceneHandle ServerSceneHandle;

			public global::UnityEngine.SceneManagement.SceneHandle MappedLocalSceneHandle;

			public global::UnityEngine.SceneManagement.SceneHandle LocalSceneHandle;

			private global::Unity.Netcode.NetworkSceneHandle m_ServerHandle;

			private global::Unity.Netcode.NetworkSceneHandle m_MappedLocalHandle;

			private global::Unity.Netcode.NetworkSceneHandle m_LocalHandle;

			internal SceneMap(global::Unity.Netcode.NetworkSceneManager.MapTypes mapType, global::UnityEngine.SceneManagement.Scene scene, bool isScenePresent, global::Unity.Netcode.NetworkSceneHandle serverHandle, global::Unity.Netcode.NetworkSceneHandle mappedLocalHandle)
			{
				MapType = mapType;
				Scene = scene;
				ScenePresent = isScenePresent;
				SceneName = (isScenePresent ? scene.name : "Not Present");
				m_ServerHandle = serverHandle;
				m_MappedLocalHandle = mappedLocalHandle;
				m_LocalHandle = new global::Unity.Netcode.NetworkSceneHandle(scene.handle);
				ServerSceneHandle = serverHandle;
				MappedLocalSceneHandle = mappedLocalHandle;
				LocalSceneHandle = scene.handle;
				ServerHandle = m_ServerHandle.GetRawData();
				MappedLocalHandle = m_MappedLocalHandle.GetRawData();
				LocalHandle = m_LocalHandle.GetRawData();
			}

			public void NetworkSerialize<T>(global::Unity.Netcode.BufferSerializer<T> serializer) where T : global::Unity.Netcode.IReaderWriter
			{
				serializer.SerializeValue(ref MapType, default(global::Unity.Netcode.FastBufferWriter.ForEnums));
				serializer.SerializeValue(ref ScenePresent, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (serializer.IsReader)
				{
					SceneName = "Not Present";
				}
				if (ScenePresent)
				{
					serializer.SerializeValue(ref SceneName);
					serializer.SerializeValue(ref LocalHandle, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				}
				serializer.SerializeValue(ref ServerHandle, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				serializer.SerializeValue(ref MappedLocalHandle, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
				if (serializer.IsWriter)
				{
					if (m_LocalHandle.IsEmpty() && LocalSceneHandle != global::UnityEngine.SceneManagement.SceneHandle.None)
					{
						m_LocalHandle = LocalSceneHandle;
					}
					if (m_ServerHandle.IsEmpty() && ServerSceneHandle != global::UnityEngine.SceneManagement.SceneHandle.None)
					{
						m_ServerHandle = ServerSceneHandle;
					}
					if (m_MappedLocalHandle.IsEmpty() && MappedLocalSceneHandle != global::UnityEngine.SceneManagement.SceneHandle.None)
					{
						m_MappedLocalHandle = MappedLocalSceneHandle;
					}
				}
				serializer.SerializeValue(ref m_LocalHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				serializer.SerializeValue(ref m_ServerHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				serializer.SerializeValue(ref m_MappedLocalHandle, default(global::Unity.Netcode.FastBufferWriter.ForNetworkSerializable));
				if (serializer.IsReader)
				{
					ServerSceneHandle = m_ServerHandle;
					ServerSceneHandle = m_LocalHandle;
					ServerSceneHandle = m_MappedLocalHandle;
				}
			}
		}

		internal const int InvalidSceneNameOrPath = -1;

		private global::Unity.Netcode.NetworkDelivery m_NetworkDelivery;

		internal static bool DisableReSynchronization;

		private bool m_IsSceneEventActive;

		public global::Unity.Netcode.NetworkSceneManager.VerifySceneBeforeLoadingDelegateHandler VerifySceneBeforeLoading;

		public global::Unity.Netcode.NetworkSceneManager.VerifySceneBeforeUnloadingDelegateHandler VerifySceneBeforeUnloading;

		public bool PostSynchronizationSceneUnloading;

		private bool m_ActiveSceneSynchronizationEnabled;

		internal global::Unity.Netcode.ISceneManagerHandler SceneManagerHandler = new global::Unity.Netcode.DefaultSceneManagerHandler();

		internal readonly global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.Netcode.SceneEventProgress> SceneEventProgressTracking = new global::System.Collections.Generic.Dictionary<global::System.Guid, global::Unity.Netcode.SceneEventProgress>();

		internal readonly global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>> ScenePlacedObjects = new global::System.Collections.Generic.Dictionary<uint, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>>();

		internal global::UnityEngine.SceneManagement.Scene SceneBeingSynchronized;

		internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene> ScenesLoaded = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene>();

		internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkSceneHandle> ServerSceneHandleToClientSceneHandle = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkSceneHandle>();

		internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkSceneHandle> ClientSceneHandleToServerSceneHandle = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkSceneHandle>();

		internal bool IsRestoringSession;

		internal global::System.Collections.Generic.Dictionary<uint, int> HashToBuildIndex = new global::System.Collections.Generic.Dictionary<uint, int>();

		internal global::System.Collections.Generic.Dictionary<int, uint> BuildIndexToHash = new global::System.Collections.Generic.Dictionary<int, uint>();

		internal static bool IsSpawnedObjectsPendingInDontDestroyOnLoad;

		internal global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.SceneEventData> SceneEventDataStore;

		internal readonly global::Unity.Netcode.NetworkManager NetworkManager;

		internal global::UnityEngine.SceneManagement.Scene DontDestroyOnLoadScene;

		private bool m_DisableValidationWarningMessages;

		internal global::UnityEngine.SceneManagement.LoadSceneMode DeferLoadingFilter;

		internal global::System.Func<string, global::UnityEngine.SceneManagement.Scene> OverrideGetAndAddNewlyLoadedSceneByName;

		internal global::System.Func<global::UnityEngine.SceneManagement.Scene, bool> ExcludeSceneFromSychronization;

		internal global::System.Collections.Generic.List<ulong> ClientConnectionQueue = new global::System.Collections.Generic.List<ulong>();

		internal bool SkipSceneHandling;

		private bool m_OriginalPostSynchronizationSceneUnloading;

		internal global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> ObjectsMigratedIntoNewScene = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>>();

		private global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneHandle> m_ScenesToRemoveFromObjectMigration = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneHandle>();

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.DeferredObjectsMovedEvent> DeferredObjectsMovedEvents = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.DeferredObjectsMovedEvent>();

		internal global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation> DeferredObjectCreationList = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation>();

		internal int DeferredObjectCreationCount;

		public bool ActiveSceneSynchronizationEnabled
		{
			get
			{
				return m_ActiveSceneSynchronizationEnabled;
			}
			set
			{
				if (m_ActiveSceneSynchronizationEnabled != value)
				{
					m_ActiveSceneSynchronizationEnabled = value;
					if (m_ActiveSceneSynchronizationEnabled)
					{
						global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
					}
					else
					{
						global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneManager_ActiveSceneChanged;
					}
				}
			}
		}

		public global::UnityEngine.SceneManagement.LoadSceneMode ClientSynchronizationMode { get; internal set; }

		public event global::Unity.Netcode.NetworkSceneManager.SceneEventDelegate OnSceneEvent;

		public event global::Unity.Netcode.NetworkSceneManager.OnLoadDelegateHandler OnLoad;

		public event global::Unity.Netcode.NetworkSceneManager.OnUnloadDelegateHandler OnUnload;

		public event global::Unity.Netcode.NetworkSceneManager.OnSynchronizeDelegateHandler OnSynchronize;

		public event global::Unity.Netcode.NetworkSceneManager.OnEventCompletedDelegateHandler OnLoadEventCompleted;

		public event global::Unity.Netcode.NetworkSceneManager.OnEventCompletedDelegateHandler OnUnloadEventCompleted;

		public event global::Unity.Netcode.NetworkSceneManager.OnLoadCompleteDelegateHandler OnLoadComplete;

		public event global::Unity.Netcode.NetworkSceneManager.OnUnloadCompleteDelegateHandler OnUnloadComplete;

		public event global::Unity.Netcode.NetworkSceneManager.OnSynchronizeCompleteDelegateHandler OnSynchronizeComplete;

		public global::System.Collections.Generic.List<global::UnityEngine.SceneManagement.Scene> GetSynchronizedScenes()
		{
			return global::System.Linq.Enumerable.ToList(ScenesLoaded.Values);
		}

		internal bool UpdateServerClientSceneHandle(global::Unity.Netcode.NetworkSceneHandle serverHandle, global::Unity.Netcode.NetworkSceneHandle clientHandle, global::UnityEngine.SceneManagement.Scene localScene)
		{
			if (!ServerSceneHandleToClientSceneHandle.ContainsKey(serverHandle))
			{
				ServerSceneHandleToClientSceneHandle.Add(serverHandle, clientHandle);
			}
			else if (!IsRestoringSession)
			{
				return false;
			}
			if (!ClientSceneHandleToServerSceneHandle.ContainsKey(clientHandle))
			{
				ClientSceneHandleToServerSceneHandle.Add(clientHandle, serverHandle);
			}
			else if (!IsRestoringSession)
			{
				return false;
			}
			if (!ScenesLoaded.ContainsKey(clientHandle))
			{
				ScenesLoaded.Add(clientHandle, localScene);
			}
			return true;
		}

		internal bool RemoveServerClientSceneHandle(global::Unity.Netcode.NetworkSceneHandle serverHandle, global::Unity.Netcode.NetworkSceneHandle clientHandle)
		{
			if (ServerSceneHandleToClientSceneHandle.ContainsKey(serverHandle))
			{
				ServerSceneHandleToClientSceneHandle.Remove(serverHandle);
				if (ClientSceneHandleToServerSceneHandle.ContainsKey(clientHandle))
				{
					ClientSceneHandleToServerSceneHandle.Remove(clientHandle);
					if (ScenesLoaded.ContainsKey(clientHandle))
					{
						ScenesLoaded.Remove(clientHandle);
						return true;
					}
					return false;
				}
				return false;
			}
			return false;
		}

		internal bool HasSceneAuthority()
		{
			if (!NetworkManager)
			{
				return false;
			}
			if (NetworkManager.DistributedAuthorityMode || !NetworkManager.IsServer)
			{
				if (NetworkManager.DistributedAuthorityMode)
				{
					return NetworkManager.LocalClient.IsSessionOwner;
				}
				return false;
			}
			return true;
		}

		public void Dispose()
		{
			global::UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneManager_ActiveSceneChanged;
			global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler.Shutdown();
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::Unity.Netcode.SceneEventData> item in SceneEventDataStore)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogInfo(string.Format("{0} is disposing {1} '{2}'.", "SceneEventDataStore", "SceneEventId", item.Key));
				}
				item.Value.Dispose();
			}
			SceneEventDataStore.Clear();
			SceneEventDataStore = null;
		}

		internal global::Unity.Netcode.SceneEventData BeginSceneEvent()
		{
			global::Unity.Netcode.SceneEventData sceneEventData = new global::Unity.Netcode.SceneEventData(NetworkManager);
			SceneEventDataStore.Add(sceneEventData.SceneEventId, sceneEventData);
			return sceneEventData;
		}

		internal void EndSceneEvent(uint sceneEventId)
		{
			if (SceneEventDataStore.ContainsKey(sceneEventId))
			{
				SceneEventDataStore[sceneEventId].Dispose();
				SceneEventDataStore.Remove(sceneEventId);
			}
			else
			{
				global::UnityEngine.Debug.LogWarning($"Trying to dispose and remove SceneEventData Id '{sceneEventId}' that no longer exists!");
			}
		}

		internal bool ShouldDeferCreateObject()
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement || HasSceneAuthority())
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::Unity.Netcode.SceneEventData> item in SceneEventDataStore)
			{
				if (item.Value.SceneEventType == global::Unity.Netcode.SceneEventType.Synchronize)
				{
					flag = true;
				}
				if (item.Value.SceneEventType == global::Unity.Netcode.SceneEventType.Load && item.Value.LoadSceneMode == DeferLoadingFilter)
				{
					flag2 = true;
				}
			}
			if (!flag || ClientSynchronizationMode != global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				return !flag && flag2;
			}
			return true;
		}

		internal string GetSceneNameFromPath(string scenePath)
		{
			int num = scenePath.LastIndexOf("/", global::System.StringComparison.Ordinal) + 1;
			int num2 = scenePath.LastIndexOf(".", global::System.StringComparison.Ordinal);
			return scenePath.Substring(num, num2 - num);
		}

		internal void GenerateScenesInBuild()
		{
			HashToBuildIndex.Clear();
			BuildIndexToHash.Clear();
			for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
			{
				string scenePathByBuildIndex = global::UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
				uint num = scenePathByBuildIndex.Hash32();
				int buildIndexByScenePath = global::UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(scenePathByBuildIndex);
				if (!HashToBuildIndex.ContainsKey(num))
				{
					HashToBuildIndex.Add(num, buildIndexByScenePath);
					BuildIndexToHash.Add(buildIndexByScenePath, num);
				}
				else
				{
					global::UnityEngine.Debug.LogError("NetworkSceneManager is skipping duplicate scene path entry " + scenePathByBuildIndex + ". Make sure your scenes in build list does not contain duplicates!");
				}
			}
		}

		internal string SceneNameFromHash(uint sceneHash)
		{
			if (sceneHash == 0)
			{
				return "No Scene";
			}
			return GetSceneNameFromPath(ScenePathFromHash(sceneHash));
		}

		internal string ScenePathFromHash(uint sceneHash)
		{
			if (HashToBuildIndex.ContainsKey(sceneHash))
			{
				return global::UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(HashToBuildIndex[sceneHash]);
			}
			throw new global::System.Exception(string.Format("Scene Hash {0} does not exist in the {1} table!  Verify that all scenes requiring", sceneHash, "HashToBuildIndex") + " server to client synchronization are in the scenes in build list.");
		}

		internal uint SceneHashFromNameOrPath(string sceneNameOrPath)
		{
			int buildIndexByScenePath = global::UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(sceneNameOrPath);
			if (buildIndexByScenePath >= 0)
			{
				if (BuildIndexToHash.ContainsKey(buildIndexByScenePath))
				{
					return BuildIndexToHash[buildIndexByScenePath];
				}
				throw new global::System.Exception(string.Format("Scene '{0}' has a build index of {1} that does not exist in the {2} table!", sceneNameOrPath, buildIndexByScenePath, "BuildIndexToHash"));
			}
			throw new global::System.Exception("Scene '" + sceneNameOrPath + "' couldn't be loaded because it has not been added to the build settings scenes in build list.");
		}

		public void DisableValidationWarnings(bool disabled)
		{
			m_DisableValidationWarningMessages = disabled;
		}

		public void SetClientSynchronizationMode(global::UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			SceneManagerHandler.SetClientSynchronizationMode(ref networkManager, mode);
		}

		internal NetworkSceneManager(global::Unity.Netcode.NetworkManager networkManager)
		{
			NetworkManager = networkManager;
			SceneEventDataStore = new global::System.Collections.Generic.Dictionary<uint, global::Unity.Netcode.SceneEventData>();
			m_NetworkDelivery = global::Unity.Netcode.MessageDeliveryType<global::Unity.Netcode.SceneEventMessage>.DefaultDelivery;
			GenerateScenesInBuild();
			DontDestroyOnLoadScene = networkManager.gameObject.scene;
			if (!NetworkManager.DistributedAuthorityMode && NetworkManager.IsServer && networkManager.NetworkConfig.EnableSceneManagement)
			{
				for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
				{
					global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
					ScenesLoaded.Add(sceneAt.handle, sceneAt);
				}
				SceneManagerHandler.PopulateLoadedScenes(ref ScenesLoaded, NetworkManager);
			}
			UpdateServerClientSceneHandle(DontDestroyOnLoadScene.handle, DontDestroyOnLoadScene.handle, DontDestroyOnLoadScene);
		}

		internal void InitializeScenesLoaded()
		{
			if (NetworkManager.DistributedAuthorityMode && HasSceneAuthority() && NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
				{
					global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
					UpdateServerClientSceneHandle(sceneAt.handle, sceneAt.handle, sceneAt);
				}
				SceneManagerHandler.PopulateLoadedScenes(ref ScenesLoaded, NetworkManager);
			}
		}

		private void SceneManager_ActiveSceneChanged(global::UnityEngine.SceneManagement.Scene current, global::UnityEngine.SceneManagement.Scene next)
		{
			if ((!NetworkManager.DistributedAuthorityMode && !NetworkManager.IsServer) || (NetworkManager.DistributedAuthorityMode && !NetworkManager.LocalClient.IsSessionOwner) || NetworkManager.ConnectedClientsIds.Count <= (NetworkManager.IsHost ? 1 : 0))
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Guid, global::Unity.Netcode.SceneEventProgress> item in SceneEventProgressTracking)
			{
				if (!item.Value.HasTimedOut() && item.Value.Status == global::Unity.Netcode.SceneEventProgressStatus.Started)
				{
					return;
				}
			}
			if (BuildIndexToHash.ContainsKey(next.buildIndex))
			{
				global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
				sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.ActiveSceneChanged;
				sceneEventData.ActiveSceneHash = BuildIndexToHash[next.buildIndex];
				ulong sessionOwner = 0uL;
				if (NetworkManager.DistributedAuthorityMode)
				{
					sessionOwner = NetworkManager.CurrentSessionOwner;
				}
				SendSceneEventData(sceneEventData.SceneEventId, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(NetworkManager.ConnectedClientsIds, (ulong c) => c != sessionOwner)));
				EndSceneEvent(sceneEventData.SceneEventId);
			}
		}

		internal bool ValidateSceneBeforeLoading(uint sceneHash, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
		{
			string text = SceneNameFromHash(sceneHash);
			int buildIndexByScenePath = global::UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(text);
			return ValidateSceneBeforeLoading(buildIndexByScenePath, text, loadSceneMode);
		}

		internal bool ValidateSceneBeforeLoading(int sceneIndex, string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
		{
			bool flag = true;
			if (VerifySceneBeforeLoading != null)
			{
				flag = VerifySceneBeforeLoading(sceneIndex, sceneName, loadSceneMode);
			}
			if (!flag && !m_DisableValidationWarningMessages)
			{
				string text = "Client";
				if (HasSceneAuthority())
				{
					text = (NetworkManager.DistributedAuthorityMode ? "Session Owner" : (NetworkManager.IsHost ? "Host" : "Server"));
				}
				global::UnityEngine.Debug.LogWarning($"Scene {sceneName} of Scenes in Build Index {sceneIndex} being loaded in {loadSceneMode} mode failed validation on the {text}!");
			}
			return flag;
		}

		internal global::UnityEngine.SceneManagement.Scene GetAndAddNewlyLoadedSceneByName(string sceneName)
		{
			if (OverrideGetAndAddNewlyLoadedSceneByName != null)
			{
				return OverrideGetAndAddNewlyLoadedSceneByName(sceneName);
			}
			for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
			{
				global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
				if (sceneAt.name == sceneName && !ScenesLoaded.ContainsKey(sceneAt.handle))
				{
					ScenesLoaded.Add(sceneAt.handle, sceneAt);
					SceneManagerHandler.StartTrackingScene(sceneAt, assigned: true, NetworkManager);
					return sceneAt;
				}
			}
			throw new global::System.Exception("Failed to find any loaded scene named " + sceneName + "!");
		}

		internal void SetTheSceneBeingSynchronized(global::Unity.Netcode.NetworkSceneHandle serverSceneHandle)
		{
			global::Unity.Netcode.NetworkSceneHandle networkSceneHandle = serverSceneHandle;
			if (ServerSceneHandleToClientSceneHandle.ContainsKey(serverSceneHandle))
			{
				networkSceneHandle = ServerSceneHandleToClientSceneHandle[serverSceneHandle];
				if (!SceneBeingSynchronized.IsValid() || !SceneBeingSynchronized.isLoaded || !(SceneBeingSynchronized.handle == networkSceneHandle))
				{
					SceneBeingSynchronized = (ScenesLoaded.ContainsKey(networkSceneHandle) ? ScenesLoaded[networkSceneHandle] : default(global::UnityEngine.SceneManagement.Scene));
					if (!SceneBeingSynchronized.IsValid() || !SceneBeingSynchronized.isLoaded)
					{
						SceneBeingSynchronized = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
						global::UnityEngine.Debug.LogWarning("[NetworkSceneManager- ScenesLoaded] Could not find the appropriate scene to set as being synchronized! Using the currently active scene.");
					}
				}
			}
			else if (serverSceneHandle == DontDestroyOnLoadScene.handle)
			{
				SceneBeingSynchronized = NetworkManager.gameObject.scene;
			}
			else
			{
				SceneBeingSynchronized = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
				global::UnityEngine.Debug.LogWarning(string.Format("[{0}- Scene Handle Mismatch] {1} ({2}) could not be found in {3}. Using the currently active scene.", "SceneEventData", "serverSceneHandle", serverSceneHandle, "ServerSceneHandleToClientSceneHandle"));
			}
		}

		internal global::Unity.Netcode.NetworkObject GetSceneRelativeInSceneNetworkObject(uint globalObjectIdHash, global::Unity.Netcode.NetworkSceneHandle? networkSceneHandle)
		{
			if (ScenePlacedObjects.TryGetValue(globalObjectIdHash, out var value))
			{
				global::Unity.Netcode.NetworkSceneHandle key = SceneBeingSynchronized.handle;
				if (networkSceneHandle.HasValue && !networkSceneHandle.Value.IsEmpty() && ServerSceneHandleToClientSceneHandle.TryGetValue(networkSceneHandle.Value, out var value2))
				{
					key = value2;
				}
				if (value.TryGetValue(key, out var value3))
				{
					return value3;
				}
			}
			return null;
		}

		private void SendSceneEventData(uint sceneEventId, ulong[] targetClientIds)
		{
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			if (targetClientIds.Length != 0 || distributedAuthorityMode)
			{
				global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
				sceneEventData.SenderClientId = NetworkManager.LocalClientId;
				if (distributedAuthorityMode && NetworkManager.CMBServiceConnection && HasSceneAuthority())
				{
					sceneEventData.TargetClientId = 0uL;
					global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
					{
						EventData = sceneEventData
					};
					int num = NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, 0uL);
					NetworkManager.NetworkMetrics.TrackSceneEventSent(0uL, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), num);
				}
				for (int i = 0; i < targetClientIds.Length; i++)
				{
					ulong num2 = (sceneEventData.TargetClientId = targetClientIds[i]);
					global::Unity.Netcode.SceneEventMessage message2 = new global::Unity.Netcode.SceneEventMessage
					{
						EventData = sceneEventData
					};
					ulong clientId = ((distributedAuthorityMode && !NetworkManager.DAHost) ? 0 : num2);
					int num3 = NetworkManager.ConnectionManager.SendMessage(ref message2, m_NetworkDelivery, clientId);
					NetworkManager.NetworkMetrics.TrackSceneEventSent(num2, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), num3);
				}
			}
		}

		private global::Unity.Netcode.SceneEventProgress ValidateSceneEventUnloading(global::UnityEngine.SceneManagement.Scene scene)
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				global::UnityEngine.Debug.LogWarning("LoadScene was called, but EnableSceneManagement was not enabled! Enable EnableSceneManagement prior to starting a client, host, or server prior to using NetworkSceneManager!");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SceneManagementNotEnabled);
			}
			if (!HasSceneAuthority())
			{
				if (NetworkManager.DistributedAuthorityMode)
				{
					global::UnityEngine.Debug.LogWarning("[SessionOwnerOnlyAction][Unload] Clients cannot invoke the UnloadScene method!");
					return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SessionOwnerOnlyAction);
				}
				global::UnityEngine.Debug.LogWarning("[ServerOnlyAction][Unload] Clients cannot invoke the UnloadScene method!");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.ServerOnlyAction);
			}
			if (!scene.isLoaded)
			{
				global::UnityEngine.Debug.LogWarning("UnloadScene was called, but the scene " + scene.name + " is not currently loaded!");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SceneNotLoaded);
			}
			return ValidateSceneEvent(scene.name, isUnloading: true);
		}

		private global::Unity.Netcode.SceneEventProgress ValidateSceneEventLoading(string sceneName)
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				global::UnityEngine.Debug.LogWarning("LoadScene was called, but EnableSceneManagement was not enabled! Enable EnableSceneManagement prior to starting a client, host, or server prior to using NetworkSceneManager!");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SceneManagementNotEnabled);
			}
			if (!HasSceneAuthority())
			{
				if (NetworkManager.DistributedAuthorityMode)
				{
					global::UnityEngine.Debug.LogWarning("[SessionOwnerOnlyAction][Load] Only the session owner can invoke the LoadScene method!");
					return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SessionOwnerOnlyAction);
				}
				global::UnityEngine.Debug.LogWarning("[ServerOnlyAction][Load] Clients cannot invoke the LoadScene method!");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.ServerOnlyAction);
			}
			return ValidateSceneEvent(sceneName);
		}

		private global::Unity.Netcode.SceneEventProgress ValidateSceneEvent(string sceneName, bool isUnloading = false)
		{
			if (m_IsSceneEventActive)
			{
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.SceneEventInProgress);
			}
			if (global::UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
			{
				global::UnityEngine.Debug.LogError("Scene '" + sceneName + "' couldn't be loaded because it has not been added to the build settings scenes in build list.");
				return new global::Unity.Netcode.SceneEventProgress(null, global::Unity.Netcode.SceneEventProgressStatus.InvalidSceneName);
			}
			global::Unity.Netcode.SceneEventProgress sceneEventProgress = new global::Unity.Netcode.SceneEventProgress(NetworkManager)
			{
				SceneHash = SceneHashFromNameOrPath(sceneName)
			};
			SceneEventProgressTracking.Add(sceneEventProgress.Guid, sceneEventProgress);
			m_IsSceneEventActive = true;
			sceneEventProgress.OnComplete = OnSceneEventProgressCompleted;
			return sceneEventProgress;
		}

		private bool OnSceneEventProgressCompleted(global::Unity.Netcode.SceneEventProgress sceneEventProgress)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
			global::System.Collections.Generic.List<ulong> clientsWithStatus = sceneEventProgress.GetClientsWithStatus(completedSceneEvent: true);
			global::System.Collections.Generic.List<ulong> clientsWithStatus2 = sceneEventProgress.GetClientsWithStatus(completedSceneEvent: false);
			sceneEventData.SceneEventProgressId = sceneEventProgress.Guid;
			sceneEventData.SceneHash = sceneEventProgress.SceneHash;
			sceneEventData.SceneEventType = sceneEventProgress.SceneEventType;
			sceneEventData.ClientsCompleted = clientsWithStatus;
			sceneEventData.LoadSceneMode = sceneEventProgress.LoadSceneMode;
			sceneEventData.ClientsTimedOut = clientsWithStatus2;
			if (NetworkManager.DistributedAuthorityMode)
			{
				SendSceneEventData(sceneEventData.SceneEventId, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(NetworkManager.ConnectedClientsIds, (ulong c) => c != NetworkManager.LocalClientId)));
			}
			else
			{
				global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
				{
					EventData = sceneEventData
				};
				int num = NetworkManager.ConnectionManager.SendMessage<global::Unity.Netcode.SceneEventMessage, global::System.Collections.Generic.IReadOnlyList<ulong>>(ref message, m_NetworkDelivery, NetworkManager.ConnectedClientsIds);
				NetworkManager.NetworkMetrics.TrackSceneEventSent(NetworkManager.ConnectedClientsIds, (uint)sceneEventProgress.SceneEventType, SceneNameFromHash(sceneEventProgress.SceneHash), num);
			}
			InvokeSceneEvents(NetworkManager.CurrentSessionOwner, sceneEventData);
			EndSceneEvent(sceneEventData.SceneEventId);
			return true;
		}

		public global::Unity.Netcode.SceneEventProgressStatus UnloadScene(global::UnityEngine.SceneManagement.Scene scene)
		{
			string name = scene.name;
			global::Unity.Netcode.NetworkSceneHandle networkSceneHandle = scene.handle;
			if (!scene.isLoaded)
			{
				global::UnityEngine.Debug.LogWarning("UnloadScene was called, but the scene " + scene.name + " is not currently loaded!");
				return global::Unity.Netcode.SceneEventProgressStatus.SceneNotLoaded;
			}
			global::Unity.Netcode.SceneEventProgress sceneEventProgress = ValidateSceneEventUnloading(scene);
			if (sceneEventProgress.Status != global::Unity.Netcode.SceneEventProgressStatus.Started)
			{
				return sceneEventProgress.Status;
			}
			if (!ScenesLoaded.ContainsKey(networkSceneHandle))
			{
				global::UnityEngine.Debug.LogError(string.Format("{0} internal error! {1} with handle {2} is not within the internal scenes loaded dictionary!", "UnloadScene", name, scene.handle));
				return global::Unity.Netcode.SceneEventProgressStatus.InternalNetcodeError;
			}
			if (NetworkManager.DistributedAuthorityMode && ClientSceneHandleToServerSceneHandle.ContainsKey(networkSceneHandle))
			{
				networkSceneHandle = ClientSceneHandleToServerSceneHandle[networkSceneHandle];
			}
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
			sceneEventData.SceneEventProgressId = sceneEventProgress.Guid;
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.Unload;
			sceneEventData.SceneHash = SceneHashFromNameOrPath(name);
			sceneEventData.LoadSceneMode = global::UnityEngine.SceneManagement.LoadSceneMode.Additive;
			sceneEventData.SceneHandle = networkSceneHandle;
			sceneEventProgress.SceneEventType = global::Unity.Netcode.SceneEventType.UnloadEventCompleted;
			sceneEventProgress.LoadSceneMode = global::UnityEngine.SceneManagement.LoadSceneMode.Additive;
			sceneEventProgress.SceneEventId = sceneEventData.SceneEventId;
			sceneEventProgress.OnSceneEventCompleted = OnSceneUnloaded;
			SceneManagerHandler.MoveObjectsFromSceneToDontDestroyOnLoad(ref networkManager, scene);
			if (!RemoveServerClientSceneHandle(sceneEventData.SceneHandle, scene.handle))
			{
				global::UnityEngine.Debug.LogError($"Failed to remove {SceneNameFromHash(sceneEventData.SceneHash)} scene handles [Server ({sceneEventData.SceneHandle})][Local({scene.handle})]");
			}
			global::UnityEngine.AsyncOperation asyncOperation = SceneManagerHandler.UnloadSceneAsync(scene, sceneEventProgress);
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, asyncOperation);
			return sceneEventProgress.Status;
		}

		private void OnClientUnloadScene(uint sceneEventId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			string text = SceneNameFromHash(sceneEventData.SceneHash);
			if (!ServerSceneHandleToClientSceneHandle.ContainsKey(sceneEventData.SceneHandle))
			{
				global::UnityEngine.Debug.Log("Client failed to unload scene " + text + " " + $"because we are missing the client scene handle due to the server scene handle {sceneEventData.SceneHandle} not being found.");
				EndSceneEvent(sceneEventId);
				return;
			}
			global::Unity.Netcode.NetworkSceneHandle networkSceneHandle = ServerSceneHandleToClientSceneHandle[sceneEventData.SceneHandle];
			if (!ScenesLoaded.ContainsKey(networkSceneHandle))
			{
				throw new global::System.Exception("Client failed to unload scene " + text + " " + $"because the client scene handle {networkSceneHandle} was not found in ScenesLoaded!");
			}
			global::UnityEngine.SceneManagement.Scene scene = ScenesLoaded[networkSceneHandle];
			global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
			SceneManagerHandler.MoveObjectsFromSceneToDontDestroyOnLoad(ref networkManager, scene);
			m_IsSceneEventActive = true;
			global::Unity.Netcode.SceneEventProgress sceneEventProgress = new global::Unity.Netcode.SceneEventProgress(NetworkManager)
			{
				SceneEventId = sceneEventData.SceneEventId,
				OnSceneEventCompleted = OnSceneUnloaded
			};
			if (NetworkManager.DistributedAuthorityMode)
			{
				SceneEventProgressTracking.Add(sceneEventData.SceneEventProgressId, sceneEventProgress);
			}
			global::UnityEngine.AsyncOperation asyncOperation = SceneManagerHandler.UnloadSceneAsync(scene, sceneEventProgress);
			SceneManagerHandler.StopTrackingScene(networkSceneHandle, text, NetworkManager);
			if (!RemoveServerClientSceneHandle(sceneEventData.SceneHandle, networkSceneHandle))
			{
				throw new global::System.Exception($"Failed to remove server scene handle ({sceneEventData.SceneHandle}) or client scene handle({networkSceneHandle})! Happened during scene unload for {text}.");
			}
			sceneEventData.LoadSceneMode = global::UnityEngine.SceneManagement.LoadSceneMode.Additive;
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, asyncOperation);
		}

		private void OnSceneUnloaded(uint sceneEventId)
		{
			if (!NetworkManager.IsListening || NetworkManager.ShutdownInProgress)
			{
				EndSceneEvent(sceneEventId);
				return;
			}
			MoveObjectsFromDontDestroyOnLoadToScene(global::UnityEngine.SceneManagement.SceneManager.GetActiveScene());
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			if (HasSceneAuthority())
			{
				ulong sessionOwner = (NetworkManager.DistributedAuthorityMode ? NetworkManager.CurrentSessionOwner : 0);
				SendSceneEventData(sceneEventId, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(NetworkManager.ConnectedClientsIds, (ulong c) => c != sessionOwner)));
				if (SceneEventProgressTracking.ContainsKey(sceneEventData.SceneEventProgressId) && HasSceneAuthority())
				{
					SceneEventProgressTracking[sceneEventData.SceneEventProgressId].ClientFinishedSceneEvent(sessionOwner);
				}
			}
			else if (NetworkManager.DistributedAuthorityMode)
			{
				SceneEventProgressTracking.Remove(sceneEventData.SceneEventProgressId);
				m_IsSceneEventActive = false;
			}
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.UnloadComplete;
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData);
			if (!HasSceneAuthority())
			{
				sceneEventData.TargetClientId = NetworkManager.CurrentSessionOwner;
				sceneEventData.SenderClientId = NetworkManager.LocalClientId;
				global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
				{
					EventData = sceneEventData
				};
				ulong num = (NetworkManager.DAHost ? NetworkManager.CurrentSessionOwner : 0);
				int num2 = NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, num);
				NetworkManager.NetworkMetrics.TrackSceneEventSent(num, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), num2);
			}
			EndSceneEvent(sceneEventId);
			m_IsSceneEventActive = false;
		}

		private void EmptySceneUnloadedOperation(uint sceneEventId)
		{
		}

		internal void UnloadAdditivelyLoadedScenes(uint sceneEventId)
		{
			_ = SceneEventDataStore[sceneEventId];
			global::UnityEngine.SceneManagement.Scene activeScene = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene> item in ScenesLoaded)
			{
				if (activeScene.name != item.Value.name && item.Value.buildIndex >= 0)
				{
					global::Unity.Netcode.SceneEventProgress sceneEventProgress = new global::Unity.Netcode.SceneEventProgress(NetworkManager)
					{
						SceneEventId = sceneEventId,
						OnSceneEventCompleted = EmptySceneUnloadedOperation
					};
					if (ClientSceneHandleToServerSceneHandle.ContainsKey(item.Value.handle))
					{
						global::Unity.Netcode.NetworkSceneHandle key = ClientSceneHandleToServerSceneHandle[item.Value.handle];
						ServerSceneHandleToClientSceneHandle.Remove(key);
					}
					ClientSceneHandleToServerSceneHandle.Remove(item.Value.handle);
					global::UnityEngine.AsyncOperation asyncOperation = SceneManagerHandler.UnloadSceneAsync(item.Value, sceneEventProgress);
					global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler.RegisterScene(this, item.Value, global::UnityEngine.SceneManagement.LoadSceneMode.Additive, asyncOperation);
				}
			}
			ScenesLoaded.Clear();
			SceneManagerHandler.ClearSceneTracking(NetworkManager);
		}

		public global::Unity.Netcode.SceneEventProgressStatus LoadScene(string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
		{
			global::Unity.Netcode.SceneEventProgress sceneEventProgress = ValidateSceneEventLoading(sceneName);
			if (sceneEventProgress.Status != global::Unity.Netcode.SceneEventProgressStatus.Started)
			{
				return sceneEventProgress.Status;
			}
			sceneEventProgress.SceneEventType = global::Unity.Netcode.SceneEventType.LoadEventCompleted;
			sceneEventProgress.LoadSceneMode = loadSceneMode;
			global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
			sceneEventData.SceneEventProgressId = sceneEventProgress.Guid;
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.Load;
			sceneEventData.SceneHash = SceneHashFromNameOrPath(sceneName);
			sceneEventData.LoadSceneMode = loadSceneMode;
			uint sceneEventId = sceneEventData.SceneEventId;
			sceneName = SceneNameFromHash(sceneEventData.SceneHash);
			m_IsSceneEventActive = ValidateSceneBeforeLoading(sceneEventData.SceneHash, loadSceneMode);
			if (!m_IsSceneEventActive)
			{
				EndSceneEvent(sceneEventId);
				return global::Unity.Netcode.SceneEventProgressStatus.SceneFailedVerification;
			}
			if (sceneEventData.LoadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				IsSpawnedObjectsPendingInDontDestroyOnLoad = true;
				NetworkManager.SpawnManager.ServerDestroySpawnedSceneObjects();
				MoveObjectsToDontDestroyOnLoad();
				UnloadAdditivelyLoadedScenes(sceneEventId);
				global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler.RegisterScene(this, global::UnityEngine.SceneManagement.SceneManager.GetActiveScene(), global::UnityEngine.SceneManagement.LoadSceneMode.Single);
			}
			sceneEventProgress.SceneEventId = sceneEventId;
			sceneEventProgress.OnSceneEventCompleted = OnSceneLoaded;
			global::UnityEngine.AsyncOperation asyncOperation = SceneManagerHandler.LoadSceneAsync(sceneName, loadSceneMode, sceneEventProgress);
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, asyncOperation);
			return sceneEventProgress.Status;
		}

		private void OnClientSceneLoadingEvent(uint sceneEventId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			string sceneName = SceneNameFromHash(sceneEventData.SceneHash);
			if (!ValidateSceneBeforeLoading(sceneEventData.SceneHash, sceneEventData.LoadSceneMode))
			{
				EndSceneEvent(sceneEventId);
				return;
			}
			if (sceneEventData.LoadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				MoveObjectsToDontDestroyOnLoad();
				UnloadAdditivelyLoadedScenes(sceneEventData.SceneEventId);
			}
			if (sceneEventData.LoadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				IsSpawnedObjectsPendingInDontDestroyOnLoad = true;
				global::Unity.Netcode.NetworkSceneManager.SceneUnloadEventHandler.RegisterScene(this, global::UnityEngine.SceneManagement.SceneManager.GetActiveScene(), global::UnityEngine.SceneManagement.LoadSceneMode.Single);
			}
			global::Unity.Netcode.SceneEventProgress sceneEventProgress = new global::Unity.Netcode.SceneEventProgress(NetworkManager)
			{
				SceneEventId = sceneEventId,
				OnSceneEventCompleted = OnSceneLoaded,
				Status = global::Unity.Netcode.SceneEventProgressStatus.Started
			};
			if (NetworkManager.DistributedAuthorityMode)
			{
				SceneEventProgressTracking.Add(sceneEventData.SceneEventProgressId, sceneEventProgress);
				m_IsSceneEventActive = true;
			}
			global::UnityEngine.AsyncOperation asyncOperation = SceneManagerHandler.LoadSceneAsync(sceneName, sceneEventData.LoadSceneMode, sceneEventProgress);
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, asyncOperation);
		}

		private void OnSceneLoaded(uint sceneEventId)
		{
			if (!NetworkManager.IsListening || NetworkManager.ShutdownInProgress)
			{
				EndSceneEvent(sceneEventId);
				return;
			}
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			global::UnityEngine.SceneManagement.Scene andAddNewlyLoadedSceneByName = GetAndAddNewlyLoadedSceneByName(SceneNameFromHash(sceneEventData.SceneHash));
			if (!andAddNewlyLoadedSceneByName.isLoaded || !andAddNewlyLoadedSceneByName.IsValid())
			{
				throw new global::System.Exception("Failed to find valid scene internal Unity.Netcode for GameObjects error!");
			}
			if (sceneEventData.LoadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(andAddNewlyLoadedSceneByName);
			}
			if (NetworkManager.DistributedAuthorityMode)
			{
				global::Unity.Netcode.NetworkSceneHandle networkSceneHandle = andAddNewlyLoadedSceneByName.handle;
				if (!HasSceneAuthority())
				{
					networkSceneHandle = sceneEventData.SceneHandle;
				}
				if (!UpdateServerClientSceneHandle(networkSceneHandle, andAddNewlyLoadedSceneByName.handle, andAddNewlyLoadedSceneByName))
				{
					global::UnityEngine.Debug.LogWarning($"Server Scene Handle ({networkSceneHandle}) already exist!  Happened during scene load of {andAddNewlyLoadedSceneByName.name} with the local handle ({andAddNewlyLoadedSceneByName.handle})");
				}
			}
			else if (NetworkManager.IsServer && !UpdateServerClientSceneHandle(andAddNewlyLoadedSceneByName.handle, andAddNewlyLoadedSceneByName.handle, andAddNewlyLoadedSceneByName))
			{
				global::UnityEngine.Debug.LogWarning($"Server Scene Handle ({andAddNewlyLoadedSceneByName.handle}) already exist!  Happened during scene load of {andAddNewlyLoadedSceneByName.name} with the local handle ({andAddNewlyLoadedSceneByName.handle})");
			}
			PopulateScenePlacedObjects(andAddNewlyLoadedSceneByName);
			if (sceneEventData.LoadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				MoveObjectsFromDontDestroyOnLoadToScene(andAddNewlyLoadedSceneByName);
			}
			IsSpawnedObjectsPendingInDontDestroyOnLoad = false;
			if (HasSceneAuthority())
			{
				OnSessionOwnerLoadedScene(sceneEventId, andAddNewlyLoadedSceneByName);
				return;
			}
			if (!NetworkManager.DistributedAuthorityMode && !UpdateServerClientSceneHandle(sceneEventData.SceneHandle, andAddNewlyLoadedSceneByName.handle, andAddNewlyLoadedSceneByName))
			{
				throw new global::System.Exception($"Server Scene Handle ({sceneEventData.SceneHandle}) already exist!  Happened during scene load of {andAddNewlyLoadedSceneByName.name} with Client Handle ({andAddNewlyLoadedSceneByName.handle})");
			}
			OnClientLoadedScene(sceneEventId, andAddNewlyLoadedSceneByName);
		}

		private void OnSessionOwnerLoadedScene(uint sceneEventId, global::UnityEngine.SceneManagement.Scene scene)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>> scenePlacedObject in ScenePlacedObjects)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject> item in scenePlacedObject.Value)
				{
					if (!item.Value.IsPlayerObject)
					{
						NetworkManager.SpawnManager.AuthorityLocalSpawn(item.Value, NetworkManager.SpawnManager.GetNetworkObjectId(), sceneObject: true, playerObject: false, NetworkManager.LocalClientId, destroyWithScene: true);
					}
				}
			}
			foreach (global::System.Collections.Generic.KeyValuePair<uint, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>> scenePlacedObject2 in ScenePlacedObjects)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject> item2 in scenePlacedObject2.Value)
				{
					if (!item2.Value.IsPlayerObject)
					{
						item2.Value.InternalInSceneNetworkObjectsSpawned();
					}
				}
			}
			sceneEventData.AddDespawnedInSceneNetworkObjects();
			sceneEventData.SceneHandle = scene.handle;
			ulong sessionOwner = 0uL;
			if (NetworkManager.DistributedAuthorityMode)
			{
				sessionOwner = NetworkManager.CurrentSessionOwner;
			}
			SendSceneEventData(sceneEventData.SceneEventId, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(NetworkManager.ConnectedClientsIds, (ulong c) => c != sessionOwner)));
			m_IsSceneEventActive = false;
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.LoadComplete;
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, null, scene);
			if (SceneEventProgressTracking.ContainsKey(sceneEventData.SceneEventProgressId) && NetworkManager.IsClient)
			{
				SceneEventProgressTracking[sceneEventData.SceneEventProgressId].ClientFinishedSceneEvent(NetworkManager.LocalClientId);
			}
			EndSceneEvent(sceneEventId);
		}

		private void OnClientLoadedScene(uint sceneEventId, global::UnityEngine.SceneManagement.Scene scene)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			sceneEventData.DeserializeScenePlacedObjects();
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.LoadComplete;
			if (NetworkManager.DistributedAuthorityMode)
			{
				sceneEventData.TargetClientId = NetworkManager.CurrentSessionOwner;
				sceneEventData.SenderClientId = NetworkManager.LocalClientId;
				global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
				{
					EventData = sceneEventData
				};
				ulong num = (NetworkManager.DAHost ? NetworkManager.CurrentSessionOwner : 0);
				int num2 = NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, num);
				NetworkManager.NetworkMetrics.TrackSceneEventSent(num, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), num2);
			}
			else
			{
				SendSceneEventData(sceneEventId, new ulong[1]);
			}
			m_IsSceneEventActive = false;
			ProcessDeferredCreateObjectMessages();
			if (NetworkManager.DistributedAuthorityMode)
			{
				SceneEventProgressTracking.Remove(sceneEventData.SceneEventProgressId);
				m_IsSceneEventActive = false;
			}
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData, null, scene);
			EndSceneEvent(sceneEventId);
		}

		internal void SynchronizeNetworkObjects(ulong clientId, bool synchronizingService = false)
		{
			if (NetworkManager.CMBServiceConnection && !synchronizingService && !ClientConnectionQueue.Contains(clientId))
			{
				ClientConnectionQueue.Add(clientId);
				if (ClientConnectionQueue.Count > 1)
				{
					if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::UnityEngine.Debug.Log($"Deferring Client-{clientId} synchrnization.");
					}
					return;
				}
			}
			NetworkManager.SpawnManager.UpdateObservedNetworkObjects(clientId);
			global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
			sceneEventData.ClientSynchronizationMode = ClientSynchronizationMode;
			sceneEventData.InitializeForSynch();
			sceneEventData.TargetClientId = clientId;
			sceneEventData.LoadSceneMode = ClientSynchronizationMode;
			global::UnityEngine.SceneManagement.Scene activeScene = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.Synchronize;
			if (BuildIndexToHash.ContainsKey(activeScene.buildIndex))
			{
				sceneEventData.ActiveSceneHash = BuildIndexToHash[activeScene.buildIndex];
			}
			bool flag = false;
			for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
			{
				global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
				if ((ExcludeSceneFromSychronization != null && !ExcludeSceneFromSychronization(sceneAt)) || sceneAt == DontDestroyOnLoadScene)
				{
					continue;
				}
				if (activeScene == sceneAt)
				{
					if (!ValidateSceneBeforeLoading(sceneAt.buildIndex, sceneAt.name, sceneEventData.LoadSceneMode))
					{
						continue;
					}
					sceneEventData.SceneHash = SceneHashFromNameOrPath(sceneAt.path);
					if (sceneEventData.SceneHash == sceneEventData.ActiveSceneHash)
					{
						flag = true;
					}
					if (NetworkManager.DistributedAuthorityMode)
					{
						sceneEventData.SenderClientId = NetworkManager.LocalClientId;
						sceneEventData.SceneHandle = ClientSceneHandleToServerSceneHandle[sceneAt.handle];
					}
					else
					{
						sceneEventData.SceneHandle = sceneAt.handle;
					}
				}
				else if (!ValidateSceneBeforeLoading(sceneAt.buildIndex, sceneAt.name, global::UnityEngine.SceneManagement.LoadSceneMode.Additive))
				{
					continue;
				}
				if (NetworkManager.DistributedAuthorityMode && NetworkManager.CMBServiceConnection)
				{
					sceneEventData.AddSceneToSynchronize(SceneHashFromNameOrPath(sceneAt.path), ClientSceneHandleToServerSceneHandle[sceneAt.handle]);
				}
				else
				{
					sceneEventData.AddSceneToSynchronize(SceneHashFromNameOrPath(sceneAt.path), sceneAt.handle);
				}
			}
			if (!flag && NetworkManager.CMBServiceConnection && synchronizingService)
			{
				sceneEventData.AddSceneToSynchronize(BuildIndexToHash[activeScene.buildIndex], ClientSceneHandleToServerSceneHandle[activeScene.handle]);
			}
			sceneEventData.AddSpawnedNetworkObjects();
			sceneEventData.AddDespawnedInSceneNetworkObjects();
			global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
			{
				EventData = sceneEventData
			};
			int num = 0;
			num = ((!NetworkManager.DistributedAuthorityMode || NetworkManager.DAHost) ? NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, clientId) : NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, 0uL));
			NetworkManager.NetworkMetrics.TrackSceneEventSent(clientId, (uint)sceneEventData.SceneEventType, "", num);
			this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
			{
				SceneEventType = sceneEventData.SceneEventType,
				ClientId = clientId
			});
			this.OnSynchronize?.Invoke(clientId);
			EndSceneEvent(sceneEventData.SceneEventId);
		}

		private void OnClientBeginSync(uint sceneEventId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			uint nextSceneSynchronizationHash = sceneEventData.GetNextSceneSynchronizationHash();
			global::Unity.Netcode.NetworkSceneHandle nextSceneSynchronizationHandle = sceneEventData.GetNextSceneSynchronizationHandle();
			string text = SceneNameFromHash(nextSceneSynchronizationHash);
			global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode = ((nextSceneSynchronizationHash != sceneEventData.SceneHash) ? global::UnityEngine.SceneManagement.LoadSceneMode.Additive : sceneEventData.LoadSceneMode);
			sceneEventData.NetworkSceneHandle = nextSceneSynchronizationHandle;
			sceneEventData.ClientSceneHash = nextSceneSynchronizationHash;
			if (nextSceneSynchronizationHash == sceneEventData.SceneHash)
			{
				this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					SceneEventType = global::Unity.Netcode.SceneEventType.Synchronize,
					ClientId = NetworkManager.LocalClientId
				});
				this.OnSynchronize?.Invoke(NetworkManager.LocalClientId);
			}
			if (!ValidateSceneBeforeLoading(nextSceneSynchronizationHash, loadSceneMode))
			{
				HandleClientSceneEvent(sceneEventId);
				if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogInfo("Client declined to load the scene " + text + ", continuing with synchronization.");
				}
				return;
			}
			global::UnityEngine.AsyncOperation asyncOperation = null;
			if (!SceneManagerHandler.ClientShouldPassThrough(text, nextSceneSynchronizationHash == sceneEventData.SceneHash, ClientSynchronizationMode, NetworkManager))
			{
				global::Unity.Netcode.SceneEventProgress sceneEventProgress = new global::Unity.Netcode.SceneEventProgress(NetworkManager)
				{
					SceneEventId = sceneEventId,
					OnSceneEventCompleted = ClientLoadedSynchronization
				};
				asyncOperation = SceneManagerHandler.LoadSceneAsync(text, loadSceneMode, sceneEventProgress);
				this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					AsyncOperation = asyncOperation,
					SceneEventType = global::Unity.Netcode.SceneEventType.Load,
					LoadSceneMode = loadSceneMode,
					SceneName = text,
					ScenePath = ScenePathFromHash(nextSceneSynchronizationHash),
					ClientId = NetworkManager.LocalClientId
				});
				this.OnLoad?.Invoke(NetworkManager.LocalClientId, text, loadSceneMode, asyncOperation);
			}
			else
			{
				ClientLoadedSynchronization(sceneEventId);
			}
		}

		private void ClientLoadedSynchronization(uint sceneEventId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			string sceneName = SceneNameFromHash(sceneEventData.ClientSceneHash);
			global::UnityEngine.SceneManagement.Scene scene = SceneManagerHandler.GetSceneFromLoadedScenes(sceneName, NetworkManager);
			if (!scene.IsValid())
			{
				scene = GetAndAddNewlyLoadedSceneByName(sceneName);
			}
			if (!scene.isLoaded || !scene.IsValid())
			{
				throw new global::System.Exception("Failed to find valid scene internal Unity.Netcode for GameObjects error!");
			}
			global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode = ((sceneEventData.ClientSceneHash != sceneEventData.SceneHash) ? global::UnityEngine.SceneManagement.LoadSceneMode.Additive : sceneEventData.LoadSceneMode);
			if (loadSceneMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)
			{
				global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
			}
			if (!UpdateServerClientSceneHandle(sceneEventData.NetworkSceneHandle, scene.handle, scene))
			{
				throw new global::System.Exception($"Server Scene Handle ({sceneEventData.SceneHandle}) already exist!  Happened during scene load of {scene.name} with Client Handle ({scene.handle})");
			}
			PopulateScenePlacedObjects(scene, clearScenePlacedObjects: false);
			global::Unity.Netcode.SceneEventData sceneEventData2 = BeginSceneEvent();
			sceneEventData2.LoadSceneMode = loadSceneMode;
			sceneEventData2.SceneEventType = global::Unity.Netcode.SceneEventType.LoadComplete;
			sceneEventData2.SceneHash = sceneEventData.ClientSceneHash;
			ulong clientId = 0uL;
			if (NetworkManager.DistributedAuthorityMode)
			{
				sceneEventData2.SenderClientId = NetworkManager.LocalClientId;
				sceneEventData2.TargetClientId = NetworkManager.CurrentSessionOwner;
				clientId = (NetworkManager.DAHost ? NetworkManager.CurrentSessionOwner : 0);
			}
			global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
			{
				EventData = sceneEventData2
			};
			int num = NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, clientId);
			NetworkManager.NetworkMetrics.TrackSceneEventSent(0uL, (uint)sceneEventData2.SceneEventType, sceneName, num);
			EndSceneEvent(sceneEventData2.SceneEventId);
			InvokeSceneEvents(NetworkManager.LocalClientId, sceneEventData2, null, scene);
			HandleClientSceneEvent(sceneEventId);
		}

		private void SynchronizeNetworkObjectScene()
		{
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				if (spawnedObjects.IsSceneObject.Value || !ServerSceneHandleToClientSceneHandle.ContainsKey(spawnedObjects.NetworkSceneHandle))
				{
					continue;
				}
				spawnedObjects.SceneOriginHandle = ServerSceneHandleToClientSceneHandle[spawnedObjects.NetworkSceneHandle];
				if (!(spawnedObjects.gameObject.scene.handle != spawnedObjects.SceneOriginHandle) || !(spawnedObjects.transform.parent == null))
				{
					continue;
				}
				if (ScenesLoaded.ContainsKey(spawnedObjects.SceneOriginHandle))
				{
					global::UnityEngine.SceneManagement.Scene scene = ScenesLoaded[spawnedObjects.SceneOriginHandle];
					if (scene == DontDestroyOnLoadScene)
					{
						global::UnityEngine.Debug.Log(spawnedObjects.gameObject.name + " migrating into DDOL!");
					}
					global::UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(spawnedObjects.gameObject, scene);
				}
				else if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarningServer($"[Client-{NetworkManager.LocalClientId}][{spawnedObjects.gameObject.name}] Server - " + $"client scene mismatch detected! Client-side has no scene loaded with handle ({spawnedObjects.SceneOriginHandle})!");
				}
			}
		}

		private void HandleClientSceneEvent(uint sceneEventId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			switch (sceneEventData.SceneEventType)
			{
			case global::Unity.Netcode.SceneEventType.ActiveSceneChanged:
				if (HashToBuildIndex.ContainsKey(sceneEventData.ActiveSceneHash))
				{
					global::UnityEngine.SceneManagement.Scene sceneByBuildIndex = global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(HashToBuildIndex[sceneEventData.ActiveSceneHash]);
					if (sceneByBuildIndex.isLoaded)
					{
						global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(sceneByBuildIndex);
					}
				}
				EndSceneEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.ObjectSceneChanged:
				EndSceneEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.Load:
				OnClientSceneLoadingEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.Unload:
				OnClientUnloadScene(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.Synchronize:
				if (!sceneEventData.IsDoneWithSynchronization())
				{
					OnClientBeginSync(sceneEventId);
					break;
				}
				PopulateScenePlacedObjects(DontDestroyOnLoadScene, clearScenePlacedObjects: false);
				if (HashToBuildIndex.ContainsKey(sceneEventData.ActiveSceneHash))
				{
					global::UnityEngine.SceneManagement.Scene sceneByBuildIndex2 = global::UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(HashToBuildIndex[sceneEventData.ActiveSceneHash]);
					if (sceneByBuildIndex2.isLoaded && sceneByBuildIndex2.handle != global::UnityEngine.SceneManagement.SceneManager.GetActiveScene().handle)
					{
						global::UnityEngine.SceneManagement.SceneManager.SetActiveScene(sceneByBuildIndex2);
					}
				}
				sceneEventData.SynchronizeSceneNetworkObjects(NetworkManager);
				SynchronizeNetworkObjectScene();
				ProcessDeferredCreateObjectMessages();
				sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.SynchronizeComplete;
				if (NetworkManager.DistributedAuthorityMode)
				{
					sceneEventData.TargetClientId = NetworkManager.CurrentSessionOwner;
					sceneEventData.SenderClientId = NetworkManager.LocalClientId;
					global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
					{
						EventData = sceneEventData
					};
					ulong num = (NetworkManager.DAHost ? NetworkManager.CurrentSessionOwner : 0);
					int num2 = NetworkManager.ConnectionManager.SendMessage(ref message, m_NetworkDelivery, num);
					NetworkManager.NetworkMetrics.TrackSceneEventSent(num, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), num2);
				}
				else
				{
					SendSceneEventData(sceneEventId, new ulong[1]);
				}
				NetworkManager.IsConnectedClient = true;
				if (NetworkManager.DistributedAuthorityMode && NetworkManager.AutoSpawnPlayerPrefabClientSide)
				{
					NetworkManager.ConnectionManager.CreateAndSpawnPlayer(NetworkManager.LocalClientId);
				}
				sceneEventData.ProcessDeferredObjectSceneChangedEvents();
				if (PostSynchronizationSceneUnloading && ClientSynchronizationMode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive)
				{
					SceneManagerHandler.UnloadUnassignedScenes(NetworkManager);
				}
				NetworkManager.ConnectionManager.InvokeOnClientConnectedCallback(NetworkManager.LocalClientId);
				this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					SceneEventType = sceneEventData.SceneEventType,
					ClientId = NetworkManager.LocalClientId
				});
				this.OnSynchronizeComplete?.Invoke(NetworkManager.LocalClientId);
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogInfo($"[Client-{NetworkManager.LocalClientId}][Scene Management Enabled] Synchronization complete!");
				}
				NetworkManager.SpawnManager.NotifyNetworkObjectsSynchronized();
				if (NetworkManager.DistributedAuthorityMode && HasSceneAuthority() && IsRestoringSession)
				{
					IsRestoringSession = false;
					PostSynchronizationSceneUnloading = m_OriginalPostSynchronizationSceneUnloading;
				}
				EndSceneEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.ReSynchronize:
				this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					SceneEventType = sceneEventData.SceneEventType,
					ClientId = 0uL
				});
				EndSceneEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.LoadEventCompleted:
			case global::Unity.Netcode.SceneEventType.UnloadEventCompleted:
			{
				ulong clientId = (NetworkManager.CMBServiceConnection ? NetworkManager.CurrentSessionOwner : 0);
				InvokeSceneEvents(clientId, sceneEventData);
				EndSceneEvent(sceneEventId);
				break;
			}
			default:
				global::UnityEngine.Debug.LogWarning($"{sceneEventData.SceneEventType} is not currently supported!");
				break;
			}
		}

		private void HandleSessionOwnerEvent(uint sceneEventId, ulong clientId)
		{
			global::Unity.Netcode.SceneEventData sceneEventData = SceneEventDataStore[sceneEventId];
			switch (sceneEventData.SceneEventType)
			{
			case global::Unity.Netcode.SceneEventType.LoadComplete:
			case global::Unity.Netcode.SceneEventType.UnloadComplete:
				InvokeSceneEvents(clientId, sceneEventData);
				if (SceneEventProgressTracking.ContainsKey(sceneEventData.SceneEventProgressId))
				{
					SceneEventProgressTracking[sceneEventData.SceneEventProgressId].ClientFinishedSceneEvent(clientId);
				}
				EndSceneEvent(sceneEventId);
				break;
			case global::Unity.Netcode.SceneEventType.SynchronizeComplete:
				if (!NetworkManager.ConnectedClients.ContainsKey(clientId))
				{
					NetworkManager.ConnectionManager.AddClient(clientId);
				}
				NetworkManager.ConnectedClients[clientId].IsConnected = true;
				this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
				{
					SceneEventType = sceneEventData.SceneEventType,
					ClientId = clientId
				});
				this.OnSynchronizeComplete?.Invoke(clientId);
				if (NetworkManager.DistributedAuthorityMode && !NetworkManager.LocalClient.IsSessionOwner)
				{
					NetworkManager.SpawnManager.ShowHiddenObjectsToNewlyJoinedClient(clientId);
					NetworkManager.SpawnManager.DistributeNetworkObjects(clientId);
					EndSceneEvent(sceneEventId);
					break;
				}
				NetworkManager.ConnectionManager.InvokeOnClientConnectedCallback(clientId);
				if (NetworkManager.IsHost)
				{
					NetworkManager.ConnectionManager.InvokeOnPeerConnectedCallback(clientId);
				}
				if (sceneEventData.ClientNeedsReSynchronization() && !DisableReSynchronization && NetworkManager.ConnectedClients.ContainsKey(clientId))
				{
					sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.ReSynchronize;
					SendSceneEventData(sceneEventId, new ulong[1] { clientId });
					this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
					{
						SceneEventType = sceneEventData.SceneEventType,
						SceneName = string.Empty,
						ClientId = clientId
					});
				}
				NetworkManager.SpawnManager.DistributeNetworkObjects(clientId);
				EndSceneEvent(sceneEventId);
				if (!NetworkManager.DistributedAuthorityMode || NetworkManager.DAHost || !NetworkManager.DistributedAuthorityMode || !NetworkManager.CMBServiceConnection)
				{
					break;
				}
				ClientConnectionQueue.Remove(clientId);
				if (ClientConnectionQueue.Count > 0)
				{
					if (NetworkManager.LogLevel <= global::Unity.Netcode.LogLevel.Developer)
					{
						global::UnityEngine.Debug.Log($"Synchronizing deferred Client-{ClientConnectionQueue[0]}...");
					}
					SynchronizeNetworkObjects(ClientConnectionQueue[0]);
				}
				break;
			default:
				global::UnityEngine.Debug.LogWarning($"{sceneEventData.SceneEventType} is not currently supported!");
				break;
			}
		}

		internal void HandleSceneEvent(ulong clientId, global::Unity.Netcode.FastBufferReader reader)
		{
			if (NetworkManager != null)
			{
				global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
				sceneEventData.Deserialize(reader);
				if (SkipSceneHandling)
				{
					return;
				}
				if (NetworkManager.DistributedAuthorityMode && NetworkManager.DAHost)
				{
					if (!sceneEventData.IsSceneEventClientSide())
					{
						if (NetworkManager.CurrentSessionOwner != NetworkManager.LocalClientId)
						{
							global::Unity.Netcode.SceneEventMessage message = new global::Unity.Netcode.SceneEventMessage
							{
								EventData = sceneEventData
							};
							foreach (ulong connectedClientId in NetworkManager.ConnectionManager.ConnectedClientIds)
							{
								if (connectedClientId != NetworkManager.LocalClientId)
								{
									NetworkManager.MessageManager.SendMessage(ref message, m_NetworkDelivery, connectedClientId);
								}
							}
						}
					}
					else if (sceneEventData.TargetClientId != NetworkManager.LocalClientId)
					{
						if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
						{
							global::Unity.Netcode.NetworkLog.LogInfoServer($"[Forward To: Client-{sceneEventData.TargetClientId}][{(global::System.Enum.GetName(typeof(global::Unity.Netcode.SceneEventType), sceneEventData.SceneEventType))}]");
						}
						sceneEventData.ForwardSynchronization = sceneEventData.SceneEventType == global::Unity.Netcode.SceneEventType.Synchronize;
						sceneEventData.IsForwarding = true;
						global::Unity.Netcode.SceneEventMessage message2 = new global::Unity.Netcode.SceneEventMessage
						{
							EventData = sceneEventData
						};
						NetworkManager.MessageManager.SendMessage(ref message2, m_NetworkDelivery, sceneEventData.TargetClientId);
						EndSceneEvent(sceneEventData.SceneEventId);
						return;
					}
				}
				NetworkManager.NetworkMetrics.TrackSceneEventReceived(clientId, (uint)sceneEventData.SceneEventType, SceneNameFromHash(sceneEventData.SceneHash), reader.Length);
				if (sceneEventData.IsSceneEventClientSide())
				{
					if (sceneEventData.SceneEventType == global::Unity.Netcode.SceneEventType.Synchronize)
					{
						ScenePlacedObjects.Clear();
						ClientSynchronizationMode = sceneEventData.ClientSynchronizationMode;
						if (ClientSynchronizationMode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive)
						{
							if (NetworkManager.DistributedAuthorityMode && HasSceneAuthority() && IsRestoringSession && clientId == 0L)
							{
								m_OriginalPostSynchronizationSceneUnloading = PostSynchronizationSceneUnloading;
								PostSynchronizationSceneUnloading = true;
							}
							SceneManagerHandler.PopulateLoadedScenes(ref ScenesLoaded, NetworkManager);
						}
					}
					HandleClientSceneEvent(sceneEventData.SceneEventId);
				}
				else
				{
					ulong clientId2 = clientId;
					if (NetworkManager.DistributedAuthorityMode)
					{
						clientId2 = sceneEventData.SenderClientId;
					}
					HandleSessionOwnerEvent(sceneEventData.SceneEventId, clientId2);
				}
			}
			else
			{
				global::UnityEngine.Debug.LogError("HandleSceneEvent was invoked but NetworkManager reference was null!");
			}
		}

		internal void MoveObjectsToDontDestroyOnLoad()
		{
			global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> hashSet = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>(NetworkManager.SpawnManager.SpawnedObjectsList);
			bool distributedAuthorityMode = NetworkManager.DistributedAuthorityMode;
			foreach (global::Unity.Netcode.NetworkObject item in hashSet)
			{
				if (item == null || (item != null && item.gameObject.scene == DontDestroyOnLoadScene))
				{
					continue;
				}
				if (distributedAuthorityMode && item.DestroyWithScene && !item.HasAuthority)
				{
					item.DestroyPendingSceneEvent = true;
				}
				if (!item.DestroyWithScene)
				{
					if (item.gameObject.transform.parent == null && item.IsSceneObject.HasValue && !item.IsSceneObject.Value)
					{
						global::UnityEngine.Object.DontDestroyOnLoad(item.gameObject);
						item.NetworkSceneHandle = ClientSceneHandleToServerSceneHandle[item.gameObject.scene.handle];
						item.SceneOriginHandle = item.gameObject.scene.handle;
					}
				}
				else if (item.HasAuthority)
				{
					item.Despawn();
				}
			}
		}

		internal void PopulateScenePlacedObjects(global::UnityEngine.SceneManagement.Scene sceneToFilterBy, bool clearScenePlacedObjects = true)
		{
			if (clearScenePlacedObjects)
			{
				ScenePlacedObjects.Clear();
			}
			global::Unity.Netcode.NetworkObject[] array = global::Unity.Netcode.FindObjects.ByType<global::Unity.Netcode.NetworkObject>();
			foreach (global::Unity.Netcode.NetworkObject networkObject in array)
			{
				uint globalObjectIdHash = networkObject.GlobalObjectIdHash;
				global::UnityEngine.SceneManagement.SceneHandle handle = networkObject.gameObject.scene.handle;
				if (networkObject.IsSceneObject != false && (networkObject.NetworkManager == NetworkManager || networkObject.NetworkManagerOwner == null) && handle == sceneToFilterBy.handle)
				{
					if (!ScenePlacedObjects.ContainsKey(globalObjectIdHash))
					{
						ScenePlacedObjects.Add(globalObjectIdHash, new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkObject>());
					}
					if (ScenePlacedObjects[globalObjectIdHash].ContainsKey(handle))
					{
						string arg = ((ScenePlacedObjects[globalObjectIdHash][handle] != null) ? ScenePlacedObjects[globalObjectIdHash][handle].name : "Null Entry");
						throw new global::System.Exception(networkObject.name + " tried to registered with ScenePlacedObjects which already contains " + string.Format("the same {0} value {1} for {2}!", "GlobalObjectIdHash", globalObjectIdHash, arg));
					}
					ScenePlacedObjects[globalObjectIdHash].Add(handle, networkObject);
				}
			}
		}

		internal void MoveObjectsFromDontDestroyOnLoadToScene(global::UnityEngine.SceneManagement.Scene scene)
		{
			foreach (global::Unity.Netcode.NetworkObject spawnedObjects in NetworkManager.SpawnManager.SpawnedObjectsList)
			{
				if (spawnedObjects == null || !(spawnedObjects.gameObject.scene == DontDestroyOnLoadScene) || spawnedObjects.DestroyWithScene || !(spawnedObjects.gameObject.transform.parent == null) || !spawnedObjects.IsSceneObject.HasValue || spawnedObjects.IsSceneObject.Value)
				{
					continue;
				}
				if (NetworkManager.DistributedAuthorityMode)
				{
					global::UnityEngine.SceneManagement.SceneHandle handle = spawnedObjects.gameObject.scene.handle;
					if (SceneManagerHandler.IsIntegrationTest() && global::UnityEngine.SceneManagement.SceneManager.GetActiveScene() == scene)
					{
						spawnedObjects.NetworkSceneHandle = handle;
					}
					else
					{
						spawnedObjects.NetworkSceneHandle = ClientSceneHandleToServerSceneHandle[handle];
					}
					spawnedObjects.SceneOriginHandle = handle;
				}
				global::UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(spawnedObjects.gameObject, scene);
			}
		}

		internal bool IsSceneEventInProgress()
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				return false;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Guid, global::Unity.Netcode.SceneEventProgress> item in SceneEventProgressTracking)
			{
				if (!item.Value.HasTimedOut() && item.Value.SceneEventType != global::Unity.Netcode.SceneEventType.Synchronize && item.Value.Status == global::Unity.Netcode.SceneEventProgressStatus.Started)
				{
					return true;
				}
			}
			return false;
		}

		internal bool IsSceneUnloading(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (!NetworkManager.NetworkConfig.EnableSceneManagement)
			{
				return false;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Guid, global::Unity.Netcode.SceneEventProgress> item in SceneEventProgressTracking)
			{
				string text = SceneNameFromHash(item.Value.SceneHash);
				global::UnityEngine.SceneManagement.Scene sceneByName = global::UnityEngine.SceneManagement.SceneManager.GetSceneByName(text);
				bool flag = text == networkObject.gameObject.scene.name && sceneByName.handle == networkObject.SceneOriginHandle;
				if (!item.Value.HasTimedOut() && item.Value.IsUnloading() && item.Value.Status == global::Unity.Netcode.SceneEventProgressStatus.Started && flag)
				{
					return true;
				}
			}
			return false;
		}

		internal void NotifyNetworkObjectSceneChanged(global::Unity.Netcode.NetworkObject networkObject)
		{
			if (!networkObject.HasAuthority)
			{
				if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[Please Report This Error][NotifyNetworkObjectSceneChanged] A client is trying to notify of an object's scene change!");
				}
			}
			else if (networkObject.IsSceneObject != false)
			{
				if (NetworkManager.LogLevel == global::Unity.Netcode.LogLevel.Developer)
				{
					global::Unity.Netcode.NetworkLog.LogErrorServer("[Please Report This Error][NotifyNetworkObjectSceneChanged] Trying to notify in-scene placed object scene change!");
				}
			}
			else if ((!(networkObject.gameObject.scene == global::UnityEngine.SceneManagement.SceneManager.GetActiveScene()) || !networkObject.ActiveSceneSynchronization) && !IsSceneEventInProgress())
			{
				if (!ObjectsMigratedIntoNewScene.ContainsKey(networkObject.NetworkSceneHandle))
				{
					ObjectsMigratedIntoNewScene.Add(networkObject.NetworkSceneHandle, new global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>());
				}
				if (!ObjectsMigratedIntoNewScene[networkObject.NetworkSceneHandle].ContainsKey(NetworkManager.LocalClientId))
				{
					ObjectsMigratedIntoNewScene[networkObject.NetworkSceneHandle].Add(NetworkManager.LocalClientId, new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>());
				}
				ObjectsMigratedIntoNewScene[networkObject.NetworkSceneHandle][NetworkManager.LocalClientId].Add(networkObject);
			}
		}

		internal void MigrateNetworkObjectsIntoScenes()
		{
			try
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item in ObjectsMigratedIntoNewScene)
				{
					if (!ServerSceneHandleToClientSceneHandle.TryGetValue(item.Key, out var value))
					{
						continue;
					}
					foreach (global::System.Collections.Generic.KeyValuePair<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>> item2 in item.Value)
					{
						if (item2.Key == NetworkManager.LocalClientId || !ScenesLoaded.TryGetValue(value, out var value2))
						{
							continue;
						}
						foreach (global::Unity.Netcode.NetworkObject item3 in item2.Value)
						{
							global::UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(item3.gameObject, value2);
							item3.NetworkSceneHandle = item.Key;
							item3.SceneOriginHandle = value2.handle;
						}
					}
				}
			}
			catch (global::System.Exception ex)
			{
				global::Unity.Netcode.NetworkLog.LogErrorServer(ex.Message + "\n Stack Trace:\n " + ex.StackTrace);
			}
		}

		internal void CheckForAndSendNetworkObjectSceneChanged()
		{
			if (ObjectsMigratedIntoNewScene.Count == 0)
			{
				return;
			}
			MigrateNetworkObjectsIntoScenes();
			m_ScenesToRemoveFromObjectMigration.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item in ObjectsMigratedIntoNewScene)
			{
				if (!item.Value.ContainsKey(NetworkManager.LocalClientId))
				{
					continue;
				}
				_ = item.Value[NetworkManager.LocalClientId];
				for (int num = item.Value[NetworkManager.LocalClientId].Count - 1; num >= 0; num--)
				{
					if (!item.Value[NetworkManager.LocalClientId][num].IsSpawned)
					{
						item.Value[NetworkManager.LocalClientId].RemoveAt(num);
					}
				}
				if (item.Value.Count == 0)
				{
					m_ScenesToRemoveFromObjectMigration.Add(item.Key);
				}
			}
			foreach (global::Unity.Netcode.NetworkSceneHandle item2 in m_ScenesToRemoveFromObjectMigration)
			{
				ObjectsMigratedIntoNewScene[item2].Remove(NetworkManager.LocalClientId);
			}
			bool flag = false;
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::System.Collections.Generic.Dictionary<ulong, global::System.Collections.Generic.List<global::Unity.Netcode.NetworkObject>>> item3 in ObjectsMigratedIntoNewScene)
			{
				if (item3.Value.ContainsKey(NetworkManager.LocalClientId))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				ObjectsMigratedIntoNewScene.Clear();
				return;
			}
			global::Unity.Netcode.SceneEventData sceneEventData = BeginSceneEvent();
			sceneEventData.SceneEventType = global::Unity.Netcode.SceneEventType.ObjectSceneChanged;
			SendSceneEventData(sceneEventData.SceneEventId, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(NetworkManager.ConnectedClientsIds, (ulong c) => c != NetworkManager.LocalClientId)));
			ObjectsMigratedIntoNewScene.Clear();
			EndSceneEvent(sceneEventData.SceneEventId);
		}

		internal unsafe void DeferCreateObject(ulong senderId, uint messageSize, global::Unity.Netcode.NetworkObject.SerializedObject serializedObject, global::Unity.Netcode.FastBufferReader fastBufferReader, ulong[] observerIds, ulong[] newObserverIds)
		{
			global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation item = new global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation
			{
				SenderId = senderId,
				MessageSize = messageSize,
				ObserverIds = observerIds,
				NewObserverIds = newObserverIds,
				SerializedObject = serializedObject
			};
			item.FastBufferReader = new global::Unity.Netcode.FastBufferReader(fastBufferReader.GetUnsafePtrAtCurrentPosition(), global::Unity.Collections.Allocator.Persistent, fastBufferReader.Length - fastBufferReader.Position);
			DeferredObjectCreationList.Add(item);
		}

		private void ProcessDeferredCreateObjectMessages()
		{
			if (DeferredObjectCreationList.Count != 0)
			{
				global::Unity.Netcode.NetworkManager networkManager = NetworkManager;
				for (int i = 0; i < DeferredObjectCreationList.Count; i++)
				{
					global::Unity.Netcode.NetworkSceneManager.DeferredObjectCreation deferredObjectCreation = DeferredObjectCreationList[i];
					global::Unity.Netcode.CreateObjectMessage.CreateObject(ref networkManager, ref deferredObjectCreation);
				}
				DeferredObjectCreationCount = DeferredObjectCreationList.Count;
				DeferredObjectCreationList.Clear();
			}
		}

		public global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneMap> GetSceneMapping(global::Unity.Netcode.NetworkSceneManager.MapTypes mapType)
		{
			global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneMap> list = new global::System.Collections.Generic.List<global::Unity.Netcode.NetworkSceneManager.SceneMap>();
			foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.NetworkSceneHandle> item2 in (mapType == global::Unity.Netcode.NetworkSceneManager.MapTypes.ServerToClient) ? ServerSceneHandleToClientSceneHandle : ClientSceneHandleToServerSceneHandle)
			{
				global::UnityEngine.SceneManagement.Scene scene = ScenesLoaded[item2.Key];
				bool isScenePresent = scene.IsValid() && scene.isLoaded;
				global::Unity.Netcode.NetworkSceneManager.SceneMap item = new global::Unity.Netcode.NetworkSceneManager.SceneMap(mapType, scene, isScenePresent, item2.Key, item2.Value);
				list.Add(item);
			}
			return list;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private void InvokeSceneEvents(ulong clientId, global::Unity.Netcode.SceneEventData eventData, global::UnityEngine.AsyncOperation asyncOperation = null, global::UnityEngine.SceneManagement.Scene scene = default(global::UnityEngine.SceneManagement.Scene))
		{
			string sceneName = SceneNameFromHash(eventData.SceneHash);
			this.OnSceneEvent?.Invoke(new global::Unity.Netcode.SceneEvent
			{
				AsyncOperation = asyncOperation,
				SceneEventType = eventData.SceneEventType,
				SceneName = sceneName,
				ScenePath = ScenePathFromHash(eventData.SceneHash),
				ClientId = clientId,
				LoadSceneMode = eventData.LoadSceneMode,
				ClientsThatCompleted = eventData.ClientsCompleted,
				ClientsThatTimedOut = eventData.ClientsTimedOut,
				Scene = scene
			});
			switch (eventData.SceneEventType)
			{
			case global::Unity.Netcode.SceneEventType.Load:
				this.OnLoad?.Invoke(clientId, sceneName, eventData.LoadSceneMode, asyncOperation);
				break;
			case global::Unity.Netcode.SceneEventType.Unload:
				this.OnUnload?.Invoke(clientId, sceneName, asyncOperation);
				break;
			case global::Unity.Netcode.SceneEventType.LoadComplete:
				this.OnLoadComplete?.Invoke(clientId, sceneName, eventData.LoadSceneMode);
				break;
			case global::Unity.Netcode.SceneEventType.UnloadComplete:
				this.OnUnloadComplete?.Invoke(clientId, sceneName);
				break;
			case global::Unity.Netcode.SceneEventType.LoadEventCompleted:
				this.OnLoadEventCompleted?.Invoke(SceneNameFromHash(eventData.SceneHash), eventData.LoadSceneMode, eventData.ClientsCompleted, eventData.ClientsTimedOut);
				break;
			case global::Unity.Netcode.SceneEventType.UnloadEventCompleted:
				this.OnUnloadEventCompleted?.Invoke(SceneNameFromHash(eventData.SceneHash), eventData.LoadSceneMode, eventData.ClientsCompleted, eventData.ClientsTimedOut);
				break;
			case global::Unity.Netcode.SceneEventType.Synchronize:
			case global::Unity.Netcode.SceneEventType.ReSynchronize:
				break;
			}
		}
	}
}

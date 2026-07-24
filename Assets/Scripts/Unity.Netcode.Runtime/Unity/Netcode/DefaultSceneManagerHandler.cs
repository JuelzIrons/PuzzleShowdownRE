namespace Unity.Netcode
{
	internal class DefaultSceneManagerHandler : global::Unity.Netcode.ISceneManagerHandler
	{
		internal struct SceneEntry
		{
			public bool IsAssigned;

			public global::UnityEngine.SceneManagement.Scene Scene;
		}

		private global::UnityEngine.SceneManagement.Scene m_InvalidScene;

		internal global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry>> SceneNameToSceneHandles = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry>>();

		private global::System.Collections.Generic.List<global::UnityEngine.SceneManagement.Scene> m_ScenesToUnload = new global::System.Collections.Generic.List<global::UnityEngine.SceneManagement.Scene>();

		public bool IsIntegrationTest()
		{
			return false;
		}

		public global::UnityEngine.AsyncOperation LoadSceneAsync(string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::Unity.Netcode.SceneEventProgress sceneEventProgress)
		{
			global::UnityEngine.AsyncOperation asyncOperation = global::UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
			sceneEventProgress.SetAsyncOperation(asyncOperation);
			return asyncOperation;
		}

		public global::UnityEngine.AsyncOperation UnloadSceneAsync(global::UnityEngine.SceneManagement.Scene scene, global::Unity.Netcode.SceneEventProgress sceneEventProgress)
		{
			global::UnityEngine.AsyncOperation asyncOperation = global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
			sceneEventProgress.SetAsyncOperation(asyncOperation);
			return asyncOperation;
		}

		public void ClearSceneTracking(global::Unity.Netcode.NetworkManager networkManager)
		{
			SceneNameToSceneHandles.Clear();
		}

		public void StopTrackingScene(global::Unity.Netcode.NetworkSceneHandle handle, string name, global::Unity.Netcode.NetworkManager networkManager)
		{
			if (SceneNameToSceneHandles.ContainsKey(name) && SceneNameToSceneHandles[name].ContainsKey(handle))
			{
				SceneNameToSceneHandles[name].Remove(handle);
				if (SceneNameToSceneHandles[name].Count == 0)
				{
					SceneNameToSceneHandles.Remove(name);
				}
			}
		}

		public void StartTrackingScene(global::UnityEngine.SceneManagement.Scene scene, bool assigned, global::Unity.Netcode.NetworkManager networkManager)
		{
			if (!SceneNameToSceneHandles.ContainsKey(scene.name))
			{
				SceneNameToSceneHandles.Add(scene.name, new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry>());
			}
			if (!SceneNameToSceneHandles[scene.name].ContainsKey(scene.handle))
			{
				global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry value = new global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry
				{
					IsAssigned = true,
					Scene = scene
				};
				SceneNameToSceneHandles[scene.name].Add(scene.handle, value);
				return;
			}
			throw new global::System.Exception($"[Duplicate Handle] Scene {scene.name} already has scene handle {scene.handle} registered!");
		}

		public bool DoesSceneHaveUnassignedEntry(string sceneName, global::Unity.Netcode.NetworkManager networkManager)
		{
			global::System.Collections.Generic.List<global::UnityEngine.SceneManagement.Scene> list = new global::System.Collections.Generic.List<global::UnityEngine.SceneManagement.Scene>();
			for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
			{
				global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
				if (sceneAt.name == sceneName)
				{
					list.Add(sceneAt);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			if (list.Count > 0 && !SceneNameToSceneHandles.ContainsKey(sceneName))
			{
				return true;
			}
			foreach (global::UnityEngine.SceneManagement.Scene item in list)
			{
				if (!SceneNameToSceneHandles[item.name].ContainsKey(item.handle))
				{
					return true;
				}
				if (!SceneNameToSceneHandles[item.name][item.handle].IsAssigned)
				{
					return true;
				}
			}
			return false;
		}

		public global::UnityEngine.SceneManagement.Scene GetSceneFromLoadedScenes(string sceneName, global::Unity.Netcode.NetworkManager networkManager)
		{
			if (SceneNameToSceneHandles.ContainsKey(sceneName))
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry> item in SceneNameToSceneHandles[sceneName])
				{
					if (!item.Value.IsAssigned)
					{
						global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry value = item.Value;
						value.IsAssigned = true;
						SceneNameToSceneHandles[sceneName][item.Key] = value;
						return value.Scene;
					}
				}
			}
			return m_InvalidScene;
		}

		public void PopulateLoadedScenes(ref global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene> scenesLoaded, global::Unity.Netcode.NetworkManager networkManager)
		{
			SceneNameToSceneHandles.Clear();
			int sceneCount = global::UnityEngine.SceneManagement.SceneManager.sceneCount;
			for (int i = 0; i < sceneCount; i++)
			{
				global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
				if (!SceneNameToSceneHandles.ContainsKey(sceneAt.name))
				{
					SceneNameToSceneHandles.Add(sceneAt.name, new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry>());
				}
				if (!SceneNameToSceneHandles[sceneAt.name].ContainsKey(sceneAt.handle))
				{
					global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry value = new global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry
					{
						IsAssigned = false,
						Scene = sceneAt
					};
					SceneNameToSceneHandles[sceneAt.name].Add(sceneAt.handle, value);
					if (!scenesLoaded.ContainsKey(sceneAt.handle))
					{
						scenesLoaded.Add(sceneAt.handle, sceneAt);
					}
					continue;
				}
				throw new global::System.Exception($"[Duplicate Handle] Scene {sceneAt.name} already has scene handle {sceneAt.handle} registered!");
			}
		}

		public void UnloadUnassignedScenes(global::Unity.Netcode.NetworkManager networkManager = null)
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = networkManager.SceneManager;
			global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry>> sceneNameToSceneHandle in SceneNameToSceneHandles)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Netcode.NetworkSceneHandle, global::Unity.Netcode.DefaultSceneManagerHandler.SceneEntry> item in SceneNameToSceneHandles[sceneNameToSceneHandle.Key])
				{
					if (!item.Value.IsAssigned && (sceneManager.VerifySceneBeforeUnloading == null || sceneManager.VerifySceneBeforeUnloading(item.Value.Scene)))
					{
						m_ScenesToUnload.Add(item.Value.Scene);
					}
				}
			}
			foreach (global::UnityEngine.SceneManagement.Scene item2 in m_ScenesToUnload)
			{
				global::UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(item2);
				if (sceneManager.ScenesLoaded.ContainsKey(item2.handle))
				{
					sceneManager.ScenesLoaded.Remove(item2.handle);
				}
			}
		}

		private void SceneManager_SceneUnloaded(global::UnityEngine.SceneManagement.Scene scene)
		{
			if (SceneNameToSceneHandles.ContainsKey(scene.name))
			{
				if (SceneNameToSceneHandles[scene.name].ContainsKey(scene.handle))
				{
					SceneNameToSceneHandles[scene.name].Remove(scene.handle);
				}
				if (SceneNameToSceneHandles[scene.name].Count == 0)
				{
					SceneNameToSceneHandles.Remove(scene.name);
				}
				m_ScenesToUnload.Remove(scene);
				if (m_ScenesToUnload.Count == 0)
				{
					global::UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= SceneManager_SceneUnloaded;
				}
			}
		}

		public bool ClientShouldPassThrough(string sceneName, bool isPrimaryScene, global::UnityEngine.SceneManagement.LoadSceneMode clientSynchronizationMode, global::Unity.Netcode.NetworkManager networkManager)
		{
			bool flag = clientSynchronizationMode != global::UnityEngine.SceneManagement.LoadSceneMode.Single && DoesSceneHaveUnassignedEntry(sceneName, networkManager);
			global::UnityEngine.SceneManagement.Scene activeScene = global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			if (!flag && sceneName == activeScene.name && (clientSynchronizationMode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive || (isPrimaryScene && clientSynchronizationMode == global::UnityEngine.SceneManagement.LoadSceneMode.Single)))
			{
				flag = true;
			}
			return flag;
		}

		public void MoveObjectsFromSceneToDontDestroyOnLoad(ref global::Unity.Netcode.NetworkManager networkManager, global::UnityEngine.SceneManagement.Scene scene)
		{
			_ = scene == global::UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject> hashSet = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.NetworkObject>(networkManager.SpawnManager.SpawnedObjectsList);
			bool distributedAuthorityMode = networkManager.DistributedAuthorityMode;
			foreach (global::Unity.Netcode.NetworkObject item in hashSet)
			{
				if (item == null || (item != null && item.gameObject.scene.handle != scene.handle))
				{
					continue;
				}
				if (distributedAuthorityMode && item.DestroyWithScene && !item.HasAuthority)
				{
					item.DestroyPendingSceneEvent = true;
				}
				if (!item.DestroyWithScene && item.gameObject.scene != networkManager.SceneManager.DontDestroyOnLoadScene)
				{
					if (item.gameObject.transform.parent == null && item.IsSceneObject.HasValue && !item.IsSceneObject.Value)
					{
						global::UnityEngine.Object.DontDestroyOnLoad(item.gameObject);
					}
				}
				else if (item.HasAuthority)
				{
					item.Despawn();
				}
				else
				{
					global::UnityEngine.Object.DontDestroyOnLoad(item.gameObject);
				}
			}
		}

		public void SetClientSynchronizationMode(ref global::Unity.Netcode.NetworkManager networkManager, global::UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			global::Unity.Netcode.NetworkSceneManager sceneManager = networkManager.SceneManager;
			if (!networkManager.DistributedAuthorityMode && !networkManager.IsServer)
			{
				if (global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
				{
					global::Unity.Netcode.NetworkLog.LogWarning("Clients should not set this value as it is automatically synchronized with the server's setting!");
				}
				return;
			}
			if (!networkManager.DistributedAuthorityMode && networkManager.ConnectedClientsIds.Count > (networkManager.IsHost ? 1 : 0) && sceneManager.ClientSynchronizationMode != mode && global::Unity.Netcode.NetworkLog.CurrentLogLevel <= global::Unity.Netcode.LogLevel.Normal)
			{
				global::Unity.Netcode.NetworkLog.LogWarning("Server is changing client synchronization mode after clients have been synchronized! It is recommended to do this before clients are connected!");
			}
			if (mode == global::UnityEngine.SceneManagement.LoadSceneMode.Additive)
			{
				for (int i = 0; i < global::UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
				{
					global::UnityEngine.SceneManagement.Scene sceneAt = global::UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
					if ((sceneManager.VerifySceneBeforeLoading == null || sceneManager.VerifySceneBeforeLoading(sceneAt.buildIndex, sceneAt.name, global::UnityEngine.SceneManagement.LoadSceneMode.Additive)) && !sceneManager.ScenesLoaded.ContainsKey(sceneAt.handle))
					{
						sceneManager.ScenesLoaded.Add(sceneAt.handle, sceneAt);
					}
				}
			}
			sceneManager.ClientSynchronizationMode = mode;
		}
	}
}

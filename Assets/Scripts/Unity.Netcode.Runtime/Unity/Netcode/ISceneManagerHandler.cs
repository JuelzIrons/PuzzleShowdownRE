namespace Unity.Netcode
{
	internal interface ISceneManagerHandler
	{
		global::UnityEngine.AsyncOperation LoadSceneAsync(string sceneName, global::UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, global::Unity.Netcode.SceneEventProgress sceneEventProgress);

		global::UnityEngine.AsyncOperation UnloadSceneAsync(global::UnityEngine.SceneManagement.Scene scene, global::Unity.Netcode.SceneEventProgress sceneEventProgress);

		void PopulateLoadedScenes(ref global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkSceneHandle, global::UnityEngine.SceneManagement.Scene> scenesLoaded, global::Unity.Netcode.NetworkManager networkManager = null);

		global::UnityEngine.SceneManagement.Scene GetSceneFromLoadedScenes(string sceneName, global::Unity.Netcode.NetworkManager networkManager = null);

		bool DoesSceneHaveUnassignedEntry(string sceneName, global::Unity.Netcode.NetworkManager networkManager = null);

		void StopTrackingScene(global::Unity.Netcode.NetworkSceneHandle handle, string name, global::Unity.Netcode.NetworkManager networkManager = null);

		void StartTrackingScene(global::UnityEngine.SceneManagement.Scene scene, bool assigned, global::Unity.Netcode.NetworkManager networkManager = null);

		void ClearSceneTracking(global::Unity.Netcode.NetworkManager networkManager = null);

		void UnloadUnassignedScenes(global::Unity.Netcode.NetworkManager networkManager = null);

		void MoveObjectsFromSceneToDontDestroyOnLoad(ref global::Unity.Netcode.NetworkManager networkManager, global::UnityEngine.SceneManagement.Scene scene);

		void SetClientSynchronizationMode(ref global::Unity.Netcode.NetworkManager networkManager, global::UnityEngine.SceneManagement.LoadSceneMode mode);

		bool ClientShouldPassThrough(string sceneName, bool isPrimaryScene, global::UnityEngine.SceneManagement.LoadSceneMode clientSynchronizationMode, global::Unity.Netcode.NetworkManager networkManager);

		bool IsIntegrationTest();
	}
}

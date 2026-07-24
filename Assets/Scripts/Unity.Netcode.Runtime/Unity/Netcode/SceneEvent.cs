namespace Unity.Netcode
{
	public class SceneEvent
	{
		public global::UnityEngine.AsyncOperation AsyncOperation;

		public global::Unity.Netcode.SceneEventType SceneEventType;

		public global::UnityEngine.SceneManagement.LoadSceneMode LoadSceneMode;

		public string SceneName;

		public string ScenePath;

		public global::UnityEngine.SceneManagement.Scene Scene;

		public ulong ClientId;

		public global::System.Collections.Generic.List<ulong> ClientsThatCompleted;

		public global::System.Collections.Generic.List<ulong> ClientsThatTimedOut;
	}
}

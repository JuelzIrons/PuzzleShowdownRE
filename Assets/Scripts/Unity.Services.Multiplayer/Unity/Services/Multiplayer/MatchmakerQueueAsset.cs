namespace Unity.Services.Multiplayer
{
	public class MatchmakerQueueAsset : global::UnityEngine.ScriptableObject
	{
		[field: global::UnityEngine.SerializeField]
		public string Name { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int MaxPlayers { get; set; }

		[field: global::UnityEngine.SerializeField]
		public int TimeoutSeconds { get; set; }
	}
}

[global::UnityEngine.CreateAssetMenu(fileName = "VoiceBoxSO", menuName = "Audio/VoiceBox")]
public class VoiceBoxSO : global::UnityEngine.ScriptableObject
{
	[global::System.Serializable]
	public class VoiceLineGroup
	{
		public int multiplier;

		public global::System.Collections.Generic.List<global::UnityEngine.AudioClip> clips = new global::System.Collections.Generic.List<global::UnityEngine.AudioClip>();
	}

	public global::System.Collections.Generic.List<VoiceBoxSO.VoiceLineGroup> groups = new global::System.Collections.Generic.List<VoiceBoxSO.VoiceLineGroup>();

	private const int HIGH_CHAIN_THRESHOLD = 7;

	private const int HIGH_CHAIN_POOL_MIN = 5;

	private const int HIGH_CHAIN_POOL_MAX = 8;

	public global::System.Collections.Generic.List<global::UnityEngine.AudioClip> GetGroup(int multiplier)
	{
		if (multiplier > 7)
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.SelectMany(global::System.Linq.Enumerable.Where(groups, (VoiceBoxSO.VoiceLineGroup g) => g.multiplier >= 5 && g.multiplier <= 8), (VoiceBoxSO.VoiceLineGroup g) => g.clips));
		}
		return groups.Find((VoiceBoxSO.VoiceLineGroup g) => g.multiplier == multiplier)?.clips;
	}
}

[global::UnityEngine.CreateAssetMenu(fileName = "Master", menuName = "ScriptableObjects/LevelDataSO", order = 6)]
public class LevelDataSO : global::UnityEngine.ScriptableObject
{
	public global::System.Collections.Generic.List<AudioSegmentDataSO> LevelSpeechData = new global::System.Collections.Generic.List<AudioSegmentDataSO>();

	public global::System.Collections.Generic.List<int> LevelDepth = new global::System.Collections.Generic.List<int>();

	public global::System.Collections.Generic.List<int> LevelSpeed = new global::System.Collections.Generic.List<int>();

	public global::UnityEngine.Sprite LevelCG;

	public global::System.Collections.Generic.List<string> Choices = new global::System.Collections.Generic.List<string>();

	public global::System.Collections.Generic.List<LevelDataSO> NextLevels = new global::System.Collections.Generic.List<LevelDataSO>();

	public CharacterType PlayersChar;

	public CharacterType EnemyChar;

	public int AiDifficultyLevel;

	public global::UnityEngine.AudioClip AmbienceTrack;

	public MusicTrackType WAKTrack;

	[global::UnityEngine.Header("Reverb")]
	public global::UnityEngine.AudioReverbPreset ReverbPreset;

	[global::UnityEngine.Tooltip("Only used if ReverbPreset is set to User — assign a custom preset asset here.")]
	public CustomReverbPresetSO CustomReverbPreset;
}

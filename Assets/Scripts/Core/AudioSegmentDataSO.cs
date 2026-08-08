[global::UnityEngine.CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioSegmentData", order = 5)]
public class AudioSegmentDataSO : global::UnityEngine.ScriptableObject
{
	public global::UnityEngine.AudioClip Clip;

	[global::UnityEngine.TextArea(1, 15)]
	public string Timestamps;

	[global::UnityEngine.TextArea(1, 20)]
	public string WrittenDialogue;

	[global::UnityEngine.TextArea(1, 20)]
	public string DialogueCharacterNames;
}

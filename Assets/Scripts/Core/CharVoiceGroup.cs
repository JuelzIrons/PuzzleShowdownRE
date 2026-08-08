[global::System.Serializable]
public class CharVoiceGroup
{
	public int LastPlayedIndex = -1;

	public global::UnityEngine.AudioClip[] Clips;

	public global::UnityEngine.AudioClip GetUniqueClip()
	{
		int num = global::UnityEngine.Random.Range(0, Clips.Length);
		if (num == LastPlayedIndex)
		{
			num = (num + 1) % Clips.Length;
		}
		LastPlayedIndex = num;
		return Clips[num];
	}
}

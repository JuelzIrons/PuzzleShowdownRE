public class VoiceBox : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private CharVoiceBank m_bank;

	public global::UnityEngine.AudioClip GetVoiceLineForChain(int chainNum)
	{
		return m_bank.VoiceGroups[global::UnityEngine.Mathf.Clamp(chainNum - 2, 0, 6)].GetUniqueClip();
	}
}

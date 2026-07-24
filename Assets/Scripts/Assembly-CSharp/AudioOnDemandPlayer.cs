public class AudioOnDemandPlayer : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip[] m_clipToPlay;

	[global::UnityEngine.SerializeField]
	private AudioGroupType[] m_group;

	[global::UnityEngine.SerializeField]
	private float[] m_volume;

	public bool DISABLED;

	public bool FIRST_TIME_MITIGATION;

	public void PlayAudioOnDemand()
	{
		if (DISABLED)
		{
			return;
		}
		if (FIRST_TIME_MITIGATION)
		{
			FIRST_TIME_MITIGATION = false;
			return;
		}
		switch (m_group[0])
		{
		case AudioGroupType.Menu:
			AudioManager.Instance.PlayMenuSfx(m_clipToPlay[0], m_volume[0], antiOverlap: false);
			break;
		case AudioGroupType.Game:
			AudioManager.Instance.PlaySfx(m_clipToPlay[0], m_volume[0], antiOverlap: false);
			break;
		case AudioGroupType.Story:
			AudioManager.Instance.PlayStorySfx(m_clipToPlay[0], m_volume[0], antiOverlap: false);
			break;
		}
	}

	public void PlayAudioOnDemandIndex(int index)
	{
		if (DISABLED)
		{
			return;
		}
		if (FIRST_TIME_MITIGATION)
		{
			FIRST_TIME_MITIGATION = false;
			return;
		}
		switch (m_group[index])
		{
		case AudioGroupType.Menu:
			AudioManager.Instance.PlayMenuSfx(m_clipToPlay[index], m_volume[index], antiOverlap: false);
			break;
		case AudioGroupType.Game:
			AudioManager.Instance.PlaySfx(m_clipToPlay[index], m_volume[index], antiOverlap: false);
			break;
		case AudioGroupType.Story:
			AudioManager.Instance.PlayStorySfx(m_clipToPlay[index], m_volume[index], antiOverlap: false);
			break;
		}
	}
}

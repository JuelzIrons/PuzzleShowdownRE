public class MusicSceneSetter : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private MusicTrackType m_trackToPlay;

	[global::UnityEngine.SerializeField]
	private bool IsMenuFade;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_musicFadeInCurve;

	private void Start()
	{
		AudioManager.Instance.ChangeSong(m_trackToPlay);
		if (IsMenuFade)
		{
			float hardMenuVolume = AudioManager.Instance.GetHardMenuVolume();
			AudioManager.Instance.SetMenuForced(-80f);
			AudioManager.Instance.CancelMenuFade();
			if (m_musicFadeInCurve != null)
			{
				AudioManager.Instance.FadeInMenu(8f, 4f, hardMenuVolume, m_musicFadeInCurve);
			}
			else
			{
				AudioManager.Instance.FadeInMenu(8f, 4f, hardMenuVolume);
			}
		}
		AudioManager.Instance.SetMusicSpeed(1f);
	}
}

public class OptionsRandomVoiceLineGen : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.AudioClip> m_amesLines = new global::System.Collections.Generic.List<global::UnityEngine.AudioClip>();

	[global::UnityEngine.SerializeField]
	private float m_initialDelay = 0.5f;

	private void Start()
	{
		PlayRandomVoiceLine();
	}

	public void PlayRandomVoiceLine()
	{
		global::UnityEngine.AudioSource adS = base.transform.GetComponent<global::UnityEngine.AudioSource>();
		adS.clip = m_amesLines[global::UnityEngine.Random.Range(0, m_amesLines.Count)];
		base.transform.DOMove(base.transform.position, m_initialDelay).OnComplete(delegate
		{
			adS.Play();
		});
	}
}

public class MenuButtonParticleHandler : global::UnityEngine.MonoBehaviour
{
	public bool ShouldEmit;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.ParticleSystem m_ps;

	private void Update()
	{
		if (m_ps.isPlaying && !ShouldEmit)
		{
			m_ps.Stop();
		}
		else if (!m_ps.isPlaying && ShouldEmit)
		{
			m_ps.Play();
		}
	}
}

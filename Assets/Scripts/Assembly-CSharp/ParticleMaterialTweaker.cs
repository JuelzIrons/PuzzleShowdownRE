public class ParticleMaterialTweaker : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.ParticleSystemRenderer m_psR;

	[global::UnityEngine.SerializeField]
	private float m_startFloat;

	[global::UnityEngine.SerializeField]
	private float m_cycleSpeed;

	private float m_time;

	private void Start()
	{
		m_psR.material.SetFloat("_RainbowFloat", m_startFloat);
		m_time = m_startFloat;
	}

	private void Update()
	{
		m_time += global::UnityEngine.Time.deltaTime;
		m_psR.material.SetFloat("_RainbowFloat", global::UnityEngine.Mathf.PingPong(m_time * m_cycleSpeed, 1f));
	}
}

public class KillAfterTime : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private float m_secondsAlive;

	public void KillIn(float seconds)
	{
		Invoke("Kill", seconds);
	}

	private void Kill()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Start()
	{
		if (m_secondsAlive > 0f)
		{
			Invoke("Kill", m_secondsAlive);
		}
	}
}

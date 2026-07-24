public class EmberAmountAdjuster : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.ParticleSystem m_ps;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_emissionCurve;

	[global::UnityEngine.SerializeField]
	private int OVERRIDE_SOLVE_SIZE;

	private void Start()
	{
		if (OVERRIDE_SOLVE_SIZE != 0)
		{
			global::UnityEngine.ParticleSystem.EmissionModule emission = m_ps.emission;
			float num = m_emissionCurve.Evaluate(OVERRIDE_SOLVE_SIZE);
			float num2 = m_emissionCurve.Evaluate(OVERRIDE_SOLVE_SIZE);
			global::UnityEngine.ParticleSystem.MinMaxCurve count = new global::UnityEngine.ParticleSystem.MinMaxCurve(num, num);
			global::UnityEngine.ParticleSystem.MinMaxCurve count2 = new global::UnityEngine.ParticleSystem.MinMaxCurve(num2 / 2f, num2 / 2f);
			emission.SetBursts(new global::UnityEngine.ParticleSystem.Burst[2]
			{
				new global::UnityEngine.ParticleSystem.Burst(0f, count, 15, 0.03f),
				new global::UnityEngine.ParticleSystem.Burst(0f, count2, 5, 0.03f)
			});
		}
	}

	public void SetParams(int solveSize)
	{
		global::UnityEngine.ParticleSystem.EmissionModule emission = m_ps.emission;
		float num = m_emissionCurve.Evaluate(solveSize);
		float num2 = m_emissionCurve.Evaluate(solveSize);
		global::UnityEngine.ParticleSystem.MinMaxCurve count = new global::UnityEngine.ParticleSystem.MinMaxCurve(num, num);
		global::UnityEngine.ParticleSystem.MinMaxCurve count2 = new global::UnityEngine.ParticleSystem.MinMaxCurve(num2 / 2f, num2 / 2f);
		emission.SetBursts(new global::UnityEngine.ParticleSystem.Burst[2]
		{
			new global::UnityEngine.ParticleSystem.Burst(0f, count, 15, 0.03f),
			new global::UnityEngine.ParticleSystem.Burst(0f, count2, 5, 0.03f)
		});
	}
}

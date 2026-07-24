namespace UnityEngine.Timeline
{
	public class ParticleControlPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		private const float kUnsetTime = float.MaxValue;

		private float m_LastPlayableTime = float.MaxValue;

		private float m_LastParticleTime = float.MaxValue;

		private uint m_RandomSeed = 1u;

		public global::UnityEngine.ParticleSystem particleSystem { get; private set; }

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ParticleControlPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.ParticleSystem component, uint randomSeed)
		{
			if (component == null)
			{
				return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ParticleControlPlayable>.Null;
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ParticleControlPlayable> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ParticleControlPlayable>.Create(graph);
			result.GetBehaviour().Initialize(component, randomSeed);
			return result;
		}

		public void Initialize(global::UnityEngine.ParticleSystem ps, uint randomSeed)
		{
			m_RandomSeed = global::System.Math.Max(1u, randomSeed);
			particleSystem = ps;
			SetRandomSeed(particleSystem, m_RandomSeed);
		}

		private static void SetRandomSeed(global::UnityEngine.ParticleSystem particleSystem, uint randomSeed)
		{
			if (!(particleSystem == null))
			{
				particleSystem.Stop(withChildren: true, global::UnityEngine.ParticleSystemStopBehavior.StopEmittingAndClear);
				if (particleSystem.useAutoRandomSeed)
				{
					particleSystem.useAutoRandomSeed = false;
					particleSystem.randomSeed = randomSeed;
				}
				for (int i = 0; i < particleSystem.subEmitters.subEmittersCount; i++)
				{
					SetRandomSeed(particleSystem.subEmitters.GetSubEmitterSystem(i), ++randomSeed);
				}
			}
		}

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData data)
		{
			if (particleSystem == null || !particleSystem.gameObject.activeInHierarchy)
			{
				m_LastPlayableTime = float.MaxValue;
				return;
			}
			float num = (float)global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
			float time = particleSystem.time;
			if (m_LastPlayableTime > num || !global::UnityEngine.Mathf.Approximately(time, m_LastParticleTime))
			{
				Simulate(num, restart: true);
			}
			else if (m_LastPlayableTime < num)
			{
				Simulate(num - m_LastPlayableTime, restart: false);
			}
			m_LastPlayableTime = num;
			m_LastParticleTime = particleSystem.time;
		}

		public override void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			m_LastPlayableTime = float.MaxValue;
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			m_LastPlayableTime = float.MaxValue;
		}

		private void Simulate(float time, bool restart)
		{
			float maximumDeltaTime = global::UnityEngine.Time.maximumDeltaTime;
			if (restart)
			{
				particleSystem.Simulate(0f, withChildren: false, restart: true, fixedTimeStep: false);
			}
			while (time > maximumDeltaTime)
			{
				particleSystem.Simulate(maximumDeltaTime, withChildren: false, restart: false, fixedTimeStep: false);
				time -= maximumDeltaTime;
			}
			if (time > 0f)
			{
				particleSystem.Simulate(time, withChildren: false, restart: false, fixedTimeStep: false);
			}
		}
	}
}

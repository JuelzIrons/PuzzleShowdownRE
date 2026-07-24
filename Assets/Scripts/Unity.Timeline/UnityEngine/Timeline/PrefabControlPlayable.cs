namespace UnityEngine.Timeline
{
	public class PrefabControlPlayable : global::UnityEngine.Playables.PlayableBehaviour
	{
		private global::UnityEngine.GameObject m_Instance;

		public global::UnityEngine.GameObject prefabInstance => m_Instance;

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.PrefabControlPlayable> Create(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject prefabGameObject, global::UnityEngine.Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				return global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.PrefabControlPlayable>.Null;
			}
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.PrefabControlPlayable> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.PrefabControlPlayable>.Create(graph);
			result.GetBehaviour().Initialize(prefabGameObject, parentTransform);
			return result;
		}

		public global::UnityEngine.GameObject Initialize(global::UnityEngine.GameObject prefabGameObject, global::UnityEngine.Transform parentTransform)
		{
			if (prefabGameObject == null)
			{
				throw new global::System.ArgumentNullException("Prefab cannot be null");
			}
			if (m_Instance != null)
			{
				global::UnityEngine.Debug.LogWarningFormat("Prefab Control Playable ({0}) has already been initialized with a Prefab ({1}).", prefabGameObject.name, m_Instance.name);
			}
			else
			{
				m_Instance = global::UnityEngine.Object.Instantiate(prefabGameObject, parentTransform, worldPositionStays: false);
				m_Instance.name = prefabGameObject.name + " [Timeline]";
				m_Instance.SetActive(value: false);
				SetHideFlagsRecursive(m_Instance);
			}
			return m_Instance;
		}

		public override void OnPlayableDestroy(global::UnityEngine.Playables.Playable playable)
		{
			if ((bool)m_Instance)
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					global::UnityEngine.Object.Destroy(m_Instance);
				}
				else
				{
					global::UnityEngine.Object.DestroyImmediate(m_Instance);
				}
			}
		}

		public override void OnBehaviourPlay(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (!(m_Instance == null))
			{
				m_Instance.SetActive(value: true);
			}
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (m_Instance != null && info.effectivePlayState == global::UnityEngine.Playables.PlayState.Paused)
			{
				m_Instance.SetActive(value: false);
			}
		}

		private static void SetHideFlagsRecursive(global::UnityEngine.GameObject gameObject)
		{
			if (gameObject == null)
			{
				return;
			}
			gameObject.hideFlags = global::UnityEngine.HideFlags.DontSaveInEditor | global::UnityEngine.HideFlags.DontSaveInBuild;
			if (!global::UnityEngine.Application.isPlaying)
			{
				gameObject.hideFlags |= global::UnityEngine.HideFlags.HideInHierarchy;
			}
			foreach (global::UnityEngine.Transform item in gameObject.transform)
			{
				SetHideFlagsRecursive(item.gameObject);
			}
		}
	}
}

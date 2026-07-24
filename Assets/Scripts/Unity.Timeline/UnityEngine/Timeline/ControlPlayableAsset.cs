namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.NotKeyable]
	public class ControlPlayableAsset : global::UnityEngine.Playables.PlayableAsset, global::UnityEngine.Timeline.IPropertyPreview, global::UnityEngine.Timeline.ITimelineClipAsset
	{
		private const int k_MaxRandInt = 10000;

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.Playables.PlayableDirector> k_EmptyDirectorsList = new global::System.Collections.Generic.List<global::UnityEngine.Playables.PlayableDirector>(0);

		private static readonly global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> k_EmptyParticlesList = new global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem>(0);

		private static readonly global::System.Collections.Generic.HashSet<global::UnityEngine.ParticleSystem> s_SubEmitterCollector = new global::System.Collections.Generic.HashSet<global::UnityEngine.ParticleSystem>();

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.ExposedReference<global::UnityEngine.GameObject> sourceGameObject;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.GameObject prefabGameObject;

		[global::UnityEngine.SerializeField]
		public bool updateParticle = true;

		[global::UnityEngine.SerializeField]
		public uint particleRandomSeed;

		[global::UnityEngine.SerializeField]
		public bool updateDirector = true;

		[global::UnityEngine.SerializeField]
		public bool updateITimeControl = true;

		[global::UnityEngine.SerializeField]
		public bool searchHierarchy;

		[global::UnityEngine.SerializeField]
		public bool active = true;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState postPlayback = global::UnityEngine.Timeline.ActivationControlPlayable.PostPlaybackState.Revert;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.Timeline.DirectorControlPlayable.PauseAction directorOnClipEnd;

		private global::UnityEngine.Playables.PlayableAsset m_ControlDirectorAsset;

		private double m_Duration = global::UnityEngine.Playables.PlayableBinding.DefaultDuration;

		private bool m_SupportLoop;

		private static global::System.Collections.Generic.HashSet<global::UnityEngine.Playables.PlayableDirector> s_ProcessedDirectors = new global::System.Collections.Generic.HashSet<global::UnityEngine.Playables.PlayableDirector>();

		private static global::System.Collections.Generic.HashSet<global::UnityEngine.GameObject> s_CreatedPrefabs = new global::System.Collections.Generic.HashSet<global::UnityEngine.GameObject>();

		internal bool controllingDirectors { get; private set; }

		internal bool controllingParticles { get; private set; }

		public override double duration => m_Duration;

		public global::UnityEngine.Timeline.ClipCaps clipCaps => (global::UnityEngine.Timeline.ClipCaps)(0xC | (m_SupportLoop ? 1 : 0));

		public void OnEnable()
		{
			if (particleRandomSeed == 0)
			{
				particleRandomSeed = (uint)global::UnityEngine.Random.Range(1, 10000);
			}
		}

		public override global::UnityEngine.Playables.Playable CreatePlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.GameObject go)
		{
			if (prefabGameObject != null)
			{
				if (s_CreatedPrefabs.Contains(prefabGameObject))
				{
					global::UnityEngine.Debug.LogWarningFormat("Control Track Clip ({0}) is causing a prefab to instantiate itself recursively. Aborting further instances.", base.name);
					return global::UnityEngine.Playables.Playable.Create(graph);
				}
				s_CreatedPrefabs.Add(prefabGameObject);
			}
			global::UnityEngine.Playables.Playable playable = global::UnityEngine.Playables.Playable.Null;
			global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> list = new global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable>();
			global::UnityEngine.GameObject gameObject = sourceGameObject.Resolve(graph.GetResolver());
			if (prefabGameObject != null)
			{
				global::UnityEngine.Transform parentTransform = ((gameObject != null) ? gameObject.transform : null);
				global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.PrefabControlPlayable> scriptPlayable = global::UnityEngine.Timeline.PrefabControlPlayable.Create(graph, prefabGameObject, parentTransform);
				gameObject = scriptPlayable.GetBehaviour().prefabInstance;
				list.Add(scriptPlayable);
			}
			m_Duration = global::UnityEngine.Playables.PlayableBinding.DefaultDuration;
			m_SupportLoop = false;
			controllingParticles = false;
			controllingDirectors = false;
			if (gameObject != null)
			{
				global::System.Collections.Generic.IList<global::UnityEngine.Playables.PlayableDirector> list3;
				if (!updateDirector)
				{
					global::System.Collections.Generic.IList<global::UnityEngine.Playables.PlayableDirector> list2 = k_EmptyDirectorsList;
					list3 = list2;
				}
				else
				{
					list3 = GetComponent<global::UnityEngine.Playables.PlayableDirector>(gameObject);
				}
				global::System.Collections.Generic.IList<global::UnityEngine.Playables.PlayableDirector> directors = list3;
				global::System.Collections.Generic.IList<global::UnityEngine.ParticleSystem> list5;
				if (!updateParticle)
				{
					global::System.Collections.Generic.IList<global::UnityEngine.ParticleSystem> list4 = k_EmptyParticlesList;
					list5 = list4;
				}
				else
				{
					list5 = GetControllableParticleSystems(gameObject);
				}
				global::System.Collections.Generic.IList<global::UnityEngine.ParticleSystem> particleSystems = list5;
				UpdateDurationAndLoopFlag(directors, particleSystems);
				global::UnityEngine.Playables.PlayableDirector component = go.GetComponent<global::UnityEngine.Playables.PlayableDirector>();
				if (component != null)
				{
					m_ControlDirectorAsset = component.playableAsset;
				}
				if (go == gameObject && prefabGameObject == null)
				{
					global::UnityEngine.Debug.LogWarningFormat("Control Playable ({0}) is referencing the same PlayableDirector component than the one in which it is playing.", base.name);
					active = false;
					if (!searchHierarchy)
					{
						updateDirector = false;
					}
				}
				if (active)
				{
					CreateActivationPlayable(gameObject, graph, list);
				}
				if (updateDirector)
				{
					SearchHierarchyAndConnectDirector(directors, graph, list, prefabGameObject != null);
				}
				if (updateParticle)
				{
					SearchHierarchyAndConnectParticleSystem(particleSystems, graph, list);
				}
				if (updateITimeControl)
				{
					SearchHierarchyAndConnectControlableScripts(GetControlableScripts(gameObject), graph, list);
				}
				playable = ConnectPlayablesToMixer(graph, list);
			}
			if (prefabGameObject != null)
			{
				s_CreatedPrefabs.Remove(prefabGameObject);
			}
			if (!global::UnityEngine.Playables.PlayableExtensions.IsValid(playable))
			{
				playable = global::UnityEngine.Playables.Playable.Create(graph);
			}
			return playable;
		}

		private static global::UnityEngine.Playables.Playable ConnectPlayablesToMixer(global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> playables)
		{
			global::UnityEngine.Playables.Playable playable = global::UnityEngine.Playables.Playable.Create(graph, playables.Count);
			for (int i = 0; i != playables.Count; i++)
			{
				ConnectMixerAndPlayable(graph, playable, playables[i], i);
			}
			global::UnityEngine.Playables.PlayableExtensions.SetPropagateSetTime(playable, value: true);
			return playable;
		}

		private void CreateActivationPlayable(global::UnityEngine.GameObject root, global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> outplayables)
		{
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.ActivationControlPlayable> scriptPlayable = global::UnityEngine.Timeline.ActivationControlPlayable.Create(graph, root, postPlayback);
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(scriptPlayable))
			{
				outplayables.Add(scriptPlayable);
			}
		}

		private void SearchHierarchyAndConnectParticleSystem(global::System.Collections.Generic.IEnumerable<global::UnityEngine.ParticleSystem> particleSystems, global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> outplayables)
		{
			foreach (global::UnityEngine.ParticleSystem particleSystem in particleSystems)
			{
				if (particleSystem != null)
				{
					controllingParticles = true;
					outplayables.Add(global::UnityEngine.Timeline.ParticleControlPlayable.Create(graph, particleSystem, particleRandomSeed));
				}
			}
		}

		private void SearchHierarchyAndConnectDirector(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableDirector> directors, global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> outplayables, bool disableSelfReferences)
		{
			foreach (global::UnityEngine.Playables.PlayableDirector director in directors)
			{
				if (director != null)
				{
					if (director.playableAsset != m_ControlDirectorAsset)
					{
						global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.DirectorControlPlayable> scriptPlayable = global::UnityEngine.Timeline.DirectorControlPlayable.Create(graph, director);
						scriptPlayable.GetBehaviour().pauseAction = directorOnClipEnd;
						outplayables.Add(scriptPlayable);
						controllingDirectors = true;
					}
					else if (disableSelfReferences)
					{
						director.enabled = false;
					}
				}
			}
		}

		private static void SearchHierarchyAndConnectControlableScripts(global::System.Collections.Generic.IEnumerable<global::UnityEngine.MonoBehaviour> controlableScripts, global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.List<global::UnityEngine.Playables.Playable> outplayables)
		{
			foreach (global::UnityEngine.MonoBehaviour controlableScript in controlableScripts)
			{
				outplayables.Add(global::UnityEngine.Timeline.TimeControlPlayable.Create(graph, (global::UnityEngine.Timeline.ITimeControl)controlableScript));
			}
		}

		private static void ConnectMixerAndPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::UnityEngine.Playables.Playable mixer, global::UnityEngine.Playables.Playable playable, int portIndex)
		{
			graph.Connect(playable, 0, mixer, portIndex);
			global::UnityEngine.Playables.PlayableExtensions.SetInputWeight(mixer, playable, 1f);
		}

		internal global::System.Collections.Generic.IList<T> GetComponent<T>(global::UnityEngine.GameObject gameObject)
		{
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			if (gameObject != null)
			{
				if (searchHierarchy)
				{
					gameObject.GetComponentsInChildren(includeInactive: true, list);
				}
				else
				{
					gameObject.GetComponents(list);
				}
			}
			return list;
		}

		internal static global::System.Collections.Generic.IEnumerable<global::UnityEngine.MonoBehaviour> GetControlableScripts(global::UnityEngine.GameObject root)
		{
			if (root == null)
			{
				yield break;
			}
			global::UnityEngine.MonoBehaviour[] componentsInChildren = root.GetComponentsInChildren<global::UnityEngine.MonoBehaviour>();
			foreach (global::UnityEngine.MonoBehaviour monoBehaviour in componentsInChildren)
			{
				if (monoBehaviour is global::UnityEngine.Timeline.ITimeControl)
				{
					yield return monoBehaviour;
				}
			}
		}

		internal void UpdateDurationAndLoopFlag(global::System.Collections.Generic.IList<global::UnityEngine.Playables.PlayableDirector> directors, global::System.Collections.Generic.IList<global::UnityEngine.ParticleSystem> particleSystems)
		{
			if (directors.Count == 0 && particleSystems.Count == 0)
			{
				return;
			}
			double num = double.NegativeInfinity;
			bool flag = false;
			foreach (global::UnityEngine.Playables.PlayableDirector director in directors)
			{
				if (director.playableAsset != null)
				{
					double num2 = director.playableAsset.duration;
					if (director.playableAsset is global::UnityEngine.Timeline.TimelineAsset && num2 > 0.0)
					{
						num2 = (double)((global::UnityEngine.Timeline.DiscreteTime)num2).OneTickAfter();
					}
					num = global::System.Math.Max(num, num2);
					flag = flag || director.extrapolationMode == global::UnityEngine.Playables.DirectorWrapMode.Loop;
				}
			}
			foreach (global::UnityEngine.ParticleSystem particleSystem in particleSystems)
			{
				num = global::System.Math.Max(num, particleSystem.main.duration);
				flag = flag || particleSystem.main.loop;
			}
			m_Duration = (double.IsNegativeInfinity(num) ? global::UnityEngine.Playables.PlayableBinding.DefaultDuration : num);
			m_SupportLoop = flag;
		}

		private global::System.Collections.Generic.IList<global::UnityEngine.ParticleSystem> GetControllableParticleSystems(global::UnityEngine.GameObject go)
		{
			global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem> list = new global::System.Collections.Generic.List<global::UnityEngine.ParticleSystem>();
			if (searchHierarchy || go.GetComponent<global::UnityEngine.ParticleSystem>() != null)
			{
				GetControllableParticleSystems(go.transform, list, s_SubEmitterCollector);
				s_SubEmitterCollector.Clear();
			}
			return list;
		}

		private static void GetControllableParticleSystems(global::UnityEngine.Transform t, global::System.Collections.Generic.ICollection<global::UnityEngine.ParticleSystem> roots, global::System.Collections.Generic.HashSet<global::UnityEngine.ParticleSystem> subEmitters)
		{
			global::UnityEngine.ParticleSystem component = t.GetComponent<global::UnityEngine.ParticleSystem>();
			if (component != null && !subEmitters.Contains(component))
			{
				roots.Add(component);
				CacheSubEmitters(component, subEmitters);
			}
			for (int i = 0; i < t.childCount; i++)
			{
				GetControllableParticleSystems(t.GetChild(i), roots, subEmitters);
			}
		}

		private static void CacheSubEmitters(global::UnityEngine.ParticleSystem ps, global::System.Collections.Generic.HashSet<global::UnityEngine.ParticleSystem> subEmitters)
		{
			if (!(ps == null))
			{
				for (int i = 0; i < ps.subEmitters.subEmittersCount; i++)
				{
					subEmitters.Add(ps.subEmitters.GetSubEmitterSystem(i));
				}
			}
		}

		public void GatherProperties(global::UnityEngine.Playables.PlayableDirector director, global::UnityEngine.Timeline.IPropertyCollector driver)
		{
			if (director == null || s_ProcessedDirectors.Contains(director))
			{
				return;
			}
			s_ProcessedDirectors.Add(director);
			global::UnityEngine.GameObject gameObject = sourceGameObject.Resolve(director);
			if (gameObject != null)
			{
				if (updateParticle)
				{
					PreviewParticles(driver, gameObject.GetComponentsInChildren<global::UnityEngine.ParticleSystem>(includeInactive: true));
				}
				if (active)
				{
					PreviewActivation(driver, new global::UnityEngine.GameObject[1] { gameObject });
				}
				if (updateITimeControl)
				{
					PreviewTimeControl(driver, director, GetControlableScripts(gameObject));
				}
				if (updateDirector)
				{
					PreviewDirectors(driver, GetComponent<global::UnityEngine.Playables.PlayableDirector>(gameObject));
				}
			}
			s_ProcessedDirectors.Remove(director);
		}

		internal static void PreviewParticles(global::UnityEngine.Timeline.IPropertyCollector driver, global::System.Collections.Generic.IEnumerable<global::UnityEngine.ParticleSystem> particles)
		{
			foreach (global::UnityEngine.ParticleSystem particle in particles)
			{
				driver.AddFromName<global::UnityEngine.ParticleSystem>(particle.gameObject, "randomSeed");
				driver.AddFromName<global::UnityEngine.ParticleSystem>(particle.gameObject, "autoRandomSeed");
			}
		}

		internal static void PreviewActivation(global::UnityEngine.Timeline.IPropertyCollector driver, global::System.Collections.Generic.IEnumerable<global::UnityEngine.GameObject> objects)
		{
			foreach (global::UnityEngine.GameObject @object in objects)
			{
				driver.AddFromName(@object, "m_IsActive");
			}
		}

		internal static void PreviewTimeControl(global::UnityEngine.Timeline.IPropertyCollector driver, global::UnityEngine.Playables.PlayableDirector director, global::System.Collections.Generic.IEnumerable<global::UnityEngine.MonoBehaviour> scripts)
		{
			foreach (global::UnityEngine.MonoBehaviour script in scripts)
			{
				if (script is global::UnityEngine.Timeline.IPropertyPreview propertyPreview)
				{
					propertyPreview.GatherProperties(director, driver);
				}
				else
				{
					driver.AddFromComponent(script.gameObject, script);
				}
			}
		}

		internal static void PreviewDirectors(global::UnityEngine.Timeline.IPropertyCollector driver, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Playables.PlayableDirector> directors)
		{
			foreach (global::UnityEngine.Playables.PlayableDirector director in directors)
			{
				if (!(director == null))
				{
					global::UnityEngine.Timeline.TimelineAsset timelineAsset = director.playableAsset as global::UnityEngine.Timeline.TimelineAsset;
					if (!(timelineAsset == null))
					{
						timelineAsset.GatherProperties(director, driver);
					}
				}
			}
		}
	}
}

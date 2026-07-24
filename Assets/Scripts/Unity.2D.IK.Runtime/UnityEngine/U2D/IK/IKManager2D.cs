namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.DefaultExecutionOrder(-10)]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::UnityEngine.ExecuteInEditMode]
	public class IKManager2D : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.Common.IPreviewable, global::UnityEngine.Animations.IAnimationPreviewable
	{
		private static class Profiling
		{
			internal static readonly global::Unity.Profiling.ProfilerMarker UpdateManager = new global::Unity.Profiling.ProfilerMarker("IKManager2D.UpdateManager");
		}

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::UnityEngine.U2D.IK.Solver2D> m_Solvers = new global::System.Collections.Generic.List<global::UnityEngine.U2D.IK.Solver2D>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 1f)]
		private float m_Weight = 1f;

		[global::UnityEngine.SerializeField]
		private bool m_AlwaysUpdate = true;

		private bool m_CullingEnabled;

		private global::UnityEngine.U2D.IK.BaseCullingStrategy m_CullingStrategy;

		private int[] m_TransformIdCache;

		public float weight
		{
			get
			{
				return m_Weight;
			}
			set
			{
				m_Weight = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public global::System.Collections.Generic.List<global::UnityEngine.U2D.IK.Solver2D> solvers => m_Solvers;

		public bool alwaysUpdate
		{
			get
			{
				return m_AlwaysUpdate;
			}
			set
			{
				m_AlwaysUpdate = value;
				ToggleCulling(!m_AlwaysUpdate);
			}
		}

		internal global::UnityEngine.U2D.IK.BaseCullingStrategy GetCullingStrategy()
		{
			return m_CullingStrategy;
		}

		private void OnEnable()
		{
			ToggleCulling(!m_AlwaysUpdate);
		}

		private void OnDisable()
		{
			ToggleCulling(enableCulling: false);
		}

		private void ToggleCulling(bool enableCulling)
		{
			if (m_CullingStrategy == null || m_CullingEnabled != enableCulling)
			{
				m_CullingEnabled = enableCulling;
				m_CullingStrategy?.RemoveRequestingObject(this);
				if (m_CullingEnabled)
				{
					m_CullingStrategy = global::UnityEngine.U2D.IK.CullingManager.instance.GetCullingStrategy<global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy>();
				}
				else
				{
					m_CullingStrategy = global::UnityEngine.U2D.IK.CullingManager.instance.GetCullingStrategy<global::UnityEngine.U2D.IK.AlwaysUpdateCullingStrategy>();
				}
				m_CullingStrategy.AddRequestingObject(this);
			}
		}

		private void OnValidate()
		{
			m_Weight = global::UnityEngine.Mathf.Clamp01(m_Weight);
			OnEditorDataValidate();
		}

		private void Reset()
		{
			FindChildSolvers();
			OnEditorDataValidate();
		}

		private void FindChildSolvers()
		{
			m_Solvers.Clear();
			global::System.Collections.Generic.List<global::UnityEngine.U2D.IK.Solver2D> list = new global::System.Collections.Generic.List<global::UnityEngine.U2D.IK.Solver2D>();
			base.transform.GetComponentsInChildren(includeInactive: true, list);
			foreach (global::UnityEngine.U2D.IK.Solver2D item in list)
			{
				if (item.GetComponentInParent<global::UnityEngine.U2D.IK.IKManager2D>() == this)
				{
					AddSolver(item);
				}
			}
		}

		public void AddSolver(global::UnityEngine.U2D.IK.Solver2D solver)
		{
			if (!m_Solvers.Contains(solver))
			{
				m_Solvers.Add(solver);
				AddSolverEditorData();
			}
		}

		public void RemoveSolver(global::UnityEngine.U2D.IK.Solver2D solver)
		{
			RemoveSolverEditorData(solver);
			m_Solvers.Remove(solver);
		}

		public void UpdateManager()
		{
			if (m_Solvers.Count == 0)
			{
				return;
			}
			ToggleCulling(!m_AlwaysUpdate);
			bool flag = false;
			for (int i = 0; i < m_Solvers.Count; i++)
			{
				global::UnityEngine.U2D.IK.Solver2D solver2D = m_Solvers[i];
				if (!(solver2D == null) && solver2D.isActiveAndEnabled)
				{
					if (!solver2D.isValid)
					{
						solver2D.Initialize();
						flag = true;
					}
					if (!m_CullingEnabled)
					{
						solver2D.UpdateIK(m_Weight);
					}
				}
			}
			if (!m_CullingEnabled)
			{
				return;
			}
			if (flag || m_TransformIdCache == null)
			{
				CacheSolversTransformIds();
			}
			if (!m_AlwaysUpdate && !m_CullingStrategy.AreBonesVisible(m_TransformIdCache))
			{
				return;
			}
			for (int j = 0; j < m_Solvers.Count; j++)
			{
				global::UnityEngine.U2D.IK.Solver2D solver2D2 = m_Solvers[j];
				if (!(solver2D2 == null) && solver2D2.isActiveAndEnabled)
				{
					solver2D2.UpdateIK(weight);
				}
			}
		}

		private void CacheSolversTransformIds()
		{
			global::System.Collections.Generic.HashSet<int> hashSet = new global::System.Collections.Generic.HashSet<int>();
			for (int i = 0; i < solvers.Count; i++)
			{
				global::UnityEngine.U2D.IK.Solver2D solver2D = solvers[i];
				for (int j = 0; j < solver2D.chainCount; j++)
				{
					global::UnityEngine.U2D.IK.IKChain2D chain = solver2D.GetChain(j);
					for (int k = 0; k < chain.transformCount; k++)
					{
						global::UnityEngine.Transform transform = chain.transforms[k];
						if (transform != null)
						{
							hashSet.Add(transform.GetInstanceID());
						}
					}
				}
			}
			m_TransformIdCache = global::System.Linq.Enumerable.ToArray(hashSet);
		}

		public void OnPreviewUpdate()
		{
		}

		private static bool IsInGUIUpdateLoop()
		{
			return global::UnityEngine.Event.current != null;
		}

		private void LateUpdate()
		{
			UpdateManager();
		}

		private void OnEditorDataValidate()
		{
		}

		private void AddSolverEditorData()
		{
		}

		private void RemoveSolverEditorData(global::UnityEngine.U2D.IK.Solver2D solver)
		{
		}
	}
}

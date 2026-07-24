namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	public abstract class Solver2D : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.Common.IPreviewable, global::UnityEngine.Animations.IAnimationPreviewable
	{
		private static class Profiling
		{
			internal static readonly global::Unity.Profiling.ProfilerMarker Initialize = new global::Unity.Profiling.ProfilerMarker("Solver2D.Initialize");

			internal static readonly global::Unity.Profiling.ProfilerMarker Prepare = new global::Unity.Profiling.ProfilerMarker("Solver2D.Prepare");

			internal static readonly global::Unity.Profiling.ProfilerMarker UpdateIK = new global::Unity.Profiling.ProfilerMarker("Solver2D.UpdateIK");
		}

		[global::UnityEngine.SerializeField]
		private bool m_ConstrainRotation = true;

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_RestoreDefaultPose")]
		[global::UnityEngine.SerializeField]
		private bool m_SolveFromDefaultPose = true;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Range(0f, 1f)]
		private float m_Weight = 1f;

		private global::UnityEngine.Plane m_Plane;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> m_TargetPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();

		private bool m_IsValid;

		private float m_LastFinalWeight;

		public int chainCount => GetChainCount();

		public bool constrainRotation
		{
			get
			{
				return m_ConstrainRotation;
			}
			set
			{
				m_ConstrainRotation = value;
			}
		}

		public bool solveFromDefaultPose
		{
			get
			{
				return m_SolveFromDefaultPose;
			}
			set
			{
				m_SolveFromDefaultPose = value;
			}
		}

		public bool isValid => m_IsValid;

		public bool allChainsHaveTargets => HasTargets();

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

		internal ref global::UnityEngine.Plane GetPlane()
		{
			return ref m_Plane;
		}

		~Solver2D()
		{
			CleanUp();
		}

		private void OnDestroy()
		{
			CleanUp();
		}

		protected virtual void OnValidate()
		{
			m_Weight = global::UnityEngine.Mathf.Clamp01(m_Weight);
			m_IsValid = Validate();
		}

		private bool Validate()
		{
			for (int i = 0; i < GetChainCount(); i++)
			{
				if (!GetChain(i).isValid)
				{
					return false;
				}
			}
			return DoValidate();
		}

		private bool HasTargets()
		{
			for (int i = 0; i < GetChainCount(); i++)
			{
				if (GetChain(i).target == null)
				{
					return false;
				}
			}
			return true;
		}

		public void Initialize()
		{
			DoInitialize();
			for (int i = 0; i < GetChainCount(); i++)
			{
				GetChain(i).Initialize();
			}
			m_IsValid = Validate();
		}

		private void Prepare()
		{
			global::UnityEngine.Transform planeRootTransform = GetPlaneRootTransform();
			if (planeRootTransform != null)
			{
				m_Plane.normal = planeRootTransform.forward;
				m_Plane.distance = 0f - global::UnityEngine.Vector3.Dot(m_Plane.normal, planeRootTransform.position);
			}
			for (int i = 0; i < GetChainCount(); i++)
			{
				global::UnityEngine.U2D.IK.IKChain2D chain = GetChain(i);
				bool targetRotationIsConstrained = constrainRotation && chain.target != null;
				if (m_SolveFromDefaultPose)
				{
					chain.RestoreDefaultPose(targetRotationIsConstrained);
				}
			}
			DoPrepare();
		}

		private void PrepareEffectorPositions()
		{
			m_TargetPositions.Clear();
			for (int i = 0; i < GetChainCount(); i++)
			{
				global::UnityEngine.U2D.IK.IKChain2D chain = GetChain(i);
				if ((bool)chain.target)
				{
					m_TargetPositions.Add(chain.target.position);
				}
			}
		}

		public void UpdateIK(float globalWeight)
		{
			if (allChainsHaveTargets)
			{
				PrepareEffectorPositions();
				UpdateIK(m_TargetPositions, globalWeight);
			}
		}

		public void UpdateIK(global::System.Collections.Generic.List<global::UnityEngine.Vector3> targetPositions, float globalWeight)
		{
			if (targetPositions.Count != chainCount)
			{
				return;
			}
			float num = globalWeight * weight;
			bool flag = global::System.Math.Abs(num - m_LastFinalWeight) > 0.0001f;
			m_LastFinalWeight = num;
			if ((num == 0f && !flag) || (!isValid && !Validate()))
			{
				return;
			}
			if (num < 1f)
			{
				StoreLocalRotations();
			}
			Prepare();
			DoUpdateIK(targetPositions);
			if (constrainRotation)
			{
				for (int i = 0; i < GetChainCount(); i++)
				{
					global::UnityEngine.U2D.IK.IKChain2D chain = GetChain(i);
					if ((bool)chain.target)
					{
						chain.effector.rotation = chain.target.rotation;
					}
				}
			}
			if (num < 1f)
			{
				BlendFkToIk(num);
			}
		}

		private void StoreLocalRotations()
		{
			for (int i = 0; i < GetChainCount(); i++)
			{
				GetChain(i).StoreLocalRotations();
			}
		}

		private void BlendFkToIk(float finalWeight)
		{
			for (int i = 0; i < GetChainCount(); i++)
			{
				global::UnityEngine.U2D.IK.IKChain2D chain = GetChain(i);
				bool targetRotationIsConstrained = constrainRotation && chain.target != null;
				chain.BlendFkToIk(finalWeight, targetRotationIsConstrained);
			}
		}

		private void CleanUp()
		{
			if (this is global::UnityEngine.U2D.IK.ISolverCleanup solverCleanup)
			{
				solverCleanup.DoCleanUp();
			}
		}

		public abstract global::UnityEngine.U2D.IK.IKChain2D GetChain(int index);

		protected abstract int GetChainCount();

		protected abstract void DoUpdateIK(global::System.Collections.Generic.List<global::UnityEngine.Vector3> targetPositions);

		protected virtual bool DoValidate()
		{
			return true;
		}

		protected virtual void DoInitialize()
		{
		}

		protected virtual void DoPrepare()
		{
		}

		protected virtual global::UnityEngine.Transform GetPlaneRootTransform()
		{
			if (chainCount <= 0)
			{
				return null;
			}
			return GetChain(0).rootTransform;
		}

		protected global::UnityEngine.Vector3 GetPointOnSolverPlane(global::UnityEngine.Vector3 worldPosition)
		{
			return GetPlaneRootTransform().InverseTransformPoint(m_Plane.ClosestPointOnPlane(worldPosition));
		}

		protected global::UnityEngine.Vector3 GetWorldPositionFromSolverPlanePoint(global::UnityEngine.Vector2 planePoint)
		{
			return GetPlaneRootTransform().TransformPoint(planePoint);
		}

		public void OnPreviewUpdate()
		{
		}
	}
}

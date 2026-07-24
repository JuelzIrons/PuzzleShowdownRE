namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteInEditMode]
	internal class DisallowSmallMeshCulling : global::UnityEngine.MonoBehaviour
	{
		private bool m_AppliedRecursively;

		public bool m_applyToChildrenRecursively;

		public bool applyToChildrenRecursively
		{
			get
			{
				return m_applyToChildrenRecursively;
			}
			set
			{
				m_applyToChildrenRecursively = value;
				OnDisable();
				OnEnable();
			}
		}

		private void OnEnable()
		{
			m_AppliedRecursively = applyToChildrenRecursively;
			if (applyToChildrenRecursively)
			{
				AllowSmallMeshCullingRecursively(base.transform, allow: false);
			}
			else
			{
				AllowSmallMeshCulling(base.transform, allow: false);
			}
		}

		private void OnDisable()
		{
			if (m_AppliedRecursively)
			{
				AllowSmallMeshCullingRecursively(base.transform, allow: true);
			}
			else
			{
				AllowSmallMeshCulling(base.transform, allow: true);
			}
		}

		private static void AllowSmallMeshCulling(global::UnityEngine.Transform transform, bool allow)
		{
			global::UnityEngine.MeshRenderer component = transform.GetComponent<global::UnityEngine.MeshRenderer>();
			if ((bool)component)
			{
				component.smallMeshCulling = allow;
			}
		}

		private static void AllowSmallMeshCullingRecursively(global::UnityEngine.Transform transform, bool allow)
		{
			AllowSmallMeshCulling(transform, allow);
			foreach (global::UnityEngine.Transform item in transform)
			{
				if (!item.GetComponent<global::UnityEngine.Rendering.DisallowGPUDrivenRendering>())
				{
					AllowSmallMeshCullingRecursively(item, allow);
				}
			}
		}

		private void OnValidate()
		{
			OnDisable();
			OnEnable();
		}
	}
}

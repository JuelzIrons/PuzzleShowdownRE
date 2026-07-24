namespace UnityEngine.Rendering
{
	[global::UnityEngine.ExecuteInEditMode]
	internal class DisallowGPUDrivenRendering : global::UnityEngine.MonoBehaviour
	{
		private bool m_AppliedRecursively;

		[global::UnityEngine.Serialization.FormerlySerializedAs("applyToChildrenRecursively")]
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
				AllowGPUDrivenRenderingRecursively(base.transform, allow: false);
			}
			else
			{
				AllowGPUDrivenRendering(base.transform, allow: false);
			}
		}

		private void OnDisable()
		{
			if (m_AppliedRecursively)
			{
				AllowGPUDrivenRenderingRecursively(base.transform, allow: true);
			}
			else
			{
				AllowGPUDrivenRendering(base.transform, allow: true);
			}
		}

		private static void AllowGPUDrivenRendering(global::UnityEngine.Transform transform, bool allow)
		{
			global::UnityEngine.MeshRenderer component = transform.GetComponent<global::UnityEngine.MeshRenderer>();
			if ((bool)component)
			{
				component.allowGPUDrivenRendering = allow;
			}
		}

		private static void AllowGPUDrivenRenderingRecursively(global::UnityEngine.Transform transform, bool allow)
		{
			AllowGPUDrivenRendering(transform, allow);
			foreach (global::UnityEngine.Transform item in transform)
			{
				if (!item.GetComponent<global::UnityEngine.Rendering.DisallowGPUDrivenRendering>())
				{
					AllowGPUDrivenRenderingRecursively(item, allow);
				}
			}
		}

		private void OnValidate()
		{
			OnDisable();
			if (base.enabled)
			{
				OnEnable();
			}
		}
	}
}

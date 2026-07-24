namespace UnityEngine.Rendering.Universal
{
	internal class ShapeProviderUtility
	{
		public static void CallOnBeforeRender(global::UnityEngine.Rendering.Universal.ShadowShape2DProvider shapeProvider, global::UnityEngine.Component component, global::UnityEngine.Rendering.Universal.ShadowMesh2D shadowMesh, global::UnityEngine.Bounds bounds)
		{
			if (component != null)
			{
				if (shapeProvider != null && component.gameObject.activeInHierarchy)
				{
					shapeProvider.OnBeforeRender(component, bounds, shadowMesh);
				}
			}
			else if (shadowMesh != null && shadowMesh.mesh != null)
			{
				shadowMesh.mesh.Clear();
			}
		}

		public static void PersistantDataCreated(global::UnityEngine.Rendering.Universal.ShadowShape2DProvider shapeProvider, global::UnityEngine.Component component, global::UnityEngine.Rendering.Universal.ShadowMesh2D shadowMesh)
		{
			if (component != null)
			{
				shapeProvider?.OnPersistantDataCreated(component, shadowMesh);
			}
		}
	}
}

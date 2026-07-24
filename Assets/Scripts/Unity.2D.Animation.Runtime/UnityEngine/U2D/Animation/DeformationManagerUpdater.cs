namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.AddComponentMenu("")]
	[global::UnityEngine.DefaultExecutionOrder(10)]
	[global::UnityEngine.ExecuteInEditMode]
	internal class DeformationManagerUpdater : global::UnityEngine.MonoBehaviour
	{
		private global::Unity.Profiling.ProfilerMarker m_ProfilerMarker = new global::Unity.Profiling.ProfilerMarker("DeformationManager.LateUpdate");

		public global::System.Action<global::UnityEngine.GameObject> onDestroyingComponent { get; set; }

		private void OnDestroy()
		{
			onDestroyingComponent?.Invoke(base.gameObject);
		}

		private void LateUpdate()
		{
			if (global::UnityEngine.U2D.Animation.DeformationManager.instance.helperGameObject != base.gameObject)
			{
				global::UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
			else
			{
				global::UnityEngine.U2D.Animation.DeformationManager.instance.Update();
			}
		}
	}
}

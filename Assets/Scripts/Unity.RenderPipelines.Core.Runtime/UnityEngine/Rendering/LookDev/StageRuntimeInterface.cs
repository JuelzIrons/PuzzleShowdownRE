namespace UnityEngine.Rendering.LookDev
{
	public class StageRuntimeInterface
	{
		private global::System.Func<bool, global::UnityEngine.GameObject> m_AddGameObject;

		private global::System.Func<global::UnityEngine.Camera> m_GetCamera;

		private global::System.Func<global::UnityEngine.Light> m_GetSunLight;

		public object SRPData;

		public global::UnityEngine.Camera camera => m_GetCamera?.Invoke();

		public global::UnityEngine.Light sunLight => m_GetSunLight?.Invoke();

		public StageRuntimeInterface(global::System.Func<bool, global::UnityEngine.GameObject> AddGameObject, global::System.Func<global::UnityEngine.Camera> GetCamera, global::System.Func<global::UnityEngine.Light> GetSunLight)
		{
			m_AddGameObject = AddGameObject;
			m_GetCamera = GetCamera;
			m_GetSunLight = GetSunLight;
		}

		public global::UnityEngine.GameObject AddGameObject(bool persistent = false)
		{
			return m_AddGameObject?.Invoke(persistent);
		}
	}
}

namespace UnityEngine.EventSystems
{
	public static class RaycasterManager
	{
		private static readonly global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseRaycaster> s_Raycasters = new global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseRaycaster>();

		internal static void AddRaycaster(global::UnityEngine.EventSystems.BaseRaycaster baseRaycaster)
		{
			if (!s_Raycasters.Contains(baseRaycaster))
			{
				s_Raycasters.Add(baseRaycaster);
			}
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.EventSystems.BaseRaycaster> GetRaycasters()
		{
			return s_Raycasters;
		}

		internal static void RemoveRaycasters(global::UnityEngine.EventSystems.BaseRaycaster baseRaycaster)
		{
			if (s_Raycasters.Contains(baseRaycaster))
			{
				s_Raycasters.Remove(baseRaycaster);
			}
		}
	}
}

namespace UnityEngine.UI
{
	public class ClipperRegistry
	{
		private static global::UnityEngine.UI.ClipperRegistry s_Instance;

		private readonly global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.IClipper> m_Clippers = new global::UnityEngine.UI.Collections.IndexedSet<global::UnityEngine.UI.IClipper>();

		public static global::UnityEngine.UI.ClipperRegistry instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::UnityEngine.UI.ClipperRegistry();
				}
				return s_Instance;
			}
		}

		protected ClipperRegistry()
		{
		}

		public void Cull()
		{
			int count = m_Clippers.Count;
			for (int i = 0; i < count; i++)
			{
				m_Clippers[i].PerformClipping();
			}
		}

		public static void Register(global::UnityEngine.UI.IClipper c)
		{
			if (c != null)
			{
				instance.m_Clippers.AddUnique(c);
			}
		}

		public static void Unregister(global::UnityEngine.UI.IClipper c)
		{
			instance.m_Clippers.Remove(c);
		}

		public static void Disable(global::UnityEngine.UI.IClipper c)
		{
			instance.m_Clippers.DisableItem(c);
		}
	}
}

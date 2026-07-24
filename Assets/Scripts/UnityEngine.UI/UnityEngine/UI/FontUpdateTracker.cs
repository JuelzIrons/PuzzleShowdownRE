namespace UnityEngine.UI
{
	public static class FontUpdateTracker
	{
		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.Font, global::System.Collections.Generic.HashSet<global::UnityEngine.UI.Text>> m_Tracked = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Font, global::System.Collections.Generic.HashSet<global::UnityEngine.UI.Text>>();

		public static void TrackText(global::UnityEngine.UI.Text t)
		{
			if (t.font == null)
			{
				return;
			}
			m_Tracked.TryGetValue(t.font, out var value);
			if (value == null)
			{
				if (m_Tracked.Count == 0)
				{
					global::UnityEngine.Font.textureRebuilt += RebuildForFont;
				}
				value = new global::System.Collections.Generic.HashSet<global::UnityEngine.UI.Text>();
				m_Tracked.Add(t.font, value);
			}
			value.Add(t);
		}

		private static void RebuildForFont(global::UnityEngine.Font f)
		{
			m_Tracked.TryGetValue(f, out var value);
			if (value == null)
			{
				return;
			}
			foreach (global::UnityEngine.UI.Text item in value)
			{
				item.FontTextureChanged();
			}
		}

		public static void UntrackText(global::UnityEngine.UI.Text t)
		{
			if (t.font == null)
			{
				return;
			}
			m_Tracked.TryGetValue(t.font, out var value);
			if (value == null)
			{
				return;
			}
			value.Remove(t);
			if (value.Count == 0)
			{
				m_Tracked.Remove(t.font);
				if (m_Tracked.Count == 0)
				{
					global::UnityEngine.Font.textureRebuilt -= RebuildForFont;
				}
			}
		}
	}
}

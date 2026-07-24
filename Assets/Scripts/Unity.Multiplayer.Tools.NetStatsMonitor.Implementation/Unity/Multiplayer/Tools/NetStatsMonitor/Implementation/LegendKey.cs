namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class LegendKey : global::UnityEngine.UIElements.VisualElement
	{
		private global::UnityEngine.UIElements.Label m_KeyLabel;

		private global::UnityEngine.UIElements.VisualElement m_Swatch;

		internal LegendKey()
		{
			AddToClassList("rnsm-graph-legendkey");
			AddToClassList("rnsm-graph-legendkey");
			m_Swatch = new global::UnityEngine.UIElements.VisualElement();
			m_Swatch.AddToClassList("rnsm-graph-legendkey-swatch");
			Add(m_Swatch);
			m_KeyLabel = new global::UnityEngine.UIElements.Label();
			m_KeyLabel.AddToClassList("rnsm-graph-legendkey-label");
			Add(m_KeyLabel);
		}

		internal LegendKey(string name, global::UnityEngine.Color32 color)
			: this()
		{
			UpdateName(name);
			UpdateColor(color);
		}

		internal void Update(string name, global::UnityEngine.Color color)
		{
			UpdateName(name);
			UpdateColor(color);
		}

		internal void UpdateName(string name)
		{
			base.name = name;
			m_KeyLabel.text = name;
		}

		internal void UpdateColor(global::UnityEngine.Color color)
		{
			m_Swatch.style.backgroundColor = color;
		}
	}
}

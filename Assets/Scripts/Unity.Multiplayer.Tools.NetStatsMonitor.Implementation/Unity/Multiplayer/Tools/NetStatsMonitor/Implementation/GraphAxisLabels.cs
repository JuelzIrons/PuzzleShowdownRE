namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphAxisLabels : global::UnityEngine.UIElements.VisualElement
	{
		private readonly global::UnityEngine.UIElements.Label m_MinLabel = new global::UnityEngine.UIElements.Label();

		private readonly global::UnityEngine.UIElements.Label m_MaxLabel = new global::UnityEngine.UIElements.Label();

		public string MinLabel
		{
			get
			{
				return m_MinLabel.text;
			}
			set
			{
				m_MinLabel.text = value;
			}
		}

		public string MaxLabel
		{
			get
			{
				return m_MaxLabel.text;
			}
			set
			{
				m_MaxLabel.text = value;
			}
		}

		public global::UnityEngine.UIElements.StyleLength MaxLabelMarginRight
		{
			get
			{
				return m_MaxLabel.style.marginRight;
			}
			set
			{
				m_MaxLabel.style.marginRight = value;
			}
		}

		internal GraphAxisLabels()
		{
			AddToClassList("rnsm-graph-axis");
			m_MinLabel.AddToClassList("rnsm-graph-axis-min-value");
			m_MaxLabel.AddToClassList("rnsm-graph-axis-max-value");
			Add(m_MinLabel);
			Add(m_MaxLabel);
		}

		public void SetLabels(string minLabel, string maxLabel)
		{
			MinLabel = minLabel;
			MaxLabel = maxLabel;
		}
	}
}

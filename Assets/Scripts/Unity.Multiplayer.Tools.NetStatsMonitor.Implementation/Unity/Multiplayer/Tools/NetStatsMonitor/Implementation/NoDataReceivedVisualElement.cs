namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class NoDataReceivedVisualElement : global::UnityEngine.UIElements.VisualElement
	{
		private global::UnityEngine.UIElements.Label m_Label = new global::UnityEngine.UIElements.Label();

		internal NoDataReceivedVisualElement()
		{
			AddToClassList("rnsm-display-element");
			AddToClassList("rnsm-no-data-received");
			Add(m_Label);
			m_Label.AddToClassList("rnsm-no-data-received-label");
		}

		internal void Update(double secondsSinceDataReceived)
		{
			string text = secondsSinceDataReceived.ToString("N0");
			m_Label.text = "No data received for " + text + " seconds";
		}
	}
}

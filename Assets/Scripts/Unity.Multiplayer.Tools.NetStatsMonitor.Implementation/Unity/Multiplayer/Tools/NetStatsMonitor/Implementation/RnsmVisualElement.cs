namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class RnsmVisualElement : global::UnityEngine.UIElements.VisualElement
	{
		private readonly global::UnityEngine.UIElements.Label m_Title = new global::UnityEngine.UIElements.Label();

		private readonly global::UnityEngine.UIElements.VisualElement m_DisplayElementsContainer = new global::UnityEngine.UIElements.VisualElement();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.CounterVisualElement> m_Counters = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.CounterVisualElement>();

		private readonly global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphVisualElement> m_Graphs = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphVisualElement>();

		private readonly global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NoDataReceivedVisualElement m_NoDataReceivedMessage = new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.NoDataReceivedVisualElement();

		private global::UnityEngine.UIElements.EventCallback<global::UnityEngine.UIElements.GeometryChangedEvent> m_OnGeoChange;

		private int m_LastUpdateFrame;

		private const float k_EpsilonPixels = 0.5f;

		internal RnsmVisualElement()
		{
			base.pickingMode = global::UnityEngine.UIElements.PickingMode.Ignore;
			AddToClassList("rnsm-monitor");
			m_Title.AddToClassList("rnsm-title");
			m_Title.text = "Runtime Network Stats";
			Add(m_Title);
			m_DisplayElementsContainer.AddToClassList("rnsm-display-elements");
			Add(m_DisplayElementsContainer);
		}

		public void UpdateConfiguration(global::Unity.Multiplayer.Tools.NetStatsMonitor.NetStatsMonitorConfiguration configuration)
		{
			m_DisplayElementsContainer.Clear();
			if (configuration == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration displayElement in configuration.DisplayElements)
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType type = displayElement.Type;
				if (type != global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter)
				{
					if ((uint)(type - 1) > 1u)
					{
						throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "DisplayElementType", type));
					}
					while (num2 >= m_Graphs.Count)
					{
						m_Graphs.Add(new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphVisualElement());
					}
					global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphVisualElement graphVisualElement = m_Graphs[num2];
					graphVisualElement.UpdateConfiguration(displayElement);
					m_DisplayElementsContainer.Add(graphVisualElement);
					num2++;
				}
				else
				{
					while (num >= m_Counters.Count)
					{
						m_Counters.Add(new global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.CounterVisualElement());
					}
					global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.CounterVisualElement counterVisualElement = m_Counters[num];
					counterVisualElement.UpdateConfiguration(displayElement);
					m_DisplayElementsContainer.Add(counterVisualElement);
					num++;
				}
			}
			if (m_Counters.Count > num)
			{
				m_Counters.RemoveRange(num, m_Counters.Count - num);
			}
			if (m_Graphs.Count > num2)
			{
				m_Graphs.RemoveRange(num2, m_Graphs.Count - num2);
			}
		}

		public void UpdateDisplayData(global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.MultiStatHistory stats, global::Unity.Multiplayer.Tools.Common.EnumMap<global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate, bool> newDataAvailable, double time)
		{
			if (m_DisplayElementsContainer.Contains(m_NoDataReceivedMessage))
			{
				m_DisplayElementsContainer.Remove(m_NoDataReceivedMessage);
			}
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.CounterVisualElement counter in m_Counters)
			{
				if (newDataAvailable[counter.SampleRate])
				{
					counter.UpdateDisplayData(stats, time);
				}
			}
			foreach (global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphVisualElement graph in m_Graphs)
			{
				if (newDataAvailable[graph.SampleRate])
				{
					graph.UpdateDisplayData(stats);
				}
			}
		}

		public void DisplayDataNotReceivedMessage(double secondsSinceDataReceived)
		{
			if (!Contains(m_NoDataReceivedMessage))
			{
				m_DisplayElementsContainer.Insert(0, m_NoDataReceivedMessage);
			}
			m_NoDataReceivedMessage.Update(secondsSinceDataReceived);
		}

		public void ApplyPosition(global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration positionConfiguration)
		{
			if (m_OnGeoChange != null)
			{
				base.parent.UnregisterCallback(m_OnGeoChange);
				UnregisterCallback(m_OnGeoChange);
				m_OnGeoChange = null;
			}
			if (positionConfiguration.OverridePosition)
			{
				ChangePosition(positionConfiguration, forced: true);
				m_OnGeoChange = delegate(global::UnityEngine.UIElements.GeometryChangedEvent evt)
				{
					if (global::System.MathF.Abs(evt.newRect.width - evt.oldRect.width) > 0.5f || global::System.MathF.Abs(evt.newRect.height - evt.oldRect.height) > 0.5f)
					{
						ChangePosition(positionConfiguration);
					}
				};
				base.parent.RegisterCallback(m_OnGeoChange);
				RegisterCallback(m_OnGeoChange);
			}
			else
			{
				base.style.left = global::UnityEngine.UIElements.StyleKeyword.Null;
				base.style.top = global::UnityEngine.UIElements.StyleKeyword.Null;
			}
		}

		private void ChangePosition(global::Unity.Multiplayer.Tools.NetStatsMonitor.PositionConfiguration positionConfiguration, bool forced = false)
		{
			if (global::UnityEngine.Time.frameCount != m_LastUpdateFrame || forced)
			{
				m_LastUpdateFrame = global::UnityEngine.Time.frameCount;
				float width = base.parent.contentRect.width;
				float height = base.parent.contentRect.height;
				float value = positionConfiguration.PositionLeftToRight * (width - base.contentRect.width);
				float value2 = positionConfiguration.PositionTopToBottom * (height - base.contentRect.height);
				base.style.left = new global::UnityEngine.UIElements.Length(value, global::UnityEngine.UIElements.LengthUnit.Pixel);
				base.style.top = new global::UnityEngine.UIElements.Length(value2, global::UnityEngine.UIElements.LengthUnit.Pixel);
			}
		}
	}
}

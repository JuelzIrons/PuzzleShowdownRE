namespace Unity.Multiplayer.Tools.NetStatsMonitor
{
	[global::System.Serializable]
	public sealed class DisplayElementConfiguration : global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::System.Serializable]
		private struct SerializedStat
		{
			[field: global::UnityEngine.HideInInspector]
			[field: global::UnityEngine.SerializeField]
			public string TypeName { get; set; }

			[field: global::UnityEngine.HideInInspector]
			[field: global::UnityEngine.SerializeField]
			public string ValueName { get; set; }
		}

		private int m_PreviousStatsHash;

		private string m_PreviousGeneratedLabel = "";

		private bool m_SerializedStatsLoaded;

		[field: global::UnityEngine.HideInInspector]
		[field: global::UnityEngine.SerializeField]
		internal bool FieldsInitialized { get; private set; } = true;

		[global::UnityEngine.Tooltip("The label to display for this visual element in the on-screen display. For graphs this field is optional, as the variables displayed in the graph are shown in the legend. Consider leaving this field blank for graphs if you would like to make them more compact.")]
		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType Type { get; set; }

		[field: global::UnityEngine.SerializeField]
		public string Label { get; set; } = "";

		[field: global::UnityEngine.SerializeField]
		public global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId> Stats { get; set; } = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.MetricId>();

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.CounterConfiguration CounterConfiguration { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.CounterConfiguration();

		[field: global::UnityEngine.SerializeField]
		public global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphConfiguration GraphConfiguration { get; set; } = new global::Unity.Multiplayer.Tools.NetStatsMonitor.GraphConfiguration();

		internal int SampleCount
		{
			get
			{
				switch (Type)
				{
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter:
					return CounterConfiguration.SampleCount;
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.LineGraph:
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.StackedAreaGraph:
					return GraphConfiguration.SampleCount;
				default:
					throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "DisplayElementType", Type));
				}
			}
		}

		internal global::Unity.Multiplayer.Tools.NetStatsMonitor.SampleRate SampleRate
		{
			get
			{
				switch (Type)
				{
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter:
					return CounterConfiguration.SampleRate;
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.LineGraph:
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.StackedAreaGraph:
					return GraphConfiguration.SampleRate;
				default:
					throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "DisplayElementType", Type));
				}
			}
		}

		internal double? HalfLife
		{
			get
			{
				switch (Type)
				{
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter:
				{
					global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod smoothingMethod = CounterConfiguration.SmoothingMethod;
					return smoothingMethod switch
					{
						global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.ExponentialMovingAverage => CounterConfiguration.ExponentialMovingAverageParams.HalfLife, 
						global::Unity.Multiplayer.Tools.NetStatsMonitor.SmoothingMethod.SimpleMovingAverage => null, 
						_ => throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "SmoothingMethod", smoothingMethod)), 
					};
				}
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.LineGraph:
				case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.StackedAreaGraph:
					return null;
				default:
					throw new global::System.NotSupportedException(string.Format("Unhandled {0} {1}", "DisplayElementType", Type));
				}
			}
		}

		internal double? DecayConstant
		{
			get
			{
				if (!HalfLife.HasValue)
				{
					return null;
				}
				return global::Unity.Multiplayer.Tools.Common.ContinuousExponentialMovingAverage.GetDecayConstantForHalfLife(HalfLife.Value);
			}
		}

		[field: global::UnityEngine.HideInInspector]
		[field: global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration.SerializedStat> SerializedStats { get; set; } = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration.SerializedStat>();

		internal void OnValidate()
		{
			RefreshGenerateLabel();
			ValidateColors();
		}

		private void RefreshGenerateLabel()
		{
			if (Type != global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter)
			{
				return;
			}
			int num = ComputeStatsHashCode();
			if (m_PreviousStatsHash == 0)
			{
				m_PreviousStatsHash = num;
				m_PreviousGeneratedLabel = global::Unity.Multiplayer.Tools.NetStatsMonitor.Configuration.LabelGeneration.GenerateLabel(Stats);
			}
			else if (num != m_PreviousStatsHash)
			{
				m_PreviousStatsHash = num;
				string text = global::Unity.Multiplayer.Tools.NetStatsMonitor.Configuration.LabelGeneration.GenerateLabel(Stats);
				if (Label == m_PreviousGeneratedLabel)
				{
					Label = text;
				}
				m_PreviousGeneratedLabel = text;
			}
		}

		private void ValidateColors()
		{
			global::System.Collections.Generic.List<global::UnityEngine.Color> list = GraphConfiguration?.VariableColors;
			if (list == null)
			{
				return;
			}
			bool flag = true;
			for (int i = 0; i < list.Count; i++)
			{
				global::UnityEngine.Color color = list[i];
				if (color.a != 0f || color.r != 0f || color.g != 0f || color.b != 0f)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				for (int j = 0; j < list.Count; j++)
				{
					global::UnityEngine.Color value = list[j];
					value.a = 1f;
					list[j] = value;
				}
			}
		}

		public void OnBeforeSerialize()
		{
			int count = Stats.Count;
			global::Unity.Multiplayer.Tools.Common.ListUtil.Resize(SerializedStats, count);
			for (int i = 0; i < count; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.MetricId metricId = Stats[i];
				SerializedStats[i] = new global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration.SerializedStat
				{
					TypeName = metricId.EnumType.AssemblyQualifiedName,
					ValueName = metricId.Name
				};
			}
		}

		public void OnAfterDeserialize()
		{
			if (m_SerializedStatsLoaded)
			{
				return;
			}
			m_SerializedStatsLoaded = true;
			int count = SerializedStats.Count;
			global::Unity.Multiplayer.Tools.Common.ListUtil.Resize(Stats, count);
			for (int i = 0; i < count; i++)
			{
				global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementConfiguration.SerializedStat serializedStat = SerializedStats[i];
				global::System.Type type = global::System.Type.GetType(serializedStat.TypeName);
				if (!(type == null))
				{
					int typeIndex = global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetTypeIndex(type);
					global::System.Collections.Generic.IReadOnlyList<string> enumNames = global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumNames(typeIndex);
					string valueName = serializedStat.ValueName;
					int num = global::Unity.Multiplayer.Tools.Common.ReadOnlyListExtensions.IndexOf(enumNames, valueName);
					if (num != -1)
					{
						int enumValue = global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumValues(typeIndex)[num];
						Stats[i] = new global::Unity.Multiplayer.Tools.NetStats.MetricId(typeIndex, enumValue);
					}
				}
			}
		}

		internal int ComputeStatsHashCode()
		{
			int num = 0;
			foreach (global::Unity.Multiplayer.Tools.NetStats.MetricId stat in Stats)
			{
				num = global::System.HashCode.Combine(num, stat);
			}
			return num;
		}

		internal int ComputeHashCode()
		{
			int value = global::System.HashCode.Combine(Type, Label, ComputeStatsHashCode());
			switch (Type)
			{
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.Counter:
				return global::System.HashCode.Combine(value, CounterConfiguration.ComputeHashCode());
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.LineGraph:
			case global::Unity.Multiplayer.Tools.NetStatsMonitor.DisplayElementType.StackedAreaGraph:
				return global::System.HashCode.Combine(value, GraphConfiguration.ComputeHashCode());
			default:
				throw new global::System.ArgumentOutOfRangeException(string.Format("Unknow {0} {1}", "DisplayElementType", Type));
			}
		}
	}
}

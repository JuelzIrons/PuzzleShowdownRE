namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal class EventMetric<TValue> : global::Unity.Multiplayer.Tools.NetStats.IEventMetric<TValue>, global::Unity.Multiplayer.Tools.NetStats.IEventMetric, global::Unity.Multiplayer.Tools.NetStats.IMetric, global::Unity.Multiplayer.Tools.NetStats.IResettable where TValue : unmanaged
	{
		private const int k_DefaultMaxNumberOfValues = 1000;

		private readonly global::System.Collections.Generic.List<TValue> m_Values = new global::System.Collections.Generic.List<TValue>();

		public int Count => m_Values.Count;

		public string Name => Id.ToString();

		public global::Unity.Multiplayer.Tools.NetStats.MetricId Id { get; }

		public global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType => global::Unity.Multiplayer.Tools.NetStats.MetricContainerType.Event;

		public global::Unity.Collections.FixedString128Bytes FactoryTypeName { get; }

		public global::System.Collections.Generic.IReadOnlyList<TValue> Values => m_Values;

		public bool ShouldResetOnDispatch { get; set; } = true;

		public int MaxNumberOfValues { get; set; } = 1000;

		public int NumberOfValuesReceived { get; private set; }

		public EventMetric(global::Unity.Multiplayer.Tools.NetStats.MetricId id)
		{
			Id = id;
			if (global::Unity.Multiplayer.Tools.NetStats.EventMetricFactory.TryGetFactoryTypeName(typeof(TValue), out var typeName))
			{
				FactoryTypeName = typeName;
			}
		}

		public int GetWriteSize()
		{
			return 0 + global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.GetWriteSize<int>() + global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.GetWriteSize<TValue>() * m_Values.Count;
		}

		public void Write(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter writer)
		{
			writer.WriteValue<int>(m_Values.Count);
			for (int i = 0; i < m_Values.Count; i++)
			{
				writer.WriteValue<TValue>(m_Values[i]);
			}
		}

		public void Read(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader reader)
		{
			m_Values.Clear();
			reader.ReadValueSafe(out int value);
			for (int i = 0; i < value; i++)
			{
				reader.ReadValueSafe(out TValue value2);
				m_Values.Add(value2);
			}
		}

		public void Mark(TValue value)
		{
			int numberOfValuesReceived = NumberOfValuesReceived + 1;
			NumberOfValuesReceived = numberOfValuesReceived;
			if (m_Values.Count < MaxNumberOfValues)
			{
				m_Values.Add(value);
			}
		}

		public void Reset()
		{
			m_Values.Clear();
			NumberOfValuesReceived = 0;
		}
	}
}

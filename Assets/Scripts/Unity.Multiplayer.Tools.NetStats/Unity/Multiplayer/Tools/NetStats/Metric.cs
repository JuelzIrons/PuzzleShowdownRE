namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	internal abstract class Metric<TValue> : global::Unity.Multiplayer.Tools.NetStats.IMetric<TValue>, global::Unity.Multiplayer.Tools.NetStats.IMetric, global::Unity.Multiplayer.Tools.NetStats.IResettable where TValue : unmanaged
	{
		public string Name => Id.ToString();

		public global::Unity.Multiplayer.Tools.NetStats.MetricId Id { get; }

		public abstract global::Unity.Multiplayer.Tools.NetStats.MetricContainerType MetricContainerType { get; }

		public global::Unity.Collections.FixedString128Bytes FactoryTypeName => default(global::Unity.Collections.FixedString128Bytes);

		public TValue Value { get; protected set; }

		protected TValue DefaultValue { get; }

		public bool ShouldResetOnDispatch { get; set; } = true;

		protected Metric(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId, TValue defaultValue = default(TValue))
		{
			Id = metricId;
			DefaultValue = defaultValue;
			Value = defaultValue;
		}

		public int GetWriteSize()
		{
			return global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.GetWriteSize<TValue>();
		}

		public void Write(global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter writer)
		{
			writer.TryBeginWriteValue<TValue>(Value);
			writer.WriteValue<TValue>(Value);
		}

		public void Read(global::Unity.Multiplayer.Tools.NetStats.FastBufferReader reader)
		{
			reader.TryBeginReadValue<TValue>(default(TValue));
			reader.ReadValue(out TValue value);
			Value = value;
		}

		public void Reset()
		{
			Value = DefaultValue;
		}
	}
}

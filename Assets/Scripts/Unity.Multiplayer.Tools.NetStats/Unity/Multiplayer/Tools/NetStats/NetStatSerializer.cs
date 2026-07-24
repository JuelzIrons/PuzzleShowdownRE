namespace Unity.Multiplayer.Tools.NetStats
{
	internal class NetStatSerializer : global::Unity.Multiplayer.Tools.NetStats.INetStatSerializer
	{
		private global::Unity.Multiplayer.Tools.NetStats.MetricFactory m_MetricFactory = new global::Unity.Multiplayer.Tools.NetStats.MetricFactory();

		public global::Unity.Collections.NativeArray<byte> Serialize(global::Unity.Multiplayer.Tools.NetStats.MetricCollection metricCollection)
		{
			int num = 0;
			for (int i = 0; i < metricCollection.Metrics.Count; i++)
			{
				global::Unity.Multiplayer.Tools.NetStats.IMetric metric = metricCollection.Metrics[i];
				num += global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.GetWriteSize<global::Unity.Multiplayer.Tools.NetStats.MetricHeader>();
				num += metric.GetWriteSize();
			}
			num += global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter.GetWriteSize<ulong>();
			using global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter writer = new global::Unity.Multiplayer.Tools.NetStats.FastBufferWriter(num, global::Unity.Collections.Allocator.Temp, int.MaxValue);
			writer.WriteValueSafe<ulong>(metricCollection.ConnectionId);
			writer.WriteValueSafe<int>(metricCollection.Metrics.Count);
			for (int j = 0; j < metricCollection.Metrics.Count; j++)
			{
				global::Unity.Multiplayer.Tools.NetStats.IMetric metric2 = metricCollection.Metrics[j];
				writer.WriteValueSafe<global::Unity.Multiplayer.Tools.NetStats.MetricHeader>(new global::Unity.Multiplayer.Tools.NetStats.MetricHeader(metric2.FactoryTypeName, metric2.MetricContainerType, metric2.Id));
				writer.TryBeginWrite(metric2.GetWriteSize());
				metric2.Write(writer);
			}
			return writer.ToNativeArray(global::Unity.Collections.Allocator.Temp);
		}

		public global::Unity.Multiplayer.Tools.NetStats.MetricCollection Deserialize(global::Unity.Collections.NativeArray<byte> bytes)
		{
			global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric> list = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Tools.NetStats.IMetric>();
			ulong value;
			using (global::Unity.Multiplayer.Tools.NetStats.FastBufferReader reader = new global::Unity.Multiplayer.Tools.NetStats.FastBufferReader(bytes, global::Unity.Collections.Allocator.Temp))
			{
				reader.ReadValueSafe(out value);
				reader.ReadValueSafe(out int value2);
				for (int i = 0; i < value2; i++)
				{
					reader.ReadValueSafe(out global::Unity.Multiplayer.Tools.NetStats.MetricHeader value3);
					if (m_MetricFactory.TryConstruct(value3, out var metric))
					{
						metric.Read(reader);
						list.Add(metric);
						continue;
					}
					throw new global::System.InvalidOperationException($"Failed to construct metric from serialized data. Metric Header: {value3}");
				}
			}
			return new global::Unity.Multiplayer.Tools.NetStats.MetricCollection(list, value);
		}
	}
}

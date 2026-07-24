namespace Unity.Multiplayer.Tools.NetStats
{
	[global::System.Serializable]
	public struct MetricId : global::System.IEquatable<global::Unity.Multiplayer.Tools.NetStats.MetricId>
	{
		[field: global::UnityEngine.SerializeField]
		internal int TypeIndex { get; set; }

		[field: global::UnityEngine.SerializeField]
		internal int EnumValue { get; set; }

		internal global::System.Type EnumType => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetType(TypeIndex);

		[global::JetBrains.Annotations.NotNull]
		internal string Name => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumName(TypeIndex, EnumValue);

		[global::JetBrains.Annotations.NotNull]
		internal string DisplayName => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumDisplayName(TypeIndex, EnumValue);

		internal global::Unity.Multiplayer.Tools.NetStats.MetricKind MetricKind => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumMetricKind(TypeIndex, EnumValue);

		internal global::Unity.Multiplayer.Tools.NetStats.BaseUnits Units => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetEnumUnit(TypeIndex, EnumValue);

		internal bool DisplayAsPercentage => global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetDisplayAsPercentage(TypeIndex, EnumValue);

		internal MetricId(int typeIndex, int enumValue)
		{
			if (!global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.IsValidTypeIndex(typeIndex))
			{
				throw new global::System.ArgumentOutOfRangeException(string.Format("Cannot construct {0} with out-of-range {1} {2}.", "MetricId", "TypeIndex", typeIndex));
			}
			TypeIndex = typeIndex;
			EnumValue = enumValue;
		}

		internal MetricId(global::System.Type enumType, int enumValue)
		{
			TypeIndex = global::Unity.Multiplayer.Tools.NetStats.MetricIdTypeLibrary.GetTypeIndex(enumType);
			EnumValue = enumValue;
		}

		public static global::Unity.Multiplayer.Tools.NetStats.MetricId Create<T>(T value) where T : unmanaged, global::System.Enum
		{
			global::System.Type typeFromHandle = typeof(T);
			int enumValue = global::Unity.Multiplayer.Tools.Common.CheckedEnumUtils<T, int>.CheckedCastToUnderlying(value);
			return new global::Unity.Multiplayer.Tools.NetStats.MetricId(typeFromHandle, enumValue);
		}

		public bool Equals(global::Unity.Multiplayer.Tools.NetStats.MetricId other)
		{
			if (TypeIndex == other.TypeIndex)
			{
				return EnumValue == other.EnumValue;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((global::Unity.Multiplayer.Tools.NetStats.MetricId)obj);
		}

		public override int GetHashCode()
		{
			return global::System.HashCode.Combine(TypeIndex, EnumValue);
		}

		public override string ToString()
		{
			return Name;
		}

		public static implicit operator string(global::Unity.Multiplayer.Tools.NetStats.MetricId metricId)
		{
			return metricId.ToString();
		}
	}
}

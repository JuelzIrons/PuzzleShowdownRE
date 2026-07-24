namespace Unity.Multiplayer.Tools.MetricTypes
{
	[global::System.Serializable]
	internal struct NetworkObjectIdentifier
	{
		public global::Unity.Collections.FixedString64Bytes Name { get; }

		public ulong NetworkId { get; }

		public ulong TreeViewId
		{
			get
			{
				long num = NetworkId.GetHashCode();
				ulong num2 = (ulong)Name.GetHashCode();
				return (ulong)num + num2;
			}
		}

		public NetworkObjectIdentifier(string name, ulong networkId)
			: this(global::Unity.Multiplayer.Tools.MetricTypes.StringConversionUtility.ConvertToFixedString(name), networkId)
		{
		}

		public NetworkObjectIdentifier(global::Unity.Collections.FixedString64Bytes name, ulong networkId)
		{
			Name = name;
			NetworkId = networkId;
		}
	}
}

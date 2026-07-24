namespace Unity.Multiplayer.Tools.NetVis.Configuration
{
	internal class BandwidthSettings
	{
		private static readonly global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset k_FirstGradientPreset = global::System.Linq.Enumerable.First(global::Unity.Multiplayer.Tools.Common.EnumUtil.GetValuesAndNames<global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset>(default(global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradientPreset))).value;

		private bool m_HasNoData;

		public float SmoothingHalfLife = 1f;

		public global::Unity.Multiplayer.Tools.Common.BandwidthTypes BandwidthType = global::Unity.Multiplayer.Tools.Common.BandwidthTypes.All;

		public global::Unity.Multiplayer.Tools.Common.NetworkDirection NetworkDirection = global::Unity.Multiplayer.Tools.Common.NetworkDirection.SentAndReceived;

		public global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradient MeshShadingFill = new global::Unity.Multiplayer.Tools.NetVis.Configuration.MeshShadingGradient
		{
			Preset = k_FirstGradientPreset,
			Gradient = k_FirstGradientPreset.ToGradient()
		};

		public bool BandwidthAutoscaling = true;

		public int BandwidthMin;

		public int BandwidthMax = 512;

		public bool MeshShadingEnabled { get; set; } = true;

		public bool TextOverlayEnabled { get; set; } = true;

		public bool HasNoData
		{
			get
			{
				return m_HasNoData;
			}
			set
			{
				if (m_HasNoData != value)
				{
					m_HasNoData = value;
					this.OnNoDataWarningChanged?.Invoke(m_HasNoData);
				}
			}
		}

		public int BandwidthMaxSafe
		{
			get
			{
				if (BandwidthMin != BandwidthMax)
				{
					return BandwidthMax;
				}
				return BandwidthMin + 1;
			}
		}

		public event global::System.Action<bool> OnNoDataWarningChanged;
	}
}

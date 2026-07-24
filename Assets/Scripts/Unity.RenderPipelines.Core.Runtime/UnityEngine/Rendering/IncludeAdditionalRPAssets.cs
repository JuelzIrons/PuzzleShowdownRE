namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(new global::System.Type[] { })]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "H: RP Assets Inclusion", Order = 990)]
	[global::UnityEngine.HideInInspector]
	public class IncludeAdditionalRPAssets : global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		private enum Version
		{
			Initial = 0,
			Count = 1,
			Last = 0
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.IncludeAdditionalRPAssets.Version m_version;

		[global::UnityEngine.SerializeField]
		private bool m_IncludeReferencedInScenes;

		[global::UnityEngine.SerializeField]
		private bool m_IncludeAssetsByLabel;

		[global::UnityEngine.SerializeField]
		private string m_LabelToInclude;

		int global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.version => (int)m_version;

		public bool includeReferencedInScenes
		{
			get
			{
				return m_IncludeReferencedInScenes;
			}
			set
			{
				this.SetValueAndNotify(ref m_IncludeReferencedInScenes, value, "m_IncludeReferencedInScenes");
			}
		}

		public bool includeAssetsByLabel
		{
			get
			{
				return m_IncludeAssetsByLabel;
			}
			set
			{
				this.SetValueAndNotify(ref m_IncludeAssetsByLabel, value, "m_IncludeAssetsByLabel");
			}
		}

		public string labelToInclude
		{
			get
			{
				return m_LabelToInclude;
			}
			set
			{
				this.SetValueAndNotify(ref m_LabelToInclude, value, "m_LabelToInclude");
			}
		}
	}
}

namespace UnityEngine.UI
{
	[global::System.Serializable]
	public class AnimationTriggers
	{
		private const string kDefaultNormalAnimName = "Normal";

		private const string kDefaultHighlightedAnimName = "Highlighted";

		private const string kDefaultPressedAnimName = "Pressed";

		private const string kDefaultSelectedAnimName = "Selected";

		private const string kDefaultDisabledAnimName = "Disabled";

		[global::UnityEngine.Serialization.FormerlySerializedAs("normalTrigger")]
		[global::UnityEngine.SerializeField]
		private string m_NormalTrigger = "Normal";

		[global::UnityEngine.Serialization.FormerlySerializedAs("highlightedTrigger")]
		[global::UnityEngine.SerializeField]
		private string m_HighlightedTrigger = "Highlighted";

		[global::UnityEngine.Serialization.FormerlySerializedAs("pressedTrigger")]
		[global::UnityEngine.SerializeField]
		private string m_PressedTrigger = "Pressed";

		[global::UnityEngine.Serialization.FormerlySerializedAs("m_HighlightedTrigger")]
		[global::UnityEngine.SerializeField]
		private string m_SelectedTrigger = "Selected";

		[global::UnityEngine.Serialization.FormerlySerializedAs("disabledTrigger")]
		[global::UnityEngine.SerializeField]
		private string m_DisabledTrigger = "Disabled";

		public string normalTrigger
		{
			get
			{
				return m_NormalTrigger;
			}
			set
			{
				m_NormalTrigger = value;
			}
		}

		public string highlightedTrigger
		{
			get
			{
				return m_HighlightedTrigger;
			}
			set
			{
				m_HighlightedTrigger = value;
			}
		}

		public string pressedTrigger
		{
			get
			{
				return m_PressedTrigger;
			}
			set
			{
				m_PressedTrigger = value;
			}
		}

		public string selectedTrigger
		{
			get
			{
				return m_SelectedTrigger;
			}
			set
			{
				m_SelectedTrigger = value;
			}
		}

		public string disabledTrigger
		{
			get
			{
				return m_DisabledTrigger;
			}
			set
			{
				m_DisabledTrigger = value;
			}
		}
	}
}

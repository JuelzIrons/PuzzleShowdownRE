namespace Unity.Multiplayer.Widgets
{
	internal class CreateSession : global::Unity.Multiplayer.Widgets.EnterSessionBase
	{
		private global::TMPro.TMP_InputField m_InputField;

		protected override void OnEnable()
		{
			m_InputField = GetComponentInChildren<global::TMPro.TMP_InputField>();
			base.OnEnable();
		}

		public override void OnServicesInitialized()
		{
			m_InputField.onEndEdit.AddListener(delegate(string value)
			{
				if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Return) && !string.IsNullOrEmpty(value))
				{
					EnterSession();
				}
			});
			m_InputField.onValueChanged.AddListener(delegate(string value)
			{
				m_EnterSessionButton.interactable = !string.IsNullOrEmpty(value) && base.Session == null;
			});
		}

		protected override global::Unity.Multiplayer.Widgets.EnterSessionData GetSessionData()
		{
			return new global::Unity.Multiplayer.Widgets.EnterSessionData
			{
				SessionAction = global::Unity.Multiplayer.Widgets.SessionAction.Create,
				SessionName = m_InputField.text,
				WidgetConfiguration = WidgetConfiguration
			};
		}
	}
}

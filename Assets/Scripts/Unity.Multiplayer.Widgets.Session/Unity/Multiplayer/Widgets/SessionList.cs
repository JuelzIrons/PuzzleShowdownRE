namespace Unity.Multiplayer.Widgets
{
	internal class SessionList : global::Unity.Multiplayer.Widgets.EnterSessionBase
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.GameObject m_SessionItemPrefab;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.GameObject m_ContentParent;

		private global::System.Collections.Generic.IList<global::UnityEngine.GameObject> m_ListItems = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		private global::System.Collections.Generic.IList<global::Unity.Services.Multiplayer.ISessionInfo> m_Sessions;

		private global::Unity.Services.Multiplayer.ISessionInfo m_SelectedSessionInfo;

		public override void OnServicesInitialized()
		{
			RefreshSessionList();
		}

		internal async void RefreshSessionList()
		{
			await UpdateSessions();
			foreach (global::UnityEngine.GameObject listItem in m_ListItems)
			{
				global::UnityEngine.Object.Destroy(listItem);
			}
			if (m_Sessions == null)
			{
				return;
			}
			foreach (global::Unity.Services.Multiplayer.ISessionInfo session in m_Sessions)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(m_SessionItemPrefab, m_ContentParent.transform);
				if (gameObject.TryGetComponent<global::Unity.Multiplayer.Widgets.SessionItem>(out var component))
				{
					component.SetSession(session);
					component.OnSessionSelected.AddListener(SelectSession);
				}
				m_ListItems.Add(gameObject);
			}
		}

		private void SelectSession(global::Unity.Services.Multiplayer.ISessionInfo sessionInfo)
		{
			m_SelectedSessionInfo = sessionInfo;
			if (base.Session == null)
			{
				m_EnterSessionButton.interactable = true;
			}
		}

		private async global::System.Threading.Tasks.Task UpdateSessions()
		{
			m_Sessions = await global::Unity.Multiplayer.Widgets.LazySingleton<global::Unity.Multiplayer.Widgets.SessionManager>.Instance.QuerySessions();
		}

		protected override global::Unity.Multiplayer.Widgets.EnterSessionData GetSessionData()
		{
			return new global::Unity.Multiplayer.Widgets.EnterSessionData
			{
				SessionAction = global::Unity.Multiplayer.Widgets.SessionAction.JoinById,
				Id = m_SelectedSessionInfo.Id,
				WidgetConfiguration = WidgetConfiguration
			};
		}
	}
}

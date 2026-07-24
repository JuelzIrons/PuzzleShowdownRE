namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Button))]
	internal class QuickJoinSession : global::Unity.Multiplayer.Widgets.EnterSessionBase
	{
		[global::UnityEngine.Header("Quick Join Options")]
		[global::UnityEngine.Tooltip("If true, the widget will automatically create a session if one does not exist. If false, the widget will only attempt to join existing sessions.")]
		[global::UnityEngine.SerializeField]
		private bool m_AutoCreateSession = true;

		protected override global::Unity.Multiplayer.Widgets.EnterSessionData GetSessionData()
		{
			return new global::Unity.Multiplayer.Widgets.EnterSessionData
			{
				SessionAction = global::Unity.Multiplayer.Widgets.SessionAction.QuickJoin,
				WidgetConfiguration = WidgetConfiguration,
				AdditionalOptions = new global::Unity.Multiplayer.Widgets.AdditionalOptions
				{
					AutoCreateSession = m_AutoCreateSession
				}
			};
		}
	}
}

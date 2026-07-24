namespace Unity.Multiplayer.Widgets
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.UI.Button))]
	internal class JoinSessionWithMatchmaker : global::Unity.Multiplayer.Widgets.EnterSessionBase
	{
		[global::UnityEngine.Header("Matchmaker Options")]
		[global::UnityEngine.Tooltip("The user will initiate the Matchmaking in this Queue.")]
		[global::UnityEngine.SerializeField]
		private string m_QueueName = "default";

		protected override global::Unity.Multiplayer.Widgets.EnterSessionData GetSessionData()
		{
			return new global::Unity.Multiplayer.Widgets.EnterSessionData
			{
				SessionAction = global::Unity.Multiplayer.Widgets.SessionAction.StartMatchmaking,
				WidgetConfiguration = WidgetConfiguration,
				AdditionalOptions = new global::Unity.Multiplayer.Widgets.AdditionalOptions
				{
					MatchmakerOptions = new global::Unity.Services.Multiplayer.MatchmakerOptions
					{
						QueueName = m_QueueName
					}
				}
			};
		}
	}
}

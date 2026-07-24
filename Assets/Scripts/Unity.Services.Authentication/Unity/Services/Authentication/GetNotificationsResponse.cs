namespace Unity.Services.Authentication
{
	[global::System.Serializable]
	internal class GetNotificationsResponse
	{
		[global::Newtonsoft.Json.JsonProperty("notifications")]
		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.NotificationResponse> Notifications;

		[global::UnityEngine.Scripting.Preserve]
		public GetNotificationsResponse()
		{
		}

		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> ToNotificationList()
		{
			if (Notifications.Count < 1)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification> list = new global::System.Collections.Generic.List<global::Unity.Services.Authentication.Notification>();
			foreach (global::Unity.Services.Authentication.NotificationResponse notification in Notifications)
			{
				list.Add(new global::Unity.Services.Authentication.Notification
				{
					Id = notification.Id,
					CaseId = notification.CaseId,
					CreatedAt = notification.CreatedAt,
					Message = notification.Message,
					PlayerId = notification.PlayerId,
					ProjectId = notification.ProjectId,
					Type = notification.Type
				});
			}
			return list;
		}
	}
}

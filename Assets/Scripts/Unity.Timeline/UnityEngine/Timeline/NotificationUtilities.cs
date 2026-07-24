namespace UnityEngine.Timeline
{
	internal static class NotificationUtilities
	{
		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> CreateNotificationsPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.IMarker> markers, global::UnityEngine.Playables.PlayableDirector director)
		{
			return CreateNotificationsPlayable(graph, markers, null, director);
		}

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> CreateNotificationsPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.IMarker> markers, global::UnityEngine.Timeline.TimelineAsset timelineAsset)
		{
			return CreateNotificationsPlayable(graph, markers, timelineAsset, null);
		}

		private static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> CreateNotificationsPlayable(global::UnityEngine.Playables.PlayableGraph graph, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.IMarker> markers, global::UnityEngine.Playables.IPlayableAsset asset, global::UnityEngine.Playables.PlayableDirector director)
		{
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> result = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour>.Null;
			global::UnityEngine.Playables.DirectorWrapMode loopMode = ((director != null) ? director.extrapolationMode : global::UnityEngine.Playables.DirectorWrapMode.None);
			bool flag = false;
			double num = 0.0;
			foreach (global::UnityEngine.Timeline.IMarker marker in markers)
			{
				if (marker is global::UnityEngine.Playables.INotification payload)
				{
					if (!flag)
					{
						num = ((director != null) ? director.playableAsset.duration : asset.duration);
						flag = true;
					}
					if (result.Equals(global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour>.Null))
					{
						result = global::UnityEngine.Timeline.TimeNotificationBehaviour.Create(graph, num, loopMode);
					}
					global::UnityEngine.Timeline.DiscreteTime discreteTime = (global::UnityEngine.Timeline.DiscreteTime)marker.time;
					global::UnityEngine.Timeline.DiscreteTime discreteTime2 = (global::UnityEngine.Timeline.DiscreteTime)num;
					if (discreteTime >= discreteTime2 && discreteTime <= discreteTime2.OneTickAfter() && discreteTime2 != 0)
					{
						discreteTime = discreteTime2.OneTickBefore();
					}
					if (marker is global::UnityEngine.Timeline.INotificationOptionProvider notificationOptionProvider)
					{
						result.GetBehaviour().AddNotification((double)discreteTime, payload, notificationOptionProvider.flags);
					}
					else
					{
						result.GetBehaviour().AddNotification((double)discreteTime, payload);
					}
				}
			}
			return result;
		}

		public static bool TrackTypeSupportsNotifications(global::System.Type type)
		{
			global::UnityEngine.Timeline.TrackBindingTypeAttribute trackBindingTypeAttribute = (global::UnityEngine.Timeline.TrackBindingTypeAttribute)global::System.Attribute.GetCustomAttribute(type, typeof(global::UnityEngine.Timeline.TrackBindingTypeAttribute));
			if (trackBindingTypeAttribute != null)
			{
				if (!typeof(global::UnityEngine.Component).IsAssignableFrom(trackBindingTypeAttribute.type))
				{
					return typeof(global::UnityEngine.GameObject).IsAssignableFrom(trackBindingTypeAttribute.type);
				}
				return true;
			}
			return false;
		}
	}
}

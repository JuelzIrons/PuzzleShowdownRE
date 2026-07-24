namespace UnityEngine.Timeline
{
	public class TimeNotificationBehaviour : global::UnityEngine.Playables.PlayableBehaviour
	{
		private struct NotificationEntry
		{
			public double time;

			public global::UnityEngine.Playables.INotification payload;

			public bool notificationFired;

			public global::UnityEngine.Timeline.NotificationFlags flags;

			public bool triggerInEditor => (flags & global::UnityEngine.Timeline.NotificationFlags.TriggerInEditMode) != 0;

			public bool prewarm => (flags & global::UnityEngine.Timeline.NotificationFlags.Retroactive) != 0;

			public bool triggerOnce => (flags & global::UnityEngine.Timeline.NotificationFlags.TriggerOnce) != 0;
		}

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry> m_Notifications = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry>();

		private double m_PreviousTime;

		private bool m_NeedSortNotifications;

		private global::UnityEngine.Playables.Playable m_TimeSource;

		public global::UnityEngine.Playables.Playable timeSource
		{
			set
			{
				m_TimeSource = value;
			}
		}

		public static global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> Create(global::UnityEngine.Playables.PlayableGraph graph, double duration, global::UnityEngine.Playables.DirectorWrapMode loopMode)
		{
			global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour> scriptPlayable = global::UnityEngine.Playables.ScriptPlayable<global::UnityEngine.Timeline.TimeNotificationBehaviour>.Create(graph);
			global::UnityEngine.Playables.PlayableExtensions.SetDuration(scriptPlayable, duration);
			global::UnityEngine.Playables.PlayableExtensions.SetTimeWrapMode(scriptPlayable, loopMode);
			global::UnityEngine.Playables.PlayableExtensions.SetPropagateSetTime(scriptPlayable, value: true);
			return scriptPlayable;
		}

		public void AddNotification(double time, global::UnityEngine.Playables.INotification payload, global::UnityEngine.Timeline.NotificationFlags flags = global::UnityEngine.Timeline.NotificationFlags.Retroactive)
		{
			m_Notifications.Add(new global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry
			{
				time = time,
				payload = payload,
				flags = flags
			});
			m_NeedSortNotifications = true;
		}

		public override void OnGraphStart(global::UnityEngine.Playables.Playable playable)
		{
			SortNotifications();
			double time = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
			for (int i = 0; i < m_Notifications.Count; i++)
			{
				if (m_Notifications[i].time > time && !m_Notifications[i].triggerOnce)
				{
					global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry value = m_Notifications[i];
					value.notificationFired = false;
					m_Notifications[i] = value;
				}
			}
			m_PreviousTime = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
		}

		public override void OnBehaviourPause(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (!global::UnityEngine.Playables.PlayableExtensions.IsDone(playable))
			{
				return;
			}
			SortNotifications();
			for (int i = 0; i < m_Notifications.Count; i++)
			{
				global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e = m_Notifications[i];
				if (!e.notificationFired)
				{
					double duration = global::UnityEngine.Playables.PlayableExtensions.GetDuration(playable);
					if (m_PreviousTime <= e.time && e.time <= duration)
					{
						Trigger_internal(playable, info.output, ref e);
						m_Notifications[i] = e;
					}
				}
			}
		}

		public override void PrepareFrame(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.FrameData info)
		{
			if (info.evaluationType == global::UnityEngine.Playables.FrameData.EvaluationType.Evaluate)
			{
				return;
			}
			SyncDurationWithExternalSource(playable);
			SortNotifications();
			double time = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
			if (info.timeLooped)
			{
				double duration = global::UnityEngine.Playables.PlayableExtensions.GetDuration(playable);
				TriggerNotificationsInRange(m_PreviousTime, duration, info, playable, checkState: true);
				double num = global::UnityEngine.Playables.PlayableExtensions.GetDuration(playable) - m_PreviousTime;
				int num2 = (int)(((double)(info.deltaTime * info.effectiveSpeed) - num) / global::UnityEngine.Playables.PlayableExtensions.GetDuration(playable));
				for (int i = 0; i < num2; i++)
				{
					TriggerNotificationsInRange(0.0, duration, info, playable, checkState: false);
				}
				TriggerNotificationsInRange(0.0, time, info, playable, checkState: false);
			}
			else
			{
				double time2 = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
				TriggerNotificationsInRange(m_PreviousTime, time2, info, playable, checkState: true);
			}
			for (int j = 0; j < m_Notifications.Count; j++)
			{
				global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e = m_Notifications[j];
				if (e.notificationFired && CanRestoreNotification(e, info, time, m_PreviousTime))
				{
					Restore_internal(ref e);
					m_Notifications[j] = e;
				}
			}
			m_PreviousTime = global::UnityEngine.Playables.PlayableExtensions.GetTime(playable);
		}

		private void SortNotifications()
		{
			if (m_NeedSortNotifications)
			{
				m_Notifications.Sort((global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry x, global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry y) => x.time.CompareTo(y.time));
				m_NeedSortNotifications = false;
			}
		}

		private static bool CanRestoreNotification(global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e, global::UnityEngine.Playables.FrameData info, double currentTime, double previousTime)
		{
			if (e.triggerOnce)
			{
				return false;
			}
			if (info.timeLooped)
			{
				return true;
			}
			if (previousTime > currentTime)
			{
				return currentTime <= e.time;
			}
			return false;
		}

		private void TriggerNotificationsInRange(double start, double end, global::UnityEngine.Playables.FrameData info, global::UnityEngine.Playables.Playable playable, bool checkState)
		{
			if (!(start <= end))
			{
				return;
			}
			bool isPlaying = global::UnityEngine.Application.isPlaying;
			for (int i = 0; i < m_Notifications.Count; i++)
			{
				global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e = m_Notifications[i];
				if (!e.notificationFired || (!checkState && !e.triggerOnce))
				{
					double time = e.time;
					if (e.prewarm && time < end && (e.triggerInEditor || isPlaying))
					{
						Trigger_internal(playable, info.output, ref e);
						m_Notifications[i] = e;
					}
					else if (!(time < start) && !(time > end) && (e.triggerInEditor || isPlaying))
					{
						Trigger_internal(playable, info.output, ref e);
						m_Notifications[i] = e;
					}
				}
			}
		}

		private void SyncDurationWithExternalSource(global::UnityEngine.Playables.Playable playable)
		{
			if (global::UnityEngine.Playables.PlayableExtensions.IsValid(m_TimeSource))
			{
				global::UnityEngine.Playables.PlayableExtensions.SetDuration(playable, global::UnityEngine.Playables.PlayableExtensions.GetDuration(m_TimeSource));
				global::UnityEngine.Playables.PlayableExtensions.SetTimeWrapMode(playable, global::UnityEngine.Playables.PlayableExtensions.GetTimeWrapMode(m_TimeSource));
			}
		}

		private static void Trigger_internal(global::UnityEngine.Playables.Playable playable, global::UnityEngine.Playables.PlayableOutput output, ref global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e)
		{
			global::UnityEngine.Playables.PlayableOutputExtensions.PushNotification(output, playable, e.payload);
			e.notificationFired = true;
		}

		private static void Restore_internal(ref global::UnityEngine.Timeline.TimeNotificationBehaviour.NotificationEntry e)
		{
			e.notificationFired = false;
		}
	}
}

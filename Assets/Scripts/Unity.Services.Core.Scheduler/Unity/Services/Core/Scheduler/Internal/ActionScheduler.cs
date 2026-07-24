namespace Unity.Services.Core.Scheduler.Internal
{
	internal class ActionScheduler : global::Unity.Services.Core.Scheduler.Internal.IActionScheduler, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private const long k_MinimumIdValue = 1L;

		internal readonly global::UnityEngine.LowLevel.PlayerLoopSystem SchedulerLoopSystem;

		private readonly global::Unity.Services.Core.Scheduler.Internal.ITimeProvider m_TimeProvider;

		private readonly object m_Lock = new object();

		private readonly global::Unity.Services.Core.Scheduler.Internal.MinimumBinaryHeap<global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation> m_ScheduledActions = new global::Unity.Services.Core.Scheduler.Internal.MinimumBinaryHeap<global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation>(new global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocationComparer());

		private readonly global::System.Collections.Generic.Dictionary<long, global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation> m_IdScheduledInvocationMap = new global::System.Collections.Generic.Dictionary<long, global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation>();

		private readonly global::System.Collections.Generic.List<global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation> m_ExpiredActions = new global::System.Collections.Generic.List<global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation>();

		private long m_NextId = 1L;

		public int ScheduledActionsCount => m_ScheduledActions.Count;

		public ActionScheduler()
			: this(new global::Unity.Services.Core.Scheduler.Internal.UtcTimeProvider())
		{
		}

		public ActionScheduler(global::Unity.Services.Core.Scheduler.Internal.ITimeProvider timeProvider)
		{
			m_TimeProvider = timeProvider;
			SchedulerLoopSystem = new global::UnityEngine.LowLevel.PlayerLoopSystem
			{
				type = typeof(global::Unity.Services.Core.Scheduler.Internal.ActionScheduler),
				updateDelegate = ExecuteExpiredActions
			};
		}

		public long ScheduleAction([global::JetBrains.Annotations.NotNull] global::System.Action action, double delaySeconds = 0.0)
		{
			if (delaySeconds < 0.0)
			{
				throw new global::System.ArgumentException("delaySeconds can not be negative");
			}
			if (action == null)
			{
				throw new global::System.ArgumentNullException("action");
			}
			lock (m_Lock)
			{
				global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation scheduledInvocation = new global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation
				{
					Action = action,
					InvocationTime = m_TimeProvider.Now.AddSeconds(delaySeconds),
					ActionId = m_NextId++
				};
				if (m_NextId < 1)
				{
					m_NextId = 1L;
				}
				m_ScheduledActions.Insert(scheduledInvocation);
				m_IdScheduledInvocationMap.Add(scheduledInvocation.ActionId, scheduledInvocation);
				return scheduledInvocation.ActionId;
			}
		}

		public void CancelAction(long actionId)
		{
			lock (m_Lock)
			{
				if (m_IdScheduledInvocationMap.TryGetValue(actionId, out var value))
				{
					m_ScheduledActions.Remove(value);
					m_IdScheduledInvocationMap.Remove(value.ActionId);
				}
			}
		}

		internal void ExecuteExpiredActions()
		{
			lock (m_Lock)
			{
				m_ExpiredActions.Clear();
				while (m_ScheduledActions.Count > 0 && m_ScheduledActions.Min?.InvocationTime <= m_TimeProvider.Now)
				{
					global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation scheduledInvocation = m_ScheduledActions.ExtractMin();
					m_ExpiredActions.Add(scheduledInvocation);
					m_ScheduledActions.Remove(scheduledInvocation);
					m_IdScheduledInvocationMap.Remove(scheduledInvocation.ActionId);
				}
				foreach (global::Unity.Services.Core.Scheduler.Internal.ScheduledInvocation expiredAction in m_ExpiredActions)
				{
					try
					{
						expiredAction.Action();
					}
					catch (global::System.Exception exception)
					{
						global::Unity.Services.Core.Internal.CoreLogger.LogException(exception);
					}
				}
			}
		}

		internal static void UpdateCurrentPlayerLoopWith(global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem> subSystemList, global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop)
		{
			currentPlayerLoop.subSystemList = subSystemList.ToArray();
			global::UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		public void JoinPlayerLoopSystem()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem> list = new global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem>(currentPlayerLoop.subSystemList);
			if (!list.Contains(SchedulerLoopSystem))
			{
				list.Add(SchedulerLoopSystem);
				UpdateCurrentPlayerLoopWith(list, currentPlayerLoop);
			}
		}

		public void QuitPlayerLoopSystem()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem> list = new global::System.Collections.Generic.List<global::UnityEngine.LowLevel.PlayerLoopSystem>(currentPlayerLoop.subSystemList);
			if (list.Remove(SchedulerLoopSystem))
			{
				UpdateCurrentPlayerLoopWith(list, currentPlayerLoop);
			}
		}
	}
}

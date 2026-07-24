namespace Unity.Netcode
{
	internal class AnticipationSystem
	{
		internal ulong LastAnticipationAck;

		internal double LastAnticipationAckTime;

		internal global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipatedObject> AllAnticipatedObjects = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipatedObject>();

		internal ulong AnticipationCounter;

		private global::Unity.Netcode.NetworkManager m_NetworkManager;

		public global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipatedObject> ObjectsToReanticipate = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipatedObject>();

		private global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipationEventReceiver> m_AnticipationEventReceivers = new global::System.Collections.Generic.HashSet<global::Unity.Netcode.IAnticipationEventReceiver>();

		public event global::Unity.Netcode.NetworkManager.ReanticipateDelegate OnReanticipate;

		public AnticipationSystem(global::Unity.Netcode.NetworkManager manager)
		{
			m_NetworkManager = manager;
		}

		public void RegisterForAnticipationEvents(global::Unity.Netcode.IAnticipationEventReceiver receiver)
		{
			m_AnticipationEventReceivers.Add(receiver);
		}

		public void DeregisterForAnticipationEvents(global::Unity.Netcode.IAnticipationEventReceiver receiver)
		{
			m_AnticipationEventReceivers.Remove(receiver);
		}

		public void SetupForUpdate()
		{
			foreach (global::Unity.Netcode.IAnticipationEventReceiver anticipationEventReceiver in m_AnticipationEventReceivers)
			{
				anticipationEventReceiver.SetupForUpdate();
			}
		}

		public void SetupForRender()
		{
			foreach (global::Unity.Netcode.IAnticipationEventReceiver anticipationEventReceiver in m_AnticipationEventReceivers)
			{
				anticipationEventReceiver.SetupForRender();
			}
		}

		public void ProcessReanticipation()
		{
			double lastRoundTripTime = m_NetworkManager.LocalTime.Time - LastAnticipationAckTime;
			foreach (global::Unity.Netcode.IAnticipatedObject item in ObjectsToReanticipate)
			{
				foreach (global::Unity.Netcode.NetworkBehaviour childNetworkBehaviour in item.OwnerObject.ChildNetworkBehaviours)
				{
					childNetworkBehaviour.OnReanticipate(lastRoundTripTime);
				}
				item.ResetAnticipation();
			}
			ObjectsToReanticipate.Clear();
			this.OnReanticipate?.Invoke(lastRoundTripTime);
		}

		public void Update()
		{
			foreach (global::Unity.Netcode.IAnticipatedObject allAnticipatedObject in AllAnticipatedObjects)
			{
				allAnticipatedObject.Update();
			}
		}

		public void Sync()
		{
			if (AllAnticipatedObjects.Count != 0 && !m_NetworkManager.ShutdownInProgress && !m_NetworkManager.ConnectionManager.LocalClient.IsServer && m_NetworkManager.ConnectionManager.LocalClient.IsConnected)
			{
				global::Unity.Netcode.AnticipationCounterSyncPingMessage message = new global::Unity.Netcode.AnticipationCounterSyncPingMessage
				{
					Counter = AnticipationCounter,
					Time = m_NetworkManager.LocalTime.Time
				};
				m_NetworkManager.MessageManager.SendMessage(ref message, global::Unity.Netcode.NetworkDelivery.Reliable, 0uL);
			}
			AnticipationCounter++;
		}
	}
}

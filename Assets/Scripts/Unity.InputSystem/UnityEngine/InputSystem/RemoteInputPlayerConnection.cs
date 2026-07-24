namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	internal class RemoteInputPlayerConnection : global::UnityEngine.ScriptableObject, global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>, global::System.IObservable<global::UnityEngine.InputSystem.InputRemoting.Message>
	{
		private class Subscriber : global::System.IDisposable
		{
			public global::UnityEngine.InputSystem.RemoteInputPlayerConnection owner;

			public global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message> observer;

			public void Dispose()
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Erase(ref owner.m_Subscribers, this);
			}
		}

		public static readonly global::System.Guid kNewDeviceMsg = new global::System.Guid("fcd9651ded40425995dfa6aeb78f1f1c");

		public static readonly global::System.Guid kNewLayoutMsg = new global::System.Guid("fccfec2b7369466d88502a9dd38505f4");

		public static readonly global::System.Guid kNewEventsMsg = new global::System.Guid("53546641df1347bc8aa315278a603586");

		public static readonly global::System.Guid kRemoveDeviceMsg = new global::System.Guid("e5e299b2d9e44255b8990bb71af8922d");

		public static readonly global::System.Guid kChangeUsagesMsg = new global::System.Guid("b9fe706dfc854d7ca109a5e38d7db730");

		public static readonly global::System.Guid kStartSendingMsg = new global::System.Guid("0d58e99045904672b3ef34b8797d23cb");

		public static readonly global::System.Guid kStopSendingMsg = new global::System.Guid("548716b2534a45369ab0c9323fc8b4a8");

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Networking.PlayerConnection.IEditorPlayerConnection m_Connection;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.RemoteInputPlayerConnection.Subscriber[] m_Subscribers;

		[global::UnityEngine.SerializeField]
		private int[] m_ConnectedIds;

		public void Bind(global::UnityEngine.Networking.PlayerConnection.IEditorPlayerConnection connection, bool isConnected)
		{
			if (m_Connection != null)
			{
				if (m_Connection != connection)
				{
					throw new global::System.InvalidOperationException("Already bound to an IEditorPlayerConnection");
				}
				return;
			}
			connection.RegisterConnection(OnConnected);
			connection.RegisterDisconnection(OnDisconnected);
			connection.Register(kNewDeviceMsg, OnNewDevice);
			connection.Register(kNewLayoutMsg, OnNewLayout);
			connection.Register(kNewEventsMsg, OnNewEvents);
			connection.Register(kRemoveDeviceMsg, OnRemoveDevice);
			connection.Register(kChangeUsagesMsg, OnChangeUsages);
			connection.Register(kStartSendingMsg, OnStartSending);
			connection.Register(kStopSendingMsg, OnStopSending);
			m_Connection = connection;
			if (isConnected)
			{
				OnConnected(0);
			}
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new global::System.ArgumentNullException("observer");
			}
			global::UnityEngine.InputSystem.RemoteInputPlayerConnection.Subscriber subscriber = new global::UnityEngine.InputSystem.RemoteInputPlayerConnection.Subscriber
			{
				owner = this,
				observer = observer
			};
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_Subscribers, subscriber);
			if (m_ConnectedIds != null)
			{
				int[] connectedIds = m_ConnectedIds;
				foreach (int participantId in connectedIds)
				{
					observer.OnNext(new global::UnityEngine.InputSystem.InputRemoting.Message
					{
						type = global::UnityEngine.InputSystem.InputRemoting.MessageType.Connect,
						participantId = participantId
					});
				}
			}
			return subscriber;
		}

		private void OnConnected(int id)
		{
			if (m_ConnectedIds == null || !global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Contains(m_ConnectedIds, id))
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_ConnectedIds, id);
				SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.Connect, new global::UnityEngine.Networking.PlayerConnection.MessageEventArgs
				{
					playerId = id
				});
			}
		}

		private void OnDisconnected(int id)
		{
			if (m_ConnectedIds != null && global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Contains(m_ConnectedIds, id))
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Erase(ref m_ConnectedIds, id);
				SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.Disconnect, new global::UnityEngine.Networking.PlayerConnection.MessageEventArgs
				{
					playerId = id
				});
			}
		}

		private void OnNewDevice(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.NewDevice, args);
		}

		private void OnNewLayout(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.NewLayout, args);
		}

		private void OnNewEvents(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.NewEvents, args);
		}

		private void OnRemoveDevice(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveDevice, args);
		}

		private void OnChangeUsages(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.ChangeUsages, args);
		}

		private void OnStartSending(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.StartSending, args);
		}

		private void OnStopSending(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType.StopSending, args);
		}

		private void SendToSubscribers(global::UnityEngine.InputSystem.InputRemoting.MessageType type, global::UnityEngine.Networking.PlayerConnection.MessageEventArgs args)
		{
			if (m_Subscribers != null)
			{
				global::UnityEngine.InputSystem.InputRemoting.Message value = new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					participantId = args.playerId,
					type = type,
					data = args.data
				};
				for (int i = 0; i < m_Subscribers.Length; i++)
				{
					m_Subscribers[i].observer.OnNext(value);
				}
			}
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnNext(global::UnityEngine.InputSystem.InputRemoting.Message msg)
		{
			if (m_Connection != null)
			{
				switch (msg.type)
				{
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewDevice:
					m_Connection.Send(kNewDeviceMsg, msg.data);
					break;
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewLayout:
					m_Connection.Send(kNewLayoutMsg, msg.data);
					break;
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewEvents:
					m_Connection.Send(kNewEventsMsg, msg.data);
					break;
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.ChangeUsages:
					m_Connection.Send(kChangeUsagesMsg, msg.data);
					break;
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveDevice:
					m_Connection.Send(kRemoveDeviceMsg, msg.data);
					break;
				case global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveLayout:
					break;
				}
			}
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnError(global::System.Exception error)
		{
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnCompleted()
		{
		}
	}
}

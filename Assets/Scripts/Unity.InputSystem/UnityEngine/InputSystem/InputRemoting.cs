namespace UnityEngine.InputSystem
{
	public sealed class InputRemoting : global::System.IObservable<global::UnityEngine.InputSystem.InputRemoting.Message>, global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>
	{
		public enum MessageType
		{
			Connect = 0,
			Disconnect = 1,
			NewLayout = 2,
			NewDevice = 3,
			NewEvents = 4,
			RemoveDevice = 5,
			RemoveLayout = 6,
			ChangeUsages = 7,
			StartSending = 8,
			StopSending = 9
		}

		public struct Message
		{
			public int participantId;

			public global::UnityEngine.InputSystem.InputRemoting.MessageType type;

			public byte[] data;
		}

		[global::System.Flags]
		private enum Flags
		{
			Sending = 1,
			StartSendingOnConnect = 2
		}

		[global::System.Serializable]
		internal struct RemoteSender
		{
			public int senderId;

			public global::UnityEngine.InputSystem.Utilities.InternedString[] layouts;

			public global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] devices;
		}

		[global::System.Serializable]
		internal struct RemoteInputDevice
		{
			public int remoteId;

			public int localId;

			public global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description;
		}

		internal class Subscriber : global::System.IDisposable
		{
			public global::UnityEngine.InputSystem.InputRemoting owner;

			public global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message> observer;

			public void Dispose()
			{
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Erase(ref owner.m_Subscribers, this);
			}
		}

		private static class ConnectMsg
		{
			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver)
			{
				if (receiver.sending)
				{
					receiver.SendInitialMessages();
				}
				else if ((receiver.m_Flags & global::UnityEngine.InputSystem.InputRemoting.Flags.StartSendingOnConnect) == global::UnityEngine.InputSystem.InputRemoting.Flags.StartSendingOnConnect)
				{
					receiver.StartSending();
				}
			}
		}

		private static class StartSendingMsg
		{
			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver)
			{
				receiver.StartSending();
			}
		}

		private static class StopSendingMsg
		{
			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver)
			{
				receiver.StopSending();
			}
		}

		private static class DisconnectMsg
		{
			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				global::UnityEngine.Debug.Log("DisconnectMsg.Process");
				receiver.RemoveRemoteDevices(msg.participantId);
				receiver.StopSending();
			}
		}

		private static class NewLayoutMsg
		{
			[global::System.Serializable]
			public struct Data
			{
				public string name;

				public string layoutJson;

				public bool isOverride;
			}

			public static global::UnityEngine.InputSystem.InputRemoting.Message? Create(global::UnityEngine.InputSystem.InputRemoting sender, string layoutName)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout;
				try
				{
					inputControlLayout = sender.m_LocalManager.TryLoadControlLayout(new global::UnityEngine.InputSystem.Utilities.InternedString(layoutName));
					if (inputControlLayout == null)
					{
						global::UnityEngine.Debug.Log($"Could not find layout '{layoutName}' meant to be sent through remote connection; this should not happen");
						return null;
					}
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.Log($"Could not load layout '{layoutName}'; not sending to remote listeners (exception: {arg})");
					return null;
				}
				global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Data data = new global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Data
				{
					name = layoutName,
					layoutJson = inputControlLayout.ToJson(),
					isOverride = inputControlLayout.isOverride
				};
				return new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					type = global::UnityEngine.InputSystem.InputRemoting.MessageType.NewLayout,
					data = SerializeData(data)
				};
			}

			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Data data = DeserializeData<global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Data>(msg.data);
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				global::UnityEngine.InputSystem.Utilities.InternedString value = new global::UnityEngine.InputSystem.Utilities.InternedString(data.name);
				receiver.m_LocalManager.RegisterControlLayout(data.layoutJson, data.name, data.isOverride);
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref receiver.m_Senders[num].layouts, value);
			}
		}

		private static class NewDeviceMsg
		{
			[global::System.Serializable]
			public struct Data
			{
				public string name;

				public string layout;

				public int deviceId;

				public string[] usages;

				public global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description;
			}

			public static global::UnityEngine.InputSystem.InputRemoting.Message Create(global::UnityEngine.InputSystem.InputDevice device)
			{
				global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Data data = new global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Data
				{
					name = device.name,
					layout = device.layout,
					deviceId = device.deviceId,
					description = device.description,
					usages = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(device.usages, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString()))
				};
				return new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					type = global::UnityEngine.InputSystem.InputRemoting.MessageType.NewDevice,
					data = SerializeData(data)
				};
			}

			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				int num = receiver.FindOrCreateSenderRecord(msg.participantId);
				global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Data data = DeserializeData<global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Data>(msg.data);
				global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] devices = receiver.m_Senders[num].devices;
				if (devices != null)
				{
					global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] array = devices;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].remoteId == data.deviceId)
						{
							global::UnityEngine.Debug.LogError(string.Format("Already received device with id {0} (layout '{1}', description '{3}) from remote {2}", data.deviceId, data.layout, msg.participantId, data.description));
							return;
						}
					}
				}
				global::UnityEngine.InputSystem.InputDevice inputDevice;
				try
				{
					global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(data.layout);
					inputDevice = receiver.m_LocalManager.AddDevice(internedString, data.name);
					inputDevice.m_ParticipantId = msg.participantId;
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogError($"Could not create remote device '{data.description}' with layout '{data.layout}' locally (exception: {arg})");
					return;
				}
				inputDevice.m_Description = data.description;
				inputDevice.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.Remote;
				string[] usages = data.usages;
				foreach (string text in usages)
				{
					receiver.m_LocalManager.AddDeviceUsage(inputDevice, new global::UnityEngine.InputSystem.Utilities.InternedString(text));
				}
				global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice value = new global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice
				{
					remoteId = data.deviceId,
					localId = inputDevice.deviceId,
					description = data.description
				};
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref receiver.m_Senders[num].devices, value);
			}
		}

		private static class NewEventsMsg
		{
			public unsafe static global::UnityEngine.InputSystem.InputRemoting.Message CreateResetEvent(global::UnityEngine.InputSystem.InputDevice device, bool isHardReset)
			{
				global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent output = global::UnityEngine.InputSystem.LowLevel.DeviceResetEvent.Create(device.deviceId, isHardReset);
				return Create((global::UnityEngine.InputSystem.LowLevel.InputEvent*)global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output), 1);
			}

			public unsafe static global::UnityEngine.InputSystem.InputRemoting.Message CreateStateEvent(global::UnityEngine.InputSystem.InputDevice device)
			{
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr;
				using (global::UnityEngine.InputSystem.LowLevel.StateEvent.From(device, out eventPtr))
				{
					return Create(eventPtr.data, 1);
				}
			}

			public unsafe static global::UnityEngine.InputSystem.InputRemoting.Message Create(global::UnityEngine.InputSystem.LowLevel.InputEvent* events, int eventCount)
			{
				uint num = 0u;
				global::UnityEngine.InputSystem.LowLevel.InputEventPtr inputEventPtr = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr(events);
				int num2 = 0;
				while (num2 < eventCount)
				{
					num = global::UnityEngine.InputSystem.Utilities.NumberHelpers.AlignToMultipleOf(num, 4u) + inputEventPtr.sizeInBytes;
					num2++;
					inputEventPtr = inputEventPtr.Next();
				}
				byte[] array = new byte[num];
				fixed (byte* destination = array)
				{
					global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, events, num);
				}
				return new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					type = global::UnityEngine.InputSystem.InputRemoting.MessageType.NewEvents,
					data = array
				};
			}

			public unsafe static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				global::UnityEngine.InputSystem.InputManager localManager = receiver.m_LocalManager;
				fixed (byte* data = msg.data)
				{
					global::System.IntPtr intPtr = new global::System.IntPtr(data + msg.data.Length);
					int num = 0;
					global::UnityEngine.InputSystem.LowLevel.InputEventPtr ptr = new global::UnityEngine.InputSystem.LowLevel.InputEventPtr((global::UnityEngine.InputSystem.LowLevel.InputEvent*)data);
					int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
					while (ptr.data < intPtr.ToPointer())
					{
						int deviceId = ptr.deviceId;
						if ((ptr.deviceId = receiver.FindLocalDeviceId(deviceId, senderIndex)) != 0)
						{
							localManager.QueueEvent(ptr);
						}
						num++;
						ptr = ptr.Next();
					}
				}
			}
		}

		private static class ChangeUsageMsg
		{
			[global::System.Serializable]
			public struct Data
			{
				public int deviceId;

				public string[] usages;
			}

			public static global::UnityEngine.InputSystem.InputRemoting.Message Create(global::UnityEngine.InputSystem.InputDevice device)
			{
				global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Data data = new global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Data
				{
					deviceId = device.deviceId,
					usages = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(device.usages, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString()))
				};
				return new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					type = global::UnityEngine.InputSystem.InputRemoting.MessageType.ChangeUsages,
					data = SerializeData(data)
				};
			}

			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Data data = DeserializeData<global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Data>(msg.data);
				global::UnityEngine.InputSystem.InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(data.deviceId, senderIndex);
				if (inputDevice == null)
				{
					return;
				}
				foreach (global::UnityEngine.InputSystem.Utilities.InternedString usage in inputDevice.usages)
				{
					if (!global::System.Linq.Enumerable.Contains(data.usages, usage))
					{
						receiver.m_LocalManager.RemoveDeviceUsage(inputDevice, new global::UnityEngine.InputSystem.Utilities.InternedString(usage));
					}
				}
				string[] usages = data.usages;
				foreach (string text in usages)
				{
					if (!global::UnityEngine.InputSystem.Utilities.ReadOnlyArrayExtensions.Contains(value: new global::UnityEngine.InputSystem.Utilities.InternedString(text), array: inputDevice.usages))
					{
						receiver.m_LocalManager.AddDeviceUsage(inputDevice, new global::UnityEngine.InputSystem.Utilities.InternedString(text));
					}
				}
			}
		}

		private static class RemoveDeviceMsg
		{
			public static global::UnityEngine.InputSystem.InputRemoting.Message Create(global::UnityEngine.InputSystem.InputDevice device)
			{
				return new global::UnityEngine.InputSystem.InputRemoting.Message
				{
					type = global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveDevice,
					data = global::System.BitConverter.GetBytes(device.deviceId)
				};
			}

			public static void Process(global::UnityEngine.InputSystem.InputRemoting receiver, global::UnityEngine.InputSystem.InputRemoting.Message msg)
			{
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				int remoteDeviceId = global::System.BitConverter.ToInt32(msg.data, 0);
				global::UnityEngine.InputSystem.InputDevice inputDevice = receiver.TryGetDeviceByRemoteId(remoteDeviceId, senderIndex);
				if (inputDevice != null)
				{
					receiver.m_LocalManager.RemoveDevice(inputDevice);
				}
			}
		}

		private global::UnityEngine.InputSystem.InputRemoting.Flags m_Flags;

		private global::UnityEngine.InputSystem.InputManager m_LocalManager;

		private global::UnityEngine.InputSystem.InputRemoting.Subscriber[] m_Subscribers;

		private global::UnityEngine.InputSystem.InputRemoting.RemoteSender[] m_Senders;

		public bool sending
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.InputRemoting.Flags.Sending) == global::UnityEngine.InputSystem.InputRemoting.Flags.Sending;
			}
			private set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.InputRemoting.Flags.Sending;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.InputRemoting.Flags.Sending;
				}
			}
		}

		internal global::UnityEngine.InputSystem.InputManager manager => m_LocalManager;

		internal InputRemoting(global::UnityEngine.InputSystem.InputManager manager, bool startSendingOnConnect = false)
		{
			if (manager == null)
			{
				throw new global::System.ArgumentNullException("manager");
			}
			m_LocalManager = manager;
			if (startSendingOnConnect)
			{
				m_Flags |= global::UnityEngine.InputSystem.InputRemoting.Flags.StartSendingOnConnect;
			}
		}

		public void StartSending()
		{
			if (!sending)
			{
				m_LocalManager.onEvent += SendEvent;
				m_LocalManager.onDeviceChange += SendDeviceChange;
				m_LocalManager.onLayoutChange += SendLayoutChange;
				sending = true;
				SendInitialMessages();
			}
		}

		public void StopSending()
		{
			if (sending)
			{
				m_LocalManager.onEvent -= SendEvent;
				m_LocalManager.onDeviceChange -= SendDeviceChange;
				m_LocalManager.onLayoutChange -= SendLayoutChange;
				sending = false;
			}
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnNext(global::UnityEngine.InputSystem.InputRemoting.Message msg)
		{
			switch (msg.type)
			{
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.Connect:
				global::UnityEngine.InputSystem.InputRemoting.ConnectMsg.Process(this);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.Disconnect:
				global::UnityEngine.InputSystem.InputRemoting.DisconnectMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewLayout:
				global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewDevice:
				global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.NewEvents:
				global::UnityEngine.InputSystem.InputRemoting.NewEventsMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.ChangeUsages:
				global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveDevice:
				global::UnityEngine.InputSystem.InputRemoting.RemoveDeviceMsg.Process(this, msg);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.StartSending:
				global::UnityEngine.InputSystem.InputRemoting.StartSendingMsg.Process(this);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.StopSending:
				global::UnityEngine.InputSystem.InputRemoting.StopSendingMsg.Process(this);
				break;
			case global::UnityEngine.InputSystem.InputRemoting.MessageType.RemoveLayout:
				break;
			}
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnError(global::System.Exception error)
		{
		}

		void global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message>.OnCompleted()
		{
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<global::UnityEngine.InputSystem.InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new global::System.ArgumentNullException("observer");
			}
			global::UnityEngine.InputSystem.InputRemoting.Subscriber subscriber = new global::UnityEngine.InputSystem.InputRemoting.Subscriber
			{
				owner = this,
				observer = observer
			};
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_Subscribers, subscriber);
			return subscriber;
		}

		private void SendInitialMessages()
		{
			SendAllGeneratedLayouts();
			SendAllDevices();
		}

		private void SendAllGeneratedLayouts()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout>> layoutBuilder in m_LocalManager.m_Layouts.layoutBuilders)
			{
				SendLayout(layoutBuilder.Key);
			}
		}

		private void SendLayout(string layoutName)
		{
			if (m_Subscribers != null)
			{
				global::UnityEngine.InputSystem.InputRemoting.Message? message = global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Create(this, layoutName);
				if (message.HasValue)
				{
					Send(message.Value);
				}
			}
		}

		private void SendAllDevices()
		{
			foreach (global::UnityEngine.InputSystem.InputDevice device in m_LocalManager.devices)
			{
				SendDevice(device);
			}
		}

		private void SendDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_Subscribers != null && !device.remote)
			{
				global::UnityEngine.InputSystem.InputRemoting.Message msg = global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Create(device);
				Send(msg);
				global::UnityEngine.InputSystem.InputRemoting.Message msg2 = global::UnityEngine.InputSystem.InputRemoting.NewEventsMsg.CreateStateEvent(device);
				Send(msg2);
			}
		}

		private unsafe void SendEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, global::UnityEngine.InputSystem.InputDevice device)
		{
			if (m_Subscribers != null && (device == null || !device.remote))
			{
				global::UnityEngine.InputSystem.InputRemoting.Message msg = global::UnityEngine.InputSystem.InputRemoting.NewEventsMsg.Create(eventPtr.data, 1);
				Send(msg);
			}
		}

		private void SendDeviceChange(global::UnityEngine.InputSystem.InputDevice device, global::UnityEngine.InputSystem.InputDeviceChange change)
		{
			if (m_Subscribers != null && !device.remote)
			{
				global::UnityEngine.InputSystem.InputRemoting.Message msg;
				switch (change)
				{
				case global::UnityEngine.InputSystem.InputDeviceChange.Added:
					msg = global::UnityEngine.InputSystem.InputRemoting.NewDeviceMsg.Create(device);
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.Removed:
					msg = global::UnityEngine.InputSystem.InputRemoting.RemoveDeviceMsg.Create(device);
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.UsageChanged:
					msg = global::UnityEngine.InputSystem.InputRemoting.ChangeUsageMsg.Create(device);
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.SoftReset:
					msg = global::UnityEngine.InputSystem.InputRemoting.NewEventsMsg.CreateResetEvent(device, isHardReset: false);
					break;
				case global::UnityEngine.InputSystem.InputDeviceChange.HardReset:
					msg = global::UnityEngine.InputSystem.InputRemoting.NewEventsMsg.CreateResetEvent(device, isHardReset: true);
					break;
				default:
					return;
				}
				Send(msg);
			}
		}

		private void SendLayoutChange(string layout, global::UnityEngine.InputSystem.InputControlLayoutChange change)
		{
			if (m_Subscribers != null && m_LocalManager.m_Layouts.IsGeneratedLayout(new global::UnityEngine.InputSystem.Utilities.InternedString(layout)) && (change == global::UnityEngine.InputSystem.InputControlLayoutChange.Added || change == global::UnityEngine.InputSystem.InputControlLayoutChange.Replaced))
			{
				global::UnityEngine.InputSystem.InputRemoting.Message? message = global::UnityEngine.InputSystem.InputRemoting.NewLayoutMsg.Create(this, layout);
				if (message.HasValue)
				{
					Send(message.Value);
				}
			}
		}

		private void Send(global::UnityEngine.InputSystem.InputRemoting.Message msg)
		{
			global::UnityEngine.InputSystem.InputRemoting.Subscriber[] subscribers = m_Subscribers;
			for (int i = 0; i < subscribers.Length; i++)
			{
				subscribers[i].observer.OnNext(msg);
			}
		}

		private int FindOrCreateSenderRecord(int senderId)
		{
			if (m_Senders != null)
			{
				int num = m_Senders.Length;
				for (int i = 0; i < num; i++)
				{
					if (m_Senders[i].senderId == senderId)
					{
						return i;
					}
				}
			}
			global::UnityEngine.InputSystem.InputRemoting.RemoteSender value = new global::UnityEngine.InputSystem.InputRemoting.RemoteSender
			{
				senderId = senderId
			};
			return global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref m_Senders, value);
		}

		private static global::UnityEngine.InputSystem.Utilities.InternedString BuildLayoutNamespace(int senderId)
		{
			return new global::UnityEngine.InputSystem.Utilities.InternedString($"Remote::{senderId}");
		}

		private int FindLocalDeviceId(int remoteDeviceId, int senderIndex)
		{
			global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] devices = m_Senders[senderIndex].devices;
			if (devices != null)
			{
				int num = devices.Length;
				for (int i = 0; i < num; i++)
				{
					if (devices[i].remoteId == remoteDeviceId)
					{
						return devices[i].localId;
					}
				}
			}
			return 0;
		}

		private global::UnityEngine.InputSystem.InputDevice TryGetDeviceByRemoteId(int remoteDeviceId, int senderIndex)
		{
			int id = FindLocalDeviceId(remoteDeviceId, senderIndex);
			return m_LocalManager.TryGetDeviceById(id);
		}

		public void RemoveRemoteDevices(int participantId)
		{
			int num = FindOrCreateSenderRecord(participantId);
			global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] devices = m_Senders[num].devices;
			if (devices != null)
			{
				global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice[] array = devices;
				for (int i = 0; i < array.Length; i++)
				{
					global::UnityEngine.InputSystem.InputRemoting.RemoteInputDevice remoteInputDevice = array[i];
					global::UnityEngine.InputSystem.InputDevice inputDevice = m_LocalManager.TryGetDeviceById(remoteInputDevice.localId);
					if (inputDevice != null)
					{
						m_LocalManager.RemoveDevice(inputDevice);
					}
				}
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.EraseAt(ref m_Senders, num);
		}

		private static byte[] SerializeData<TData>(TData data)
		{
			string s = global::UnityEngine.JsonUtility.ToJson(data);
			return global::System.Text.Encoding.UTF8.GetBytes(s);
		}

		private static TData DeserializeData<TData>(byte[] data)
		{
			return global::UnityEngine.JsonUtility.FromJson<TData>(global::System.Text.Encoding.UTF8.GetString(data));
		}
	}
}

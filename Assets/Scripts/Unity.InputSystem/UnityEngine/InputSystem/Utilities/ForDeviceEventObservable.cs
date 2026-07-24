namespace UnityEngine.InputSystem.Utilities
{
	internal class ForDeviceEventObservable : global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>
	{
		private class ForDevice : global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr>
		{
			private global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_Observer;

			private global::UnityEngine.InputSystem.InputDevice m_Device;

			private global::System.Type m_DeviceType;

			public ForDevice(global::System.Type deviceType, global::UnityEngine.InputSystem.InputDevice device, global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
			{
				m_Device = device;
				m_DeviceType = deviceType;
				m_Observer = observer;
			}

			public void OnCompleted()
			{
			}

			public void OnError(global::System.Exception error)
			{
				global::UnityEngine.Debug.LogException(error);
			}

			public void OnNext(global::UnityEngine.InputSystem.LowLevel.InputEventPtr value)
			{
				if (m_DeviceType != null)
				{
					global::UnityEngine.InputSystem.InputDevice deviceById = global::UnityEngine.InputSystem.InputSystem.GetDeviceById(value.deviceId);
					if (deviceById == null || !m_DeviceType.IsInstanceOfType(deviceById))
					{
						return;
					}
				}
				if (m_Device == null || value.deviceId == m_Device.deviceId)
				{
					m_Observer.OnNext(value);
				}
			}
		}

		private global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> m_Source;

		private global::UnityEngine.InputSystem.InputDevice m_Device;

		private global::System.Type m_DeviceType;

		public ForDeviceEventObservable(global::System.IObservable<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> source, global::System.Type deviceType, global::UnityEngine.InputSystem.InputDevice device)
		{
			m_Source = source;
			m_DeviceType = deviceType;
			m_Device = device;
		}

		public global::System.IDisposable Subscribe(global::System.IObserver<global::UnityEngine.InputSystem.LowLevel.InputEventPtr> observer)
		{
			return m_Source.Subscribe(new global::UnityEngine.InputSystem.Utilities.ForDeviceEventObservable.ForDevice(m_DeviceType, m_Device, observer));
		}
	}
}

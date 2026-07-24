namespace UnityEngine.Timeline
{
	public class SignalReceiver : global::UnityEngine.MonoBehaviour, global::UnityEngine.Playables.INotificationReceiver
	{
		[global::System.Serializable]
		private class EventKeyValue
		{
			[global::UnityEngine.SerializeField]
			private global::System.Collections.Generic.List<global::UnityEngine.Timeline.SignalAsset> m_Signals = new global::System.Collections.Generic.List<global::UnityEngine.Timeline.SignalAsset>();

			[global::UnityEngine.SerializeField]
			[global::UnityEngine.Timeline.CustomSignalEventDrawer]
			private global::System.Collections.Generic.List<global::UnityEngine.Events.UnityEvent> m_Events = new global::System.Collections.Generic.List<global::UnityEngine.Events.UnityEvent>();

			public global::System.Collections.Generic.List<global::UnityEngine.Timeline.SignalAsset> signals => m_Signals;

			public global::System.Collections.Generic.List<global::UnityEngine.Events.UnityEvent> events => m_Events;

			public bool TryGetValue(global::UnityEngine.Timeline.SignalAsset key, out global::UnityEngine.Events.UnityEvent value)
			{
				int num = m_Signals.IndexOf(key);
				if (num != -1)
				{
					value = m_Events[num];
					return true;
				}
				value = null;
				return false;
			}

			public void Append(global::UnityEngine.Timeline.SignalAsset key, global::UnityEngine.Events.UnityEvent value)
			{
				m_Signals.Add(key);
				m_Events.Add(value);
			}

			public void Remove(int idx)
			{
				if (idx != -1)
				{
					m_Signals.RemoveAt(idx);
					m_Events.RemoveAt(idx);
				}
			}

			public void Remove(global::UnityEngine.Timeline.SignalAsset key)
			{
				int num = m_Signals.IndexOf(key);
				if (num != -1)
				{
					m_Signals.RemoveAt(num);
					m_Events.RemoveAt(num);
				}
			}
		}

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.SignalReceiver.EventKeyValue m_Events = new global::UnityEngine.Timeline.SignalReceiver.EventKeyValue();

		public void OnNotify(global::UnityEngine.Playables.Playable origin, global::UnityEngine.Playables.INotification notification, object context)
		{
			global::UnityEngine.Timeline.SignalEmitter signalEmitter = notification as global::UnityEngine.Timeline.SignalEmitter;
			if (signalEmitter != null && signalEmitter.asset != null && m_Events.TryGetValue(signalEmitter.asset, out var value))
			{
				value?.Invoke();
			}
		}

		public void AddReaction(global::UnityEngine.Timeline.SignalAsset asset, global::UnityEngine.Events.UnityEvent reaction)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (m_Events.signals.Contains(asset))
			{
				throw new global::System.ArgumentException("SignalAsset already used.");
			}
			m_Events.Append(asset, reaction);
		}

		public int AddEmptyReaction(global::UnityEngine.Events.UnityEvent reaction)
		{
			m_Events.Append(null, reaction);
			return m_Events.events.Count - 1;
		}

		public void Remove(global::UnityEngine.Timeline.SignalAsset asset)
		{
			if (!m_Events.signals.Contains(asset))
			{
				throw new global::System.ArgumentException("The SignalAsset is not registered with this receiver.");
			}
			m_Events.Remove(asset);
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Timeline.SignalAsset> GetRegisteredSignals()
		{
			return m_Events.signals;
		}

		public global::UnityEngine.Events.UnityEvent GetReaction(global::UnityEngine.Timeline.SignalAsset key)
		{
			if (m_Events.TryGetValue(key, out var value))
			{
				return value;
			}
			return null;
		}

		public int Count()
		{
			return m_Events.signals.Count;
		}

		public void ChangeSignalAtIndex(int idx, global::UnityEngine.Timeline.SignalAsset newKey)
		{
			if (idx < 0 || idx > m_Events.signals.Count - 1)
			{
				throw new global::System.IndexOutOfRangeException();
			}
			if (!(m_Events.signals[idx] == newKey))
			{
				bool flag = m_Events.signals.Contains(newKey);
				if (newKey == null || m_Events.signals[idx] == null || !flag)
				{
					m_Events.signals[idx] = newKey;
				}
				if (newKey != null && flag)
				{
					throw new global::System.ArgumentException("SignalAsset already used.");
				}
			}
		}

		public void RemoveAtIndex(int idx)
		{
			if (idx < 0 || idx > m_Events.signals.Count - 1)
			{
				throw new global::System.IndexOutOfRangeException();
			}
			m_Events.Remove(idx);
		}

		public void ChangeReactionAtIndex(int idx, global::UnityEngine.Events.UnityEvent reaction)
		{
			if (idx < 0 || idx > m_Events.events.Count - 1)
			{
				throw new global::System.IndexOutOfRangeException();
			}
			m_Events.events[idx] = reaction;
		}

		public global::UnityEngine.Events.UnityEvent GetReactionAtIndex(int idx)
		{
			if (idx < 0 || idx > m_Events.events.Count - 1)
			{
				throw new global::System.IndexOutOfRangeException();
			}
			return m_Events.events[idx];
		}

		public global::UnityEngine.Timeline.SignalAsset GetSignalAssetAtIndex(int idx)
		{
			if (idx < 0 || idx > m_Events.signals.Count - 1)
			{
				throw new global::System.IndexOutOfRangeException();
			}
			return m_Events.signals[idx];
		}

		private void OnEnable()
		{
		}
	}
}

namespace UnityEngine.Rendering.Universal
{
	public class UniversalCameraHistory : global::UnityEngine.Rendering.ICameraHistoryReadAccess, global::UnityEngine.Rendering.ICameraHistoryWriteAccess, global::UnityEngine.Rendering.IPerFrameHistoryAccessTracker, global::System.IDisposable
	{
		private static class TypeId<T>
		{
			public static uint value = s_TypeCount++;
		}

		private struct Item
		{
			public global::UnityEngine.Rendering.ContextItem storage;

			public int requestVersion;

			public int writeVersion;

			public void Reset()
			{
				storage?.Reset();
				requestVersion = -2;
				writeVersion = -2;
			}
		}

		private const int k_ValidVersionCount = 2;

		private static uint s_TypeCount;

		private global::UnityEngine.Rendering.Universal.UniversalCameraHistory.Item[] m_Items = new global::UnityEngine.Rendering.Universal.UniversalCameraHistory.Item[32];

		private int m_Version;

		private global::UnityEngine.Rendering.BufferedRTHandleSystem m_HistoryTextures = new global::UnityEngine.Rendering.BufferedRTHandleSystem();

		public event global::UnityEngine.Rendering.ICameraHistoryReadAccess.HistoryRequestDelegate OnGatherHistoryRequests;

		public void RequestAccess<Type>() where Type : global::UnityEngine.Rendering.ContextItem
		{
			uint value = global::UnityEngine.Rendering.Universal.UniversalCameraHistory.TypeId<Type>.value;
			if (value >= m_Items.Length)
			{
				global::UnityEngine.Rendering.Universal.UniversalCameraHistory.Item[] array = new global::UnityEngine.Rendering.Universal.UniversalCameraHistory.Item[global::Unity.Mathematics.math.max(global::Unity.Mathematics.math.ceilpow2(s_TypeCount), m_Items.Length * 2)];
				for (int i = 0; i < m_Items.Length; i++)
				{
					array[i] = m_Items[i];
				}
				m_Items = array;
			}
			m_Items[value].requestVersion = m_Version;
		}

		public Type GetHistoryForRead<Type>() where Type : global::UnityEngine.Rendering.ContextItem
		{
			uint value = global::UnityEngine.Rendering.Universal.UniversalCameraHistory.TypeId<Type>.value;
			if (value >= m_Items.Length)
			{
				return null;
			}
			if (!IsValid((int)value))
			{
				return null;
			}
			return (Type)m_Items[value].storage;
		}

		public bool IsAccessRequested<Type>() where Type : global::UnityEngine.Rendering.ContextItem
		{
			uint value = global::UnityEngine.Rendering.Universal.UniversalCameraHistory.TypeId<Type>.value;
			if (value >= m_Items.Length)
			{
				return false;
			}
			return IsValidRequest((int)value);
		}

		public Type GetHistoryForWrite<Type>() where Type : global::UnityEngine.Rendering.ContextItem, new()
		{
			uint value = global::UnityEngine.Rendering.Universal.UniversalCameraHistory.TypeId<Type>.value;
			if (value >= m_Items.Length)
			{
				return null;
			}
			if (!IsValidRequest((int)value))
			{
				return null;
			}
			if (m_Items[value].storage == null)
			{
				ref global::UnityEngine.Rendering.Universal.UniversalCameraHistory.Item reference = ref m_Items[value];
				reference.storage = new Type();
				if (reference.storage is global::UnityEngine.Rendering.CameraHistoryItem cameraHistoryItem)
				{
					cameraHistoryItem.OnCreate(m_HistoryTextures, value);
				}
			}
			m_Items[value].writeVersion = m_Version;
			return (Type)m_Items[value].storage;
		}

		public bool IsWritten<Type>() where Type : global::UnityEngine.Rendering.ContextItem
		{
			uint value = global::UnityEngine.Rendering.Universal.UniversalCameraHistory.TypeId<Type>.value;
			if (value >= m_Items.Length)
			{
				return false;
			}
			return m_Items[value].writeVersion == m_Version;
		}

		internal UniversalCameraHistory()
		{
			for (int i = 0; i < m_Items.Length; i++)
			{
				m_Items[i].Reset();
			}
		}

		public void Dispose()
		{
			for (int i = 0; i < m_Items.Length; i++)
			{
				m_Items[i].Reset();
			}
			m_HistoryTextures.ReleaseAll();
		}

		internal void GatherHistoryRequests()
		{
			this.OnGatherHistoryRequests?.Invoke(this);
		}

		private bool IsValidRequest(int i)
		{
			return m_Version - m_Items[i].requestVersion < 2;
		}

		private bool IsValid(int i)
		{
			return m_Version - m_Items[i].writeVersion < 2;
		}

		internal void ReleaseUnusedHistory()
		{
			for (int i = 0; i < m_Items.Length; i++)
			{
				if (!IsValidRequest(i) && !IsValid(i))
				{
					m_Items[i].Reset();
				}
			}
			m_Version++;
		}

		internal void SwapAndSetReferenceSize(int cameraWidth, int cameraHeight)
		{
			m_HistoryTextures.SwapAndSetReferenceSize(cameraWidth, cameraHeight);
		}
	}
}

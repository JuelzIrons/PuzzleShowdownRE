namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	[global::UnityEngine.Timeline.CustomStyle("SignalEmitter")]
	[global::UnityEngine.ExcludeFromPreset]
	public class SignalEmitter : global::UnityEngine.Timeline.Marker, global::UnityEngine.Playables.INotification, global::UnityEngine.Timeline.INotificationOptionProvider
	{
		[global::UnityEngine.SerializeField]
		private bool m_Retroactive;

		[global::UnityEngine.SerializeField]
		private bool m_EmitOnce;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Timeline.SignalAsset m_Asset;

		public bool retroactive
		{
			get
			{
				return m_Retroactive;
			}
			set
			{
				m_Retroactive = value;
			}
		}

		public bool emitOnce
		{
			get
			{
				return m_EmitOnce;
			}
			set
			{
				m_EmitOnce = value;
			}
		}

		public global::UnityEngine.Timeline.SignalAsset asset
		{
			get
			{
				return m_Asset;
			}
			set
			{
				m_Asset = value;
			}
		}

		global::UnityEngine.PropertyName global::UnityEngine.Playables.INotification.id
		{
			get
			{
				if (m_Asset != null)
				{
					return new global::UnityEngine.PropertyName(m_Asset.name);
				}
				return new global::UnityEngine.PropertyName(string.Empty);
			}
		}

		global::UnityEngine.Timeline.NotificationFlags global::UnityEngine.Timeline.INotificationOptionProvider.flags => (global::UnityEngine.Timeline.NotificationFlags)((retroactive ? 2 : 0) | (emitOnce ? 4 : 0) | 1);
	}
}

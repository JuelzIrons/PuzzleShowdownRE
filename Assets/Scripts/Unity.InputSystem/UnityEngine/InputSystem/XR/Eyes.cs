namespace UnityEngine.InputSystem.XR
{
	public struct Eyes
	{
		public global::UnityEngine.Vector3 m_LeftEyePosition;

		public global::UnityEngine.Quaternion m_LeftEyeRotation;

		public global::UnityEngine.Vector3 m_RightEyePosition;

		public global::UnityEngine.Quaternion m_RightEyeRotation;

		public global::UnityEngine.Vector3 m_FixationPoint;

		public float m_LeftEyeOpenAmount;

		public float m_RightEyeOpenAmount;

		public global::UnityEngine.Vector3 leftEyePosition
		{
			get
			{
				return m_LeftEyePosition;
			}
			set
			{
				m_LeftEyePosition = value;
			}
		}

		public global::UnityEngine.Quaternion leftEyeRotation
		{
			get
			{
				return m_LeftEyeRotation;
			}
			set
			{
				m_LeftEyeRotation = value;
			}
		}

		public global::UnityEngine.Vector3 rightEyePosition
		{
			get
			{
				return m_RightEyePosition;
			}
			set
			{
				m_RightEyePosition = value;
			}
		}

		public global::UnityEngine.Quaternion rightEyeRotation
		{
			get
			{
				return m_RightEyeRotation;
			}
			set
			{
				m_RightEyeRotation = value;
			}
		}

		public global::UnityEngine.Vector3 fixationPoint
		{
			get
			{
				return m_FixationPoint;
			}
			set
			{
				m_FixationPoint = value;
			}
		}

		public float leftEyeOpenAmount
		{
			get
			{
				return m_LeftEyeOpenAmount;
			}
			set
			{
				m_LeftEyeOpenAmount = value;
			}
		}

		public float rightEyeOpenAmount
		{
			get
			{
				return m_RightEyeOpenAmount;
			}
			set
			{
				m_RightEyeOpenAmount = value;
			}
		}
	}
}

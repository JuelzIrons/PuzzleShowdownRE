namespace UnityEngine.InputSystem.XR
{
	public struct Bone
	{
		public uint m_ParentBoneIndex;

		public global::UnityEngine.Vector3 m_Position;

		public global::UnityEngine.Quaternion m_Rotation;

		public uint parentBoneIndex
		{
			get
			{
				return m_ParentBoneIndex;
			}
			set
			{
				m_ParentBoneIndex = value;
			}
		}

		public global::UnityEngine.Vector3 position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public global::UnityEngine.Quaternion rotation
		{
			get
			{
				return m_Rotation;
			}
			set
			{
				m_Rotation = value;
			}
		}
	}
}

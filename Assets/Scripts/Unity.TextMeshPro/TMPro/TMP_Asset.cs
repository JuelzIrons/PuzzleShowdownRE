namespace TMPro
{
	[global::System.Serializable]
	public abstract class TMP_Asset : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		internal string m_Version;

		internal int m_InstanceID;

		internal int m_HashCode;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.TextCore.FaceInfo m_FaceInfo;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("material")]
		internal global::UnityEngine.Material m_Material;

		internal int m_MaterialHashCode;

		public string version
		{
			get
			{
				return m_Version;
			}
			internal set
			{
				m_Version = value;
			}
		}

		public int instanceID
		{
			get
			{
				if (m_InstanceID == 0)
				{
					m_InstanceID = GetInstanceID();
				}
				return m_InstanceID;
			}
		}

		public int hashCode
		{
			get
			{
				if (m_HashCode == 0)
				{
					m_HashCode = global::TMPro.TMP_TextUtilities.GetHashCode(base.name);
				}
				return m_HashCode;
			}
			set
			{
				m_HashCode = value;
			}
		}

		public global::UnityEngine.TextCore.FaceInfo faceInfo
		{
			get
			{
				return m_FaceInfo;
			}
			set
			{
				m_FaceInfo = value;
			}
		}

		public global::UnityEngine.Material material
		{
			get
			{
				return m_Material;
			}
			set
			{
				m_Material = value;
			}
		}

		public int materialHashCode
		{
			get
			{
				if (m_MaterialHashCode == 0)
				{
					if (m_Material == null)
					{
						return 0;
					}
					m_MaterialHashCode = global::TMPro.TMP_TextUtilities.GetSimpleHashCode(m_Material.name);
				}
				return m_MaterialHashCode;
			}
			set
			{
				m_MaterialHashCode = value;
			}
		}
	}
}

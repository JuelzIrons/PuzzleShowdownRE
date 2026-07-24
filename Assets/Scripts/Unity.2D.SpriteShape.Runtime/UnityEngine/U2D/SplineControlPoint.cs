namespace UnityEngine.U2D
{
	[global::System.Serializable]
	public class SplineControlPoint
	{
		public global::UnityEngine.Vector3 position;

		public global::UnityEngine.Vector3 leftTangent;

		public global::UnityEngine.Vector3 rightTangent;

		public global::UnityEngine.U2D.ShapeTangentMode mode;

		public float height = 1f;

		public int spriteIndex;

		public bool corner;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.U2D.Corner m_CornerMode;

		public global::UnityEngine.U2D.Corner cornerMode
		{
			get
			{
				return m_CornerMode;
			}
			set
			{
				m_CornerMode = value;
			}
		}

		public override int GetHashCode()
		{
			int num = ((int)position.x).GetHashCode() ^ ((int)position.y).GetHashCode() ^ position.GetHashCode() ^ (leftTangent.GetHashCode() << 2) ^ (rightTangent.GetHashCode() >> 2);
			int num2 = (int)mode;
			return num ^ num2.GetHashCode() ^ height.GetHashCode() ^ spriteIndex.GetHashCode() ^ corner.GetHashCode() ^ (m_CornerMode.GetHashCode() << 2);
		}
	}
}

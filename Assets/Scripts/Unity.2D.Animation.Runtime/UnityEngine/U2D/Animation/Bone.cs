namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.AddComponentMenu("")]
	internal class Bone : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private string m_Guid;

		public string guid
		{
			get
			{
				return m_Guid;
			}
			set
			{
				m_Guid = value;
			}
		}
	}
}

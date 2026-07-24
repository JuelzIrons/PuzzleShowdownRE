namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class TrackColorAttribute : global::System.Attribute
	{
		private global::UnityEngine.Color m_Color;

		public global::UnityEngine.Color color => m_Color;

		public TrackColorAttribute(float r, float g, float b)
		{
			m_Color = new global::UnityEngine.Color(r, g, b);
		}
	}
}

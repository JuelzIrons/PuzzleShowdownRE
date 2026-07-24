namespace UnityEngine.U2D.IK
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.IK")]
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public sealed class Solver2DMenuAttribute : global::System.Attribute
	{
		private string m_MenuPath;

		public string menuPath => m_MenuPath;

		public Solver2DMenuAttribute(string _menuPath)
		{
			m_MenuPath = _menuPath;
		}
	}
}

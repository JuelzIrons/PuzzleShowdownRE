namespace UnityEngine.Rendering
{
	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Enum, AllowMultiple = false)]
	public class CoreRPHelpURLAttribute : global::UnityEngine.HelpURLAttribute
	{
		public CoreRPHelpURLAttribute(string pageName, string packageName = "com.unity.render-pipelines.core")
			: base(global::UnityEngine.Rendering.DocumentationInfo.GetPageLink(packageName, pageName, ""))
		{
		}

		public CoreRPHelpURLAttribute(string pageName, string pageHash, string packageName = "com.unity.render-pipelines.core")
			: base(global::UnityEngine.Rendering.DocumentationInfo.GetPageLink(packageName, pageName, pageHash))
		{
		}
	}
}

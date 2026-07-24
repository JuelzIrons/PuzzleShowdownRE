namespace UnityEngine.Rendering.Universal
{
	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	internal class URPHelpURLAttribute : global::UnityEngine.Rendering.CoreRPHelpURLAttribute
	{
		public URPHelpURLAttribute(string pageName, string pageHash = "")
			: base(pageName, pageHash, "com.unity.render-pipelines.universal")
		{
		}
	}
}

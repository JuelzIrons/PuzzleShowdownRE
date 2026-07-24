namespace UnityEngine.Rendering
{
	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Enum, AllowMultiple = false)]
	public class CurrentPipelineHelpURLAttribute : global::UnityEngine.HelpURLAttribute
	{
		private string pageName { get; }

		private string pageHash { get; }

		public override string URL => string.Empty;

		public CurrentPipelineHelpURLAttribute(string pageName, string pageHash = "")
			: base(null)
		{
			this.pageName = pageName;
			this.pageHash = pageHash;
		}
	}
}

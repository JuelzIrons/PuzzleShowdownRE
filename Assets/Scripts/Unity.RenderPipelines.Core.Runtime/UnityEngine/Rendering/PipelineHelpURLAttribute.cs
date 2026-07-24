namespace UnityEngine.Rendering
{
	[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
	[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Enum, AllowMultiple = true)]
	public class PipelineHelpURLAttribute : global::UnityEngine.HelpURLAttribute
	{
		private string pipelineName { get; }

		private string pageName { get; }

		private string pageHash { get; }

		public override string URL => string.Empty;

		public PipelineHelpURLAttribute(string pipelineName, string pageName, string pageHash = "")
			: base(null)
		{
			this.pipelineName = pipelineName;
			this.pageName = pageName;
			this.pageHash = pageHash;
		}
	}
}

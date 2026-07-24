namespace UnityEngine.Rendering
{
	public static class RenderGraphGraphicsAutomatedTests
	{
		private static bool activatedFromCommandLine => global::System.Array.Exists(global::System.Environment.GetCommandLineArgs(), (string arg) => arg == "-render-graph-reuse-tests");

		[global::System.Obsolete]
		public static bool enabled { get; set; } = activatedFromCommandLine;

		public static bool? forceRenderGraphState { get; set; } = activatedFromCommandLine ? new bool?(true) : ((bool?)null);
	}
}

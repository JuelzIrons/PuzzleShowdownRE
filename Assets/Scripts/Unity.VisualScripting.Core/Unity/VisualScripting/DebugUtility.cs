namespace Unity.VisualScripting
{
	public static class DebugUtility
	{
		public static string logPath => global::System.IO.Path.Combine(global::System.Environment.GetFolderPath(global::System.Environment.SpecialFolder.Desktop), "Ludiq.log");

		public static void LogToFile(string message)
		{
			global::System.IO.File.AppendAllText(logPath, message + global::System.Environment.NewLine);
		}
	}
}

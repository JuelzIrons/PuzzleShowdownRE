namespace Unity.Netcode
{
	public class CommandLineOptions
	{
		private static global::Unity.Netcode.CommandLineOptions s_Instance;

		internal static global::System.Collections.Generic.List<string> CommandLineArguments = new global::System.Collections.Generic.List<string>();

		public static global::Unity.Netcode.CommandLineOptions Instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::Unity.Netcode.CommandLineOptions();
				}
				return s_Instance;
			}
			private set
			{
				s_Instance = value;
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitializeOnLoad()
		{
			Instance = new global::Unity.Netcode.CommandLineOptions();
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod]
		private static void ParseCommandLineArguments()
		{
			CommandLineArguments = new global::System.Collections.Generic.List<string>(global::System.Environment.GetCommandLineArgs());
		}

		public string GetArg(string arg)
		{
			int num = CommandLineArguments.IndexOf(arg);
			if (num >= 0 && num < CommandLineArguments.Count - 1)
			{
				return CommandLineArguments[num + 1];
			}
			return null;
		}
	}
}

namespace Unity.Multiplayer.Tools.Common
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public ref struct ProfilerScope
	{
		public static global::Unity.Multiplayer.Tools.Common.ProfilerScope BeginSample(string name)
		{
			return new global::Unity.Multiplayer.Tools.Common.ProfilerScope(name);
		}

		private ProfilerScope(string name)
		{
		}

		public void Dispose()
		{
		}
	}
}

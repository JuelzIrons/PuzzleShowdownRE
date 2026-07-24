namespace Unity.Multiplayer.Tools.Common
{
	internal static class RuntimeUtils
	{
		public static void NoEffectWarning(this object source, [global::System.Runtime.CompilerServices.CallerMemberName] string caller = "")
		{
			source.NoEffectWarning<bool>(caller);
		}

		public static T NoEffectWarning<T>(this object source, [global::System.Runtime.CompilerServices.CallerMemberName] string caller = "")
		{
			string name = source.GetType().Name;
			global::UnityEngine.Debug.LogWarning("\"" + name + "." + caller + "\" has no effect as it has been disabled by scripting symbols.");
			return default(T);
		}
	}
}

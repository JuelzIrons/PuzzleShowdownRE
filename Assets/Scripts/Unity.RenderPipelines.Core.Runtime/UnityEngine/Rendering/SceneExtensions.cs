namespace UnityEngine.Rendering
{
	internal static class SceneExtensions
	{
		private static global::System.Reflection.PropertyInfo s_SceneGUID = typeof(global::UnityEngine.SceneManagement.Scene).GetProperty("guid", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic);

		public static string GetGUID(this global::UnityEngine.SceneManagement.Scene scene)
		{
			return (string)s_SceneGUID.GetValue(scene);
		}
	}
}

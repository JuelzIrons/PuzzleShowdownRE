namespace UnityEngine.Rendering.Universal
{
	internal static class CameraTypeUtility
	{
		private static string[] s_CameraTypeNames = global::System.Linq.Enumerable.ToArray(global::System.Enum.GetNames(typeof(global::UnityEngine.Rendering.Universal.CameraRenderType)));

		public static string GetName(this global::UnityEngine.Rendering.Universal.CameraRenderType type)
		{
			int num = (int)type;
			if (num < 0 || num >= s_CameraTypeNames.Length)
			{
				num = 0;
			}
			return s_CameraTypeNames[num];
		}
	}
}

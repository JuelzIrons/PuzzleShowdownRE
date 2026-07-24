namespace UnityEngine.Rendering.RadeonRays
{
	internal static class Common
	{
		public static uint CeilDivide(uint val, uint div)
		{
			return (val + div - 1) / div;
		}

		public static void EnableKeyword(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.ComputeShader shader, string keyword, bool enable)
		{
			if (enable)
			{
				cmd.EnableKeyword(shader, new global::UnityEngine.Rendering.LocalKeyword(shader, keyword));
			}
			else
			{
				cmd.DisableKeyword(shader, new global::UnityEngine.Rendering.LocalKeyword(shader, keyword));
			}
		}
	}
}

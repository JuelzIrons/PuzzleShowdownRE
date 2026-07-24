namespace UnityEngine.Rendering.Universal
{
	public static class ShaderUtils
	{
		private static readonly string[] s_ShaderPaths = new string[13]
		{
			"Universal Render Pipeline/Lit", "Universal Render Pipeline/Simple Lit", "Universal Render Pipeline/Unlit", "Universal Render Pipeline/Terrain/Lit", "Universal Render Pipeline/Particles/Lit", "Universal Render Pipeline/Particles/Simple Lit", "Universal Render Pipeline/Particles/Unlit", "Universal Render Pipeline/Baked Lit", "Universal Render Pipeline/Nature/SpeedTree7", "Universal Render Pipeline/Nature/SpeedTree7 Billboard",
			"Universal Render Pipeline/Nature/SpeedTree8_PBRLit", "SpeedTree9_Dummy_Path", "Universal Render Pipeline/Complex Lit"
		};

		internal static float PersistentDeltaTime => global::UnityEngine.Time.deltaTime;

		public static string GetShaderPath(global::UnityEngine.Rendering.Universal.ShaderPathID id)
		{
			int num = (int)id;
			int num2 = s_ShaderPaths.Length;
			if (num2 > 0 && num >= 0 && num < num2)
			{
				return s_ShaderPaths[num];
			}
			global::UnityEngine.Debug.LogError("Trying to access universal shader path out of bounds: (" + id.ToString() + ": " + num + ")");
			return "";
		}

		public static global::UnityEngine.Rendering.Universal.ShaderPathID GetEnumFromPath(string path)
		{
			return (global::UnityEngine.Rendering.Universal.ShaderPathID)global::System.Array.FindIndex(s_ShaderPaths, (string m) => m == path);
		}

		public static bool IsLWShader(global::UnityEngine.Shader shader)
		{
			return global::System.Linq.Enumerable.Contains(s_ShaderPaths, shader.name);
		}
	}
}

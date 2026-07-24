namespace UnityEngine.Rendering
{
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("Utilities")]
	public static class MaterialQualityUtilities
	{
		public static string[] KeywordNames = new string[3] { "MATERIAL_QUALITY_LOW", "MATERIAL_QUALITY_MEDIUM", "MATERIAL_QUALITY_HIGH" };

		public static string[] EnumNames = global::System.Enum.GetNames(typeof(global::UnityEngine.Rendering.MaterialQuality));

		public static global::UnityEngine.Rendering.ShaderKeyword[] Keywords = new global::UnityEngine.Rendering.ShaderKeyword[3]
		{
			new global::UnityEngine.Rendering.ShaderKeyword(KeywordNames[0]),
			new global::UnityEngine.Rendering.ShaderKeyword(KeywordNames[1]),
			new global::UnityEngine.Rendering.ShaderKeyword(KeywordNames[2])
		};

		public static global::UnityEngine.Rendering.MaterialQuality GetHighestQuality(this global::UnityEngine.Rendering.MaterialQuality levels)
		{
			for (int num = Keywords.Length - 1; num >= 0; num--)
			{
				global::UnityEngine.Rendering.MaterialQuality materialQuality = (global::UnityEngine.Rendering.MaterialQuality)(1 << num);
				if ((levels & materialQuality) != 0)
				{
					return materialQuality;
				}
			}
			return (global::UnityEngine.Rendering.MaterialQuality)0;
		}

		public static global::UnityEngine.Rendering.MaterialQuality GetClosestQuality(this global::UnityEngine.Rendering.MaterialQuality availableLevels, global::UnityEngine.Rendering.MaterialQuality requestedLevel)
		{
			if (availableLevels == (global::UnityEngine.Rendering.MaterialQuality)0)
			{
				return global::UnityEngine.Rendering.MaterialQuality.Low;
			}
			int num = requestedLevel.ToFirstIndex();
			global::UnityEngine.Rendering.MaterialQuality materialQuality = (global::UnityEngine.Rendering.MaterialQuality)0;
			for (int num2 = num; num2 >= 0; num2--)
			{
				global::UnityEngine.Rendering.MaterialQuality materialQuality2 = FromIndex(num2);
				if ((materialQuality2 & availableLevels) != 0)
				{
					materialQuality = materialQuality2;
					break;
				}
			}
			if (materialQuality != 0)
			{
				return materialQuality;
			}
			for (int i = num + 1; i < Keywords.Length; i++)
			{
				global::UnityEngine.Rendering.MaterialQuality materialQuality3 = FromIndex(i);
				global::System.Math.Abs(requestedLevel - materialQuality3);
				if ((materialQuality3 & availableLevels) != 0)
				{
					materialQuality = materialQuality3;
					break;
				}
			}
			return materialQuality;
		}

		public static void SetGlobalShaderKeywords(this global::UnityEngine.Rendering.MaterialQuality level)
		{
			for (int i = 0; i < KeywordNames.Length; i++)
			{
				if (((uint)level & (uint)(1 << i)) != 0)
				{
					global::UnityEngine.Shader.EnableKeyword(KeywordNames[i]);
				}
				else
				{
					global::UnityEngine.Shader.DisableKeyword(KeywordNames[i]);
				}
			}
		}

		public static void SetGlobalShaderKeywords(this global::UnityEngine.Rendering.MaterialQuality level, global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			for (int i = 0; i < KeywordNames.Length; i++)
			{
				if (((uint)level & (uint)(1 << i)) != 0)
				{
					cmd.EnableShaderKeyword(KeywordNames[i]);
				}
				else
				{
					cmd.DisableShaderKeyword(KeywordNames[i]);
				}
			}
		}

		public static int ToFirstIndex(this global::UnityEngine.Rendering.MaterialQuality level)
		{
			for (int i = 0; i < KeywordNames.Length; i++)
			{
				if (((uint)level & (uint)(1 << i)) != 0)
				{
					return i;
				}
			}
			return -1;
		}

		public static global::UnityEngine.Rendering.MaterialQuality FromIndex(int index)
		{
			return (global::UnityEngine.Rendering.MaterialQuality)(1 << index);
		}
	}
}

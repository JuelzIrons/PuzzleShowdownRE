namespace UnityEngine.UI
{
	public static class StencilMaterial
	{
		private class MatEntry
		{
			public global::UnityEngine.Material baseMat;

			public global::UnityEngine.Material customMat;

			public int count;

			public int stencilId;

			public global::UnityEngine.Rendering.StencilOp operation;

			public global::UnityEngine.Rendering.CompareFunction compareFunction = global::UnityEngine.Rendering.CompareFunction.Always;

			public int readMask;

			public int writeMask;

			public bool useAlphaClip;

			public global::UnityEngine.Rendering.ColorWriteMask colorMask;
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.UI.StencilMaterial.MatEntry> m_List = new global::System.Collections.Generic.List<global::UnityEngine.UI.StencilMaterial.MatEntry>();

		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		[global::System.Obsolete("Use Material.Add instead.", true)]
		public static global::UnityEngine.Material Add(global::UnityEngine.Material baseMat, int stencilID)
		{
			return null;
		}

		public static global::UnityEngine.Material Add(global::UnityEngine.Material baseMat, int stencilID, global::UnityEngine.Rendering.StencilOp operation, global::UnityEngine.Rendering.CompareFunction compareFunction, global::UnityEngine.Rendering.ColorWriteMask colorWriteMask)
		{
			return Add(baseMat, stencilID, operation, compareFunction, colorWriteMask, 255, 255);
		}

		private static void LogWarningWhenNotInBatchmode(string warning, global::UnityEngine.Object context)
		{
			if (!global::UnityEngine.Application.isBatchMode)
			{
				global::UnityEngine.Debug.LogWarning(warning, context);
			}
		}

		public static global::UnityEngine.Material Add(global::UnityEngine.Material baseMat, int stencilID, global::UnityEngine.Rendering.StencilOp operation, global::UnityEngine.Rendering.CompareFunction compareFunction, global::UnityEngine.Rendering.ColorWriteMask colorWriteMask, int readMask, int writeMask)
		{
			if ((stencilID <= 0 && colorWriteMask == global::UnityEngine.Rendering.ColorWriteMask.All) || baseMat == null)
			{
				return baseMat;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _Stencil property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilOp"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilOp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilComp"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilComp property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilReadMask"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilReadMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_StencilWriteMask"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _StencilWriteMask property", baseMat);
				return baseMat;
			}
			if (!baseMat.HasProperty("_ColorMask"))
			{
				LogWarningWhenNotInBatchmode("Material " + baseMat.name + " doesn't have _ColorMask property", baseMat);
				return baseMat;
			}
			int count = m_List.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.StencilMaterial.MatEntry matEntry = m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilId == stencilID && matEntry.operation == operation && matEntry.compareFunction == compareFunction && matEntry.readMask == readMask && matEntry.writeMask == writeMask && matEntry.colorMask == colorWriteMask)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			global::UnityEngine.UI.StencilMaterial.MatEntry matEntry2 = new global::UnityEngine.UI.StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new global::UnityEngine.Material(baseMat);
			matEntry2.customMat.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
			matEntry2.stencilId = stencilID;
			matEntry2.operation = operation;
			matEntry2.compareFunction = compareFunction;
			matEntry2.readMask = readMask;
			matEntry2.writeMask = writeMask;
			matEntry2.colorMask = colorWriteMask;
			matEntry2.useAlphaClip = operation != global::UnityEngine.Rendering.StencilOp.Keep && writeMask > 0;
			matEntry2.customMat.name = $"Stencil Id:{stencilID}, Op:{operation}, Comp:{compareFunction}, WriteMask:{writeMask}, ReadMask:{readMask}, ColorMask:{colorWriteMask} AlphaClip:{matEntry2.useAlphaClip} ({baseMat.name})";
			matEntry2.customMat.SetFloat("_Stencil", stencilID);
			matEntry2.customMat.SetFloat("_StencilOp", (float)operation);
			matEntry2.customMat.SetFloat("_StencilComp", (float)compareFunction);
			matEntry2.customMat.SetFloat("_StencilReadMask", readMask);
			matEntry2.customMat.SetFloat("_StencilWriteMask", writeMask);
			matEntry2.customMat.SetFloat("_ColorMask", (float)colorWriteMask);
			matEntry2.customMat.SetFloat("_UseUIAlphaClip", matEntry2.useAlphaClip ? 1f : 0f);
			if (matEntry2.useAlphaClip)
			{
				matEntry2.customMat.EnableKeyword("UNITY_UI_ALPHACLIP");
			}
			else
			{
				matEntry2.customMat.DisableKeyword("UNITY_UI_ALPHACLIP");
			}
			m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		public static void Remove(global::UnityEngine.Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			int count = m_List.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.StencilMaterial.MatEntry matEntry = m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					if (--matEntry.count == 0)
					{
						global::UnityEngine.UI.Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						m_List.RemoveAt(i);
					}
					break;
				}
			}
		}

		public static void ClearAll()
		{
			int count = m_List.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.StencilMaterial.MatEntry matEntry = m_List[i];
				global::UnityEngine.UI.Misc.DestroyImmediate(matEntry.customMat);
				matEntry.baseMat = null;
			}
			m_List.Clear();
		}
	}
}

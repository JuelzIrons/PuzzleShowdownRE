namespace Unity.Multiplayer.Tools.NetStatsMonitor.Implementation
{
	internal class GraphBuffers
	{
		public const int k_VerticesPerPoint = 2;

		private const int k_TrisPerLine = 2;

		private const int k_IndicesPerTri = 3;

		private const int k_IndicesPerLineSegment = 6;

		private int m_VariableColorsHash;

		public global::UnityEngine.UIElements.Vertex[] Vertices { get; private set; }

		private ushort[] Indices { get; set; }

		public global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters Parameters { get; private set; }

		private static int ComputeColorsHash(global::UnityEngine.Color[] colors)
		{
			int num = 0;
			for (int i = 0; i < colors.Length; i++)
			{
				int hashCode = colors[i].GetHashCode();
				num = global::System.HashCode.Combine(num, hashCode);
			}
			return num;
		}

		public void UpdateIfNeeded(in global::Unity.Multiplayer.Tools.NetStatsMonitor.Implementation.GraphBufferParameters newParams, in global::UnityEngine.Color[] variableColors)
		{
			int num = newParams.StatCount * newParams.GraphWidthPoints;
			int num2 = 2 * num;
			int num3 = global::System.Math.Max(0, newParams.GraphWidthPoints - 1) * newParams.StatCount;
			int num4 = 6 * num3;
			global::UnityEngine.UIElements.Vertex[] vertices = Vertices;
			if (((vertices != null) ? vertices.Length : 0) != num2)
			{
				Vertices = new global::UnityEngine.UIElements.Vertex[num2];
			}
			ushort[] indices = Indices;
			if (((indices != null) ? indices.Length : 0) != num4)
			{
				Indices = new ushort[num4];
			}
			int num5;
			if (newParams.StatCount == Parameters.StatCount)
			{
				num5 = ((newParams.GraphWidthPoints != Parameters.GraphWidthPoints) ? 1 : 0);
				if (num5 == 0)
				{
					goto IL_00b1;
				}
			}
			else
			{
				num5 = 1;
			}
			ComputeIndices(newParams.StatCount, newParams.GraphWidthPoints);
			goto IL_00b1;
			IL_00b1:
			Parameters = newParams;
			int num6 = ComputeColorsHash(variableColors);
			bool flag = num6 != m_VariableColorsHash;
			if (((uint)num5 | (flag ? 1u : 0u)) != 0)
			{
				SetVertexColors(newParams.StatCount, newParams.GraphWidthPoints, variableColors);
			}
			m_VariableColorsHash = num6;
		}

		private void ComputeIndices(int statCount, int pointsPerStat)
		{
			int num = global::System.Math.Max(0, pointsPerStat - 1);
			int num2 = 6 * num;
			int num3 = 2 * pointsPerStat;
			for (int i = 0; i < statCount; i++)
			{
				int num4 = i * num2;
				int num5 = i * num3;
				for (int j = 0; j < num; j++)
				{
					int num6 = num4 + 6 * j;
					int num7 = num5 + 2 * j;
					Indices[num6] = (ushort)num7;
					Indices[num6 + 1] = (ushort)(num7 + 1);
					Indices[num6 + 2] = (ushort)(num7 + 2);
					Indices[num6 + 3] = (ushort)(num7 + 3);
					Indices[num6 + 4] = (ushort)(num7 + 2);
					Indices[num6 + 5] = (ushort)(num7 + 1);
				}
			}
		}

		private void SetVertexColors(int statCount, int pointsPerStat, global::UnityEngine.Color[] variableColors)
		{
			int num = 2 * pointsPerStat;
			for (int i = 0; i < statCount; i++)
			{
				int num2 = i * num;
				global::UnityEngine.Color color = ((variableColors != null && i < variableColors.Length) ? variableColors[i] : global::Unity.Multiplayer.Tools.Common.Visualization.CategoricalColorPalette.GetColor(i));
				for (int j = 0; j < num; j++)
				{
					int num3 = num2 + j;
					Vertices[num3].tint = color;
				}
			}
		}

		public void WriteToMeshGenerationContext(global::UnityEngine.UIElements.MeshGenerationContext mgc)
		{
			if (Vertices != null && Indices != null && Vertices.Length != 0 && Indices.Length != 0)
			{
				global::UnityEngine.UIElements.MeshWriteData meshWriteData = mgc.Allocate(Vertices.Length, Indices.Length);
				meshWriteData.SetAllVertices(Vertices);
				meshWriteData.SetAllIndices(Indices);
			}
		}
	}
}

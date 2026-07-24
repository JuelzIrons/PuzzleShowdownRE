namespace UnityEngine.Experimental.Rendering
{
	internal class XROcclusionMesh
	{
		private global::UnityEngine.Experimental.Rendering.XRPass m_Pass;

		private global::UnityEngine.Mesh m_CombinedMesh;

		private global::UnityEngine.Material m_Material;

		private int m_CombinedMeshHashCode;

		private static readonly global::UnityEngine.Rendering.ProfilingSampler k_OcclusionMeshProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("XR Occlusion Mesh");

		internal bool hasValidOcclusionMesh
		{
			get
			{
				if (IsOcclusionMeshSupported())
				{
					if (m_Pass.singlePassEnabled)
					{
						return m_CombinedMesh != null;
					}
					return m_Pass.GetOcclusionMesh() != null;
				}
				return false;
			}
		}

		internal XROcclusionMesh(global::UnityEngine.Experimental.Rendering.XRPass xrPass)
		{
			m_Pass = xrPass;
		}

		internal void SetMaterial(global::UnityEngine.Material mat)
		{
			m_Material = mat;
		}

		internal void RenderOcclusionMesh(global::UnityEngine.Rendering.CommandBuffer cmd, float occlusionMeshScale, bool yFlip = false)
		{
			if (!IsOcclusionMeshSupported())
			{
				return;
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(cmd, k_OcclusionMeshProfilingSampler))
			{
				if (m_Pass.singlePassEnabled)
				{
					if (m_CombinedMesh != null && global::UnityEngine.SystemInfo.supportsMultiview)
					{
						cmd.EnableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
						global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(occlusionMeshScale, yFlip ? occlusionMeshScale : (0f - occlusionMeshScale), 1f);
						cmd.DrawMesh(m_CombinedMesh, global::UnityEngine.Matrix4x4.Scale(vector), m_Material);
						cmd.DisableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
					}
					else if (m_CombinedMesh != null && global::UnityEngine.SystemInfo.supportsRenderTargetArrayIndexFromVertexShader)
					{
						m_Pass.StopSinglePass(cmd);
						cmd.EnableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
						global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(occlusionMeshScale, yFlip ? occlusionMeshScale : (0f - occlusionMeshScale), 1f);
						cmd.DrawMesh(m_CombinedMesh, global::UnityEngine.Matrix4x4.Scale(vector2), m_Material);
						cmd.DisableShaderKeyword("XR_OCCLUSION_MESH_COMBINED");
						m_Pass.StartSinglePass(cmd);
					}
				}
				else
				{
					global::UnityEngine.Mesh occlusionMesh = m_Pass.GetOcclusionMesh();
					if (occlusionMesh != null)
					{
						cmd.DrawMesh(occlusionMesh, global::UnityEngine.Matrix4x4.identity, m_Material);
					}
				}
			}
		}

		internal void UpdateCombinedMesh()
		{
			if (IsOcclusionMeshSupported() && m_Pass.singlePassEnabled && TryGetOcclusionMeshCombinedHashCode(out var hashCode))
			{
				if (m_CombinedMesh == null || hashCode != m_CombinedMeshHashCode)
				{
					CreateOcclusionMeshCombined();
					m_CombinedMeshHashCode = hashCode;
				}
			}
			else
			{
				m_CombinedMesh = null;
				m_CombinedMeshHashCode = 0;
			}
		}

		private bool IsOcclusionMeshSupported()
		{
			if (m_Pass.enabled)
			{
				return m_Material != null;
			}
			return false;
		}

		private bool TryGetOcclusionMeshCombinedHashCode(out int hashCode)
		{
			hashCode = 17;
			for (int i = 0; i < m_Pass.viewCount; i++)
			{
				global::UnityEngine.Mesh occlusionMesh = m_Pass.GetOcclusionMesh(i);
				if (occlusionMesh != null)
				{
					hashCode = hashCode * 23 + occlusionMesh.GetHashCode();
					continue;
				}
				hashCode = 0;
				return false;
			}
			return true;
		}

		private void CreateOcclusionMeshCombined()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(m_CombinedMesh);
			m_CombinedMesh = new global::UnityEngine.Mesh();
			m_CombinedMesh.indexFormat = global::UnityEngine.Rendering.IndexFormat.UInt16;
			int num = 0;
			uint num2 = 0u;
			for (int i = 0; i < m_Pass.viewCount; i++)
			{
				global::UnityEngine.Mesh occlusionMesh = m_Pass.GetOcclusionMesh(i);
				num += occlusionMesh.vertexCount;
				num2 += occlusionMesh.GetIndexCount(0);
			}
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[num];
			ushort[] array2 = new ushort[num2];
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < m_Pass.viewCount; j++)
			{
				global::UnityEngine.Mesh occlusionMesh2 = m_Pass.GetOcclusionMesh(j);
				int[] indices = occlusionMesh2.GetIndices(0);
				occlusionMesh2.vertices.CopyTo(array, num3);
				for (int k = 0; k < occlusionMesh2.vertices.Length; k++)
				{
					array[num3 + k].z = j;
				}
				for (int l = 0; l < indices.Length; l++)
				{
					int num5 = num3 + indices[l];
					array2[num4 + l] = (ushort)num5;
				}
				num3 += occlusionMesh2.vertexCount;
				num4 += indices.Length;
			}
			m_CombinedMesh.vertices = array;
			m_CombinedMesh.SetIndices(array2, global::UnityEngine.MeshTopology.Triangles, 0);
		}
	}
}

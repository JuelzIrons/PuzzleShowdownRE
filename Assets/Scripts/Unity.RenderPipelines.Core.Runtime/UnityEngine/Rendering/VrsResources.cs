namespace UnityEngine.Rendering
{
	internal class VrsResources : global::System.IDisposable
	{
		internal global::UnityEngine.Rendering.ProfilingSampler conversionProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("VrsConversion");

		internal global::UnityEngine.Rendering.ProfilingSampler visualizationProfilingSampler = new global::UnityEngine.Rendering.ProfilingSampler("VrsVisualization");

		internal global::UnityEngine.GraphicsBuffer conversionLutBuffer;

		internal global::UnityEngine.GraphicsBuffer visualizationLutBuffer;

		internal global::UnityEngine.ComputeShader textureComputeShader;

		internal int textureReduceKernel = -1;

		internal int textureCopyKernel = -1;

		internal global::UnityEngine.Vector2Int tileSize;

		internal global::UnityEngine.GraphicsBuffer validatedShadingRateFragmentSizeBuffer;

		private global::UnityEngine.Shader m_VisualizationShader;

		private global::UnityEngine.Material m_VisualizationMaterial;

		internal global::UnityEngine.Material visualizationMaterial
		{
			get
			{
				if (m_VisualizationMaterial == null)
				{
					m_VisualizationMaterial = new global::UnityEngine.Material(m_VisualizationShader);
				}
				return m_VisualizationMaterial;
			}
		}

		internal VrsResources(global::UnityEngine.Rendering.VrsRenderPipelineRuntimeResources resources)
		{
			InitializeResources(resources);
		}

		~VrsResources()
		{
			Dispose();
			global::System.GC.SuppressFinalize(this);
		}

		public void Dispose()
		{
			DisposeResources();
		}

		private void InitializeResources(global::UnityEngine.Rendering.VrsRenderPipelineRuntimeResources resources)
		{
			if (!InitComputeShader(resources))
			{
				DisposeResources();
				return;
			}
			m_VisualizationShader = resources.visualizationShader;
			conversionLutBuffer = resources.conversionLookupTable.CreateBuffer();
			visualizationLutBuffer = resources.visualizationLookupTable.CreateBuffer(forVisualization: true);
			AllocFragmentSizeBuffer();
		}

		private void DisposeResources()
		{
			conversionLutBuffer?.Dispose();
			conversionLutBuffer = null;
			visualizationLutBuffer?.Dispose();
			visualizationLutBuffer = null;
			validatedShadingRateFragmentSizeBuffer?.Dispose();
			validatedShadingRateFragmentSizeBuffer = null;
			m_VisualizationShader = null;
			m_VisualizationMaterial = null;
		}

		private void AllocFragmentSizeBuffer()
		{
			uint[] array = new uint[global::UnityEngine.Rendering.Vrs.shadingRateFragmentSizeCount];
			global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize = global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x1;
			uint value = global::UnityEngine.Rendering.ShadingRateInfo.QueryNativeValue(shadingRateFragmentSize);
			global::UnityEngine.Rendering.ShadingRateFragmentSize[] availableFragmentSizes = global::UnityEngine.Rendering.ShadingRateInfo.availableFragmentSizes;
			foreach (global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize2 in availableFragmentSizes)
			{
				global::System.Array.Fill(array, value, (int)shadingRateFragmentSize, shadingRateFragmentSize2 - shadingRateFragmentSize + 1);
				shadingRateFragmentSize = shadingRateFragmentSize2;
				value = global::UnityEngine.Rendering.ShadingRateInfo.QueryNativeValue(shadingRateFragmentSize);
			}
			global::System.Array.Fill(array, value, (int)shadingRateFragmentSize, (int)(8 - shadingRateFragmentSize + 1));
			validatedShadingRateFragmentSizeBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, array.Length, 4);
			validatedShadingRateFragmentSizeBuffer.SetData(array);
		}

		private bool InitComputeShader(global::UnityEngine.Rendering.VrsRenderPipelineRuntimeResources resources)
		{
			if (!global::UnityEngine.Rendering.ShadingRateInfo.supportsPerImageTile)
			{
				return false;
			}
			if (!global::UnityEngine.SystemInfo.supportsComputeShaders)
			{
				return false;
			}
			tileSize = global::UnityEngine.Rendering.ShadingRateInfo.imageTileSize;
			if (tileSize.x != tileSize.y || (tileSize.x != 8 && tileSize.x != 16 && tileSize.x != 32))
			{
				global::UnityEngine.Debug.LogError($"VRS unsupported tile size: {tileSize.x}x{tileSize.y}.");
				return false;
			}
			global::UnityEngine.ComputeShader computeShader = resources.textureComputeShader;
			if ((object)computeShader != null && computeShader.keywordSpace.keywordCount == 0)
			{
				textureReduceKernel = -1;
				textureCopyKernel = -1;
				return false;
			}
			textureComputeShader = resources.textureComputeShader;
			textureComputeShader.EnableKeyword(string.Format("{0}{1}", "VRS_TILE_SIZE_", tileSize.x));
			textureReduceKernel = TryFindKernel(textureComputeShader, "TextureReduce");
			textureCopyKernel = TryFindKernel(textureComputeShader, "TextureReduce");
			if (textureReduceKernel == -1 || textureCopyKernel == -1)
			{
				return false;
			}
			return true;
		}

		private static int TryFindKernel(global::UnityEngine.ComputeShader computeShader, string name)
		{
			if (!computeShader.HasKernel(name))
			{
				return -1;
			}
			return computeShader.FindKernel(name);
		}
	}
}

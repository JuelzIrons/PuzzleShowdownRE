namespace UnityEngine.Rendering.Universal
{
	internal class LightBatch
	{
		private static readonly global::UnityEngine.Rendering.ProfilingSampler profilingDrawBatched = new global::UnityEngine.Rendering.ProfilingSampler("Light2D Batcher");

		private static readonly int k_BufferOffset = global::UnityEngine.Shader.PropertyToID("_BatchBufferOffset");

		private static int sBatchIndexCounter = 0;

		private int[] subsets = new int[global::UnityEngine.Rendering.Universal.LightBuffer.kMax];

		private global::UnityEngine.Mesh[] lightMeshes = new global::UnityEngine.Mesh[global::UnityEngine.Rendering.Universal.LightBuffer.kMax];

		private global::UnityEngine.Matrix4x4[] matrices = new global::UnityEngine.Matrix4x4[global::UnityEngine.Rendering.Universal.LightBuffer.kMax];

		private global::UnityEngine.Rendering.Universal.LightBuffer[] lightBuffer = new global::UnityEngine.Rendering.Universal.LightBuffer[global::UnityEngine.Rendering.Universal.LightBuffer.kCount];

		private global::UnityEngine.Rendering.Universal.Light2D cachedLight;

		private global::UnityEngine.Material cachedMaterial;

		private int hashCode;

		private int lightCount;

		private int maxIndex;

		private int batchCount;

		private int activeCount;

		private static int batchLightMod => global::UnityEngine.Rendering.Universal.LightBuffer.kLightMod;

		private static float batchRunningIndex => (float)(sBatchIndexCounter++ % global::UnityEngine.Rendering.Universal.LightBuffer.kLightMod) / (float)global::UnityEngine.Rendering.Universal.LightBuffer.kLightMod;

		public static bool isBatchingSupported => false;

		internal global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.PerLight2D> nativeBuffer
		{
			get
			{
				if (lightBuffer[activeCount] == null)
				{
					lightBuffer[activeCount] = new global::UnityEngine.Rendering.Universal.LightBuffer();
				}
				return lightBuffer[activeCount].nativeBuffer;
			}
		}

		internal global::UnityEngine.GraphicsBuffer graphicsBuffer
		{
			get
			{
				if (lightBuffer[activeCount] == null)
				{
					lightBuffer[activeCount] = new global::UnityEngine.Rendering.Universal.LightBuffer();
				}
				return lightBuffer[activeCount].graphicsBuffer;
			}
		}

		internal global::Unity.Collections.NativeArray<int> lightMarker
		{
			get
			{
				if (lightBuffer[activeCount] == null)
				{
					lightBuffer[activeCount] = new global::UnityEngine.Rendering.Universal.LightBuffer();
				}
				return lightBuffer[activeCount].lightMarkers;
			}
		}

		internal static int batchSlotIndex => (int)(batchRunningIndex * (float)global::UnityEngine.Rendering.Universal.LightBuffer.kLightMod);

		internal global::UnityEngine.Rendering.Universal.PerLight2D GetLight(int index)
		{
			return nativeBuffer[index];
		}

		internal void SetLight(int index, global::UnityEngine.Rendering.Universal.PerLight2D light)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.PerLight2D> nativeArray = nativeBuffer;
			nativeArray[index] = light;
		}

		internal static float GetBatchColor()
		{
			return (float)batchSlotIndex / (float)batchLightMod;
		}

		internal static int GetBatchSlotIndex(float channelColor)
		{
			return (int)(channelColor * (float)global::UnityEngine.Rendering.Universal.LightBuffer.kLightMod);
		}

		private static int Hash(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.Material material)
		{
			return ((0x50C5D1F ^ material.GetHashCode()) * 16777619) ^ ((!(light.lightCookieSprite == null)) ? light.lightCookieSprite.GetHashCode() : 0);
		}

		private void Validate()
		{
		}

		private void OnAssemblyReload()
		{
			for (int i = 0; i < global::UnityEngine.Rendering.Universal.LightBuffer.kCount; i++)
			{
				lightBuffer[activeCount].Release();
			}
		}

		private void ResetInternals()
		{
			for (int i = 0; i < global::UnityEngine.Rendering.Universal.LightBuffer.kCount; i++)
			{
				if (lightBuffer[i] != null)
				{
					lightBuffer[i].Reset();
				}
			}
		}

		private void SetBuffer()
		{
			Validate();
			graphicsBuffer.SetData(nativeBuffer, lightCount, lightCount, global::Unity.Mathematics.math.min(global::UnityEngine.Rendering.Universal.LightBuffer.kBatchMax, global::UnityEngine.Rendering.Universal.LightBuffer.kMax - lightCount));
		}

		internal int SlotIndex(int x)
		{
			return lightCount + x;
		}

		internal void Reset()
		{
			if (isBatchingSupported)
			{
				maxIndex = 0;
				hashCode = 0;
				batchCount = 0;
				lightCount = 0;
				activeCount = 0;
				global::UnityEngine.Shader.SetGlobalBuffer("_Light2DBuffer", graphicsBuffer);
			}
		}

		internal bool CanBatch(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.Material material, int index, out int lightHash)
		{
			lightHash = Hash(light, material);
			hashCode = ((hashCode == 0) ? lightHash : hashCode);
			if (batchCount == 0)
			{
				hashCode = lightHash;
			}
			else if (hashCode != lightHash || SlotIndex(index) >= global::UnityEngine.Rendering.Universal.LightBuffer.kMax || lightMarker[index] == 1)
			{
				hashCode = lightHash;
				return false;
			}
			return true;
		}

		internal bool AddBatch(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.Material material, global::UnityEngine.Matrix4x4 mat, global::UnityEngine.Mesh mesh, int subset, int lightHash, int index)
		{
			cachedLight = light;
			cachedMaterial = material;
			matrices[batchCount] = mat;
			lightMeshes[batchCount] = mesh;
			subsets[batchCount] = subset;
			batchCount++;
			maxIndex = global::Unity.Mathematics.math.max(maxIndex, index);
			global::Unity.Collections.NativeArray<int> nativeArray = lightMarker;
			nativeArray[index] = 1;
			return true;
		}

		internal void Flush(global::UnityEngine.Rendering.RasterCommandBuffer cmd)
		{
			if (batchCount > 0)
			{
				using (new global::UnityEngine.Rendering.ProfilingScope(cmd, profilingDrawBatched))
				{
					SetBuffer();
					cmd.SetGlobalInt(k_BufferOffset, lightCount);
					cmd.DrawMultipleMeshes(matrices, lightMeshes, subsets, batchCount, cachedMaterial, -1, null);
				}
				lightCount = lightCount + maxIndex + 1;
			}
			for (int i = 0; i < batchCount; i++)
			{
				lightMeshes[i] = null;
			}
			ResetInternals();
			batchCount = 0;
			maxIndex = 0;
		}
	}
}

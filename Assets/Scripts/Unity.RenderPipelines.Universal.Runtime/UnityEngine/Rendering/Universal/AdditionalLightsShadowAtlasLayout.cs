namespace UnityEngine.Rendering.Universal
{
	internal struct AdditionalLightsShadowAtlasLayout
	{
		internal struct ShadowResolutionRequest
		{
			[global::System.Flags]
			private enum SettingsOptions : ushort
			{
				None = 0,
				SoftShadow = 1,
				PointLightShadow = 2,
				All = ushort.MaxValue
			}

			public ushort visibleLightIndex;

			public ushort perLightShadowSliceIndex;

			public ushort requestedResolution;

			public ushort offsetX;

			public ushort offsetY;

			public ushort allocatedResolution;

			private global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions m_ShadowProperties;

			public bool softShadow
			{
				get
				{
					return m_ShadowProperties.HasFlag(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow);
				}
				set
				{
					if (value)
					{
						m_ShadowProperties |= global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow;
					}
					else
					{
						m_ShadowProperties &= ~global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow;
					}
				}
			}

			public bool pointLightShadow
			{
				get
				{
					return m_ShadowProperties.HasFlag(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow);
				}
				set
				{
					if (value)
					{
						m_ShadowProperties |= global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow;
					}
					else
					{
						m_ShadowProperties &= ~global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow;
					}
				}
			}
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.RectInt> s_UnusedAtlasSquareAreas;

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> s_ShadowResolutionRequests;

		private static float[] s_VisibleLightIndexToCameraSquareDistance;

		private static global::System.Func<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, int> s_CompareShadowResolutionRequest;

		private static global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest[] s_SortedShadowResolutionRequests;

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> m_SortedShadowResolutionRequests;

		private global::Unity.Collections.NativeArray<int> m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex;

		private int m_TotalShadowSlicesCount;

		private int m_TotalShadowResolutionRequestCount;

		private bool m_TooManyShadowMaps;

		private int m_ShadowSlicesScaleFactor;

		private int m_AtlasSize;

		public AdditionalLightsShadowAtlasLayout(global::UnityEngine.Rendering.Universal.UniversalLightData lightData, global::UnityEngine.Rendering.Universal.UniversalShadowData shadowData, global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			bool useStructuredBuffer = global::UnityEngine.Rendering.Universal.RenderingUtils.useStructuredBuffer;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> visibleLights = lightData.visibleLights;
			int length = visibleLights.Length;
			if (s_UnusedAtlasSquareAreas == null)
			{
				s_UnusedAtlasSquareAreas = new global::System.Collections.Generic.List<global::UnityEngine.RectInt>();
			}
			if (s_ShadowResolutionRequests == null)
			{
				s_ShadowResolutionRequests = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest>();
			}
			if (s_VisibleLightIndexToCameraSquareDistance == null || s_VisibleLightIndexToCameraSquareDistance.Length < length)
			{
				s_VisibleLightIndexToCameraSquareDistance = new float[length];
			}
			if (s_CompareShadowResolutionRequest == null)
			{
				s_CompareShadowResolutionRequest = CreateCompareShadowResolutionRequesPredicate();
			}
			if (!useStructuredBuffer)
			{
				int maxVisibleAdditionalLights = global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights;
				if (s_UnusedAtlasSquareAreas.Capacity < maxVisibleAdditionalLights)
				{
					s_UnusedAtlasSquareAreas.Capacity = maxVisibleAdditionalLights;
				}
				if (s_ShadowResolutionRequests.Count < length)
				{
					s_ShadowResolutionRequests.Capacity = length;
					int num = length - s_ShadowResolutionRequests.Count + 1;
					for (int i = 0; i < num; i++)
					{
						s_ShadowResolutionRequests.Add(default(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest));
					}
				}
			}
			s_UnusedAtlasSquareAreas.Clear();
			ushort num2 = 0;
			for (int j = 0; j < visibleLights.Length; j++)
			{
				if (j == lightData.mainLightIndex)
				{
					s_VisibleLightIndexToCameraSquareDistance[j] = float.MaxValue;
					continue;
				}
				ref global::UnityEngine.Rendering.VisibleLight reference = ref visibleLights.UnsafeElementAt(j);
				global::UnityEngine.Light light = reference.light;
				global::UnityEngine.LightType lightType = reference.lightType;
				global::UnityEngine.LightShadows shadows = light.shadows;
				float shadowStrength = light.shadowStrength;
				if (!global::UnityEngine.Rendering.Universal.ShadowUtils.IsValidShadowCastingLight(lightData, j, lightType, shadows, shadowStrength))
				{
					s_VisibleLightIndexToCameraSquareDistance[j] = float.MaxValue;
					continue;
				}
				bool softShadow = shadows == global::UnityEngine.LightShadows.Soft;
				bool pointLightShadow = lightType == global::UnityEngine.LightType.Point;
				ushort visibleLightIndex = (ushort)j;
				ushort requestedResolution = (ushort)shadowData.resolution[j];
				int punctualLightShadowSlicesCount = global::UnityEngine.Rendering.Universal.ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
				for (ushort num3 = 0; num3 < punctualLightShadowSlicesCount; num3++)
				{
					if (num2 >= s_ShadowResolutionRequests.Count)
					{
						s_ShadowResolutionRequests.Add(default(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest));
					}
					global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest value = s_ShadowResolutionRequests[num2];
					value.visibleLightIndex = visibleLightIndex;
					value.perLightShadowSliceIndex = num3;
					value.requestedResolution = requestedResolution;
					value.softShadow = softShadow;
					value.pointLightShadow = pointLightShadow;
					s_ShadowResolutionRequests[num2] = value;
					num2++;
				}
				s_VisibleLightIndexToCameraSquareDistance[j] = (cameraData.worldSpaceCameraPos - light.transform.position).sqrMagnitude;
			}
			if (s_SortedShadowResolutionRequests == null || s_SortedShadowResolutionRequests.Length < num2)
			{
				s_SortedShadowResolutionRequests = new global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest[num2];
			}
			for (int k = 0; k < num2; k++)
			{
				s_SortedShadowResolutionRequests[k] = s_ShadowResolutionRequests[k];
			}
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.Universal.Sorting.s_QuickSortSampler))
			{
				global::UnityEngine.Rendering.Universal.Sorting.QuickSort(s_SortedShadowResolutionRequests, 0, num2 - 1, s_CompareShadowResolutionRequest);
			}
			m_SortedShadowResolutionRequests = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest>(s_SortedShadowResolutionRequests, global::Unity.Collections.Allocator.Temp);
			int num4 = (useStructuredBuffer ? num2 : global::System.Math.Min(num2, global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.maxVisibleAdditionalLights));
			int additionalLightsShadowmapWidth = shadowData.additionalLightsShadowmapWidth;
			bool flag = false;
			int num5 = 1;
			while (!flag && num4 > 0)
			{
				global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest shadowResolutionRequest = m_SortedShadowResolutionRequests[num4 - 1];
				num5 = EstimateScaleFactorNeededToFitAllShadowsInAtlas(in m_SortedShadowResolutionRequests, num4, additionalLightsShadowmapWidth);
				if (shadowResolutionRequest.requestedResolution >= num5 * global::UnityEngine.Rendering.Universal.ShadowUtils.MinimalPunctualLightShadowResolution(shadowResolutionRequest.softShadow))
				{
					flag = true;
				}
				else
				{
					num4 -= global::UnityEngine.Rendering.Universal.ShadowUtils.GetPunctualLightShadowSlicesCount(shadowResolutionRequest.pointLightShadow ? global::UnityEngine.LightType.Point : global::UnityEngine.LightType.Spot);
				}
			}
			for (int l = num4; l < m_SortedShadowResolutionRequests.Length; l++)
			{
				m_SortedShadowResolutionRequests[l] = default(global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest);
			}
			m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex = new global::Unity.Collections.NativeArray<int>(visibleLights.Length, global::Unity.Collections.Allocator.Temp);
			for (int m = 0; m < m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex.Length; m++)
			{
				m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[m] = -1;
			}
			for (int num6 = num4 - 1; num6 >= 0; num6--)
			{
				int visibleLightIndex2 = s_SortedShadowResolutionRequests[num6].visibleLightIndex;
				m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[visibleLightIndex2] = num6;
			}
			bool flag2 = false;
			bool flag3 = false;
			int num7 = num5;
			while (!flag2 && !flag3)
			{
				s_UnusedAtlasSquareAreas.Clear();
				s_UnusedAtlasSquareAreas.Add(new global::UnityEngine.RectInt(0, 0, additionalLightsShadowmapWidth, additionalLightsShadowmapWidth));
				flag2 = true;
				for (int n = 0; n < num4; n++)
				{
					int num8 = m_SortedShadowResolutionRequests[n].requestedResolution / num7;
					if (num8 < global::UnityEngine.Rendering.Universal.ShadowUtils.MinimalPunctualLightShadowResolution(m_SortedShadowResolutionRequests[n].softShadow))
					{
						flag3 = true;
						break;
					}
					bool flag4 = false;
					for (int num9 = 0; num9 < s_UnusedAtlasSquareAreas.Count; num9++)
					{
						global::UnityEngine.RectInt rectInt = s_UnusedAtlasSquareAreas[num9];
						int width = rectInt.width;
						if (width < num8)
						{
							continue;
						}
						int height = rectInt.height;
						int x = rectInt.x;
						int y = rectInt.y;
						ref global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest reference2 = ref m_SortedShadowResolutionRequests.UnsafeElementAtMutable(n);
						reference2.offsetX = (ushort)x;
						reference2.offsetY = (ushort)y;
						reference2.allocatedResolution = (ushort)num8;
						s_UnusedAtlasSquareAreas.RemoveAt(num9);
						int num10 = num4 - n - 1;
						int num11 = 0;
						int num12 = num8;
						int num13 = num8;
						int num14 = x;
						int num15 = y;
						for (; num11 < num10; num11++)
						{
							num14 += num12;
							if (num14 + num12 > x + width)
							{
								num14 = x;
								num15 += num13;
								if (num15 + num13 > y + height)
								{
									break;
								}
							}
							s_UnusedAtlasSquareAreas.Insert(num9 + num11, new global::UnityEngine.RectInt(num14, num15, num12, num13));
						}
						flag4 = true;
						break;
					}
					if (!flag4)
					{
						flag2 = false;
						break;
					}
				}
				if (!flag2 && !flag3)
				{
					num7 *= 2;
				}
			}
			m_TooManyShadowMaps = flag3;
			m_ShadowSlicesScaleFactor = num7;
			m_TotalShadowSlicesCount = num4;
			m_TotalShadowResolutionRequestCount = num2;
			m_AtlasSize = additionalLightsShadowmapWidth;
		}

		public int GetTotalShadowSlicesCount()
		{
			return m_TotalShadowSlicesCount;
		}

		public int GetTotalShadowResolutionRequestCount()
		{
			return m_TotalShadowResolutionRequestCount;
		}

		public bool HasTooManyShadowMaps()
		{
			return m_TooManyShadowMaps;
		}

		public int GetShadowSlicesScaleFactor()
		{
			return m_ShadowSlicesScaleFactor;
		}

		public int GetAtlasSize()
		{
			return m_AtlasSize;
		}

		public bool HasSpaceForLight(int originalVisibleLightIndex)
		{
			return m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[originalVisibleLightIndex] != -1;
		}

		public global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest GetSortedShadowResolutionRequest(int sortedShadowResolutionRequestIndex)
		{
			return m_SortedShadowResolutionRequests[sortedShadowResolutionRequestIndex];
		}

		public global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest GetSliceShadowResolutionRequest(int originalVisibleLightIndex, int sliceIndex)
		{
			int num = m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[originalVisibleLightIndex];
			return m_SortedShadowResolutionRequests[num + sliceIndex];
		}

		public static void ClearStaticCaches()
		{
			s_UnusedAtlasSquareAreas = null;
			s_ShadowResolutionRequests = null;
			s_VisibleLightIndexToCameraSquareDistance = null;
			s_CompareShadowResolutionRequest = null;
			s_SortedShadowResolutionRequests = null;
		}

		private static int EstimateScaleFactorNeededToFitAllShadowsInAtlas(in global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> shadowResolutionRequests, int endIndex, int atlasSize)
		{
			long num = atlasSize * atlasSize;
			long num2 = 0L;
			for (int i = 0; i < endIndex; i++)
			{
				num2 += shadowResolutionRequests[i].requestedResolution * shadowResolutionRequests[i].requestedResolution;
			}
			int num3 = 1;
			while (num2 > num * num3 * num3)
			{
				num3 *= 2;
			}
			return num3;
		}

		private static global::System.Func<global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, int> CreateCompareShadowResolutionRequesPredicate()
		{
			return (global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest curr, global::UnityEngine.Rendering.Universal.AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest other) => (curr.requestedResolution <= other.requestedResolution && (curr.requestedResolution != other.requestedResolution || curr.softShadow || !other.softShadow) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || !(s_VisibleLightIndexToCameraSquareDistance[curr.visibleLightIndex] < s_VisibleLightIndexToCameraSquareDistance[other.visibleLightIndex])) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || s_VisibleLightIndexToCameraSquareDistance[curr.visibleLightIndex] != s_VisibleLightIndexToCameraSquareDistance[other.visibleLightIndex] || curr.visibleLightIndex >= other.visibleLightIndex) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || s_VisibleLightIndexToCameraSquareDistance[curr.visibleLightIndex] != s_VisibleLightIndexToCameraSquareDistance[other.visibleLightIndex] || curr.visibleLightIndex != other.visibleLightIndex || curr.perLightShadowSliceIndex >= other.perLightShadowSliceIndex)) ? 1 : (-1);
		}
	}
}

namespace UnityEngine.Experimental.Rendering
{
	public class XRPass
	{
		private readonly global::System.Collections.Generic.List<global::UnityEngine.Experimental.Rendering.XRView> m_Views;

		private readonly global::UnityEngine.Experimental.Rendering.XROcclusionMesh m_OcclusionMesh;

		private readonly global::UnityEngine.Experimental.Rendering.XRVisibleMesh m_VisibleMesh;

		public bool enabled => viewCount > 0;

		public bool supportsFoveatedRendering
		{
			get
			{
				if (enabled && foveatedRenderingInfo != global::System.IntPtr.Zero)
				{
					return global::UnityEngine.Experimental.Rendering.XRSystem.foveatedRenderingCaps != global::UnityEngine.Rendering.FoveatedRenderingCaps.None;
				}
				return false;
			}
		}

		public bool copyDepth { get; private set; }

		public bool hasMotionVectorPass { get; private set; }

		public bool spaceWarpRightHandedNDC { get; private set; }

		public bool isFirstCameraPass => multipassId == 0;

		public bool isLastCameraPass { get; private set; }

		public int multipassId { get; private set; }

		public int cullingPassId { get; private set; }

		public int renderTargetScaledWidth { get; private set; }

		public int renderTargetScaledHeight { get; private set; }

		public global::UnityEngine.Rendering.RenderTargetIdentifier renderTarget { get; private set; }

		public global::UnityEngine.RenderTextureDescriptor renderTargetDesc { get; private set; }

		public global::UnityEngine.Rendering.RenderTargetIdentifier motionVectorRenderTarget { get; private set; }

		public global::UnityEngine.RenderTextureDescriptor motionVectorRenderTargetDesc { get; private set; }

		public global::UnityEngine.Rendering.ScriptableCullingParameters cullingParams { get; private set; }

		public int viewCount => m_Views.Count;

		public bool singlePassEnabled => viewCount > 1;

		public global::System.IntPtr foveatedRenderingInfo { get; private set; }

		public bool isHDRDisplayOutputActive => global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.active ?? false;

		public global::UnityEngine.ColorGamut hdrDisplayOutputColorGamut => global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.displayColorGamut ?? global::UnityEngine.ColorGamut.sRGB;

		public global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation hdrDisplayOutputInformation => new global::UnityEngine.Rendering.HDROutputUtils.HDRDisplayInformation(global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.maxFullFrameToneMapLuminance ?? (-1), global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.maxToneMapLuminance ?? (-1), global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.minToneMapLuminance ?? (-1), global::UnityEngine.Experimental.Rendering.XRSystem.GetActiveDisplay().hdrOutputSettings?.paperWhiteNits ?? 160f);

		public float occlusionMeshScale { get; private set; }

		public bool hasValidOcclusionMesh => m_OcclusionMesh.hasValidOcclusionMesh;

		public bool hasValidVisibleMesh
		{
			get
			{
				if (m_VisibleMesh.hasValidVisibleMesh)
				{
					return global::UnityEngine.Experimental.Rendering.XRSystem.GetUseVisibilityMesh();
				}
				return false;
			}
		}

		public XRPass()
		{
			m_Views = new global::System.Collections.Generic.List<global::UnityEngine.Experimental.Rendering.XRView>(2);
			m_OcclusionMesh = new global::UnityEngine.Experimental.Rendering.XROcclusionMesh(this);
			m_VisibleMesh = new global::UnityEngine.Experimental.Rendering.XRVisibleMesh(this);
			isLastCameraPass = true;
		}

		public static global::UnityEngine.Experimental.Rendering.XRPass CreateDefault(global::UnityEngine.Experimental.Rendering.XRPassCreateInfo createInfo)
		{
			global::UnityEngine.Experimental.Rendering.XRPass xRPass = global::UnityEngine.Rendering.GenericPool<global::UnityEngine.Experimental.Rendering.XRPass>.Get();
			xRPass.InitBase(createInfo);
			return xRPass;
		}

		public virtual void Release()
		{
			m_VisibleMesh.Dispose();
			global::UnityEngine.Rendering.GenericPool<global::UnityEngine.Experimental.Rendering.XRPass>.Release(this);
		}

		public global::UnityEngine.Matrix4x4 GetProjMatrix(int viewIndex = 0)
		{
			return m_Views[viewIndex].projMatrix;
		}

		public global::UnityEngine.Matrix4x4 GetViewMatrix(int viewIndex = 0)
		{
			return m_Views[viewIndex].viewMatrix;
		}

		public bool GetPrevViewValid(int viewIndex = 0)
		{
			return m_Views[viewIndex].isPrevViewMatrixValid;
		}

		public global::UnityEngine.Matrix4x4 GetPrevViewMatrix(int viewIndex = 0)
		{
			return m_Views[viewIndex].prevViewMatrix;
		}

		public global::UnityEngine.Rect GetViewport(int viewIndex = 0)
		{
			return m_Views[viewIndex].viewport;
		}

		public global::UnityEngine.Mesh GetOcclusionMesh(int viewIndex = 0)
		{
			return m_Views[viewIndex].occlusionMesh;
		}

		public global::UnityEngine.Mesh GetVisibleMesh(int viewIndex = 0)
		{
			return m_Views[viewIndex].visibleMesh;
		}

		public int GetTextureArraySlice(int viewIndex = 0)
		{
			return m_Views[viewIndex].textureArraySlice;
		}

		public void StartSinglePass(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (enabled && singlePassEnabled)
			{
				if (viewCount > global::UnityEngine.Rendering.TextureXR.slices)
				{
					throw new global::System.NotImplementedException($"Invalid XR setup for single-pass, trying to render too many views! Max supported: {(global::UnityEngine.Rendering.TextureXR.slices)}");
				}
				if (global::UnityEngine.SystemInfo.supportsMultiview)
				{
					cmd.EnableKeyword(in global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_MULTIVIEW_ON);
					return;
				}
				cmd.EnableKeyword(in global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_INSTANCING_ON);
				cmd.SetInstanceMultiplier((uint)viewCount);
			}
		}

		public void StartSinglePass(global::UnityEngine.Rendering.IRasterCommandBuffer cmd)
		{
			StartSinglePass((cmd as global::UnityEngine.Rendering.BaseCommandBuffer).m_WrappedCommandBuffer);
		}

		public void StopSinglePass(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			if (enabled && singlePassEnabled)
			{
				if (global::UnityEngine.SystemInfo.supportsMultiview)
				{
					cmd.DisableKeyword(in global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_MULTIVIEW_ON);
					return;
				}
				cmd.DisableKeyword(in global::UnityEngine.Experimental.Rendering.SinglepassKeywords.STEREO_INSTANCING_ON);
				cmd.SetInstanceMultiplier(1u);
			}
		}

		public void StopSinglePass(global::UnityEngine.Rendering.BaseCommandBuffer cmd)
		{
			StopSinglePass(cmd.m_WrappedCommandBuffer);
		}

		public void RenderOcclusionMesh(global::UnityEngine.Rendering.CommandBuffer cmd, bool renderIntoTexture = false)
		{
			if (occlusionMeshScale > 0f)
			{
				m_OcclusionMesh.RenderOcclusionMesh(cmd, occlusionMeshScale, renderIntoTexture);
			}
		}

		public void RenderOcclusionMesh(global::UnityEngine.Rendering.RasterCommandBuffer cmd, bool renderIntoTexture = false)
		{
			if (occlusionMeshScale > 0f)
			{
				m_OcclusionMesh.RenderOcclusionMesh(cmd.m_WrappedCommandBuffer, occlusionMeshScale, renderIntoTexture);
			}
		}

		public void RenderVisibleMeshCustomMaterial(global::UnityEngine.Rendering.RasterCommandBuffer cmd, float occlusionMeshScale, global::UnityEngine.Material material, global::UnityEngine.MaterialPropertyBlock materialBlock, int shaderPass, bool renderIntoTexture = false)
		{
			if (occlusionMeshScale > 0f)
			{
				m_VisibleMesh.RenderVisibleMeshCustomMaterial(cmd.m_WrappedCommandBuffer, occlusionMeshScale, material, materialBlock, shaderPass, renderIntoTexture);
			}
		}

		public void RenderVisibleMeshCustomMaterial(global::UnityEngine.Rendering.CommandBuffer cmd, float occlusionMeshScale, global::UnityEngine.Material material, global::UnityEngine.MaterialPropertyBlock materialBlock, int shaderPass = 0, bool renderIntoTexture = false)
		{
			if (occlusionMeshScale > 0f)
			{
				m_VisibleMesh.RenderVisibleMeshCustomMaterial(cmd, occlusionMeshScale, material, materialBlock, shaderPass, renderIntoTexture);
			}
		}

		public void RenderDebugXRViewsFrustum()
		{
			for (int i = 0; i < m_Views.Count; i++)
			{
				global::UnityEngine.Experimental.Rendering.XRView xRView = m_Views[i];
				global::UnityEngine.Vector3[] array = global::UnityEngine.Rendering.CoreUtils.CalculateViewSpaceCorners(xRView.projMatrix, 10f);
				global::UnityEngine.Vector3 start = -xRView.viewMatrix.GetColumn(3);
				for (int j = 0; j < 4; j++)
				{
					global::UnityEngine.Debug.DrawLine(start, xRView.viewMatrix.MultiplyPoint(array[j]), (i == 0) ? global::UnityEngine.Color.green : global::UnityEngine.Color.red);
				}
			}
		}

		public global::UnityEngine.Vector4 ApplyXRViewCenterOffset(global::UnityEngine.Vector2 center)
		{
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			float num = 0.5f - center.x;
			float num2 = 0.5f - center.y;
			zero.x = m_Views[0].eyeCenterUV.x - num;
			zero.y = m_Views[0].eyeCenterUV.y - num2;
			if (singlePassEnabled)
			{
				zero.z = m_Views[1].eyeCenterUV.x - num;
				zero.w = m_Views[1].eyeCenterUV.y - num2;
			}
			return zero;
		}

		internal void AssignView(int viewId, global::UnityEngine.Experimental.Rendering.XRView xrView)
		{
			if (viewId < 0 || viewId >= m_Views.Count)
			{
				throw new global::System.ArgumentOutOfRangeException("viewId");
			}
			m_Views[viewId] = xrView;
		}

		internal void AssignCullingParams(int cullingPassId, global::UnityEngine.Rendering.ScriptableCullingParameters cullingParams)
		{
			cullingParams.cullingOptions &= ~global::UnityEngine.Rendering.CullingOptions.Stereo;
			this.cullingPassId = cullingPassId;
			this.cullingParams = cullingParams;
		}

		internal void UpdateCombinedOcclusionMesh()
		{
			m_OcclusionMesh.UpdateCombinedMesh();
			m_VisibleMesh.UpdateCombinedMesh();
		}

		public void InitBase(global::UnityEngine.Experimental.Rendering.XRPassCreateInfo createInfo)
		{
			m_Views.Clear();
			copyDepth = createInfo.copyDepth;
			multipassId = createInfo.multipassId;
			AssignCullingParams(createInfo.cullingPassId, createInfo.cullingParameters);
			renderTarget = new global::UnityEngine.Rendering.RenderTargetIdentifier(createInfo.renderTarget, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			renderTargetDesc = createInfo.renderTargetDesc;
			renderTargetScaledWidth = createInfo.renderTargetScaledWidth;
			renderTargetScaledHeight = createInfo.renderTargetScaledHeight;
			motionVectorRenderTarget = new global::UnityEngine.Rendering.RenderTargetIdentifier(createInfo.motionVectorRenderTarget, 0, global::UnityEngine.CubemapFace.Unknown, -1);
			motionVectorRenderTargetDesc = createInfo.motionVectorRenderTargetDesc;
			hasMotionVectorPass = createInfo.hasMotionVectorPass;
			spaceWarpRightHandedNDC = createInfo.spaceWarpRightHandedNDC;
			m_OcclusionMesh.SetMaterial(createInfo.occlusionMeshMaterial);
			occlusionMeshScale = createInfo.occlusionMeshScale;
			foveatedRenderingInfo = createInfo.foveatedRenderingInfo;
			isLastCameraPass = createInfo.isLastCameraPass;
		}

		internal void AddView(global::UnityEngine.Experimental.Rendering.XRView xrView)
		{
			if (m_Views.Count < global::UnityEngine.Rendering.TextureXR.slices)
			{
				m_Views.Add(xrView);
				return;
			}
			throw new global::System.NotImplementedException($"Invalid XR setup for single-pass, trying to add too many views! Max supported: {(global::UnityEngine.Rendering.TextureXR.slices)}");
		}
	}
}

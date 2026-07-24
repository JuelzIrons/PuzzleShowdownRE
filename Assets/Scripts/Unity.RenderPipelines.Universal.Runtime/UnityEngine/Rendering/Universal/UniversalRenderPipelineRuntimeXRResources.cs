namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: Runtime XR", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	public class UniversalRenderPipelineRuntimeXRResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/XR/XROcclusionMesh.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_xrOcclusionMeshPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/XR/XRMirrorView.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_xrMirrorViewPS;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/XR/XRMotionVector.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_xrMotionVector;

		public int version => 0;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		public global::UnityEngine.Shader xrOcclusionMeshPS
		{
			get
			{
				return m_xrOcclusionMeshPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_xrOcclusionMeshPS, value, "m_xrOcclusionMeshPS");
			}
		}

		public global::UnityEngine.Shader xrMirrorViewPS
		{
			get
			{
				return m_xrMirrorViewPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_xrMirrorViewPS, value, "m_xrMirrorViewPS");
			}
		}

		public global::UnityEngine.Shader xrMotionVector
		{
			get
			{
				return m_xrMotionVector;
			}
			set
			{
				this.SetValueAndNotify(ref m_xrMotionVector, value, "m_xrMotionVector");
			}
		}

		internal bool valid
		{
			get
			{
				if (xrOcclusionMeshPS == null)
				{
					return false;
				}
				if (xrMirrorViewPS == null)
				{
					return false;
				}
				if (m_xrMotionVector == null)
				{
					return false;
				}
				return true;
			}
		}
	}
}

namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	[global::UnityEngine.Rendering.SupportedOnRenderPipeline(typeof(global::UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset))]
	[global::UnityEngine.Categorization.CategoryInfo(Name = "R: 2D Renderer", Order = 1000)]
	[global::UnityEngine.HideInInspector]
	internal class Renderer2DResources : global::UnityEngine.Rendering.IRenderPipelineResources, global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private int m_Version;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Light2D.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_LightShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Shadow2D-Projected.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_ProjectedShadowShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Shadow2D-Shadow-Sprite.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_SpriteShadowShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Shadow2D-Unshadow-Sprite.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_SpriteUnshadowShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Shadow2D-Shadow-Geometry.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_GeometryShadowShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/2D/Shadow2D-Unshadow-Geometry.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_GeometryUnshadowShader;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Rendering.ResourcePath("Shaders/Utils/CopyDepth.shader", global::UnityEngine.Rendering.SearchType.ProjectPath)]
		private global::UnityEngine.Shader m_CopyDepthPS;

		public int version => m_Version;

		bool global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild => true;

		internal global::UnityEngine.Shader lightShader
		{
			get
			{
				return m_LightShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_LightShader, value, "m_LightShader");
			}
		}

		internal global::UnityEngine.Shader projectedShadowShader
		{
			get
			{
				return m_ProjectedShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_ProjectedShadowShader, value, "m_ProjectedShadowShader");
			}
		}

		internal global::UnityEngine.Shader spriteShadowShader
		{
			get
			{
				return m_SpriteShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_SpriteShadowShader, value, "m_SpriteShadowShader");
			}
		}

		internal global::UnityEngine.Shader spriteUnshadowShader
		{
			get
			{
				return m_SpriteUnshadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_SpriteUnshadowShader, value, "m_SpriteUnshadowShader");
			}
		}

		internal global::UnityEngine.Shader geometryShadowShader
		{
			get
			{
				return m_GeometryShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_GeometryShadowShader, value, "m_GeometryShadowShader");
			}
		}

		internal global::UnityEngine.Shader geometryUnshadowShader
		{
			get
			{
				return m_GeometryUnshadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref m_GeometryUnshadowShader, value, "m_GeometryUnshadowShader");
			}
		}

		internal global::UnityEngine.Shader copyDepthPS
		{
			get
			{
				return m_CopyDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref m_CopyDepthPS, value, "m_CopyDepthPS");
			}
		}
	}
}

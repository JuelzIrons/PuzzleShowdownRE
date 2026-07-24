namespace UnityEngine.Rendering.Universal
{
	[global::UnityEngine.ExcludeFromPreset]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	[global::UnityEngine.Tooltip("Render Objects simplifies the injection of additional render passes by exposing a selection of commonly used settings.")]
	public class RenderObjects : global::UnityEngine.Rendering.Universal.ScriptableRendererFeature
	{
		[global::System.Serializable]
		public class RenderObjectsSettings
		{
			public enum OverrideMaterialMode
			{
				None = 0,
				Material = 1,
				Shader = 2
			}

			public string passTag = "RenderObjectsFeature";

			public global::UnityEngine.Rendering.Universal.RenderPassEvent Event = global::UnityEngine.Rendering.Universal.RenderPassEvent.AfterRenderingOpaques;

			public global::UnityEngine.Rendering.Universal.RenderObjects.FilterSettings filterSettings = new global::UnityEngine.Rendering.Universal.RenderObjects.FilterSettings();

			public global::UnityEngine.Material overrideMaterial;

			public int overrideMaterialPassIndex;

			public global::UnityEngine.Shader overrideShader;

			public int overrideShaderPassIndex;

			public global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings.OverrideMaterialMode overrideMode = global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Material;

			public bool overrideDepthState;

			public global::UnityEngine.Rendering.CompareFunction depthCompareFunction = global::UnityEngine.Rendering.CompareFunction.LessEqual;

			public bool enableWrite = true;

			public global::UnityEngine.Rendering.Universal.StencilStateData stencilSettings = new global::UnityEngine.Rendering.Universal.StencilStateData();

			public global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings cameraSettings = new global::UnityEngine.Rendering.Universal.RenderObjects.CustomCameraSettings();
		}

		[global::System.Serializable]
		public class FilterSettings
		{
			public global::UnityEngine.Rendering.Universal.RenderQueueType RenderQueueType;

			public global::UnityEngine.LayerMask LayerMask;

			public string[] PassNames;

			public FilterSettings()
			{
				RenderQueueType = global::UnityEngine.Rendering.Universal.RenderQueueType.Opaque;
				LayerMask = 0;
			}
		}

		[global::System.Serializable]
		public class CustomCameraSettings
		{
			public bool overrideCamera;

			public bool restoreCamera = true;

			public global::UnityEngine.Vector4 offset;

			public float cameraFieldOfView = 60f;
		}

		public global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings settings = new global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings();

		private global::UnityEngine.Rendering.Universal.RenderObjectsPass renderObjectsPass;

		public override void Create()
		{
			global::UnityEngine.Rendering.Universal.RenderObjects.FilterSettings filterSettings = settings.filterSettings;
			if (settings.Event < global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses)
			{
				settings.Event = global::UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPrePasses;
			}
			renderObjectsPass = new global::UnityEngine.Rendering.Universal.RenderObjectsPass(settings.passTag, settings.Event, filterSettings.PassNames, filterSettings.RenderQueueType, filterSettings.LayerMask, settings.cameraSettings);
			switch (settings.overrideMode)
			{
			case global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings.OverrideMaterialMode.None:
				renderObjectsPass.overrideMaterial = null;
				renderObjectsPass.overrideShader = null;
				break;
			case global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Material:
				renderObjectsPass.overrideMaterial = settings.overrideMaterial;
				renderObjectsPass.overrideMaterialPassIndex = settings.overrideMaterialPassIndex;
				renderObjectsPass.overrideShader = null;
				break;
			case global::UnityEngine.Rendering.Universal.RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Shader:
				renderObjectsPass.overrideMaterial = null;
				renderObjectsPass.overrideShader = settings.overrideShader;
				renderObjectsPass.overrideShaderPassIndex = settings.overrideShaderPassIndex;
				break;
			}
			if (settings.overrideDepthState)
			{
				renderObjectsPass.SetDepthState(settings.enableWrite, settings.depthCompareFunction);
			}
			if (settings.stencilSettings.overrideStencilState)
			{
				renderObjectsPass.SetStencilState(settings.stencilSettings.stencilReference, settings.stencilSettings.stencilCompareFunction, settings.stencilSettings.passOperation, settings.stencilSettings.failOperation, settings.stencilSettings.zFailOperation);
			}
		}

		public override void AddRenderPasses(global::UnityEngine.Rendering.Universal.ScriptableRenderer renderer, ref global::UnityEngine.Rendering.Universal.RenderingData renderingData)
		{
			if (renderingData.cameraData.cameraType != global::UnityEngine.CameraType.Preview && !global::UnityEngine.Rendering.Universal.UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				renderer.EnqueuePass(renderObjectsPass);
			}
		}
	}
}

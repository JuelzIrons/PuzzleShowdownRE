namespace UnityEngine.Rendering.Universal
{
	internal class PostProcessMaterialLibrary
	{
		public readonly global::UnityEngine.Material stopNaN;

		public readonly global::UnityEngine.Material subpixelMorphologicalAntialiasing;

		public readonly global::UnityEngine.Material gaussianDepthOfField;

		public readonly global::UnityEngine.Material gaussianDepthOfFieldCoC;

		public readonly global::UnityEngine.Material bokehDepthOfField;

		public readonly global::UnityEngine.Material bokehDepthOfFieldCoC;

		public readonly global::UnityEngine.Material temporalAntialiasing;

		public readonly global::UnityEngine.Material motionBlur;

		public readonly global::UnityEngine.Material paniniProjection;

		public readonly global::UnityEngine.Material bloom;

		public readonly global::UnityEngine.Material[] bloomUpsample;

		public readonly global::UnityEngine.Material lensFlareScreenSpace;

		public readonly global::UnityEngine.Material lensFlareDataDriven;

		public readonly global::UnityEngine.Material uber;

		public readonly global::UnityEngine.Material scalingSetup;

		public readonly global::UnityEngine.Material easu;

		public readonly global::UnityEngine.Material finalPass;

		internal global::UnityEngine.Rendering.Universal.PostProcessData m_Resources;

		public global::UnityEngine.Rendering.Universal.PostProcessData resources => m_Resources;

		public PostProcessMaterialLibrary(global::UnityEngine.Rendering.Universal.PostProcessData data)
		{
			stopNaN = Load(data.shaders.stopNanPS);
			subpixelMorphologicalAntialiasing = Load(data.shaders.subpixelMorphologicalAntialiasingPS);
			gaussianDepthOfField = Load(data.shaders.gaussianDepthOfFieldPS);
			gaussianDepthOfFieldCoC = Load(data.shaders.gaussianDepthOfFieldPS);
			bokehDepthOfField = Load(data.shaders.bokehDepthOfFieldPS);
			bokehDepthOfFieldCoC = Load(data.shaders.bokehDepthOfFieldPS);
			temporalAntialiasing = Load(data.shaders.temporalAntialiasingPS);
			motionBlur = Load(data.shaders.cameraMotionBlurPS);
			paniniProjection = Load(data.shaders.paniniProjectionPS);
			bloom = Load(data.shaders.bloomPS);
			lensFlareScreenSpace = Load(data.shaders.LensFlareScreenSpacePS);
			lensFlareDataDriven = Load(data.shaders.LensFlareDataDrivenPS);
			uber = Load(data.shaders.uberPostPS);
			scalingSetup = Load(data.shaders.scalingSetupPS);
			easu = Load(data.shaders.easuPS);
			finalPass = Load(data.shaders.finalPostPassPS);
			bloomUpsample = new global::UnityEngine.Material[16];
			for (uint num = 0u; num < 16; num++)
			{
				bloomUpsample[num] = Load(data.shaders.bloomPS);
			}
			m_Resources = data;
		}

		private global::UnityEngine.Material Load(global::UnityEngine.Shader shader)
		{
			if (shader == null)
			{
				global::UnityEngine.Debug.LogErrorFormat("Missing shader. PostProcessing render passes will not execute. Check for missing reference in the renderer resources.");
				return null;
			}
			if (!shader.isSupported)
			{
				return null;
			}
			return global::UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(shader);
		}

		internal void Cleanup()
		{
			global::UnityEngine.Rendering.CoreUtils.Destroy(stopNaN);
			global::UnityEngine.Rendering.CoreUtils.Destroy(subpixelMorphologicalAntialiasing);
			global::UnityEngine.Rendering.CoreUtils.Destroy(gaussianDepthOfField);
			global::UnityEngine.Rendering.CoreUtils.Destroy(gaussianDepthOfFieldCoC);
			global::UnityEngine.Rendering.CoreUtils.Destroy(bokehDepthOfField);
			global::UnityEngine.Rendering.CoreUtils.Destroy(bokehDepthOfFieldCoC);
			global::UnityEngine.Rendering.CoreUtils.Destroy(temporalAntialiasing);
			global::UnityEngine.Rendering.CoreUtils.Destroy(motionBlur);
			global::UnityEngine.Rendering.CoreUtils.Destroy(paniniProjection);
			global::UnityEngine.Rendering.CoreUtils.Destroy(bloom);
			global::UnityEngine.Rendering.CoreUtils.Destroy(lensFlareScreenSpace);
			global::UnityEngine.Rendering.CoreUtils.Destroy(lensFlareDataDriven);
			global::UnityEngine.Rendering.CoreUtils.Destroy(scalingSetup);
			global::UnityEngine.Rendering.CoreUtils.Destroy(uber);
			global::UnityEngine.Rendering.CoreUtils.Destroy(easu);
			global::UnityEngine.Rendering.CoreUtils.Destroy(finalPass);
			for (uint num = 0u; num < 16; num++)
			{
				global::UnityEngine.Rendering.CoreUtils.Destroy(bloomUpsample[num]);
			}
		}
	}
}

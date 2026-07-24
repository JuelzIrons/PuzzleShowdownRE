namespace UnityEngine.Rendering.LookDev
{
	public interface IDataProvider
	{
		global::System.Collections.Generic.IEnumerable<string> supportedDebugModes { get; }

		void FirstInitScene(global::UnityEngine.Rendering.LookDev.StageRuntimeInterface stage);

		void UpdateSky(global::UnityEngine.Camera camera, global::UnityEngine.Rendering.LookDev.Sky sky, global::UnityEngine.Rendering.LookDev.StageRuntimeInterface stage);

		void UpdateDebugMode(int debugIndex);

		void GetShadowMask(ref global::UnityEngine.RenderTexture output, global::UnityEngine.Rendering.LookDev.StageRuntimeInterface stage);

		void OnBeginRendering(global::UnityEngine.Rendering.LookDev.StageRuntimeInterface stage);

		void OnEndRendering(global::UnityEngine.Rendering.LookDev.StageRuntimeInterface stage);

		void Cleanup(global::UnityEngine.Rendering.LookDev.StageRuntimeInterface SRI);
	}
}

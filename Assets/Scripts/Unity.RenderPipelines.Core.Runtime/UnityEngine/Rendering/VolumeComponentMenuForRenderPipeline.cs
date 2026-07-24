namespace UnityEngine.Rendering
{
	[global::System.Obsolete("VolumeComponentMenuForRenderPipelineAttribute is deprecated. Use VolumeComponentMenu with SupportedOnRenderPipeline instead. #from(2023.1)")]
	public class VolumeComponentMenuForRenderPipeline : global::UnityEngine.Rendering.VolumeComponentMenu
	{
		public global::System.Type[] pipelineTypes { get; }

		public VolumeComponentMenuForRenderPipeline(string menu, params global::System.Type[] pipelineTypes)
			: base(menu)
		{
			if (pipelineTypes == null)
			{
				throw new global::System.Exception("Specify a list of supported pipeline.");
			}
			foreach (global::System.Type type in pipelineTypes)
			{
				if (!typeof(global::UnityEngine.Rendering.RenderPipeline).IsAssignableFrom(type))
				{
					throw new global::System.Exception($"You can only specify types that inherit from {(typeof(global::UnityEngine.Rendering.RenderPipeline))}, please check {type}");
				}
			}
			this.pipelineTypes = pipelineTypes;
		}
	}
}

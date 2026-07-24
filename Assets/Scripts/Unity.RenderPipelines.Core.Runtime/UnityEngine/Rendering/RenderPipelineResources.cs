namespace UnityEngine.Rendering
{
	public abstract class RenderPipelineResources : global::UnityEngine.ScriptableObject
	{
		protected virtual string packagePath => null;

		internal string packagePath_Internal => packagePath;
	}
}

namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	public class StencilStateData
	{
		public bool overrideStencilState;

		public int stencilReference;

		public global::UnityEngine.Rendering.CompareFunction stencilCompareFunction = global::UnityEngine.Rendering.CompareFunction.Always;

		public global::UnityEngine.Rendering.StencilOp passOperation;

		public global::UnityEngine.Rendering.StencilOp failOperation;

		public global::UnityEngine.Rendering.StencilOp zFailOperation;
	}
}

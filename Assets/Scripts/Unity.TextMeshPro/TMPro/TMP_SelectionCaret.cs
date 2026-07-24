namespace TMPro
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.CanvasRenderer))]
	public class TMP_SelectionCaret : global::UnityEngine.UI.MaskableGraphic
	{
		public override void Cull(global::UnityEngine.Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				global::UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
			else
			{
				base.Cull(clipRect, validRect);
			}
		}

		protected override void UpdateGeometry()
		{
		}
	}
}

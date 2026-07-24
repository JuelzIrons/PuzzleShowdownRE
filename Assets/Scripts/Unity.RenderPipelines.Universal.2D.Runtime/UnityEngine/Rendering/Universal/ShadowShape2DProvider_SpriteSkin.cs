namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class ShadowShape2DProvider_SpriteSkin : global::UnityEngine.Rendering.Universal.ShadowShape2DProvider
	{
		private const float k_InitialTrim = 0.05f;

		private global::UnityEngine.Rendering.Universal.ShadowShape2D m_PersistantShapeData;

		private int m_LastDeformedVertexHash;

		private void TryToSetPersistantShapeData(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape, bool force)
		{
			if (spriteSkin != null)
			{
				persistantShadowShape.SetShape(spriteSkin.outlineVertices, spriteSkin.outlineIndices, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology.Lines);
			}
		}

		private void UpdatePersistantShapeData(global::UnityEngine.SpriteRenderer spriteRenderer)
		{
			spriteRenderer.TryGetComponent<global::UnityEngine.U2D.Animation.SpriteSkin>(out var component);
			if (component != null)
			{
				TryToSetPersistantShapeData(component, m_PersistantShapeData, force: true);
			}
		}

		public override int Priority()
		{
			return 10;
		}

		public override bool IsShapeSource(global::UnityEngine.Component sourceComponent)
		{
			return sourceComponent is global::UnityEngine.U2D.Animation.SpriteSkin;
		}

		public override void OnPersistantDataCreated(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin = (global::UnityEngine.U2D.Animation.SpriteSkin)sourceComponent;
			spriteSkin.TryGetComponent<global::UnityEngine.SpriteRenderer>(out var component);
			float trimEdgeFromBounds = ShadowShapeProvider2DUtility.GetTrimEdgeFromBounds(component.bounds, 0.05f);
			persistantShadowShape.SetDefaultTrim(trimEdgeFromBounds);
			TryToSetPersistantShapeData(spriteSkin, persistantShadowShape, force: true);
		}

		public override void OnBeforeRender(global::UnityEngine.Component sourceComponent, global::UnityEngine.Bounds worldCullingBounds, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin = (global::UnityEngine.U2D.Animation.SpriteSkin)sourceComponent;
			if (spriteSkin != null && spriteSkin.vertexDeformationHash != m_LastDeformedVertexHash)
			{
				spriteSkin.TryGetComponent<global::UnityEngine.SpriteRenderer>(out var component);
				persistantShadowShape.SetFlip(component.flipX, component.flipY);
				TryToSetPersistantShapeData(spriteSkin, persistantShadowShape, force: false);
				m_LastDeformedVertexHash = spriteSkin.vertexDeformationHash;
			}
		}
	}
}

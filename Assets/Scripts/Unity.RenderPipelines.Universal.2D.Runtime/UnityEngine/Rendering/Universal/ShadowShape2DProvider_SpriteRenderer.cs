namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class ShadowShape2DProvider_SpriteRenderer : global::UnityEngine.Rendering.Universal.ShadowShape2DProvider
	{
		private const float k_InitialTrim = 0.05f;

		private global::UnityEngine.Rendering.Universal.ShadowShape2D m_PersistantShapeData;

		private global::UnityEngine.SpriteDrawMode m_CurrentDrawMode;

		private global::UnityEngine.Vector2 m_CurrentDrawModeSize;

		private void SetFullRectShapeData(global::UnityEngine.SpriteRenderer spriteRenderer, global::UnityEngine.Rendering.Universal.ShadowShape2D shadowShape2D)
		{
			if (spriteRenderer.drawMode != global::UnityEngine.SpriteDrawMode.Simple)
			{
				global::UnityEngine.Sprite sprite = spriteRenderer.sprite;
				global::UnityEngine.Vector2 size = spriteRenderer.size;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector2(size.x * sprite.pivot.x / sprite.rect.width, size.y * sprite.pivot.y / sprite.rect.height);
				global::UnityEngine.Rect rect = new global::UnityEngine.Rect(-vector, new global::UnityEngine.Vector2(size.x, size.y));
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(4, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<int> indices = new global::Unity.Collections.NativeArray<int>(8, global::Unity.Collections.Allocator.Temp);
				vertices[0] = new global::UnityEngine.Vector3(rect.min.x, rect.min.y);
				vertices[1] = new global::UnityEngine.Vector3(rect.min.x, rect.max.y);
				vertices[2] = new global::UnityEngine.Vector3(rect.max.x, rect.max.y);
				vertices[3] = new global::UnityEngine.Vector3(rect.max.x, rect.min.y);
				indices[0] = 0;
				indices[1] = 1;
				indices[2] = 1;
				indices[3] = 2;
				indices[4] = 2;
				indices[5] = 3;
				indices[6] = 3;
				indices[7] = 0;
				shadowShape2D.SetShape(vertices, indices, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology.Lines);
				vertices.Dispose();
				indices.Dispose();
			}
		}

		private void SetPersistantShapeData(global::UnityEngine.Sprite sprite, global::UnityEngine.Rendering.Universal.ShadowShape2D shadowShape2D, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertexSlice)
		{
			if (shadowShape2D != null)
			{
				global::Unity.Collections.NativeArray<ushort> indices = global::UnityEngine.U2D.SpriteDataAccessExtensions.GetIndices(sprite);
				global::Unity.Collections.NativeArray<int> indices2 = new global::Unity.Collections.NativeArray<int>(indices.Length, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(vertexSlice.Length, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < indices2.Length; i++)
				{
					indices2[i] = indices[i];
				}
				for (int j = 0; j < vertices.Length; j++)
				{
					vertices[j] = vertexSlice[j];
				}
				shadowShape2D.SetShape(vertices, indices2, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology.Triangles);
				vertices.Dispose();
				indices2.Dispose();
			}
		}

		private void TryToSetPersistantShapeData(global::UnityEngine.SpriteRenderer spriteRenderer, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape, bool force)
		{
			if (spriteRenderer != null && spriteRenderer.sprite != null)
			{
				if (spriteRenderer.drawMode != global::UnityEngine.SpriteDrawMode.Simple && (spriteRenderer.size.x != m_CurrentDrawModeSize.x || spriteRenderer.size.y != m_CurrentDrawModeSize.y || spriteRenderer.drawMode != m_CurrentDrawMode || force))
				{
					m_CurrentDrawModeSize = spriteRenderer.size;
					SetFullRectShapeData(spriteRenderer, persistantShadowShape);
				}
				else if (spriteRenderer.drawMode != m_CurrentDrawMode || force)
				{
					global::UnityEngine.Sprite sprite = spriteRenderer.sprite;
					global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertexAttribute = global::UnityEngine.U2D.SpriteDataAccessExtensions.GetVertexAttribute<global::UnityEngine.Vector3>(sprite, global::UnityEngine.Rendering.VertexAttribute.Position);
					SetPersistantShapeData(sprite, m_PersistantShapeData, vertexAttribute);
				}
				m_CurrentDrawMode = spriteRenderer.drawMode;
			}
		}

		private void UpdatePersistantShapeData(global::UnityEngine.SpriteRenderer spriteRenderer)
		{
			TryToSetPersistantShapeData(spriteRenderer, m_PersistantShapeData, force: true);
		}

		public override int Priority()
		{
			return 1;
		}

		public override bool IsShapeSource(global::UnityEngine.Component sourceComponent)
		{
			return sourceComponent is global::UnityEngine.SpriteRenderer;
		}

		public override void OnPersistantDataCreated(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.SpriteRenderer spriteRenderer = (global::UnityEngine.SpriteRenderer)sourceComponent;
			m_PersistantShapeData = persistantShadowShape as global::UnityEngine.Rendering.Universal.ShadowMesh2D;
			if (spriteRenderer.sprite != null)
			{
				float trimEdgeFromBounds = ShadowShapeProvider2DUtility.GetTrimEdgeFromBounds(spriteRenderer.bounds, 0.05f);
				persistantShadowShape.SetDefaultTrim(trimEdgeFromBounds);
			}
			TryToSetPersistantShapeData(spriteRenderer, persistantShadowShape, force: true);
		}

		public override void OnBeforeRender(global::UnityEngine.Component sourceComponent, global::UnityEngine.Bounds worldCullingBounds, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.SpriteRenderer spriteRenderer = (global::UnityEngine.SpriteRenderer)sourceComponent;
			persistantShadowShape.SetFlip(spriteRenderer.flipX, spriteRenderer.flipY);
			TryToSetPersistantShapeData(spriteRenderer, persistantShadowShape, force: false);
		}

		public override void Enabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.SpriteRenderer obj = (global::UnityEngine.SpriteRenderer)sourceComponent;
			m_PersistantShapeData = persistantShadowShape;
			obj.RegisterSpriteChangeCallback(UpdatePersistantShapeData);
		}

		public override void Disabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			((global::UnityEngine.SpriteRenderer)sourceComponent).UnregisterSpriteChangeCallback(UpdatePersistantShapeData);
		}
	}
}

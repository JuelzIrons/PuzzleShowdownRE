namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class ShadowShape2DProvider_SpriteShape : global::UnityEngine.Rendering.Universal.ShadowShape2DProvider
	{
		private const float k_InitialTrim = 0.02f;

		internal void UpdateShadows(global::UnityEngine.U2D.SpriteShapeController spriteShapeController, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShapeData)
		{
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> shadowShapeData = spriteShapeController.GetShadowShapeData();
			int length = shadowShapeData.Length;
			if (length > 0)
			{
				bool flag = shadowShapeData[0].x == shadowShapeData[length - 1].x && shadowShapeData[0].y == shadowShapeData[length - 1].y;
				int num = (flag ? (length - 1) : length);
				int num2 = 2 * length;
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(num, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<int> indices = new global::Unity.Collections.NativeArray<int>(num2 - 2, global::Unity.Collections.Allocator.Temp);
				for (int i = 0; i < num; i++)
				{
					vertices[i] = new global::UnityEngine.Vector3(shadowShapeData[i].x, shadowShapeData[i].y, 0f);
				}
				for (int j = 0; j < length - 1; j++)
				{
					int num3 = 2 * j;
					indices[num3] = j;
					indices[num3 + 1] = j + 1;
				}
				if (flag)
				{
					int num4 = 2 * num;
					indices[num4 - 1] = 0;
				}
				persistantShapeData.SetShape(vertices, indices, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology.Lines);
				vertices.Dispose();
				indices.Dispose();
			}
			shadowShapeData.Dispose();
		}

		public override int Priority()
		{
			return 10;
		}

		public override void Enabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			((global::UnityEngine.U2D.SpriteShapeController)sourceComponent).ForceShadowShapeUpdate(forceUpdate: true);
		}

		public override void Disabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			((global::UnityEngine.U2D.SpriteShapeController)sourceComponent).ForceShadowShapeUpdate(forceUpdate: false);
		}

		public override bool IsShapeSource(global::UnityEngine.Component sourceComponent)
		{
			return sourceComponent as global::UnityEngine.U2D.SpriteShapeController;
		}

		public override void OnPersistantDataCreated(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.U2D.SpriteShapeController spriteShapeController = (global::UnityEngine.U2D.SpriteShapeController)sourceComponent;
			spriteShapeController.TryGetComponent<global::UnityEngine.U2D.SpriteShapeRenderer>(out var component);
			float trimEdgeFromBounds = ShadowShapeProvider2DUtility.GetTrimEdgeFromBounds(component.bounds, 0.02f);
			persistantShadowShape.SetDefaultTrim(trimEdgeFromBounds);
			UpdateShadows(spriteShapeController, persistantShadowShape);
		}

		public override void OnBeforeRender(global::UnityEngine.Component sourceComponent, global::UnityEngine.Bounds worldCullingBounds, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			UpdateShadows((global::UnityEngine.U2D.SpriteShapeController)sourceComponent, persistantShadowShape);
		}
	}
}

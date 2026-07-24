namespace UnityEngine.Rendering.Universal
{
	[global::System.Serializable]
	internal class ShadowShape2DProvider_Collider2D : global::UnityEngine.Rendering.Universal.ShadowShape2DProvider
	{
		private struct MinMaxBounds
		{
			public global::UnityEngine.Vector3 min;

			public global::UnityEngine.Vector3 max;

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			public bool Intersects(ref global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds bounds)
			{
				if (min.x <= bounds.max.x && max.x >= bounds.min.x && min.y <= bounds.max.y && max.y >= bounds.min.y && min.z <= bounds.max.z)
				{
					return max.z >= bounds.min.z;
				}
				return false;
			}

			public MinMaxBounds(ref global::UnityEngine.Bounds bounds)
			{
				min = bounds.min;
				max = bounds.max;
			}
		}

		private const float k_InitialTrim = 0.05f;

		private global::System.Collections.Generic.List<global::UnityEngine.Bounds> m_ShadowShapeBounds;

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds> m_ShadowShapeMinMaxBounds;

		private global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds m_ShadowCombinedShapeMinMaxBounds;

		private global::UnityEngine.Bounds m_LastWorldCullingBounds;

		private global::UnityEngine.Matrix4x4 m_LastColliderSpace;

		private bool m_ShadowDirty = true;

		private uint m_ShadowStateHash;

		private global::UnityEngine.PhysicsShapeGroup2D m_ShadowShapeGroup;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static bool CompareApproximately(ref global::UnityEngine.Bounds a, ref global::UnityEngine.Bounds b)
		{
			if (!((a.min - b.min).sqrMagnitude > global::UnityEngine.Mathf.Epsilon))
			{
				return !((a.max - b.max).sqrMagnitude > global::UnityEngine.Mathf.Epsilon);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void TransformBounds2D(global::UnityEngine.Matrix4x4 transform, ref global::UnityEngine.Bounds bounds)
		{
			global::UnityEngine.Vector3 center = transform.MultiplyPoint(bounds.center);
			global::UnityEngine.Vector3 extents = bounds.extents;
			global::UnityEngine.Vector3 vector = transform.MultiplyVector(new global::UnityEngine.Vector3(extents.x, 0f, 0f));
			global::UnityEngine.Vector3 vector2 = transform.MultiplyVector(new global::UnityEngine.Vector3(0f, extents.y, 0f));
			extents.x = global::System.MathF.Abs(vector.x) + global::System.MathF.Abs(vector2.x);
			extents.y = global::System.MathF.Abs(vector.y) + global::System.MathF.Abs(vector2.y);
			bounds = new global::UnityEngine.Bounds
			{
				center = center,
				extents = extents
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		private static void ClearShapes(global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShapeObject)
		{
			persistantShapeObject.SetShape(default(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>), default(global::Unity.Collections.NativeArray<int>), global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology.Lines, global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder.CounterClockwise);
		}

		private void CalculateShadows(global::UnityEngine.Collider2D collider, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShapeObject, global::UnityEngine.Bounds worldCullingBounds)
		{
			if (m_ShadowShapeGroup == null)
			{
				m_ShadowShapeGroup = new global::UnityEngine.PhysicsShapeGroup2D(collider.shapeCount);
			}
			if (m_ShadowShapeBounds == null)
			{
				m_ShadowShapeBounds = new global::System.Collections.Generic.List<global::UnityEngine.Bounds>(collider.shapeCount);
			}
			if (m_ShadowShapeMinMaxBounds == null)
			{
				m_ShadowShapeMinMaxBounds = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds>();
			}
			global::UnityEngine.Rigidbody2D attachedRigidbody = collider.attachedRigidbody;
			global::UnityEngine.Matrix4x4 matrix4x = (attachedRigidbody ? attachedRigidbody.localToWorldMatrix : global::UnityEngine.Matrix4x4.identity);
			uint shapeHash = collider.GetShapeHash();
			if (shapeHash != m_ShadowStateHash)
			{
				m_ShadowStateHash = shapeHash;
				m_ShadowShapeGroup.Clear();
				if (collider.shapeCount == 0)
				{
					ClearShapes(persistantShapeObject);
					return;
				}
				if (collider.GetShapes(m_ShadowShapeGroup) == 0)
				{
					return;
				}
				m_LastWorldCullingBounds = worldCullingBounds;
				global::UnityEngine.Bounds bounds = collider.GetShapeBounds(m_ShadowShapeBounds, useRadii: true, useWorldSpace: false);
				m_ShadowCombinedShapeMinMaxBounds = new global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds(ref bounds);
				m_ShadowShapeMinMaxBounds.Clear();
				m_ShadowShapeMinMaxBounds.Capacity = m_ShadowShapeBounds.Capacity;
				for (int i = 0; i < m_ShadowShapeBounds.Count; i++)
				{
					global::UnityEngine.Bounds bounds2 = m_ShadowShapeBounds[i];
					m_ShadowShapeMinMaxBounds.Add(new global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds(ref bounds2));
				}
				m_ShadowDirty = true;
			}
			else
			{
				if (matrix4x.Equals(m_LastColliderSpace) && CompareApproximately(ref m_LastWorldCullingBounds, ref worldCullingBounds))
				{
					return;
				}
				m_LastWorldCullingBounds = worldCullingBounds;
				m_ShadowDirty = true;
			}
			m_LastColliderSpace = matrix4x;
			if (!m_ShadowDirty || m_ShadowShapeGroup.shapeCount == 0)
			{
				return;
			}
			m_ShadowDirty = false;
			TransformBounds2D(global::UnityEngine.Matrix4x4.Inverse(matrix4x), ref worldCullingBounds);
			global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds bounds3 = new global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds(ref worldCullingBounds);
			if (!m_ShadowCombinedShapeMinMaxBounds.Intersects(ref bounds3))
			{
				ClearShapes(persistantShapeObject);
				return;
			}
			int shapeCount = m_ShadowShapeGroup.shapeCount;
			global::System.Collections.Generic.List<global::UnityEngine.PhysicsShape2D> groupShapes = m_ShadowShapeGroup.groupShapes;
			global::System.Collections.Generic.List<global::UnityEngine.Vector2> groupVertices = m_ShadowShapeGroup.groupVertices;
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(shapeCount, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < shapeCount; j++)
			{
				if (m_ShadowShapeMinMaxBounds[j].Intersects(ref bounds3))
				{
					global::UnityEngine.PhysicsShape2D physicsShape2D = groupShapes[j];
					int vertexCount = physicsShape2D.vertexCount;
					global::UnityEngine.PhysicsShapeType2D shapeType = physicsShape2D.shapeType;
					num += vertexCount;
					switch (shapeType)
					{
					case global::UnityEngine.PhysicsShapeType2D.Circle:
					case global::UnityEngine.PhysicsShapeType2D.Capsule:
						num2 += 2;
						break;
					case global::UnityEngine.PhysicsShapeType2D.Polygon:
						num2 += 2 * vertexCount;
						break;
					case global::UnityEngine.PhysicsShapeType2D.Edges:
					{
						global::UnityEngine.Vector2 vector = groupVertices[physicsShape2D.vertexStartIndex];
						bool flag = (groupVertices[physicsShape2D.vertexStartIndex + physicsShape2D.vertexCount - 1] - vector).sqrMagnitude > global::UnityEngine.Mathf.Epsilon;
						num2 += 2 * (flag ? (vertexCount - 1) : vertexCount);
						break;
					}
					}
					nativeArray[num3++] = j;
				}
			}
			if (num3 > 0)
			{
				global::Unity.Collections.NativeArray<float> radii = new global::Unity.Collections.NativeArray<float>(num, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(num, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<int> indices = new global::Unity.Collections.NativeArray<int>(num2, global::Unity.Collections.Allocator.Temp);
				int num4 = 0;
				int num5 = 0;
				for (int k = 0; k < num3; k++)
				{
					global::UnityEngine.PhysicsShape2D physicsShape2D2 = groupShapes[nativeArray[k]];
					global::UnityEngine.PhysicsShapeType2D shapeType2 = physicsShape2D2.shapeType;
					float radius = physicsShape2D2.radius;
					int vertexStartIndex = physicsShape2D2.vertexStartIndex;
					int vertexCount2 = physicsShape2D2.vertexCount;
					switch (shapeType2)
					{
					case global::UnityEngine.PhysicsShapeType2D.Circle:
						radii[num4] = radius;
						indices[num5++] = num4;
						indices[num5++] = num4;
						vertices[num4++] = groupVertices[vertexStartIndex];
						break;
					case global::UnityEngine.PhysicsShapeType2D.Capsule:
						radii[num4] = radius;
						indices[num5++] = num4;
						vertices[num4++] = groupVertices[vertexStartIndex++];
						radii[num4] = radius;
						indices[num5++] = num4;
						vertices[num4++] = groupVertices[vertexStartIndex++];
						break;
					case global::UnityEngine.PhysicsShapeType2D.Polygon:
					{
						int value3 = num4;
						int value4 = num4;
						for (int m = 0; m < vertexCount2 - 1; m++)
						{
							radii[num4] = radius;
							vertices[num4++] = groupVertices[vertexStartIndex++];
							indices[num5++] = value4++;
							indices[num5++] = value4;
						}
						radii[num4] = radius;
						vertices[num4++] = groupVertices[vertexStartIndex++];
						indices[num5++] = value4;
						indices[num5++] = value3;
						break;
					}
					case global::UnityEngine.PhysicsShapeType2D.Edges:
					{
						int value = num4;
						int value2 = num4;
						for (int l = 0; l < vertexCount2 - 1; l++)
						{
							radii[num4] = radius;
							vertices[num4++] = groupVertices[vertexStartIndex++];
							indices[num5++] = value2++;
							indices[num5++] = value2;
						}
						radii[num4] = radius;
						vertices[num4++] = groupVertices[vertexStartIndex++];
						global::UnityEngine.Vector2 vector2 = groupVertices[physicsShape2D2.vertexStartIndex];
						if (!((groupVertices[physicsShape2D2.vertexStartIndex + physicsShape2D2.vertexCount - 1] - vector2).sqrMagnitude > global::UnityEngine.Mathf.Epsilon))
						{
							indices[num5++] = value2;
							indices[num5++] = value;
						}
						break;
					}
					}
				}
				global::UnityEngine.Matrix4x4 transform = collider.transform.worldToLocalMatrix * matrix4x;
				global::UnityEngine.Renderer component;
				bool createInteriorGeometry = !collider.TryGetComponent<global::UnityEngine.Renderer>(out component);
				persistantShapeObject.SetShape(vertices, indices, radii, transform, global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder.CounterClockwise, allowContraction: true, createInteriorGeometry);
				indices.Dispose();
				vertices.Dispose();
				radii.Dispose();
			}
			else
			{
				ClearShapes(persistantShapeObject);
			}
			nativeArray.Dispose();
		}

		private void Initialize()
		{
			m_ShadowStateHash = 0u;
			m_ShadowCombinedShapeMinMaxBounds = default(global::UnityEngine.Rendering.Universal.ShadowShape2DProvider_Collider2D.MinMaxBounds);
			m_LastColliderSpace = global::UnityEngine.Matrix4x4.identity;
		}

		public override bool IsShapeSource(global::UnityEngine.Component sourceComponent)
		{
			return sourceComponent is global::UnityEngine.Collider2D;
		}

		public override void OnPersistantDataCreated(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShapeData)
		{
			Initialize();
		}

		public override void OnBeforeRender(global::UnityEngine.Component sourceComponent, global::UnityEngine.Bounds worldCullingBounds, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			global::UnityEngine.Collider2D collider = (global::UnityEngine.Collider2D)sourceComponent;
			CalculateShadows(collider, persistantShadowShape, worldCullingBounds);
		}

		public override void Enabled(global::UnityEngine.Component sourceComponent, global::UnityEngine.Rendering.Universal.ShadowShape2D persistantShadowShape)
		{
			Initialize();
		}
	}
}

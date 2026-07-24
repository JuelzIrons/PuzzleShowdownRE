namespace UnityEngine.U2D
{
	internal class SpriteShapeDefaultCreator : global::UnityEngine.U2D.SpriteShapeGeometryCreator
	{
		private static global::UnityEngine.U2D.SpriteShapeDefaultCreator creator;

		internal static global::UnityEngine.U2D.SpriteShapeDefaultCreator defaultInstance
		{
			get
			{
				if (null == creator)
				{
					creator = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.U2D.SpriteShapeDefaultCreator>();
					creator.hideFlags = global::UnityEngine.HideFlags.DontSave;
				}
				return creator;
			}
		}

		public override int GetVertexArrayCount(global::UnityEngine.U2D.SpriteShapeController sc)
		{
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapeControlPoints = sc.GetShapeControlPoints();
			sc.CalculateMaxArrayCount(shapeControlPoints);
			shapeControlPoints.Dispose();
			return sc.maxArrayCount;
		}

		public override global::Unity.Jobs.JobHandle MakeCreatorJob(global::UnityEngine.U2D.SpriteShapeController sc, global::Unity.Collections.NativeArray<ushort> indices, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> positions, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> texCoords, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> tangents, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeSegment> segments, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> colliderData)
		{
			bool useUTess = sc.ValidateUTess2D();
			global::Unity.Collections.NativeArray<global::UnityEngine.Bounds> bounds = sc.spriteShapeRenderer.GetBounds();
			global::UnityEngine.U2D.SpriteShapeGenerator jobData = new global::UnityEngine.U2D.SpriteShapeGenerator
			{
				m_Bounds = bounds,
				m_PosArray = positions,
				m_Uv0Array = texCoords,
				m_TanArray = tangents,
				m_GeomArray = segments,
				m_IndexArray = indices,
				m_ColliderPoints = colliderData,
				m_Stats = sc.stats,
				m_ShadowPoints = sc.shadowData
			};
			jobData.generateCollider = global::UnityEngine.U2D.SpriteShapeController.generateCollider;
			jobData.generateGeometry = global::UnityEngine.U2D.SpriteShapeController.generateGeometry;
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ShapeControlPoint> shapeControlPoints = sc.GetShapeControlPoints();
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SplinePointMetaData> splinePointMetaData = sc.GetSplinePointMetaData();
			jobData.Prepare(sc, sc.spriteShapeParameters, sc.maxArrayCount, shapeControlPoints, splinePointMetaData, sc.angleRangeInfoArray, sc.edgeSpriteArray, sc.cornerSpriteArray, useUTess);
			global::Unity.Jobs.JobHandle result = global::Unity.Jobs.IJobExtensions.Schedule(jobData);
			shapeControlPoints.Dispose();
			splinePointMetaData.Dispose();
			return result;
		}

		public override int GetVersion()
		{
			int num = 1;
			return ((-2128831035 ^ GetInstanceID()) * 16777619) ^ num;
		}
	}
}

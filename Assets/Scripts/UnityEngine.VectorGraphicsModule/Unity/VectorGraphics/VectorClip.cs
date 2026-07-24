namespace Unity.VectorGraphics
{
	internal static class VectorClip
	{
		private const int k_ClipperScale = 100000;

		private static global::System.Collections.Generic.Stack<global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>> m_ClipStack = new global::System.Collections.Generic.Stack<global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>>();

		internal static void ResetClip()
		{
			m_ClipStack.Clear();
		}

		internal static void PushClip(global::System.Collections.Generic.List<global::UnityEngine.Vector2[]> clipper, global::Unity.VectorGraphics.Matrix2D transform)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>(10);
			foreach (global::UnityEngine.Vector2[] item in clipper)
			{
				global::System.Collections.Generic.List<global::ClipperLib.IntPoint> list2 = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>(item.Length);
				global::UnityEngine.Vector2[] array = item;
				foreach (global::UnityEngine.Vector2 vector in array)
				{
					global::UnityEngine.Vector2 vector2 = transform * vector;
					list2.Add(new global::ClipperLib.IntPoint(vector2.x * 100000f, vector2.y * 100000f));
				}
				list.Add(list2);
			}
			m_ClipStack.Push(list);
		}

		internal static void PopClip()
		{
			m_ClipStack.Pop();
		}

		internal static void ClipGeometry(global::Unity.VectorGraphics.VectorUtils.Geometry geom)
		{
			global::ClipperLib.Clipper clipper = new global::ClipperLib.Clipper();
			foreach (global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> item in m_ClipStack)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Vector2> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>(geom.Vertices.Length);
				global::System.Collections.Generic.List<ushort> list2 = new global::System.Collections.Generic.List<ushort>(geom.Indices.Length);
				global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> list3 = BuildTriangleClipPaths(geom);
				global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> list4 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>();
				ushort maxIndex = 0;
				foreach (global::System.Collections.Generic.List<global::ClipperLib.IntPoint> item2 in list3)
				{
					clipper.AddPaths(item, global::ClipperLib.PolyType.ptClip, closed: true);
					clipper.AddPath(item2, global::ClipperLib.PolyType.ptSubject, Closed: true);
					clipper.Execute(global::ClipperLib.ClipType.ctIntersection, list4, global::ClipperLib.PolyFillType.pftNonZero, global::ClipperLib.PolyFillType.pftNonZero);
					if (list4.Count > 0)
					{
						BuildGeometryFromClipPaths(geom, list4, list, list2, ref maxIndex);
					}
					clipper.Clear();
					list4.Clear();
				}
				geom.Vertices = list.ToArray();
				geom.Indices = list2.ToArray();
			}
		}

		private static global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> BuildTriangleClipPaths(global::Unity.VectorGraphics.VectorUtils.Geometry geom)
		{
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>>(geom.Indices.Length / 3);
			global::UnityEngine.Vector2[] vertices = geom.Vertices;
			ushort[] indices = geom.Indices;
			int num = geom.Indices.Length;
			global::Unity.VectorGraphics.Matrix2D worldTransform = geom.WorldTransform;
			for (int i = 0; i < num; i += 3)
			{
				global::UnityEngine.Vector2 vector = worldTransform * vertices[indices[i]];
				global::UnityEngine.Vector2 vector2 = worldTransform * vertices[indices[i + 1]];
				global::UnityEngine.Vector2 vector3 = worldTransform * vertices[indices[i + 2]];
				global::System.Collections.Generic.List<global::ClipperLib.IntPoint> list2 = new global::System.Collections.Generic.List<global::ClipperLib.IntPoint>(3);
				list2.Add(new global::ClipperLib.IntPoint(vector.x * 100000f, vector.y * 100000f));
				list2.Add(new global::ClipperLib.IntPoint(vector2.x * 100000f, vector2.y * 100000f));
				list2.Add(new global::ClipperLib.IntPoint(vector3.x * 100000f, vector3.y * 100000f));
				list.Add(list2);
			}
			return list;
		}

		private static void BuildGeometryFromClipPaths(global::Unity.VectorGraphics.VectorUtils.Geometry geom, global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::ClipperLib.IntPoint>> paths, global::System.Collections.Generic.List<global::UnityEngine.Vector2> outVerts, global::System.Collections.Generic.List<ushort> outInds, ref ushort maxIndex)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Vector2> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>(100);
			global::System.Collections.Generic.List<ushort> list2 = new global::System.Collections.Generic.List<ushort>(list.Capacity * 3);
			global::System.Collections.Generic.Dictionary<global::ClipperLib.IntPoint, ushort> vertexIndex = new global::System.Collections.Generic.Dictionary<global::ClipperLib.IntPoint, ushort>();
			foreach (global::System.Collections.Generic.List<global::ClipperLib.IntPoint> path in paths)
			{
				if (path.Count == 3)
				{
					foreach (global::ClipperLib.IntPoint item in path)
					{
						StoreClipVertex(vertexIndex, list, list2, item, ref maxIndex);
					}
				}
				else if (path.Count > 3)
				{
					global::LibTessDotNet.Tess tess = new global::LibTessDotNet.Tess();
					global::LibTessDotNet.ContourVertex[] array = new global::LibTessDotNet.ContourVertex[path.Count];
					for (int i = 0; i < path.Count; i++)
					{
						array[i] = new global::LibTessDotNet.ContourVertex
						{
							Position = new global::LibTessDotNet.Vec3
							{
								X = path[i].X,
								Y = path[i].Y,
								Z = 0f
							}
						};
					}
					tess.AddContour(array, global::LibTessDotNet.ContourOrientation.Original);
					global::LibTessDotNet.WindingRule windingRule = global::LibTessDotNet.WindingRule.NonZero;
					tess.Tessellate(windingRule, global::LibTessDotNet.ElementType.Polygons, 3);
					int[] elements = tess.Elements;
					foreach (int num in elements)
					{
						global::LibTessDotNet.ContourVertex contourVertex = tess.Vertices[num];
						global::ClipperLib.IntPoint pt = new global::ClipperLib.IntPoint(contourVertex.Position.X, contourVertex.Position.Y);
						StoreClipVertex(vertexIndex, list, list2, pt, ref maxIndex);
					}
				}
			}
			global::Unity.VectorGraphics.Matrix2D matrix2D = geom.WorldTransform.Inverse();
			for (int k = 0; k < list.Count; k++)
			{
				outVerts.Add(matrix2D * list[k]);
			}
			outInds.AddRange(list2);
		}

		private static void StoreClipVertex(global::System.Collections.Generic.Dictionary<global::ClipperLib.IntPoint, ushort> vertexIndex, global::System.Collections.Generic.List<global::UnityEngine.Vector2> vertices, global::System.Collections.Generic.List<ushort> indices, global::ClipperLib.IntPoint pt, ref ushort index)
		{
			if (vertexIndex.TryGetValue(pt, out var value))
			{
				indices.Add(value);
				return;
			}
			vertices.Add(new global::UnityEngine.Vector2((float)pt.X / 100000f, (float)pt.Y / 100000f));
			indices.Add(index);
			vertexIndex[pt] = index;
			index++;
		}
	}
}

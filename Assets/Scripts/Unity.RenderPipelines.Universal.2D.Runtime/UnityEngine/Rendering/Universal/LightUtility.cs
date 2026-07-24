namespace UnityEngine.Rendering.Universal
{
	internal static class LightUtility
	{
		private enum PivotType
		{
			PivotBase = 0,
			PivotCurve = 1,
			PivotIntersect = 2,
			PivotSkip = 3,
			PivotClip = 4
		}

		[global::System.Serializable]
		internal struct LightMeshVertex
		{
			public global::UnityEngine.Vector3 position;

			public global::UnityEngine.Color color;

			public global::UnityEngine.Vector2 uv;

			public static readonly global::UnityEngine.Rendering.VertexAttributeDescriptor[] VertexLayout = new global::UnityEngine.Rendering.VertexAttributeDescriptor[3]
			{
				new global::UnityEngine.Rendering.VertexAttributeDescriptor(global::UnityEngine.Rendering.VertexAttribute.Position, global::UnityEngine.Rendering.VertexAttributeFormat.Float32, 3, 0),
				new global::UnityEngine.Rendering.VertexAttributeDescriptor(global::UnityEngine.Rendering.VertexAttribute.Color, global::UnityEngine.Rendering.VertexAttributeFormat.Float32, 4),
				new global::UnityEngine.Rendering.VertexAttributeDescriptor(global::UnityEngine.Rendering.VertexAttribute.TexCoord0, global::UnityEngine.Rendering.VertexAttributeFormat.Float32, 2)
			};
		}

		public static bool CheckForChange(global::UnityEngine.Rendering.Universal.Light2D.LightType a, ref global::UnityEngine.Rendering.Universal.Light2D.LightType b)
		{
			bool result = a != b;
			b = a;
			return result;
		}

		public static bool CheckForChange(global::UnityEngine.Component a, ref global::UnityEngine.Component b)
		{
			bool result = a != b;
			b = a;
			return result;
		}

		public static bool CheckForChange(int a, ref int b)
		{
			bool result = a != b;
			b = a;
			return result;
		}

		public static bool CheckForChange(float a, ref float b)
		{
			bool result = a != b;
			b = a;
			return result;
		}

		public static bool CheckForChange(bool a, ref bool b)
		{
			bool result = a != b;
			b = a;
			return result;
		}

		private static bool TestPivot(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, int activePoint, long lastPoint)
		{
			for (int i = activePoint; i < path.Count; i++)
			{
				if (path[i].N > lastPoint)
				{
					return true;
				}
			}
			return path[activePoint].N == -1;
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> DegeneratePivots(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> path, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> inPath, ref int interiorStart)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();
			long num = path[0].N;
			long num2 = path[0].N;
			for (int i = 1; i < path.Count; i++)
			{
				if (path[i].N != -1)
				{
					num = global::System.Math.Min(num, path[i].N);
					num2 = global::System.Math.Max(num2, path[i].N);
				}
			}
			for (long num3 = 0L; num3 < num; num3++)
			{
				global::UnityEngine.Rendering.Universal.IntPoint item = path[(int)num];
				item.N = num3;
				list.Add(item);
			}
			list.AddRange(path.GetRange(0, path.Count));
			interiorStart = list.Count;
			for (long num4 = num2 + 1; num4 < inPath.Count; num4++)
			{
				global::UnityEngine.Rendering.Universal.IntPoint item2 = inPath[(int)num4];
				item2.N = num4;
				list.Add(item2);
			}
			return list;
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> SortPivots(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> outPath, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> inPath)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();
			_ = outPath[0];
			long n = outPath[0].N;
			int num = 0;
			bool flag = true;
			for (int i = 1; i < outPath.Count; i++)
			{
				if (n > outPath[i].N && flag && outPath[i].N != -1)
				{
					n = outPath[i].N;
					num = i;
					flag = false;
				}
				else if (outPath[i].N >= n)
				{
					n = outPath[i].N;
					flag = true;
				}
			}
			list.AddRange(outPath.GetRange(num, outPath.Count - num));
			list.AddRange(outPath.GetRange(0, num));
			return list;
		}

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> FixPivots(global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> outPath, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> inPath, ref int interiorStart)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = SortPivots(outPath, inPath);
			long n = list[0].N;
			for (int i = 1; i < list.Count; i++)
			{
				int index = ((i != list.Count - 1) ? (i + 1) : 0);
				global::UnityEngine.Rendering.Universal.IntPoint intPoint = list[i - 1];
				global::UnityEngine.Rendering.Universal.IntPoint value = list[i];
				global::UnityEngine.Rendering.Universal.IntPoint intPoint2 = list[index];
				if (intPoint.N > value.N && TestPivot(list, i, n))
				{
					if (intPoint.N == intPoint2.N)
					{
						value.N = intPoint.N;
					}
					else
					{
						value.N = ((n + 1 < inPath.Count) ? (n + 1) : 0);
					}
					value.D = 3L;
					list[i] = value;
				}
				n = list[i].N;
			}
			int num = 1;
			while (num < list.Count - 1)
			{
				global::UnityEngine.Rendering.Universal.IntPoint intPoint3 = list[num - 1];
				global::UnityEngine.Rendering.Universal.IntPoint intPoint4 = list[num];
				global::UnityEngine.Rendering.Universal.IntPoint intPoint5 = list[num + 1];
				if (intPoint4.N - intPoint3.N > 1)
				{
					if (intPoint4.N == intPoint5.N)
					{
						global::UnityEngine.Rendering.Universal.IntPoint value2 = intPoint4;
						value2.N--;
						list[num] = value2;
					}
					else
					{
						global::UnityEngine.Rendering.Universal.IntPoint item = intPoint4;
						item.N--;
						list.Insert(num, item);
					}
				}
				else
				{
					num++;
				}
			}
			return DegeneratePivots(list, inPath, ref interiorStart);
		}

		internal static global::System.Collections.Generic.List<global::UnityEngine.Vector2> GetOutlinePath(global::UnityEngine.Vector3[] shapePath, float offsetDistance)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();
			global::System.Collections.Generic.List<global::UnityEngine.Vector2> list2 = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();
			for (int i = 0; i < shapePath.Length; i++)
			{
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(shapePath[i].x, shapePath[i].y) * 10000f;
				list.Add(new global::UnityEngine.Rendering.Universal.IntPoint((long)vector.x, (long)vector.y));
			}
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> solution = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			global::UnityEngine.Rendering.Universal.ClipperOffset clipperOffset = new global::UnityEngine.Rendering.Universal.ClipperOffset(24.0);
			clipperOffset.AddPath(list, global::UnityEngine.Rendering.Universal.JoinTypes.jtRound, global::UnityEngine.Rendering.Universal.EndTypes.etClosedPolygon);
			clipperOffset.Execute(ref solution, 10000f * offsetDistance, list.Count);
			if (solution.Count > 0)
			{
				int interiorStart = 0;
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> outPath = solution[0];
				outPath = FixPivots(outPath, list, ref interiorStart);
				for (int j = 0; j < outPath.Count; j++)
				{
					list2.Add(new global::UnityEngine.Vector2((float)outPath[j].X / 10000f, (float)outPath[j].Y / 10000f));
				}
			}
			return list2;
		}

		private static void TransferToMesh(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex> vertices, int vertexCount, global::Unity.Collections.NativeArray<ushort> indices, int indexCount, global::UnityEngine.Rendering.Universal.Light2D light)
		{
			global::UnityEngine.Mesh lightMesh = light.lightMesh;
			lightMesh.SetVertexBufferParams(vertexCount, global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData(vertices, 0, 0, vertexCount);
			lightMesh.SetIndices(indices, 0, indexCount, global::UnityEngine.MeshTopology.Triangles, 0);
			light.vertices = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[vertexCount];
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>.Copy(vertices, light.vertices, vertexCount);
			light.indices = new ushort[indexCount];
			global::Unity.Collections.NativeArray<ushort>.Copy(indices, light.indices, indexCount);
		}

		public static global::UnityEngine.Bounds GenerateShapeMesh(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.Vector3[] shapePath, float falloffDistance, float batchColor)
		{
			global::UnityEngine.Random.State state = global::UnityEngine.Random.state;
			global::UnityEngine.Random.InitState(123456);
			global::UnityEngine.Color color = new global::UnityEngine.Color(0f, 0f, batchColor, 1f);
			global::UnityEngine.Color color2 = new global::UnityEngine.Color(0f, 0f, batchColor, 0f);
			int num = shapePath.Length;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(num, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(num, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < num; i++)
			{
				int num2 = i + 1;
				if (num2 == num)
				{
					num2 = 0;
				}
				int x = (edges[i] = new global::Unity.Mathematics.int2(i, num2)).x;
				points[x] = new global::Unity.Mathematics.float2(shapePath[x].x, shapePath[x].y);
			}
			global::Unity.Collections.NativeArray<int> outIndices = new global::Unity.Collections.NativeArray<int>(edges.Length * 8, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(edges.Length * 8, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(edges.Length * 8, global::Unity.Collections.Allocator.Temp);
			int outVertexCount = 0;
			int outIndexCount = 0;
			int outEdgeCount = 0;
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.Tessellate(global::Unity.Collections.Allocator.Temp, points, edges, ref outVertices, ref outVertexCount, ref outIndices, ref outIndexCount, ref outEdges, ref outEdgeCount);
			int num3 = shapePath.Length;
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>();
			for (int j = 0; j < num3; j++)
			{
				long num4 = (long)((double)shapePath[j].x * 10000.0);
				long num5 = (long)((double)shapePath[j].y * 10000.0);
				global::UnityEngine.Rendering.Universal.IntPoint item = new global::UnityEngine.Rendering.Universal.IntPoint(num4 + global::UnityEngine.Random.Range(-10, 10), num5 + global::UnityEngine.Random.Range(-10, 10));
				item.N = j;
				item.D = -1L;
				list.Add(item);
			}
			int num6 = num3 - 1;
			int interiorStart = 0;
			global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>> solution = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint>>();
			global::UnityEngine.Rendering.Universal.ClipperOffset clipperOffset = new global::UnityEngine.Rendering.Universal.ClipperOffset(24.0);
			clipperOffset.AddPath(list, global::UnityEngine.Rendering.Universal.JoinTypes.jtRound, global::UnityEngine.Rendering.Universal.EndTypes.etClosedPolygon);
			clipperOffset.Execute(ref solution, 10000f * falloffDistance, list.Count);
			if (solution.Count > 0)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.IntPoint> list2 = solution[0];
				long num7 = num3;
				for (int k = 0; k < list2.Count; k++)
				{
					num7 = ((list2[k].N != -1) ? global::System.Math.Min(num7, list2[k].N) : num7);
				}
				bool flag = num7 == 0;
				list2 = FixPivots(list2, list, ref interiorStart);
				int length = outVertexCount + list2.Count + num3;
				int length2 = outIndexCount + list2.Count * 6 + 6;
				global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex> vertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>(length, global::Unity.Collections.Allocator.Temp);
				global::Unity.Collections.NativeArray<ushort> indices = new global::Unity.Collections.NativeArray<ushort>(length2, global::Unity.Collections.Allocator.Temp);
				for (int l = 0; l < outIndexCount; l++)
				{
					indices[l] = (ushort)outIndices[l];
				}
				for (int m = 0; m < outVertexCount; m++)
				{
					vertices[m] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
					{
						position = new global::Unity.Mathematics.float3(outVertices[m].x, outVertices[m].y, 0f),
						color = color
					};
				}
				int num8 = outVertexCount;
				int num9 = outIndexCount;
				ushort[] array = new ushort[num3];
				for (int n = 0; n < num3; n++)
				{
					vertices[num8++] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
					{
						position = new global::Unity.Mathematics.float3(shapePath[n].x, shapePath[n].y, 0f),
						color = color
					};
					array[n] = (ushort)(num8 - 1);
				}
				ushort num10 = (ushort)num8;
				ushort num11 = num10;
				long num12 = ((list2[0].N == -1) ? 0 : list2[0].N);
				for (int num13 = 0; num13 < list2.Count; num13++)
				{
					global::UnityEngine.Rendering.Universal.IntPoint intPoint = list2[num13];
					global::Unity.Mathematics.float2 float5 = new global::Unity.Mathematics.float2((float)intPoint.X / 10000f, (float)intPoint.Y / 10000f);
					long num14 = ((intPoint.N == -1) ? 0 : intPoint.N);
					vertices[num8++] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
					{
						position = new global::Unity.Mathematics.float3(float5.x, float5.y, 0f),
						color = ((interiorStart > num13) ? color2 : color)
					};
					if (num12 != num14)
					{
						indices[num9++] = array[num12];
						indices[num9++] = array[num14];
						indices[num9++] = (ushort)(num8 - 1);
					}
					indices[num9++] = array[num12];
					indices[num9++] = num10;
					num10 = (indices[num9++] = (ushort)(num8 - 1));
					num12 = num14;
				}
				indices[num9++] = num11;
				indices[num9++] = array[num7];
				indices[num9++] = (flag ? array[num6] : num10);
				indices[num9++] = (flag ? num11 : num10);
				indices[num9++] = (flag ? num10 : array[num7]);
				if (flag)
				{
					float num16 = 0.001f;
					ushort num17 = array[num6];
					bool num18 = global::System.MathF.Abs(vertices[num17].position.x - vertices[indices[num9 - 1]].position.x) > num16 || global::System.MathF.Abs(vertices[num17].position.y - vertices[indices[num9 - 1]].position.y) > num16;
					bool flag2 = global::System.MathF.Abs(vertices[num17].position.x - vertices[indices[num9 - 2]].position.x) > num16 || global::System.MathF.Abs(vertices[num17].position.y - vertices[indices[num9 - 2]].position.y) > num16;
					if (!num18 || !flag2)
					{
						num17 = (ushort)(interiorStart + num3 + outVertexCount - 1);
					}
					indices[num9++] = num17;
				}
				else
				{
					indices[num9++] = array[num7 - 1];
				}
				TransferToMesh(vertices, num8, indices, num9, light);
			}
			global::UnityEngine.Random.state = state;
			return light.lightMesh.GetSubMesh(0).bounds;
		}

		public static global::UnityEngine.Bounds GenerateParametricMesh(global::UnityEngine.Rendering.Universal.Light2D light, float radius, float falloffDistance, float angle, int sides, float batchColor)
		{
			float num = global::System.MathF.PI / 2f + global::System.MathF.PI / 180f * angle;
			if (sides < 3)
			{
				radius = 0.70710677f * radius;
				sides = 4;
			}
			if (sides == 4)
			{
				num = global::System.MathF.PI / 4f + global::System.MathF.PI / 180f * angle;
			}
			int num2 = 1 + 2 * sides;
			int num3 = 9 * sides;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>(num2, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<ushort> nativeArray2 = new global::Unity.Collections.NativeArray<ushort>(num3, global::Unity.Collections.Allocator.Temp);
			ushort num4 = (ushort)(2 * sides);
			global::UnityEngine.Mesh lightMesh = light.lightMesh;
			global::UnityEngine.Color color = new global::UnityEngine.Color(0f, 0f, batchColor, 1f);
			nativeArray[num4] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
			{
				position = global::Unity.Mathematics.float3.zero,
				color = color
			};
			float num5 = global::System.MathF.PI * 2f / (float)sides;
			global::Unity.Mathematics.float3 float5 = new global::Unity.Mathematics.float3(float.MaxValue, float.MaxValue, 0f);
			global::Unity.Mathematics.float3 float6 = new global::Unity.Mathematics.float3(float.MinValue, float.MinValue, 0f);
			for (int i = 0; i < sides; i++)
			{
				float num6 = (float)(i + 1) * num5;
				global::Unity.Mathematics.float3 float7 = new global::Unity.Mathematics.float3(global::Unity.Mathematics.math.cos(num6 + num), global::Unity.Mathematics.math.sin(num6 + num), 0f);
				global::Unity.Mathematics.float3 float8 = radius * float7;
				int num7 = (2 * i + 2) % (2 * sides);
				nativeArray[num7] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
				{
					position = float8,
					color = new global::UnityEngine.Color(float7.x, float7.y, batchColor, 0f)
				};
				nativeArray[num7 + 1] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
				{
					position = float8,
					color = color
				};
				int num8 = 9 * i;
				nativeArray2[num8] = (ushort)(num7 + 1);
				nativeArray2[num8 + 1] = (ushort)(2 * i + 1);
				nativeArray2[num8 + 2] = num4;
				nativeArray2[num8 + 3] = (ushort)num7;
				nativeArray2[num8 + 4] = (ushort)(2 * i);
				nativeArray2[num8 + 5] = (ushort)(2 * i + 1);
				nativeArray2[num8 + 6] = (ushort)(num7 + 1);
				nativeArray2[num8 + 7] = (ushort)num7;
				nativeArray2[num8 + 8] = (ushort)(2 * i + 1);
				float5 = global::Unity.Mathematics.math.min(float5, float8 + float7 * falloffDistance);
				float6 = global::Unity.Mathematics.math.max(float6, float8 + float7 * falloffDistance);
			}
			lightMesh.SetVertexBufferParams(num2, global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData(nativeArray, 0, 0, num2);
			lightMesh.SetIndices(nativeArray2, global::UnityEngine.MeshTopology.Triangles, 0, calculateBounds: false);
			light.vertices = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[num2];
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>.Copy(nativeArray, light.vertices, num2);
			light.indices = new ushort[num3];
			global::Unity.Collections.NativeArray<ushort>.Copy(nativeArray2, light.indices, num3);
			return new global::UnityEngine.Bounds
			{
				min = float5,
				max = float6
			};
		}

		public static global::UnityEngine.Bounds GenerateSpriteMesh(global::UnityEngine.Rendering.Universal.Light2D light, global::UnityEngine.Sprite sprite, float batchColor)
		{
			global::UnityEngine.Mesh lightMesh = light.lightMesh;
			if (sprite == null)
			{
				lightMesh.Clear();
				return new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.zero);
			}
			_ = sprite.uv;
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertexAttribute = global::UnityEngine.U2D.SpriteDataAccessExtensions.GetVertexAttribute<global::UnityEngine.Vector3>(sprite, global::UnityEngine.Rendering.VertexAttribute.Position);
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> vertexAttribute2 = global::UnityEngine.U2D.SpriteDataAccessExtensions.GetVertexAttribute<global::UnityEngine.Vector2>(sprite, global::UnityEngine.Rendering.VertexAttribute.TexCoord0);
			global::Unity.Collections.NativeArray<ushort> indices = global::UnityEngine.U2D.SpriteDataAccessExtensions.GetIndices(sprite);
			_ = 0.5f * (sprite.bounds.min + sprite.bounds.max);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>(indices.Length, global::Unity.Collections.Allocator.Temp);
			global::UnityEngine.Color color = new global::UnityEngine.Color(0f, 0f, batchColor, 1f);
			for (int i = 0; i < vertexAttribute.Length; i++)
			{
				nativeArray[i] = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex
				{
					position = new global::UnityEngine.Vector3(vertexAttribute[i].x, vertexAttribute[i].y, 0f),
					color = color,
					uv = vertexAttribute2[i]
				};
			}
			lightMesh.SetVertexBufferParams(nativeArray.Length, global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex.VertexLayout);
			lightMesh.SetVertexBufferData(nativeArray, 0, 0, nativeArray.Length);
			lightMesh.SetIndices(indices, global::UnityEngine.MeshTopology.Triangles, 0);
			light.vertices = new global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex[nativeArray.Length];
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.LightUtility.LightMeshVertex>.Copy(nativeArray, light.vertices, nativeArray.Length);
			light.indices = new ushort[indices.Length];
			global::Unity.Collections.NativeArray<ushort>.Copy(indices, light.indices, indices.Length);
			return lightMesh.GetSubMesh(0).bounds;
		}

		public static int GetShapePathHash(global::UnityEngine.Vector3[] path)
		{
			int num = -2128831035;
			if (path != null)
			{
				for (int i = 0; i < path.Length; i++)
				{
					global::UnityEngine.Vector3 vector = path[i];
					num = (num * 16777619) ^ vector.GetHashCode();
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}
	}
}

namespace UnityEngine.Rendering.Universal
{
	[global::Unity.Burst.BurstCompile]
	internal class ShadowUtility
	{
		public enum ProjectionType
		{
			ProjectionNone = -1,
			ProjectionHard = 0,
			ProjectionSoftLeft = 1,
			ProjectionSoftRight = 3
		}

		internal struct ShadowMeshVertex
		{
			internal global::UnityEngine.Vector3 position;

			internal global::UnityEngine.Vector4 tangent;

			internal ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType inProjectionType, global::UnityEngine.Vector2 inEdgePosition0, global::UnityEngine.Vector2 inEdgePosition1)
			{
				position.x = inEdgePosition0.x;
				position.y = inEdgePosition0.y;
				position.z = 0f;
				tangent.x = (float)inProjectionType;
				tangent.y = 0f;
				tangent.z = inEdgePosition1.x;
				tangent.w = inEdgePosition1.y;
			}
		}

		internal struct RemappingInfo
		{
			public int count;

			public int index;

			public int v0Offset;

			public int v1Offset;

			public void Initialize()
			{
				count = 0;
				index = -1;
				v0Offset = 0;
				v1Offset = 0;
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateProjectionInfo_000002D2_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> outProjectionInfo);

		internal static class CalculateProjectionInfo_000002D2_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateProjectionInfo_000002D2_0024PostfixBurstDelegate>(CalculateProjectionInfo).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> outProjectionInfo)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<bool>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>, void>)functionPointer)(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
						return;
					}
				}
				CalculateProjectionInfo_0024BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateVertices_000002D3_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> inEdgeOtherPoints, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outMeshVertices);

		internal static class CalculateVertices_000002D3_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateVertices_000002D3_0024PostfixBurstDelegate>(CalculateVertices).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> inEdgeOtherPoints, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outMeshVertices)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex>, void>)functionPointer)(ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices);
						return;
					}
				}
				CalculateVertices_0024BurstManaged(ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateTriangles_000002D4_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<int> outMeshIndices);

		internal static class CalculateTriangles_000002D4_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateTriangles_000002D4_0024PostfixBurstDelegate>(CalculateTriangles).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<int> outMeshIndices)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<bool>, ref global::Unity.Collections.NativeArray<int>, void>)functionPointer)(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
						return;
					}
				}
				CalculateTriangles_0024BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateLocalBounds_000002D5_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, out global::UnityEngine.Bounds retBounds);

		internal static class CalculateLocalBounds_000002D5_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateLocalBounds_000002D5_0024PostfixBurstDelegate>(CalculateLocalBounds).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, out global::UnityEngine.Bounds retBounds)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::UnityEngine.Bounds, void>)functionPointer)(ref inVertices, ref retBounds);
						return;
					}
				}
				CalculateLocalBounds_0024BurstManaged(ref inVertices, out retBounds);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GenerateInteriorMesh_000002D6_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> inVertices, ref global::Unity.Collections.NativeArray<int> inIndices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outVertices, out global::Unity.Collections.NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount);

		internal static class GenerateInteriorMesh_000002D6_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.GenerateInteriorMesh_000002D6_0024PostfixBurstDelegate>(GenerateInteriorMesh).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> inVertices, ref global::Unity.Collections.NativeArray<int> inIndices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outVertices, out global::Unity.Collections.NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex>, ref global::Unity.Collections.NativeArray<int>, ref int, ref int, void>)functionPointer)(ref inVertices, ref inIndices, ref inEdges, ref outVertices, ref outIndices, ref outStartIndex, ref outIndexCount);
						return;
					}
				}
				GenerateInteriorMesh_0024BurstManaged(ref inVertices, ref inIndices, ref inEdges, out outVertices, out outIndices, out outStartIndex, out outIndexCount);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateEdgesFromLines_000002D8_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<int> indices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray);

		internal static class CalculateEdgesFromLines_000002D8_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateEdgesFromLines_000002D8_0024PostfixBurstDelegate>(CalculateEdgesFromLines).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<int> indices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<bool>, void>)functionPointer)(ref indices, ref outEdges, ref outShapeStartingEdge, ref outShapeIsClosedArray);
						return;
					}
				}
				CalculateEdgesFromLines_0024BurstManaged(ref indices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetVertexReferenceStats_000002D9_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo> remappingInfo);

		internal static class GetVertexReferenceStats_000002D9_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.GetVertexReferenceStats_000002D9_0024PostfixBurstDelegate>(GetVertexReferenceStats).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo> remappingInfo)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, int, ref bool, ref int, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo>, void>)functionPointer)(ref vertices, ref edges, vertexCount, ref hasReusedVertices, ref newVertexCount, ref remappingInfo);
						return;
					}
				}
				GetVertexReferenceStats_0024BurstManaged(ref vertices, ref edges, vertexCount, out hasReusedVertices, out newVertexCount, out remappingInfo);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void CalculateEdgesFromTriangles_000002DB_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<int> indices, bool duplicatesVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> newVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray);

		internal static class CalculateEdgesFromTriangles_000002DB_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateEdgesFromTriangles_000002DB_0024PostfixBurstDelegate>(CalculateEdgesFromTriangles).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<int> indices, bool duplicatesVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> newVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<int>, bool, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<bool>, void>)functionPointer)(ref vertices, ref indices, duplicatesVertices, ref newVertices, ref outEdges, ref outShapeStartingEdge, ref outShapeIsClosedArray);
						return;
					}
				}
				CalculateEdgesFromTriangles_0024BurstManaged(ref vertices, ref indices, duplicatesVertices, out newVertices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void ReverseWindingOrder_000002DC_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inOutSortedEdges);

		internal static class ReverseWindingOrder_000002DC_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.ReverseWindingOrder_000002DC_0024PostfixBurstDelegate>(ReverseWindingOrder).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inOutSortedEdges)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, void>)functionPointer)(ref inShapeStartingEdge, ref inOutSortedEdges);
						return;
					}
				}
				ReverseWindingOrder_0024BurstManaged(ref inShapeStartingEdge, ref inOutSortedEdges);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void ClipEdges_000002DF_0024PostfixBurstDelegate(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, float contractEdge, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> outVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge);

		internal static class ClipEdges_000002DF_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.Rendering.Universal.ShadowUtility.ClipEdges_000002DF_0024PostfixBurstDelegate>(ClipEdges).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, float contractEdge, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> outVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<bool>, float, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>, ref global::Unity.Collections.NativeArray<int>, void>)functionPointer)(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, ref outVertices, ref outEdges, ref outShapeStartingEdge);
						return;
					}
				}
				ClipEdges_0024BurstManaged(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, out outVertices, out outEdges, out outShapeStartingEdge);
			}
		}

		internal const int k_AdditionalVerticesPerEdge = 4;

		internal const int k_VerticesPerTriangle = 3;

		internal const int k_TrianglesPerEdge = 3;

		internal const int k_MinimumEdges = 3;

		internal const int k_SafeSize = 40;

		private static global::UnityEngine.Rendering.VertexAttributeDescriptor[] m_VertexLayout = new global::UnityEngine.Rendering.VertexAttributeDescriptor[2]
		{
			new global::UnityEngine.Rendering.VertexAttributeDescriptor(global::UnityEngine.Rendering.VertexAttribute.Position, global::UnityEngine.Rendering.VertexAttributeFormat.Float32, 3, 0),
			new global::UnityEngine.Rendering.VertexAttributeDescriptor(global::UnityEngine.Rendering.VertexAttribute.Tangent, global::UnityEngine.Rendering.VertexAttributeFormat.Float32, 4)
		};

		private unsafe static int GetNextShapeStart(int currentShape, int* inShapeStartingEdgePtr, int inShapeStartingEdgeLength, int maxValue)
		{
			if (currentShape + 1 >= inShapeStartingEdgeLength || inShapeStartingEdgePtr[currentShape + 1] < 0)
			{
				return maxValue;
			}
			return inShapeStartingEdgePtr[currentShape + 1];
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateProjectionInfo_000002D2_0024PostfixBurstDelegate))]
		internal static void CalculateProjectionInfo(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> outProjectionInfo)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateProjectionInfo_000002D2_0024BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateVertices_000002D3_0024PostfixBurstDelegate))]
		internal static void CalculateVertices(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> inEdgeOtherPoints, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outMeshVertices)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateVertices_000002D3_0024BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inEdgeOtherPoints, ref outMeshVertices);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateTriangles_000002D4_0024PostfixBurstDelegate))]
		internal static void CalculateTriangles(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<int> outMeshIndices)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateTriangles_000002D4_0024BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateLocalBounds_000002D5_0024PostfixBurstDelegate))]
		internal static void CalculateLocalBounds(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, out global::UnityEngine.Bounds retBounds)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateLocalBounds_000002D5_0024BurstDirectCall.Invoke(ref inVertices, out retBounds);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002EGenerateInteriorMesh_000002D6_0024PostfixBurstDelegate))]
		private static void GenerateInteriorMesh(ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> inVertices, ref global::Unity.Collections.NativeArray<int> inIndices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outVertices, out global::Unity.Collections.NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.GenerateInteriorMesh_000002D6_0024BurstDirectCall.Invoke(ref inVertices, ref inIndices, ref inEdges, out outVertices, out outIndices, out outStartIndex, out outIndexCount);
		}

		public static global::UnityEngine.Bounds GenerateShadowMesh(global::UnityEngine.Mesh mesh, global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, global::Unity.Collections.NativeArray<int> inShapeStartingEdge, global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, bool allowContraction, bool fill, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology topology)
		{
			int length = inVertices.Length + 4 * inEdges.Length;
			int length2 = inEdges.Length * 3 * 3;
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> outProjectionInfo = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>(length, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeArray<int> outMeshIndices = new global::Unity.Collections.NativeArray<int>(length2, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outMeshVertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex>(length, global::Unity.Collections.Allocator.Persistent);
			CalculateProjectionInfo(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outProjectionInfo);
			CalculateVertices(ref inVertices, ref inEdges, ref outProjectionInfo, ref outMeshVertices);
			CalculateTriangles(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, ref outMeshIndices);
			int outStartIndex = 0;
			int outIndexCount = 0;
			global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outVertices;
			global::Unity.Collections.NativeArray<int> outIndices;
			if (fill)
			{
				GenerateInteriorMesh(ref outMeshVertices, ref outMeshIndices, ref inEdges, out outVertices, out outIndices, out outStartIndex, out outIndexCount);
				outMeshVertices.Dispose();
				outMeshIndices.Dispose();
			}
			else
			{
				outVertices = outMeshVertices;
				outIndices = outMeshIndices;
			}
			mesh.SetVertexBufferParams(outVertices.Length, m_VertexLayout);
			mesh.SetVertexBufferData(outVertices, 0, 0, outVertices.Length);
			mesh.SetIndexBufferParams(outIndices.Length, global::UnityEngine.Rendering.IndexFormat.UInt32);
			mesh.SetIndexBufferData(outIndices, 0, 0, outIndices.Length);
			mesh.SetSubMesh(0, new global::UnityEngine.Rendering.SubMeshDescriptor(0, outIndices.Length));
			mesh.subMeshCount = 1;
			outProjectionInfo.Dispose();
			outVertices.Dispose();
			outIndices.Dispose();
			CalculateLocalBounds(ref inVertices, out var retBounds);
			return retBounds;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateEdgesFromLines_000002D8_0024PostfixBurstDelegate))]
		public static void CalculateEdgesFromLines(ref global::Unity.Collections.NativeArray<int> indices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateEdgesFromLines_000002D8_0024BurstDirectCall.Invoke(ref indices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002EGetVertexReferenceStats_000002D9_0024PostfixBurstDelegate))]
		internal static void GetVertexReferenceStats(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo> remappingInfo)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.GetVertexReferenceStats_000002D9_0024BurstDirectCall.Invoke(ref vertices, ref edges, vertexCount, out hasReusedVertices, out newVertexCount, out remappingInfo);
		}

		public static bool IsTriangleReversed(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, int idx0, int idx1, int idx2)
		{
			global::UnityEngine.Vector3 vector = vertices[idx0];
			global::UnityEngine.Vector3 vector2 = vertices[idx1];
			global::UnityEngine.Vector3 vector3 = vertices[idx2];
			return global::UnityEngine.Mathf.Sign(vector.x * vector2.y + vector2.x * vector3.y + vector3.x * vector.y - (vector.y * vector2.x + vector2.y * vector3.x + vector3.y * vector.x)) >= 0f;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002ECalculateEdgesFromTriangles_000002DB_0024PostfixBurstDelegate))]
		public static void CalculateEdgesFromTriangles(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<int> indices, bool duplicatesVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> newVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.CalculateEdgesFromTriangles_000002DB_0024BurstDirectCall.Invoke(ref vertices, ref indices, duplicatesVertices, out newVertices, out outEdges, out outShapeStartingEdge, out outShapeIsClosedArray);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002EReverseWindingOrder_000002DC_0024PostfixBurstDelegate))]
		public static void ReverseWindingOrder(ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inOutSortedEdges)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.ReverseWindingOrder_000002DC_0024BurstDirectCall.Invoke(ref inShapeStartingEdge, ref inOutSortedEdges);
		}

		private static int GetClosedPathCount(ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray)
		{
			int num = 0;
			for (int i = 0; i < inShapeStartingEdge.Length && inShapeStartingEdge[i] >= 0; i++)
			{
				num++;
			}
			return num;
		}

		private static void GetPathInfo(global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, global::Unity.Collections.NativeArray<int> inShapeStartingEdge, global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, out int closedPathArrayCount, out int closedPathsCount, out int openPathArrayCount, out int openPathsCount)
		{
			closedPathArrayCount = 0;
			openPathArrayCount = 0;
			closedPathsCount = 0;
			openPathsCount = 0;
			for (int i = 0; i < inShapeStartingEdge.Length && inShapeStartingEdge[i] >= 0; i++)
			{
				int num = inShapeStartingEdge[i];
				int num2 = ((i < inShapeStartingEdge.Length - 1 && inShapeStartingEdge[i + 1] != -1) ? inShapeStartingEdge[i + 1] : inEdges.Length) - num;
				if (inShapeIsClosedArray[i])
				{
					closedPathArrayCount += num2 + 1;
					closedPathsCount++;
				}
				else
				{
					openPathArrayCount += num2 + 1;
					openPathsCount++;
				}
			}
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002ERendering_002EUniversal_002EClipEdges_000002DF_0024PostfixBurstDelegate))]
		public static void ClipEdges(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, float contractEdge, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> outVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge)
		{
			global::UnityEngine.Rendering.Universal.ShadowUtility.ClipEdges_000002DF_0024BurstDirectCall.Invoke(ref inVertices, ref inEdges, ref inShapeStartingEdge, ref inShapeIsClosedArray, contractEdge, out outVertices, out outEdges, out outShapeStartingEdge);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateProjectionInfo_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> outProjectionInfo)
		{
			global::UnityEngine.Vector3* buffer = (global::UnityEngine.Vector3*)inVertices.m_Buffer;
			global::UnityEngine.Rendering.Universal.ShadowEdge* buffer2 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)inEdges.m_Buffer;
			int* buffer3 = (int*)inShapeStartingEdge.m_Buffer;
			bool* buffer4 = (bool*)inShapeIsClosedArray.m_Buffer;
			global::UnityEngine.Vector2* buffer5 = (global::UnityEngine.Vector2*)outProjectionInfo.m_Buffer;
			global::UnityEngine.Vector2 vector = default(global::UnityEngine.Vector2);
			int length = inEdges.Length;
			int length2 = inShapeStartingEdge.Length;
			int length3 = inVertices.Length;
			int num = 0;
			int num2 = 0;
			int nextShapeStart = GetNextShapeStart(num, buffer3, length2, length);
			int num3 = nextShapeStart;
			for (int i = 0; i < length; i++)
			{
				if (i == nextShapeStart)
				{
					num++;
					num2 = nextShapeStart;
					nextShapeStart = GetNextShapeStart(num, buffer3, length2, length);
					num3 = nextShapeStart - num2;
				}
				int num4 = (i - num2 + 1) % num3 + num2;
				int num5 = (i - num2 + num3 - 1) % num3 + num2;
				int v = buffer2[i].v0;
				int v2 = buffer2[i].v1;
				int v3 = buffer2[num5].v0;
				int v4 = buffer2[num4].v1;
				vector.x = buffer[v].x;
				vector.y = buffer[v].y;
				global::UnityEngine.Vector2 vector2 = vector;
				vector.x = buffer[v2].x;
				vector.y = buffer[v2].y;
				global::UnityEngine.Vector2 vector3 = vector;
				vector.x = buffer[v3].x;
				vector.y = buffer[v3].y;
				vector.x = buffer[v4].x;
				vector.y = buffer[v4].y;
				buffer5[v] = vector3;
				int num6 = 4 * i + length3;
				buffer5[num6] = vector3;
				buffer5[num6 + 1] = vector2;
				buffer5[num6 + 2] = vector3;
				buffer5[num6 + 3] = vector3;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateVertices_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> inEdgeOtherPoints, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outMeshVertices)
		{
			global::UnityEngine.Vector3* buffer = (global::UnityEngine.Vector3*)inVertices.m_Buffer;
			global::UnityEngine.Rendering.Universal.ShadowEdge* buffer2 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)inEdges.m_Buffer;
			global::UnityEngine.Vector2* buffer3 = (global::UnityEngine.Vector2*)inEdgeOtherPoints.m_Buffer;
			global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex* buffer4 = (global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex*)outMeshVertices.m_Buffer;
			global::UnityEngine.Vector2 vector = default(global::UnityEngine.Vector2);
			int length = inEdges.Length;
			int length2 = inVertices.Length;
			for (int i = 0; i < length2; i++)
			{
				vector.x = buffer[i].x;
				vector.y = buffer[i].y;
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex shadowMeshVertex = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionNone, vector, buffer3[i]);
				buffer4[i] = shadowMeshVertex;
			}
			for (int j = 0; j < length; j++)
			{
				int v = buffer2[j].v0;
				int v2 = buffer2[j].v1;
				vector.x = buffer[v].x;
				vector.y = buffer[v].y;
				global::UnityEngine.Vector2 inEdgePosition = vector;
				vector.x = buffer[v2].x;
				vector.y = buffer[v2].y;
				global::UnityEngine.Vector2 inEdgePosition2 = vector;
				int num = 4 * j + length2;
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex shadowMeshVertex2 = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionHard, inEdgePosition, buffer3[num]);
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex shadowMeshVertex3 = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionHard, inEdgePosition2, buffer3[num + 1]);
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex shadowMeshVertex4 = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionSoftLeft, inEdgePosition, buffer3[num + 2]);
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex shadowMeshVertex5 = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionSoftRight, inEdgePosition, buffer3[num + 3]);
				buffer4[num] = shadowMeshVertex2;
				buffer4[num + 1] = shadowMeshVertex3;
				buffer4[num + 2] = shadowMeshVertex4;
				buffer4[num + 3] = shadowMeshVertex5;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateTriangles_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, ref global::Unity.Collections.NativeArray<int> outMeshIndices)
		{
			global::UnityEngine.Rendering.Universal.ShadowEdge* buffer = (global::UnityEngine.Rendering.Universal.ShadowEdge*)inEdges.m_Buffer;
			int* buffer2 = (int*)inShapeStartingEdge.m_Buffer;
			int* buffer3 = (int*)outMeshIndices.m_Buffer;
			int length = inEdges.Length;
			int length2 = inShapeStartingEdge.Length;
			int length3 = inVertices.Length;
			int num = 0;
			for (int i = 0; i < length2; i++)
			{
				int num2 = buffer2[i];
				if (num2 < 0)
				{
					break;
				}
				int num3 = length;
				if (i + 1 < length2 && buffer2[i + 1] > -1)
				{
					num3 = buffer2[i + 1];
				}
				for (int j = num2; j < num3; j++)
				{
					int v = buffer[j].v0;
					int v2 = buffer[j].v1;
					int num4 = 4 * j + length3;
					buffer3[num++] = (ushort)v;
					buffer3[num++] = (ushort)num4;
					buffer3[num++] = (ushort)(num4 + 1);
					buffer3[num++] = (ushort)(num4 + 1);
					buffer3[num++] = (ushort)v2;
					buffer3[num++] = (ushort)v;
				}
				for (int k = num2; k < num3; k++)
				{
					int v3 = buffer[k].v0;
					_ = buffer[k];
					int num5 = 4 * k + length3;
					buffer3[num++] = (ushort)v3;
					buffer3[num++] = (ushort)num5 + 2;
					buffer3[num++] = (ushort)num5 + 3;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateLocalBounds_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, out global::UnityEngine.Bounds retBounds)
		{
			if (inVertices.Length <= 0)
			{
				retBounds = new global::UnityEngine.Bounds(global::UnityEngine.Vector3.zero, global::UnityEngine.Vector3.zero);
				return;
			}
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.positiveInfinity;
			global::UnityEngine.Vector2 vector2 = global::UnityEngine.Vector2.negativeInfinity;
			global::UnityEngine.Vector3* buffer = (global::UnityEngine.Vector3*)inVertices.m_Buffer;
			int length = inVertices.Length;
			for (int i = 0; i < length; i++)
			{
				global::UnityEngine.Vector2 rhs = new global::UnityEngine.Vector2(buffer[i].x, buffer[i].y);
				vector = global::UnityEngine.Vector2.Min(vector, rhs);
				vector2 = global::UnityEngine.Vector2.Max(vector2, rhs);
			}
			retBounds = new global::UnityEngine.Bounds
			{
				max = vector2,
				min = vector
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GenerateInteriorMesh_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> inVertices, ref global::Unity.Collections.NativeArray<int> inIndices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex> outVertices, out global::Unity.Collections.NativeArray<int> outIndices, out int outStartIndex, out int outIndexCount)
		{
			int length = inEdges.Length;
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> edges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> points = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < length; i++)
			{
				int x = (edges[i] = new global::Unity.Mathematics.int2(inEdges[i].v0, inEdges[i].v1)).x;
				points[x] = new global::Unity.Mathematics.float2(inVertices[x].position.x, inVertices[x].position.y);
			}
			global::Unity.Collections.NativeArray<int> outIndices2 = new global::Unity.Collections.NativeArray<int>(points.Length * 8, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> outVertices2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(points.Length * 4, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(edges.Length * 4, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int outVertexCount = 0;
			int outIndexCount2 = 0;
			int outEdgeCount = 0;
			global::UnityEngine.Rendering.Universal.UTess.ModuleHandle.Tessellate(global::Unity.Collections.Allocator.Persistent, points, edges, ref outVertices2, ref outVertexCount, ref outIndices2, ref outIndexCount2, ref outEdges, ref outEdgeCount);
			int length2 = inIndices.Length;
			int length3 = inVertices.Length;
			int length4 = outVertexCount + inVertices.Length;
			int length5 = outIndexCount2 + inIndices.Length;
			outVertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex>(length4, global::Unity.Collections.Allocator.Persistent);
			outIndices = new global::Unity.Collections.NativeArray<int>(length5, global::Unity.Collections.Allocator.Persistent);
			for (int j = 0; j < inVertices.Length; j++)
			{
				outVertices[j] = inVertices[j];
			}
			for (int k = 0; k < outVertexCount; k++)
			{
				global::Unity.Mathematics.float2 float5 = outVertices2[k];
				global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex value = new global::UnityEngine.Rendering.Universal.ShadowUtility.ShadowMeshVertex(global::UnityEngine.Rendering.Universal.ShadowUtility.ProjectionType.ProjectionNone, float5, global::UnityEngine.Vector2.zero);
				outVertices[k + length3] = value;
			}
			for (int l = 0; l < inIndices.Length; l++)
			{
				outIndices[l] = inIndices[l];
			}
			for (int m = 0; m < outIndexCount2; m++)
			{
				outIndices[m + length2] = outIndices2[m] + length3;
			}
			outStartIndex = length2;
			outIndexCount = outIndexCount2;
			edges.Dispose();
			points.Dispose();
			outIndices2.Dispose();
			outVertices2.Dispose();
			outEdges.Dispose();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateEdgesFromLines_0024BurstManaged(ref global::Unity.Collections.NativeArray<int> indices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
		{
			int num = indices.Length >> 1;
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(num, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeArray<bool> nativeArray2 = new global::Unity.Collections.NativeArray<bool>(num, global::Unity.Collections.Allocator.Persistent);
			int* buffer = (int*)indices.m_Buffer;
			int* buffer2 = (int*)nativeArray.m_Buffer;
			bool* buffer3 = (bool*)nativeArray2.m_Buffer;
			int length = indices.Length;
			int num2 = 0;
			int num3 = *buffer;
			int num4 = *buffer;
			bool flag = false;
			*buffer2 = 0;
			for (int i = 0; i < length; i += 2)
			{
				if (flag)
				{
					num3 = buffer[i];
					buffer3[num2] = true;
					buffer2[++num2] = i >> 1;
					flag = false;
				}
				else if (buffer[i] != num4)
				{
					buffer3[num2] = false;
					buffer2[++num2] = i >> 1;
					num3 = buffer[i];
				}
				if (num3 == buffer[i + 1])
				{
					flag = true;
				}
				num4 = buffer[i + 1];
			}
			buffer3[num2++] = flag;
			outShapeStartingEdge = new global::Unity.Collections.NativeArray<int>(num2, global::Unity.Collections.Allocator.Persistent);
			outShapeIsClosedArray = new global::Unity.Collections.NativeArray<bool>(num2, global::Unity.Collections.Allocator.Persistent);
			int* buffer4 = (int*)outShapeStartingEdge.m_Buffer;
			bool* buffer5 = (bool*)outShapeIsClosedArray.m_Buffer;
			for (int j = 0; j < num2; j++)
			{
				buffer4[j] = buffer2[j];
				buffer5[j] = buffer3[j];
			}
			nativeArray.Dispose();
			nativeArray2.Dispose();
			outEdges = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>(num, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.Rendering.Universal.ShadowEdge* buffer6 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)outEdges.m_Buffer;
			for (int k = 0; k < num; k++)
			{
				int num5 = k << 1;
				int indexA = buffer[num5];
				int indexB = buffer[num5 + 1];
				buffer6[k] = new global::UnityEngine.Rendering.Universal.ShadowEdge(indexA, indexB);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void GetVertexReferenceStats_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> edges, int vertexCount, out bool hasReusedVertices, out int newVertexCount, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo> remappingInfo)
		{
			int length = edges.Length;
			newVertexCount = 0;
			hasReusedVertices = false;
			remappingInfo = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo>(vertexCount, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo* unsafePtr = (global::UnityEngine.Rendering.Universal.ShadowUtility.RemappingInfo*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(remappingInfo);
			global::UnityEngine.Rendering.Universal.ShadowEdge* unsafePtr2 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(edges);
			for (int i = 0; i < vertexCount; i++)
			{
				unsafePtr[i].Initialize();
			}
			for (int j = 0; j < length; j++)
			{
				int v = unsafePtr2[j].v0;
				unsafePtr[v].count++;
				if (unsafePtr[v].count > 1)
				{
					hasReusedVertices = true;
				}
				newVertexCount++;
			}
			for (int k = 0; k < length; k++)
			{
				int v2 = unsafePtr2[k].v1;
				if (unsafePtr[v2].count == 0)
				{
					unsafePtr[v2].count = 1;
					newVertexCount++;
				}
			}
			int num = 0;
			for (int l = 0; l < vertexCount; l++)
			{
				if (unsafePtr[l].count > 0)
				{
					unsafePtr[l].index = num;
					num += unsafePtr[l].count;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void CalculateEdgesFromTriangles_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, ref global::Unity.Collections.NativeArray<int> indices, bool duplicatesVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> newVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge, out global::Unity.Collections.NativeArray<bool> outShapeIsClosedArray)
		{
			global::UnityEngine.U2D.Clipper2D.Solution solution = default(global::UnityEngine.U2D.Clipper2D.Solution);
			global::UnityEngine.U2D.Clipper2D.ExecuteArguments inExecuteArguments = new global::UnityEngine.U2D.Clipper2D.ExecuteArguments(global::UnityEngine.U2D.Clipper2D.InitOptions.ioDefault, global::UnityEngine.U2D.Clipper2D.ClipType.ctUnion);
			int num = indices.Length / 3;
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> nativeArray = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>(indices.Length, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeArray<int> nativeArray2 = new global::Unity.Collections.NativeArray<int>(num, global::Unity.Collections.Allocator.Persistent);
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Clipper2D.PathArguments> nativeArray3 = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Clipper2D.PathArguments>(num, global::Unity.Collections.Allocator.Persistent);
			global::UnityEngine.Vector2* unsafePtr = (global::UnityEngine.Vector2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
			int* unsafePtr2 = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray2);
			global::UnityEngine.U2D.Clipper2D.PathArguments* unsafePtr3 = (global::UnityEngine.U2D.Clipper2D.PathArguments*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray3);
			global::UnityEngine.Vector3* unsafePtr4 = (global::UnityEngine.Vector3*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(vertices);
			global::UnityEngine.U2D.Clipper2D.PathArguments pathArguments = new global::UnityEngine.U2D.Clipper2D.PathArguments(global::UnityEngine.U2D.Clipper2D.PolyType.ptSubject, inClosed: true);
			for (int i = 0; i < num; i++)
			{
				unsafePtr2[i] = 3;
				unsafePtr3[i] = pathArguments;
				int num2 = 3 * i;
				unsafePtr[num2] = unsafePtr4[indices[num2]];
				unsafePtr[num2 + 1] = unsafePtr4[indices[num2 + 1]];
				unsafePtr[num2 + 2] = unsafePtr4[indices[num2 + 2]];
			}
			global::UnityEngine.U2D.Clipper2D.Execute(ref solution, nativeArray, nativeArray2, nativeArray3, inExecuteArguments, global::Unity.Collections.Allocator.Persistent);
			nativeArray.Dispose();
			nativeArray2.Dispose();
			nativeArray3.Dispose();
			int length = solution.points.Length;
			int length2 = solution.pathSizes.Length;
			newVertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(length, global::Unity.Collections.Allocator.Persistent);
			outEdges = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>(length, global::Unity.Collections.Allocator.Persistent);
			outShapeStartingEdge = new global::Unity.Collections.NativeArray<int>(length2, global::Unity.Collections.Allocator.Persistent);
			outShapeIsClosedArray = new global::Unity.Collections.NativeArray<bool>(length2, global::Unity.Collections.Allocator.Persistent);
			int* unsafePtr5 = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(solution.pathSizes);
			global::UnityEngine.Vector2* unsafePtr6 = (global::UnityEngine.Vector2*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(solution.points);
			global::UnityEngine.Vector3* unsafePtr7 = (global::UnityEngine.Vector3*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(newVertices);
			global::UnityEngine.Rendering.Universal.ShadowEdge* unsafePtr8 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(outEdges);
			int* unsafePtr9 = (int*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(outShapeStartingEdge);
			bool* unsafePtr10 = (bool*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(outShapeIsClosedArray);
			int num3 = 0;
			for (int j = 0; j < length2; j++)
			{
				int num4 = num3;
				int num5 = unsafePtr5[j];
				unsafePtr9[j] = num3;
				num3 += num5;
				int indexA = num3 - 1;
				for (int k = num4; k < num3; k++)
				{
					unsafePtr7[k] = unsafePtr6[k];
					unsafePtr8[k] = new global::UnityEngine.Rendering.Universal.ShadowEdge(indexA, k);
					indexA = k;
				}
				unsafePtr10[j] = true;
			}
			solution.Dispose();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void ReverseWindingOrder_0024BurstManaged(ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inOutSortedEdges)
		{
			for (int i = 0; i < inShapeStartingEdge.Length; i++)
			{
				int num = inShapeStartingEdge[i];
				if (num < 0)
				{
					break;
				}
				int num2 = inOutSortedEdges.Length;
				if (i + 1 < inShapeStartingEdge.Length && inShapeStartingEdge[i + 1] > -1)
				{
					num2 = inShapeStartingEdge[i + 1];
				}
				int num3 = num2 - num;
				for (int j = 0; j < num3 >> 1; j++)
				{
					int index = num + j;
					int index2 = num + num3 - 1 - j;
					global::UnityEngine.Rendering.Universal.ShadowEdge value = inOutSortedEdges[index];
					global::UnityEngine.Rendering.Universal.ShadowEdge value2 = inOutSortedEdges[index2];
					value.Reverse();
					value2.Reverse();
					inOutSortedEdges[index] = value2;
					inOutSortedEdges[index2] = value;
				}
				if ((num3 & 1) == 1)
				{
					int index3 = num + (num3 >> 1);
					global::UnityEngine.Rendering.Universal.ShadowEdge value3 = inOutSortedEdges[index3];
					value3.Reverse();
					inOutSortedEdges[index3] = value3;
				}
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void ClipEdges_0024BurstManaged(ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> inVertices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> inEdges, ref global::Unity.Collections.NativeArray<int> inShapeStartingEdge, ref global::Unity.Collections.NativeArray<bool> inShapeIsClosedArray, float contractEdge, out global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> outVertices, out global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge> outEdges, out global::Unity.Collections.NativeArray<int> outShapeStartingEdge)
		{
			global::Unity.Collections.Allocator allocator = global::Unity.Collections.Allocator.Persistent;
			int num = 65536;
			GetPathInfo(inEdges, inShapeStartingEdge, inShapeIsClosedArray, out var closedPathArrayCount, out var closedPathsCount, out var openPathArrayCount, out var openPathsCount);
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Clipper2D.PathArguments> inPathArguments = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.Clipper2D.PathArguments>(closedPathsCount, allocator);
			global::Unity.Collections.NativeArray<int> nativeArray = new global::Unity.Collections.NativeArray<int>(closedPathsCount, allocator);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> nativeArray2 = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>(closedPathArrayCount, allocator);
			global::Unity.Collections.NativeArray<int> nativeArray3 = new global::Unity.Collections.NativeArray<int>(openPathsCount, allocator);
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> nativeArray4 = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector2>(openPathArrayCount, allocator);
			global::UnityEngine.U2D.Clipper2D.PathArguments* buffer = (global::UnityEngine.U2D.Clipper2D.PathArguments*)inPathArguments.m_Buffer;
			int* buffer2 = (int*)nativeArray.m_Buffer;
			global::UnityEngine.Vector2* buffer3 = (global::UnityEngine.Vector2*)nativeArray2.m_Buffer;
			int* buffer4 = (int*)nativeArray3.m_Buffer;
			global::UnityEngine.Vector2* buffer5 = (global::UnityEngine.Vector2*)nativeArray4.m_Buffer;
			int* buffer6 = (int*)inShapeStartingEdge.m_Buffer;
			bool* buffer7 = (bool*)inShapeIsClosedArray.m_Buffer;
			global::UnityEngine.Vector3* buffer8 = (global::UnityEngine.Vector3*)inVertices.m_Buffer;
			global::UnityEngine.Rendering.Universal.ShadowEdge* buffer9 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)inEdges.m_Buffer;
			int length = inEdges.Length;
			global::UnityEngine.Vector2 vector = default(global::UnityEngine.Vector2);
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = closedPathsCount + openPathsCount;
			for (int i = 0; i < num6; i++)
			{
				int num7 = buffer6[i];
				int num8 = ((i + 1 < num6) ? buffer6[i + 1] : length) - num7;
				if (buffer7[i])
				{
					buffer2[num3] = num8 + 1;
					buffer[num3] = new global::UnityEngine.U2D.Clipper2D.PathArguments(global::UnityEngine.U2D.Clipper2D.PolyType.ptSubject, inClosed: true);
					num3++;
					for (int j = 0; j < num8; j++)
					{
						global::UnityEngine.Vector3 vector2 = buffer8[buffer9[j + num7].v0];
						vector.x = vector2.x;
						vector.y = vector2.y;
						buffer3[num2++] = vector;
					}
					buffer3[num2++] = buffer8[buffer9[num8 + num7 - 1].v1];
				}
				else
				{
					buffer4[num5++] = num8 + 1;
					for (int k = 0; k < num8; k++)
					{
						global::UnityEngine.Vector3 vector3 = buffer8[buffer9[k + num7].v0];
						vector.x = vector3.x;
						vector.y = vector3.y;
						buffer5[num4++] = vector;
					}
					buffer5[num4++] = buffer8[buffer9[num8 + num7 - 1].v1];
				}
			}
			global::Unity.Collections.NativeArray<global::UnityEngine.Vector2> inPoints = nativeArray2;
			global::Unity.Collections.NativeArray<int> inPathSizes = nativeArray;
			global::UnityEngine.U2D.Clipper2D.Solution solution = default(global::UnityEngine.U2D.Clipper2D.Solution);
			if (nativeArray.Length > 1)
			{
				global::UnityEngine.U2D.Clipper2D.Execute(inExecuteArguments: new global::UnityEngine.U2D.Clipper2D.ExecuteArguments
				{
					clipType = global::UnityEngine.U2D.Clipper2D.ClipType.ctUnion,
					clipFillType = global::UnityEngine.U2D.Clipper2D.PolyFillType.pftEvenOdd,
					subjFillType = global::UnityEngine.U2D.Clipper2D.PolyFillType.pftEvenOdd,
					strictlySimple = false,
					preserveColinear = false
				}, solution: ref solution, inPoints: nativeArray2, inPathSizes: nativeArray, inPathArguments: inPathArguments, inSolutionAllocator: allocator, inIntScale: num, useRounding: true);
				inPoints = solution.points;
				inPathSizes = solution.pathSizes;
			}
			global::UnityEngine.U2D.ClipperOffset2D.Solution solution2 = default(global::UnityEngine.U2D.ClipperOffset2D.Solution);
			global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ClipperOffset2D.PathArguments> inPathArguments2 = new global::Unity.Collections.NativeArray<global::UnityEngine.U2D.ClipperOffset2D.PathArguments>(inPathSizes.Length, allocator);
			global::UnityEngine.U2D.ClipperOffset2D.Execute(ref solution2, inPoints, inPathSizes, inPathArguments2, allocator, 0f - contractEdge, 2.0, 0.25, 0.0, num);
			if (solution2.pathSizes.Length > 0 || openPathsCount > 0)
			{
				int num9 = 0;
				int length2 = solution2.pathSizes.Length + openPathsCount;
				outVertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(solution2.points.Length + openPathArrayCount, allocator);
				outEdges = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>(solution2.points.Length + openPathArrayCount, allocator);
				outShapeStartingEdge = new global::Unity.Collections.NativeArray<int>(length2, allocator);
				global::UnityEngine.Vector3* buffer10 = (global::UnityEngine.Vector3*)outVertices.m_Buffer;
				global::UnityEngine.Rendering.Universal.ShadowEdge* buffer11 = (global::UnityEngine.Rendering.Universal.ShadowEdge*)outEdges.m_Buffer;
				int* buffer12 = (int*)outShapeStartingEdge.m_Buffer;
				global::UnityEngine.Vector2* buffer13 = (global::UnityEngine.Vector2*)solution2.points.m_Buffer;
				int length3 = solution2.points.Length;
				int* buffer14 = (int*)solution2.pathSizes.m_Buffer;
				int length4 = solution2.pathSizes.Length;
				for (int l = 0; l < length3; l++)
				{
					zero.x = buffer13[l].x;
					zero.y = buffer13[l].y;
					buffer10[num9++] = zero;
				}
				int num10 = 0;
				for (int m = 0; m < length4; m++)
				{
					int num11 = buffer14[m];
					int num12 = num10 + num11;
					buffer12[m] = num10;
					for (int n = 0; n < num11; n++)
					{
						global::UnityEngine.Rendering.Universal.ShadowEdge shadowEdge = new global::UnityEngine.Rendering.Universal.ShadowEdge(n + num10, (n + 1) % num11 + num10);
						buffer11[n + num10] = shadowEdge;
					}
					num10 = num12;
				}
				int num13 = length4;
				num10 = num9;
				for (int num14 = 0; num14 < nativeArray4.Length; num14++)
				{
					zero.x = buffer5[num14].x;
					zero.y = buffer5[num14].y;
					buffer10[num9++] = zero;
				}
				for (int num15 = 0; num15 < openPathsCount; num15++)
				{
					int num16 = buffer4[num15];
					int num17 = num10 + num16;
					buffer12[num13 + num15] = num10;
					for (int num18 = 0; num18 < num16 - 1; num18++)
					{
						global::UnityEngine.Rendering.Universal.ShadowEdge shadowEdge2 = new global::UnityEngine.Rendering.Universal.ShadowEdge(num18 + num10, num18 + 1);
						buffer11[num18 + num10] = shadowEdge2;
					}
					num10 = num17;
				}
			}
			else
			{
				outVertices = new global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>(0, allocator);
				outEdges = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.ShadowEdge>(0, allocator);
				outShapeStartingEdge = new global::Unity.Collections.NativeArray<int>(0, allocator);
			}
			nativeArray.Dispose();
			nativeArray2.Dispose();
			nativeArray3.Dispose();
			nativeArray4.Dispose();
			inPathArguments.Dispose();
			inPathArguments2.Dispose();
			solution.Dispose();
			solution2.Dispose();
		}
	}
}

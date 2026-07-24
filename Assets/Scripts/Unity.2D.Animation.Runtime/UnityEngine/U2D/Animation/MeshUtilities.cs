namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal static class MeshUtilities
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate int GenerateUTessOutline_00000087_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<ushort> indices, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline);

		internal static class GenerateUTessOutline_00000087_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.MeshUtilities.GenerateUTessOutline_00000087_0024PostfixBurstDelegate>(GenerateUTessOutline).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static int Invoke(in global::Unity.Collections.NativeArray<ushort> indices, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<ushort>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>, int>)functionPointer)(ref indices, ref outline);
					}
				}
				return GenerateUTessOutline_0024BurstManaged(in indices, ref outline);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void GetOutlineEdgesFallback_00000088_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<ushort> indices, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> output);

		internal static class GetOutlineEdgesFallback_00000088_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.MeshUtilities.GetOutlineEdgesFallback_00000088_0024PostfixBurstDelegate>(GetOutlineEdgesFallback).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<ushort> indices, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> output)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<ushort>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>, void>)functionPointer)(ref indices, ref output);
						return;
					}
				}
				GetOutlineEdgesFallback_0024BurstManaged(in indices, out output);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void AddToEdgeMap_00000089_0024PostfixBurstDelegate(int x, int y, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2> edgeMap);

		internal static class AddToEdgeMap_00000089_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.MeshUtilities.AddToEdgeMap_00000089_0024PostfixBurstDelegate>(AddToEdgeMap).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(int x, int y, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2> edgeMap)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<int, int, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2>, void>)functionPointer)(x, y, ref edgeMap);
						return;
					}
				}
				AddToEdgeMap_0024BurstManaged(x, y, ref edgeMap);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void SortEdges_0000008A_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> unsortedEdges, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> sortedEdges);

		internal static class SortEdges_0000008A_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.MeshUtilities.SortEdges_0000008A_0024PostfixBurstDelegate>(SortEdges).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> unsortedEdges, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> sortedEdges)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>, void>)functionPointer)(ref unsortedEdges, ref sortedEdges);
						return;
					}
				}
				SortEdges_0024BurstManaged(in unsortedEdges, out sortedEdges);
			}
		}

		private static readonly global::Unity.Profiling.ProfilerMarker k_OldOutline = new global::Unity.Profiling.ProfilerMarker("MeshUtilities.OldOutline");

		private static readonly global::Unity.Profiling.ProfilerMarker k_newOutline = new global::Unity.Profiling.ProfilerMarker("MeshUtilities.NewOutline");

		public static global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> GetOutlineEdges(in global::Unity.Collections.NativeArray<ushort> indices)
		{
			GetOutlineEdgesFallback(in indices, out var output);
			return output;
		}

		public unsafe static global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> GetOutlineEdgesUTess(in global::Unity.Collections.NativeArray<ushort> indices)
		{
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(indices.Length, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num = GenerateUTessOutline(in indices, ref outline);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(num, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(outline), num * global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::Unity.Mathematics.int2>());
			return nativeArray;
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002EGenerateUTessOutline_00000087_0024PostfixBurstDelegate))]
		private static int GenerateUTessOutline(in global::Unity.Collections.NativeArray<ushort> indices, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline)
		{
			return global::UnityEngine.U2D.Animation.MeshUtilities.GenerateUTessOutline_00000087_0024BurstDirectCall.Invoke(in indices, ref outline);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002EGetOutlineEdgesFallback_00000088_0024PostfixBurstDelegate))]
		public static void GetOutlineEdgesFallback(in global::Unity.Collections.NativeArray<ushort> indices, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> output)
		{
			global::UnityEngine.U2D.Animation.MeshUtilities.GetOutlineEdgesFallback_00000088_0024BurstDirectCall.Invoke(in indices, out output);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002EAddToEdgeMap_00000089_0024PostfixBurstDelegate))]
		private static void AddToEdgeMap(int x, int y, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2> edgeMap)
		{
			global::UnityEngine.U2D.Animation.MeshUtilities.AddToEdgeMap_00000089_0024BurstDirectCall.Invoke(x, y, ref edgeMap);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002ESortEdges_0000008A_0024PostfixBurstDelegate))]
		private static void SortEdges(in global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> unsortedEdges, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> sortedEdges)
		{
			global::UnityEngine.U2D.Animation.MeshUtilities.SortEdges_0000008A_0024BurstDirectCall.Invoke(in unsortedEdges, out sortedEdges);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static int GenerateUTessOutline_0024BurstManaged(in global::Unity.Collections.NativeArray<ushort> indices, ref global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> outline)
		{
			return global::UnityEngine.U2D.Common.UTess.ModuleHandle.GenerateOutlineFromTriangleIndices(in indices, ref outline);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void GetOutlineEdgesFallback_0024BurstManaged(in global::Unity.Collections.NativeArray<ushort> indices, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> output)
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2> edgeMap = new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2>(indices.Length, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < indices.Length; i += 3)
			{
				ushort num = indices[i];
				ushort num2 = indices[i + 1];
				ushort num3 = indices[i + 2];
				AddToEdgeMap(num, num2, ref edgeMap);
				AddToEdgeMap(num2, num3, ref edgeMap);
				AddToEdgeMap(num3, num, ref edgeMap);
			}
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> unsortedEdges = edgeMap.GetValueArray(global::Unity.Collections.Allocator.Temp);
			SortEdges(in unsortedEdges, out output);
			unsortedEdges.Dispose();
			edgeMap.Dispose();
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void AddToEdgeMap_0024BurstManaged(int x, int y, ref global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<ulong, global::Unity.Mathematics.int2> edgeMap)
		{
			int num = global::Unity.Mathematics.math.min(x, y);
			int num2 = global::Unity.Mathematics.math.max(x, y);
			ulong key = (ulong)(((long)num << 32) | (uint)num2);
			if (!edgeMap.Remove(key))
			{
				edgeMap[key] = new global::Unity.Mathematics.int2(x, y);
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static void SortEdges_0024BurstManaged(in global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> unsortedEdges, out global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> sortedEdges)
		{
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(unsortedEdges.Length, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.NativeList<int> nativeList = new global::Unity.Collections.NativeList<int>(1, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<int, int> unsafeHashMap = new global::Unity.Collections.LowLevel.Unsafe.UnsafeHashMap<int, int>(unsortedEdges.Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeBitArray nativeBitArray = new global::Unity.Collections.NativeBitArray(unsortedEdges.Length, global::Unity.Collections.Allocator.Temp);
			int num = 0;
			for (int i = 0; i < unsortedEdges.Length; i++)
			{
				unsafeHashMap[unsortedEdges[i].x] = i;
			}
			bool flag = true;
			int num2 = -1;
			int num3 = 0;
			for (int j = 0; j < unsortedEdges.Length; j++)
			{
				if (flag)
				{
					for (int k = num; k < unsortedEdges.Length; k += 64)
					{
						ulong num4 = ~nativeBitArray.GetBits(k, global::Unity.Mathematics.math.min(64, unsortedEdges.Length - k));
						if (num4 != 0L)
						{
							int num5 = global::Unity.Mathematics.math.tzcnt(num4);
							num2 = k + num5;
							num = num2;
							break;
						}
					}
					num3 = num2;
					flag = false;
					nativeList.Add(in j);
				}
				nativeBitArray.Set(num2, value: true);
				nativeArray[j] = unsortedEdges[num2];
				int y = unsortedEdges[num2].y;
				num2 = unsafeHashMap[y];
				if (num2 == num3)
				{
					flag = true;
				}
			}
			int length = unsortedEdges.Length;
			sortedEdges = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.int2>(length, global::Unity.Collections.Allocator.Persistent, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			int num6 = 0;
			for (int l = 0; l < nativeList.Length; l++)
			{
				int num7 = nativeList[l];
				int num8 = ((l + 1 == nativeList.Length) ? nativeArray.Length : nativeList[l + 1]);
				for (int m = num7; m < num8; m++)
				{
					sortedEdges[num6++] = nativeArray[m];
				}
			}
			nativeBitArray.Dispose();
			unsafeHashMap.Dispose();
			nativeList.Dispose();
			nativeArray.Dispose();
		}
	}
}

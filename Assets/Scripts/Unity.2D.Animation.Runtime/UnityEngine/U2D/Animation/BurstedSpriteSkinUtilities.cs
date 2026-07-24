namespace UnityEngine.U2D.Animation
{
	[global::Unity.Burst.BurstCompile]
	internal static class BurstedSpriteSkinUtilities
	{
		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate bool ValidateBoneWeights_000001AE_0024PostfixBurstDelegate(in global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> boneWeights, int bindPoseCount);

		internal static class ValidateBoneWeights_000001AE_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.ValidateBoneWeights_000001AE_0024PostfixBurstDelegate>(ValidateBoneWeights).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static bool Invoke(in global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> boneWeights, int bindPoseCount)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						return ((delegate* unmanaged[Cdecl]<ref global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight>, int, bool>)functionPointer)(ref boneWeights, bindPoseCount);
					}
				}
				return ValidateBoneWeights_0024BurstManaged(in boneWeights, bindPoseCount);
			}
		}

		[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
		internal delegate void SetVertexPositionFromByteBuffer_000001AF_0024PostfixBurstDelegate(in global::Unity.Collections.NativeArray<byte> buffer, in global::Unity.Collections.NativeArray<int> indices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, int stride);

		internal static class SetVertexPositionFromByteBuffer_000001AF_0024BurstDirectCall
		{
			private static global::System.IntPtr Pointer;

			[global::Unity.Burst.BurstDiscard]
			private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
			{
				if (Pointer == (global::System.IntPtr)0)
				{
					Pointer = global::Unity.Burst.BurstCompiler.CompileFunctionPointer<global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.SetVertexPositionFromByteBuffer_000001AF_0024PostfixBurstDelegate>(SetVertexPositionFromByteBuffer).Value;
				}
				P_0 = Pointer;
			}

			private static global::System.IntPtr GetFunctionPointer()
			{
				nint result = 0;
				GetFunctionPointerDiscard(ref result);
				return result;
			}

			public unsafe static void Invoke(in global::Unity.Collections.NativeArray<byte> buffer, in global::Unity.Collections.NativeArray<int> indices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, int stride)
			{
				if (global::Unity.Burst.BurstCompiler.IsEnabled)
				{
					global::System.IntPtr functionPointer = GetFunctionPointer();
					if (functionPointer != (global::System.IntPtr)0)
					{
						((delegate* unmanaged[Cdecl]<ref global::Unity.Collections.NativeArray<byte>, ref global::Unity.Collections.NativeArray<int>, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3>, int, void>)functionPointer)(ref buffer, ref indices, ref vertices, stride);
						return;
					}
				}
				SetVertexPositionFromByteBuffer_0024BurstManaged(in buffer, in indices, ref vertices, stride);
			}
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002EValidateBoneWeights_000001AE_0024PostfixBurstDelegate))]
		internal static bool ValidateBoneWeights(in global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> boneWeights, int bindPoseCount)
		{
			return global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.ValidateBoneWeights_000001AE_0024BurstDirectCall.Invoke(in boneWeights, bindPoseCount);
		}

		[global::Unity.Burst.BurstCompile]
		[global::AOT.MonoPInvokeCallback(typeof(UnityEngine_002EU2D_002EAnimation_002ESetVertexPositionFromByteBuffer_000001AF_0024PostfixBurstDelegate))]
		internal static void SetVertexPositionFromByteBuffer(in global::Unity.Collections.NativeArray<byte> buffer, in global::Unity.Collections.NativeArray<int> indices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, int stride)
		{
			global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.SetVertexPositionFromByteBuffer_000001AF_0024BurstDirectCall.Invoke(in buffer, in indices, ref vertices, stride);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal static bool ValidateBoneWeights_0024BurstManaged(in global::UnityEngine.U2D.Animation.NativeCustomSlice<global::UnityEngine.BoneWeight> boneWeights, int bindPoseCount)
		{
			int length = boneWeights.Length;
			for (int i = 0; i < length; i++)
			{
				global::UnityEngine.BoneWeight boneWeight = boneWeights[i];
				int boneIndex = boneWeight.boneIndex0;
				int boneIndex2 = boneWeight.boneIndex1;
				int boneIndex3 = boneWeight.boneIndex2;
				int boneIndex4 = boneWeight.boneIndex3;
				if (boneIndex < 0 || boneIndex >= bindPoseCount || boneIndex2 < 0 || boneIndex2 >= bindPoseCount || boneIndex3 < 0 || boneIndex3 >= bindPoseCount || boneIndex4 < 0 || boneIndex4 >= bindPoseCount)
				{
					return false;
				}
			}
			return true;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		[global::Unity.Burst.BurstCompile]
		internal unsafe static void SetVertexPositionFromByteBuffer_0024BurstManaged(in global::Unity.Collections.NativeArray<byte> buffer, in global::Unity.Collections.NativeArray<int> indices, ref global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, int stride)
		{
			byte* unsafeReadOnlyPtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(buffer);
			for (int i = 0; i < indices.Length; i++)
			{
				int num = indices[i];
				global::UnityEngine.Vector3* ptr = (global::UnityEngine.Vector3*)(unsafeReadOnlyPtr + num * stride);
				vertices[num] = *ptr;
			}
		}
	}
}

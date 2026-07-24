namespace UnityEngine.U2D.Animation
{
	internal static class SpriteSkinUtility
	{
		internal static bool CanUseGpuDeformation()
		{
			return global::UnityEngine.SystemInfo.supportsComputeShaders;
		}

		internal static bool IsUsingGpuDeformation()
		{
			if (CanUseGpuDeformation() && global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null && global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset != null && global::UnityEngine.Rendering.Universal.UniversalRenderPipeline.asset.useSRPBatcher)
			{
				return global::UnityEngine.U2D.Common.InternalEngineBridge.IsGPUSkinningEnabled();
			}
			return false;
		}

		internal static bool IsGpuDeformationActive(global::UnityEngine.SpriteRenderer spriteRenderer)
		{
			if (CanUseGpuDeformation() && global::UnityEngine.U2D.Common.InternalEngineBridge.IsSRPBatchingEnabled(spriteRenderer))
			{
				return global::UnityEngine.U2D.Common.InternalEngineBridge.IsGPUSkinningEnabled();
			}
			return false;
		}

		internal static bool CanSpriteSkinUseGpuDeformation(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (global::UnityEngine.U2D.Common.InternalEngineBridge.IsSRPBatchingEnabled(spriteSkin.spriteRenderer) && IsUsingGpuDeformation())
			{
				return global::UnityEngine.U2D.Animation.GpuDeformationSystem.DoesShaderSupportGpuDeformation(spriteSkin.spriteRenderer.sharedMaterial);
			}
			return false;
		}

		internal static global::UnityEngine.U2D.Animation.SpriteSkinState Validate(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			global::UnityEngine.Sprite sprite = spriteSkin.sprite;
			if (sprite == null)
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.SpriteNotFound;
			}
			int length = sprite.GetBindPoses().Length;
			if (length == 0)
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.SpriteHasNoSkinningInformation;
			}
			if (spriteSkin.rootBone == null)
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.RootTransformNotFound;
			}
			if (spriteSkin.boneTransforms == null)
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.InvalidTransformArray;
			}
			if (length != spriteSkin.boneTransforms.Length)
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.InvalidTransformArrayLength;
			}
			global::UnityEngine.Transform[] boneTransforms = spriteSkin.boneTransforms;
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				if (boneTransforms[i] == null)
				{
					return global::UnityEngine.U2D.Animation.SpriteSkinState.TransformArrayContainsNull;
				}
			}
			if (!global::UnityEngine.U2D.Animation.BurstedSpriteSkinUtilities.ValidateBoneWeights(spriteSkin.spriteBoneWeights, length))
			{
				return global::UnityEngine.U2D.Animation.SpriteSkinState.InvalidBoneWeights;
			}
			return global::UnityEngine.U2D.Animation.SpriteSkinState.Ready;
		}

		internal static void CreateBoneHierarchy(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (spriteSkin.spriteRenderer.sprite == null)
			{
				throw new global::System.InvalidOperationException("SpriteRenderer has no Sprite set");
			}
			global::UnityEngine.U2D.SpriteBone[] bones = spriteSkin.spriteRenderer.sprite.GetBones();
			global::UnityEngine.Transform[] array = new global::UnityEngine.Transform[bones.Length];
			global::UnityEngine.Transform transform = null;
			for (int i = 0; i < bones.Length; i++)
			{
				CreateGameObject(i, bones, array, spriteSkin.transform);
				if (bones[i].parentId < 0 && transform == null)
				{
					transform = array[i];
				}
			}
			spriteSkin.SetRootBone(transform);
			spriteSkin.SetBoneTransforms(array);
		}

		internal static int GetVertexStreamSize(this global::UnityEngine.Sprite sprite)
		{
			int num = 12;
			if (sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Normal))
			{
				num += 12;
			}
			if (sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				num += 16;
			}
			return num;
		}

		internal static int GetVertexStreamOffset(this global::UnityEngine.Sprite sprite, global::UnityEngine.Rendering.VertexAttribute channel)
		{
			bool flag = sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Position);
			bool flag2 = sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Normal);
			bool flag3 = sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent);
			switch (channel)
			{
			case global::UnityEngine.Rendering.VertexAttribute.Position:
				if (!flag)
				{
					return -1;
				}
				return 0;
			case global::UnityEngine.Rendering.VertexAttribute.Normal:
				if (!flag2)
				{
					return -1;
				}
				return 12;
			case global::UnityEngine.Rendering.VertexAttribute.Tangent:
				if (!flag3)
				{
					return -1;
				}
				if (!flag2)
				{
					return 12;
				}
				return 24;
			default:
				return -1;
			}
		}

		private static void CreateGameObject(int index, global::UnityEngine.U2D.SpriteBone[] spriteBones, global::UnityEngine.Transform[] transforms, global::UnityEngine.Transform root)
		{
			if (transforms[index] == null)
			{
				global::UnityEngine.U2D.SpriteBone spriteBone = spriteBones[index];
				if (spriteBone.parentId >= 0)
				{
					CreateGameObject(spriteBone.parentId, spriteBones, transforms, root);
				}
				global::UnityEngine.Transform transform = new global::UnityEngine.GameObject(spriteBone.name).transform;
				if (spriteBone.parentId >= 0)
				{
					transform.SetParent(transforms[spriteBone.parentId]);
				}
				else
				{
					transform.SetParent(root);
				}
				transform.localPosition = spriteBone.position;
				transform.localRotation = spriteBone.rotation;
				transform.localScale = global::UnityEngine.Vector3.one;
				transforms[index] = transform;
			}
		}

		private unsafe static int GetHash(global::UnityEngine.Matrix4x4 matrix)
		{
			uint* ptr = (uint*)(&matrix);
			char* pBuffer = (char*)ptr;
			return (int)global::Unity.Mathematics.math.hash(pBuffer, 64);
		}

		internal static int CalculateTransformHash(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			int num = 0;
			int num2 = GetHash(spriteSkin.transform.localToWorldMatrix) >> num;
			num++;
			global::UnityEngine.Transform[] boneTransforms = spriteSkin.boneTransforms;
			foreach (global::UnityEngine.Transform transform in boneTransforms)
			{
				num2 ^= GetHash(transform.localToWorldMatrix) >> num;
				num = (num + 1) % 8;
			}
			return num2;
		}

		internal unsafe static void Deform(global::UnityEngine.Sprite sprite, global::UnityEngine.Matrix4x4 rootInv, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> tangents, global::Unity.Collections.NativeSlice<global::UnityEngine.BoneWeight> boneWeights, global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> boneTransforms, global::Unity.Collections.NativeSlice<global::UnityEngine.Matrix4x4> bindPoses, global::Unity.Collections.NativeArray<byte> deformableVertices)
		{
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> vertices2 = vertices.SliceWithStride<global::Unity.Mathematics.float3>();
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4> tangents2 = tangents.SliceWithStride<global::Unity.Mathematics.float4>();
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4x4> bindPoses2 = bindPoses.SliceWithStride<global::Unity.Mathematics.float4x4>();
			int vertexCount = sprite.GetVertexCount();
			int vertexStreamSize = sprite.GetVertexStreamSize();
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> boneTransforms2 = global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<global::Unity.Mathematics.float4x4>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(boneTransforms), boneTransforms.Length, global::Unity.Collections.Allocator.None);
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(deformableVertices);
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> deformed = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float3>(unsafePtr, vertexStreamSize, vertexCount);
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4> deformedTangents = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float4>(unsafePtr, vertexStreamSize, 1);
			if (sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				deformedTangents = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float4>(unsafePtr + sprite.GetVertexStreamOffset(global::UnityEngine.Rendering.VertexAttribute.Tangent), vertexStreamSize, vertexCount);
			}
			if (sprite.HasVertexAttribute(global::UnityEngine.Rendering.VertexAttribute.Tangent))
			{
				Deform(rootInv, vertices2, tangents2, boneWeights, boneTransforms2, bindPoses2, deformed, deformedTangents);
			}
			else
			{
				Deform(rootInv, vertices2, boneWeights, boneTransforms2, bindPoses2, deformed);
			}
		}

		internal static void Deform(global::Unity.Mathematics.float4x4 rootInv, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> vertices, global::Unity.Collections.NativeSlice<global::UnityEngine.BoneWeight> boneWeights, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> boneTransforms, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4x4> bindPoses, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> deformed)
		{
			if (boneTransforms.Length != 0)
			{
				for (int i = 0; i < boneTransforms.Length; i++)
				{
					global::Unity.Mathematics.float4x4 b = bindPoses[i];
					global::Unity.Mathematics.float4x4 a = boneTransforms[i];
					boneTransforms[i] = global::Unity.Mathematics.math.mul(rootInv, global::Unity.Mathematics.math.mul(a, b));
				}
				for (int j = 0; j < vertices.Length; j++)
				{
					int boneIndex = boneWeights[j].boneIndex0;
					int boneIndex2 = boneWeights[j].boneIndex1;
					int boneIndex3 = boneWeights[j].boneIndex2;
					int boneIndex4 = boneWeights[j].boneIndex3;
					global::Unity.Mathematics.float3 b2 = vertices[j];
					deformed[j] = global::Unity.Mathematics.math.transform(boneTransforms[boneIndex], b2) * boneWeights[j].weight0 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex2], b2) * boneWeights[j].weight1 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex3], b2) * boneWeights[j].weight2 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex4], b2) * boneWeights[j].weight3;
				}
			}
		}

		internal static void Deform(global::Unity.Mathematics.float4x4 rootInv, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> vertices, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4> tangents, global::Unity.Collections.NativeSlice<global::UnityEngine.BoneWeight> boneWeights, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float4x4> boneTransforms, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4x4> bindPoses, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> deformed, global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float4> deformedTangents)
		{
			if (boneTransforms.Length != 0)
			{
				for (int i = 0; i < boneTransforms.Length; i++)
				{
					global::Unity.Mathematics.float4x4 b = bindPoses[i];
					global::Unity.Mathematics.float4x4 a = boneTransforms[i];
					boneTransforms[i] = global::Unity.Mathematics.math.mul(rootInv, global::Unity.Mathematics.math.mul(a, b));
				}
				for (int j = 0; j < vertices.Length; j++)
				{
					int boneIndex = boneWeights[j].boneIndex0;
					int boneIndex2 = boneWeights[j].boneIndex1;
					int boneIndex3 = boneWeights[j].boneIndex2;
					int boneIndex4 = boneWeights[j].boneIndex3;
					global::Unity.Mathematics.float3 b2 = vertices[j];
					deformed[j] = global::Unity.Mathematics.math.transform(boneTransforms[boneIndex], b2) * boneWeights[j].weight0 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex2], b2) * boneWeights[j].weight1 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex3], b2) * boneWeights[j].weight2 + global::Unity.Mathematics.math.transform(boneTransforms[boneIndex4], b2) * boneWeights[j].weight3;
					global::Unity.Mathematics.float4 b3 = new global::Unity.Mathematics.float4(tangents[j].xyz, 0f);
					b3 = global::Unity.Mathematics.math.mul(boneTransforms[boneIndex], b3) * boneWeights[j].weight0 + global::Unity.Mathematics.math.mul(boneTransforms[boneIndex2], b3) * boneWeights[j].weight1 + global::Unity.Mathematics.math.mul(boneTransforms[boneIndex3], b3) * boneWeights[j].weight2 + global::Unity.Mathematics.math.mul(boneTransforms[boneIndex4], b3) * boneWeights[j].weight3;
					deformedTangents[j] = new global::Unity.Mathematics.float4(global::Unity.Mathematics.math.normalize(b3.xyz), tangents[j].w);
				}
			}
		}

		internal static void Deform(global::UnityEngine.Sprite sprite, global::UnityEngine.Matrix4x4 invRoot, global::UnityEngine.Transform[] boneTransformsArray, global::Unity.Collections.NativeArray<byte> deformVertexData)
		{
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> vertexAttribute = sprite.GetVertexAttribute<global::UnityEngine.Vector3>(global::UnityEngine.Rendering.VertexAttribute.Position);
			global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> vertexAttribute2 = sprite.GetVertexAttribute<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.VertexAttribute.Tangent);
			global::Unity.Collections.NativeSlice<global::UnityEngine.BoneWeight> vertexAttribute3 = sprite.GetVertexAttribute<global::UnityEngine.BoneWeight>(global::UnityEngine.Rendering.VertexAttribute.BlendWeight);
			global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> bindPoses = sprite.GetBindPoses();
			global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4> boneTransforms = new global::Unity.Collections.NativeArray<global::UnityEngine.Matrix4x4>(boneTransformsArray.Length, global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < boneTransformsArray.Length; i++)
			{
				boneTransforms[i] = boneTransformsArray[i].localToWorldMatrix;
			}
			Deform(sprite, invRoot, vertexAttribute, vertexAttribute2, vertexAttribute3, boneTransforms, bindPoses, deformVertexData);
			boneTransforms.Dispose();
		}

		internal static void Bake(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin, global::Unity.Collections.NativeArray<byte> deformVertexData)
		{
			if (!spriteSkin.isValid)
			{
				throw new global::System.Exception("Bake error: invalid SpriteSkin");
			}
			Deform(spriteSkin.spriteRenderer.sprite, boneTransformsArray: spriteSkin.boneTransforms, invRoot: global::UnityEngine.Matrix4x4.identity, deformVertexData: deformVertexData);
		}

		internal unsafe static void CalculateBounds(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			global::UnityEngine.Sprite sprite = spriteSkin.sprite;
			global::Unity.Collections.NativeArray<byte> nativeArray = new global::Unity.Collections.NativeArray<byte>(sprite.GetVertexStreamSize() * sprite.GetVertexCount(), global::Unity.Collections.Allocator.Temp, global::Unity.Collections.NativeArrayOptions.UninitializedMemory);
			global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::UnityEngine.Vector3>(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray), sprite.GetVertexStreamSize(), sprite.GetVertexCount());
			spriteSkin.Bake(nativeArray);
			spriteSkin.UpdateBounds(nativeArray);
			nativeArray.Dispose();
		}

		internal static global::UnityEngine.Bounds CalculateSpriteSkinBounds(global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> deformablePositions)
		{
			global::Unity.Mathematics.float3 float5 = deformablePositions[0];
			global::Unity.Mathematics.float3 float6 = deformablePositions[0];
			for (int i = 1; i < deformablePositions.Length; i++)
			{
				float5 = global::Unity.Mathematics.math.min(float5, deformablePositions[i]);
				float6 = global::Unity.Mathematics.math.max(float6, deformablePositions[i]);
			}
			global::Unity.Mathematics.float3 float7 = (float6 - float5) * 0.5f;
			global::Unity.Mathematics.float3 float8 = float5 + float7;
			return new global::UnityEngine.Bounds
			{
				center = float8,
				extents = float7
			};
		}

		internal unsafe static void UpdateBounds(this global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin, global::Unity.Collections.NativeArray<byte> deformedVertices)
		{
			byte* unsafePtr = (byte*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(deformedVertices);
			int vertexCount = spriteSkin.sprite.GetVertexCount();
			int vertexStreamSize = spriteSkin.sprite.GetVertexStreamSize();
			global::Unity.Collections.NativeSlice<global::Unity.Mathematics.float3> deformablePositions = global::Unity.Collections.LowLevel.Unsafe.NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<global::Unity.Mathematics.float3>(unsafePtr, vertexStreamSize, vertexCount);
			spriteSkin.bounds = CalculateSpriteSkinBounds(deformablePositions);
			global::UnityEngine.U2D.Common.InternalEngineBridge.SetLocalAABB(spriteSkin.spriteRenderer, spriteSkin.bounds);
		}
	}
}
